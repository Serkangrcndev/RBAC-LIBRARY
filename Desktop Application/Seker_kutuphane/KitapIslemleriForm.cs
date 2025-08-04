using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Seker_kutuphane
{
    public partial class KitapIslemleriForm : Form
    {
        private ApiHelper apiHelper;
        private DataTable kitaplarTable;

        public KitapIslemleriForm()
        {
            InitializeComponent();
            this.apiHelper = new ApiHelper();
            this.kitaplarTable = new DataTable();
            SetupDataGridView();
            LoadKitaplar();
        }

        private void InitializeComponent()
        {
            panelMain = new Panel();
            lblBaslik = new Label();
            lblIstatistikler = new Label();
            dgvKitaplar = new DataGridView();
            btnYeniKitap = new Button();
            btnGuncelle = new Button();
            btnSil = new Button();
            btnYenile = new Button();
            btnKapat = new Button();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKitaplar).BeginInit();
            SuspendLayout();

            // panelMain
            panelMain.BackColor = Color.FromArgb(245, 245, 245);
            panelMain.Controls.Add(lblBaslik);
            panelMain.Controls.Add(lblIstatistikler);
            panelMain.Controls.Add(dgvKitaplar);
            panelMain.Controls.Add(btnYeniKitap);
            panelMain.Controls.Add(btnGuncelle);
            panelMain.Controls.Add(btnSil);
            panelMain.Controls.Add(btnYenile);
            panelMain.Controls.Add(btnKapat);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1063, 600);
            panelMain.TabIndex = 0;

            // lblBaslik
            lblBaslik.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(0, 128, 0);
            lblBaslik.Location = new Point(20, 15);
            lblBaslik.Name = "lblBaslik";
            lblBaslik.Size = new Size(1023, 35);
            lblBaslik.TabIndex = 0;
            lblBaslik.Text = "Kitap İşlemleri";
            lblBaslik.TextAlign = ContentAlignment.MiddleCenter;

            // lblIstatistikler
            lblIstatistikler.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            lblIstatistikler.ForeColor = Color.FromArgb(52, 73, 94);
            lblIstatistikler.Location = new Point(20, 55);
            lblIstatistikler.Name = "lblIstatistikler";
            lblIstatistikler.Size = new Size(1023, 25);
            lblIstatistikler.TabIndex = 1;
            lblIstatistikler.Text = "Yükleniyor...";
            lblIstatistikler.TextAlign = ContentAlignment.MiddleCenter;

            // dgvKitaplar
            dgvKitaplar.AllowUserToAddRows = false;
            dgvKitaplar.AllowUserToDeleteRows = false;
            dgvKitaplar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKitaplar.BackgroundColor = Color.White;
            dgvKitaplar.BorderStyle = BorderStyle.None;
            dgvKitaplar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKitaplar.Location = new Point(20, 85);
            dgvKitaplar.MultiSelect = false;
            dgvKitaplar.Name = "dgvKitaplar";
            dgvKitaplar.ReadOnly = true;
            dgvKitaplar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKitaplar.Size = new Size(1000, 400);
            dgvKitaplar.TabIndex = 1;
            dgvKitaplar.CellClick += dgvKitaplar_CellClick;

            // btnYeniKitap
            btnYeniKitap.BackColor = Color.FromArgb(102, 16, 242);
            btnYeniKitap.FlatAppearance.BorderSize = 0;
            btnYeniKitap.FlatStyle = FlatStyle.Flat;
            btnYeniKitap.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnYeniKitap.ForeColor = Color.White;
            btnYeniKitap.Location = new Point(20, 520);
            btnYeniKitap.Name = "btnYeniKitap";
            btnYeniKitap.Size = new Size(150, 40);
            btnYeniKitap.TabIndex = 2;
            btnYeniKitap.Text = "Yeni Kitap";
            btnYeniKitap.UseVisualStyleBackColor = false;
            btnYeniKitap.Click += btnYeniKitap_Click;

            // btnGuncelle
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

            // btnSil
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

            // btnYenile
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

            // btnKapat
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

            // KitapIslemleriForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1063, 600);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "KitapIslemleriForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Kitap İşlemleri";
            panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvKitaplar).EndInit();
            ResumeLayout(false);
        }

        private Panel panelMain;
        private Label lblBaslik;
        private Label lblIstatistikler;
        private DataGridView dgvKitaplar;
        private Button btnYeniKitap;
        private Button btnGuncelle;
        private Button btnSil;
        private Button btnYenile;
        private Button btnKapat;

        private void SetupDataGridView()
        {
            // DataGridView sütunlarını ayarla
            kitaplarTable.Columns.Add("kitap_id", typeof(int));
            kitaplarTable.Columns.Add("kitap_adi", typeof(string));
            kitaplarTable.Columns.Add("yazar", typeof(string));
            kitaplarTable.Columns.Add("yayinevi", typeof(string));
            kitaplarTable.Columns.Add("yayin_tarihi", typeof(string)); // Tarih sütunu string olarak
            kitaplarTable.Columns.Add("kitap_adet", typeof(int));
            kitaplarTable.Columns.Add("mevcut", typeof(int));
            kitaplarTable.Columns.Add("sayfa_sayisi", typeof(object)); // Object olarak tanımla (null değer alabilir)

            dgvKitaplar.DataSource = kitaplarTable;
        }

        private async void LoadKitaplar()
        {
            try
            {
                btnYenile.Enabled = false;
                btnYenile.Text = "Yükleniyor...";

                var response = await apiHelper.GetAllBooksAsync();
                
                if (response is Newtonsoft.Json.Linq.JArray kitapArray)
                {
                    kitaplarTable.Clear();

                    foreach (var kitap in kitapArray)
                    {
                        // Sadece publishYear boşsa uyarı göster (sessizce)
                        if (kitap["publishYear"] == null || kitap["publishYear"].ToString() == "")
                        {
                            // Sessizce devam et, uyarı gösterme
                        }
                        
                        // Doğru alan isimlerini kullan
                        var kitapId = kitap["id"];
                        var kitapAdi = kitap["title"];
                        var yazar = kitap["author"];
                        var yayinevi = kitap["yayinevi"];
                        
                        // Tarih alanını kontrol et - API'de publishYear olarak geliyor
                        var tarih = kitap["publishYear"] ?? kitap["basim_yili"];
                        int yil = 0;
                        

                        
                        if (tarih != null)
                        {
                            try
                            {
                                // JToken'ı string'e çevir
                                string tarihString = tarih.ToString();
                                
                                // Önce DateTime olarak parse etmeyi dene
                                if (DateTime.TryParse(tarihString, out DateTime parsedDate))
                                {
                                    yil = parsedDate.Year;
                                }
                                // DateTime olmazsa integer olarak dene
                                else if (int.TryParse(tarihString, out int yilInt))
                                {
                                    yil = yilInt;
                                }
                                // Parse edilemezse 0 olarak bırak
                                else
                                {
                                    yil = 0;
                                }
                            }
                            catch (Exception ex)
                            {
                                // Hata durumunda 0 olarak bırak
                                yil = 0;
                            }
                        }
                        else
                        {
                            // Tarih alanı null ise 0 olarak bırak
                            yil = 0;
                        }
                        
                        var kitapAdet = kitap["kitap_adet"] ?? kitap["quantity"] ?? kitap["kitapAdet"];
                        var mevcut = kitap["mevcut"] ?? kitap["available"];
                        var sayfaSayisi = kitap["pageCount"] ?? kitap["sayfa_sayisi"] ?? kitap["sayfaSayisi"] ?? kitap["pages"];
                        
                        // Sayfa sayısını güvenli bir şekilde parse et
                        object sayfaSayisiObj = DBNull.Value; // Varsayılan olarak DBNull
                        if (sayfaSayisi != null && !string.IsNullOrEmpty(sayfaSayisi.ToString()))
                        {
                            if (int.TryParse(sayfaSayisi.ToString(), out int parsedSayfa))
                            {
                                sayfaSayisiObj = parsedSayfa;
                            }
                        }
                        
                        // Tarih formatını hazırla
                        string tarihGosterimi = "";
                        if (tarih != null && !string.IsNullOrEmpty(tarih.ToString()))
                        {
                            if (DateTime.TryParse(tarih.ToString(), out DateTime parsedDate))
                            {
                                tarihGosterimi = parsedDate.ToString("dd.MM.yyyy"); // Türkçe tarih formatı
                            }
                            else if (int.TryParse(tarih.ToString(), out int yilInt))
                            {
                                tarihGosterimi = $"01.01.{yilInt}"; // Yıl varsa 1 Ocak olarak göster
                            }
                        }
                        
                        kitaplarTable.Rows.Add(
                            kitapId,
                            kitapAdi?.ToString() ?? "",
                            yazar?.ToString() ?? "",
                            yayinevi?.ToString() ?? "",
                            tarihGosterimi, // Tam tarih formatında
                            kitapAdet,
                            mevcut,
                            sayfaSayisiObj
                        );
                    }
                }

                // İstatistikleri hesapla
                int toplamKitap = kitaplarTable.Rows.Count;
                int toplamAdet = 0;
                int toplamMevcut = 0;
                
                foreach (DataRow row in kitaplarTable.Rows)
                {
                    toplamAdet += Convert.ToInt32(row["kitap_adet"]);
                    toplamMevcut += Convert.ToInt32(row["mevcut"]);
                }
                
                // İstatistikleri göster
                lblIstatistikler.Text = $"📚 Toplam: {toplamKitap} Kitap    •    📖 Toplam Adet: {toplamAdet}    •    ✅ Mevcut: {toplamMevcut}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kitaplar yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnYenile.Enabled = true;
                btnYenile.Text = "Yenile";
            }
        }

        private void dgvKitaplar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvKitaplar.Rows[e.RowIndex].Selected = true;
            }
        }

        private void btnYeniKitap_Click(object sender, EventArgs e)
        {
            var kitapEkleForm = new KitapEkleForm();
            kitapEkleForm.ShowDialog();
            LoadKitaplar(); // Listeyi yenile
        }

        private async void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dgvKitaplar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen güncellenecek kitabı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvKitaplar.SelectedRows[0];
            int kitapId = Convert.ToInt32(selectedRow.Cells["kitap_id"].Value);
            
            var kitapGuncelleForm = new KitapGuncelleForm(kitapId, selectedRow);
            kitapGuncelleForm.ShowDialog();
            LoadKitaplar(); // Listeyi yenileme
        }

        private async void btnSil_Click(object sender, EventArgs e)
        {
            if (dgvKitaplar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek kitabı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvKitaplar.SelectedRows[0];
            int kitapId = Convert.ToInt32(selectedRow.Cells["kitap_id"].Value);
            string kitapAdi = selectedRow.Cells["kitap_adi"].Value.ToString();

            var result = MessageBox.Show(
                $"'{kitapAdi}' adlı kitabı silmek istediğinizden emin misiniz?",
                "Kitap Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    btnSil.Enabled = false;
                    btnSil.Text = "Siliniyor...";

                    await apiHelper.DeleteBookAsync(kitapId);
                    
                    MessageBox.Show("Kitap başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadKitaplar(); // Listeyi yenile
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kitap silinirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnSil.Enabled = true;
                    btnSil.Text = "Sil";
                }
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            LoadKitaplar();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

} 
