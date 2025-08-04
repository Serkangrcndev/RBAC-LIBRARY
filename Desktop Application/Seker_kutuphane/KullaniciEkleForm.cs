using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks; // Added missing import for Task
using System.Linq; // Added for LINQ methods

namespace Seker_kutuphane
{
    public partial class KullaniciEkleForm : Form
    {
        private ApiHelper apiHelper;
        private bool isAdmin; // Admin kontrolü için

        public KullaniciEkleForm(bool isAdmin = false)
        {
            InitializeComponent();
            this.apiHelper = new ApiHelper();
            this.isAdmin = isAdmin;
            SetupInputRestrictions();
            SetupEnterKeyEvents();
        }

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
            lblSifre = new Label();
            txtSifre = new TextBox();
            lblSifreTekrar = new Label();
            txtSifreTekrar = new TextBox();
            lblRol = new Label();
            clbRoller = new CheckedListBox();
            btnEkle = new Button();
            btnIptal = new Button();
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
            panelMain.Controls.Add(lblSifre);
            panelMain.Controls.Add(txtSifre);
            panelMain.Controls.Add(lblSifreTekrar);
            panelMain.Controls.Add(txtSifreTekrar);
            panelMain.Controls.Add(lblRol);
            panelMain.Controls.Add(clbRoller);
            panelMain.Controls.Add(btnEkle);
            panelMain.Controls.Add(btnIptal);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(500, 500);
            panelMain.TabIndex = 0;
            // 
            // lblBaslik
            // 
            lblBaslik.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(0, 128, 0);
            lblBaslik.Location = new Point(20, 20);
            lblBaslik.Name = "lblBaslik";
            lblBaslik.Size = new Size(460, 40);
            lblBaslik.TabIndex = 0;
            lblBaslik.Text = "Yeni Kullanıcı Ekle";
            lblBaslik.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblAd
            // 
            lblAd.AutoSize = true;
            lblAd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAd.ForeColor = Color.FromArgb(0, 128, 0);
            lblAd.Location = new Point(50, 80);
            lblAd.Name = "lblAd";
            lblAd.Size = new Size(32, 19);
            lblAd.TabIndex = 1;
            lblAd.Text = "Ad:";
            // 
            // txtAd
            // 
            txtAd.Font = new Font("Segoe UI", 10F);
            txtAd.Location = new Point(150, 77);
            txtAd.Name = "txtAd";
            txtAd.Size = new Size(300, 25);
            txtAd.TabIndex = 2;
            // 
            // lblSoyad
            // 
            lblSoyad.AutoSize = true;
            lblSoyad.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSoyad.ForeColor = Color.FromArgb(0, 128, 0);
            lblSoyad.Location = new Point(50, 120);
            lblSoyad.Name = "lblSoyad";
            lblSoyad.Size = new Size(55, 19);
            lblSoyad.TabIndex = 3;
            lblSoyad.Text = "Soyad:";
            // 
            // txtSoyad
            // 
            txtSoyad.Font = new Font("Segoe UI", 10F);
            txtSoyad.Location = new Point(150, 117);
            txtSoyad.Name = "txtSoyad";
            txtSoyad.Size = new Size(300, 25);
            txtSoyad.TabIndex = 4;
            // 
            // lblTC
            // 
            lblTC.AutoSize = true;
            lblTC.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTC.ForeColor = Color.FromArgb(0, 128, 0);
            lblTC.Location = new Point(50, 160);
            lblTC.Name = "lblTC";
            lblTC.Size = new Size(100, 19);
            lblTC.TabIndex = 5;
            lblTC.Text = "TC Kimlik No:";
            // 
            // txtTC
            // 
            txtTC.Font = new Font("Segoe UI", 10F);
            txtTC.Location = new Point(150, 157);
            txtTC.Name = "txtTC";
            txtTC.Size = new Size(300, 25);
            txtTC.TabIndex = 6;
            // 
            // lblTelefon
            // 
            lblTelefon.AutoSize = true;
            lblTelefon.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTelefon.ForeColor = Color.FromArgb(0, 128, 0);
            lblTelefon.Location = new Point(50, 200);
            lblTelefon.Name = "lblTelefon";
            lblTelefon.Size = new Size(62, 19);
            lblTelefon.TabIndex = 7;
            lblTelefon.Text = "Telefon:";
            // 
            // txtTelefon
            // 
            txtTelefon.Font = new Font("Segoe UI", 10F);
            txtTelefon.Location = new Point(150, 197);
            txtTelefon.Name = "txtTelefon";
            txtTelefon.Size = new Size(300, 25);
            txtTelefon.TabIndex = 8;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmail.ForeColor = Color.FromArgb(0, 128, 0);
            lblEmail.Location = new Point(50, 240);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(55, 19);
            lblEmail.TabIndex = 9;
            lblEmail.Text = "E-mail:";
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 10F);
            txtEmail.Location = new Point(150, 237);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 25);
            txtEmail.TabIndex = 10;
            // 
            // lblSifre
            // 
            lblSifre.AutoSize = true;
            lblSifre.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSifre.ForeColor = Color.FromArgb(0, 128, 0);
            lblSifre.Location = new Point(50, 280);
            lblSifre.Name = "lblSifre";
            lblSifre.Size = new Size(44, 19);
            lblSifre.TabIndex = 11;
            lblSifre.Text = "Şifre:";
            // 
            // txtSifre
            // 
            txtSifre.Font = new Font("Segoe UI", 10F);
            txtSifre.Location = new Point(150, 277);
            txtSifre.Name = "txtSifre";
            txtSifre.PasswordChar = '●';
            txtSifre.Size = new Size(300, 25);
            txtSifre.TabIndex = 12;
            // 
            // lblSifreTekrar
            // 
            lblSifreTekrar.AutoSize = true;
            lblSifreTekrar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSifreTekrar.ForeColor = Color.FromArgb(0, 128, 0);
            lblSifreTekrar.Location = new Point(50, 320);
            lblSifreTekrar.Name = "lblSifreTekrar";
            lblSifreTekrar.Size = new Size(101, 19);
            lblSifreTekrar.TabIndex = 13;
            lblSifreTekrar.Text = "Şifre (Tekrar):";
            // 
            // txtSifreTekrar
            // 
            txtSifreTekrar.Font = new Font("Segoe UI", 10F);
            txtSifreTekrar.Location = new Point(150, 317);
            txtSifreTekrar.Name = "txtSifreTekrar";
            txtSifreTekrar.PasswordChar = '●';
            txtSifreTekrar.Size = new Size(300, 25);
            txtSifreTekrar.TabIndex = 14;
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRol.ForeColor = Color.FromArgb(0, 128, 0);
            lblRol.Location = new Point(50, 360);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(35, 19);
            lblRol.TabIndex = 15;
            lblRol.Text = "Rol:";
            // 
            // clbRoller
            // 
            clbRoller.CheckOnClick = true;
            clbRoller.Font = new Font("Segoe UI", 10F);
            clbRoller.Location = new Point(150, 357);
            clbRoller.Name = "clbRoller";
            clbRoller.Size = new Size(300, 64);
            clbRoller.TabIndex = 16;
            // 
            // btnEkle
            // 
            btnEkle.BackColor = Color.FromArgb(76, 175, 80);
            btnEkle.FlatAppearance.BorderSize = 0;
            btnEkle.FlatStyle = FlatStyle.Flat;
            btnEkle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEkle.ForeColor = Color.White;
            btnEkle.Location = new Point(150, 450);
            btnEkle.Name = "btnEkle";
            btnEkle.Size = new Size(120, 40);
            btnEkle.TabIndex = 17;
            btnEkle.Text = "Ekle";
            btnEkle.UseVisualStyleBackColor = false;
            btnEkle.Click += btnEkle_Click;
            // 
            // btnIptal
            // 
            btnIptal.BackColor = Color.FromArgb(158, 158, 158);
            btnIptal.FlatAppearance.BorderSize = 0;
            btnIptal.FlatStyle = FlatStyle.Flat;
            btnIptal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnIptal.ForeColor = Color.White;
            btnIptal.Location = new Point(290, 450);
            btnIptal.Name = "btnIptal";
            btnIptal.Size = new Size(120, 40);
            btnIptal.TabIndex = 18;
            btnIptal.Text = "İptal";
            btnIptal.UseVisualStyleBackColor = false;
            btnIptal.Click += btnIptal_Click;
            // 
            // KullaniciEkleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 500);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "KullaniciEkleForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Yeni Kullanıcı Ekle";
            Load += KullaniciEkleForm_Load;
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ResumeLayout(false);
        }

        private Panel panelMain;
        private Label lblBaslik;
        private Label lblAd;
        private TextBox txtAd;
        private Label lblSoyad;
        private TextBox txtSoyad;
        private Label lblTC;
        private TextBox txtTC;
        private Label lblTelefon;
        private TextBox txtTelefon;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblSifre;
        private TextBox txtSifre;
        private Label lblSifreTekrar;
        private TextBox txtSifreTekrar;
        private Label lblRol;
        private CheckedListBox clbRoller;
        private Button btnEkle;
        private Button btnIptal;

        private void SetupInputRestrictions()
        {
            // TC kimlik numarası için kısıtlamalar
            txtTC.MaxLength = 11;
            txtTC.KeyPress += TxtTC_KeyPress;
            txtTC.TextChanged += TxtTC_TextChanged;

            // Telefon numarası için kısıtlamalar
            txtTelefon.MaxLength = 11;
            txtTelefon.KeyPress += TxtTelefon_KeyPress;
            txtTelefon.TextChanged += TxtTelefon_TextChanged;
        }

        private void TxtTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtTC_TextChanged(object sender, EventArgs e)
        {
            if (txtTC.Text.Contains(" "))
            {
                txtTC.Text = txtTC.Text.Replace(" ", "");
                txtTC.SelectionStart = txtTC.Text.Length;
            }
        }

        private void TxtTelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtTelefon_TextChanged(object sender, EventArgs e)
        {
            if (txtTelefon.Text.Contains(" "))
            {
                txtTelefon.Text = txtTelefon.Text.Replace(" ", "");
                txtTelefon.SelectionStart = txtTelefon.Text.Length;
            }
        }

        private async void KullaniciEkleForm_Load(object sender, EventArgs e)
        {
            await LoadRoller();
        }

        private async Task LoadRoller()
        {
            try
            {
                var response = await apiHelper.GetRolesAsync();
                
                if (response is Newtonsoft.Json.Linq.JArray rollerArray)
                {
                    clbRoller.Items.Clear();
                    
                    foreach (var rol in rollerArray)
                    {
                        string rolAdi = rol["rol_adi"]?.ToString() ?? "";
                        if (!string.IsNullOrEmpty(rolAdi))
                        {
                            clbRoller.Items.Add(rolAdi, false); // Hiçbiri seçili değil
                        }
                    }

                    // Admin değilse CheckedListBox'ı devre dışı bırak
                    clbRoller.Enabled = isAdmin;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Roller yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEkle_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (string.IsNullOrEmpty(txtAd.Text.Trim()) ||
                string.IsNullOrEmpty(txtSoyad.Text.Trim()) ||
                string.IsNullOrEmpty(txtTC.Text.Trim()) ||
                string.IsNullOrEmpty(txtTelefon.Text.Trim()) ||
                string.IsNullOrEmpty(txtEmail.Text.Trim()) ||
                string.IsNullOrEmpty(txtSifre.Text.Trim()) ||
                string.IsNullOrEmpty(txtSifreTekrar.Text.Trim()) ||
                (isAdmin && clbRoller.CheckedItems.Count == 0)) // Sadece Admin için rol seçimi zorunlu
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtTC.Text.Length != 11)
            {
                MessageBox.Show("TC Kimlik Numarası 11 haneli olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtTelefon.Text.Length < 10)
            {
                MessageBox.Show("Telefon numarası en az 10 haneli olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Geçerli bir e-posta adresi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtSifre.Text.Length < 6)
            {
                MessageBox.Show("Şifre en az 6 karakter olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtSifre.Text != txtSifreTekrar.Text)
            {
                MessageBox.Show("Şifreler eşleşmiyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnEkle.Enabled = false;
                btnEkle.Text = "Ekleniyor...";

                // Şifreyi hash'le
                string hashedSifre = Sha256Hash(txtSifre.Text.Trim());

                // Seçilen rollerin ID'lerini bul
                var selectedRoleIds = new List<int>();
                var selectedRoleNames = new List<string>();
                
                for (int i = 0; i < clbRoller.Items.Count; i++)
                {
                    if (clbRoller.GetItemChecked(i))
                    {
                        string rolAdi = clbRoller.Items[i].ToString();
                        int rolId = await GetRolId(rolAdi);
                        selectedRoleIds.Add(rolId);
                        selectedRoleNames.Add(rolAdi);
                    }
                }

                // Kullanıcı verilerini hazırla
                var userData = new
                {
                    ad = txtAd.Text.Trim(),
                    soyad = txtSoyad.Text.Trim(),
                    tc = txtTC.Text.Trim(),
                    telefon = txtTelefon.Text.Trim(),
                    email = txtEmail.Text.Trim(),
                    sifre = hashedSifre
                    // API rol parametrelerini görmezden geliyor, bu yüzden kaldırdık
                };

                var response = await apiHelper.RegisterAsync(userData);
                
                // Kullanıcı başarıyla oluşturulduysa, seçilen roller de ekle
                if (response != null && response.user != null)
                {
                    int yeniKullaniciId = Convert.ToInt32(response.user.kullanici_id);
                    int addedRolesCount = 0;
                    
                    // Seçilen her rol için ekleme yap
                    foreach (int rolId in selectedRoleIds)
                    {
                        // Seçilen rol "Üye" değilse ekle (çünkü API zaten "Üye" rolünü atıyor)
                        if (rolId != 1) // 1 = Üye rolü
                        {
                            try
                            {
                                await apiHelper.AddRoleToUserAsync(yeniKullaniciId, rolId);
                                addedRolesCount++;
                            }
                            catch (Exception roleEx)
                            {
                                MessageBox.Show($"'{selectedRoleNames[selectedRoleIds.IndexOf(rolId)]}' rolü eklenirken hata oluştu: {roleEx.Message}", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    
                    if (addedRolesCount > 0)
                    {
                        string rolesText = string.Join(", ", selectedRoleNames.Where(name => name != "Üye"));
                        MessageBox.Show($"Kullanıcı başarıyla oluşturuldu ve şu roller eklendi: {rolesText}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı başarıyla oluşturuldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                    
                    this.DialogResult = DialogResult.OK;
                    this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kullanıcı eklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnEkle.Enabled = true;
                btnEkle.Text = "Ekle";
            }
        }

        private async Task<int> GetRolId(string rolAdi)
        {
            try
            {
                var response = await apiHelper.GetRolesAsync();
                
                if (response is Newtonsoft.Json.Linq.JArray rollerArray)
                {
                    foreach (var rol in rollerArray)
                    {
                        if (rol["rol_adi"]?.ToString() == rolAdi)
                        {
                            return Convert.ToInt32(rol["rol_id"]);
                        }
                    }
                }
                
                // Varsayılan olarak "Üye" rolü ID'si (genellikle 1)
                return 1;
            }
            catch
            {
                return 1; // Varsayılan
            }
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

        private void SetupEnterKeyEvents()
        {
            // Tüm textbox'lara Enter tuşu desteği ekle
            txtAd.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) btnEkle.PerformClick(); };
            txtSoyad.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) btnEkle.PerformClick(); };
            txtTC.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) btnEkle.PerformClick(); };
            txtTelefon.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) btnEkle.PerformClick(); };
            txtEmail.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) btnEkle.PerformClick(); };
            txtSifre.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) btnEkle.PerformClick(); };
            txtSifreTekrar.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) btnEkle.PerformClick(); };
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
} 