using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TeleBajaUEA.ClassesAuxiliares.Extensões
{
    public partial class IntegerTextBox : TextBox
    {
        public IntegerTextBox() : base()
        {
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // ignora qualquer coisa que não seja número
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back)
            {
                e.Handled = true;
            }

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            // remove zeros a esquerda
            if (this.Text.Length > 1 && Text[0] == '0')
            {
                // caso seja 00, o Trim não funciona
                string newText = Text + "#";
                newText = newText.TrimStart('0');

                // se sobrar apenas o delimitador, só tinha 0s
                if (newText == "#")
                    Text = "0";
                else
                    Text = newText.Replace("#", "");

                Select(Text.Length, 0);
            }
            base.OnTextChanged(e);
        }
    }
}
