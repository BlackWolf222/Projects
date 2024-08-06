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