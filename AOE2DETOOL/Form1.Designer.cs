namespace AOE2DETOOL
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            button1 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            textBox1 = new TextBox();
            listBox1 = new ListBox();
            textBox2 = new TextBox();
            userControl11 = new UserControl1();
            userControl12 = new UserControl1();
            userControl13 = new UserControl1();
            userControl14 = new UserControl1();
            userControl15 = new UserControl1();
            userControl16 = new UserControl1();
            userControl17 = new UserControl1();
            userControl18 = new UserControl1();
            button2 = new Button();
            label1 = new Label();
            textGameTime = new TextBox();
            textMyselfName = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(2768, 17);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(172, 108);
            button1.TabIndex = 0;
            button1.Text = "データ得";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 2000;
            timer1.Tick += timer1_Tick;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(15, 606);
            textBox1.Margin = new Padding(4);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Both;
            textBox1.Size = new Size(1197, 521);
            textBox1.TabIndex = 1;
            textBox1.WordWrap = false;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 21;
            listBox1.Location = new Point(15, 80);
            listBox1.Margin = new Padding(4);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(1197, 508);
            listBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(2350, 146);
            textBox2.Margin = new Padding(4);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ScrollBars = ScrollBars.Both;
            textBox2.Size = new Size(622, 981);
            textBox2.TabIndex = 3;
            // 
            // userControl11
            // 
            userControl11.Location = new Point(1236, 17);
            userControl11.Margin = new Padding(5, 6, 5, 6);
            userControl11.Name = "userControl11";
            userControl11.Size = new Size(555, 279);
            userControl11.TabIndex = 4;
            // 
            // userControl12
            // 
            userControl12.Location = new Point(1236, 279);
            userControl12.Margin = new Padding(5, 6, 5, 6);
            userControl12.Name = "userControl12";
            userControl12.Size = new Size(555, 279);
            userControl12.TabIndex = 5;
            // 
            // userControl13
            // 
            userControl13.Location = new Point(1236, 535);
            userControl13.Margin = new Padding(5, 6, 5, 6);
            userControl13.Name = "userControl13";
            userControl13.Size = new Size(555, 279);
            userControl13.TabIndex = 6;
            // 
            // userControl14
            // 
            userControl14.Location = new Point(1236, 822);
            userControl14.Margin = new Padding(5, 6, 5, 6);
            userControl14.Name = "userControl14";
            userControl14.Size = new Size(555, 279);
            userControl14.TabIndex = 7;
            // 
            // userControl15
            // 
            userControl15.Location = new Point(1799, 17);
            userControl15.Margin = new Padding(5, 6, 5, 6);
            userControl15.Name = "userControl15";
            userControl15.Size = new Size(555, 279);
            userControl15.TabIndex = 8;
            // 
            // userControl16
            // 
            userControl16.Location = new Point(1799, 279);
            userControl16.Margin = new Padding(5, 6, 5, 6);
            userControl16.Name = "userControl16";
            userControl16.Size = new Size(555, 279);
            userControl16.TabIndex = 9;
            // 
            // userControl17
            // 
            userControl17.Location = new Point(1799, 535);
            userControl17.Margin = new Padding(5, 6, 5, 6);
            userControl17.Name = "userControl17";
            userControl17.Size = new Size(555, 279);
            userControl17.TabIndex = 10;
            // 
            // userControl18
            // 
            userControl18.Location = new Point(1799, 822);
            userControl18.Margin = new Padding(5, 6, 5, 6);
            userControl18.Name = "userControl18";
            userControl18.Size = new Size(555, 279);
            userControl18.TabIndex = 11;
            // 
            // button2
            // 
            button2.Location = new Point(1131, 17);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Size = new Size(82, 45);
            button2.TabIndex = 12;
            button2.Text = "更新";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2377, 27);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(45, 21);
            label1.TabIndex = 13;
            label1.Text = "時間:";
            // 
            // textGameTime
            // 
            textGameTime.Location = new Point(2429, 22);
            textGameTime.Margin = new Padding(4);
            textGameTime.Name = "textGameTime";
            textGameTime.Size = new Size(202, 29);
            textGameTime.TabIndex = 14;
            // 
            // textMyselfName
            // 
            textMyselfName.Location = new Point(170, 24);
            textMyselfName.Name = "textMyselfName";
            textMyselfName.Size = new Size(265, 29);
            textMyselfName.TabIndex = 15;
            textMyselfName.Text = "Yoshix";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2989, 1145);
            Controls.Add(textMyselfName);
            Controls.Add(textGameTime);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(userControl18);
            Controls.Add(userControl17);
            Controls.Add(userControl16);
            Controls.Add(userControl15);
            Controls.Add(userControl14);
            Controls.Add(userControl13);
            Controls.Add(userControl12);
            Controls.Add(userControl11);
            Controls.Add(textBox2);
            Controls.Add(listBox1);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private System.Windows.Forms.Timer timer1;
        private TextBox textBox1;
        private ListBox listBox1;
        private TextBox textBox2;
        private UserControl1 userControl11;
        private UserControl1 userControl12;
        private UserControl1 userControl13;
        private UserControl1 userControl14;
        private UserControl1 userControl15;
        private UserControl1 userControl16;
        private UserControl1 userControl17;
        private UserControl1 userControl18;
        private Button button2;
        private Label label1;
        private TextBox textGameTime;
        private TextBox textMyselfName;
    }
}