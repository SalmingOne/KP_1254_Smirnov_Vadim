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

        public string Serialize()
        {
            string data = "";
            data += "[\n";
            data += "  {\n";
            data+="      \"Название вопроса\":" + '"' + nameAnswer + '"' + ','+ "\n";
            data += "      \"Имя\":" + '"' + name + '"' + ',' + "\n";
            data += "      \"Фамилия\":" + '"' + surname + '"' + ',' + "\n";
            data += "      \"Возраст\":" + '"' + age + '"' + ',' + "\n";
            data += "      \"Вопрос 1\":" + '"' + answer1 + '"' + ',' + "\n";
            data += "      \"Вопрос 2\":[" + "\n";
            foreach (string i in answer2)
            {
                data += "          " + '"' + i + '"' + ',' + "\n";
            }
            data += "      ],\n";
            data += "      \"Вопрос 3\":" + '"' + answer3 + '"' + ',' + "\n";
            data += "      \"Вопрос 4\":" + '"' + answer4 + '"' + ',' + "\n";
            data += "      \"Вопрос 5\":" + '"' + answer5 + '"' + ',' + "\n";
            data += "  }" + "\n";
            data += ']' + "\n";
            return data;
        }
    }
}

