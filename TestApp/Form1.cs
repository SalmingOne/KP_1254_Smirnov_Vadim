using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
        List<TestResult> results = new List<TestResult>();
        TestResult createResult()
        {
            TestResult result = new TestResult();
            result.nameAnswer = "Ответ"+' '+x;
            foreach (RadioButton rb in groupBox2.Controls)
            {
                if (rb.Checked)
                {
                    result.answer1 = rb.TabIndex;
                }
            }
            foreach (CheckBox сb in groupBox3.Controls)
            {
                if (сb.Checked == true)
                {
                    result.answer2.Add(сb.TabIndex);
               }
            }
            result.answer3 = comboBox1.SelectedIndex;
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

            textBox3.Text = result.answer5.ToString();
            comboBox1.SelectedIndex = result.answer3;
            numericUpDown2.Value = result.answer4;
            foreach(RadioButton rb in groupBox2.Controls) 
            {
                if (rb.TabIndex == result.answer1)
                {
                    rb.Checked = true;
                }
            }
            ;
            foreach(CheckBox cb in groupBox3.Controls)
            {
                if (cb.Checked == true)
                {
                    cb.Checked = false;
                }
                foreach (int index in result.answer2)
                {
                    if (cb.TabIndex == index)
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
            comboBox1.SelectedIndex = 0;
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
        void Deserialize()
        {
            StreamReader sr = new StreamReader("C:/Users/User/source/repos/WindowsFormsApp2/data.txt");
            string line = sr.ReadToEnd();
            string[] blocks = line.Split('.');


            foreach (string block in blocks)
            {
                TestResult testResult = new TestResult();
                string[] mas_str = block.Split(',');
                List<string> strings = new List<string>();
                foreach(string ma in mas_str)
                {
                    strings.Add(ma);
                }
                foreach(string s in strings)
                {
                    textBox3.Text += s;
                }
                
                
                
               /*testResult.nameAnswer = mas_str[0].Split('-')[1];
                testResult.name = mas_str[1].Split('-')[1];
                testResult.surname = mas_str[2].Split('-')[1];
                testResult.age = Convert.ToInt32(mas_str[3].Split('-')[1]);
                testResult.answer1 = Convert.ToInt32(mas_str[4].Split('-')[1]);
                foreach(char s in mas_str[5].Split('-')[1])
                {
                    testResult.answer2.Add(Convert.ToInt32(s));
                }
                testResult.answer3 = Convert.ToInt32(mas_str[6].Split('-')[1]);
                testResult.answer4 = Convert.ToInt32(mas_str[7].Split('-')[1]);
                testResult.answer5 = mas_str[8].Split('-')[1];
                results.Add(testResult);*/

            }
            
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TestResult result = createResult();
            results.Add(result);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            try
            {
                TestResult result = results[index - 1];
                fillForm(result);
                textBox3.Text = checkBox1.Text;
            }
            catch
            {
                return;
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
            
            foreach (TestResult testResult in results)
            {
                testResult.Serialize();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Deserialize();
            
        }
    }
}
