namespace Seker_kutuphane
{
    partial class sifre_yenileme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sifre_yenileme));
            panel1 = new Panel();
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
            panel1.Controls.Add(linkRegister);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(156, 57);
            panel1.Name = "panel1";
            panel1.Size = new Size(488, 542);
            panel1.TabIndex = 1;
            panel1.Paint += panel1_Paint;
            // 
            // linkRegister
            // 
            linkRegister.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            linkRegister.LinkColor = Color.FromArgb(76, 175, 80);
            linkRegister.Location = new Point(94, 406);
            linkRegister.Name = "linkRegister";
            linkRegister.Size = new Size(300, 30);
            linkRegister.TabIndex = 32;
            linkRegister.TabStop = true;
            linkRegister.Text = "Giriş Sayfasına Dön";
            linkRegister.TextAlign = ContentAlignment.MiddleCenter;
            linkRegister.LinkClicked += linkRegister_LinkClicked;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(200, 18);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(97, 85);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 31;
            pictureBox1.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(94, 128);
            label7.Name = "label7";
            label7.Size = new Size(327, 46);
            label7.TabIndex = 15;
            label7.Text = "Şifremi Unuttum";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            label7.Click += label7_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Arial", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label8.ForeColor = Color.Gray;
            label8.Location = new Point(82, 196);
            label8.Name = "label8";
            label8.Size = new Size(349, 16);
            label8.TabIndex = 16;
            label8.Text = "TC kimlik numaranızı doğrulayarak şifrenizi sıfırlayabilirsiniz";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(55, 255);
            label3.Name = "label3";
            label3.Size = new Size(100, 20);
            label3.TabIndex = 21;
            label3.Text = "TC Kimlik No*";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(55, 278);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(399, 27);
            textBox3.TabIndex = 22;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(76, 175, 80);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button1.ForeColor = Color.White;
            button1.Location = new Point(55, 334);
            button1.Name = "button1";
            button1.Size = new Size(399, 40);
            button1.TabIndex = 29;
            button1.Text = "TC Doğrula";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // btnCikis
            // 
            btnCikis.BackColor = Color.Red;
            btnCikis.FlatStyle = FlatStyle.Popup;
            btnCikis.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnCikis.Location = new Point(745, 0);
            btnCikis.Name = "btnCikis";
            btnCikis.Size = new Size(34, 31);
            btnCikis.TabIndex = 2;
            btnCikis.Text = "X";
            btnCikis.UseVisualStyleBackColor = false;
            btnCikis.Click += btnCikis_Click;
            // 
            // sifre_yenileme
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(76, 175, 80);
            ClientSize = new Size(779, 650);
            Controls.Add(btnCikis);
            Controls.Add(panel1);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.None;
            Name = "sifre_yenileme";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "sifre_yenileme";
            Load += sifre_yenileme_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Label label7;
        private Label label8;
        private Label label3;
        private TextBox textBox3;
        private Button button1;
        private LinkLabel linkRegister;
        private Button btnCikis;
    }
}