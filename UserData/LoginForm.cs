using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserData
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        Form1 form = new Form1();
        string name = "";
        string surname = "";
        string date = "";
        string login = "";
        string password = "";
        string status = "";
        Dictionary<string, Account.Permissions> keyValuePairs = new Dictionary<string, Account.Permissions>()
        {
            {"Admin", Account.Permissions.Admin},
            {"CommonUser", Account.Permissions.CommonUser},
            {"ExtendedUser", Account.Permissions.ExtendedUser},
            {"Moderator", Account.Permissions.Moderator},
        };
        Dictionary<string, List<string>> users = new Dictionary<string, List<string>>();
        void Deserialize()
        {

            string JsonString = File.ReadAllText("C:/Users/User/source/repos/UserData/data.txt");
            string JsonString_acc = File.ReadAllText("C:/Users/User/source/repos/UserData/data_acc.txt");
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


                    login = JsonString_acc.Substring(IndexLogin_Start + 2, IndexLogin_End - IndexLogin_Start - 3);
                    password = JsonString_acc.Substring(IndexPassword_Start + 2, IndexPassword_End - IndexPassword_Start - 3);
                    users.Add(login, new List<string>());
                    users[login].Add(password);
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
                    date = JsonString.Substring(IndexDate_Start + 2, IndexDate_End - IndexDate_Start - 3);
                    login = JsonString.Substring(IndexLogin_Start + 2, IndexLogin_End - IndexLogin_Start - 3);
                    status = JsonString.Substring(IndexStatus_Start + 2, IndexStatus_End - IndexStatus_Start - 3);
                    users[login].Add(name);
                    users[login].Add(surname);
                    users[login].Add(date);
                    users[login].Add(status);
                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            form.currentAccount = new Account("", "", DateTime.Now, "", "");
            form.currentAccount.Perm = Account.Permissions.Guest;
            form.ShowDialog();
            this.Hide();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (users.ContainsKey(textBox1.Text)) 
            {
                if (users[textBox1.Text][0]==textBox2.Text)
                {
                    List<string> user = users[textBox1.Text];

                    form.currentAccount = new Account(user[1], user[2], Convert.ToDateTime(user[3]), textBox1.Text, user[0]);
                    form.currentAccount.Perm = keyValuePairs[user[4]];
                    form.ShowDialog();
                    this.Hide();
                    this.Close();
                }
            }
            else { MessageBox.Show("Пользователь не найден", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Deserialize();
        }
    }
}
