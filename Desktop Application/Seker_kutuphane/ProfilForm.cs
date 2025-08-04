using System;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Seker_kutuphane
{
    public partial class ProfilForm : Form
    {
        private string kullaniciAdi;
        private string rol;
        private dynamic userData;
        private ApiHelper apiHelper;
        public Action<string>? OnRoleUpdated { get; set; }

        public ProfilForm(string kullaniciAdi, string rol, dynamic userData)
        {
            InitializeComponent();
            this.kullaniciAdi = kullaniciAdi;
            this.rol = rol;
            this.userData = userData;
            this.apiHelper = new ApiHelper();
            
            // Form başlığını güncelle
            this.Text = $"Profil Bilgileri - {kullaniciAdi}";
            
            SetupTCRestrictions();
            SetupTelefonRestrictions();
            SetupRoleBasedPermissions();
            SetupButtonHoverEffects();
            
            // Form yüklendiğinde verileri yükle
            this.Load += ProfilForm_Load;
        }
        
        private async void ProfilForm_Load(object sender, EventArgs e)
        {
            await LoadUserData();
        }

        private async Task LoadUserData()
        {
            try
            {
                // API'den güncel kullanıcı verilerini çek
                var apiHelper = new ApiHelper();
                var response = await apiHelper.GetAllUsersAsync();
                
                if (response is Newtonsoft.Json.Linq.JArray kullaniciArray)
                {
                    var guncelKullanici = kullaniciArray.FirstOrDefault(k => 
                        k["kullanici_id"]?.ToString() == userData.kullanici_id.ToString());
                    
                    if (guncelKullanici != null)
                    {
                        // Güncel verileri userData'ya yükle
                        userData = guncelKullanici;
                    }
                }
                
                // Kullanıcı bilgilerini form alanlarına yükle
                txtAd.Text = userData.ad?.ToString() ?? "";
                txtSoyad.Text = userData.soyad?.ToString() ?? "";
                txtTC.Text = userData.tc?.ToString() ?? "";
                txtTelefon.Text = userData.telefon?.ToString() ?? "";
                txtEmail.Text = userData.email?.ToString() ?? "";
                
                // Rol bilgisini al
                string guncelRol = await GetCurrentUserRole();
                if (!string.IsNullOrEmpty(guncelRol))
                {
                    rol = guncelRol; // Rol bilgisini güncelle
                }
                
                // Rol bilgisini Türkçe olarak göster
                string rolText = rol.ToLower() switch
                {
                    "admin" or "yönetici" or "yonetici" => "Admin",
                    "kütüphane görevlisi" or "kutuphane gorevlisi" or "kütüphane yetkilisi" or "kutuphane yetkilisi" or "görevli" or "gorevli" or "yetkili" or "librarian" or "staff" => "Kütüphane Görevlisi",
                    "üye" or "uye" or "kullanici" or "user" or "member" => "Üye",
                    _ => rol
                };
                
                lblRol.Text = $"Rol: {rolText}";
                
                // Rol bazlı yetkilendirmeyi güncelle
                SetupRoleBasedPermissions();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LoadUserData hatası: {ex.Message}");
                // Hata durumunda mevcut rol bilgisini kullan
                lblRol.Text = $"Rol: {rol}";
            }
        }

        private async Task<string> GetCurrentUserRole()
        {
            try
            {
                // Önce mevcut userData'dan rol bilgisini al
                if (userData.rol_adlari != null)
                {
                    var rolAdlari = userData.rol_adlari as Newtonsoft.Json.Linq.JArray;
                    if (rolAdlari != null && rolAdlari.Count > 0)
                    {
                        // En yüksek yetkili rolü al (Admin > Kütüphane Yetkilisi > Üye)
                        string enYuksekRol = "";
                        foreach (var rolAdi in rolAdlari)
                        {
                            string rol = rolAdi.ToString().ToLower();
                            if (rol.Contains("admin") || rol.Contains("yönetici") || rol.Contains("yonetici"))
                            {
                                enYuksekRol = "Admin";
                                break;
                            }
                            else if (rol.Contains("kütüphane") || rol.Contains("kutuphane") || rol.Contains("görevli") || rol.Contains("gorevli") || rol.Contains("yetkili"))
                            {
                                enYuksekRol = "Kütüphane Görevlisi";
                            }
                            else if (rol.Contains("üye") || rol.Contains("uye"))
                            {
                                if (string.IsNullOrEmpty(enYuksekRol))
                                    enYuksekRol = "Üye";
                            }
                        }
                        
                        if (!string.IsNullOrEmpty(enYuksekRol))
                        {
                            return enYuksekRol;
                        }
                    }
                }
                
                // Eğer rol_adlari'dan alamazsak API'den çek
                var response = await apiHelper.GetCurrentUserRoleAsync(userData.kullanici_id);
                return response?.ToString() ?? rol;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetCurrentUserRole hatası: {ex.Message}");
                return rol; // Hata durumunda mevcut rolü döndür
            }
        }

        private void SetupTCRestrictions()
        {
            // TC kimlik numarası için kısıtlamalar
            txtTC.MaxLength = 11;
            txtTC.KeyPress += TxtTC_KeyPress;
            txtTC.TextChanged += TxtTC_TextChanged;
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

        private void SetupTelefonRestrictions()
        {
            // Telefon numarası için kısıtlamalar
            txtTelefon.MaxLength = 11;
            txtTelefon.KeyPress += TxtTelefon_KeyPress;
            txtTelefon.TextChanged += TxtTelefon_TextChanged;
        }
        
        private void SetupButtonHoverEffects()
        {
            // Güncelle butonu hover efektleri
            btnGuncelle.MouseEnter += (s, e) => {
                if (btnGuncelle.Enabled)
                    btnGuncelle.BackColor = Color.FromArgb(129, 199, 132); // Daha açık yeşil
            };
            btnGuncelle.MouseLeave += (s, e) => {
                if (btnGuncelle.Enabled)
                    btnGuncelle.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            };
            
            // İptal butonu hover efektleri
            btnIptal.MouseEnter += (s, e) => {
                btnIptal.BackColor = Color.FromArgb(189, 189, 189); // Daha açık gri
            };
            btnIptal.MouseLeave += (s, e) => {
                btnIptal.BackColor = Color.FromArgb(158, 158, 158); // Gri
            };
            
            // Şifre Değiştir butonu hover efektleri
            btnSifreDegistir.MouseEnter += (s, e) => {
                if (btnSifreDegistir.Enabled)
                    btnSifreDegistir.BackColor = Color.FromArgb(66, 165, 245); // Daha açık mavi
            };
            btnSifreDegistir.MouseLeave += (s, e) => {
                if (btnSifreDegistir.Enabled)
                    btnSifreDegistir.BackColor = Color.FromArgb(33, 150, 243); // Mavi
            };
            
            // Yenile butonu hover efektleri
            btnYenile.MouseEnter += (s, e) => {
                if (btnYenile.Enabled)
                    btnYenile.BackColor = Color.FromArgb(129, 199, 132); // Daha açık yeşil
            };
            btnYenile.MouseLeave += (s, e) => {
                if (btnYenile.Enabled)
                    btnYenile.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            };
        }

        private void SetupRoleBasedPermissions()
        {
            // Rol bazlı yetkilendirme
            switch (rol.ToLower())
            {
                case "admin":
                case "yönetici":
                case "yonetici":
                    // Admin: Tüm alanları değiştirebilir
                    txtAd.Enabled = true;
                    txtSoyad.Enabled = true;
                    txtTC.Enabled = true;
                    txtTelefon.Enabled = true;
                    txtEmail.Enabled = true;
                    break;

                case "kütüphane görevlisi":
                case "kutuphane gorevlisi":
                case "kütüphane yetkilisi":
                case "kutuphane yetkilisi":
                case "görevli":
                case "gorevli":
                case "yetkili":
                case "librarian":
                case "staff":
                    // Yetkili: TC hariç tüm alanları değiştirebilir
                    txtAd.Enabled = true;
                    txtSoyad.Enabled = true;
                    txtTC.Enabled = false; // TC değiştirilemez
                    txtTC.BackColor = Color.LightGray;
                    txtTelefon.Enabled = true;
                    txtEmail.Enabled = true;
                    break;

                case "üye":
                case "uye":
                case "kullanici":
                case "user":
                case "member":
                default:
                    // Üye: Sadece telefon ve email değiştirebilir
                    txtAd.Enabled = false;
                    txtAd.BackColor = Color.LightGray;
                    txtSoyad.Enabled = false;
                    txtSoyad.BackColor = Color.LightGray;
                    txtTC.Enabled = false;
                    txtTC.BackColor = Color.LightGray;
                    txtTelefon.Enabled = true;
                    txtEmail.Enabled = true;
                    break;
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

        private async void btnGuncelle_Click(object sender, EventArgs e)
        {
            // Rol bazlı validasyon
            bool isValid = true;
            string errorMessage = "";

            switch (rol.ToLower())
            {
                case "admin":
                case "yönetici":
                case "yonetici":
                    // Admin: Tüm alanları kontrol et
                    if (string.IsNullOrEmpty(txtAd.Text.Trim()) || 
                        string.IsNullOrEmpty(txtSoyad.Text.Trim()) ||
                        string.IsNullOrEmpty(txtTC.Text.Trim()) ||
                        string.IsNullOrEmpty(txtTelefon.Text.Trim()) ||
                        string.IsNullOrEmpty(txtEmail.Text.Trim()))
                    {
                        errorMessage = "Lütfen tüm alanları doldurunuz.";
                        isValid = false;
                    }
                    else if (txtTC.Text.Length != 11)
                    {
                        errorMessage = "TC Kimlik Numarası 11 haneli olmalıdır.";
                        isValid = false;
                    }
                    break;

                case "kütüphane görevlisi":
                case "kutuphane gorevlisi":
                case "kütüphane yetkilisi":
                case "kutuphane yetkilisi":
                case "görevli":
                case "gorevli":
                case "yetkili":
                case "librarian":
                case "staff":
                    // Yetkili: TC hariç tüm alanları kontrol et
                    if (string.IsNullOrEmpty(txtAd.Text.Trim()) || 
                        string.IsNullOrEmpty(txtSoyad.Text.Trim()) ||
                        string.IsNullOrEmpty(txtTelefon.Text.Trim()) ||
                        string.IsNullOrEmpty(txtEmail.Text.Trim()))
                    {
                        errorMessage = "Lütfen tüm alanları doldurunuz.";
                        isValid = false;
                    }
                    break;

                case "üye":
                case "uye":
                case "kullanici":
                case "user":
                case "member":
                default:
                    // Üye: Sadece telefon ve email kontrol et
                    if (string.IsNullOrEmpty(txtTelefon.Text.Trim()) ||
                        string.IsNullOrEmpty(txtEmail.Text.Trim()))
                    {
                        errorMessage = "Lütfen telefon ve e-posta alanlarını doldurunuz.";
                        isValid = false;
                    }
                    break;
            }

            // Genel validasyonlar
            if (isValid && txtTelefon.Text.Length < 10)
            {
                errorMessage = "Telefon numarası en az 10 haneli olmalıdır.";
                isValid = false;
            }

            if (isValid && (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains(".")))
            {
                errorMessage = "Geçerli bir e-posta adresi giriniz.";
                isValid = false;
            }

            if (!isValid)
            {
                MessageBox.Show(errorMessage, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Güncelleme işlemi
            try
            {
                btnGuncelle.Enabled = false;
                btnGuncelle.Text = "Güncelleniyor...";

                // Tüm alanları gönder ama rol bilgisini koru
                var updateData = new
                {
                    kullanici_id = userData.kullanici_id,
                    ad = txtAd.Text.Trim(),
                    soyad = txtSoyad.Text.Trim(),
                    tc = txtTC.Text.Trim(),
                    telefon = txtTelefon.Text.Trim(),
                    email = txtEmail.Text.Trim(),
                    // Rol bilgilerini koru - bunları değiştirme
                    rol_ids = userData.rol_ids,
                    rol_adlari = userData.rol_adlari
                };

                Console.WriteLine($"Profil güncelleme verisi: {JsonConvert.SerializeObject(updateData)}");

                var result = await apiHelper.UpdateUserProfileAsync(updateData);
                Console.WriteLine($"Profil güncelleme sonucu: {JsonConvert.SerializeObject(result)}");

                // Rol güncellemesi sonrasında callback'i çağır
                if (OnRoleUpdated != null)
                {
                    OnRoleUpdated(rol);
                }

                MessageBox.Show("Profil bilgileriniz başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Form kapanmasın, sadece verileri yenile
                await LoadUserData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Profil güncelleme hatası: {ex.Message}");
                MessageBox.Show($"Güncelleme başarısız: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGuncelle.Enabled = true;
                btnGuncelle.Text = "Güncelle";
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSifreDegistir_Click(object sender, EventArgs e)
        {
            // Şifre değiştirme dialog'u aç
            int kullaniciId = Convert.ToInt32(userData.kullanici_id);
            using (var sifreForm = new SifreDegistirForm(kullaniciId))
            {
                if (sifreForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Şifreniz başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        
        private async void btnYenile_Click(object sender, EventArgs e)
        {
            try
            {
                btnYenile.Enabled = false;
                btnYenile.Text = "⏳ Yenileniyor...";
                
                // Kullanıcı verilerini yeniden yükle
                await LoadUserData();
                
                // Başarı mesajı yerine kısa bir görsel feedback
                btnYenile.Text = "✅ Yenilendi!";
                await Task.Delay(1000); // 1 saniye bekle
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yenileme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnYenile.Enabled = true;
                btnYenile.Text = "🔄 Yenile";
            }
        }
    }
} 