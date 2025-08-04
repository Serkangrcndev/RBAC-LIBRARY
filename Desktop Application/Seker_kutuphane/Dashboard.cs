using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seker_kutuphane
{
    public partial class Dashboard : Form
    {
        private string kullaniciAdi;
        private string rol;
        private dynamic userData;
        
        public Dashboard(string kullaniciAdi, string rol, dynamic userData = null)
        {
            InitializeComponent();
            this.kullaniciAdi = kullaniciAdi;
            this.rol = rol;
            this.userData = userData;
            SetupRoleBasedAccess();
            SetupButtonHoverEffects();
        }

        private void SetupButtonHoverEffects()
        {
            // Buton hover efektleri - Kayseri Şeker renkleri
            btnKitaplar.MouseEnter += (s, e) => {
                if (btnKitaplar.Enabled)
                    btnKitaplar.BackColor = Color.FromArgb(129, 199, 132); // Daha açık yeşil
            };
            btnKitaplar.MouseLeave += (s, e) => {
                if (btnKitaplar.Enabled)
                    btnKitaplar.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            };

            btnUyeler.MouseEnter += (s, e) => {
                if (btnUyeler.Enabled)
                    btnUyeler.BackColor = Color.FromArgb(129, 199, 132); // Daha açık yeşil
            };
            btnUyeler.MouseLeave += (s, e) => {
                if (btnUyeler.Enabled)
                    btnUyeler.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            };

            btnEmanetler.MouseEnter += (s, e) => {
                if (btnEmanetler.Enabled)
                    btnEmanetler.BackColor = Color.FromArgb(129, 199, 132); // Daha açık yeşil
            };
            btnEmanetler.MouseLeave += (s, e) => {
                if (btnEmanetler.Enabled)
                    btnEmanetler.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            };

            btnRaporlar.MouseEnter += (s, e) => {
                if (btnRaporlar.Enabled)
                    btnRaporlar.BackColor = Color.FromArgb(129, 199, 132); // Daha açık yeşil
            };
            btnRaporlar.MouseLeave += (s, e) => {
                if (btnRaporlar.Enabled)
                    btnRaporlar.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            };

            btnYonetim.MouseEnter += (s, e) => {
                if (btnYonetim.Enabled)
                    btnYonetim.BackColor = Color.FromArgb(129, 199, 132); // Daha açık yeşil
            };
            btnYonetim.MouseLeave += (s, e) => {
                if (btnYonetim.Enabled)
                    btnYonetim.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            };

            btnKitapEkle.MouseEnter += (s, e) => {
                if (btnKitapEkle.Enabled)
                    btnKitapEkle.BackColor = Color.FromArgb(255, 167, 38); // Daha açık turuncu
            };
            btnKitapEkle.MouseLeave += (s, e) => {
                if (btnKitapEkle.Enabled)
                    btnKitapEkle.BackColor = Color.FromArgb(255, 152, 0); // Turuncu
            };

            btnCikis.MouseEnter += (s, e) => {
                btnCikis.BackColor = Color.FromArgb(239, 83, 80); // Daha açık kırmızı
            };
            btnCikis.MouseLeave += (s, e) => {
                btnCikis.BackColor = Color.FromArgb(244, 67, 54); // Kırmızı
            };

            // lblKullanici için hover efekti
            lblKullanici.MouseEnter += (s, e) => {
                lblKullanici.ForeColor = Color.FromArgb(129, 199, 132); // Daha açık yeşil
            };
            lblKullanici.MouseLeave += (s, e) => {
                lblKullanici.ForeColor = Color.FromArgb(0, 128, 0); // Kayseri Şeker Yeşili
            };
        }

        private void SetupRoleBasedAccess()
        {
            // Kullanıcı bilgisi göster
            lblKullanici.Text = $"Hoş geldiniz {kullaniciAdi}";
            lblKullanici.Visible = true;
            lblKullanici.BringToFront();
            
            // Tüm butonları varsayılan olarak görünür yap ama sadece yetkili olanları aktif et
            SetupAllButtons();
            
            // Rol bazlı yetki ayarları
            switch (rol.ToLower())
            {
                case "üye":
                case "uye":
                case "kullanici":
                case "user":
                case "member":
                    SetupUyePermissions();
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
                    SetupGorevliPermissions();
                    break;
                    
                case "admin":
                case "yönetici":
                case "yonetici":
                case "administrator":
                    SetupAdminPermissions();
                    break;
                    
                default:
                    // Bilinmeyen rol için sadece temel yetkiler
                    SetupUyePermissions();
                    break;
            }
        }

        private void SetupAllButtons()
        {
            // Tüm butonları görünür yap ve varsayılan metinleri ayarla
            btnKitaplar.Visible = true;
            btnKitaplar.Text = "Kitap Ara";
            btnKitaplar.Enabled = true;
            
            btnUyeler.Visible = true;
            btnUyeler.Text = "Profilim";
            btnUyeler.Enabled = true;
            
            btnEmanetler.Visible = true;
            btnEmanetler.Text = "Emanet İşlemleri";
            btnEmanetler.Enabled = true;
            
            btnRaporlar.Visible = true;
            btnRaporlar.Text = "Raporlar";
            btnRaporlar.Enabled = true;
            
            btnYonetim.Visible = true;
            btnYonetim.Text = "Üyelik İşlemleri";
            btnYonetim.Enabled = true;
            
            btnKitapEkle.Visible = true;
            btnKitapEkle.Text = "Kitap Ekle";
            btnKitapEkle.Enabled = true;
            
            btnCikis.Visible = true;
            btnCikis.Text = "Çıkış";
            btnCikis.Enabled = true;
        }

        private void SetupUyePermissions()
        {
            // Üye yetkileri: Sadece kitap arama, kitaplarım ve profil güncelleme
            btnKitaplar.Text = "Kitap Ara";
            btnKitaplar.Enabled = true;
            btnKitaplar.Visible = true;
            btnKitaplar.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            btnKitaplar.ForeColor = Color.White;

            btnKitaplarim.Text = "Kitaplarım";
            btnKitaplarim.Enabled = true;
            btnKitaplarim.Visible = true;
            btnKitaplarim.BackColor = Color.FromArgb(76, 175, 80);
            btnKitaplarim.ForeColor = Color.White;

            btnUyeler.Text = "Profilim";
            btnUyeler.Enabled = true;
            btnUyeler.Visible = true;
            btnUyeler.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            btnUyeler.ForeColor = Color.White;

            // Diğer butonlar gizli
            btnEmanetler.Visible = false;
            btnRaporlar.Visible = false;
            btnYonetim.Visible = false;
            btnKitapEkle.Visible = false;
        }

        private void SetupGorevliPermissions()
        {
            // Kütüphane görevlisi yetkileri: Üye yetkileri + üye yönetimi + emanet işlemleri
            btnKitaplar.Text = "Kitap Ara";
            btnKitaplar.Enabled = true;
            btnKitaplar.Visible = true;
            btnKitaplar.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            btnKitaplar.ForeColor = Color.White;

            btnKitaplarim.Text = "Kitaplarım";
            btnKitaplarim.Enabled = true;
            btnKitaplarim.Visible = true;
            btnKitaplarim.BackColor = Color.FromArgb(76, 175, 80);
            btnKitaplarim.ForeColor = Color.White;

            btnUyeler.Text = "Profilim";
            btnUyeler.Enabled = true;
            btnUyeler.Visible = true;
            btnUyeler.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            btnUyeler.ForeColor = Color.White;

            btnEmanetler.Text = "Ödünç İşlemleri";
            btnEmanetler.Enabled = true;
            btnEmanetler.Visible = true;
            btnEmanetler.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            btnEmanetler.ForeColor = Color.White;

            // Üyelik işlemleri butonu görünür (rol değiştirme yetkisi yok)
            btnYonetim.Text = "Üyelik İşlemleri";
            btnYonetim.Enabled = true;
            btnYonetim.Visible = true;
            btnYonetim.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            btnYonetim.ForeColor = Color.White;

            // Raporlar ve kitap ekleme gizli (sadece admin)
            btnRaporlar.Visible = false;
            btnKitapEkle.Visible = false;
        }

        private void SetupAdminPermissions()
        {
            // Admin yetkileri: Tüm yetkiler (Görevli + raporlar + sistem yönetimi)
            btnKitaplar.Text = "Kitap Ara";
            btnKitaplar.Enabled = true;
            btnKitaplar.Visible = true;
            btnKitaplar.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            btnKitaplar.ForeColor = Color.White;

            btnKitaplarim.Text = "Kitaplarım";
            btnKitaplarim.Enabled = true;
            btnKitaplarim.Visible = true;
            btnKitaplarim.BackColor = Color.FromArgb(76, 175, 80);
            btnKitaplarim.ForeColor = Color.White;

            btnUyeler.Text = "Profilim";
            btnUyeler.Enabled = true;
            btnUyeler.Visible = true;
            btnUyeler.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            btnUyeler.ForeColor = Color.White;

            btnEmanetler.Text = "Ödünç İşlemleri";
            btnEmanetler.Enabled = true;
            btnEmanetler.Visible = true;
            btnEmanetler.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            btnEmanetler.ForeColor = Color.White;

            btnRaporlar.Text = "Raporlar";
            btnRaporlar.Enabled = true;
            btnRaporlar.Visible = true;
            btnRaporlar.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            btnRaporlar.ForeColor = Color.White;

            btnYonetim.Text = "Üyelik İşlemleri";
            btnYonetim.Enabled = true;
            btnYonetim.Visible = true;
            btnYonetim.BackColor = Color.FromArgb(76, 175, 80); // Açık yeşil
            btnYonetim.ForeColor = Color.White;

            btnKitapEkle.Text = "Kitap İşlemleri";
            btnKitapEkle.Enabled = true;
            btnKitapEkle.Visible = true;
            btnKitapEkle.BackColor = Color.FromArgb(255, 152, 0); // Turuncu
            btnKitapEkle.ForeColor = Color.White;
        }

        private void Dashboard_Load(object? sender, EventArgs e)
        {
            // Form yüklendiğinde rol bilgisini logla
            Console.WriteLine($"Dashboard yüklendi - Kullanıcı: {kullaniciAdi}, Rol: {rol}");
        }
        
        // Rol bilgisini güncelle ve butonları yeniden ayarla
        public void UpdateRole(string newRole)
        {
            this.rol = newRole;
            SetupRoleBasedAccess();
            Console.WriteLine($"Dashboard rol güncellendi - Yeni rol: {rol}");
        }

        private void btnKitaplar_Click(object sender, EventArgs e)
        {
            // Tüm rollerde kitap arama işlemi
            this.Hide();
            var kitapAramaForm = new KitapAramaForm(this);
            kitapAramaForm.Show();
        }

        private void btnUyeler_Click(object sender, EventArgs e)
        {
            // Profil formu aç
            ProfilForm profilForm = new ProfilForm(kullaniciAdi, rol, userData);
            profilForm.OnRoleUpdated = UpdateRole; // Rol güncellemesi için callback ayarla
            profilForm.ShowDialog();
        }

        private void btnEmanetler_Click(object sender, EventArgs e)
        {
            // Ödünç işlemleri (sadece görevli ve admin)
            this.Hide();
            var oduncForm = new OduncIslemleriForm(kullaniciAdi, rol, userData);
            oduncForm.Show();
        }

        // Kitap işlemleri formu
        private void btnKitapEkle_Click(object sender, EventArgs e)
        {
            // Sadece Admin kullanabilir
            if (!CheckIfUserIsAdmin())
            {
                MessageBox.Show("Bu işlem sadece Admin kullanıcılar tarafından yapılabilir.", "Yetki Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var kitapIslemleriForm = new KitapIslemleriForm();
            kitapIslemleriForm.ShowDialog();
        }

        private void btnRaporlar_Click(object sender, EventArgs e)
        {
            // Raporlar (sadece admin)
            MessageBox.Show("Raporlar sayfası açılıyor...", "Raporlar");
            // RaporlarForm.Show();
        }

        private void btnYonetim_Click(object sender, EventArgs e)
        {
            // Üyelik işlemleri formunu aç (Admin kontrolü ile)
            bool isAdmin = CheckIfUserIsAdmin();
            UyelikIslemleriForm uyelikForm = new UyelikIslemleriForm(isAdmin);
            uyelikForm.ShowDialog();
        }

        private bool CheckIfUserIsAdmin()
        {
            try
            {
                // userData'dan rol bilgilerini kontrol et
                if (userData != null && userData.rol_ids != null)
                {
                    var rolIds = userData.rol_ids as Newtonsoft.Json.Linq.JArray;
                    if (rolIds != null)
                    {
                        foreach (var rolId in rolIds)
                        {
                            // Admin rol ID'si genellikle 3'tür (API'ye göre değişebilir)
                            if (Convert.ToInt32(rolId) == 3)
                            {
                                return true;
                            }
                        }
                    }
                }

                // Alternatif olarak rol string'ini kontrol et
                if (!string.IsNullOrEmpty(rol))
                {
                    return rol.ToLower().Contains("admin");
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            // Çıkış işlemi
            Application.Exit();
        }

        private void btnKitaplarim_Click(object sender, EventArgs e)
        {
            // Kitaplarım formu aç
            KitaplarimForm kitaplarimForm = new KitaplarimForm(kullaniciAdi, rol, userData);
            kitaplarimForm.ShowDialog();
        }
    }
}
