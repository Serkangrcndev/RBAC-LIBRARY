using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Seker_kutuphane;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Seker_kutuphane
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            SetupTCRestrictions();
            SetupEnterKeyEvents();
        }

        private void SetupTCRestrictions()
        {
            // TC kimlik numarası için kısıtlamalar
            txtTC.MaxLength = 11; // 11 hane sınırı
            txtTC.KeyPress += TxtTC_KeyPress; // Sadece sayı girişi
            txtTC.TextChanged += TxtTC_TextChanged; // Boşluk engelleme
        }

        private void SetupEnterKeyEvents()
        {
            // TC alanında Enter tuşuna basıldığında şifre alanına geç
            txtTC.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtSifre.Focus();
                    e.Handled = true;
                }
            };

            // Şifre alanında Enter tuşuna basıldığında giriş yap
            txtSifre.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    PerformLogin();
                    e.Handled = true;
                }
            };
        }

        private void TxtTC_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Sadece sayı ve backspace'e izin ver
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtTC_TextChanged(object? sender, EventArgs e)
        {
            // Boşlukları kaldır
            if (txtTC.Text.Contains(" "))
            {
                txtTC.Text = txtTC.Text.Replace(" ", "");
                txtTC.SelectionStart = txtTC.Text.Length;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics v = e.Graphics;
            v.SmoothingMode = SmoothingMode.AntiAlias;
            v.Clear(panel1.BackColor);
            GraphicsPath p = new GraphicsPath();
            int radius = 20;
            p.AddArc(new Rectangle(0, 0, 2 * radius, 2 * radius), 180, 90);
            p.AddLine(new Point(radius, 0), new Point(panel1.Width - radius, 0));
            p.AddArc(new Rectangle(panel1.Width - 2 * radius, 0, 2 * radius, 2 * radius), -90, 90);
            p.AddLine(new Point(panel1.Width, radius), new Point(panel1.Width, panel1.Height - radius));
            p.AddArc(new Rectangle(panel1.Width - 2 * radius, panel1.Height - 2 * radius, 2 * radius, 2 * radius), 0, 90);
            p.AddLine(new Point(panel1.Width - radius, panel1.Height), new Point(radius, panel1.Height));
            p.AddArc(new Rectangle(0, panel1.Height - 2 * radius, 2 * radius, 2 * radius), 90, 90);
            p.CloseFigure();
            panel1.Region = new Region(p);
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Kayit kayitform = new Kayit();
            kayitform.Show();
            this.Hide();
        }

        private string Sha256Hash(string value)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private async void btnGirisYap_Click(object sender, EventArgs e)
        {
            await PerformLogin();
        }

        private async Task PerformLogin()
        {
            string tc = txtTC.Text.Trim();
            string sifre = txtSifre.Text.Trim();
            string hashedSifre = Sha256Hash(sifre);
            
            if (string.IsNullOrEmpty(tc) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Lütfen TC Kimlik No ve şifre giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            ApiHelper api = new ApiHelper();
            try
            {
                var (sessionId, user) = await api.LoginAsync(tc, hashedSifre);
                if (user != null)
                {
                    // Silinen kullanıcı kontrolü (status = 0)
                    var status = user.status;
                    if (status != null && Convert.ToInt32(status) == 0)
                    {
                        MessageBox.Show("Bu hesap silinmiş durumda!\n\nLütfen yönetici ile iletişime geçin.", "Hesap Silinmiş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string ad = user.ad ?? "";
                    
                    // Rol bilgisini rol_ids_str veya rol_adlari array'inden al
                    string rol = "";
                    try
                    {
                        // Önce rol_ids_str alanını kontrol et
                        if (user.rol_ids_str != null)
                        {
                            string rolIdsStr = user.rol_ids_str.ToString();
                            // rol_ids_str'den rol adlarını çıkar
                            if (rolIdsStr.Contains("1")) // Admin ID
                                rol = "Admin";
                            else if (rolIdsStr.Contains("2")) // Kütüphane Görevlisi ID
                                rol = "Kütüphane Görevlisi";
                            else if (rolIdsStr.Contains("3")) // Üye ID
                                rol = "Üye";
                            else
                                rol = "Üye"; // Varsayılan 
                        } //şu an çalışmıyor neden bilmiyoruz
                        
                        // Eğer rol_ids_str yoksa, rol_adlari array'ini kontrol et
                        if (string.IsNullOrEmpty(rol))
                        {
                            if (user.rol_adlari != null)
                            {
                                var rolAdlari = user.rol_adlari as Newtonsoft.Json.Linq.JArray;
                                if (rolAdlari != null && rolAdlari.Count > 0)
                                {
                                    // En yüksek yetkili rolü bul (Admin > Kütüphane Yetkilisi > Üye)
                                    string[] roller = rolAdlari.ToObject<string[]>() ?? new string[0];
                                    
                                    if (roller.Contains("Admin"))
                                        rol = "Admin";
                                    else if (roller.Contains("Kütüphane Yetkilisi") || roller.Contains("Kütüphane Görevlisi"))
                                        rol = "Kütüphane Görevlisi";
                                    else if (roller.Contains("Üye"))
                                        rol = "Üye";
                                    else
                                        rol = roller.Length > 0 ? roller[0] : "Üye"; // İlk rolü al
                                }
                            }
                        }
                        
                        // Eğer hala rol bulunamadıysa, eski yöntemleri dene
                        if (string.IsNullOrEmpty(rol))
                        {
                            rol = user.rol_adi ?? user.rol ?? user.role ?? user.role_name ?? user.rolAdi ?? "";
                        }
                        
                        // Eğer hala boşsa, varsayılan rol olarak ata
                        if (string.IsNullOrEmpty(rol))
                        {
                            rol = "Üye";
                        }
                    }
                    catch (Exception)
                    {
                        rol = "Üye";
                    }
                    
                    Dashboard dashboard = new Dashboard(ad, rol, user);
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("TC Kimlik Numarası veya şifre hatalı!\n\nLütfen bilgilerinizi kontrol edip tekrar deneyin.", "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Daha kullanıcı dostu hata mesajları
                string userFriendlyMessage = "TC Kimlik Numarası veya şifre hatalı!\n\nLütfen bilgilerinizi kontrol edip tekrar deneyin.";
                MessageBox.Show(userFriendlyMessage, "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sifre_yenileme yenile = new sifre_yenileme();
            yenile.Show();
            this.Hide();
        }



        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
