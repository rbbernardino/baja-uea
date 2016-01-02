using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TeleBajaUEA.ClassesAuxiliares;

namespace TeleBajaUEA
{
    public partial class Configurações : Form
    {
        private readonly string TEXT_NO_AVAIABLE_PORTS = "Nenhuma Porta Disponível";
        private bool avaiablePortExists, hasSavedPort;

        public Configurações()
        {
            InitializeComponent();

            SetPortControls();

            checkKeepBackup.Checked = Program.Settings.KeepBackup;
            textBackupPath.Text = Program.Settings.BackupPath;

            UpdateControls();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            Program.Settings.KeepBackup = checkKeepBackup.Checked;

            // se não houver porta disponível agora, mas em uma execução anterior
            // ela foi configurada, logo está salva no config.xml, mantém a configuração salva
            if (avaiablePortExists && comboUSB.SelectedItem != null)
                Program.Settings.PortXBee = (string)comboUSB.SelectedItem;

            if (Directory.Exists(textBackupPath.Text))
                Program.Settings.BackupPath = textBackupPath.Text;
            else
            {
                FontStyle BIFontStyle = FontStyle.Bold | FontStyle.Italic;
                textBackupPath.Font = new Font(textBackupPath.Font, BIFontStyle);
                ShowDirectoryError();
                return;
            }

            SettingsFile.SaveToFile(Program.Settings);
            Close();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Configurações_FormClosing(object sender, FormClosingEventArgs e)
        {
            AlertPortConfig();
        }

        private void btDefaultPath_Click(object sender, EventArgs e)
        {
            var folderDialog = new Ionic.Utils.FolderBrowserDialogEx();
            folderDialog.Description = "Escolha uma pasta para salvar os arquivos " +
                                        "de backup e configurações:";
            folderDialog.ShowNewFolderButton = true;
            folderDialog.ShowEditBox = true;
            //dlg1.NewStyle = false;
            folderDialog.SelectedPath = textBackupPath.Text;
            folderDialog.ShowFullPathInEditBox = true;
            folderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;

            // Show the FolderBrowserDialog.
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                textBackupPath.Text = folderDialog.SelectedPath + @"\";
            }
        }

        private void SetPortControls()
        {
            string[] portList = SerialPort.GetPortNames();
            hasSavedPort = !Program.Settings.PortXBee.Equals("NULL");

            // TODO tratar caso de não haver nenhuma porta disponível
            if (portList.Count() == 0)
            {
                comboUSB.Items.Add(TEXT_NO_AVAIABLE_PORTS);
                comboUSB.SelectedIndex = 0;
                comboUSB.Font = new Font(comboUSB.Font, FontStyle.Italic);
                comboUSB.Size = new Size(222, 28);
                comboUSB.Enabled = false;

                avaiablePortExists = false;
            }
            else
            {
                avaiablePortExists = true;

                foreach (string portName in portList)
                    comboUSB.Items.Add(portName);

                // seleção automática de configuração salva da porta USB
                if (hasSavedPort)
                    foreach (string item in comboUSB.Items)
                    {
                        if (item.Equals(Program.Settings.PortXBee))
                            comboUSB.SelectedItem = item;
                    }
            }

            if (!avaiablePortExists && hasSavedPort)
            {
                labelPortError.Text += " '" + Program.Settings.PortXBee + "'";
                labelPortError.Visible = true;
            }
            else
                labelPortError.Visible = false;
        }

        // TODO: no caso de nenhuma porta disponível e for manter a config anterior
        //       exibir essa informação na própria janela, ao lado do campo "porta XBee",
        //       pois uma janela de aviso é muito incômodo. Suponha que o usuário
        //       apenas quis mudar a cor das linhas do gráfico da análise, é óbvio
        //       que ele não se importa com a configuração de porta
        //
        //       outra possibilidade é simplesmente não informar nada e fazer tudo
        //       sem feedback. Se for manter a config, deixa aparecendo a config,
        //       se o usuário clicar na combobox não aparece nenhuma opção...
        private void AlertPortConfig()
        {
            if (avaiablePortExists)
            {
                if (comboUSB.SelectedItem == null)
                {
                    string alertMsg =
                            "\t\tATENÇÃO\n\n" +
                            "Existem portas disponíveis, mas nenhuma foi selecionada!\n\n";

                    if (hasSavedPort)
                        alertMsg +=
                            "Foi detectada uma configuração anterior e ela será mantida.\n" +
                            "\nA configuração de porta será: '" + Program.Settings.PortXBee + "'";
                    else
                        alertMsg +=
                            "Nenhuma configuração anterior foi detectada. Para " +
                            "gravar uma corrida, é preciso selecionar uma porta " +
                            "onde o XBee esteja conectado.\n";

                    MessageBox.Show(alertMsg, "TeleBajaUEA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ShowDirectoryError()
        {
            string errorMsg =
                "\t\tERRO\n\n" +
                "O caminho especificado para a pasta de backup não existe!";
            MessageBox.Show(errorMsg, "TeleBajaUEA", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void textBackupPath_Enter(object sender, EventArgs e)
        {
            if (textBackupPath.Font.Italic || textBackupPath.Font.Bold)
                textBackupPath.Font = new Font(textBackupPath.Font, FontStyle.Regular);
        }

        private void checkKeepBackup_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void UpdateControls()
        {
            if (checkKeepBackup.Checked)
                textBackupPath.Enabled = btBackupPath.Enabled = true;
            else
            {
                textBackupPath.Text = Program.Settings.BackupPath;
                textBackupPath.Enabled = btBackupPath.Enabled = false;
            }
        }

    }
}
