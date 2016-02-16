using System;
using System.Windows.Forms;
using TeleBajaUEA.RaceDataStructs;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeleBajaUEA.ClassesAuxiliares;

namespace TeleBajaUEA.GravacaoDeCorrida
{
    // Configuração de gráficos na na partial class ChartSettings
    public partial class GravarCorrida : FormPrincipal
    {
        public double PointsCount { get { return (dataList.Count); } }
        private StatusDaConexao formStatusDaConexao;

        // flag usada para, ao clicar em "encerrar e salvar", alertar o evento
        // form_closing de que já foi salvo
        private bool confirmClose = false;

        private Timer timerCheckIncomeData;
        private List<FileSensorsData> dataList;
        private RaceParameters parameters;

        // TODO criar timer para a cada 5min salvar os dados no arquivo temporario
        private Timer timerBackupData;

        private Timer timerCheckConn;
        private readonly int CHECK_CONN_INTERVAL = 5000;
        private uint prevTotalBytes;

        // o primeiro Millis será usado como referência (zero) do gráfico no eixo X
        private uint zeroMillis;

        public GravarCorrida(RaceParameters pParameters)
        {
            InitializeComponent();

            parameters = pParameters;
            dataList = new List<FileSensorsData>();

            // Prepara timer que vai atualizar o gráfico a cada UPDATE_RATE ms
            timerCheckIncomeData = new Timer();
            timerCheckIncomeData.Interval = UPDATE_RATE;
            timerCheckIncomeData.Tick += new EventHandler(TickCheckIncomeData);

            // Prepara timer que vai fazer backup de segurança continuamente
            timerBackupData = new Timer();
            timerBackupData.Interval = UPDATE_BACKUP_RATE;
            timerBackupData.Tick += new EventHandler(TickBackupData);

            // Preparar timer que vai detectar quando perder conexao
            timerCheckConn = new Timer();
            timerCheckConn.Interval = CHECK_CONN_INTERVAL;
            timerCheckConn.Tick += new EventHandler(TimerCheckConn_Tick);
        }

        private void TimerCheckConn_Tick(object sender, EventArgs e)
        {
            if (prevTotalBytes == CarConnection.TotalReceivedBytes)
            {
                imgConnStatus.Image = Properties.Resources.conn_off;
                labelSemSinal.Visible = true;
                labelForca.Visible = false;
            }

            prevTotalBytes = CarConnection.TotalReceivedBytes;
        }

        private void TickBackupData(object sender, EventArgs e)
        {
            RaceData raceData = new RaceData(dataList, parameters);
            RaceFile.UpdateTempFile(raceData);
        }

        public async void StartUpdateCharts()
        {
            try
            {
                SensorsData firstData;
                firstData = await CarConnection.GetNextData();
                zeroMillis = firstData.Millis;
                await UpdateData(firstData);

                timerCheckIncomeData.Enabled = true;
                // ---------TESTE---------
                //return; // impede timers de checagem executarem
                // -----------------------
                timerBackupData.Enabled = true;
                timerCheckConn.Enabled = true;

                formStatusDaConexao = new StatusDaConexao(this);
                formStatusDaConexao.Show();
            }
            catch (ErrorMessage.InvalidProtocolException exception)
            {
                ErrorMessage.Show(ErrorType.Error, ErrorReason.ReceiveFromCarFail, exception.Message);
                ReopenSetup();
            }
            catch (ErrorMessage.ReceiveDataTimeoutException exception)
            {
                ErrorMessage.Show(ErrorType.Error, ErrorReason.ReceiveFromCarFail, exception.Message);
                ReopenSetup();
            }
            catch (Exception exception)
            {
                ErrorMessage.Show(ErrorType.Error, ErrorReason.ReceiveFromCarFail, exception.Message);
                ReopenSetup();
            }
        }

