﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeleBajaUEA.RaceDataStructs;

namespace TeleBajaUEA
{
    public partial class AnalisarCorrida : FormPrincipal
    {
        private RaceData raceData;

        public AnalisarCorrida(RaceData pRaceData)
        {
            InitializeComponent();

            ConfigureCharts();

            raceData = pRaceData;

            double brakePosition;
            foreach (FileSensorsData pointData in raceData.DataList)
            {
                chartsNew.Series["Speed"].Points.AddXY(pointData.xValue, pointData.speed);

                chartsNew.Series["RPM"].Points.AddXY(pointData.xValue, pointData.rpm);

                if (pointData.breakState)
                    brakePosition = (SPEED_MAXIMUM / 2) + SPEED_Y_INTERVAL;
                else
                    brakePosition = (SPEED_MAXIMUM / 2) - SPEED_Y_INTERVAL;

                chartsNew.Series["Brake"].Points.AddXY(pointData.xValue, brakePosition);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MenuPrincipal aux = new MenuPrincipal();
            aux.Show();
        }

        private void btVoltar_Click(object sender, EventArgs e)
        {
            CloseOnlyThis();
            Program.ShowMenuPrincipal();
        }
    }
}
