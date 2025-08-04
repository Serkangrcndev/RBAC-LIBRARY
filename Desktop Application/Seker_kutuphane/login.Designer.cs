namespace Seker_kutuphane
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            panel1 = new Panel();
            label3 = new Label();
            txtTC = new TextBox();
            label4 = new Label();
            btnGirisYap = new Button();
            pictureBox1 = new PictureBox();
            labelTitle = new Label();
            panelLine = new Panel();
            labelDesc = new Label();
            panelDivider = new Panel();
            linkRegister = new LinkLabel();
            linkForgot = new LinkLabel();
            btnCikis = new Button();
            txtSifre = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtTC);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(txtSifre);
            panel1.Controls.Add(btnGirisYap);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(labelTitle);
            panel1.Controls.Add(panelLine);
            panel1.Controls.Add(labelDesc);
            panel1.Controls.Add(panelDivider);
            panel1.Controls.Add(linkRegister);
            panel1.Controls.Add(linkForgot);
            panel1.Location = new Point(132, 84);
            panel1.Name = "panel1";
            panel1.Size = new Size(551, 583);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(80, 242);
            label3.Name = "label3";
            label3.Size = new Size(104, 20);
            label3.TabIndex = 31;
            label3.Text = "TC Kimlik No *";
            label3.Click += label3_Click;
            // 
            // txtTC
            // 
            txtTC.Location = new Point(80, 265);
            txtTC.Name = "txtTC";
            txtTC.Size = new Size(400, 27);
            txtTC.TabIndex = 32;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(80, 309);
            label4.Name = "label4";
            label4.Size = new Size(49, 20);
            label4.TabIndex = 33;
            label4.Text = "Şifre *";
            // 
            // btnGirisYap
            // 
            btnGirisYap.BackColor = Color.FromArgb(76, 175, 80);
            btnGirisYap.FlatStyle = FlatStyle.Flat;
            btnGirisYap.ForeColor = Color.White;
            btnGirisYap.Location = new Point(80, 383);
            btnGirisYap.Name = "btnGirisYap";
            btnGirisYap.Size = new Size(400, 40);
            btnGirisYap.TabIndex = 30;
            btnGirisYap.Text = "Giriş Yap";
            btnGirisYap.UseVisualStyleBackColor = false;
            btnGirisYap.Click += btnGirisYap_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(231, 23);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(114, 95);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelTitle.ForeColor = Color.FromArgb(33, 37, 41);
            labelTitle.Location = new Point(195, 121);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(185, 54);
            labelTitle.TabIndex = 1;
            labelTitle.Text = "Giriş Yap";
            // 
            // panelLine
            // 
            panelLine.BackColor = Color.FromArgb(76, 175, 80);
            panelLine.Location = new Point(250, 187);
            panelLine.Name = "panelLine";
            panelLine.Size = new Size(80, 4);
            panelLine.TabIndex = 2;
            // 
            // labelDesc
            // 
            labelDesc.AutoSize = true;
            labelDesc.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            labelDesc.ForeColor = Color.Gray;
            labelDesc.Location = new Point(193, 194);
            labelDesc.Name = "labelDesc";
            labelDesc.Size = new Size(202, 17);
            labelDesc.TabIndex = 3;
            labelDesc.Text = "Kütüphane sistemine hoş geldiniz";
            // 
            // panelDivider
            // 
            panelDivider.BackColor = Color.Gainsboro;
            panelDivider.Location = new Point(80, 450);
            panelDivider.Name = "panelDivider";
            panelDivider.Size = new Size(400, 1);
            panelDivider.TabIndex = 7;
            // 
            // linkRegister
            // 
            linkRegister.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            linkRegister.LinkColor = Color.FromArgb(76, 175, 80);
            linkRegister.Location = new Point(140, 470);
            linkRegister.Name = "linkRegister";
            linkRegister.Size = new Size(300, 30);
            linkRegister.TabIndex = 8;
            linkRegister.TabStop = true;
            linkRegister.Text = "Hesabınız yok mu? Kayıt olun";
            linkRegister.TextAlign = ContentAlignment.MiddleCenter;
            linkRegister.LinkClicked += linkRegister_LinkClicked;
            // 
            // linkForgot
            // 
            linkForgot.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            linkForgot.LinkColor = Color.FromArgb(76, 175, 80);
            linkForgot.Location = new Point(195, 500);
            linkForgot.Name = "linkForgot";
            linkForgot.Size = new Size(200, 30);
            linkForgot.TabIndex = 9;
            linkForgot.TabStop = true;
            linkForgot.Text = "Şifrenizi mi unuttunuz?";
            linkForgot.TextAlign = ContentAlignment.MiddleCenter;
            linkForgot.LinkClicked += linkForgot_LinkClicked;
            // 
            // btnCikis
            // 
            btnCikis.BackColor = Color.Red;
            btnCikis.FlatStyle = FlatStyle.Popup;
            btnCikis.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnCikis.Location = new Point(765, 3);
            btnCikis.Name = "btnCikis";
            btnCikis.Size = new Size(34, 31);
            btnCikis.TabIndex = 1;
            btnCikis.Text = "X";
            btnCikis.UseVisualStyleBackColor = false;
            btnCikis.Click += btnCikis_Click;
            // 
            // txtSifre
            // 
            txtSifre.Location = new Point(80, 329);
            txtSifre.Name = "txtSifre";
            txtSifre.PasswordChar = '*';
            txtSifre.Size = new Size(400, 27);
            txtSifre.TabIndex = 34;
            // 
            // Login
            // 
            BackColor = Color.FromArgb(76, 175, 80);
            ClientSize = new Size(801, 750);
            Controls.Add(btnCikis);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelLine;
        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.Panel panelDivider;
        private System.Windows.Forms.LinkLabel linkRegister;
        private System.Windows.Forms.LinkLabel linkForgot;
        private Button btnGirisYap;
        private Label label3;
        private TextBox txtTC;
        private Label label4;
        private Button btnCikis;
        private TextBox txtSifre;
    }
}