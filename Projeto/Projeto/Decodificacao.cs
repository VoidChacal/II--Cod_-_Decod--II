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

            key = Convert.FromBase64String("MCtJn1KPGMmQUG5XXqWHLg=="); // chave base64
            iv = Convert.FromBase64String("f6jRK3yDepFyB5ImmN/zPA=="); // IV base64

            // Verifica se a chave está correta
            if (key.Length != 16)
                throw new ArgumentException("A chave deve ter 16 bytes (128 bits) para AES-128.");
            if (iv.Length != 16)
                throw new ArgumentException("O IV deve ter 16 bytes.");

            this.FormClosing += new FormClosingEventHandler(Decodi_formClosing);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        // ----------------- Comfirmar Fechar ---------------------------
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

        // ------------------ Função para Decodficar -----------------------
        private string DecryptText(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText); // <--------- Converte para uma array
            using (ICryptoTransform decryptor = aes.CreateDecryptor(key, iv)) // <---------- realizar a descriptografia com as chaves: key e iv
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length); // <------------ descriptografa os bytes
                return Encoding.UTF8.GetString(decryptedBytes); // <------------ Retorna o resultado como um texto
            }
        }

        // ------------------ Botão de Decodificar -------------------------------
        private void decodifica_Click(object sender, EventArgs e)
        {
            // Descriptografar o texto do richTextBox2_TextChanged e exibir o resultado
            string encryptedText = richTextBox2.Text;
            try
            {
                string decryptedText = DecryptText(encryptedText); //<---------- Chama a função
                richTextBox2.Text = decryptedText; // <--------- Substitui o texto criptografado pelo descriptografado
            }
            catch (CryptographicException ex) //<------------ Caso aconteça algum erro
            {
                MessageBox.Show($"Erro ao descriptografar: {ex.Message}");
            }
        }

        // ------------------ Botão de Voltar ------------------------
        private void Voltar_Click(object sender, EventArgs e)
        {
            
            Form1 menu = new Form1();
            menu.Show(); // <---------- Mostra o form menu
            this.Hide(); // <---------- Fecha o form Decodificação
        }

        // ---------------------------------------------
        //           Não utilizado - AKA Lixo
        // ---------------------------------------------
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            // Textbox2 da Decodificação 
        }
    }
}