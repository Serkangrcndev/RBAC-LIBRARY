namespace Seker_kutuphane
{
    partial class sifreBelirle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sifreBelirle));
            panel1 = new Panel();
            label1 = new Label();
            textBox1 = new TextBox();
            linkRegister = new LinkLabel();
            pictureBox1 = new PictureBox();
            label7 = new Label();
            label8 = new Label();
            label3 = new Label();
            textBox3 = new TextBox();
            button1 = new Button();
            btnCikis = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(linkRegister);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(134, 39);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(427, 406);
            panel1.TabIndex = 2;
            panel1.Paint += panel1_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(46, 213);
            label1.Name = "label1";
            label1.Size = new Size(95, 15);
            label1.TabIndex = 33;
            label1.Text = "Yeni Şifre Tekrar*";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(46, 230);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(350, 23);
            textBox1.TabIndex = 34;
            // 
            // linkRegister
            // 
            linkRegister.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            linkRegister.LinkColor = Color.FromArgb(76, 175, 80);
            linkRegister.Location = new Point(89, 316);
            linkRegister.Name = "linkRegister";
            linkRegister.Size = new Size(262, 22);
            linkRegister.TabIndex = 32;
            linkRegister.TabStop = true;
            linkRegister.Text = "Giriş Sayfasına Dön";
            linkRegister.TextAlign = ContentAlignment.MiddleCenter;
            linkRegister.LinkClicked += linkRegister_LinkClicked;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(175, 14);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(85, 64);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 31;
            pictureBox1.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(99, 80);
            label7.Name = "label7";
            label7.Size = new Size(238, 37);
            label7.TabIndex = 15;
            label7.Text = "Şifre Sıfırlama";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label8.ForeColor = Color.Gray;
            label8.Location = new Point(72, 147);
            label8.Name = "label8";
            label8.Size = new Size(304, 14);
            label8.TabIndex = 16;
            label8.Text = "TC kimlik numaranız doğrulandı. Yeni şifrenizi oluşturabilirsiniz.";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(48, 166);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 21;
            label3.Text = "Yeni Şİfre*";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(48, 184);
            textBox3.Margin = new Padding(3, 2, 3, 2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(350, 23);
            textBox3.TabIndex = 22;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(76, 175, 80);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button1.ForeColor = Color.White;
            button1.Location = new Point(48, 272);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(349, 30);
            button1.TabIndex = 29;
            button1.Text = "Sıfırla";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // btnCikis
            // 
            btnCikis.BackColor = Color.Red;
            btnCikis.FlatStyle = FlatStyle.Popup;
            btnCikis.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnCikis.Location = new Point(663, 1);
            btnCikis.Margin = new Padding(3, 2, 3, 2);
            btnCikis.Name = "btnCikis";
            btnCikis.Size = new Size(30, 23);
            btnCikis.TabIndex = 3;
            btnCikis.Text = "X";
            btnCikis.UseVisualStyleBackColor = false;
            btnCikis.Click += btnCikis_Click;
            // 
            // sifreBelirle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Green;
            ClientSize = new Size(695, 484);
            Controls.Add(btnCikis);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "sifreBelirle";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "sifreBelirle";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private LinkLabel linkRegister;
        private PictureBox pictureBox1;
        private Label label7;
        private Label label8;
        private Label label3;
        private TextBox textBox3;
        private Button button1;
        private Label label1;
        private TextBox textBox1;
        private Button btnCikis;
    }
}