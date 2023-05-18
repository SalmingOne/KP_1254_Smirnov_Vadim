using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserData
{
    public class Account
    {
        string name;
        string surname;
        DateTime date;
        string login;
        string password;
        Permissions permissions;
        public Account(string name,string surname,DateTime date,string login,string password)
        {
            this.name = name;
            this.surname = surname;
            this.date = date;
            this.login = login;
            this.password = password;
        }
        [Flags]
        public enum Permissions
        {
            None = 0,
            ViewUsers = 1,
            ViewAdmins = 2,
            EditSelf = 4,
            EditOther = 8,
            ViewPasswords = 16,


            Guest = ViewUsers,
            CommonUser = ViewAdmins | ViewUsers,
            ExtendedUser = CommonUser | EditSelf,
            Moderator = ExtendedUser | ViewPasswords,
            Admin = ExtendedUser | EditOther | ViewPasswords
        }

        public string Name{ get { return name; } set { name = value; } }
        public string Surname { get { return surname; } set { surname = value; } }
        public DateTime Date { get { return date; } set { date = value; } }
        public string Login { get { return login; } set { login = value; } }
        public string Password { get { return password; } set {password = value;} }
        public Permissions Perm { get { return permissions; } set { permissions = value; } }

        public void Serialize()
        {
            StreamWriter sw = new StreamWriter("C:/Users/User/source/repos/UserData/data.txt", append: true);
            sw.WriteLine('[');
            sw.WriteLine("  {");
            sw.WriteLine("      \"Имя\":" + '"' + name + '"' + ',');
            sw.WriteLine("      \"Фамилия\":" + '"' + surname + '"' + ',');
            sw.WriteLine("      \"Дата рождения\":" + '"' + date + '"' + ',');
            sw.WriteLine("      \"Логин\":" + '"' + login + '"' + ',');
            sw.WriteLine("      \"Статус\":" + '"' + permissions + '"' + ',');
            sw.WriteLine("  }");
            sw.WriteLine(']');

            sw.Close();
        }
        public void SerializeData()
        {
            StreamWriter sw = new StreamWriter("C:/Users/User/source/repos/UserData/data_acc.txt", append: true);
            sw.WriteLine('[');
            sw.WriteLine("  {");
            sw.WriteLine("      \"Логин\":" + '"' + login + '"' + ',');
            sw.WriteLine("      \"Пароль\":" + '"' + password.Trim() + '"' + ',');
            sw.WriteLine("  }");
            sw.WriteLine(']');

            sw.Close();
        }
    }
}
