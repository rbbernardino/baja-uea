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
    public partial class DecimalTextBox : TextBox
    {
        public DecimalTextBox() : base()
        {
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // ignora qualquer coisa que não seja ponto, vírgula ou número
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back
                & e.KeyChar != ',' & e.KeyChar != '.')
            {
                e.Handled = true;
            }
            else
            {
                if (Text == "0")
                    Text = "";
            }

            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (this.Text.Length > 0)
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

                // impede que comece com vírgula
                if (Text[0] == ',' || Text[0] == '.')
                {
                    Text = Text.Substring(1);
                    Select(0, 0);
                }

                // caso digite ponto, troca para vírgula
                if (Text.Count(x => x == '.') > 0)
                {
                    this.Text = Text.Replace('.', ',');
                    this.Select(this.Text.Length, 0);
                }

                // se digitar mais de uma vírgula, mantém a primeira apenas
                if (Text.Count(x => x == ',') > 1)
                {
                    int i = this.Text.IndexOf(",");
                    Text = Text.Substring(0, i + 1) + Text.Substring(i + 1).Replace(",", string.Empty);
                    this.Select(this.Text.Length, 0);
                }
            }
            base.OnTextChanged(e);
        }
    }
}
