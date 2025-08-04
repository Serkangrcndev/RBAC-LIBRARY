using System;
using System.Windows.Forms;

namespace Seker_kutuphane
{
    public partial class EmanetTestForm : Form
    {
        private ApiHelper apiHelper;

        public EmanetTestForm()
        {
            InitializeComponent();
            this.apiHelper = new ApiHelper();
        }

        private async void btnTestEmanetler_Click(object sender, EventArgs e)
        {
            try
            {
                txtResults.Clear();
                txtResults.AppendText("Emanet endpoint'leri test ediliyor...\n\n");

                var results = await apiHelper.TestEmanetEndpointsAsync();
                txtResults.AppendText(results);

                txtResults.AppendText("\n\nTest tamamlandı!");
            }
            catch (Exception ex)
            {
                txtResults.AppendText($"Test hatası: {ex.Message}");
            }
        }

        private async void btnTestEmanetEkle_Click(object sender, EventArgs e)
        {
            try
            {
                txtResults.Clear();
                txtResults.AppendText("Ödünç ekleme test ediliyor...\n\n");

                var testData = new
                {
                    kullanici_id = 1,
                    kitap_id = 1,
                    odunc_tarihi = DateTime.Now.ToString("yyyy-MM-dd"),
                    beklenen_teslim = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd"),
                    durum = "AKTİF"
                };

                var result = await apiHelper.CreateOduncAsync(testData);
                txtResults.AppendText($"Ödünç ekleme başarılı: {result}\n");
            }
            catch (Exception ex)
            {
                txtResults.AppendText($"Ödünç ekleme hatası: {ex.Message}\n");
            }
        }

        private async void btnTestEmanetIade_Click(object sender, EventArgs e)
        {
            try
            {
                txtResults.Clear();
                txtResults.AppendText("Emanet iade test ediliyor...\n\n");

                if (int.TryParse(txtEmanetId.Text, out int emanetId))
                {
                    var result = await apiHelper.ReturnEmanetAsync(emanetId);
                    txtResults.AppendText($"Emanet iade başarılı: {result}\n");
                }
                else
                {
                    txtResults.AppendText("Geçerli bir emanet ID girin!\n");
                }
            }
            catch (Exception ex)
            {
                txtResults.AppendText($"Emanet iade hatası: {ex.Message}\n");
            }
        }

        private async void btnTestEmanetAra_Click(object sender, EventArgs e)
        {
            try
            {
                txtResults.Clear();
                txtResults.AppendText("Emanet arama test ediliyor...\n\n");

                var searchTerm = txtAramaTerm.Text;
                var results = await apiHelper.SearchOdunclerAsync(searchTerm);
                txtResults.AppendText($"Arama sonuçları: {results}\n");
            }
            catch (Exception ex)
            {
                txtResults.AppendText($"Emanet arama hatası: {ex.Message}\n");
            }
        }

        private async void btnTestGecikmisEmanetler_Click(object sender, EventArgs e)
        {
            try
            {
                txtResults.Clear();
                txtResults.AppendText("Gecikmiş emanetler test ediliyor...\n\n");

                var results = await apiHelper.GetGecikmisEmanetlerAsync();
                txtResults.AppendText($"Gecikmiş emanetler: {results}\n");
            }
            catch (Exception ex)
            {
                txtResults.AppendText($"Gecikmiş emanetler hatası: {ex.Message}\n");
            }
        }

        private async void btnTestTumEmanetler_Click(object sender, EventArgs e)
        {
            try
            {
                txtResults.Clear();
                txtResults.AppendText("Tüm ödünçler test ediliyor...\n\n");

                var results = await apiHelper.GetAllOdunclerAsync();
                txtResults.AppendText($"Tüm ödünçler: {results}\n");
            }
            catch (Exception ex)
            {
                txtResults.AppendText($"Tüm ödünçler hatası: {ex.Message}\n");
            }
        }
    }
} 