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
        public string name = " ";
        public string surname = " ";
        public decimal age = 0;
        


        public string nameAnswer = " ";
        public string answer1 = " ";
        public List<string> answer2 = new List<string>();
        public string answer3 = " ";
        public decimal answer4 = 0;
        public string answer5 = " ";

        public void Serialize()
        {
            StreamWriter sw = new StreamWriter("C:/Users/User/source/repos/TestApp/data.txt", append: true);
            sw.WriteLine('[');
            sw.WriteLine("  {");
            sw.WriteLine("      \"Название вопроса\":" + '"' + nameAnswer + '"'  + ',');
            sw.WriteLine("      \"Имя\":" + '"' + name + '"' +',');
            sw.WriteLine("      \"Фамилия\":" + '"' + surname + '"' +',');
            sw.WriteLine("      \"Возраст\":" + '"' + age + '"'  + ',');
            sw.WriteLine("      \"Вопрос 1\":" + '"' + answer1 + '"'  + ',');
            sw.WriteLine("      \"Вопрос 2\":[");
            foreach (string i in answer2)
            {
                sw.WriteLine("          " + '"' + i + '"' +',');
            }
            sw.WriteLine("      ],");
            sw.WriteLine("      \"Вопрос 3\":" + '"' + answer3 + '"'  + ',');
            sw.WriteLine("      \"Вопрос 4\":" + '"' + answer4 + '"'  + ',');
            sw.WriteLine("      \"Вопрос 5\":" + '"' + answer5 + '"' + ',');
            sw.WriteLine("  }");
            sw.WriteLine(']');
            
            sw.Close();
        }
    }
}
