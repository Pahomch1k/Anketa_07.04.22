using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Dz_6_Anketa
{
    public partial class Form1 : Form
    {
        MainMenu MyMenu;
        MenuItem m1, m2, subm1, subm2, subm3, subm4;
        public int x { get; set; }

        public Form1()
        {
            InitializeComponent();

            MyMenu = new MainMenu();

            m1 = new MenuItem("Импорт"); 
            MyMenu.MenuItems.Add(m1);

            m2 = new MenuItem("Экспорт"); 
            MyMenu.MenuItems.Add(m2);

            subm1 = new MenuItem("txt");
            subm1.Click += new EventHandler(button3_Click); 
            m1.MenuItems.Add(subm1);

            subm2 = new MenuItem("xml");
            subm2.Click += new EventHandler(button4_Click);
            m1.MenuItems.Add(subm2);

            subm3 = new MenuItem("txt");
            subm3.Click += new EventHandler(button5_Click); 
            m2.MenuItems.Add(subm3);

            subm4 = new MenuItem("xml");
            subm4.Click += new EventHandler(button6_Click);
            m2.MenuItems.Add(subm4);

            Menu = MyMenu;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                string s = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text;
                comboBox1.Items.Add(s);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
            else MessageBox.Show("Заполните все поля");

            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            object obj = comboBox1.SelectedItem;
            string op = obj.ToString(); 
            textBox4.Text = op;

            ComboBox C = (ComboBox)sender;

            x = C.SelectedIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                string s = textBox4.Text;
                comboBox1.Items[x] = s;
            }
            else MessageBox.Show("Поле пустое");
        }

        private void button5_Click(object sender, EventArgs e)
        { 
            StreamReader sr = new StreamReader("1.txt", Encoding.UTF8);
            List<string> s1 = new List<string>();

            while (sr.EndOfStream != true)
                s1.Add(sr.ReadLine());  
            for (int i = 0; i < s1.Count; i++)
                comboBox1.Items.Add(s1[i]);  

            sr.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            StreamWriter sw = new StreamWriter("2.log", true);
            string line;

            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                line = comboBox1.Items[i].ToString();
                sw.WriteLine(line);
            } 
            sw.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        { 
            XmlTextReader reader = new XmlTextReader("3.xml");
            string str = null; 
            while (reader.Read()) // Считывает следующий узел из потока
            {
                if (reader.NodeType == XmlNodeType.Text)
                    str = reader.Value + "\n";
                 
                // NodeType возвращает тип текущего узла
                if (reader.NodeType == XmlNodeType.Element)
                {
                    str += reader.Value + "\n";
                    comboBox1.Items.Add(str); 
                } 
            }  
            reader.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            XmlTextWriter xmlwriter = new XmlTextWriter("3.xml", Encoding.UTF8);
            xmlwriter.WriteStartDocument();
            xmlwriter.Formatting = Formatting.Indented;
            xmlwriter.IndentChar = '\t';
            xmlwriter.Indentation = 1;
            xmlwriter.WriteStartElement("Persons"); // Записывает указанный открывающий тег 

            for (int i = 0; i < comboBox1.Items.Count; i++)
            {  
                xmlwriter.WriteStartElement($"Person_{i + 1}"); 
                xmlwriter.WriteString(comboBox1.Items[i].ToString()); 
                xmlwriter.WriteEndElement();
            } 
            xmlwriter.WriteEndElement(); 
            xmlwriter.Close();
        }
    }
}
