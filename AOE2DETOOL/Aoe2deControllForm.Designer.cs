namespace AOE2DETOOL
{
    partial class Aoe2deControllForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(1351, 32);
            button1.Name = "button1";
            button1.Size = new Size(112, 49);
            button1.TabIndex = 0;
            button1.Text = "非表示";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(1486, 32);
            button2.Name = "button2";
            button2.Size = new Size(112, 49);
            button2.TabIndex = 1;
            button2.Text = "監視開始";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(1616, 32);
            button3.Name = "button3";
            button3.Size = new Size(112, 49);
            button3.TabIndex = 2;
            button3.Text = "終了";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(1750, 32);
            button4.Name = "button4";
            button4.Size = new Size(112, 49);
            button4.TabIndex = 3;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label1
            // 
            label1.BackColor = Color.Gray;
            label1.Font = new Font("Yu Gothic UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(1888, 32);
            label1.Name = "label1";
            label1.Size = new Size(74, 49);
            label1.TabIndex = 4;
            label1.Text = "敵前";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.BackColor = Color.Gray;
            label2.Font = new Font("Yu Gothic UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(1979, 32);
            label2.Name = "label2";
            label2.Size = new Size(74, 49);
            label2.TabIndex = 5;
            label2.Text = "敵後";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.BackColor = Color.Gray;
            label3.Font = new Font("Yu Gothic UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.Window;
            label3.Location = new Point(2069, 32);
            label3.Name = "label3";
            label3.Size = new Size(74, 49);
            label3.TabIndex = 6;
            label3.Text = "＿＿";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Aoe2deControllForm
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2248, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Aoe2deControllForm";
            Text = "Aoe2deControllForm";
            FormClosed += Aoe2deControllForm_FormClosed;
            Load += Aoe2deControllForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}