using System.IO;
using System.Reflection;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace teszt
{
    public partial class Form1 : Form
    {
        public String s;
        public String r;
        public String v;
        public String ap;
        public String pa;
        public static Form1 instance;
        public RichTextBox rtb2;
        public List<string> termlst = new List<string>();

        public Form1()
        {
            InitializeComponent();
            s = "";
            richTextBox1.DragDrop += new DragEventHandler(richTextBox1_DragDrop);
            richTextBox1.AllowDrop = true;
            ap = "";
            v = "";
            instance = this;
            rtb2 = richTextBox2;
        }

        void richTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = e.Data.GetData(DataFormats.FileDrop) as string[];

                if (fileNames != null)
                {
                    foreach (string name in fileNames)
                    {
                        try
                        {
                            richTextBox1.AppendText(File.ReadAllText(name) + "\r\n--------------------------------------------------------------------------------------------------------\r\n\r\n");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        listBox1.Items.Add(name);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                string[] files = Directory.GetFiles(dialog.SelectedPath, "*.txt");
                string[] files2 = Directory.GetFiles(dialog.SelectedPath, "*.xml");
                foreach (string file in files)
                {
                    listBox1.Items.Add(file);
                }
                foreach (string file2 in files2)
                {
                    listBox1.Items.Add(file2);
                }
            }
            string text = "";
            string select1 = "\r\n\r\n-----------------------------------------------------------------------------------------------------------------\r\n\r\n";


            foreach (var item in listBox1.Items)
            {
                if (Path.GetExtension(item.ToString()) == ".xml" && val == true)
                {
                    var xml = XElement.Parse(File.ReadAllText(item.ToString()));
                    string strtext = xml.ToString();
                    text += strtext + select1;
                }
                else
                {
                    text += File.ReadAllText(item.ToString()) + select1;
                }
                //text += File.ReadAllText(item.ToString()) + select1;
            }

            richTextBox1.Text = text;

            string na = "Nem található";
            string text3 = "";
            foreach (var item in listBox1.Items)
            {
                string select = "\r\n";

                string nev1 = Path.GetFileName(item.ToString());
                string input = File.ReadAllText(item.ToString());

                string nev = "";
                var NAM = input.IndexOf("<NAM>") + 5;
                if (NAM < 5)
                {
                    nev = "Név: " + na;
                }
                else
                {
                    var NAM2 = input.Substring(NAM, input.IndexOf("</NAM>") - NAM);
                    nev = "Név: " + NAM2;
                }

                string cim = "";
                // var IRS2 = "";
                var IRS = input.IndexOf("<IRS>") + 5;
                if (IRS < 5)
                {
                    cim = "Székhely: " + na;
                }
                else
                {
                    var IRS2 = input.Substring(IRS, input.IndexOf("</IRS>") - IRS);
                    var TEL = input.IndexOf("<TEL>") + 5;
                    var TEL2 = input.Substring(TEL, input.IndexOf("</TEL>") - TEL);
                    var KZT = input.IndexOf("<KZT>") + 5;
                    var KZT2 = input.Substring(KZT, input.IndexOf("</KZT>") - KZT);
                    var KZJ = input.IndexOf("<KZJ>") + 5;
                    var KZJ2 = input.Substring(KZJ, input.IndexOf("</KZJ>") - KZJ);
                    var HSZ = input.IndexOf("<HSZ>") + 5;
                    var HSZ2 = input.Substring(HSZ, input.IndexOf("</HSZ") - HSZ);
                    cim = "Székhely: " + IRS2 + " " + TEL2 + " " + KZT2 + " " + KZJ2 + " " + HSZ2;
                }

                string TÖRZS = "";
                var TSZ = input.IndexOf("<TSZ>") + 5;
                if (TSZ < 5)
                {
                    TÖRZS = "Törzsszáma: " + na;
                }
                else
                {
                    var TSZ2 = input.Substring(TSZ, input.IndexOf("</TSZ") - TSZ);
                    TÖRZS = "Törzsszáma: " + TSZ2;
                }

                string cegnev = "";
                var TNA = input.IndexOf("<TNA>") + 5;
                if (TNA < 5)
                {
                    cegnev = "Cég neve: " + na;
                }
                else
                {
                    var TNA2 = input.Substring(TNA, input.IndexOf("</TNA") - TNA);
                    cegnev = "Cég neve: " + TNA2;
                }

                string telephely = "";
                var TZC = input.IndexOf("<TZC>") + 5;
                if (TZC < 5)
                {
                    telephely = "Telephely: " + na;
                }
                else
                {
                    var TZC2 = input.Substring(TZC, input.IndexOf("</TZC>") - TZC);
                    var TCY = input.IndexOf("<TCY>") + 5;
                    var TCY2 = input.Substring(TCY, input.IndexOf("</TCY>") - TCY);
                    var TKN = input.IndexOf("<TKN>") + 5;
                    var TKN2 = input.Substring(TKN, input.IndexOf("</TKN>") - TKN);
                    var TKJ = input.IndexOf("<TKJ>") + 5;
                    var TKJ2 = input.Substring(TKJ, input.IndexOf("</TKJ>") - TKJ);
                    var THH = input.IndexOf("<THH>") + 5;
                    var THH2 = input.Substring(THH, input.IndexOf("</THH") - THH);
                    telephely = "Telephely: " + TZC2 + " " + TCY2 + " " + TKN2 + " " + TKJ2 + " " + THH2;
                }

                string AP = "";
                var APN = input.IndexOf("<APN>") + 5;
                if (APN < 5)
                {
                    AP = "AP Szám: " + na;
                }
                else
                {
                    var APN2 = input.Substring(APN, input.IndexOf("</APN") - APN);
                    AP = "AP Szám: " + APN2;
                }

                string PV = "";
                var DPF = input.IndexOf("<DPF>") + 5;
                if (DPF < 5)
                {
                    PV = "Pénztárgép szoftver verziószáma: " + na;
                }
                else
                {

                    var DPF2 = input.Substring(DPF, input.IndexOf("</DPF") - DPF);
                    PV = "Pénztárgép szoftver verziószáma: " + DPF2;
                }

                string v = "";
                var DFW = input.IndexOf("<DFW>") + 5;
                if (DFW < 5)
                {
                    v = "AEE szoftver verzió: " + na;
                }
                else
                {
                    var DFW2 = input.Substring(DFW, input.IndexOf("</DFW") - DFW);
                    v = "AEE szoftver verzió: " + DFW2;
                }

                string HT = "";
                var SSG = input.IndexOf("<SSG>") + 5;
                if (SSG < 5)
                {
                    HT = "Hálózati térerõ: " + na;
                }
                else
                {
                    var SSG2 = input.Substring(SSG, input.IndexOf("</SSG") - SSG);
                    HT = "Hálózati térerõ: " + SSG2 + " dBm";
                }

                string SZ = "";
                var SAC = input.IndexOf("<SAC>") + 5;
                if (SAC < 5)
                {
                    SZ = "Akkumulátor töltöttség: " + na;
                }
                else
                {

                    var SAC2 = input.Substring(SAC, input.IndexOf("</SAC") - SAC);
                    SZ = "Akkumulátor töltöttség: " + SAC2 + "%";
                }

                text3 += nev1 + select + select + nev + select + cim + select + TÖRZS + select + cegnev + select + telephely + select + AP + select + PV + select + v + select + HT + select + SZ + select + select;

                richTextBox2.Text = text3;
            }
        }

        string serializedButtonsPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\DynamicButtons.csv";
        private async Task btn()
        {
            Button btn = new Button();
            btn.Width = 198;
            btn.Height = 40;
            btn.Name = Interaction.InputBox("Add meg az új gomb nevét");
            btn.Text = btn.Name;

            Action<object, EventArgs> dynamicButtonOnClick = btn_Click;
            btn.Click += new EventHandler(btn_Click);

            Action<object, MouseEventArgs> dynamicButtonMouseUp = btn_MouseUp;
            btn.MouseUp += new MouseEventHandler(btn_MouseUp);

            btn.TabStop = false;
            btn.Dock = DockStyle.Top;
            panel1.Controls.Add(btn);

            var membersData = new List<string>();
            membersData.Add(btn.Height.ToString());
            membersData.Add(btn.Width.ToString());
            membersData.Add(btn.BackColor.Name.ToString());
            membersData.Add(btn.ForeColor.Name.ToString());
            membersData.Add(btn.Name);
            membersData.Add(btn.Font.OriginalFontName);
            membersData.Add(btn.Font.Size.ToString());
            membersData.Add(dynamicButtonOnClick.Method.Name);
            membersData.Add(dynamicButtonMouseUp.Method.Name);
            File.AppendAllText(serializedButtonsPath, string.Join(";", membersData) + Environment.NewLine);
        }
        private int selctionStop3 = 0;
        private async void btn_Click(object? sender, EventArgs e)
        {
            Button btn = (Button)sender;
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = Color.White;
            label2.Text = "";

            selectionStart = 0;
            selectionStop = 0;

            ct = 0;
            ni = 0;

            var str = btn.Text;
            char[] separator = { ',', '|' };
            parts.Clear();
            i = 0;
            subs = str.Split(separator);
            ar = subs.Length;
            if (ar > 1)
            {
                foreach (string wordToFind in subs)
                {
                    if (btn.Text.Length > 0)
                    {

                        int index = richTextBox1.Text.IndexOf(wordToFind);
                        while (index != -1)
                        {

                            richTextBox1.Select(index, wordToFind.Length);
                            richTextBox1.SelectionBackColor = Color.GreenYellow;

                            index = richTextBox1.Text.IndexOf(wordToFind,
                            index + wordToFind.Length);

                            int ab;
                            if (index < ar)
                            {

                                ab = 0;
                            }
                            else
                            {
                                ab = index;
                            }
                            parts.Add(new Part() { PartName = wordToFind, PartId = ab });
                            count++;
                        }
                    }

                }

                await multifirst();

                parts.Sort();


                for (int i = 0; i < parts.Count; ++i)
                {
                    terms.Add(parts[i].PartName);
                }

                int cont = 0;
                int cnt = 0;
                foreach (string s in subs)
                {
                    cont = 0;
                    cnt = 0;

                    for (int i = 0; i < terms.Count; ++i)
                    {
                        if (terms[i] == s)
                        {
                            cnt++;
                        }
                    }
                    cont = cnt;

                    label2.Text += s + ": " + cont.ToString() + "  ";

                }

                terms.Clear();
                panel3.Show();

                textBox1.Text = btn.Text;
            }
            else
            {
                int index = 0;
                string temp = richTextBox1.Text;
                richTextBox1.Text = "";
                richTextBox1.Text = temp;
                int count = 0;
                while (index < richTextBox1.Text.LastIndexOf(btn.Name))
                {
                    richTextBox1.Find(btn.Name, index, richTextBox1.TextLength, RichTextBoxFinds.None);
                    richTextBox1.SelectionBackColor = Color.GreenYellow;
                    index = richTextBox1.Text.IndexOf(btn.Name, index) + 1;
                    count++;
                }
                await first();
                textBox1.Text = btn.Name.ToString();

                label2.Text = count.ToString();
                panel3.Show();
                ct = count;
                label2.Text = ni.ToString() + "/" + ct.ToString();

                string res = ": " + count;
                if (btn.Text == btn.Name)
                {
                    btn.Text += res;
                }
                else
                {
                    btn.Text = btn.Name;
                }
            }
        }

        private void btn_MouseUp(object? sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            if (e.Button == MouseButtons.Right)
            {
                string split = ";";
                string buttononclick = "btn_Click";
                string mouseup = "btn_MouseUp";
                s = btn.Name;
                r = btn.Height + split + btn.Width + split + btn.BackColor.Name + split + btn.ForeColor.Name + split + /*btn.Location.X + split + btn.Location.Y + split + btn.Text + split +*/ btn.Name + split + btn.Font.OriginalFontName + split + btn.Font.Size + split + buttononclick + split + mouseup;
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await btn();
        }

        private int selctionStop2 = 0;
        private async Task first()
        {
            if (richTextBox1.Text.Length > 0 && textBox1.Text.Length > 0)
            {
                selectionStart = richTextBox1.Find(textBox1.Text, selctionStop2, richTextBox1.TextLength, RichTextBoxFinds.None);
                richTextBox1.Focus();

                richTextBox1.SelectionBackColor = Color.GreenYellow;
            }
        }

        public class Part : IEquatable<Part>, IComparable<Part>
        {
            public string PartName { get; set; }

            public int PartId { get; set; }

            //public override int PartInd()
            //{
            //    return PartId;
            //}

            public override string ToString()
            {
                return PartName;
            }

            //public override string ToString()
            //{
            //    return PartId + " " + PartName;
            //}

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                Part objAsPart = obj as Part;
                if (objAsPart == null) return false;
                else return Equals(objAsPart);
            }
            public int SortByNameAscending(string name1, string name2)
            {

                return name1.CompareTo(name2);
            }

            // Default comparer for Part type.
            public int CompareTo(Part comparePart)
            {
                // A null value means that this object is greater.
                if (comparePart == null)
                    return 1;

                else
                    return this.PartId.CompareTo(comparePart.PartId);
            }
            public override int GetHashCode()
            {
                return PartId;
            }
            public bool Equals(Part other)
            {
                if (other == null) return false;
                return (this.PartId.Equals(other.PartId));
            }
            // Should also override == and != operators.
        }

        private int Stop = 0;
        private async Task multifirst()
        {

            int index;
            List<string> terms = new List<string>();

            foreach (var item in subs)
            {
                index = richTextBox1.Text.IndexOf(item);
                terms.Add(index.ToString());
            }

            string shrt = terms.AsQueryable().Min();

            int shrt1 = Convert.ToInt32(shrt);

            string val = null;
            foreach (var item in subs)
            {
                if (richTextBox1.Text.IndexOf(item) == shrt1)
                {
                    val = item;
                }
            }

            selectionStart = richTextBox1.Find(val, Stop, richTextBox1.TextLength, RichTextBoxFinds.None);

            richTextBox1.Focus();

            richTextBox1.SelectionBackColor = Color.GreenYellow;

        }

        public int count = 0;
        string[] subs = null;

        List<Part> parts = new List<Part>();
        List<string> terms = new List<string>();
        public int ar;
        public int ct;
        //keresés(FN SEARCH)
        private async void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = Color.White;
            label2.Text = "";

            selectionStart = 0;
            selectionStop = 0;

            ct = 0;
            ni = 0;

            var str = textBox1.Text;
            char[] separator = { ',', '|' };
            parts.Clear();
            i = 0;
            subs = str.Split(separator);
            ar = subs.Length;
            if (ar > 1)
            {
                foreach (string wordToFind in subs)
                {
                    if (textBox1.Text.Length > 0)
                    {

                        int index = richTextBox1.Text.IndexOf(wordToFind);
                        while (index != -1)
                        {

                            richTextBox1.Select(index, wordToFind.Length);
                            richTextBox1.SelectionBackColor = Color.GreenYellow;

                            index = richTextBox1.Text.IndexOf(wordToFind,
                            index + wordToFind.Length);

                            int ab;
                            if (index < ar)
                            {

                                ab = 0;
                            }
                            else
                            {
                                ab = index;
                            }
                            parts.Add(new Part() { PartName = wordToFind, PartId = ab });
                            count++;
                        }
                    }

                }

                await multifirst();

                parts.Sort();


                for (int i = 0; i < parts.Count; i++)
                {
                    terms.Add(parts[i].PartName);
                }

                int cont = 0;
                int cnt = 0;
                foreach (string s in subs)
                {
                    cont = 0;
                    cnt = 0;

                    for (int i = 0; i < terms.Count; i++)
                    {
                        if (terms[i] == s)
                        {
                            cnt++;
                        }
                    }
                    cont = cnt;

                    label2.Text += s + ": " + cont.ToString() + "  ";

                }

                terms.Clear();
                panel3.Show();
            }
            else
            {
                if (textBox1.Text.Length > 0)
                {
                    int index = 0;
                    string temp = richTextBox1.Text;
                    richTextBox1.Text = "";
                    richTextBox1.Text = temp;
                    int count = 0;
                    while (index < richTextBox1.Text.LastIndexOf(textBox1.Text))
                    {
                        richTextBox1.Find(textBox1.Text, index, richTextBox1.TextLength, RichTextBoxFinds.None);
                        richTextBox1.SelectionBackColor = Color.GreenYellow;
                        index = richTextBox1.Text.IndexOf(textBox1.Text, index) + 1;
                        count++;
                    }
                    await first();

                    panel3.Show();

                    ct = count;
                    label2.Text = ni.ToString() + "/" + ct.ToString();
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string line = null;
            groupBox1.Hide();
            if (File.Exists("config/check.txt"))
            {
                if (new FileInfo("config/check.txt").Length != 0)
                {
                    line = File.ReadLines("config/check.txt").First();

                    if (line == "XML-ek tagolt betöltése:false")
                    {
                        val = false;
                        checkBox1.Checked = false;
                    }
                    else if (line == "XML-ek tagolt betöltése:true")
                    {
                        val = true;
                        checkBox1.Checked = true;
                    }
                }
            }

            string line1 = null;
            
            if (File.Exists("config/check2.txt"))
            {
                if (new FileInfo("config/check2.txt").Length != 0)
                {
                    line1 = File.ReadLines("config/check2.txt").First();

                    if (line1 == "Vizsgálat megjelenitése máskép:false")
                    {
                        val1 = false;
                        checkBox2.Checked = false;
                    }
                    else if (line1 == "Vizsgálat megjelenitése máskép:true")
                    {
                        val1 = true;
                        checkBox2.Checked = true;
                    }
                }
            }

            panel3.Hide();
            label2.Text = "";
            string path = "DynamicButtons.csv";
            if (File.Exists(path))
            {
                var serializedButtons = File.ReadAllLines(serializedButtonsPath);
                foreach (var button in serializedButtons)
                {
                    var membersData = button.Split(';');


                    Button dynamicButton = new Button();

                    // Set Button properties
                    var i = 0;
                    dynamicButton.Height = int.Parse(membersData[i++]);
                    dynamicButton.Width = int.Parse(membersData[i++]);
                    dynamicButton.BackColor = Color.FromName(membersData[i++]);
                    dynamicButton.ForeColor = Color.FromName(membersData[i++]);
                    //dynamicButton.Location = new Point(int.Parse(membersData[i++]), int.Parse(membersData[i++]));
                    //dynamicButton.Text = membersData[i++];
                    dynamicButton.Name = membersData[i++];
                    dynamicButton.Font = new Font(membersData[i++], float.Parse(membersData[i++]));

                    //Store method name to make it independent of the index (i)
                    var eventMethodName = membersData[i++];

                    //Set the event by method name using reflection, the binding flags allow to access the private method
                    dynamicButton.Click += (_sender, _e) =>
                            GetType().GetMethod(eventMethodName, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, new[] { _sender, _e });

                    var mouseMethodName = membersData[i++];

                    dynamicButton.MouseUp += (_sender, _e) =>
                            GetType().GetMethod(mouseMethodName, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, new[] { _sender, _e });
                    dynamicButton.Dock = DockStyle.Top;
                    dynamicButton.Text = dynamicButton.Name;
                    panel1.Controls.Add(dynamicButton);
                }
            }

            int id = 0;
            string path2 = "ListsButtons.csv";
            if (File.Exists(path2))
            {
                var serializedButtons = File.ReadAllLines(serializedButtonsPath1);
                foreach (String items in serializedButtons)
                {
                    var membersData = items.Split(';');
                    ToolStripMenuItem item = new ToolStripMenuItem(membersData[5]);

                    item.Tag = id;

                    id++;

                    toolStripMenuItem8.DropDownItems.Add(item);

                    item.Click += new EventHandler(item_Click);
                }
            }

            if (File.Exists(path2))
            {
                var serializedButtons = File.ReadAllLines(serializedButtonsPath1);
                foreach (var button in serializedButtons)
                {
                    var membersData = button.Split(';');

                    Button dynamicButton = new Button();

                    // Set Button properties
                    var i = 0;
                    dynamicButton.Height = int.Parse(membersData[i++]);
                    dynamicButton.Width = int.Parse(membersData[i++]);
                    dynamicButton.BackColor = Color.FromName(membersData[i++]);
                    dynamicButton.ForeColor = Color.FromName(membersData[i++]);
                    dynamicButton.Text = membersData[i++];
                    dynamicButton.Name = membersData[i++];
                    dynamicButton.Font = new Font(membersData[i++], float.Parse(membersData[i++]));

                    //Store method name to make it independent of the index (i)
                    var eventMethodName = membersData[i++];

                    //Set the event by method name using reflection, the binding flags allow to access the private method
                    dynamicButton.Click += (_sender, _e) =>
                            GetType().GetMethod(eventMethodName, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, new[] { _sender, _e });

                    var mouseupmethode = membersData[i++];

                    dynamicButton.MouseUp += (_sender, _e) =>
                            GetType().GetMethod(mouseupmethode, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, new[] { _sender, _e });

                    dynamicButton.Dock = DockStyle.Top;

                    panel4.Controls.Add(dynamicButton);
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string path = "DynamicButtons.csv";
            foreach (Control item in panel1.Controls)
            {
                if (item.Name == s)
                {
                    panel1.Controls.Remove(item);

                    //fájlbol törlés
                    string line = null;
                    string line_to_delete = r;

                    using (StreamReader reader = new StreamReader(path))
                    {
                        using (StreamWriter writer = new StreamWriter("new.csv"))
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (String.Compare(line, line_to_delete) == 0)
                                    continue;

                                writer.WriteLine(line);

                            }
                        }
                    }
                    File.Delete(path);
                    System.IO.File.Move("new.csv", "DynamicButtons.csv");
                    break; //important step
                }
            }

            //richTextBox1.Text = r;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = "";
            string select1 = "\r\n\r\n-----------------------------------------------------------------------------------------------------------------\r\n\r\n";
            int ltc = listBox1.Items.Count;
            if (ltc != 0)
            {
                foreach (var item in listBox1.Items)
                {
                    if (Path.GetExtension(item.ToString()) == ".xml" && val == true)
                    {
                        var xml = XElement.Parse(File.ReadAllText(item.ToString()));
                        string strtext = xml.ToString();
                        text += strtext + select1;
                    }
                    else
                    {
                        text += File.ReadAllText(item.ToString()) + select1;
                    }
                    //text += File.ReadAllText(item.ToString()) + select1;
                }

                richTextBox1.Text = text;
            }
        }

        //találatok törlése(FN X)
        private void button6_Click(object sender, EventArgs e)
        {
            i = 0;
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = Color.White;
            textBox1.Clear();
            label2.Text = 0.ToString();
            if (terms.Count > 0)
            {
                terms.Clear();
            }
        }

        //FN Fájlbol
        private void button5_Click(object sender, EventArgs e)
        {
            Stream mystream;
            OpenFileDialog dg = new OpenFileDialog();

            if (dg.ShowDialog() == DialogResult.OK)
            {
                if ((mystream = dg.OpenFile()) != null)
                {
                    string strfilename = dg.FileName;
                    listBox1.Items.Clear();
                    listBox1.Items.Add(strfilename);
                    string filetext = File.ReadAllText(strfilename);
                    if (Path.GetExtension(strfilename) == ".xml" && val == true)
                    {
                        //filetext = File.ReadAllText(strfilename);
                        var xml = XElement.Parse(filetext);
                        string strtext = xml.ToString();
                        richTextBox1.Text = strtext;
                    }
                    else
                    {
                        //filetext = File.ReadAllText(strfilename);
                        richTextBox1.Text = filetext;
                    }
                }
            }
        }


        //FN Tisztitás
        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            listBox1.Items.Clear();
            textBox1.Clear();
        }


        private void button9_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
        }


        private void button8_Click(object sender, EventArgs e)
        {

            //if ciklus ha van benne szöveg
            if (String.IsNullOrEmpty(richTextBox1.Text))
            {

                MessageBox.Show("Nincs szöveg betöltve");

            }
            else
            {
                string text3 = "";
                foreach (var item in listBox1.Items)
                {
                    string na = "Nem található";
                    string select = "\r\n";

                    string nev1 = Path.GetFileName(item.ToString());
                    string input = File.ReadAllText(item.ToString());

                    string nev = "";
                    var NAM = input.IndexOf("<NAM>") + 5;
                    if (NAM < 5)
                    {
                        nev = "Név: " + na;
                    }
                    else
                    {
                        var NAM2 = input.Substring(NAM, input.IndexOf("</NAM>") - NAM);
                        nev = "Név: " + NAM2;
                    }

                    string cim = "";
                    // var IRS2 = "";
                    var IRS = input.IndexOf("<IRS>") + 5;
                    if (IRS < 5)
                    {
                        cim = "Székhely: " + na;
                    }
                    else
                    {
                        var IRS2 = input.Substring(IRS, input.IndexOf("</IRS>") - IRS);
                        var TEL = input.IndexOf("<TEL>") + 5;
                        var TEL2 = input.Substring(TEL, input.IndexOf("</TEL>") - TEL);
                        var KZT = input.IndexOf("<KZT>") + 5;
                        var KZT2 = input.Substring(KZT, input.IndexOf("</KZT>") - KZT);
                        var KZJ = input.IndexOf("<KZJ>") + 5;
                        var KZJ2 = input.Substring(KZJ, input.IndexOf("</KZJ>") - KZJ);
                        var HSZ = input.IndexOf("<HSZ>") + 5;
                        var HSZ2 = input.Substring(HSZ, input.IndexOf("</HSZ") - HSZ);
                        cim = "Székhely: " + IRS2 + " " + TEL2 + " " + KZT2 + " " + KZJ2 + " " + HSZ2;
                    }

                    string TÖRZS = "";
                    var TSZ = input.IndexOf("<TSZ>") + 5;
                    if (TSZ < 5)
                    {
                        TÖRZS = "Törzsszáma: " + na;
                    }
                    else
                    {
                        var TSZ2 = input.Substring(TSZ, input.IndexOf("</TSZ") - TSZ);
                        TÖRZS = "Törzsszáma: " + TSZ2;
                    }

                    string cegnev = "";
                    var TNA = input.IndexOf("<TNA>") + 5;
                    if (TNA < 5)
                    {
                        cegnev = "Cég neve: " + na;
                    }
                    else
                    {
                        var TNA2 = input.Substring(TNA, input.IndexOf("</TNA") - TNA);
                        cegnev = "Cég neve: " + TNA2;
                    }

                    string telephely = "";
                    var TZC = input.IndexOf("<TZC>") + 5;
                    if (TZC < 5)
                    {
                        telephely = "Telephely: " + na;
                    }
                    else
                    {
                        var TZC2 = input.Substring(TZC, input.IndexOf("</TZC>") - TZC);
                        var TCY = input.IndexOf("<TCY>") + 5;
                        var TCY2 = input.Substring(TCY, input.IndexOf("</TCY>") - TCY);
                        var TKN = input.IndexOf("<TKN>") + 5;
                        var TKN2 = input.Substring(TKN, input.IndexOf("</TKN>") - TKN);
                        var TKJ = input.IndexOf("<TKJ>") + 5;
                        var TKJ2 = input.Substring(TKJ, input.IndexOf("</TKJ>") - TKJ);
                        var THH = input.IndexOf("<THH>") + 5;
                        var THH2 = input.Substring(THH, input.IndexOf("</THH") - THH);
                        telephely = "Telephely: " + TZC2 + " " + TCY2 + " " + TKN2 + " " + TKJ2 + " " + THH2;
                    }

                    string AP = "";
                    var APN = input.IndexOf("<APN>") + 5;
                    if (APN < 5)
                    {
                        AP = "AP Szám: " + na;
                    }
                    else
                    {
                        var APN2 = input.Substring(APN, input.IndexOf("</APN") - APN);
                        AP = "AP Szám: " + APN2;
                    }

                    string PV = "";
                    var DPF = input.IndexOf("<DPF>") + 5;
                    if (DPF < 5)
                    {
                        PV = "Pénztárgép szoftver verziószáma: " + na;
                    }
                    else
                    {

                        var DPF2 = input.Substring(DPF, input.IndexOf("</DPF") - DPF);
                        PV = "Pénztárgép szoftver verziószáma: " + DPF2;
                    }

                    string v = "";
                    var DFW = input.IndexOf("<DFW>") + 5;
                    if (DFW < 5)
                    {
                        v = "AEE szoftver verzió: " + na;
                    }
                    else
                    {
                        var DFW2 = input.Substring(DFW, input.IndexOf("</DFW") - DFW);
                        v = "AEE szoftver verzió: " + DFW2;
                    }

                    string HT = "";
                    var SSG = input.IndexOf("<SSG>") + 5;
                    if (SSG < 5)
                    {
                        HT = "Hálózati térerõ: " + na;
                    }
                    else
                    {
                        var SSG2 = input.Substring(SSG, input.IndexOf("</SSG") - SSG);
                        HT = "Hálózati térerõ: " + SSG2 + " dBm";
                    }

                    string SZ = "";
                    var SAC = input.IndexOf("<SAC>") + 5;
                    if (SAC < 5)
                    {
                        SZ = "Akkumulátor töltöttség: " + na;
                    }
                    else
                    {

                        var SAC2 = input.Substring(SAC, input.IndexOf("</SAC") - SAC);
                        SZ = "Akkumulátor töltöttség: " + SAC2 + "%";
                    }

                    text3 += nev1 + select + select + nev + select + cim + select + TÖRZS + select + cegnev + select + telephely + select + AP + select + PV + select + v + select + HT + select + SZ + select + select;

                    richTextBox2.Text = text3;

                }
            }

            if (listBox1.Items.Count == 0)
            {
                string na = "Nem található";
                string text3 = "";
                string select = "\r\n";
                string input = richTextBox1.Text;

                string nev = "";
                var NAM = input.IndexOf("<NAM>") + 5;
                if (NAM < 5)
                {
                    nev = "Név: " + na;
                }
                else
                {
                    var NAM2 = input.Substring(NAM, input.IndexOf("</NAM>") - NAM);
                    nev = "Név: " + NAM2;
                }

                string cim = "";
                // var IRS2 = "";
                var IRS = input.IndexOf("<IRS>") + 5;
                if (IRS < 5)
                {
                    cim = "Székhely: " + na;
                }
                else
                {
                    var IRS2 = input.Substring(IRS, input.IndexOf("</IRS>") - IRS);
                    var TEL = input.IndexOf("<TEL>") + 5;
                    var TEL2 = input.Substring(TEL, input.IndexOf("</TEL>") - TEL);
                    var KZT = input.IndexOf("<KZT>") + 5;
                    var KZT2 = input.Substring(KZT, input.IndexOf("</KZT>") - KZT);
                    var KZJ = input.IndexOf("<KZJ>") + 5;
                    var KZJ2 = input.Substring(KZJ, input.IndexOf("</KZJ>") - KZJ);
                    var HSZ = input.IndexOf("<HSZ>") + 5;
                    var HSZ2 = input.Substring(HSZ, input.IndexOf("</HSZ") - HSZ);
                    cim = "Székhely: " + IRS2 + " " + TEL2 + " " + KZT2 + " " + KZJ2 + " " + HSZ2;
                }

                string TÖRZS = "";
                var TSZ = input.IndexOf("<TSZ>") + 5;
                if (TSZ < 5)
                {
                    TÖRZS = "Törzsszáma: " + na;
                }
                else
                {
                    var TSZ2 = input.Substring(TSZ, input.IndexOf("</TSZ") - TSZ);
                    TÖRZS = "Törzsszáma: " + TSZ2;
                }

                string cegnev = "";
                var TNA = input.IndexOf("<TNA>") + 5;
                if (TNA < 5)
                {
                    cegnev = "Cég neve: " + na;
                }
                else
                {
                    var TNA2 = input.Substring(TNA, input.IndexOf("</TNA") - TNA);
                    cegnev = "Cég neve: " + TNA2;
                }

                string telephely = "";
                var TZC = input.IndexOf("<TZC>") + 5;
                if (TZC < 5)
                {
                    telephely = "Telephely: " + na;
                }
                else
                {
                    var TZC2 = input.Substring(TZC, input.IndexOf("</TZC>") - TZC);
                    var TCY = input.IndexOf("<TCY>") + 5;
                    var TCY2 = input.Substring(TCY, input.IndexOf("</TCY>") - TCY);
                    var TKN = input.IndexOf("<TKN>") + 5;
                    var TKN2 = input.Substring(TKN, input.IndexOf("</TKN>") - TKN);
                    var TKJ = input.IndexOf("<TKJ>") + 5;
                    var TKJ2 = input.Substring(TKJ, input.IndexOf("</TKJ>") - TKJ);
                    var THH = input.IndexOf("<THH>") + 5;
                    var THH2 = input.Substring(THH, input.IndexOf("</THH") - THH);
                    telephely = "Telephely: " + TZC2 + " " + TCY2 + " " + TKN2 + " " + TKJ2 + " " + THH2;
                }

                string AP = "";
                var APN = input.IndexOf("<APN>") + 5;
                if (APN < 5)
                {
                    AP = "AP Szám: " + na;
                }
                else
                {
                    var APN2 = input.Substring(APN, input.IndexOf("</APN") - APN);
                    AP = "AP Szám: " + APN2;
                }

                string PV = "";
                var DPF = input.IndexOf("<DPF>") + 5;
                if (DPF < 5)
                {
                    PV = "Pénztárgép szoftver verziószáma: " + na;
                }
                else
                {

                    var DPF2 = input.Substring(DPF, input.IndexOf("</DPF") - DPF);
                    PV = "Pénztárgép szoftver verziószáma: " + DPF2;
                }

                string v = "";
                var DFW = input.IndexOf("<DFW>") + 5;
                if (DFW < 5)
                {
                    v = "AEE szoftver verzió: " + na;
                }
                else
                {
                    var DFW2 = input.Substring(DFW, input.IndexOf("</DFW") - DFW);
                    v = "AEE szoftver verzió: " + DFW2;
                }

                string HT = "";
                var SSG = input.IndexOf("<SSG>") + 5;
                if (SSG < 5)
                {
                    HT = "Hálózati térerõ: " + na;
                }
                else
                {
                    var SSG2 = input.Substring(SSG, input.IndexOf("</SSG") - SSG);
                    HT = "Hálózati térerõ: " + SSG2 + " dBm";
                }

                string SZ = "";
                var SAC = input.IndexOf("<SAC>") + 5;
                if (SAC < 5)
                {
                    SZ = "Akkumulátor töltöttség: " + na;
                }
                else
                {

                    var SAC2 = input.Substring(SAC, input.IndexOf("</SAC") - SAC);
                    SZ = "Akkumulátor töltöttség: " + SAC2 + "%";
                }

                text3 += nev + select + cim + select + TÖRZS + select + cegnev + select + telephely + select + AP + select + PV + select + v + select + HT + select + SZ + select + select;

                richTextBox2.Text = text3;
            }
        }


        int selectionStart = 0;
        int selectionStop = 0;

        public int i = 0;
        public int ni = 0;
        private void button11_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0 && textBox1.Text.Length > 0)
            {
                if (ar > 1)
                {

                    selectionStart = richTextBox1.Find(parts[i].PartName, selectionStop, richTextBox1.Text.Length, RichTextBoxFinds.None);
                    selectionStop = selectionStart + parts[i].PartName.Length;
                    richTextBox1.Focus();
                    richTextBox1.SelectionBackColor = Color.GreenYellow;


                    i++;

                    if (i == parts.Count)
                    {
                        i = 0;
                    }
                }
                else
                {
                    selectionStart = richTextBox1.Find(textBox1.Text, selectionStop, richTextBox1.TextLength, RichTextBoxFinds.None);
                    selectionStop = selectionStart + textBox1.Text.Length;
                    richTextBox1.Focus();
                    ni++;
                    richTextBox1.SelectionBackColor = Color.GreenYellow;
                    if (ni == ct + 1)
                    {
                        ni = 1;
                    }
                    label2.Text = ni.ToString() + "/" + ct.ToString();
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0 && textBox1.Text.Length > 0)
            {
                if (ar > 1)
                {

                    selectionStart = richTextBox1.Find(parts[i].PartName, 0, selectionStart, RichTextBoxFinds.Reverse);
                    selectionStop = selectionStart + parts[i].PartName.Length;
                    richTextBox1.Focus();

                    richTextBox1.SelectionBackColor = Color.GreenYellow;

                    i++;

                    if (i == parts.Count)
                    {
                        i = 0;
                    }
                }
                else
                {
                    selectionStart = richTextBox1.Find(textBox1.Text, 0, selectionStart, RichTextBoxFinds.Reverse);
                    selectionStop = selectionStart + textBox1.Text.Length;
                    richTextBox1.Focus();
                    ni--;
                    richTextBox1.SelectionBackColor = Color.GreenYellow;
                    if (ni == -1)
                    {
                        ni = ct;
                    }
                    label2.Text = ni.ToString() + "/" + ct.ToString();
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //foreach (var item in listBox1.Items)
            //{
            //    if (Path.GetExtension(item.ToString()) == ".xml")
            //    {
            //        var xml = XElement.Parse(File.ReadAllText(item.ToString()));
            //        string strtext = xml.ToString();
            //        text += strtext + select1;
            //    }
            //    else
            //    {
            //        text += File.ReadAllText(item.ToString()) + select1;
            //    }
            //    //text += File.ReadAllText(item.ToString()) + select1;
            //}


            string na = "Nem található";
            string text = "";

            var item = listBox1.SelectedItem;
            if (Path.GetExtension(item.ToString()) == ".xml" && val == true)
            {
                var xml = XElement.Parse(File.ReadAllText(item.ToString()));
                string strtext = xml.ToString();
                text += strtext;
            }
            else
            {
                text += File.ReadAllText(item.ToString());
            }

            richTextBox1.Text = text;
            richTextBox1.Focus();

            string text3 = "";

            string nev1 = Path.GetFileName(item.ToString());
            string select = "\r\n";
            string input = File.ReadAllText(item.ToString());

            string nev = "";
            var NAM = input.IndexOf("<NAM>") + 5;
            if (NAM < 5)
            {
                nev = "Név: " + na;
            }
            else
            {
                var NAM2 = input.Substring(NAM, input.IndexOf("</NAM>") - NAM);
                nev = "Név: " + NAM2;
            }

            string cim = "";
            //var IRS2 = "";
            var IRS = input.IndexOf("<IRS>") + 5;
            if (IRS < 5)
            {
                cim = "Székhely: " + na;
            }
            else
            {
                var IRS2 = input.Substring(IRS, input.IndexOf("</IRS>") - IRS);
                var TEL = input.IndexOf("<TEL>") + 5;
                var TEL2 = input.Substring(TEL, input.IndexOf("</TEL>") - TEL);
                var KZT = input.IndexOf("<KZT>") + 5;
                var KZT2 = input.Substring(KZT, input.IndexOf("</KZT>") - KZT);
                var KZJ = input.IndexOf("<KZJ>") + 5;
                var KZJ2 = input.Substring(KZJ, input.IndexOf("</KZJ>") - KZJ);
                var HSZ = input.IndexOf("<HSZ>") + 5;
                var HSZ2 = input.Substring(HSZ, input.IndexOf("</HSZ") - HSZ);
                cim = "Székhely: " + IRS2 + " " + TEL2 + " " + KZT2 + " " + KZJ2 + " " + HSZ2;
            }

            string TÖRZS = "";
            var TSZ = input.IndexOf("<TSZ>") + 5;
            if (TSZ < 5)
            {
                TÖRZS = "Törzsszáma: " + na;
            }
            else
            {
                var TSZ2 = input.Substring(TSZ, input.IndexOf("</TSZ") - TSZ);
                TÖRZS = "Törzsszáma: " + TSZ2;
            }

            string cegnev = "";
            var TNA = input.IndexOf("<TNA>") + 5;
            if (TNA < 5)
            {
                cegnev = "Cég neve: " + na;
            }
            else
            {
                var TNA2 = input.Substring(TNA, input.IndexOf("</TNA") - TNA);
                cegnev = "Cég neve: " + TNA2;
            }

            string telephely = "";
            var TZC = input.IndexOf("<TZC>") + 5;
            if (TZC < 5)
            {
                telephely = "Telephely: " + na;
            }
            else
            {
                var TZC2 = input.Substring(TZC, input.IndexOf("</TZC>") - TZC);
                var TCY = input.IndexOf("<TCY>") + 5;
                var TCY2 = input.Substring(TCY, input.IndexOf("</TCY>") - TCY);
                var TKN = input.IndexOf("<TKN>") + 5;
                var TKN2 = input.Substring(TKN, input.IndexOf("</TKN>") - TKN);
                var TKJ = input.IndexOf("<TKJ>") + 5;
                var TKJ2 = input.Substring(TKJ, input.IndexOf("</TKJ>") - TKJ);
                var THH = input.IndexOf("<THH>") + 5;
                var THH2 = input.Substring(THH, input.IndexOf("</THH") - THH);
                telephely = "Telephely: " + TZC2 + " " + TCY2 + " " + TKN2 + " " + TKJ2 + " " + THH2;
            }

            string AP = "";
            var APN = input.IndexOf("<APN>") + 5;
            if (APN < 5)
            {
                AP = "AP Szám: " + na;
            }
            else
            {
                var APN2 = input.Substring(APN, input.IndexOf("</APN") - APN);
                AP = "AP Szám: " + APN2;
            }

            string PV = "";
            var DPF = input.IndexOf("<DPF>") + 5;
            if (DPF < 5)
            {
                PV = "Pénztárgép szoftver verziószáma: " + na;
            }
            else
            {

                var DPF2 = input.Substring(DPF, input.IndexOf("</DPF") - DPF);
                PV = "Pénztárgép szoftver verziószáma: " + DPF2;
            }

            string v = "";
            var DFW = input.IndexOf("<DFW>") + 5;
            if (DFW < 5)
            {
                v = "AEE szoftver verzió: " + na;
            }
            else
            {
                var DFW2 = input.Substring(DFW, input.IndexOf("</DFW") - DFW);
                v = "AEE szoftver verzió: " + DFW2;
            }

            string HT = "";
            var SSG = input.IndexOf("<SSG>") + 5;
            if (SSG < 5)
            {
                HT = "Hálózati térerõ: " + na;
            }
            else
            {
                var SSG2 = input.Substring(SSG, input.IndexOf("</SSG") - SSG);
                HT = "Hálózati térerõ: " + SSG2 + " dBm";
            }

            string SZ = "";
            var SAC = input.IndexOf("<SAC>") + 5;
            if (SAC < 5)
            {
                SZ = "Akkumulátor töltöttség: " + na;
            }
            else
            {

                var SAC2 = input.Substring(SAC, input.IndexOf("</SAC") - SAC);
                SZ = "Akkumulátor töltöttség: " + SAC2 + "%";
            }

            text3 += nev1 + select + select + nev + select + cim + select + TÖRZS + select + cegnev + select + telephely + select + AP + select + PV + select + v + select + HT + select + SZ + select + select;

            richTextBox2.Text = text3;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(richTextBox1.Text))
            {

            }
            else
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(richTextBox2.Text))
            {

            }
            else
            {
                Clipboard.SetText(richTextBox2.SelectedText);
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                Clipboard.SetText(listBox1.SelectedItem.ToString());
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            richTextBox2.Paste();
        }

        private async void lstbtn_Click(object? sender, EventArgs e)
        {
            panel1.Controls.Clear();

            Button lstbtn = (Button)sender;

            string path = lstbtn.Name + ".csv";
            if (File.Exists(path))
            {

                var serializedButtons = File.ReadAllLines(path);
                foreach (var button in serializedButtons)
                {
                    var membersData = button.Split(';');

                    Button dynamicButton = new Button();

                    // Set Button properties
                    var i = 0;
                    dynamicButton.Height = int.Parse(membersData[i++]);
                    dynamicButton.Width = int.Parse(membersData[i++]);
                    dynamicButton.BackColor = Color.FromName(membersData[i++]);
                    dynamicButton.ForeColor = Color.FromName(membersData[i++]);
                    dynamicButton.Name = membersData[i++];
                    dynamicButton.Font = new Font(membersData[i++], float.Parse(membersData[i++]));

                    //Store method name to make it independent of the index (i)
                    var eventMethodName = membersData[i++];

                    //Set the event by method name using reflection, the binding flags allow to access the private method
                    dynamicButton.Click += (_sender, _e) =>
                            GetType().GetMethod(eventMethodName, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, new[] { _sender, _e });

                    var mouseMethodName = membersData[i++];

                    dynamicButton.MouseUp += (_sender, _e) =>
                            GetType().GetMethod(mouseMethodName, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, new[] { _sender, _e });

                    dynamicButton.Dock = DockStyle.Top;
                    dynamicButton.Text = dynamicButton.Name;

                    panel1.Controls.Add(dynamicButton);
                }
            }
        }

        private void lstbtn_MouseUp(object? sender, MouseEventArgs e)
        {
            Button lstbtn = (Button)sender;

            if (e.Button == MouseButtons.Right)
            {
                string width = "";
                string lsttext = "";
                var serializedButtons = File.ReadAllLines(serializedButtonsPath1);
                foreach (var button in serializedButtons)
                {
                    var membersData = button.Split(';');
                    if (lstbtn.Name == membersData[5])
                    {
                        lsttext = membersData[4];
                        width = membersData[1];
                    }
                }

                string split = ";";
                string buttononclick = "lstbtn_Click";
                string mouseup = "lstbtn_MouseUp";

                ap = lstbtn.Name;

                pa = lstbtn.Height + split + width + split + lstbtn.BackColor.Name + split + lstbtn.ForeColor.Name + split + lsttext + split + lstbtn.Name + split + lstbtn.Font.OriginalFontName + split + lstbtn.Font.Size + split + buttononclick /* + split + mousehover + split + mouseleave*/ + split + mouseup;

            }
        }

        string serializedButtonsPath1 = Path.GetDirectoryName(Application.ExecutablePath) + "\\ListsButtons.csv";
        private async Task plus()
        {
            Button lstbtn = new Button();
            lstbtn.Width = 75;
            lstbtn.Height = 24;
            lstbtn.Name = Interaction.InputBox("Add meg az új lista nevét");

            lstbtn.Text = lstbtn.Name;

            Action<object, EventArgs> dynamicButtonOnClick = lstbtn_Click;
            lstbtn.Click += new EventHandler(lstbtn_Click);

            Action<object, MouseEventArgs> dynamicButtonMUp = lstbtn_MouseUp;
            lstbtn.MouseUp += new MouseEventHandler(lstbtn_MouseUp);

            lstbtn.TabStop = false;
            lstbtn.Dock = DockStyle.Top;
            panel4.Controls.Add(lstbtn);

            var lstData = new List<string>();
            lstData.Add(lstbtn.Height.ToString());
            lstData.Add(lstbtn.Width.ToString());
            lstData.Add(lstbtn.BackColor.Name.ToString());
            lstData.Add(lstbtn.ForeColor.Name.ToString());
            lstData.Add(lstbtn.Text);
            lstData.Add(lstbtn.Name);
            lstData.Add(lstbtn.Font.OriginalFontName);
            lstData.Add(lstbtn.Font.Size.ToString());
            lstData.Add(dynamicButtonOnClick.Method.Name);
            lstData.Add(dynamicButtonMUp.Method.Name);

            File.AppendAllText(serializedButtonsPath1, string.Join(";", lstData) + Environment.NewLine);

            ToolStripMenuItem item = new ToolStripMenuItem(lstbtn.Text);

            toolStripMenuItem8.DropDownItems.Add(item);

            item.Click += new EventHandler(item_Click);
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            await plus();
        }

        private void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            string path = item.Text + ".csv";
            TextWriter tsw = new StreamWriter(path, true);
            if (File.Exists(path))
            {
                string createText = r + Environment.NewLine;
                tsw.Write(createText);
                tsw.Close();
            }
            else
            {
                string pt = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + path.ToString();
                string createText = r + Environment.NewLine;
                tsw.Write(createText);
            }
        }

        private void toolStripMenuItem7_Click_1(object sender, EventArgs e)
        {
            string path = "ListsButtons.csv";
            foreach (Control item in panel4.Controls)
            {
                if (item.Name == ap)
                {
                    panel4.Controls.Remove(item);
                    string delpath = item.Name + ".csv";
                    //fájlbol törlés
                    string line = null;
                    string line_to_delete = pa;

                    using (StreamReader reader = new StreamReader(path))
                    {
                        using (StreamWriter writer = new StreamWriter("uj.csv"))
                        {
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (String.Compare(line, line_to_delete) == 0)
                                    continue;

                                writer.WriteLine(line);

                            }
                        }
                    }
                    File.Delete(path);

                    File.Delete(delpath);

                    System.IO.File.Move("uj.csv", "ListsButtons.csv");
                    break; //important step
                }
            }

        }

        public bool val = false;

        private void button13_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = !groupBox1.Visible;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string path = "config/check.txt";
            if (!File.Exists(path))
            {
                File.Create(path);
                TextWriter tw = new StreamWriter(path);

                if (checkBox1.Checked)
                {
                    val = true;
                    tw.Write("XML-ek tagolt betöltése:true");
                    tw.Close();
                }
                else if (!checkBox1.Checked)
                {
                    val = false;
                    tw.Write("XML-ek tagolt betöltése:false");
                    tw.Close();
                }
            }
            else if (File.Exists(path))
            {
                TextWriter tw = new StreamWriter(path);
                if (checkBox1.Checked)
                {
                    val = true;
                    tw.Write("XML-ek tagolt betöltése:true");
                    tw.Close();
                }
                else if (!checkBox1.Checked)
                {
                    val = false;
                    tw.Write("XML-ek tagolt betöltése:false");
                    tw.Close();
                }
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            //Process proc = new Process();
            //proc.StartInfo.UseShellExecute = true;
            //proc.StartInfo.FileName = "rendelet.pdf";
            //proc.Start();

        }

        public class Part1 : IEquatable<Part1>, IComparable<Part1>
        {
            public string item { get; set; }

            public string desc { get; set; }

            public override string ToString()
            {
                return item;
            }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                Part1 objAsPart = obj as Part1;
                if (objAsPart == null) return false;
                else return Equals(objAsPart);
            }
            public int SortByNameAscending(string name1, string name2)
            {

                return name1.CompareTo(name2);
            }

            // Default comparer for Part type.
            public int CompareTo(Part1 comparePart)
            {
                // A null value means that this object is greater.
                if (comparePart == null)
                    return 1;

                else
                    return this.desc.CompareTo(comparePart.desc);
            }

            public bool Equals(Part1 other)
            {
                if (other == null) return false;
                return (this.desc.Equals(other.desc));
            }
            // Should also override == and != operators.
        }

        List<Part1> seft = new List<Part1>();

        public int ai = 0;
        public string res;
        private void button14_Click(object sender, EventArgs e)
        {
            seft.Clear();
            richTextBox2.Clear();
            ai = 0;
            res = "";
            termlst.Clear();

            var path = "hibacode.txt";
            if (File.Exists(path) && richTextBox1.Text != null)
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        string str;
                        string[] strArray;
                        str = sr.ReadLine();

                        strArray = str.Split('-');
                        Part1 currentBook = new Part1();
                        currentBook.item = strArray[0];
                        currentBook.desc = strArray[1];


                        seft.Add(currentBook);
                    }
                }

                if (ai == seft.Count)
                {
                    ai = 0;
                }
                
                foreach (var item in seft)
                {
                    Regex regex1 = new Regex(seft[ai].item, RegexOptions.None);

                    RichTextBox rtbMain = richTextBox1;
                    rtbMain.SelectAll();
                    rtbMain.SelectionColor = Color.Black;
                    rtbMain.SelectionBackColor = Color.White;
                    Regex regex = new Regex(regex1.ToString(), RegexOptions.Compiled);
                    MatchCollection matches = regex.Matches(rtbMain.Text);

                    if (matches.Count > 0)
                    {
                        foreach (Match m in matches)
                        {
                            rtbMain.Select(m.Index, m.Length);
                            rtbMain.SelectionBackColor = Color.Red;
                        }
                    }

                    if (val1 == true)
                    {
                        res += matches.Count + ": " + seft[ai].item + "-" + seft[ai].desc + "\n";
                    }

                    richTextBox2.Text += matches.Count + ": " + seft[ai].desc + "\n";


                    if (matches.Count > 0)
                    {
                        termlst.Add(matches.Count.ToString());
                    }
                    ai++;
                }

                int index = 0;
                foreach (var item in termlst)
                { 
                  richTextBox2.Find(item, index, richTextBox2.TextLength, RichTextBoxFinds.None);
                  richTextBox2.SelectionBackColor = Color.GreenYellow;
                  index = richTextBox2.Text.IndexOf(item, index) + 1;
                }
            }
        }

        public Color richTextBoxColor
        {
            get { return richTextBox2.SelectionBackColor; }
        }

        public string txt;
        private void button15_Click(object sender, EventArgs e)
        {
            txt = "";
            Form2 f2 = new Form2();
            
            if (checkBox2.Checked)
            {
                txt = res;
            }
            else if (!checkBox2.Checked)
            {
                txt = richTextBox2.Text;
            }
            f2.ShowDialog();
            
        }

        public bool val1 = false;

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            string path = "config/check2.txt";
            if (!File.Exists(path))
            {
                File.Create(path);
                TextWriter tw = new StreamWriter(path);

                if (checkBox2.Checked)
                {
                    val1 = true;
                    tw.Write("Vizsgálat megjelenitése máskép:true");
                    tw.Close();
                }
                else if (!checkBox2.Checked)
                {
                    val1 = false;
                    tw.Write("Vizsgálat megjelenitése máskép:false");
                    tw.Close();
                }
            }
            else if (File.Exists(path))
            {
                TextWriter tw = new StreamWriter(path);
                if (checkBox2.Checked)
                {
                    val1 = true;
                    tw.Write("Vizsgálat megjelenitése máskép:true");
                    tw.Close();
                }
                else if (!checkBox2.Checked)
                {
                    val1 = false;
                    tw.Write("Vizsgálat megjelenitése máskép:false");
                    tw.Close();
                }
            }
        }

        public string sourceDirectory;

        void bejar(string path)
        {

            var txtFiles = Directory.EnumerateFiles(path, "*.xml");

            foreach (string currentFile in txtFiles)
            {
                string fileName = currentFile[(path.Length + 1)..];
                listBox1.Items.Add(currentFile);
            }

            var txtFiles1 = Directory.EnumerateFiles(path, "*.txt");

            foreach (string currentFile in txtFiles1)
            {
                string fileName = currentFile[(path.Length + 1)..];
                listBox1.Items.Add(currentFile);
            }

            var dirs = Directory.EnumerateDirectories(path);

            foreach (string dir in dirs)
            {
                bejar(dir);
            }

        }

        private void button16_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                sourceDirectory = dialog.SelectedPath;
                listBox1.Items.Clear();
                bejar(sourceDirectory);
            }

            string text = "";
            string select1 = "\r\n-------------------------------------------------------------------------------------------------------\r\n\r\n";
            foreach (string item in listBox1.Items)
            {
                if (Path.GetExtension(item.ToString()) == ".xml" && val == true)
                {
                    var xml = XElement.Parse(File.ReadAllText(item.ToString()));
                    string strtext = xml.ToString();
                    text += strtext + select1;
                }
                else
                {
                    text += File.ReadAllText(item.ToString()) + select1;
                }
            }
            richTextBox1.Text = text;
        }
    }
}
