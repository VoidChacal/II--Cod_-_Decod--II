using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem certeza que deseja fechar o aplicativo?", "Fechar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Exit();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Codificacao novo = new Codificacao();
            novo.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Decodificacao novo = new Decodificacao();
            novo.Show();
            this.Hide();
        }
    }
}
