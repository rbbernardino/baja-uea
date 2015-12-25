using System;
using System.Windows.Forms;
using TeleBajaUEA.RaceDataStructs;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeleBajaUEA.ClassesAuxiliares;

namespace TeleBajaUEA
{
    // Configuração de gráficos na na partial class ChartSettings
    public partial class GravarCorrida : FormPrincipal
    {
        // flag usada para, ao clicar em "encerrar e salvar", alertar o evento
        // form_closing de que já foi salvo
        private bool closeWithoutConfirmation = false;

        private Timer timerCheckIncomeData;
        private List<FileSensorsData> dataList;
        private RaceParameters parameters;

        // TODO criar timer para a cada 5min salvar os dados no arquivo temporario
        private Timer timerBackupData;

        // ------------- temporário para testar -------------------//
        //public TESTEJanelaSensores formTesteMQSQ;
        //--------------------------------------------------------//

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
        }

        private void TickBackupData(object sender, EventArgs e)
        {
            RaceData raceData = new RaceData(dataList, parameters);
            RaceFile.UpdateTempFile(raceData);
        }

        public async void StartUpdateCharts()
        {
            SensorsData firstData;
            firstData = await CarConnection.GetNextData();
            zeroMillis = firstData.Millis;
            await UpdateData(firstData);

            timerCheckIncomeData.Enabled = true;
            timerBackupData.Enabled = true;
        }

        private async void TickCheckIncomeData(object source, EventArgs e)
        {
            timerCheckIncomeData.Tick -= TickCheckIncomeData;
            SensorsData newData = await CarConnection.GetNextData();
            await UpdateData(newData);
            //UpdateTESTEformMillis(data.Millis - zeroMillis);
            timerCheckIncomeData.Tick += TickCheckIncomeData;
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
                textFreio.ForeColor = System.Drawing.Color.DarkRed;
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

        // --------------- teste ------------------
        //private void UpdateTESTEform(SensorsData newData)
        //{
        //    formTesteMQSQ.SetData(this, newData);
        //}
        //private void UpdateTESTEformMillis(uint millis)
        //{
        //    formTesteMQSQ.SetMillis(this, millis);
        //}

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
            if (!closeWithoutConfirmation)
            {
                var result = MessageBox.Show(
                    "Tem certeza que deseja sair sem salvar?", // mensagem da janela
                    "Sair", // titulo da janela
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                e.Cancel = (result == DialogResult.No);

                if (result == DialogResult.Yes)
                {
                    // Interrompe recebimento de dados
                    StopReceiveData();

                    //---------- temporário para teste ---------------------- //
                    //formTesteMQSQ.Close();
                }
            }
        }

        private void GravarCorrida_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerCheckIncomeData.Dispose();
            timerCheckIncomeData = null;

            timerBackupData.Dispose();
            timerBackupData = null;

            // Ao encerrar corrida mantém o arquivo de backup para que mesmo
            // o usuário optando por "sair sem salvar", manterá um backup
            // Mas isso apenas se o usuário quiser
            if(ProgramSettings.KeepBackup)
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

                // Desativa janela de Gravar para exibir confirmacao + salvamento
                Enabled = false;

                await ConfirmaGravarCorrida();
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
                        closeWithoutConfirmation = true;
                        CloseOnlyThis();
                        Program.ShowMenuPrincipal();
                        canContinue = true;
                        break;

                    // cancelou salvamento
                    case DialogResult.Cancel:
                        // Exibe mensagem confirmando se deseja encerrar a gravação de corrida
                        var confirmaNaoSalvar = MessageBox.Show(
                            "Tem certeza que deseja encerrar sem salvar?", // mensagem da janela
                            "Encerrar Corrida", // titulo da janela
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2);

                        if (confirmaNaoSalvar == DialogResult.Yes)
                        {
                            closeWithoutConfirmation = true;
                            CloseOnlyThis();
                            Program.ShowMenuPrincipal();
                            canContinue = true;
                        }// else continua no while...
                        break;
                }
            }
        }

        private async Task SaveRaceToFile(string pFileName)
        {
            RaceData raceData = new RaceData(dataList, parameters);
            await RaceFile.SaveToFile(pFileName, raceData);
        }

        private void StopReceiveData()
        {
            timerCheckIncomeData.Stop();
            timerCheckIncomeData.Tick -= new EventHandler(TickCheckIncomeData);

            timerBackupData.Stop();
            timerBackupData.Tick -= new EventHandler(TickBackupData);

            CarConnection.CloseConnection();
        }
    }

}
