using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public partial class FormAdd : Form
    {
        private int a;
        private int b;

        public FormAdd()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxA.Text, out a) &&
                int.TryParse(textBoxB.Text, out b))
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Некорретные данные", "Ошибка");
            }
        }

        public Data GetValue()
        {
            return new Data(a, b);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
