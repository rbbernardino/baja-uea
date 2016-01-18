using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TeleBajaUEA
{
    //TODO Adaptar esse código para a abordagem de zoom e scroll usada?
    //     Realmente é necessário? Como fica melhor?
    // pensei em usar uma implementação que achei para permitir usar a
    // rodinha do mouse para fazer rolar ou dar zoom, mas como exige
    // adaptar algumas coisas e não foi combinado antes deixei para fazer
    // no futuro, por algum outro membro.
    // Ass. Rodrigo Bernardino

    // baseado no código em https://bitbucket.org/grumly57/mschartfriend
    // requer eventos: Mouse(Enter|Leave|Move|Down)
    partial class AnalisarCorrida
    {
        private double ZoomFactor = .1d;
        private bool ZoomMouseWheelReversed = false;
        private MouseButtons ScrollButton = MouseButtons.Left;

        Point fromPoint;

        Axis getAxisUnderPoint(Point loc)
        {
            var h = chartsNew.HitTest(loc.X, loc.Y, ChartElementType.Axis);
            if (h == null)
                return null;

            return h.Axis;
        }

        void TryMouseScroll(MouseEventArgs e)
        {
            var axis = getAxisUnderPoint(e.Location);
            if (axis == null)
            {
                chartsNew.Cursor = Cursors.Default;
                return;
            }

            var loc = e.Location;
            if (loc == fromPoint)
                return;

            var isX = axis.AxisName == AxisName.X || axis.AxisName == AxisName.X2;

            chartsNew.Cursor = isX ? Cursors.NoMoveHoriz : Cursors.NoMoveVert;

            if (e.Button == ScrollButton)
            {
                var d = axis.PixelPositionToValue(isX ? loc.X : loc.Y)
                      - axis.PixelPositionToValue(isX ? fromPoint.X : fromPoint.Y);

                chartsNew.ChartAreas.SuspendUpdates();
                axis.Minimum -= d;
                axis.Maximum -= d;
                chartsNew.ChartAreas.ResumeUpdates();

                fromPoint = loc;
            }
        }

        private void MouseWheelScroll(MouseEventArgs e)
        {
            var axis = getAxisUnderPoint(e.Location);
            if (axis == null)
                return;

            var d = axis.Maximum - axis.Minimum;
            d = e.Delta > 0 ^ ZoomMouseWheelReversed ? d * ZoomFactor : -d * ZoomFactor;

            chartsNew.ChartAreas.SuspendUpdates();
            axis.Minimum += d;
            axis.Maximum -= d;
            chartsNew.ChartAreas.ResumeUpdates();
        }
    }
}
