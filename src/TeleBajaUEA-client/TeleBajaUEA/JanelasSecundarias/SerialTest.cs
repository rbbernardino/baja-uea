using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeleBajaUEA.ClassesAuxiliares;

namespace TeleBajaUEA
{
    public partial class SerialTest : Form
    {
        private int outputLine;
        private SerialPortExt p1;
        private string MsgString;

        private enum SerialMsg
        {
            // recebe da Serial
            START = 'S',
            BEGIN = 'B',
            END = 'E',

            // envia para Serial
            OK = 'K',
        }

        private rcvState currentState;
        private enum rcvState
        {
            BEGIN,
            Freio, NivelComb, Temperatura, RPM,
            END,
        }

        public SerialTest()
        {
            InitializeComponent();
            SetupOutput();
            currentState = rcvState.BEGIN;

            PrintLnOutput("Conectando...");

            ConnectToCar();
        }

        private void ConnectToCar()
        {
            p1 = new SerialPortExt();
            p1.PortName = "COM15";

            p1.Open();
            if (p1.IsOpen)
                PrintLnOutput("Porta " + p1.PortName + " aberta, esperando START...");

            p1.DataReceived += P1_ConnectionHandler;
        }
        private void P1_ConnectionHandler(object sender, SerialDataReceivedEventArgs e)
        {
            if (p1.ReadCharCasted() == (char) SerialMsg.START)
            {
                PrintLn_Invoke("OK! Iniciando exibição de dados...");
                p1.DataReceived -= P1_ConnectionHandler;
                p1.DataReceived += P1_DataReceived;
                p1.WriteChar((char) SerialMsg.OK);
            }
        }

        private void P1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //p1.DataReceived -= P1_DataReceived;

            switch (currentState)
            {
                // flag BEGIN usada para garantir que dados estarão sincronizados
                case rcvState.BEGIN:
                    if (p1.ReadCharCasted() == (char) SerialMsg.BEGIN)
                    {
                        PrintLn_Invoke("----------------BEGIN-------------");
                        PrintLn_Invoke("");
                        currentState = rcvState.Freio;
                    }
                    break;

                case rcvState.Freio:
                    PrintLn_Invoke("Freio: " + p1.ReadCharCasted());
                    currentState = rcvState.NivelComb;
                    break;
                case rcvState.NivelComb:
                    PrintLn_Invoke("Combustivel: " + p1.ReadInt8());
                    currentState = rcvState.Temperatura;
                    break;
                case rcvState.Temperatura:
                    PrintLn_Invoke("Temperatura: " + p1.ReadInt16());
                    currentState = rcvState.RPM;
                    break;
                case rcvState.RPM:
                    PrintLn_Invoke("RPM: " + p1.ReadInt16());
                    currentState = rcvState.BEGIN;
                    break;
            }
        }

        private void PrintLn_Invoke(string msg)
        {
            MsgString = msg;
            this.Invoke(new EventHandler(handleDataReceived));
        }

        private void handleDataReceived(object sender, EventArgs e)
        {
            PrintLnOutput(MsgString);
        }

        private void SetupOutput()
        {
            outputLine = 1;
            labelOutput.Text = outputLine + "> ";
            outputLine++;
        }

        private void PrintOutput(string output)
        {
            labelOutput.Text +=  output;
        }

        private void PrintLnOutput(string output)
        {
            outputLine++;
            labelOutput.Text += output + "\n" + outputLine + "> ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            labelOutput.Text = outputLine + "> ";
        }
    }
}
