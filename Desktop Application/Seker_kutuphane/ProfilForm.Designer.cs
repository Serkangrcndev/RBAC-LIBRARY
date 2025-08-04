namespace Seker_kutuphane
{
    partial class ProfilForm
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
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Label lblAd;
        private System.Windows.Forms.TextBox txtAd;
        private System.Windows.Forms.Label lblSoyad;
        private System.Windows.Forms.TextBox txtSoyad;
        private System.Windows.Forms.Label lblTC;
        private System.Windows.Forms.TextBox txtTC;
        private System.Windows.Forms.Label lblTelefon;
        private System.Windows.Forms.TextBox txtTelefon;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.Button btnGuncelle;
        private System.Windows.Forms.Button btnIptal;
        private System.Windows.Forms.Button btnSifreDegistir;
        private System.Windows.Forms.Button btnYenile;

        private void InitializeComponent()
        {
            panelMain = new Panel();
            lblBaslik = new Label();
            lblAd = new Label();
            txtAd = new TextBox();
            lblSoyad = new Label();
            txtSoyad = new TextBox();
            lblTC = new Label();
            txtTC = new TextBox();
            lblTelefon = new Label();
            txtTelefon = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblRol = new Label();
            btnGuncelle = new Button();
            btnIptal = new Button();
            btnSifreDegistir = new Button();
            btnYenile = new Button();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(245, 245, 245);
            panelMain.Controls.Add(lblBaslik);
            panelMain.Controls.Add(lblAd);
            panelMain.Controls.Add(txtAd);
            panelMain.Controls.Add(lblSoyad);
            panelMain.Controls.Add(txtSoyad);
            panelMain.Controls.Add(lblTC);
            panelMain.Controls.Add(txtTC);
            panelMain.Controls.Add(lblTelefon);
            panelMain.Controls.Add(txtTelefon);
            panelMain.Controls.Add(lblEmail);
            panelMain.Controls.Add(txtEmail);
            panelMain.Controls.Add(lblRol);
            panelMain.Controls.Add(btnGuncelle);
            panelMain.Controls.Add(btnIptal);
            panelMain.Controls.Add(btnSifreDegistir);
            panelMain.Controls.Add(btnYenile);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(600, 520);
            panelMain.TabIndex = 0;
            // 
            // lblBaslik
            // 
            lblBaslik.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(0, 128, 0);
            lblBaslik.Location = new Point(20, 20);
            lblBaslik.Name = "lblBaslik";
            lblBaslik.Size = new Size(560, 40);
            lblBaslik.TabIndex = 0;
            lblBaslik.Text = "Profil Bilgileri";
            lblBaslik.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblAd
            // 
            lblAd.AutoSize = true;
            lblAd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAd.ForeColor = Color.FromArgb(0, 128, 0);
            lblAd.Location = new Point(50, 79);
            lblAd.Name = "lblAd";
            lblAd.Size = new Size(32, 19);
            lblAd.TabIndex = 1;
            lblAd.Text = "Ad:";
            // 
            // txtAd
            // 
            txtAd.Font = new Font("Segoe UI", 10F);
            txtAd.Location = new Point(200, 76);
            txtAd.Name = "txtAd";
            txtAd.Size = new Size(300, 25);
            txtAd.TabIndex = 2;
            // 
            // lblSoyad
            // 
            lblSoyad.AutoSize = true;
            lblSoyad.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSoyad.ForeColor = Color.FromArgb(0, 128, 0);
            lblSoyad.Location = new Point(50, 119);
            lblSoyad.Name = "lblSoyad";
            lblSoyad.Size = new Size(55, 19);
            lblSoyad.TabIndex = 3;
            lblSoyad.Text = "Soyad:";
            // 
            // txtSoyad
            // 
            txtSoyad.Font = new Font("Segoe UI", 10F);
            txtSoyad.Location = new Point(200, 116);
            txtSoyad.Name = "txtSoyad";
            txtSoyad.Size = new Size(300, 25);
            txtSoyad.TabIndex = 4;
            // 
            // lblTC
            // 
            lblTC.AutoSize = true;
            lblTC.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTC.ForeColor = Color.FromArgb(0, 128, 0);
            lblTC.Location = new Point(50, 159);
            lblTC.Name = "lblTC";
            lblTC.Size = new Size(100, 19);
            lblTC.TabIndex = 5;
            lblTC.Text = "TC Kimlik No:";
            // 
            // txtTC
            // 
            txtTC.Font = new Font("Segoe UI", 10F);
            txtTC.Location = new Point(200, 156);
            txtTC.Name = "txtTC";
            txtTC.Size = new Size(300, 25);
            txtTC.TabIndex = 6;
            // 
            // lblTelefon
            // 
            lblTelefon.AutoSize = true;
            lblTelefon.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTelefon.ForeColor = Color.FromArgb(0, 128, 0);
            lblTelefon.Location = new Point(50, 199);
            lblTelefon.Name = "lblTelefon";
            lblTelefon.Size = new Size(62, 19);
            lblTelefon.TabIndex = 7;
            lblTelefon.Text = "Telefon:";
            // 
            // txtTelefon
            // 
            txtTelefon.Font = new Font("Segoe UI", 10F);
            txtTelefon.Location = new Point(200, 196);
            txtTelefon.Name = "txtTelefon";
            txtTelefon.Size = new Size(300, 25);
            txtTelefon.TabIndex = 8;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmail.ForeColor = Color.FromArgb(0, 128, 0);
            lblEmail.Location = new Point(50, 239);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(55, 19);
            lblEmail.TabIndex = 9;
            lblEmail.Text = "E-mail:";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(200, 236);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 25);
            txtEmail.TabIndex = 10;
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRol.ForeColor = Color.FromArgb(0, 128, 0);
            lblRol.Location = new Point(50, 279);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(65, 19);
            lblRol.TabIndex = 11;
            lblRol.Text = "Rol: Üye";
            // 
            // btnGuncelle
            // 
            btnGuncelle.BackColor = Color.FromArgb(76, 175, 80);
            btnGuncelle.FlatAppearance.BorderSize = 0;
            btnGuncelle.FlatStyle = FlatStyle.Flat;
            btnGuncelle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGuncelle.ForeColor = Color.White;
            btnGuncelle.Location = new Point(200, 319);
            btnGuncelle.Name = "btnGuncelle";
            btnGuncelle.Size = new Size(120, 40);
            btnGuncelle.TabIndex = 12;
            btnGuncelle.Text = "Güncelle";
            btnGuncelle.UseVisualStyleBackColor = false;
            btnGuncelle.Click += btnGuncelle_Click;
            // 
            // btnIptal
            // 
            btnIptal.BackColor = Color.FromArgb(158, 158, 158);
            btnIptal.FlatAppearance.BorderSize = 0;
            btnIptal.FlatStyle = FlatStyle.Flat;
            btnIptal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnIptal.ForeColor = Color.White;
            btnIptal.Location = new Point(340, 319);
            btnIptal.Name = "btnIptal";
            btnIptal.Size = new Size(120, 40);
            btnIptal.TabIndex = 13;
            btnIptal.Text = "İptal";
            btnIptal.UseVisualStyleBackColor = false;
            btnIptal.Click += btnIptal_Click;
            // 
            // btnSifreDegistir
            // 
            btnSifreDegistir.BackColor = Color.FromArgb(33, 150, 243);
            btnSifreDegistir.FlatAppearance.BorderSize = 0;
            btnSifreDegistir.FlatStyle = FlatStyle.Flat;
            btnSifreDegistir.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSifreDegistir.ForeColor = Color.White;
            btnSifreDegistir.Location = new Point(200, 379);
            btnSifreDegistir.Name = "btnSifreDegistir";
            btnSifreDegistir.Size = new Size(260, 40);
            btnSifreDegistir.TabIndex = 14;
            btnSifreDegistir.Text = "Şifre Değiştir";
            btnSifreDegistir.UseVisualStyleBackColor = false;
            btnSifreDegistir.Click += btnSifreDegistir_Click;
            // 
            // btnYenile
            // 
            btnYenile.BackColor = Color.FromArgb(76, 175, 80);
            btnYenile.FlatAppearance.BorderSize = 0;
            btnYenile.FlatStyle = FlatStyle.Flat;
            btnYenile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnYenile.ForeColor = Color.White;
            btnYenile.Location = new Point(492, 473);
            btnYenile.Name = "btnYenile";
            btnYenile.Size = new Size(88, 35);
            btnYenile.TabIndex = 15;
            btnYenile.Text = "🔄 Yenile";
            btnYenile.UseVisualStyleBackColor = false;
            btnYenile.Click += btnYenile_Click;
            // 
            // ProfilForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 520);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProfilForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Profil Bilgileri";
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
} 