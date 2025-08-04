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
    public partial class YeniOduncForm : Form
    {
        private ApiHelper apiHelper = null!;
        private DataTable kullanicilarTable = null!;
        private DataTable kitaplarTable = null!;
        
        // Ana formu yenilemek için callback
        public Action? OnOduncCreated { get; set; }

        public YeniOduncForm(ApiHelper apiHelper)
        {
            InitializeComponent();
            this.apiHelper = apiHelper;
            Console.WriteLine("=== YeniOduncForm Constructor Başladı ===");
            InitializeTables();
            LoadKullanicilar();
            Console.WriteLine("LoadSampleBooks çağrılıyor...");
            _ = LoadSampleBooks();
            Console.WriteLine("=== YeniOduncForm Constructor Tamamlandı ===");
        }

        private void InitializeTables()
        {
            // Kullanıcılar tablosu
            kullanicilarTable = new DataTable();
            kullanicilarTable.Columns.Add("KullaniciId", typeof(int));
            kullanicilarTable.Columns.Add("AdSoyad", typeof(string));
            kullanicilarTable.Columns.Add("TC", typeof(string));
            kullanicilarTable.Columns.Add("Email", typeof(string));

            // Kitaplar tablosu
            kitaplarTable = new DataTable();
            kitaplarTable.Columns.Add("KitapId", typeof(int));
            kitaplarTable.Columns.Add("KitapAdi", typeof(string));
            kitaplarTable.Columns.Add("Yazar", typeof(string));
            kitaplarTable.Columns.Add("Stok", typeof(int));
            kitaplarTable.Columns.Add("Yayinevi", typeof(string));
        }

        private async void LoadKullanicilar()
        {
            try
            {
                var kullanicilar = await apiHelper.GetAllUsersAsync();
                kullanicilarTable.Clear();

                Console.WriteLine($"Kullanıcılar API Response: {kullanicilar}");

                if (kullanicilar is Newtonsoft.Json.Linq.JArray kullaniciArray)
                {
                    Console.WriteLine($"Kullanıcı sayısı: {kullaniciArray.Count}");
                    foreach (var kullanici in kullaniciArray)
                    {
                        try
                        {
                            var row = kullanicilarTable.NewRow();
                            row["KullaniciId"] = kullanici["kullanici_id"]?.ToString() != null ? int.Parse(kullanici["kullanici_id"].ToString()) : 0;
                            row["AdSoyad"] = $"{kullanici["ad"]?.ToString() ?? ""} {kullanici["soyad"]?.ToString() ?? ""}".Trim();
                            row["TC"] = kullanici["tc"]?.ToString() ?? "";
                            row["Email"] = kullanici["email"]?.ToString() ?? "";
                            kullanicilarTable.Rows.Add(row);
                            Console.WriteLine($"Added user: {row["AdSoyad"]} - {row["TC"]}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing user: {ex.Message}");
                        }
                    }
                }

                Console.WriteLine($"Total users in table: {kullanicilarTable.Rows.Count}");
                comboBoxKullanici.DataSource = kullanicilarTable;
                comboBoxKullanici.DisplayMember = "AdSoyad";
                comboBoxKullanici.ValueMember = "KullaniciId";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LoadKullanicilar Error: {ex.Message}");
                MessageBox.Show($"Kullanıcılar yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // KitapAramaForm'daki LoadSampleBooks mantığını birebir kopyala
        private async Task LoadSampleBooks()
        {
            try
            {
                // API'den kitapları çek
                var books = await apiHelper.GetAllBooksAsync();
                
                // Veriyi JArray'e çevir
                Newtonsoft.Json.Linq.JArray bookArray = null!;
                
                if (books is Newtonsoft.Json.Linq.JArray jArray)
                {
                    bookArray = jArray;
                }
                else
                {
                    kitaplarTable.Clear();
                    comboBoxKitap.DataSource = null;
                    return;
                }
                
                // DataTable'ı temizle
                kitaplarTable.Clear();
                
                // Her kitabı işle
                int basariliKitap = 0;
                foreach (var book in bookArray)
                {
                    try
                    {
                        // Kitap verilerini al
                        int kitapId = 0;
                        string kitapAdi = "";
                        string yazar = "";
                        string yayinevi = "";
                        int stok = 0;
                        
                        // Güvenli şekilde değerleri al
                        if (book["id"] != null) kitapId = int.Parse(book["id"].ToString());
                        if (book["title"] != null) kitapAdi = book["title"].ToString();
                        if (book["author"] != null) yazar = book["author"].ToString();
                        if (book["yayinevi"] != null) yayinevi = book["yayinevi"].ToString();
                        
                        // mevcut alanı boolean olabilir, güvenli şekilde işle
                        if (book["mevcut"] != null)
                        {
                            string mevcutStr = book["mevcut"].ToString().ToLower();
                            if (mevcutStr == "true" || mevcutStr == "1")
                                stok = 1;
                            else if (mevcutStr == "false" || mevcutStr == "0")
                                stok = 0;
                            else
                                int.TryParse(mevcutStr, out stok);
                        }
                        
                        // DataTable'a ekle
                        var row = kitaplarTable.NewRow();
                        row["KitapId"] = kitapId;
                        row["KitapAdi"] = kitapAdi;
                        row["Yazar"] = yazar;
                        row["Stok"] = stok;
                        row["Yayinevi"] = yayinevi;
                        
                        kitaplarTable.Rows.Add(row);
                        basariliKitap++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Kitap işlenirken hata: {ex.Message}");
                    }
                }
                
                // ComboBox'a bağla
                if (basariliKitap > 0)
                {
                    comboBoxKitap.DataSource = kitaplarTable;
                    comboBoxKitap.DisplayMember = "KitapAdi";
                    comboBoxKitap.ValueMember = "KitapId";
                }
                else
                {
                    comboBoxKitap.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LoadSampleBooks HATA: {ex.Message}");
                kitaplarTable.Clear();
                comboBoxKitap.DataSource = null;
            }
        }

        private void comboBoxKitap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxKitap.SelectedItem != null)
            {
                var selectedRow = (DataRowView)comboBoxKitap.SelectedItem;
                int stok = (int)selectedRow["Stok"];
                string yazar = selectedRow["Yazar"].ToString();
                string yayinevi = selectedRow["Yayinevi"].ToString();

                lblStok.Text = $"Stok: {stok}";
                lblYazar.Text = $"Yazar: {yazar}";
                lblYayinevi.Text = $"Yayınevi: {yayinevi}";

                // Stok kontrolü
                if (stok <= 0)
                {
                    btnOduncVer.Enabled = false;
                    lblStok.ForeColor = Color.Red;
                    lblStok.Text += " (Stokta kitap yok!)";
                }
                else
                {
                    btnOduncVer.Enabled = true;
                    lblStok.ForeColor = Color.Green;
                }
            }
        }

        private async void btnOduncVer_Click(object sender, EventArgs e)
        {
            if (comboBoxKullanici.SelectedItem == null || comboBoxKitap.SelectedItem == null)
            {
                MessageBox.Show("Lütfen kullanıcı ve kitap seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var kullaniciRow = (DataRowView)comboBoxKullanici.SelectedItem;
            var kitapRow = (DataRowView)comboBoxKitap.SelectedItem;

            int kullaniciId = (int)kullaniciRow["KullaniciId"];
            int kitapId = (int)kitapRow["KitapId"];
            int stok = (int)kitapRow["Stok"];
            
            // Seçilen kitap bilgilerini logla
            string kitapAdi = kitapRow["KitapAdi"].ToString();
            Console.WriteLine($"Seçilen Kitap - ID: {kitapId}, Ad: {kitapAdi}, Stok: {stok}");

            if (stok <= 0)
            {
                MessageBox.Show("Seçilen kitabın stokta kopyası bulunmamaktadır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ödünç süresi 30 gün
            DateTime oduncTarihi = DateTime.Now;
            DateTime iadeTarihi = oduncTarihi.AddDays(30);

            var oduncData = new
            {
                kullanici_id = kullaniciId,
                kitap_id = kitapId,
                odunc_tarihi = oduncTarihi.ToString("yyyy-MM-dd"),
                iade_tarihi = iadeTarihi.ToString("yyyy-MM-dd"),
                durum = "AKTİF"
            };

            try
            {
                Console.WriteLine($"btnOduncVer_Click - Ödünç verisi: {JsonConvert.SerializeObject(oduncData)}");
                
                var result = await apiHelper.CreateOduncAsync(oduncData);
                Console.WriteLine($"btnOduncVer_Click - API sonucu: {JsonConvert.SerializeObject(result)}");
                
                MessageBox.Show("Ödünç başarıyla oluşturuldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Ana formu yenile
                OnOduncCreated?.Invoke();
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"btnOduncVer_Click - Hata: {ex.Message}");
                Console.WriteLine($"btnOduncVer_Click - Stack Trace: {ex.StackTrace}");
                MessageBox.Show($"Ödünç oluşturulurken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtAramaKullanici_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAramaKullanici.Text))
                {
                    comboBoxKullanici.DataSource = kullanicilarTable;
                }
                else
                {
                    var filteredTable = kullanicilarTable.Clone();
                    var searchTerm = txtAramaKullanici.Text.ToLower();
                    
                    foreach (DataRow row in kullanicilarTable.Rows)
                    {
                        var adSoyad = row["AdSoyad"].ToString().ToLower();
                        var tc = row["TC"].ToString().ToLower();
                        
                        if (adSoyad.Contains(searchTerm) || tc.Contains(searchTerm))
                        {
                            filteredTable.ImportRow(row);
                        }
                    }
                    
                    comboBoxKullanici.DataSource = filteredTable;
                    comboBoxKullanici.DisplayMember = "AdSoyad";
                    comboBoxKullanici.ValueMember = "KullaniciId";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kullanıcı arama hatası: {ex.Message}");
            }
        }

        // Eski karmaşık arama metodu - Artık kullanılmıyor
        private List<object> FilterBooks(List<object> allBooks, string searchTerm, string filterType)
        {
            var filteredBooks = new List<object>();
            
            Console.WriteLine($"FilterBooks called with {allBooks.Count} books, searchTerm: '{searchTerm}', filterType: '{filterType}'");
            
            foreach (var book in allBooks)
            {
                bool matches = false;
                
                try
                {
                    // JToken olarak parse et
                    var bookToken = Newtonsoft.Json.Linq.JToken.FromObject(book);
                    
                    // Debug: İlk kitabın tüm alanlarını yazdır
                    if (filteredBooks.Count == 0)
                    {
                        Console.WriteLine("=== FIRST BOOK STRUCTURE ===");
                        Console.WriteLine($"Book object type: {book.GetType()}");
                        Console.WriteLine($"BookToken type: {bookToken.GetType()}");
                        
                        if (bookToken is Newtonsoft.Json.Linq.JObject jObj)
                        {
                            Console.WriteLine("All properties:");
                            foreach (var prop in jObj.Properties())
                            {
                                Console.WriteLine($"  {prop.Name}: {prop.Value} (Type: {prop.Value?.GetType()})");
                            }
                        }
                        else
                        {
                            Console.WriteLine("BookToken is not JObject, it's: " + bookToken.GetType());
                        }
                        Console.WriteLine("=== END FIRST BOOK STRUCTURE ===");
                    }
                    
                    // API'den gelen alan adlarını kontrol et
                    string kitapAdi = "";
                    string yazar = "";
                    string yayinevi = "";
                    string yil = "";
                    
                    // API'den gelen alan adlarını doğru eşleştir (API'de title, author, yayinevi, publishYear kullanılıyor)
                    if (bookToken["title"] != null) kitapAdi = bookToken["title"].ToString();
                    else if (bookToken["kitap_adi"] != null) kitapAdi = bookToken["kitap_adi"].ToString();
                    else if (bookToken["kitapAdi"] != null) kitapAdi = bookToken["kitapAdi"].ToString();
                    
                    if (bookToken["author"] != null) yazar = bookToken["author"].ToString();
                    else if (bookToken["yazar"] != null) yazar = bookToken["yazar"].ToString();
                    else if (bookToken["yazar_adi"] != null) yazar = bookToken["yazar_adi"].ToString();
                    else if (bookToken["yazarAdi"] != null) yazar = bookToken["yazarAdi"].ToString();
                    
                    if (bookToken["yayinevi"] != null) yayinevi = bookToken["yayinevi"].ToString();
                    else if (bookToken["publisher"] != null) yayinevi = bookToken["publisher"].ToString();
                    else if (bookToken["yayin_evi"] != null) yayinevi = bookToken["yayin_evi"].ToString();
                    
                    if (bookToken["publishYear"] != null) yil = bookToken["publishYear"].ToString();
                    else if (bookToken["yil"] != null) yil = bookToken["yil"].ToString();
                    else if (bookToken["year"] != null) yil = bookToken["year"].ToString();
                    else if (bookToken["yayin_yili"] != null) yil = bookToken["yayin_yili"].ToString();
                    
                    Console.WriteLine($"  Extracted values - Kitap: '{kitapAdi}', Yazar: '{yazar}', Yayınevi: '{yayinevi}', Yıl: '{yil}'");
                    
                    switch (filterType)
                    {
                        case "kitap_adi":
                            matches = kitapAdi.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
                            Console.WriteLine($"  Kitap adı arama: '{kitapAdi}' contains '{searchTerm}' = {matches}");
                            break;
                        case "yazar":
                            matches = yazar.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
                            Console.WriteLine($"  Yazar arama: '{yazar}' contains '{searchTerm}' = {matches}");
                            break;
                        case "yil":
                            matches = yil.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
                            Console.WriteLine($"  Yıl arama: '{yil}' contains '{searchTerm}' = {matches}");
                            break;
                        case "yayinevi":
                            matches = yayinevi.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
                            Console.WriteLine($"  Yayınevi arama: '{yayinevi}' contains '{searchTerm}' = {matches}");
                            break;
                        default:
                            // Genel arama - tüm alanlarda ara
                            matches = kitapAdi.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                    yazar.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                    yayinevi.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                    yil.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
                            Console.WriteLine($"  Genel arama - Matches: {matches}");
                            break;
                    }
                    
                    if (matches)
                    {
                        filteredBooks.Add(book);
                        Console.WriteLine($"  ✓ Kitap eklendi: {kitapAdi}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error filtering book: {ex.Message}");
                }
            }
            
            Console.WriteLine($"FilterBooks completed. Found {filteredBooks.Count} matching books.");
            return filteredBooks;
        }

        // KitapAramaForm'daki PerformSearch mantığını birebir kopyala
        private async void PerformSearch()
        {
            string searchTerm = "";
            string filterType = "";
            
            // Arama türüne göre parametreleri belirle (genel arama için)
            searchTerm = txtAramaKitap.Text.Trim();
            filterType = "genel"; // Genel arama
            
            Console.WriteLine($"PerformSearch called with term: '{searchTerm}', filter: '{filterType}'");
            
            if (string.IsNullOrEmpty(searchTerm))
            {
                // Arama terimi boşsa tüm kitapları göster
                Console.WriteLine("Search term is empty, loading all books");
                await LoadSampleBooks();
                return;
            }

            try
            {
                Console.WriteLine($"Loading all books and filtering by: '{searchTerm}' in '{filterType}'");
                
                // Önce tüm kitapları çek
                var allBooks = await apiHelper.GetAllBooksAsync();
                
                Console.WriteLine($"All books loaded: {allBooks?.GetType()}");
                
                // API'den gelen veriyi parse et
                List<object> allBookList = new List<object>();
                
                if (allBooks is Newtonsoft.Json.Linq.JArray jArray)
                {
                    Console.WriteLine($"All books is JArray with {jArray.Count} items");
                    allBookList = jArray.ToObject<List<object>>() ?? new List<object>();
                }
                else if (allBooks is List<object> list)
                {
                    Console.WriteLine($"All books is List<object> with {list.Count} items");
                    allBookList = list;
                }
                else
                {
                    Console.WriteLine($"All books is of type: {allBooks?.GetType()}");
                    if (allBooks != null)
                    {
                        allBookList = new List<object> { allBooks };
                    }
                }
                
                Console.WriteLine($"Total books loaded: {allBookList.Count}");
                
                // Client-side filtreleme yap
                var filteredBooks = FilterBooks(allBookList, searchTerm, filterType);
                
                Console.WriteLine($"Filtered books count: {filteredBooks.Count}");
                
                // DataTable'ı temizle ve yeni veriyi yükle
                kitaplarTable.Clear();
                
                if (filteredBooks.Count > 0)
                {
                    // Filtrelenmiş kitapları DataTable'a ekle
                    foreach (var book in filteredBooks)
                    {
                        try
                        {
                            // JToken olarak parse et
                            var bookToken = Newtonsoft.Json.Linq.JToken.FromObject(book);
                            
                            var row = kitaplarTable.NewRow();
                            
                            // API'den gelen alan adlarını kontrol et
                            string kitapAdi = "";
                            string yazar = "";
                            string yayinevi = "";
                            int stok = 0;
                            int kitapId = 0;
                            
                            // API'den gelen alan adlarını doğru eşleştir (API'de title, author, yayinevi, mevcut kullanılıyor)
                            if (bookToken["id"] != null) kitapId = int.Parse(bookToken["id"].ToString());
                            else if (bookToken["kitap_id"] != null) kitapId = int.Parse(bookToken["kitap_id"].ToString());
                            
                            // API title döndürüyor
                            if (bookToken["title"] != null) kitapAdi = bookToken["title"].ToString();
                            else if (bookToken["kitap_adi"] != null) kitapAdi = bookToken["kitap_adi"].ToString();
                            else if (bookToken["kitapAdi"] != null) kitapAdi = bookToken["kitapAdi"].ToString();
                            
                            // API author döndürüyor
                            if (bookToken["author"] != null) yazar = bookToken["author"].ToString();
                            else if (bookToken["yazar"] != null) yazar = bookToken["yazar"].ToString();
                            else if (bookToken["yazar_adi"] != null) yazar = bookToken["yazar_adi"].ToString();
                            else if (bookToken["yazarAdi"] != null) yazar = bookToken["yazarAdi"].ToString();
                            
                            // API yayinevi döndürüyor
                            if (bookToken["yayinevi"] != null) yayinevi = bookToken["yayinevi"].ToString();
                            else if (bookToken["publisher"] != null) yayinevi = bookToken["publisher"].ToString();
                            else if (bookToken["yayin_evi"] != null) yayinevi = bookToken["yayin_evi"].ToString();
                            
                            // API mevcut döndürüyor
                            if (bookToken["mevcut"] != null) stok = int.Parse(bookToken["mevcut"].ToString());
                            else if (bookToken["stok"] != null) stok = int.Parse(bookToken["stok"].ToString());
                            else if (bookToken["kitap_adet"] != null) stok = int.Parse(bookToken["kitap_adet"].ToString());
                            
                            row["KitapId"] = kitapId;
                            row["KitapAdi"] = kitapAdi;
                            row["Yazar"] = yazar;
                            row["Stok"] = stok;
                            row["Yayinevi"] = yayinevi;
                            
                            kitaplarTable.Rows.Add(row);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing filtered book: {ex.Message}");
                        }
                    }
                    
                    // ComboBox binding
                    comboBoxKitap.DataSource = kitaplarTable;
                    comboBoxKitap.DisplayMember = "KitapAdi";
                    comboBoxKitap.ValueMember = "KitapId";
                    Console.WriteLine($"Found {filteredBooks.Count} books for '{searchTerm}'");
                }
                else
                {
                    // ComboBox'ı temizle
                    comboBoxKitap.DataSource = null;
                    Console.WriteLine($"No books found for '{searchTerm}'");
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda sessizce boş liste göster
                Console.WriteLine($"PerformSearch error: {ex.Message}");
                kitaplarTable.Clear();
                comboBoxKitap.DataSource = null;
            }
        }

        // Kitap arama - Basit ve etkili yaklaşım
        private void txtAramaKitap_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtAramaKitap.Text.Trim().ToLower();
            
            if (string.IsNullOrEmpty(searchTerm))
            {
                // Arama boşsa tüm kitapları göster
                comboBoxKitap.DataSource = kitaplarTable;
                return;
            }
            
            // Filtrelenmiş DataTable oluştur
            var filteredTable = kitaplarTable.Clone();
            
            foreach (DataRow row in kitaplarTable.Rows)
            {
                string kitapAdi = row["KitapAdi"].ToString().ToLower();
                string yazar = row["Yazar"].ToString().ToLower();
                string yayinevi = row["Yayinevi"].ToString().ToLower();
                
                // Kitap adı, yazar veya yayınevi içinde arama terimi varsa ekle
                if (kitapAdi.Contains(searchTerm) || 
                    yazar.Contains(searchTerm) || 
                    yayinevi.Contains(searchTerm))
                {
                    filteredTable.ImportRow(row);
                }
            }
            
            // Filtrelenmiş veriyi ComboBox'a bağla
            comboBoxKitap.DataSource = filteredTable;
            comboBoxKitap.DisplayMember = "KitapAdi";
            comboBoxKitap.ValueMember = "KitapId";
            
            // Debug mesajı
            Console.WriteLine($"Kitap arama: '{searchTerm}' için {filteredTable.Rows.Count} sonuç bulundu");
        }

        // Kitap listesini yenileme metodu
        private async void btnYenileKitaplar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kitap listesi yenileniyor...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            await LoadSampleBooks();
        }

        // API Test metodu
        private async Task TestAPI()
        {
            try
            {
                var result = await apiHelper.TestEndpointsAsync();
                MessageBox.Show($"API Test Sonuçları:\n\n{result}", "API Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"API Test Hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 