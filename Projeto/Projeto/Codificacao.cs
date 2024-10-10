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

            key = Convert.FromBase64String("MCtJn1KPGMmQUG5XXqWHLg=="); // chave base64
            iv = Convert.FromBase64String("f6jRK3yDepFyB5ImmN/zPA=="); // IV base64

            // Verifica se a chave está correta
            if (key.Length != 16)
                throw new ArgumentException("A chave deve ter 16 bytes (128 bits) para AES-128.");
            if (iv.Length != 16)
                throw new ArgumentException("O IV deve ter 16 bytes.");

            this.FormClosing += new FormClosingEventHandler(Codificacao_formClosing);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        // ----------------- Comfirmar Fechar ---------------------------
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

        // ------------------ Função para Codficar -----------------------
        private string EncryptText(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText); // <--------- Converte para uma array
            using (ICryptoTransform encryptor = aes.CreateEncryptor(key, iv)) // <---------- realizar a criptografia com as chaves: key e iv
            {
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length); // <------------ Criptografa os bytes
                return Convert.ToBase64String(encryptedBytes); // <------------ Retorna o resultado
            }
        }

        // ------------------ Botão de Codificar -------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            // Criptografar o texto do richTextBox1 e exibir o resultado
            string plainText = richTextBox1.Text;
            string encryptedText = EncryptText(plainText);
            richTextBox1.Text = encryptedText; // Substitui o texto original pelo criptografado
        }

        // ------------------ Botão de Voltar ------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            // Voltar a outro formulário e fechar o formulário atual
            Form1 menu = new Form1();
            menu.Show();
            this.Hide();
        }

        // ---------------------------------------------
        //           Não utilizado - AKA Lixo
        // ---------------------------------------------
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //  Text box do texto
        }
    }
}

