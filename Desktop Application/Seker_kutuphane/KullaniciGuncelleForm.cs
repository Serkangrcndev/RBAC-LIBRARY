using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Seker_kutuphane
{
    public partial class KullaniciGuncelleForm : Form
    {
        private int kullaniciId;
        private DataGridViewRow selectedRow;
        private ApiHelper apiHelper;
        private bool isAdmin; // Admin kontrolü için

        public KullaniciGuncelleForm(int kullaniciId, DataGridViewRow selectedRow, bool isAdmin = false)
        {
            InitializeComponent();
            this.kullaniciId = kullaniciId;
            this.selectedRow = selectedRow;
            this.apiHelper = new ApiHelper();
            this.isAdmin = isAdmin;
            LoadKullaniciData();
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
            lblRol = new Label();
            clbRoller = new CheckedListBox();
            btnGuncelle = new Button();
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
            panelMain.Controls.Add(lblRol);
            panelMain.Controls.Add(clbRoller);
            panelMain.Controls.Add(btnGuncelle);
            panelMain.Controls.Add(btnIptal);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(500, 450);
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
            lblBaslik.Text = "Kullanıcı Güncelle";
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
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRol.ForeColor = Color.FromArgb(0, 128, 0);
            lblRol.Location = new Point(50, 280);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(54, 19);
            lblRol.TabIndex = 11;
            lblRol.Text = "Roller:";
            // 
            // clbRoller
            // 
            clbRoller.CheckOnClick = true;
            clbRoller.Font = new Font("Segoe UI", 10F);
            clbRoller.Location = new Point(150, 277);
            clbRoller.Name = "clbRoller";
            clbRoller.Size = new Size(300, 64);
            clbRoller.TabIndex = 12;
            // 
            // btnGuncelle
            // 
            btnGuncelle.BackColor = Color.FromArgb(76, 175, 80);
            btnGuncelle.FlatAppearance.BorderSize = 0;
            btnGuncelle.FlatStyle = FlatStyle.Flat;
            btnGuncelle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGuncelle.ForeColor = Color.White;
            btnGuncelle.Location = new Point(150, 370);
            btnGuncelle.Name = "btnGuncelle";
            btnGuncelle.Size = new Size(120, 40);
            btnGuncelle.TabIndex = 13;
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
            btnIptal.Location = new Point(290, 370);
            btnIptal.Name = "btnIptal";
            btnIptal.Size = new Size(120, 40);
            btnIptal.TabIndex = 14;
            btnIptal.Text = "İptal";
            btnIptal.UseVisualStyleBackColor = false;
            btnIptal.Click += btnIptal_Click;
            // 
            // KullaniciGuncelleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 450);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "KullaniciGuncelleForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Kullanıcı Güncelle";
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
        private Label lblRol;
        private CheckedListBox clbRoller;
        private Button btnGuncelle;
        private Button btnIptal;

        private async void LoadKullaniciData()
        {
            // Seçilen satırdan kullanıcı verilerini yükle
            txtAd.Text = selectedRow.Cells["ad"].Value?.ToString() ?? "";
            txtSoyad.Text = selectedRow.Cells["soyad"].Value?.ToString() ?? "";
            txtTC.Text = selectedRow.Cells["tc"].Value?.ToString() ?? "";
            txtTelefon.Text = selectedRow.Cells["telefon"].Value?.ToString() ?? "";
            txtEmail.Text = selectedRow.Cells["email"].Value?.ToString() ?? "";

            // Form başlığını güncelle
            this.Text = $"Kullanıcı Güncelle - {txtAd.Text} {txtSoyad.Text}";

            // CheckedListBox'ı Admin durumuna göre aktif/pasif yap
            clbRoller.Enabled = isAdmin;

            // Roller yükle
            await LoadRoller();
        }

        private async Task LoadRoller()
        {
            try
            {
                // Tüm roller yükle
                var response = await apiHelper.GetRolesAsync();
                if (response is Newtonsoft.Json.Linq.JArray rollerArray)
                {
                    clbRoller.Items.Clear();
                    
                    foreach (var rol in rollerArray)
                    {
                        string rolAdi = rol["rol_adi"]?.ToString() ?? "";
                        if (!string.IsNullOrEmpty(rolAdi))
                        {
                            clbRoller.Items.Add(rolAdi, false);
                        }
                    }

                    // Kullanıcının mevcut rollerini işaretle
                    string currentRoles = selectedRow.Cells["rol_adlari"].Value?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(currentRoles))
                    {
                        string[] roles = currentRoles.Split(',');
                        foreach (string role in roles)
                        {
                            string trimmedRole = role.Trim();
                            for (int i = 0; i < clbRoller.Items.Count; i++)
                            {
                                if (clbRoller.Items[i].ToString() == trimmedRole)
                                {
                                    clbRoller.SetItemChecked(i, true);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Roller yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnGuncelle_Click(object sender, EventArgs e)
        {
            // Validasyon
            if (string.IsNullOrEmpty(txtAd.Text.Trim()) ||
                string.IsNullOrEmpty(txtSoyad.Text.Trim()) ||
                string.IsNullOrEmpty(txtTC.Text.Trim()) ||
                string.IsNullOrEmpty(txtTelefon.Text.Trim()) ||
                string.IsNullOrEmpty(txtEmail.Text.Trim()))
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

            try
            {
                btnGuncelle.Enabled = false;
                btnGuncelle.Text = "Güncelleniyor...";

                // Güncelleme verilerini hazırla
                var updateData = new
                {
                    kullanici_id = kullaniciId,
                    ad = txtAd.Text.Trim(),
                    soyad = txtSoyad.Text.Trim(),
                    tc = txtTC.Text.Trim(),
                    telefon = txtTelefon.Text.Trim(),
                    email = txtEmail.Text.Trim()
                };

                var response = await apiHelper.UpdateUserProfileAsync(updateData);

                // Eğer Admin ise ve roller değiştirildiyse, rolleri güncelle
                if (isAdmin)
                {
                    await UpdateUserRoles();
                }
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kullanıcı güncellenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGuncelle.Enabled = true;
                btnGuncelle.Text = "Güncelle";
            }
        }

        private async Task UpdateUserRoles()
        {
            try
            {
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

                // Kullanıcının mevcut rollerini al
                var allUsers = await apiHelper.GetAllUsersAsync();
                dynamic currentUser = null;
                
                if (allUsers is Newtonsoft.Json.Linq.JArray usersArray)
                {
                    foreach (var user in usersArray)
                    {
                        if (Convert.ToInt32(user["kullanici_id"]) == kullaniciId)
                        {
                            currentUser = user;
                            break;
                        }
                    }
                }

                if (currentUser != null)
                {
                    // Güncelleme verisi hazırla
                    var updateData = new
                    {
                        kullanici_id = kullaniciId,
                        ad = currentUser["ad"],
                        soyad = currentUser["soyad"],
                        tc = currentUser["tc"],
                        telefon = currentUser["telefon"],
                        email = currentUser["email"],
                        rol_ids = selectedRoleIds.ToArray()
                    };

                    await apiHelper.UpdateUserProfileAsync(updateData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Roller güncellenirken hata oluştu: {ex.Message}", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                
                return 1; // Varsayılan
            }
            catch
            {
                return 1; // Varsayılan
            }
        }

        private void SetupEnterKeyEvents()
        {
            // Tüm textbox'lara Enter tuşu desteği ekle
            txtAd.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) btnGuncelle.PerformClick(); };
            txtSoyad.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) btnGuncelle.PerformClick(); };
            txtTC.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) btnGuncelle.PerformClick(); };
            txtTelefon.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) btnGuncelle.PerformClick(); };
            txtEmail.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) btnGuncelle.PerformClick(); };
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
} 