using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Seker_kutuphane
{
    public partial class KitaplarimForm : Form
    {
        private string kullaniciAdi;
        private string rol;
        private dynamic userData;
        private const int MAX_KITAP_SAYISI = 3;

        public KitaplarimForm(string kullaniciAdi, string rol, dynamic userData)
        {
            InitializeComponent();
            this.kullaniciAdi = kullaniciAdi;
            this.rol = rol;
            this.userData = userData;
            LoadKitaplarim();
        }

        private async void LoadKitaplarim()
        {
            try
            {
                // API'den kitapları çek
                var kitaplarim = await GetKitaplarimFromAPI();
                
                // İstatistikleri güncelle
                UpdateStats(kitaplarim.Count);
                
                if (kitaplarim.Count == 0)
                {
                    // Boş durum göster
                    ShowEmptyState();
                }
                else
                {
                    // Kitapları göster
                    ShowBooksList(kitaplarim);
                }
            }
            catch (Exception ex)
            {
                // 404 hatası durumunda sessizce boş liste göster
                if (ex.Message.Contains("404") || ex.Message.Contains("Not Found"))
                {
                    UpdateStats(0);
                    ShowEmptyState();
                }
                else
                {
                    MessageBox.Show($"Kitaplarınız yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblBilgi.Text = "Kitaplarınız yüklenirken hata oluştu.";
                    lblBilgi.Visible = true;
                    dgvKitaplarim.Visible = false;
                    panelEmptyState.Visible = false;
                }
            }
        }

        private async Task<List<KitapEmanet>> GetKitaplarimFromAPI()
        {
            try
            {
                // Kullanıcı ID kontrolü
                if (userData?.kullanici_id == null)
                {
                    MessageBox.Show("Kullanıcı bilgileri alınamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new List<KitapEmanet>();
                }

                int kullaniciId;
                if (!int.TryParse(userData.kullanici_id.ToString(), out kullaniciId))
                {
                    MessageBox.Show("Geçersiz kullanıcı ID.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new List<KitapEmanet>();
                }

                Console.WriteLine($"GetKitaplarimFromAPI - Kullanıcı ID: {kullaniciId}");

                ApiHelper api = new ApiHelper();
                var response = await api.GetKitaplarimAsync(kullaniciId);
                
                Console.WriteLine($"GetKitaplarimFromAPI - API Response: {JsonConvert.SerializeObject(response)}");
                
                if (response == null)
                {
                    return new List<KitapEmanet>();
                }
                
                var kitaplarim = new List<KitapEmanet>();
                
                // API'den gelen veriyi parse et - yeni API yapısına göre
                if (response != null)
                {
                    // Response direkt olarak kitap listesi olabilir
                    var kitapListesi = response as Newtonsoft.Json.Linq.JArray ?? response.kitaplar as Newtonsoft.Json.Linq.JArray;
                    
                    if (kitapListesi != null && kitapListesi.Count > 0)
                    {
                        foreach (var kitap in kitapListesi)
                        {
                            kitaplarim.Add(new KitapEmanet
                            {
                                KitapAdi = kitap["kitap_adi"]?.ToString() ?? "",
                                Yazar = kitap["yazar"]?.ToString() ?? "",
                                ISBN = kitap["isbn"]?.ToString() ?? "",
                                OduncAlmaTarihi = kitap["verilis_tarihi"] != null ? 
                                    DateTime.Parse(kitap["verilis_tarihi"].ToString()) : DateTime.Now,
                                GeriVermeTarihi = kitap["geri_verme_tarihi"] != null ? 
                                    DateTime.Parse(kitap["geri_verme_tarihi"].ToString()) : DateTime.Now.AddDays(30),
                                Durum = kitap["durum"]?.ToString() ?? "Ödünç Alındı"
                            });
                        }
                    }
                }
                
                return kitaplarim;
            }
            catch (Exception ex)
            {
                // API endpoint'i bulunamadığında sessizce boş liste döndür
                if (ex.Message.Contains("404") || ex.Message.Contains("Not Found"))
                {
                    return new List<KitapEmanet>();
                }
                
                MessageBox.Show($"API'den veri alınırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<KitapEmanet>();
            }
        }

        private void UpdateStats(int kitapSayisi)
        {
            lblKitapSayisi.Text = $"Ödünç Alınan: {kitapSayisi}";
            lblMaxKitap.Text = $"Maksimum: {MAX_KITAP_SAYISI} kitap";
            
            // Progress bar'ı güncelle
            int progressValue = (int)((double)kitapSayisi / MAX_KITAP_SAYISI * 100);
            progressBar.Value = Math.Min(progressValue, 100);
            
            // Başlığı güncelle
            lblBaslik.Text = $"📚 Kitaplarım ({kitapSayisi}/{MAX_KITAP_SAYISI})";
        }

        private void ShowEmptyState()
        {
            panelEmptyState.Visible = true;
            dgvKitaplarim.Visible = false;
            lblBilgi.Visible = false;
        }

        private void ShowBooksList(List<KitapEmanet> kitaplarim)
        {
            panelEmptyState.Visible = false;
            dgvKitaplarim.Visible = true;
            lblBilgi.Visible = false;
            
            dgvKitaplarim.DataSource = kitaplarim;
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            try
            {
                if (dgvKitaplarim.Columns.Count > 0)
                {
                    // Kitap Adı sütunu
                    if (dgvKitaplarim.Columns.Contains("KitapAdi"))
                    {
                        dgvKitaplarim.Columns["KitapAdi"].HeaderText = "📖 Kitap Adı";
                        dgvKitaplarim.Columns["KitapAdi"].Width = 250;
                        dgvKitaplarim.Columns["KitapAdi"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    }

                    // Yazar sütunu
                    if (dgvKitaplarim.Columns.Contains("Yazar"))
                    {
                        dgvKitaplarim.Columns["Yazar"].HeaderText = "✍️ Yazar";
                        dgvKitaplarim.Columns["Yazar"].Width = 180;
                    }

                    // ISBN sütunu
                    if (dgvKitaplarim.Columns.Contains("ISBN"))
                    {
                        dgvKitaplarim.Columns["ISBN"].HeaderText = "📋 ISBN";
                        dgvKitaplarim.Columns["ISBN"].Width = 120;
                    }

                    // Ödünç Alma Tarihi sütunu
                    if (dgvKitaplarim.Columns.Contains("OduncAlmaTarihi"))
                    {
                        dgvKitaplarim.Columns["OduncAlmaTarihi"].HeaderText = "📅 Ödünç Alma";
                        dgvKitaplarim.Columns["OduncAlmaTarihi"].Width = 120;
                        dgvKitaplarim.Columns["OduncAlmaTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
                        dgvKitaplarim.Columns["OduncAlmaTarihi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    // Geri Verme Tarihi sütunu
                    if (dgvKitaplarim.Columns.Contains("GeriVermeTarihi"))
                    {
                        dgvKitaplarim.Columns["GeriVermeTarihi"].HeaderText = "⏰ Geri Verme";
                        dgvKitaplarim.Columns["GeriVermeTarihi"].Width = 120;
                        dgvKitaplarim.Columns["GeriVermeTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy";
                        dgvKitaplarim.Columns["GeriVermeTarihi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    // Durum sütunu
                    if (dgvKitaplarim.Columns.Contains("Durum"))
                    {
                        dgvKitaplarim.Columns["Durum"].HeaderText = "📊 Durum";
                        dgvKitaplarim.Columns["Durum"].Width = 100;
                        dgvKitaplarim.Columns["Durum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    // Header stilleri
                    dgvKitaplarim.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 128, 0);
                    dgvKitaplarim.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgvKitaplarim.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    dgvKitaplarim.ColumnHeadersHeight = 45;

                    // Satır stilleri
                    dgvKitaplarim.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
                    dgvKitaplarim.DefaultCellStyle.BackColor = Color.White;
                    dgvKitaplarim.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 128, 0);
                    dgvKitaplarim.DefaultCellStyle.SelectionForeColor = Color.White;
                    dgvKitaplarim.RowTemplate.Height = 35;
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda sessizce devam et
                Console.WriteLine($"DataGridView ayarlanırken hata: {ex.Message}");
            }
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            LoadKitaplarim();
        }

        public class KitapEmanet
        {
            public string KitapAdi { get; set; } = "";
            public string Yazar { get; set; } = "";
            public string ISBN { get; set; } = "";
            public DateTime OduncAlmaTarihi { get; set; }
            public DateTime GeriVermeTarihi { get; set; }
            public string Durum { get; set; } = "";
        }
    }
} 