        // TODO tratar quando uma exceção for lançada (encerrar corrida, etc.)
        private async void TickCheckIncomeData(object source, EventArgs e)
        {
            try
            {
                timerCheckIncomeData.Tick -= TickCheckIncomeData;
                SensorsData newData = await CarConnection.GetNextData();
                await UpdateData(newData);
                UpdateConnectionStatus();
                timerCheckIncomeData.Tick += TickCheckIncomeData;
            }
            catch (ErrorMessage.InvalidProtocolException exception)
            {
                ErrorMessage.Show(ErrorType.Error, ErrorReason.ReceiveFromCarFail, exception.Message);
                ReopenSetup();
            }
            catch(ErrorMessage.ReceiveDataTimeoutException exception)
            {
                ErrorMessage.Show(ErrorType.Error, ErrorReason.ReceiveFromCarFail, exception.Message);
                ErrorMessage.Show(ErrorType.Info, ErrorReason.BackupWillBeSaved);
                ReopenSetup();
            }
            catch(ErrorMessage.PortClosedOnReadException exception)
            {
                return; // não faz nada, porta foi fechada porque usuário encerrou
            }
            catch (Exception exception)
            {
                ErrorMessage.Show(ErrorType.Error, ErrorReason.ReceiveFromCarFail, exception.Message);
                ReopenSetup();
            }
        }

        private async Task UpdateData(SensorsData pNewData)
        {
            await Task.Run(() =>
            {
                // necessário por chamada se originar de um evento (timer)
                // esse invoke foi modificado (extensão WindowsFormsInvokingExtensions)
                // facilita a chamada de funções de modo "thread-safe"
                this.Invoke(() =>
                {

                    // TODO verificar necessidade de remover pontos não mais mostrados
                    // acho que NÃO precisa... chartDinamic.Series[0].Points.RemoveAt(0);
                    double interval = chartDinamic.ChartAreas["ChartArea1"].AxisX.Interval;

                    // TODO capturar último X impresso no gráfico real
                    //double lastX = chartDinamic.Series[0].Points.Last().XValue;
                    //double lastX = previousMillis;
                    //if (lastX >= currentMaximumX)
                    if ((pNewData.Millis -zeroMillis) >= maxX)
                    {
                        UpdateGraphLimits();
                    }

                    AddNewDataToGraph(pNewData);
                    UpdateGauges(pNewData);
                    UpdateTextBoxes(pNewData);
                });
            });
        }

        private void UpdateGauges(SensorsData pNewData)
        {
            aGaugeTemperature.Value = pNewData.EngineTemperature;
        }

