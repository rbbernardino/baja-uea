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
            //if (ProgramSettings.PortXBee != null &&
            //    comboUSB.Items.Contains(ProgramSettings.PortXBee))
            //    comboUSB.Items.
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            ProgramSettings.SaveXBeePort((string) comboUSB.SelectedItem);
            CloseOnlyThis();
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            CloseOnlyThis();
        }
    }
}
