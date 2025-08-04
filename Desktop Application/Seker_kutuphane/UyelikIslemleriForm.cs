using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Seker_kutuphane
{
    public partial class UyelikIslemleriForm : Form
    {
        private ApiHelper apiHelper;
        private DataTable kullanicilarTable;
        private bool isAdmin; // Admin kontrolü için

        public UyelikIslemleriForm(bool isAdmin = false)
        {
            InitializeComponent();
            this.apiHelper = new ApiHelper();
            this.kullanicilarTable = new DataTable();
            this.isAdmin = isAdmin;
            SetupDataGridView();
            LoadKullanicilar();
        }

        private void InitializeComponent()
        {
            panelMain = new Panel();
            lblBaslik = new Label();
            lblIstatistikler = new Label();
            dgvKullanicilar = new DataGridView();
            btnYeniKullanici = new Button();
            btnGuncelle = new Button();
            btnSil = new Button();
            btnAktiflestir = new Button();
            btnYenile = new Button();
            btnKapat = new Button();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKullanicilar).BeginInit();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(245, 245, 245);
            panelMain.Controls.Add(lblBaslik);
            panelMain.Controls.Add(lblIstatistikler);
            panelMain.Controls.Add(dgvKullanicilar);
            panelMain.Controls.Add(btnYeniKullanici);
            panelMain.Controls.Add(btnGuncelle);
            panelMain.Controls.Add(btnSil);
            panelMain.Controls.Add(btnAktiflestir);
            panelMain.Controls.Add(btnYenile);
            panelMain.Controls.Add(btnKapat);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1063, 600);
            panelMain.TabIndex = 0;
            // 
            // lblBaslik
            // 
            lblBaslik.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(0, 128, 0);
            lblBaslik.Location = new Point(20, 15);
            lblBaslik.Name = "lblBaslik";
            lblBaslik.Size = new Size(1023, 35);
            lblBaslik.TabIndex = 0;
            lblBaslik.Text = "Üyelik İşlemleri";
            lblBaslik.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblIstatistikler
            // 
            lblIstatistikler.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            lblIstatistikler.ForeColor = Color.FromArgb(52, 73, 94);
            lblIstatistikler.Location = new Point(20, 55);
            lblIstatistikler.Name = "lblIstatistikler";
            lblIstatistikler.Size = new Size(1023, 25);
            lblIstatistikler.TabIndex = 1;
            lblIstatistikler.Text = "Yükleniyor...";
            lblIstatistikler.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dgvKullanicilar
            // 
            dgvKullanicilar.AllowUserToAddRows = false;
            dgvKullanicilar.AllowUserToDeleteRows = false;
            dgvKullanicilar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKullanicilar.BackgroundColor = Color.White;
            dgvKullanicilar.BorderStyle = BorderStyle.None;
            dgvKullanicilar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKullanicilar.Location = new Point(20, 85);
            dgvKullanicilar.MultiSelect = false;
            dgvKullanicilar.Name = "dgvKullanicilar";
            dgvKullanicilar.ReadOnly = true;
            dgvKullanicilar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKullanicilar.Size = new Size(1000, 400);
            dgvKullanicilar.TabIndex = 1;
            dgvKullanicilar.CellClick += dgvKullanicilar_CellClick;
            // 
            // btnYeniKullanici
            // 
            btnYeniKullanici.BackColor = Color.FromArgb(102, 16, 242);
            btnYeniKullanici.FlatAppearance.BorderSize = 0;
            btnYeniKullanici.FlatStyle = FlatStyle.Flat;
            btnYeniKullanici.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnYeniKullanici.ForeColor = Color.White;
            btnYeniKullanici.Location = new Point(20, 520);
            btnYeniKullanici.Name = "btnYeniKullanici";
            btnYeniKullanici.Size = new Size(150, 40);
            btnYeniKullanici.TabIndex = 2;
            btnYeniKullanici.Text = "Yeni Kullanıcı";
            btnYeniKullanici.UseVisualStyleBackColor = false;
            btnYeniKullanici.Click += btnYeniKullanici_Click;
            // 
            // btnGuncelle
            // 
            btnGuncelle.BackColor = Color.FromArgb(33, 150, 243);
            btnGuncelle.FlatAppearance.BorderSize = 0;
            btnGuncelle.FlatStyle = FlatStyle.Flat;
            btnGuncelle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGuncelle.ForeColor = Color.White;
            btnGuncelle.Location = new Point(190, 520);
            btnGuncelle.Name = "btnGuncelle";
            btnGuncelle.Size = new Size(150, 40);
            btnGuncelle.TabIndex = 3;
            btnGuncelle.Text = "Güncelle";
            btnGuncelle.UseVisualStyleBackColor = false;
            btnGuncelle.Click += btnGuncelle_Click;
            // 
            // btnSil
            // 
            btnSil.BackColor = Color.FromArgb(244, 67, 54);
            btnSil.FlatAppearance.BorderSize = 0;
            btnSil.FlatStyle = FlatStyle.Flat;
            btnSil.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSil.ForeColor = Color.White;
            btnSil.Location = new Point(360, 520);
            btnSil.Name = "btnSil";
            btnSil.Size = new Size(150, 40);
            btnSil.TabIndex = 4;
            btnSil.Text = "Sil";
            btnSil.UseVisualStyleBackColor = false;
            btnSil.Click += btnSil_Click;
            // 
            // btnAktiflestir
            // 
            btnAktiflestir.BackColor = Color.FromArgb(40, 167, 69);
            btnAktiflestir.FlatAppearance.BorderSize = 0;
            btnAktiflestir.FlatStyle = FlatStyle.Flat;
            btnAktiflestir.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAktiflestir.ForeColor = Color.White;
            btnAktiflestir.Location = new Point(530, 520);
            btnAktiflestir.Name = "btnAktiflestir";
            btnAktiflestir.Size = new Size(150, 40);
            btnAktiflestir.TabIndex = 5;
            btnAktiflestir.Text = "Aktifleştir";
            btnAktiflestir.UseVisualStyleBackColor = false;
            btnAktiflestir.Click += btnAktiflestir_Click;
            // 
            // btnYenile
            // 
            btnYenile.BackColor = Color.FromArgb(255, 152, 0);
            btnYenile.FlatAppearance.BorderSize = 0;
            btnYenile.FlatStyle = FlatStyle.Flat;
            btnYenile.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnYenile.ForeColor = Color.White;
            btnYenile.Location = new Point(700, 520);
            btnYenile.Name = "btnYenile";
            btnYenile.Size = new Size(150, 40);
            btnYenile.TabIndex = 6;
            btnYenile.Text = "Yenile";
            btnYenile.UseVisualStyleBackColor = false;
            btnYenile.Click += btnYenile_Click;
            // 
            // btnKapat
            // 
            btnKapat.BackColor = Color.FromArgb(158, 158, 158);
            btnKapat.FlatAppearance.BorderSize = 0;
            btnKapat.FlatStyle = FlatStyle.Flat;
            btnKapat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnKapat.ForeColor = Color.White;
            btnKapat.Location = new Point(870, 520);
            btnKapat.Name = "btnKapat";
            btnKapat.Size = new Size(150, 40);
            btnKapat.TabIndex = 7;
            btnKapat.Text = "Kapat";
            btnKapat.UseVisualStyleBackColor = false;
            btnKapat.Click += btnKapat_Click;
            // 
            // UyelikIslemleriForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1063, 600);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "UyelikIslemleriForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Üyelik İşlemleri";
            panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvKullanicilar).EndInit();
            ResumeLayout(false);
        }

        private Panel panelMain;
        private Label lblBaslik;
        private Label lblIstatistikler;
        private DataGridView dgvKullanicilar;
        private Button btnYeniKullanici;
        private Button btnGuncelle;
        private Button btnSil;
        private Button btnAktiflestir;
        private Button btnYenile;
        private Button btnKapat;

        private void SetupDataGridView()
        {
            // DataGridView sütunlarını ayarla
            kullanicilarTable.Columns.Add("kullanici_id", typeof(int));
            kullanicilarTable.Columns.Add("ad", typeof(string));
            kullanicilarTable.Columns.Add("soyad", typeof(string));
            kullanicilarTable.Columns.Add("tc", typeof(string));
            kullanicilarTable.Columns.Add("telefon", typeof(string));
            kullanicilarTable.Columns.Add("email", typeof(string));
            kullanicilarTable.Columns.Add("rol_adlari", typeof(string));
            kullanicilarTable.Columns.Add("status", typeof(string));

            dgvKullanicilar.DataSource = kullanicilarTable;
            
            // Hücre formatlaması için event ekle
            dgvKullanicilar.CellFormatting += dgvKullanicilar_CellFormatting;
        }

        private async void LoadKullanicilar()
        {
            try
            {
                btnYenile.Enabled = false;
                btnYenile.Text = "Yükleniyor...";

                var response = await apiHelper.GetAllUsersAsync();
                
                if (response is Newtonsoft.Json.Linq.JArray kullaniciArray)
                {
                    kullanicilarTable.Clear();

                    foreach (var kullanici in kullaniciArray)
                    {
                        // Tüm kullanıcıları göster (aktif ve pasif)
                        string rolAdlari = "";
                        if (kullanici["rol_adlari"] != null)
                        {
                            var roller = kullanici["rol_adlari"] as Newtonsoft.Json.Linq.JArray;
                            if (roller != null)
                            {
                                rolAdlari = string.Join(", ", roller.Select(r => r.ToString()));
                            }
                        }

                        // Status değerini kullanıcı dostu metne çevir
                        string statusText = "Aktif";
                        var status = kullanici["status"];
                        if (status != null && Convert.ToInt32(status) == 0)
                        {
                            statusText = "Pasif";
                        }

                        kullanicilarTable.Rows.Add(
                            kullanici["kullanici_id"],
                            kullanici["ad"]?.ToString() ?? "",
                            kullanici["soyad"]?.ToString() ?? "",
                            kullanici["tc"]?.ToString() ?? "",
                            kullanici["telefon"]?.ToString() ?? "",
                            kullanici["email"]?.ToString() ?? "",
                            rolAdlari,
                            statusText
                        );
                    }
                }

                // İstatistikleri hesapla
                int aktifKullanici = 0;
                int pasifKullanici = 0;
                
                foreach (DataRow row in kullanicilarTable.Rows)
                {
                    if (row["status"].ToString() == "Aktif")
                        aktifKullanici++;
                    else
                        pasifKullanici++;
                }
                
                // İstatistikleri göster
                lblIstatistikler.Text = $"📊 Toplam: {kullanicilarTable.Rows.Count} Kullanıcı    •    ✅ Aktif: {aktifKullanici}    •    ❌ Pasif: {pasifKullanici}";
                
                // Status sütununu görünür yap ve başlığını güncelle
                if (dgvKullanicilar.Columns.Contains("status"))
                {
                    dgvKullanicilar.Columns["status"].HeaderText = "Durum";
                    dgvKullanicilar.Columns["status"].Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kullanıcılar yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnYenile.Enabled = true;
                btnYenile.Text = "Yenile";
            }
        }

        private void dgvKullanicilar_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Status sütununu bul (7. sütun)
                int statusColumnIndex = 7;
                if (dgvKullanicilar.Columns.Count > statusColumnIndex)
                {
                    var statusValue = dgvKullanicilar.Rows[e.RowIndex].Cells[statusColumnIndex].Value?.ToString();
                    
                    // Eğer status "Pasif" ise tüm satırı kırmızı yap
                    if (statusValue == "Pasif")
                    {
                        dgvKullanicilar.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                        dgvKullanicilar.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(220, 53, 69); // Kırmızı
                    }
                    else
                    {
                        // Aktif kullanıcılar için normal renk
                        dgvKullanicilar.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                        dgvKullanicilar.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
        }

        private void dgvKullanicilar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvKullanicilar.Rows[e.RowIndex].Selected = true;
            }
        }

        private void btnYeniKullanici_Click(object sender, EventArgs e)
        {
            // Yeni kullanıcı ekleme dialog'u aç (Admin kontrolü ile)
            using (var ekleForm = new KullaniciEkleForm(isAdmin))
            {
                if (ekleForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Yeni kullanıcı başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Listeyi yenile
                    LoadKullanicilar();
                }
            }
        }

        private async void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dgvKullanicilar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen güncellenecek kullanıcıyı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvKullanicilar.SelectedRows[0];
            int kullaniciId = Convert.ToInt32(selectedRow.Cells["kullanici_id"].Value);
            string kullaniciAdi = selectedRow.Cells["ad"].Value.ToString() + " " + selectedRow.Cells["soyad"].Value.ToString();

            // Kullanıcı güncelleme dialog'u aç (Admin kontrolü ile)
            using (var guncelleForm = new KullaniciGuncelleForm(kullaniciId, selectedRow, isAdmin))
            {
                if (guncelleForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show($"'{kullaniciAdi}' adlı kullanıcının bilgileri başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Listeyi yenile
                    LoadKullanicilar();
                }
            }
        }

        private async void btnSil_Click(object sender, EventArgs e)
        {
            if (dgvKullanicilar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek kullanıcıyı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvKullanicilar.SelectedRows[0];
            int kullaniciId = Convert.ToInt32(selectedRow.Cells["kullanici_id"].Value);
            string kullaniciAdi = selectedRow.Cells["ad"].Value.ToString() + " " + selectedRow.Cells["soyad"].Value.ToString();

            var result = MessageBox.Show(
                $"'{kullaniciAdi}' adlı kullanıcıyı silmek istediğinizden emin misiniz?\n\nBu işlem geri alınamaz!",
                "Kullanıcı Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    btnSil.Enabled = false;
                    btnSil.Text = "Siliniyor...";

                    // Soft delete işlemi - özel silme metodu kullan
                    var response = await apiHelper.DeleteUserAsync(kullaniciId);
                    
                    MessageBox.Show("Kullanıcı başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Listeyi yenile
                    LoadKullanicilar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kullanıcı silinirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnSil.Enabled = true;
                    btnSil.Text = "Sil";
                }
            }
        }

        private async void btnAktiflestir_Click(object sender, EventArgs e)
        {
            if (dgvKullanicilar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen aktifleştirilecek kullanıcıyı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvKullanicilar.SelectedRows[0];
            int kullaniciId = Convert.ToInt32(selectedRow.Cells["kullanici_id"].Value);
            string kullaniciAdi = selectedRow.Cells["ad"].Value.ToString() + " " + selectedRow.Cells["soyad"].Value.ToString();
            string durum = selectedRow.Cells["status"].Value.ToString();

            // Sadece pasif kullanıcıları aktifleştir
            if (durum != "Pasif")
            {
                MessageBox.Show("Sadece pasif kullanıcılar aktifleştirilebilir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"'{kullaniciAdi}' adlı kullanıcıyı aktifleştirmek istediğinizden emin misiniz?",
                "Kullanıcı Aktifleştirme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    btnAktiflestir.Enabled = false;
                    btnAktiflestir.Text = "Aktifleştiriliyor...";

                    // Kullanıcıyı aktifleştir (status = 1)
                    var response = await apiHelper.ActivateUserAsync(kullaniciId);
                    
                    MessageBox.Show("Kullanıcı başarıyla aktifleştirildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Listeyi yenile
                    LoadKullanicilar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kullanıcı aktifleştirilirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnAktiflestir.Enabled = true;
                    btnAktiflestir.Text = "Aktifleştir";
                }
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            LoadKullanicilar();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
} 