namespace TXBeditor
{
    partial class ImglibAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImglibAbout));
            groupBox1 = new GroupBox();
            label5 = new Label();
            license = new TextBox();
            label4 = new Label();
            a = new Label();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label3 = new Label();
            rainbowlink = new LinkLabel();
            button1 = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.None;
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(license);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(a);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(rainbowlink);
            groupBox1.Location = new Point(14, 8);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(382, 309);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(30, 276);
            label5.Name = "label5";
            label5.Size = new Size(324, 15);
            label5.TabIndex = 10;
            label5.Text = "palette and it just makes it a pain to edit 80% of the textures.";
            // 
            // license
            // 
            license.Location = new Point(12, 102);
            license.Multiline = true;
            license.Name = "license";
            license.ScrollBars = ScrollBars.Vertical;
            license.Size = new Size(361, 128);
            license.TabIndex = 5;
            license.Text = "aaa";
            license.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(19, 261);
            label4.Name = "label4";
            label4.Size = new Size(347, 15);
            label4.TabIndex = 9;
            label4.Text = "count to always be one, since the game never utilizes the second";
            // 
            // a
            // 
            a.AutoSize = true;
            a.Location = new Point(23, 246);
            a.Name = "a";
            a.Size = new Size(342, 15);
            a.TabIndex = 8;
            a.Text = "Multi-clut TIM2 editing has been disabled by forcing the palette";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(81, 41);
            label2.Name = "label2";
            label2.Size = new Size(214, 15);
            label2.TabIndex = 7;
            label2.Text = "Rainbow: a texture format converter";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(41, 50);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(34, 34);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(364, 15);
            label1.TabIndex = 6;
            label1.Text = "The utilized image library for TIM2 view and editing is sourced from:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(81, 56);
            label3.Name = "label3";
            label3.Size = new Size(247, 15);
            label3.TabIndex = 3;
            label3.Text = "Copyright (c) 2021+ Marco (Phoenix) Calautti";
            // 
            // rainbowlink
            // 
            rainbowlink.AutoSize = true;
            rainbowlink.Location = new Point(81, 71);
            rainbowlink.Name = "rainbowlink";
            rainbowlink.Size = new Size(243, 15);
            rainbowlink.TabIndex = 4;
            rainbowlink.TabStop = true;
            rainbowlink.Text = "https://github.com/marco-calautti/Rainbow";
            rainbowlink.LinkClicked += linkLabel1_LinkClicked;
            // 
            // button1
            // 
            button1.Location = new Point(164, 326);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 9;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // ImglibAbout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(408, 359);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ImglibAbout";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "About Rainbow ImgLib";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label5;
        private TextBox license;
        private Label label4;
        private Label a;
        private Label label2;
        private PictureBox pictureBox1;
        private Label label1;
        private Label label3;
        private LinkLabel rainbowlink;
        private Button button1;
    }
}