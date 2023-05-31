using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int x = 1;
        bool flagLoad = true;
        bool flagSave = false;
        List<TestResult> results = new List<TestResult>();
        TestResult createResult()
        {
            TestResult result = new TestResult();
            result.nameAnswer = "Ответ" + ' ' + x;
            foreach (RadioButton rb in groupBox2.Controls)
            {
                if (rb.Checked)
                {
                    result.answer1 = rb.Text;
                }
            }
            foreach (CheckBox сb in groupBox3.Controls)
            {
                if (сb.Checked == true)
                {
                    result.answer2.Add(сb.Text);
                }
            }
            result.answer3 = comboBox1.Text;
            result.answer4 = numericUpDown2.Value;
            result.answer5 = textBox3.Text;
            listBox1.Items.Add(result.nameAnswer);
            x += 1;


            result.name = textBox1.Text;
            result.surname = textBox2.Text;
            result.age = numericUpDown1.Value;


            return result;
        }
        void fillForm(TestResult result)
        {
            textBox1.Text = result.name;
            textBox2.Text = result.surname;
            numericUpDown1.Value = result.age;

            textBox3.Text = result.answer5;
            comboBox1.Text = result.answer3;
            numericUpDown2.Value = result.answer4;
            foreach (RadioButton rb in groupBox2.Controls)
            {
                if (rb.Text == result.answer1)
                {
                    rb.Checked = true;
                }
            }
            ;
            foreach (CheckBox cb in groupBox3.Controls)
            {
                if (cb.Checked == true)
                {
                    cb.Checked = false;
                }
                foreach (string text in result.answer2)
                {
                    if (cb.Text == text)
                    {
                        cb.Checked = true;
                    }

                }
            }
        }
        void clearForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            numericUpDown1.Value = 0;

            textBox3.Text = "";
            comboBox1.Text = "";
            numericUpDown2.Value = 0;
            foreach (RadioButton rb in groupBox2.Controls)
            {

                rb.Checked = false;

            }
            ;
            foreach (CheckBox cb in groupBox3.Controls)
            {
                cb.Checked = false;
            }
        }
        void Deserialize(string filename)
        {
            if (File.Exists(filename))
            {
                string JsonString = File.ReadAllText(filename);
                int startIndex = 0;
                for (int i = 0; i < JsonString.Length; i++)
                {
                    if (JsonString[i] == '{')
                    {
                        TestResult result = new TestResult();

                        int IndexNameAnswer_Start = JsonString.IndexOf(':', startIndex);
                        startIndex = IndexNameAnswer_Start;
                        int IndexNameAnswer_End = JsonString.IndexOf(',', startIndex);
                        startIndex = IndexNameAnswer_End;

                        int IndexName_Start = JsonString.IndexOf(":", startIndex);
                        startIndex = IndexName_Start + 1;
                        int IndexName_End = JsonString.IndexOf(",", startIndex);
                        startIndex = IndexName_End + 1;

                        int IndexSurname_Start = JsonString.IndexOf(":", startIndex);
                        startIndex = IndexSurname_Start + 1;
                        int IndexSurname_End = JsonString.IndexOf(",", startIndex);
                        startIndex = IndexSurname_End + 1;

                        int IndexAge_Start = JsonString.IndexOf(":", startIndex);
                        startIndex = IndexAge_Start + 1;
                        int IndexAge_End = JsonString.IndexOf(",", startIndex);
                        startIndex = IndexAge_End + 1;

                        int IndexAnswer1_Start = JsonString.IndexOf(":", startIndex);
                        startIndex = IndexAnswer1_Start + 1;
                        int IndexAnswer1_End = JsonString.IndexOf(",", startIndex);
                        startIndex = IndexAnswer1_End + 1;

                        int IndexAnswer2_Start = JsonString.IndexOf(":", startIndex);
                        startIndex = IndexAnswer2_Start + 1;

                        int IndexAnswer3_Start = JsonString.IndexOf(":", startIndex);
                        startIndex = IndexAnswer3_Start + 1;
                        int IndexAnswer3_End = JsonString.IndexOf(",", startIndex);
                        startIndex = IndexAnswer3_End + 1;

                        int IndexAnswer4_Start = JsonString.IndexOf(":", startIndex);
                        startIndex = IndexAnswer4_Start + 1;
                        int IndexAnswer4_End = JsonString.IndexOf(",", startIndex);
                        startIndex = IndexAnswer4_End + 1;

                        int IndexAnswer5_Start = JsonString.IndexOf(":", startIndex);
                        startIndex = IndexAnswer5_Start + 1;
                        int IndexAnswer5_End = JsonString.IndexOf(",", startIndex);
                        startIndex = IndexAnswer5_End + 1;


                        int index_array_start = JsonString.IndexOf("[", IndexAnswer2_Start);
                        int index_array_end = JsonString.IndexOf("]", IndexAnswer2_Start);
                        string array = JsonString.Substring(index_array_start + 1, index_array_end - index_array_start - 1).Replace('"', ' ');
                        string[] elements = array.Split(new char[] { ',' });
                        foreach (string element in elements)
                        {
                            result.answer2.Add(element.Trim());
                        }

                        result.nameAnswer = JsonString.Substring(IndexNameAnswer_Start + 2, IndexNameAnswer_End - IndexNameAnswer_Start - 3);
                        result.name = JsonString.Substring(IndexName_Start + 2, IndexName_End - IndexName_Start - 3);
                        result.surname = JsonString.Substring(IndexSurname_Start + 2, IndexSurname_End - IndexSurname_Start - 3);
                        result.age = Convert.ToInt32(JsonString.Substring(IndexAge_Start + 2, IndexAge_End - IndexAge_Start - 3));
                        result.answer1 = JsonString.Substring(IndexAnswer1_Start + 2, IndexAnswer1_End - IndexAnswer1_Start - 3);
                        result.answer3 = JsonString.Substring(IndexAnswer3_Start + 2, IndexAnswer3_End - IndexAnswer3_Start - 3);
                        result.answer4 += Convert.ToInt32(JsonString.Substring(IndexAnswer4_Start + 2, IndexAnswer4_End - IndexAnswer4_Start - 3));
                        result.answer5 = JsonString.Substring(IndexAnswer5_Start + 2, IndexAnswer5_End - IndexAnswer5_Start - 3);
                        results.Add(result);
                        listBox1.Items.Add(result.nameAnswer);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TestResult result = createResult();
            results.Add(result);
            flagSave = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            try
            {
                TestResult result = results[index - 1];
                fillForm(result);
            }
            catch
            {
                MessageBox.Show("Возникла ошибка", "Ошибка",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Очистить форму?", "Предупреждение",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Exclamation);

            if (dialogResult == DialogResult.Yes)
                clearForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (flagSave)
            {
                foreach (TestResult testResult in results)
                {
                    StreamWriter sw = new StreamWriter("TestResults.txt", append: true);
                    sw.Write(testResult.Serialize());
                    sw.Close();
                }
            }
            flagLoad = false;

        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (flagLoad)
            {
                Deserialize("TestResults.txt");
            }
            flagLoad = false;
        }
    }
}