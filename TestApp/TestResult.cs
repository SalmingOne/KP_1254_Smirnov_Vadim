using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    internal class TestResult
    {
        public string name = "";
        public string surname = "";
        public decimal age = 0;
        


        public string nameAnswer = "";
        public int answer1 = 0;
        public List<int> answer2 = new List<int>();
        public int answer3 = 0;
        public decimal answer4 = 0;
        public string answer5 = "";

        public void Serialize()
        {
            StreamWriter sw = new StreamWriter("C:/Users/User/source/repos/WindowsFormsApp2/data.txt", append: true);
            sw.WriteLine("Название вопроса -" + nameAnswer + ',');
            sw.WriteLine("Имя -" + name+',');
            sw.WriteLine("Фамилия -" + surname+',');
            sw.WriteLine("Возраст -" + age + ',');
            sw.WriteLine("Вопрос 1 -" + answer1 + ',');
            string s = "Вопрос 2 -";
            foreach (int i in answer2)
            {
                s += i;
            }
            sw.WriteLine(s+',');
            sw.WriteLine("Вопрос 3 -" + answer3 + ',');
            sw.WriteLine("Вопрос 4 -" + answer4 + ',');
            sw.WriteLine("Вопрос 5 -" + answer5 + ',');
            sw.WriteLine('.');
            sw.Close();
        }
    }
}
