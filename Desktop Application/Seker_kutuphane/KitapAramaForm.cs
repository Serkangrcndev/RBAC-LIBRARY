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
    public partial class KitapAramaForm : Form
    {
        private ApiHelper apiHelper;
        private Dashboard dashboardForm;

        public KitapAramaForm(Dashboard dashboardForm)
        {
            InitializeComponent();
            this.dashboardForm = dashboardForm;
            this.apiHelper = new ApiHelper();
            SetupFormDesign();
            LoadInitialData();
        }

        private void SetupFormDesign()
        {
            // Form başlığı
            lblBaslik.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(0, 128, 0);
            lblBaslik.TextAlign = ContentAlignment.MiddleCenter;

            // Arama butonu
            btnAra.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnAra.BackColor = Color.FromArgb(76, 175, 80);
            btnAra.ForeColor = Color.White;
            btnAra.FlatStyle = FlatStyle.Flat;
            btnAra.FlatAppearance.BorderSize = 0;

            // Temizle butonu
            btnTemizle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnTemizle.BackColor = Color.FromArgb(255, 152, 0);
            btnTemizle.ForeColor = Color.White;
            btnTemizle.FlatStyle = FlatStyle.Flat;
            btnTemizle.FlatAppearance.BorderSize = 0;

            // Geri butonu
            btnGeri.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnGeri.BackColor = Color.FromArgb(244, 67, 54);
            btnGeri.ForeColor = Color.White;
            btnGeri.FlatStyle = FlatStyle.Flat;
            btnGeri.FlatAppearance.BorderSize = 0;

            // DataGridView ayarları
            dgvKitaplar.BackgroundColor = Color.White;
            dgvKitaplar.BorderStyle = BorderStyle.None;
            dgvKitaplar.GridColor = Color.FromArgb(224, 224, 224);
            dgvKitaplar.Font = new Font("Segoe UI", 9);
            dgvKitaplar.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvKitaplar.RowHeadersVisible = false;
            dgvKitaplar.AllowUserToAddRows = false;
            dgvKitaplar.AllowUserToDeleteRows = false;
            dgvKitaplar.ReadOnly = true;
            dgvKitaplar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKitaplar.MultiSelect = false;

            // Hover efektleri
            SetupHoverEffects();
        }

        private void SetupHoverEffects()
        {
            btnAra.MouseEnter += (s, e) => btnAra.BackColor = Color.FromArgb(129, 199, 132);
            btnAra.MouseLeave += (s, e) => btnAra.BackColor = Color.FromArgb(76, 175, 80);

            btnTemizle.MouseEnter += (s, e) => btnTemizle.BackColor = Color.FromArgb(255, 167, 38);
            btnTemizle.MouseLeave += (s, e) => btnTemizle.BackColor = Color.FromArgb(255, 152, 0);

            btnGeri.MouseEnter += (s, e) => btnGeri.BackColor = Color.FromArgb(239, 83, 80);
            btnGeri.MouseLeave += (s, e) => btnGeri.BackColor = Color.FromArgb(244, 67, 54);
        }

        private async void LoadInitialData()
        {
            try
            {
                // Örnek kitap verilerini yükle
                await LoadSampleBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadSampleBooks()
        {
            try
            {
                Console.WriteLine("LoadSampleBooks called - getting all books");

                // API'den tüm kitap verilerini al
                var books = await apiHelper.GetAllBooksAsync();

                Console.WriteLine($"GetAllBooks returned: {books?.GetType()}");

                // API'den gelen veriyi parse et
                List<object> bookList = new List<object>();

                if (books is Newtonsoft.Json.Linq.JArray jArray)
                {
                    Console.WriteLine($"Books is JArray with {jArray.Count} items");
                    bookList = jArray.ToObject<List<object>>() ?? new List<object>();
                }
                else if (books is List<object> list)
                {
                    Console.WriteLine($"Books is List<object> with {list.Count} items");
                    bookList = list;
                }
                else
                {
                    Console.WriteLine($"Books is of type: {books?.GetType()}");
                    // Diğer türleri de kontrol et
                    if (books != null)
                    {
                        bookList = new List<object> { books };
                    }
                }

                Console.WriteLine($"Final bookList count: {bookList.Count}");

                if (bookList.Count > 0)
                {
                    // DataGridView'ı temizle ve yeni veriyi yükle
                    dgvKitaplar.DataSource = null;
                    dgvKitaplar.DataSource = bookList;

                    // Veri yüklendikten sonra sütun ayarlarını yap
                    SetupDataGridView();
                    lblSonuc.Text = $"Toplam {bookList.Count} kitap mevcut. Arama yapmak için yukarıdaki kutuya yazın.";
                    Console.WriteLine($"Loaded {bookList.Count} books");
                }
                else
                {
                    // Kitap yoksa
                    dgvKitaplar.DataSource = null;
                    lblSonuc.Text = "Henüz kitap bulunmamaktadır.";
                    Console.WriteLine("No books found");
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda sessizce boş liste göster
                Console.WriteLine($"LoadSampleBooks error: {ex.Message}");
                dgvKitaplar.DataSource = null;
                lblSonuc.Text = "Kitap verileri yüklenemedi.";
            }
        }

        private void SetupDataGridView()
        {
            try
            {
                if (dgvKitaplar.Columns.Count > 0)
                {
                    // KitapAdi sütunu
                    if (dgvKitaplar.Columns.Contains("KitapAdi"))
                    {
                        dgvKitaplar.Columns["KitapAdi"].HeaderText = "Kitap Adı";
                        dgvKitaplar.Columns["KitapAdi"].Width = 200;
                        dgvKitaplar.Columns["KitapAdi"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    }

                    // Yazar sütunu
                    if (dgvKitaplar.Columns.Contains("Yazar"))
                    {
                        dgvKitaplar.Columns["Yazar"].HeaderText = "Yazar";
                        dgvKitaplar.Columns["Yazar"].Width = 150;
                    }

                    // Yayinevi sütunu
                    if (dgvKitaplar.Columns.Contains("Yayinevi"))
                    {
                        dgvKitaplar.Columns["Yayinevi"].HeaderText = "Yayınevi";
                        dgvKitaplar.Columns["Yayinevi"].Width = 180;
                    }

                    // Yil sütunu
                    if (dgvKitaplar.Columns.Contains("Yil"))
                    {
                        dgvKitaplar.Columns["Yil"].HeaderText = "Yıl";
                        dgvKitaplar.Columns["Yil"].Width = 80;
                        dgvKitaplar.Columns["Yil"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    // Stok sütunu
                    if (dgvKitaplar.Columns.Contains("Stok"))
                    {
                        dgvKitaplar.Columns["Stok"].HeaderText = "Stok";
                        dgvKitaplar.Columns["Stok"].Width = 80;
                        dgvKitaplar.Columns["Stok"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    // Durum sütunu
                    if (dgvKitaplar.Columns.Contains("Durum"))
                    {
                        dgvKitaplar.Columns["Durum"].HeaderText = "Durum";
                        dgvKitaplar.Columns["Durum"].Width = 100;
                        dgvKitaplar.Columns["Durum"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    // Header stilleri
                    dgvKitaplar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 128, 0);
                    dgvKitaplar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                    dgvKitaplar.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    dgvKitaplar.ColumnHeadersHeight = 40;
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda sadece log yaz, kullanıcıya gösterme
                Console.WriteLine($"DataGridView ayarlanırken hata: {ex.Message}");
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void txtKitapAdi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformSearch();
                e.Handled = true;
            }
        }

        private void txtYazar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformSearch();
                e.Handled = true;
            }
        }

        private void txtYil_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece sayı ve backspace'e izin ver
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformSearch();
                e.Handled = true;
            }
        }

        private void txtYayinevi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformSearch();
                e.Handled = true;
            }
        }

        private void cmbAramaTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Arama türü değiştiğinde ilgili alanı vurgula
            switch (cmbAramaTuru.SelectedItem?.ToString())
            {
                case "Kitap Adı":
                    txtKitapAdi.Focus();
                    break;
                case "Yazar":
                    txtYazar.Focus();
                    break;
                case "Yıl":
                    txtYil.Focus();
                    break;
                case "Yayınevi":
                    txtYayinevi.Focus();
                    break;
            }
        }

        private async void PerformSearch()
        {
            string searchTerm = "";
            string filterType = "";
            
            // Arama türüne göre parametreleri belirle
            switch (cmbAramaTuru.SelectedItem?.ToString())
            {
                case "Kitap Adı":
                    searchTerm = txtKitapAdi.Text.Trim();
                    filterType = "kitap_adi";
                    Console.WriteLine($"Selected: Kitap Adı - Term: '{searchTerm}'");
                    break;
                case "Yazar":
                    searchTerm = txtYazar.Text.Trim();
                    filterType = "yazar";
                    Console.WriteLine($"Selected: Yazar - Term: '{searchTerm}'");
                    break;
                case "Yıl":
                    searchTerm = txtYil.Text.Trim();
                    filterType = "yil";
                    Console.WriteLine($"Selected: Yıl - Term: '{searchTerm}'");
                    break;
                case "Yayınevi":
                    searchTerm = txtYayinevi.Text.Trim();
                    filterType = "yayinevi";
                    Console.WriteLine($"Selected: Yayınevi - Term: '{searchTerm}'");
                    break;
                case "Tümü":
                default:
                    // Tümü seçilmişse, dolu olan ilk alanı kullan
                    if (!string.IsNullOrEmpty(txtKitapAdi.Text.Trim()))
                    {
                        searchTerm = txtKitapAdi.Text.Trim();
                        filterType = "kitap_adi";
                        Console.WriteLine($"Tümü - Using Kitap Adı: '{searchTerm}'");
                    }
                    else if (!string.IsNullOrEmpty(txtYazar.Text.Trim()))
                    {
                        searchTerm = txtYazar.Text.Trim();
                        filterType = "yazar";
                        Console.WriteLine($"Tümü - Using Yazar: '{searchTerm}'");
                    }
                    else if (!string.IsNullOrEmpty(txtYil.Text.Trim()))
                    {
                        searchTerm = txtYil.Text.Trim();
                        filterType = "yil";
                        Console.WriteLine($"Tümü - Using Yıl: '{searchTerm}'");
                    }
                    else if (!string.IsNullOrEmpty(txtYayinevi.Text.Trim()))
                    {
                        searchTerm = txtYayinevi.Text.Trim();
                        filterType = "yayinevi";
                        Console.WriteLine($"Tümü - Using Yayınevi: '{searchTerm}'");
                    }
                    break;
            }
            
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
                
                // DataGridView'ı temizle ve yeni veriyi yükle
                dgvKitaplar.DataSource = null;
                
                if (filteredBooks.Count > 0)
                {
                    dgvKitaplar.DataSource = filteredBooks;
                    
                    // Veri yüklendikten sonra sütun ayarlarını yap
                    SetupDataGridView();
                    lblSonuc.Text = $"'{searchTerm}' için {filteredBooks.Count} kitap bulundu.";
                    Console.WriteLine($"Found {filteredBooks.Count} books for '{searchTerm}'");
                }
                else
                {
                    lblSonuc.Text = $"'{searchTerm}' ile ilgili kitap bulunamadı.";
                    // DataGridView'ı temizle
                    dgvKitaplar.DataSource = null;
                    Console.WriteLine($"No books found for '{searchTerm}'");
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda sessizce boş liste göster
                Console.WriteLine($"PerformSearch error: {ex.Message}");
                dgvKitaplar.DataSource = null;
                lblSonuc.Text = $"'{searchTerm}' ile ilgili kitap bulunamadı.";
            }
        }

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
                    
                    // Farklı olası alan adlarını dene
                    if (bookToken["kitap_adi"] != null) kitapAdi = bookToken["kitap_adi"].ToString();
                    else if (bookToken["title"] != null) kitapAdi = bookToken["title"].ToString();
                    else if (bookToken["kitapAdi"] != null) kitapAdi = bookToken["kitapAdi"].ToString();
                    
                    if (bookToken["yazar"] != null) yazar = bookToken["yazar"].ToString();
                    else if (bookToken["author"] != null) yazar = bookToken["author"].ToString();
                    else if (bookToken["yazar_adi"] != null) yazar = bookToken["yazar_adi"].ToString();
                    else if (bookToken["yazarAdi"] != null) yazar = bookToken["yazarAdi"].ToString();
                    
                    if (bookToken["yayinevi"] != null) yayinevi = bookToken["yayinevi"].ToString();
                    else if (bookToken["publisher"] != null) yayinevi = bookToken["publisher"].ToString();
                    else if (bookToken["yayin_evi"] != null) yayinevi = bookToken["yayin_evi"].ToString();
                    
                    if (bookToken["yil"] != null) yil = bookToken["yil"].ToString();
                    else if (bookToken["year"] != null) yil = bookToken["year"].ToString();
                    else if (bookToken["yayin_yili"] != null) yil = bookToken["yayin_yili"].ToString();
                    else if (bookToken["publishYear"] != null) yil = bookToken["publishYear"].ToString();
                    
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


        private void btnTemizle_Click(object sender, EventArgs e)
        {
            // Tüm arama alanlarını temizle
            txtKitapAdi.Text = "";
            txtYazar.Text = "";
            txtYil.Text = "";
            txtYayinevi.Text = "";
            cmbAramaTuru.SelectedIndex = 0;

            // Tüm kitapları göster
            _ = LoadSampleBooks();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Close();
            dashboardForm.Show();
        }

        private void KitapAramaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            dashboardForm.Show();
        }

        private void lblKitapAdi_Click(object sender, EventArgs e)
        {

        }
    }
} 