using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Seker_kutuphane
{
    public partial class sifre_yenileme : Form
    {
        public sifre_yenileme()
        {
            InitializeComponent();
            SetupTCRestrictions();
            SetupEnterKeyEvents();
        }

        private void SetupTCRestrictions()
        {
            // TC kimlik numarası için sadece rakam girişi ve 11 hane sınırlaması
            textBox3.MaxLength = 11;
            textBox3.KeyPress += TextBox3_KeyPress;
        }

        private void TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece rakam girişine izin ver
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SetupEnterKeyEvents()
        {
            // TC textbox'ına Enter tuşu desteği ekle
            textBox3.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) button1.PerformClick(); };
        }

        private void sifre_yenileme_Load(object sender, EventArgs e)
        {
            // Panelin köşelerini oval yap
            panel1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, panel1.Width, panel1.Height, 30, 30));
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string tc = textBox3.Text.Trim();
            if (string.IsNullOrEmpty(tc))
            {
                MessageBox.Show("Lütfen TC kimlik numaranızı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // TC kimlik numarası 11 hane olmalı
            if (tc.Length != 11)
            {
                MessageBox.Show("TC kimlik numarası 11 haneli olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            ApiHelper api = new ApiHelper();
            bool isValid = await api.VerifyTCAsync(tc);
            if (isValid)
            {
                sifreBelirle sifreForm = new sifreBelirle(tc);
                sifreForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Girilen TC numarası sistemde bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Login giris = new Login();
            giris.Show();
            this.Hide();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