        private void UpdateTextBoxes(SensorsData pNewData)
        {
            textVelocidade.Text = " " + pNewData.Speed;
            textRPM.Text = " " + pNewData.RPM;
            if (pNewData.BreakState)
            {
                textFreio.Text = "ON";
                textFreio.ForeColor = System.Drawing.Color.Lime;
            }
            else
            {
                textFreio.Text = "OFF";
                textFreio.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void AddNewDataToGraph(SensorsData pNewData)
        {
            uint currentXValue = pNewData.Millis - zeroMillis;
            
            // ajusta valor do RPM para ficar proporcional à altura do gráfico
            double newRPMData = pNewData.RPM / (RPM_MAXIMUM / Y_AXIS_MAXIMUM);

            double brakePosition;
            if (pNewData.BreakState)
                brakePosition = (Y_AXIS_MAXIMUM / 2) + Y_AXIS_INTERVAL;
            else
                brakePosition = (Y_AXIS_MAXIMUM / 2) - Y_AXIS_INTERVAL;

            chartDinamic.Series["Speed"].Points.AddXY(currentXValue, pNewData.Speed);
            chartDinamic.Series["RPM"].Points.AddXY(currentXValue, newRPMData);
            chartDinamic.Series["Brake"].Points.AddXY(currentXValue, brakePosition);

            FileSensorsData fileNewData = new FileSensorsData
            {
                xValue = currentXValue,
                speed = pNewData.Speed,
                rpm = pNewData.RPM,
                breakState = pNewData.BreakState,
            };
            dataList.Add(fileNewData);
        }

        private void UpdateGraphLimits()
        {
            minX += UPDATE_LIMITS_INTERVAL;
            maxX += UPDATE_LIMITS_INTERVAL;

            chartDinamic.ChartAreas["ChartArea1"].AxisX.Minimum = minX;
            chartDinamic.ChartAreas["ChartArea1"].AxisX.Maximum = maxX;
            
            UpdateXLabels();
            chartDinamic.Update();
        }

        private void UpdateConnectionStatus()
        {
            switch (CarConnection.ConnStatus)
            {
                case SignalStrg.Off:
                    imgConnStatus.Image = Properties.Resources.conn_off;
                    break;
                case SignalStrg.Low:
                    imgConnStatus.Image = Properties.Resources.conn_low;
                    break;
                case SignalStrg.Medium:
                    imgConnStatus.Image = Properties.Resources.conn_med;
                    break;
                case SignalStrg.Good:
                    imgConnStatus.Image = Properties.Resources.conn_good;
                    break;
                case SignalStrg.Excelent:
                    imgConnStatus.Image = Properties.Resources.conn_hi;
                    break;
            }
            if(CarConnection.ConnStatus == SignalStrg.Off)
            {
                labelSemSinal.Visible = true;
                labelForca.Visible = false;
            }
            else
            {
                labelSemSinal.Visible = false;
                labelForca.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CloseOnlyThis();
            Program.ShowMenuPrincipal();
        }

        private void GravarCorrida_FormClosing(object sender, FormClosingEventArgs e)
        {
            // se fechou pelo botao "Encerrar e Salvar" essa flag será true, logo
            // não requer confirmar novamente. De outro modo, só pode ter dado
            // Alt+f4 ou apertado no botão "x"
            if (!confirmClose)
            {
                var result = MessageBox.Show(
                    "Tem certeza que deseja sair sem salvar?", // mensagem da janela
                    "Sair", // titulo da janela
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                e.Cancel = (result == DialogResult.No);

                // Interrompe recebimento de dados
                if (result == DialogResult.Yes)
                    StopReceiveData();
            }
        }

        private void GravarCorrida_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(timerCheckIncomeData != null)
            {
                timerCheckIncomeData.Dispose();
                timerCheckIncomeData = null;
            }

            if(timerBackupData != null)
            {
                timerBackupData.Dispose();
                timerBackupData = null;
            }

            // Ao encerrar corrida mantém o arquivo de backup para que mesmo
            // o usuário optando por "sair sem salvar", manterá um backup
            // Mas isso apenas se o usuário configurou nas opções
            if (Program.Settings.KeepBackup)
                RaceFile.SaveToBackupDir();
        }

        private async void btEncerrar_Click(object sender, EventArgs e)
        {
            // Exibe mensagem confirmando se deseja encerrar a gravação de corrida
            var confirmaEncerrar = MessageBox.Show(
                "Tem certeza que deseja Encerrar Corrida?", // mensagem da janela
                "Encerrar Corrida", // titulo da janela
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (confirmaEncerrar == DialogResult.Yes)
            {
                // Interrompe recebimento de dados
                StopReceiveData();

                SetSeriesDisabled();

                // Desativa janela de Gravar para exibir confirmacao + salvamento
                Enabled = false;

                await PerguntaSalvarCorrida();
            }
        }

        private void ReopenSetup()
        {
            CarConnection.CloseConnection();
            CloseNoConfirmation();
            GravarCorridaSetup corridaSetup = new GravarCorridaSetup(parameters);
            corridaSetup.Show();
        }

        // fecha janela de gravação sem confirmar se deseja salvar
        private void CloseNoConfirmation()
        {
            confirmClose = true;
            CloseOnlyThis();
        }

        private async Task PerguntaSalvarCorrida()
        {
            // Exibe mensagem confirmando se deseja encerrar a gravação de corrida
            var confirmaSalvar = MessageBox.Show(
                "Você deseja Salvar essa Corrida?", // mensagem da janela
                "Encerrar Corrida", // titulo da janela
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);

            switch (confirmaSalvar)
            {
                case DialogResult.Yes:
                    await ConfirmaGravarCorrida();
                    break;
                
                case DialogResult.No:
                    CloseNoConfirmation();
                    Program.ShowMenuPrincipal();
                    break;
            }
        }

        private async Task ConfirmaGravarCorrida()
        {
            // Prepara janela para salvar dados em disco (arquivo .tbu)
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Arquivos TeleBajaUEA (*.tbu)|*.tbu|Todos os Arquivos (*.*)|*.*";
            saveDialog.FilterIndex = 1;
            saveDialog.RestoreDirectory = true;

            // Loop para re-exibir confirmação de "sair sem salvar" caso o usuário
            // clique en "Cancelar" na janela de salvamento
            bool canContinue = false;
            while (!canContinue)
            {
                switch (saveDialog.ShowDialog())
                {
                    // escreveu nome do arquivo e optou por salvar (OK)
                    case DialogResult.OK:
                        // TODO verificar real necessidade de ser async...
                        await SaveRaceToFile(saveDialog.FileName);
                        CloseNoConfirmation();
                        Program.ShowMenuPrincipal();
                        canContinue = true;
                        break;

                    // cancelou salvamento
                    case DialogResult.Cancel:
                        // Exibe mensagem confirmando se deseja encerrar a gravação de corrida
                        var confirmaNaoSalvar = ShowConfirmaNaoSalvar();

                        if (confirmaNaoSalvar == DialogResult.Yes)
                        {
                            CloseNoConfirmation();
                            Program.ShowMenuPrincipal();
                            canContinue = true;
                        }// else continua no while...
                        break;
                }
            }
        }

        private DialogResult ShowConfirmaNaoSalvar()
        {
            return MessageBox.Show(
                            "Tem certeza que deseja encerrar sem salvar?", // mensagem da janela
                            "Encerrar Corrida", // titulo da janela
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2);
        }

        private async Task SaveRaceToFile(string pFileName)
        {
            RaceData raceData = new RaceData(dataList, parameters);
            await RaceFile.SaveToFile(pFileName, raceData);
        }

        private void StopReceiveData()
        {
            if(timerCheckIncomeData != null && timerCheckIncomeData.Enabled)
            {
                timerCheckIncomeData.Stop();
                timerCheckIncomeData.Tick -= new EventHandler(TickCheckIncomeData);
            }

            if(timerBackupData != null && timerBackupData.Enabled)
            {
                timerBackupData.Stop();
                timerBackupData.Tick -= new EventHandler(TickBackupData);
            }

            CarConnection.CloseConnection();

            if(formStatusDaConexao != null && formStatusDaConexao.Visible)
                formStatusDaConexao.Close();
        }

        private void checkBoxEnabledSeries_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox setCheckBox = sender as CheckBox;

            // padrão de nomes: checkBoxSpeed, checkBoxBrake...
            string seriesName = setCheckBox.Name.Substring("checkBox".Length);

            if (setCheckBox.Checked)
                chartDinamic.Series[seriesName].Enabled = true;

            else
            {
                if(seriesName == "Speed")
                {
                    if (checkBoxRPM.Checked || checkBoxBrake.Checked)
                        chartDinamic.Series["Speed"].Enabled = false;
                    else
                        checkBoxSpeed.Checked = true;
                }
                if(seriesName == "RPM")
                {
                    if (checkBoxSpeed.Checked || checkBoxBrake.Checked)
                        chartDinamic.Series["RPM"].Enabled = false;
                    else
                        checkBoxRPM.Checked = true;
                }
                if(seriesName == "Brake")
                {
                    if (checkBoxSpeed.Checked || checkBoxRPM.Checked)
                        chartDinamic.Series["Brake"].Enabled = false;
                    else
                        checkBoxBrake.Checked = true;
                }
            }
        }
    }

}
