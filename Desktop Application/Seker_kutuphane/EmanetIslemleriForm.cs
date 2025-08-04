using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Seker_kutuphane
{
    public partial class OduncIslemleriForm : Form
    {
        private ApiHelper apiHelper = null!;
        private string kullaniciAdi = string.Empty;
        private string rol = string.Empty;
        private dynamic? userData;
        private DataTable emanetTable = null!;

        public OduncIslemleriForm(string kullaniciAdi, string rol, dynamic userData = null)
        {
            InitializeComponent();
            this.kullaniciAdi = kullaniciAdi;
            this.rol = rol;
            this.userData = userData;
            this.apiHelper = new ApiHelper();
            InitializeOduncTable();
            LoadOduncler();
        }

        private void InitializeOduncTable()
        {
            emanetTable = new DataTable();
            emanetTable.Columns.Add("EmanetId", typeof(int));
            emanetTable.Columns.Add("KullaniciAdi", typeof(string));
            emanetTable.Columns.Add("KitapAdi", typeof(string));
            emanetTable.Columns.Add("OduncTarihi", typeof(DateTime));
            emanetTable.Columns.Add("BeklenenTeslim", typeof(DateTime));
            emanetTable.Columns.Add("TeslimTarihi", typeof(DateTime));
            emanetTable.Columns.Add("Durum", typeof(string));
            emanetTable.Columns.Add("KullaniciId", typeof(int));
            emanetTable.Columns.Add("KitapId", typeof(int));

            dataGridViewEmanetler.DataSource = emanetTable;
            dataGridViewEmanetler.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private async void LoadOduncler()
        {
            try
            {
                var oduncler = await apiHelper.GetAllOdunclerAsync();
                emanetTable.Clear();

                // Debug: API yanıtını kontrol et
                Console.WriteLine($"API Response Type: {oduncler?.GetType()}");
                Console.WriteLine($"API Response: {oduncler}");

                if (oduncler is Newtonsoft.Json.Linq.JArray oduncArray)
                {
                    Console.WriteLine($"JArray Count: {oduncArray.Count}");

                    // API yanıtını daha detaylı logla
                    Console.WriteLine($"=== API Response Details ===");
                    for (int i = 0; i < oduncArray.Count; i++)
                    {
                        var item = oduncArray[i];
                        Console.WriteLine($"Item {i}: ID={item["odunc_id"]}, TeslimEdildi={item["teslim_edildi"]}, Ad={item["ad"]}, Kitap={item["title"]}");
                    }
                    Console.WriteLine($"=== End API Response Details ===");

                    foreach (var odunc in oduncArray)
                    {
                        Console.WriteLine($"Processing odunc: {odunc}");

                        // Debug: teslim_edildi değerini kontrol et
                        var teslimEdildi = odunc["teslim_edildi"]?.ToString() ?? "0";
                        var oduncId = odunc["odunc_id"]?.ToString() ?? "0";
                        Console.WriteLine($"Odunc ID: {oduncId}, Teslim Edildi: '{teslimEdildi}' (Type: {teslimEdildi.GetType()})");

                        // Sadece iade edilmemiş ödünçleri göster (teslim_edildi = 0)
                        if (teslimEdildi == "1" || teslimEdildi == "True" || teslimEdildi == "true")
                        {
                            Console.WriteLine($"Skipping returned odunc: {oduncId}");
                            continue; // İade edilmiş ödünçleri atla
                        }

                        var row = emanetTable.NewRow();

                        try
                        {
                            // API'deki alan adlarına göre uyarlama
                            row["EmanetId"] = odunc["odunc_id"]?.ToString() != null ? int.Parse(odunc["odunc_id"].ToString()) : 0;
                            row["KullaniciAdi"] = $"{odunc["ad"]?.ToString() ?? ""} {odunc["soyad"]?.ToString() ?? ""}".Trim();
                            row["KitapAdi"] = odunc["title"]?.ToString() ?? "";
                            row["OduncTarihi"] = DateTime.Parse(odunc["odunc_tarihi"]?.ToString() ?? DateTime.Now.ToString());
                            row["BeklenenTeslim"] = DateTime.Parse(odunc["iade_tarihi"]?.ToString() ?? DateTime.Now.AddDays(30).ToString());
                            row["TeslimTarihi"] = DBNull.Value; // Aktif ödünçler için boş
                            row["Durum"] = "İade Edilmedi";
                            row["KullaniciId"] = odunc["kullanici_id"]?.ToString() != null ? int.Parse(odunc["kullanici_id"].ToString()) : 0;
                            row["KitapId"] = odunc["id"]?.ToString() != null ? int.Parse(odunc["id"].ToString()) : 0;
                            emanetTable.Rows.Add(row);
                            Console.WriteLine($"Added active odunc: {row["EmanetId"]} - {row["KullaniciAdi"]} - {row["KitapAdi"]}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing odunc: {ex.Message}");
                        }
                    }
                }
                else if (oduncler is Newtonsoft.Json.Linq.JObject)
                {
                    // Tek bir ödünç objesi olabilir
                    var odunc = (Newtonsoft.Json.Linq.JObject)oduncler;
                    var teslimEdildi = odunc["teslim_edildi"]?.ToString() ?? "0";
                    var oduncId = odunc["odunc_id"]?.ToString() ?? "0";
                    Console.WriteLine($"Single Odunc ID: {oduncId}, Teslim Edildi: '{teslimEdildi}'");

                    if (teslimEdildi != "1" && teslimEdildi != "True" && teslimEdildi != "true") // Sadece iade edilmemiş ödünçleri göster
                    {
                        var row = emanetTable.NewRow();
                        row["EmanetId"] = odunc["odunc_id"]?.ToString() != null ? int.Parse(odunc["odunc_id"].ToString()) : 0;
                        row["KullaniciAdi"] = $"{odunc["ad"]?.ToString() ?? ""} {odunc["soyad"]?.ToString() ?? ""}".Trim();
                        row["KitapAdi"] = odunc["title"]?.ToString() ?? "";
                        row["OduncTarihi"] = DateTime.Parse(odunc["odunc_tarihi"]?.ToString() ?? DateTime.Now.ToString());
                        row["BeklenenTeslim"] = DateTime.Parse(odunc["iade_tarihi"]?.ToString() ?? DateTime.Now.AddDays(30).ToString());
                        row["TeslimTarihi"] = DBNull.Value; // Aktif ödünçler için boş
                        row["Durum"] = "İade Edilmedi";
                        row["KullaniciId"] = odunc["kullanici_id"]?.ToString() != null ? int.Parse(odunc["kullanici_id"].ToString()) : 0;
                        row["KitapId"] = odunc["id"]?.ToString() != null ? int.Parse(odunc["id"].ToString()) : 0;
                        emanetTable.Rows.Add(row);
                    }
                }
                else
                {
                    Console.WriteLine($"Unexpected response type: {oduncler?.GetType()}");
                }

                Console.WriteLine($"Total active odunc rows in table: {emanetTable.Rows.Count}");
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LoadOduncler Error: {ex.Message}");
                MessageBox.Show($"Ödünçler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatistics()
        {
            int toplamOdunc = emanetTable.Rows.Count;
            int aktifOdunc = toplamOdunc; // Artık sadece aktif ödünçler gösteriliyor
            int gecikmisOdunc = 0;

            foreach (DataRow row in emanetTable.Rows)
            {
                var beklenenTeslim = (DateTime)row["BeklenenTeslim"];
                if (DateTime.Now > beklenenTeslim)
                {
                    gecikmisOdunc++;
                }
            }

            lblToplamEmanet.Text = toplamOdunc.ToString();
            lblAktifEmanet.Text = aktifOdunc.ToString();
            lblGecikmisEmanet.Text = gecikmisOdunc.ToString();
        }

        private void btnYeniOdunc_Click(object sender, EventArgs e)
        {
            var yeniOduncForm = new YeniOduncForm(apiHelper);
            
            // Callback ayarla - ödünç oluşturulduğunda listeyi yenile
            yeniOduncForm.OnOduncCreated = () => LoadOduncler();
            
            if (yeniOduncForm.ShowDialog() == DialogResult.OK)
            {
                LoadOduncler();
            }
        }

        private async void btnIadeEt_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmanetler.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen iade edilecek ödünçü seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridViewEmanetler.SelectedRows[0];
            int emanetId = (int)selectedRow.Cells["EmanetId"].Value;
            string kullaniciAdi = selectedRow.Cells["KullaniciAdi"].Value.ToString();
            string kitapAdi = selectedRow.Cells["KitapAdi"].Value.ToString();
            string durum = selectedRow.Cells["Durum"].Value.ToString();

            // Eğer ödünç zaten iade edilmişse uyarı ver 
            if (durum == "İade Edildi")
            {
                MessageBox.Show("Bu ödünç zaten iade edilmiş.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"{kullaniciAdi} adlı kullanıcının '{kitapAdi}' kitabını iade etmek istediğinizden emin misiniz?",
                "Ödünç İade Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    await apiHelper.ReturnEmanetAsync(emanetId);
                    MessageBox.Show("Ödünç başarıyla iade edildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOduncler();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ödünç iade edilirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void txtArama_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtArama.Text))
            {
                LoadOduncler();
                return;
            }

            try
            {
                var aramaSonuclari = await apiHelper.SearchOdunclerAsync(txtArama.Text);
                emanetTable.Clear();

                if (aramaSonuclari is Newtonsoft.Json.Linq.JArray sonucArray)
                {
                    foreach (var odunc in sonucArray)
                    {
                        var row = emanetTable.NewRow();
                        row["EmanetId"] = odunc["odunc_id"]?.ToString() != null ? int.Parse(odunc["odunc_id"].ToString()) : 0;
                        row["KullaniciAdi"] = $"{odunc["ad"]?.ToString() ?? ""} {odunc["soyad"]?.ToString() ?? ""}".Trim();
                        row["KitapAdi"] = odunc["title"]?.ToString() ?? "";
                        row["OduncTarihi"] = DateTime.Parse(odunc["odunc_tarihi"]?.ToString() ?? DateTime.Now.ToString());
                        row["BeklenenTeslim"] = DateTime.Parse(odunc["iade_tarihi"]?.ToString() ?? DateTime.Now.AddDays(30).ToString());
                        row["TeslimTarihi"] = DBNull.Value; // Aktif ödünçler için boş
                        row["Durum"] = "İade Edilmedi";
                        row["KullaniciId"] = odunc["kullanici_id"]?.ToString() != null ? int.Parse(odunc["kullanici_id"].ToString()) : 0;
                        row["KitapId"] = odunc["id"]?.ToString() != null ? int.Parse(odunc["id"].ToString()) : 0;
                        emanetTable.Rows.Add(row);
                    }
                }

                UpdateStatistics();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Arama hatası: {ex.Message}");
            }
        }

        private async void btnYenile_Click(object sender, EventArgs e)
        {
            await Task.Run(() => LoadOduncler());
        }

        // Test butonu için event handler
        private async void btnTestAPI_Click(object sender, EventArgs e)
        {
            try
            {
                var oduncler = await apiHelper.GetAllOdunclerAsync();
                string debugInfo = "=== API Debug Bilgileri ===\n";

                if (oduncler is Newtonsoft.Json.Linq.JArray oduncArray)
                {
                    debugInfo += $"Toplam Ödünç Sayısı: {oduncArray.Count}\n\n";

                    for (int i = 0; i < oduncArray.Count; i++)
                    {
                        var item = oduncArray[i];
                        var id = item["odunc_id"]?.ToString() ?? "N/A";
                        var teslimEdildi = item["teslim_edildi"]?.ToString() ?? "N/A";
                        var ad = item["ad"]?.ToString() ?? "N/A";
                        var kitap = item["title"]?.ToString() ?? "N/A";

                        debugInfo += $"Ödünç {i + 1}:\n";
                        debugInfo += $"  ID: {id}\n";
                        debugInfo += $"  Teslim Edildi: '{teslimEdildi}' (Type: {teslimEdildi.GetType()})\n";
                        debugInfo += $"  Kullanıcı: {ad}\n";
                        debugInfo += $"  Kitap: {kitap}\n\n";
                    }
                }
                else
                {
                    debugInfo += "API yanıtı JArray değil!\n";
                    debugInfo += $"Yanıt tipi: {oduncler?.GetType()}\n";
                    debugInfo += $"Yanıt içeriği: {oduncler}";
                }

                MessageBox.Show(debugInfo, "API Debug Bilgileri", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"API test hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Ana dashboard'a dönüş butonu için event handler
        private void btnAnaSayfa_Click(object sender, EventArgs e)
        {
            // Dashboard'ı tekrar aç
            var dashboard = new Dashboard(kullaniciAdi, rol, userData);
            dashboard.Show();
            this.Close();
        }

        private void dataGridViewEmanetler_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewEmanetler.Columns["Durum"].Index && e.Value != null)
            {
                var cell = dataGridViewEmanetler.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (e.Value.ToString() == "AKTİF")
                {
                    cell.Style.BackColor = Color.LightGreen;
                    cell.Style.ForeColor = Color.DarkGreen;
                }
                else if (e.Value.ToString() == "İADE EDİLDİ")
                {
                    cell.Style.BackColor = Color.LightBlue;
                    cell.Style.ForeColor = Color.DarkBlue;
                }
            }

            if (e.ColumnIndex == dataGridViewEmanetler.Columns["BeklenenTeslim"].Index && e.Value != null)
            {
                var beklenenTeslim = (DateTime)e.Value;
                var oduncTarihi = (DateTime)dataGridViewEmanetler.Rows[e.RowIndex].Cells["OduncTarihi"].Value;
                var durum = dataGridViewEmanetler.Rows[e.RowIndex].Cells["Durum"].Value.ToString();

                if (durum == "AKTİF" && DateTime.Now > beklenenTeslim)
                {
                    dataGridViewEmanetler.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                }
            }
        }

        private void lblToplamEmanetTitle_Click(object sender, EventArgs e)
        {

        }
    }
} 