using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TeleBajaUEA.ClassesAuxiliares;

namespace TeleBajaUEA
{
    public partial class Configurações : FormPrincipal
    {
        public Configurações()
        {
            InitializeComponent();
            string[] portList = SerialPort.GetPortNames();

            // TODO tratar caso de não haver nenhuma porta disponível
            foreach (string portName in portList)
                comboUSB.Items.Add(portName);

            // TODO seleção automática de configuração salva da porta USB
            if (!Program.Settings.PortXBee.Equals("NULL") &&
                    comboUSB.Items.Contains(Program.Settings.PortXBee))
            {
                comboUSB.SelectedItem = comboUSB.Items.IndexOf(Program.Settings.PortXBee);
            }

            checkKeepBackup.Checked = Program.Settings.KeepBackup;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            SettingsFile.SaveToFile();
            CloseOnlyThis();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            CloseOnlyThis();
        }

        private void checkKeepBackup_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.KeepBackup = checkKeepBackup.Checked;
        }
    }
}
