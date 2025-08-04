using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Seker_kutuphane
{
    public partial class SifreDegistirForm : Form
    {
        private int kullaniciId;
        private ApiHelper apiHelper;

        public SifreDegistirForm(int kullaniciId)
        {
            InitializeComponent();
            this.kullaniciId = kullaniciId;
            this.apiHelper = new ApiHelper();
            SetupEnterKeyEvents();
        }

        private void InitializeComponent()
        {
            panelMain = new Panel();
            lblBaslik = new Label();
            lblMevcutSifre = new Label();
            txtMevcutSifre = new TextBox();
            lblYeniSifre = new Label();
            txtYeniSifre = new TextBox();
            lblYeniSifreTekrar = new Label();
            txtYeniSifreTekrar = new TextBox();
            btnGuncelle = new Button();
            btnIptal = new Button();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(245, 245, 245);
            panelMain.Controls.Add(lblBaslik);
            panelMain.Controls.Add(lblMevcutSifre);
            panelMain.Controls.Add(txtMevcutSifre);
            panelMain.Controls.Add(lblYeniSifre);
            panelMain.Controls.Add(txtYeniSifre);
            panelMain.Controls.Add(lblYeniSifreTekrar);
            panelMain.Controls.Add(txtYeniSifreTekrar);
            panelMain.Controls.Add(btnGuncelle);
            panelMain.Controls.Add(btnIptal);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(450, 300);
            panelMain.TabIndex = 0;
            // 
            // lblBaslik
            // 
            lblBaslik.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(0, 128, 0);
            lblBaslik.Location = new Point(20, 20);
            lblBaslik.Name = "lblBaslik";
            lblBaslik.Size = new Size(410, 40);
            lblBaslik.TabIndex = 0;
            lblBaslik.Text = "Şifre Değiştir";
            lblBaslik.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMevcutSifre
            // 
            lblMevcutSifre.AutoSize = true;
            lblMevcutSifre.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMevcutSifre.ForeColor = Color.FromArgb(0, 128, 0);
            lblMevcutSifre.Location = new Point(50, 80);
            lblMevcutSifre.Name = "lblMevcutSifre";
            lblMevcutSifre.Size = new Size(97, 19);
            lblMevcutSifre.TabIndex = 1;
            lblMevcutSifre.Text = "Mevcut Şifre:";
            // 
            // txtMevcutSifre
            // 
            txtMevcutSifre.Font = new Font("Segoe UI", 10F);
            txtMevcutSifre.Location = new Point(200, 77);
            txtMevcutSifre.Name = "txtMevcutSifre";
            txtMevcutSifre.PasswordChar = '●';
            txtMevcutSifre.Size = new Size(200, 25);
            txtMevcutSifre.TabIndex = 2;
            // 
            // lblYeniSifre
            // 
            lblYeniSifre.AutoSize = true;
            lblYeniSifre.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblYeniSifre.ForeColor = Color.FromArgb(0, 128, 0);
            lblYeniSifre.Location = new Point(50, 120);
            lblYeniSifre.Name = "lblYeniSifre";
            lblYeniSifre.Size = new Size(76, 19);
            lblYeniSifre.TabIndex = 3;
            lblYeniSifre.Text = "Yeni Şifre:";
            // 
            // txtYeniSifre
            // 
            txtYeniSifre.Font = new Font("Segoe UI", 10F);
            txtYeniSifre.Location = new Point(200, 117);
            txtYeniSifre.Name = "txtYeniSifre";
            txtYeniSifre.PasswordChar = '●';
            txtYeniSifre.Size = new Size(200, 25);
            txtYeniSifre.TabIndex = 4;
            // 
            // lblYeniSifreTekrar
            // 
            lblYeniSifreTekrar.AutoSize = true;
            lblYeniSifreTekrar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblYeniSifreTekrar.ForeColor = Color.FromArgb(0, 128, 0);
            lblYeniSifreTekrar.Location = new Point(50, 160);
            lblYeniSifreTekrar.Name = "lblYeniSifreTekrar";
            lblYeniSifreTekrar.Size = new Size(133, 19);
            lblYeniSifreTekrar.TabIndex = 5;
            lblYeniSifreTekrar.Text = "Yeni Şifre (Tekrar):";
            // 
            // txtYeniSifreTekrar
            // 
            txtYeniSifreTekrar.Font = new Font("Segoe UI", 10F);
            txtYeniSifreTekrar.Location = new Point(200, 157);
            txtYeniSifreTekrar.Name = "txtYeniSifreTekrar";
            txtYeniSifreTekrar.PasswordChar = '●';
            txtYeniSifreTekrar.Size = new Size(200, 25);
            txtYeniSifreTekrar.TabIndex = 6;
            // 
            // btnGuncelle
            // 
            btnGuncelle.BackColor = Color.FromArgb(76, 175, 80);
            btnGuncelle.FlatAppearance.BorderSize = 0;
            btnGuncelle.FlatStyle = FlatStyle.Flat;
            btnGuncelle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGuncelle.ForeColor = Color.White;
            btnGuncelle.Location = new Point(50, 200);
            btnGuncelle.Name = "btnGuncelle";
            btnGuncelle.Size = new Size(120, 40);
            btnGuncelle.TabIndex = 7;
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
            btnIptal.Location = new Point(247, 200);
            btnIptal.Name = "btnIptal";
            btnIptal.Size = new Size(120, 40);
            btnIptal.TabIndex = 8;
            btnIptal.Text = "İptal";
            btnIptal.UseVisualStyleBackColor = false;
            btnIptal.Click += btnIptal_Click;
            // 
            // SifreDegistirForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 300);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SifreDegistirForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Şifre Değiştir";
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ResumeLayout(false);
        }

        private Panel panelMain;
        private Label lblBaslik;
        private Label lblMevcutSifre;
        private TextBox txtMevcutSifre;
        private Label lblYeniSifre;
        private TextBox txtYeniSifre;
        private Label lblYeniSifreTekrar;
        private TextBox txtYeniSifreTekrar;
        private Button btnGuncelle;
        private Button btnIptal;

        private async void btnGuncelle_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (string.IsNullOrEmpty(txtMevcutSifre.Text.Trim()))
            {
                MessageBox.Show("Lütfen mevcut şifrenizi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMevcutSifre.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtYeniSifre.Text.Trim()))
            {
                MessageBox.Show("Lütfen yeni şifrenizi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtYeniSifre.Focus();
                return;
            }

            if (txtYeniSifre.Text.Trim().Length < 6)
            {
                MessageBox.Show("Yeni şifre en az 6 karakter olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtYeniSifre.Focus();
                return;
            }

            if (txtYeniSifre.Text.Trim() != txtYeniSifreTekrar.Text.Trim())
            {
                MessageBox.Show("Yeni şifreler eşleşmiyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtYeniSifreTekrar.Focus();
                return;
            }

            try
            {
                btnGuncelle.Enabled = false;
                btnGuncelle.Text = "Güncelleniyor...";

                // Hem mevcut şifreyi hem de yeni şifreyi hash'le (API hashleme yapmıyor)
                string hashedMevcutSifre = Sha256Hash(txtMevcutSifre.Text.Trim());
                string hashedYeniSifre = Sha256Hash(txtYeniSifre.Text.Trim());
                var result = await apiHelper.UpdatePasswordAsync(kullaniciId, hashedMevcutSifre, hashedYeniSifre);
                
                MessageBox.Show("Şifreniz başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Şifre güncelleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGuncelle.Enabled = true;
                btnGuncelle.Text = "Güncelle";
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SetupEnterKeyEvents()
        {
            // Tüm şifre textbox'larına Enter tuşu desteği ekle
            txtMevcutSifre.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) btnGuncelle.PerformClick(); };
            txtYeniSifre.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) btnGuncelle.PerformClick(); };
            txtYeniSifreTekrar.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) btnGuncelle.PerformClick(); };
        }

        private string Sha256Hash(string value)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
} 