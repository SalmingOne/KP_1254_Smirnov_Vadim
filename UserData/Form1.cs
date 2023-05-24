using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UserData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool flagViewPass = true;
        Dictionary<char, string> dict = new Dictionary<char, string>()
        {
            {'а', "a" },
            {'б', "b" },
            {'в', "v" },
            {'г', "g" },
            {'д', "d" },
            {'е', "e" },
            {'ё', "yo" },
            {'ж', "zh" },
            {'з', "z" },
            {'и', "i" },
            {'й', "j" },
            {'к', "k" },
            {'л', "l" },
            {'м', "m" },
            {'н', "n" },
            {'о', "o" },
            {'п', "p" },
            {'р', "r" },
            {'с', "s" },
            {'т', "t" },
            {'у', "u" },
            {'ф', "f" },
            {'х', "h" },
            {'ц', "ts" },
            {'ч', "ch" },
            {'ш', "sh" },
            {'щ', "sch" },
            {'ы', "y" },
            {'ь', "'" },
            {'э', "e" },
            {'ю', "iu" },
            {'я', "ya" },

        };
        List<Account> users = new List<Account>();
        public Account currentAccount = null;
        Dictionary<string, string> access = new Dictionary<string, string>();
        Dictionary<string, Account.Permissions> keyValuePairs = new Dictionary<string, Account.Permissions>()
        {
            {"Admin", Account.Permissions.Admin},
            {"CommonUser", Account.Permissions.CommonUser},
            {"ExtendedUser", Account.Permissions.ExtendedUser},
            {"Moderator", Account.Permissions.Moderator},
        };
        void ClearFiles(string filename, string filename_acc)
        {
            StreamWriter sw = new StreamWriter(filename);
            StreamWriter sw1 = new StreamWriter(filename_acc);
            sw.Write(string.Empty);
            sw1.Write(string.Empty);
            sw.Close();
            sw1.Close();
        }
        void Deserialize(string filename, string filename_acc)
        {
            string name = "";
            string surname = "";
            DateTime date = DateTime.MinValue;
            string login = "";
            string password = "";
            if (File.Exists(filename) && File.Exists(filename_acc))
            {
                string JsonString = File.ReadAllText(filename);
                string JsonString_acc = File.ReadAllText(filename_acc);
                int startIndex = 0;
                for (int i = 0; i < JsonString_acc.Length; i++)
                {
                    if (JsonString_acc[i] == '{')
                    {
                        int IndexLogin_Start = JsonString_acc.IndexOf(":", startIndex);
                        startIndex = IndexLogin_Start + 1;
                        int IndexLogin_End = JsonString_acc.IndexOf(",", startIndex);
                        startIndex = IndexLogin_End + 1;
                        int IndexPassword_Start = JsonString_acc.IndexOf(":", startIndex);
                        startIndex = IndexPassword_Start + 1;
                        int IndexPassword_End = JsonString_acc.IndexOf(",", startIndex);
                        startIndex = IndexPassword_End + 1;


                        password = JsonString_acc.Substring(IndexPassword_Start + 2, IndexPassword_End - IndexPassword_Start - 3);
                    }
                }
                startIndex = 0;
                for (int i = 0; i < JsonString.Length; i++)
                {
                    if (JsonString[i] == '{')
                    {
                        int IndexName_Start = JsonString.IndexOf(':', startIndex);
                        startIndex = IndexName_Start;
                        int IndexName_End = JsonString.IndexOf(',', startIndex);
                        startIndex = IndexName_End;

                        int IndexSurname_Start = JsonString.IndexOf(":", startIndex);
                        startIndex = IndexSurname_Start + 1;
                        int IndexSurname_End = JsonString.IndexOf(",", startIndex);
                        startIndex = IndexSurname_End + 1;

                        int IndexDate_Start = JsonString.IndexOf(":", startIndex);
                        startIndex = IndexDate_Start + 1;
                        int IndexDate_End = JsonString.IndexOf(",", startIndex);
                        startIndex = IndexDate_End + 1;

                        int IndexLogin_Start = JsonString.IndexOf(":", startIndex);
                        startIndex = IndexLogin_Start + 1;
                        int IndexLogin_End = JsonString.IndexOf(",", startIndex);
                        startIndex = IndexLogin_End + 1;

                        int IndexStatus_Start = JsonString.IndexOf(":", startIndex);
                        startIndex = IndexStatus_Start + 1;
                        int IndexStatus_End = JsonString.IndexOf(",", startIndex);
                        startIndex = IndexStatus_End + 1;

                        name = JsonString.Substring(IndexName_Start + 2, IndexName_End - IndexName_Start - 3);
                        surname = JsonString.Substring(IndexSurname_Start + 2, IndexSurname_End - IndexSurname_Start - 3);
                        date = Convert.ToDateTime(JsonString.Substring(IndexDate_Start + 2, IndexDate_End - IndexDate_Start - 3));
                        Console.WriteLine(date);
                        login = JsonString.Substring(IndexLogin_Start + 2, IndexLogin_End - IndexLogin_Start - 3);
                        string statuss = JsonString.Substring(IndexStatus_Start + 2, IndexStatus_End - IndexStatus_Start - 3);
                        Account account1 = new Account(name, surname, date, login, password);
                        Console.WriteLine(statuss);
                        account1.Perm = keyValuePairs[statuss];
                        if (currentAccount.Login != account1.Login)
                        {
                            users.Add(account1);
                        }
                    }
                }
            }



        }
        private void SaveData(string filename, string filename_acc)
        {
            try {
                if (File.Exists(filename) && File.Exists(filename_acc))
                {
                    StreamWriter sw = new StreamWriter(filename, append: true);
                    StreamWriter sw1 = new StreamWriter(filename_acc, append: true);
                    Account account = new Account(textBox1.Text, textBox2.Text, dateTimePicker1.Value, textBox3.Text, textBox4.Text);
                    foreach (RadioButton radioButton in groupBox2.Controls)
                    {
                        if (radioButton.Checked)
                        {
                            account.Perm = keyValuePairs[radioButton.Text];
                        }
                    }

                    users.Add(account);
                    access.Add(account.Login, account.Password);
                    sw.Write(account.Serialize());
                    sw1.Write(account.SerializeData());
                    listBox1.Items.Add("User(" + account.Name + ' ' + account.Surname + ')');

                    sw.Close();
                    sw1.Close();
                }
            }
            catch { MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
        private string GetLogin()
        {

            string login;
            string name = textBox1.Text.ToLower();
            string surname = textBox2.Text.ToLower();
            int year = dateTimePicker1.Value.Year;
            if (name.Length == 0 || surname.Length == 0)
            {
                return "Заполните все обязательные поля";
            }
            login = name + surname;
            string new_login = "";

            foreach (char c in login)
            {
                if (dict.ContainsKey(c))
                {
                    new_login += dict[c];
                }
                else { return "Некорректные имя и фамилия"; }
            }
            return new_login + year;
        }
        private void FillForm(Account account)
        {
            textBox4.Text = account.Password;
            textBox3.Text = account.Login;
            textBox1.Text = account.Name;
            textBox2.Text = account.Surname;
            dateTimePicker1.Value = account.Date;
            foreach (RadioButton radioButton in groupBox2.Controls)
            {
                if (radioButton.Text == Convert.ToString(account.Perm))
                {
                    radioButton.Checked = true;
                }
            }
        }
        private void ClearForm()
        {
            textBox4.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            textBox3.Text = "";
        }
        private string GetPassword(string args)
        {
            if (args == "Error")
            {
                return "Введите длину пароля";
            }
            Process process = new Process();
            process.StartInfo.FileName = "GeneratorPassword.exe";
            process.StartInfo.Arguments = args;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;

            bool start = process.Start();

            if (start)
            {
                string output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();
                return output.Replace(" ", "");
            }
            else
            {
                return "Ошибка";
            }
        }
        private string getArgs()
        {
            string args = string.Empty;
            if (numericUpDown1.Value != 0)
            {
                args += "" + numericUpDown1.Value + " ";

                if (checkBox2.Checked)
                {
                    args += "-s" + ' ';
                }
                if (checkBox1.Checked)
                {
                    args += "-u" + ' ';
                }
                if (numericUpDown2.Value > 0)
                {
                    args += "--digits=" + numericUpDown2.Value + ' ';
                }
                if (numericUpDown3.Value > 0)
                {
                    args += "--letters=" + numericUpDown3.Value + ' ';
                }
            }
            else { args = "Error"; }

            return args;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                numericUpDown2.Visible = true;
            }
            else { numericUpDown2.Visible = false; }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                numericUpDown3.Visible = true;
            }
            else { numericUpDown3.Visible = false; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = GetPassword(getArgs());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveData("data.txt", "data_acc.txt");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Visible = true;
            if (currentAccount.Perm == Account.Permissions.Admin) 
            {
                button2.Visible = false;
            }
            
            if (listBox1.SelectedIndex >= 1)
            {
                Account account = users[listBox1.SelectedIndex];
                if (account != null)
                {
                    FillForm(account);
                }
                if (currentAccount.Perm != Account.Permissions.Admin)
                {
                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                    textBox3.ReadOnly = true;
                }
                if (flagViewPass)
                {
                    groupBox1.Visible = true;
                }
                else { groupBox1.Visible = false; }
            }
            else
            {
                if (currentAccount.Perm != Account.Permissions.CommonUser || currentAccount.Perm != Account.Permissions.Guest)
                {
                    groupBox4.Visible = true;
                }
                FillForm(currentAccount);
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                groupBox1.Visible = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

                textBox3.Text = GetLogin();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

                textBox3.Text = GetLogin();

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

                textBox3.Text = GetLogin();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Deserialize("data.txt","data_acc.txt");
            listBox1.Items.Add("Me");
            listBox1.SetSelected(0, true);
            // Admin
            if (Convert.ToString((int)currentAccount.Perm,2).Count(f => (f == '1')) == 5) 
            {
                groupBox3.Visible = true;
                groupBox2.Visible = true;
                listBox1.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                FillForm(currentAccount);
                foreach (Account acc in users)
                {
                    listBox1.Items.Add("User(" + acc.Name + ' ' + acc.Surname + ')');
                }
            }
            // Moderator
            else if (Convert.ToString((int)currentAccount.Perm, 2).Count(f => (f == '1')) == 4)
            {
                groupBox3.Visible = true;
                groupBox1.Visible = true;
                listBox1.Visible = true;
                button2.Visible = false;
                FillForm(currentAccount);
                foreach (Account acc in users)
                {
                    listBox1.Items.Add("User(" + acc.Name + ' ' + acc.Surname + ')');
                }
            }
            //Extended
            else if (Convert.ToString((int)currentAccount.Perm, 2).Count(f => (f == '1')) == 3)
            {
                groupBox3.Visible = true; listBox1.Visible = true;
                groupBox3.Visible = true;
                groupBox1.Visible = true;
                groupBox4.Visible = false;
                listBox1.Visible = true;
                button2.Visible = false;
                button3 .Visible = true;
                FillForm(currentAccount);
                foreach (Account acc in users)
                {
                    listBox1.Items.Add("User(" + acc.Name + ' ' + acc.Surname + ')');
                }
            }
            // Common
            else if (Convert.ToString((int)currentAccount.Perm, 2).Count(f => (f == '1')) == 2)
            {
                groupBox3.Visible = true;
                groupBox4.Visible = false;
                groupBox1.Visible = true;
                listBox1.Visible = true;
                button2.Visible = false;
                flagViewPass = false;
                
                FillForm(currentAccount);
                foreach (Account acc in users)
                {
                    listBox1.Items.Add("User(" + acc.Name + ' ' + acc.Surname + ')');
                }
            }
            //Guest
            else if (Convert.ToString((int)currentAccount.Perm, 2).Count(f => (f == '1')) == 1) 
            {
                groupBox3.Visible = true;
                groupBox1.Visible = true;
                listBox1.Visible = true;
                button2.Visible = false;
                flagViewPass = false;
                FillForm(currentAccount);
                foreach (Account acc in users)
                {
                    if (acc.Perm == Account.Permissions.CommonUser || acc.Perm == Account.Permissions.ExtendedUser)
                    {
                        listBox1.Items.Add("User(" + acc.Name + ' ' + acc.Surname + ')');
                    }
                }
            }
            users.Insert(0, currentAccount);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            users[listBox1.SelectedIndex].Name = textBox1.Text;
            users[listBox1.SelectedIndex].Surname = textBox2.Text;
            users[listBox1.SelectedIndex].Date = dateTimePicker1.Value;
            users[listBox1.SelectedIndex].Login = textBox3.Text;
            users[listBox1.SelectedIndex].Password = textBox4.Text;
            if (currentAccount.Perm != Account.Permissions.Guest)
            {
                foreach (RadioButton radioButton in groupBox2.Controls)
                {
                    if (radioButton.Checked)
                    {
                        users[listBox1.SelectedIndex].Perm = keyValuePairs[radioButton.Text];
                    }
                }
            }
            else { users[listBox1.SelectedIndex].Perm = Account.Permissions.CommonUser; }
            ClearFiles("data.txt", "data_acc.txt");
            StreamWriter sw = new StreamWriter("data.txt",append:true);
            StreamWriter sw1 = new StreamWriter("data_acc.txt",append:true);
            foreach (Account account in users)
            {
                sw.Write(account.Serialize());
                sw1.Write(account.SerializeData());
            }
            sw.Close();
            sw1.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                listBox1.SetSelected(listBox1.SelectedIndex, false);
                ClearForm();
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                groupBox4.Visible = true;
                if (currentAccount.Perm == Account.Permissions.Admin)
                {
                    button2.Visible = true;
                }
            }
        }
    }
}
