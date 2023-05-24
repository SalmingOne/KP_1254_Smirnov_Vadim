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

        public string Serialize()
        {
            string data = "";
            data += '[' + "\n";
            data += "  {" + "\n";
            data += "      \"Имя\":" + '"' + name + '"' + ',' + "\n";
            data += "      \"Фамилия\":" + '"' + surname + '"' + ',' + "\n";
            data += "      \"Дата рождения\":" + '"' + date + '"' + ',' + "\n";
            data += "      \"Логин\":" + '"' + login + '"' + ',' + "\n";
            data += "      \"Статус\":" + '"' + permissions + '"' + ',' + "\n";
            data += "  }" + "\n";
            data += "]" + "\n";

            return data;
        }
        public string SerializeData()
        {
            string data = "";
            data += '[' + "\n";
            data += "  {" + "\n";
            data += "      \"Логин\":" + '"' + login + '"' + ',' + "\n";
            data += "      \"Пароль\":" + '"' + password.Trim() + '"' + ',' + "\n";
            data += "  }" + "\n";
            data += "]" + "\n";

            return data;
        }
    }
}
