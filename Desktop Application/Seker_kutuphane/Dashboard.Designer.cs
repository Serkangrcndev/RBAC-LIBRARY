namespace Seker_kutuphane
{
    partial class Dashboard
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
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnKitaplar;
        private System.Windows.Forms.Button btnKitaplarim;
        private System.Windows.Forms.Button btnUyeler;
        private System.Windows.Forms.Button btnEmanetler;
        private System.Windows.Forms.Button btnRaporlar;
        private System.Windows.Forms.Button btnYonetim;
        private System.Windows.Forms.Button btnCikis;
        private System.Windows.Forms.Button btnKitapEkle;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Label lblKullanici;
        private void InitializeComponent()
        {
            panelMenu = new Panel();
            btnKitaplar = new Button();
            btnKitaplarim = new Button();
            btnUyeler = new Button();
            btnEmanetler = new Button();
            btnRaporlar = new Button();
            btnYonetim = new Button();
            btnCikis = new Button();
            btnKitapEkle = new Button();
            lblBaslik = new Label();
            lblKullanici = new Label();
            panelMenu.SuspendLayout();
            SuspendLayout();
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(0, 128, 0);
            panelMenu.Controls.Add(btnKitaplar);
            panelMenu.Controls.Add(btnKitaplarim);
            panelMenu.Controls.Add(btnUyeler);
            panelMenu.Controls.Add(btnEmanetler);
            panelMenu.Controls.Add(btnRaporlar);
            panelMenu.Controls.Add(btnYonetim);
            panelMenu.Controls.Add(btnCikis);
            panelMenu.Controls.Add(btnKitapEkle);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(200, 500);
            panelMenu.TabIndex = 2;
            // 
            // btnKitaplar
            // 
            btnKitaplar.BackColor = Color.FromArgb(76, 175, 80);
            btnKitaplar.FlatAppearance.BorderSize = 0;
            btnKitaplar.FlatStyle = FlatStyle.Flat;
            btnKitaplar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnKitaplar.ForeColor = Color.White;
            btnKitaplar.Location = new Point(10, 27);
            btnKitaplar.Name = "btnKitaplar";
            btnKitaplar.Size = new Size(180, 45);
            btnKitaplar.TabIndex = 0;
            btnKitaplar.Text = "Kitap Ara";
            btnKitaplar.UseVisualStyleBackColor = false;
            btnKitaplar.Click += btnKitaplar_Click;
            // 
            // btnKitaplarim
            // 
            btnKitaplarim.BackColor = Color.FromArgb(76, 175, 80);
            btnKitaplarim.FlatAppearance.BorderSize = 0;
            btnKitaplarim.FlatStyle = FlatStyle.Flat;
            btnKitaplarim.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnKitaplarim.ForeColor = Color.White;
            btnKitaplarim.Location = new Point(10, 82);
            btnKitaplarim.Name = "btnKitaplarim";
            btnKitaplarim.Size = new Size(180, 43);
            btnKitaplarim.TabIndex = 1;
            btnKitaplarim.Text = "Kitaplarım";
            btnKitaplarim.UseVisualStyleBackColor = false;
            btnKitaplarim.Click += btnKitaplarim_Click;
            // 
            // btnUyeler
            // 
            btnUyeler.BackColor = Color.FromArgb(76, 175, 80);
            btnUyeler.FlatAppearance.BorderSize = 0;
            btnUyeler.FlatStyle = FlatStyle.Flat;
            btnUyeler.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUyeler.ForeColor = Color.White;
            btnUyeler.Location = new Point(10, 135);
            btnUyeler.Name = "btnUyeler";
            btnUyeler.Size = new Size(180, 45);
            btnUyeler.TabIndex = 1;
            btnUyeler.Text = "Profilim";
            btnUyeler.UseVisualStyleBackColor = false;
            btnUyeler.Click += btnUyeler_Click;
            // 
            // btnEmanetler
            // 
            btnEmanetler.BackColor = Color.FromArgb(76, 175, 80);
            btnEmanetler.FlatAppearance.BorderSize = 0;
            btnEmanetler.FlatStyle = FlatStyle.Flat;
            btnEmanetler.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEmanetler.ForeColor = Color.White;
            btnEmanetler.Location = new Point(10, 190);
            btnEmanetler.Name = "btnEmanetler";
            btnEmanetler.Size = new Size(180, 45);
            btnEmanetler.TabIndex = 2;
            btnEmanetler.Text = "Ödünç İşlemleri";
            btnEmanetler.UseVisualStyleBackColor = false;
            btnEmanetler.Click += btnEmanetler_Click;
            // 
            // btnRaporlar
            // 
            btnRaporlar.BackColor = Color.FromArgb(76, 175, 80);
            btnRaporlar.FlatAppearance.BorderSize = 0;
            btnRaporlar.FlatStyle = FlatStyle.Flat;
            btnRaporlar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRaporlar.ForeColor = Color.White;
            btnRaporlar.Location = new Point(10, 300);
            btnRaporlar.Name = "btnRaporlar";
            btnRaporlar.Size = new Size(180, 45);
            btnRaporlar.TabIndex = 3;
            btnRaporlar.Text = "Raporlar";
            btnRaporlar.UseVisualStyleBackColor = false;
            btnRaporlar.Click += btnRaporlar_Click;
            // 
            // btnYonetim
            // 
            btnYonetim.BackColor = Color.FromArgb(76, 175, 80);
            btnYonetim.FlatAppearance.BorderSize = 0;
            btnYonetim.FlatStyle = FlatStyle.Flat;
            btnYonetim.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnYonetim.ForeColor = Color.White;
            btnYonetim.Location = new Point(10, 245);
            btnYonetim.Name = "btnYonetim";
            btnYonetim.Size = new Size(180, 45);
            btnYonetim.TabIndex = 4;
            btnYonetim.Text = "Üyelik İşlemleri";
            btnYonetim.UseVisualStyleBackColor = false;
            btnYonetim.Click += btnYonetim_Click;
            // 
            // btnCikis
            // 
            btnCikis.BackColor = Color.FromArgb(244, 67, 54);
            btnCikis.FlatAppearance.BorderSize = 0;
            btnCikis.FlatStyle = FlatStyle.Flat;
            btnCikis.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCikis.ForeColor = Color.White;
            btnCikis.Location = new Point(10, 447);
            btnCikis.Name = "btnCikis";
            btnCikis.Size = new Size(180, 45);
            btnCikis.TabIndex = 5;
            btnCikis.Text = "Çıkış";
            btnCikis.UseVisualStyleBackColor = false;
            btnCikis.Click += btnCikis_Click;
            // 
            // btnKitapEkle
            // 
            btnKitapEkle.BackColor = Color.FromArgb(255, 152, 0);
            btnKitapEkle.FlatAppearance.BorderSize = 0;
            btnKitapEkle.FlatStyle = FlatStyle.Flat;
            btnKitapEkle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnKitapEkle.ForeColor = Color.White;
            btnKitapEkle.Location = new Point(10, 355);
            btnKitapEkle.Name = "btnKitapEkle";
            btnKitapEkle.Size = new Size(180, 45);
            btnKitapEkle.TabIndex = 6;
            btnKitapEkle.Text = "Kitap İşlemleri";
            btnKitapEkle.UseVisualStyleBackColor = false;
            btnKitapEkle.Click += btnKitapEkle_Click;
            // 
            // lblBaslik
            // 
            lblBaslik.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(0, 128, 0);
            lblBaslik.Location = new Point(220, 30);
            lblBaslik.Name = "lblBaslik";
            lblBaslik.Size = new Size(500, 40);
            lblBaslik.TabIndex = 0;
            lblBaslik.Text = "Kayseri Şeker Kütüphane Sistemi";
            // 
            // lblKullanici
            // 
            lblKullanici.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblKullanici.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblKullanici.ForeColor = Color.FromArgb(0, 128, 0);
            lblKullanici.Location = new Point(638, 34);
            lblKullanici.Name = "lblKullanici";
            lblKullanici.Size = new Size(200, 30);
            lblKullanici.TabIndex = 1;
            lblKullanici.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(850, 500);
            Controls.Add(lblBaslik);
            Controls.Add(lblKullanici);
            Controls.Add(panelMenu);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dashboard";
            Load += Dashboard_Load;
            panelMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}