using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace teszt
{
    public partial class Form2 : Form
    {
        public static Form2 instance;
        public RichTextBox rtb1;

        public Form2()
        {
            InitializeComponent();
            instance = this;
            rtb1 = richTextBox1;
        }


        private void Form2_Load(object sender, EventArgs e)
        {
           
            if (Form1.instance.val1 == true)
            {
                richTextBox1.Text = Form1.instance.txt;
            }
            else if (Form1.instance.val1 == false)
            {
                richTextBox1.Text = Form1.instance.rtb2.Text;
            }
            
            int index = 0;

            foreach (var item in Form1.instance.termlst)
            {
                richTextBox1.Find(item, index, richTextBox1.TextLength, RichTextBoxFinds.None);
                richTextBox1.SelectionBackColor = Color.GreenYellow;
                index = richTextBox1.Text.IndexOf(item, index) + 1;
                richTextBox1.SelectAll(); 
            }

        }
    }
}
