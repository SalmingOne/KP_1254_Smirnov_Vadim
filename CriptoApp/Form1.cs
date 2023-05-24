using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CriptoApp
{
    public partial class Form1 : Form
    {
        List<Encryptor> encryptorsList = new List<Encryptor>();
        public Form1()
        {
            InitializeComponent();
        }
        void ClearForm()
        {
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
        }
        void fillEncryptors()
        {
            foreach (string s in listBox2.Items)
            {
                switch (s)
                {
                    case "Increment(+1)":
                        encryptorsList.Add(new Encryptor(IncrementSymbolCode, "Increment", "I"));
                        break;
                    case "Decrement(-1)":
                        encryptorsList.Add(new Encryptor(DecrementSymbolCode, "Decrement", "D"));
                        break;
                    case "Zero(0)":
                        encryptorsList.Add(new Encryptor(ZeroSymbolCode, "Zero", "Z"));
                        break;
                }
            }
        }
        char IncrementSymbolCode(char symbolCode)
        {
            return (char)((int)symbolCode + 1);
        }
        char DecrementSymbolCode(char symbolCode)
        {
            return (char)((int)symbolCode - 1);
        }
        char ZeroSymbolCode(char symbolCode)
        {
            return symbolCode;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            listBox2.Items.Add(listBox1.Items[index]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = listBox2.SelectedIndex;
            if (index == -1) { return; }
            listBox2.Items.RemoveAt(index);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearForm();
            encryptorsList.Clear();
            fillEncryptors();
            for (int i=0;i<textBox1.Text.Length;i++)
            {
                textBox3.Text += encryptorsList[i % encryptorsList.Count].encrypt(textBox1.Text[i]);
            }
            foreach (Encryptor encryptor in encryptorsList)
            {
                textBox4.Text += encryptor.Key;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Decryptor decryptor = new Decryptor();
            textBox2.Text = decryptor.Decrypt(textBox4.Text,textBox3.Text); 
        }
    }
}
