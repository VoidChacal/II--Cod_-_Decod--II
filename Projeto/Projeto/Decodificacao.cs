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
    public partial class Decodificacao : Form
    {
        private Aes aes;
        private byte[] key;
        private byte[] iv;

        public Decodificacao()
        {
            InitializeComponent();
            aes = Aes.Create();

            // Substitua pelos valores gerados
            key = Convert.FromBase64String("MCtJn1KPGMmQUG5XXqWHLg=="); // Substitua com sua chave base64
            iv = Convert.FromBase64String("f6jRK3yDepFyB5ImmN/zPA==");     // Substitua com seu IV base64

            // Certifique-se de que a chave e o IV têm o tamanho correto
            if (key.Length != 16)
                throw new ArgumentException("A chave deve ter 16 bytes (128 bits) para AES-128.");
            if (iv.Length != 16)
                throw new ArgumentException("O IV deve ter 16 bytes.");

            this.FormClosing += new FormClosingEventHandler(Decodi_formClosing);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void Decodi_formClosing(object sender, FormClosingEventArgs e)
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

        private string DecryptText(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            using (ICryptoTransform decryptor = aes.CreateDecryptor(key, iv))
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        private void decodifica_Click(object sender, EventArgs e)
        {
            // Descriptografar o texto do richTextBox2 e exibir o resultado
            string encryptedText = richTextBox2.Text;
            try
            {
                string decryptedText = DecryptText(encryptedText);
                richTextBox2.Text = decryptedText; // Substitui o texto criptografado pelo descriptografado
            }
            catch (CryptographicException ex)
            {
                MessageBox.Show($"Erro ao descriptografar: {ex.Message}");
            }
        }

        private void Voltar_Click(object sender, EventArgs e)
        {
            // Voltar a outro formulário ou fechar o formulário atual
            Form1 menu = new Form1();
            menu.Show();
            this.Hide();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            // Código opcional para lidar com mudanças no richTextBox2
        }
    }
}