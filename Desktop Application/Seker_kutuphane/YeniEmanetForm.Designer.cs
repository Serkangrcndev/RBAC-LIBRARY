namespace Seker_kutuphane
{
    partial class YeniOduncForm
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelKullanici = new System.Windows.Forms.Panel();
            this.lblKullaniciTitle = new System.Windows.Forms.Label();
            this.txtAramaKullanici = new System.Windows.Forms.TextBox();
            this.comboBoxKullanici = new System.Windows.Forms.ComboBox();
            this.lblKullaniciArama = new System.Windows.Forms.Label();
            this.panelKitap = new System.Windows.Forms.Panel();
            this.lblKitapTitle = new System.Windows.Forms.Label();
            this.txtAramaKitap = new System.Windows.Forms.TextBox();
            this.comboBoxKitap = new System.Windows.Forms.ComboBox();
            this.lblKitapArama = new System.Windows.Forms.Label();
            this.panelKitapBilgi = new System.Windows.Forms.Panel();
            this.lblYayinevi = new System.Windows.Forms.Label();
            this.lblYazar = new System.Windows.Forms.Label();
            this.lblStok = new System.Windows.Forms.Label();
            this.lblKitapBilgiTitle = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnIptal = new System.Windows.Forms.Button();
            this.btnOduncVer = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelKullanici.SuspendLayout();
            this.panelKitap.SuspendLayout();
            this.panelKitapBilgi.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(600, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(150, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Yeni Ödünç";
            // 
            // panelKullanici
            // 
            this.panelKullanici.BackColor = System.Drawing.Color.White;
            this.panelKullanici.Controls.Add(this.lblKullaniciTitle);
            this.panelKullanici.Controls.Add(this.txtAramaKullanici);
            this.panelKullanici.Controls.Add(this.comboBoxKullanici);
            this.panelKullanici.Controls.Add(this.lblKullaniciArama);
            this.panelKullanici.Location = new System.Drawing.Point(20, 80);
            this.panelKullanici.Name = "panelKullanici";
            this.panelKullanici.Size = new System.Drawing.Size(560, 120);
            this.panelKullanici.TabIndex = 1;
            // 
            // lblKullaniciTitle
            // 
            this.lblKullaniciTitle.AutoSize = true;
            this.lblKullaniciTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblKullaniciTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblKullaniciTitle.Location = new System.Drawing.Point(10, 10);
            this.lblKullaniciTitle.Name = "lblKullaniciTitle";
            this.lblKullaniciTitle.Size = new System.Drawing.Size(80, 21);
            this.lblKullaniciTitle.TabIndex = 3;
            this.lblKullaniciTitle.Text = "Kullanıcı:";
            // 
            // txtAramaKullanici
            // 
            this.txtAramaKullanici.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtAramaKullanici.Location = new System.Drawing.Point(10, 40);
            this.txtAramaKullanici.Name = "txtAramaKullanici";
            this.txtAramaKullanici.PlaceholderText = "🔍 Kullanıcı ara...";
            this.txtAramaKullanici.Size = new System.Drawing.Size(200, 25);
            this.txtAramaKullanici.TabIndex = 2;
            this.txtAramaKullanici.TextChanged += new System.EventHandler(this.txtAramaKullanici_TextChanged);
            // 
            // comboBoxKullanici
            // 
            this.comboBoxKullanici.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxKullanici.FormattingEnabled = true;
            this.comboBoxKullanici.Location = new System.Drawing.Point(10, 80);
            this.comboBoxKullanici.Name = "comboBoxKullanici";
            this.comboBoxKullanici.Size = new System.Drawing.Size(540, 25);
            this.comboBoxKullanici.TabIndex = 1;
            // 
            // lblKullaniciArama
            // 
            this.lblKullaniciArama.AutoSize = true;
            this.lblKullaniciArama.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblKullaniciArama.ForeColor = System.Drawing.Color.Gray;
            this.lblKullaniciArama.Location = new System.Drawing.Point(220, 43);
            this.lblKullaniciArama.Name = "lblKullaniciArama";
            this.lblKullaniciArama.Size = new System.Drawing.Size(120, 15);
            this.lblKullaniciArama.TabIndex = 0;
            this.lblKullaniciArama.Text = "Ad, soyad veya TC ile ara";
            // 
            // panelKitap
            // 
            this.panelKitap.BackColor = System.Drawing.Color.White;
            this.panelKitap.Controls.Add(this.lblKitapTitle);
            this.panelKitap.Controls.Add(this.txtAramaKitap);
            this.panelKitap.Controls.Add(this.comboBoxKitap);
            this.panelKitap.Controls.Add(this.lblKitapArama);
            this.panelKitap.Location = new System.Drawing.Point(20, 220);
            this.panelKitap.Name = "panelKitap";
            this.panelKitap.Size = new System.Drawing.Size(560, 120);
            this.panelKitap.TabIndex = 2;
            // 
            // lblKitapTitle
            // 
            this.lblKitapTitle.AutoSize = true;
            this.lblKitapTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblKitapTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblKitapTitle.Location = new System.Drawing.Point(10, 10);
            this.lblKitapTitle.Name = "lblKitapTitle";
            this.lblKitapTitle.Size = new System.Drawing.Size(50, 21);
            this.lblKitapTitle.TabIndex = 3;
            this.lblKitapTitle.Text = "Kitap:";
            // 
            // txtAramaKitap
            // 
            this.txtAramaKitap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtAramaKitap.Location = new System.Drawing.Point(10, 40);
            this.txtAramaKitap.Name = "txtAramaKitap";
            this.txtAramaKitap.PlaceholderText = "🔍 Kitap ara...";
            this.txtAramaKitap.Size = new System.Drawing.Size(200, 25);
            this.txtAramaKitap.TabIndex = 2;
            this.txtAramaKitap.TextChanged += new System.EventHandler(this.txtAramaKitap_TextChanged);
            // 
            // comboBoxKitap
            // 
            this.comboBoxKitap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxKitap.FormattingEnabled = true;
            this.comboBoxKitap.Location = new System.Drawing.Point(10, 80);
            this.comboBoxKitap.Name = "comboBoxKitap";
            this.comboBoxKitap.Size = new System.Drawing.Size(540, 25);
            this.comboBoxKitap.TabIndex = 1;
            this.comboBoxKitap.SelectedIndexChanged += new System.EventHandler(this.comboBoxKitap_SelectedIndexChanged);
            // 
            // lblKitapArama
            // 
            this.lblKitapArama.AutoSize = true;
            this.lblKitapArama.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblKitapArama.ForeColor = System.Drawing.Color.Gray;
            this.lblKitapArama.Location = new System.Drawing.Point(220, 43);
            this.lblKitapArama.Name = "lblKitapArama";
            this.lblKitapArama.Size = new System.Drawing.Size(120, 15);
            this.lblKitapArama.TabIndex = 0;
            this.lblKitapArama.Text = "Kitap adı veya yazar ile ara";
            // 
            // panelKitapBilgi
            // 
            this.panelKitapBilgi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelKitapBilgi.Controls.Add(this.lblYayinevi);
            this.panelKitapBilgi.Controls.Add(this.lblYazar);
            this.panelKitapBilgi.Controls.Add(this.lblStok);
            this.panelKitapBilgi.Controls.Add(this.lblKitapBilgiTitle);
            this.panelKitapBilgi.Location = new System.Drawing.Point(20, 360);
            this.panelKitapBilgi.Name = "panelKitapBilgi";
            this.panelKitapBilgi.Size = new System.Drawing.Size(560, 100);
            this.panelKitapBilgi.TabIndex = 3;
            // 
            // lblYayinevi
            // 
            this.lblYayinevi.AutoSize = true;
            this.lblYayinevi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblYayinevi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblYayinevi.Location = new System.Drawing.Point(10, 70);
            this.lblYayinevi.Name = "lblYayinevi";
            this.lblYayinevi.Size = new System.Drawing.Size(80, 19);
            this.lblYayinevi.TabIndex = 3;
            this.lblYayinevi.Text = "Yayınevi: -";
            // 
            // lblYazar
            // 
            this.lblYazar.AutoSize = true;
            this.lblYazar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblYazar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblYazar.Location = new System.Drawing.Point(10, 50);
            this.lblYazar.Name = "lblYazar";
            this.lblYazar.Size = new System.Drawing.Size(50, 19);
            this.lblYazar.TabIndex = 2;
            this.lblYazar.Text = "Yazar: -";
            // 
            // lblStok
            // 
            this.lblStok.AutoSize = true;
            this.lblStok.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStok.ForeColor = System.Drawing.Color.Green;
            this.lblStok.Location = new System.Drawing.Point(10, 30);
            this.lblStok.Name = "lblStok";
            this.lblStok.Size = new System.Drawing.Size(50, 19);
            this.lblStok.TabIndex = 1;
            this.lblStok.Text = "Stok: -";
            // 
            // lblKitapBilgiTitle
            // 
            this.lblKitapBilgiTitle.AutoSize = true;
            this.lblKitapBilgiTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblKitapBilgiTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblKitapBilgiTitle.Location = new System.Drawing.Point(10, 10);
            this.lblKitapBilgiTitle.Name = "lblKitapBilgiTitle";
            this.lblKitapBilgiTitle.Size = new System.Drawing.Size(90, 20);
            this.lblKitapBilgiTitle.TabIndex = 0;
            this.lblKitapBilgiTitle.Text = "Kitap Bilgisi:";
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.White;
            this.panelButtons.Controls.Add(this.btnIptal);
            this.panelButtons.Controls.Add(this.btnOduncVer);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 480);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(600, 70);
            this.panelButtons.TabIndex = 4;
            // 
            // btnIptal
            // 
            this.btnIptal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIptal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnIptal.FlatAppearance.BorderSize = 0;
            this.btnIptal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIptal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnIptal.ForeColor = System.Drawing.Color.White;
            this.btnIptal.Location = new System.Drawing.Point(480, 20);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(100, 35);
            this.btnIptal.TabIndex = 1;
            this.btnIptal.Text = "İptal";
            this.btnIptal.UseVisualStyleBackColor = false;
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // btnOduncVer
            // 
            this.btnOduncVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOduncVer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnOduncVer.FlatAppearance.BorderSize = 0;
            this.btnOduncVer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOduncVer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOduncVer.ForeColor = System.Drawing.Color.White;
            this.btnOduncVer.Location = new System.Drawing.Point(360, 20);
            this.btnOduncVer.Name = "btnOduncVer";
            this.btnOduncVer.Size = new System.Drawing.Size(110, 35);
            this.btnOduncVer.TabIndex = 0;
            this.btnOduncVer.Text = "Ödünç Ver";
            this.btnOduncVer.UseVisualStyleBackColor = false;
            this.btnOduncVer.Click += new System.EventHandler(this.btnOduncVer_Click);
            // 
            // YeniEmanetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(600, 550);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelKitapBilgi);
            this.Controls.Add(this.panelKitap);
            this.Controls.Add(this.panelKullanici);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "YeniOduncForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Yeni Ödünç";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelKullanici.ResumeLayout(false);
            this.panelKullanici.PerformLayout();
            this.panelKitap.ResumeLayout(false);
            this.panelKitap.PerformLayout();
            this.panelKitapBilgi.ResumeLayout(false);
            this.panelKitapBilgi.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelHeader;
        private Label lblTitle;
        private Panel panelKullanici;
        private Label lblKullaniciTitle;
        private TextBox txtAramaKullanici;
        private ComboBox comboBoxKullanici;
        private Label lblKullaniciArama;
        private Panel panelKitap;
        private Label lblKitapTitle;
        private TextBox txtAramaKitap;
        private ComboBox comboBoxKitap;
        private Label lblKitapArama;
        private Panel panelKitapBilgi;
        private Label lblYayinevi;
        private Label lblYazar;
        private Label lblStok;
        private Label lblKitapBilgiTitle;
        private Panel panelButtons;
        private Button btnIptal;
        private Button btnOduncVer;
    }
} 