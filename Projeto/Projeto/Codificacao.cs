using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto
{
    public partial class Codificacao : Form
    {
        private Aes aes;
        private byte[] key;
        private byte[] iv;

        public Codificacao()
        {
            InitializeComponent();
            aes = Aes.Create();

            // Substitua pelos valores gerados
            key = Convert.FromBase64String("MCtJn1KPGMmQUG5XXqWHLg=="); // Substitua com sua chave base64
            iv = Convert.FromBase64String("f6jRK3yDepFyB5ImmN/zPA=="); // Substitua com seu IV base64

            // Certifique-se de que a chave e o IV têm o tamanho correto
            if (key.Length != 16)
                throw new ArgumentException("A chave deve ter 16 bytes (128 bits) para AES-128.");
            if (iv.Length != 16)
                throw new ArgumentException("O IV deve ter 16 bytes.");

            this.FormClosing += new FormClosingEventHandler(Codificacao_formClosing);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void Codificacao_formClosing(object sender, FormClosingEventArgs e)
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

        private string EncryptText(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            using (ICryptoTransform encryptor = aes.CreateEncryptor(key, iv))
            {
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Criptografar o texto do richTextBox1 e exibir o resultado
            string plainText = richTextBox1.Text;
            string encryptedText = EncryptText(plainText);
            richTextBox1.Text = encryptedText; // Substitui o texto original pelo criptografado
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Voltar a outro formulário ou fechar o formulário atual
            Form1 menu = new Form1();
            menu.Show();
            this.Hide();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //  Text box do texto
        }
    }
}

