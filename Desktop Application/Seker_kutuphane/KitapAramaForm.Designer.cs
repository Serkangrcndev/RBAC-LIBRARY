namespace Seker_kutuphane
{
    partial class KitapAramaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblBaslik = new Label();
            panelArama = new Panel();
            lblAramaTuru = new Label();
            cmbAramaTuru = new ComboBox();
            lblKitapAdi = new Label();
            txtKitapAdi = new TextBox();
            lblYazar = new Label();
            txtYazar = new TextBox();
            lblYil = new Label();
            txtYil = new TextBox();
            lblYayinevi = new Label();
            txtYayinevi = new TextBox();
            btnAra = new Button();
            btnTemizle = new Button();
            dgvKitaplar = new DataGridView();
            btnGeri = new Button();
            lblSonuc = new Label();
            panelSonuc = new Panel();
            panelArama.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKitaplar).BeginInit();
            panelSonuc.SuspendLayout();
            SuspendLayout();
            // 
            // lblBaslik
            // 
            lblBaslik.Dock = DockStyle.Top;
            lblBaslik.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(0, 128, 0);
            lblBaslik.Location = new Point(0, 0);
            lblBaslik.Name = "lblBaslik";
            lblBaslik.Size = new Size(1000, 60);
            lblBaslik.TabIndex = 0;
            lblBaslik.Text = "📚 Kitap Arama Motoru";
            lblBaslik.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelArama
            // 
            panelArama.BackColor = Color.White;
            panelArama.Controls.Add(lblAramaTuru);
            panelArama.Controls.Add(cmbAramaTuru);
            panelArama.Controls.Add(lblKitapAdi);
            panelArama.Controls.Add(txtKitapAdi);
            panelArama.Controls.Add(lblYazar);
            panelArama.Controls.Add(txtYazar);
            panelArama.Controls.Add(lblYil);
            panelArama.Controls.Add(txtYil);
            panelArama.Controls.Add(lblYayinevi);
            panelArama.Controls.Add(txtYayinevi);
            panelArama.Controls.Add(btnAra);
            panelArama.Controls.Add(btnTemizle);
            panelArama.Dock = DockStyle.Top;
            panelArama.Location = new Point(0, 60);
            panelArama.Name = "panelArama";
            panelArama.Padding = new Padding(20);
            panelArama.Size = new Size(1000, 200);
            panelArama.TabIndex = 1;
            // 
            // lblAramaTuru
            // 
            lblAramaTuru.AutoSize = true;
            lblAramaTuru.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAramaTuru.ForeColor = Color.FromArgb(0, 128, 0);
            lblAramaTuru.Location = new Point(20, 20);
            lblAramaTuru.Name = "lblAramaTuru";
            lblAramaTuru.Size = new Size(91, 19);
            lblAramaTuru.TabIndex = 0;
            lblAramaTuru.Text = "Arama Türü:";
            // 
            // cmbAramaTuru
            // 
            cmbAramaTuru.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAramaTuru.Font = new Font("Segoe UI", 10F);
            cmbAramaTuru.FormattingEnabled = true;
            cmbAramaTuru.Items.AddRange(new object[] { "Tümü", "Kitap Adı", "Yazar", "Yıl", "Yayınevi" });
            cmbAramaTuru.Location = new Point(110, 17);
            cmbAramaTuru.Name = "cmbAramaTuru";
            cmbAramaTuru.Size = new Size(150, 25);
            cmbAramaTuru.TabIndex = 1;
            cmbAramaTuru.SelectedIndexChanged += cmbAramaTuru_SelectedIndexChanged;
            // 
            // lblKitapAdi
            // 
            lblKitapAdi.AutoSize = true;
            lblKitapAdi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblKitapAdi.ForeColor = Color.FromArgb(0, 128, 0);
            lblKitapAdi.Location = new Point(281, 20);
            lblKitapAdi.Name = "lblKitapAdi";
            lblKitapAdi.Size = new Size(99, 19);
            lblKitapAdi.TabIndex = 2;
            lblKitapAdi.Text = "📖 Kitap Adı:";
            lblKitapAdi.Click += lblKitapAdi_Click;
            // 
            // txtKitapAdi
            // 
            txtKitapAdi.Font = new Font("Segoe UI", 10F);
            txtKitapAdi.Location = new Point(386, 17);
            txtKitapAdi.Name = "txtKitapAdi";
            txtKitapAdi.Size = new Size(200, 25);
            txtKitapAdi.TabIndex = 3;
            txtKitapAdi.KeyPress += txtKitapAdi_KeyPress;
            // 
            // lblYazar
            // 
            lblYazar.AutoSize = true;
            lblYazar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblYazar.ForeColor = Color.FromArgb(0, 128, 0);
            lblYazar.Location = new Point(599, 20);
            lblYazar.Name = "lblYazar";
            lblYazar.Size = new Size(75, 19);
            lblYazar.TabIndex = 4;
            lblYazar.Text = "✍️ Yazar:";
            // 
            // txtYazar
            // 
            txtYazar.Font = new Font("Segoe UI", 10F);
            txtYazar.Location = new Point(680, 17);
            txtYazar.Name = "txtYazar";
            txtYazar.Size = new Size(200, 25);
            txtYazar.TabIndex = 5;
            txtYazar.KeyPress += txtYazar_KeyPress;
            // 
            // lblYil
            // 
            lblYil.AutoSize = true;
            lblYil.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblYil.ForeColor = Color.FromArgb(0, 128, 0);
            lblYil.Location = new Point(20, 82);
            lblYil.Name = "lblYil";
            lblYil.Size = new Size(54, 19);
            lblYil.TabIndex = 6;
            lblYil.Text = "📅 Yıl:";
            // 
            // txtYil
            // 
            txtYil.Font = new Font("Segoe UI", 10F);
            txtYil.Location = new Point(110, 79);
            txtYil.Name = "txtYil";
            txtYil.Size = new Size(100, 25);
            txtYil.TabIndex = 7;
            txtYil.KeyPress += txtYil_KeyPress;
            // 
            // lblYayinevi
            // 
            lblYayinevi.AutoSize = true;
            lblYayinevi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblYayinevi.ForeColor = Color.FromArgb(0, 128, 0);
            lblYayinevi.Location = new Point(284, 82);
            lblYayinevi.Name = "lblYayinevi";
            lblYayinevi.Size = new Size(93, 19);
            lblYayinevi.TabIndex = 8;
            lblYayinevi.Text = "🏢 Yayınevi:";
            // 
            // txtYayinevi
            // 
            txtYayinevi.Font = new Font("Segoe UI", 10F);
            txtYayinevi.Location = new Point(385, 79);
            txtYayinevi.Name = "txtYayinevi";
            txtYayinevi.Size = new Size(200, 25);
            txtYayinevi.TabIndex = 9;
            txtYayinevi.KeyPress += txtYayinevi_KeyPress;
            // 
            // btnAra
            // 
            btnAra.BackColor = Color.FromArgb(0, 128, 0);
            btnAra.FlatAppearance.BorderSize = 0;
            btnAra.FlatStyle = FlatStyle.Flat;
            btnAra.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAra.ForeColor = Color.White;
            btnAra.Location = new Point(613, 79);
            btnAra.Name = "btnAra";
            btnAra.Size = new Size(120, 30);
            btnAra.TabIndex = 10;
            btnAra.Text = "🔍 Ara";
            btnAra.UseVisualStyleBackColor = false;
            btnAra.Click += btnAra_Click;
            // 
            // btnTemizle
            // 
            btnTemizle.BackColor = Color.FromArgb(100, 100, 100);
            btnTemizle.FlatAppearance.BorderSize = 0;
            btnTemizle.FlatStyle = FlatStyle.Flat;
            btnTemizle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnTemizle.ForeColor = Color.White;
            btnTemizle.Location = new Point(759, 79);
            btnTemizle.Name = "btnTemizle";
            btnTemizle.Size = new Size(120, 30);
            btnTemizle.TabIndex = 11;
            btnTemizle.Text = "\U0001f9f9 Temizle";
            btnTemizle.UseVisualStyleBackColor = false;
            btnTemizle.Click += btnTemizle_Click;
            // 
            // dgvKitaplar
            // 
            dgvKitaplar.AllowUserToAddRows = false;
            dgvKitaplar.AllowUserToDeleteRows = false;
            dgvKitaplar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKitaplar.BackgroundColor = Color.White;
            dgvKitaplar.BorderStyle = BorderStyle.None;
            dgvKitaplar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKitaplar.Dock = DockStyle.Fill;
            dgvKitaplar.GridColor = Color.FromArgb(224, 224, 224);
            dgvKitaplar.Location = new Point(0, 260);
            dgvKitaplar.Name = "dgvKitaplar";
            dgvKitaplar.ReadOnly = true;
            dgvKitaplar.RowHeadersVisible = false;
            dgvKitaplar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKitaplar.Size = new Size(1000, 390);
            dgvKitaplar.TabIndex = 4;
            // 
            // btnGeri
            // 
            btnGeri.Anchor = AnchorStyles.Right;
            btnGeri.Location = new Point(860, 10);
            btnGeri.Name = "btnGeri";
            btnGeri.Size = new Size(120, 30);
            btnGeri.TabIndex = 1;
            btnGeri.Text = "Geri Dön";
            btnGeri.UseVisualStyleBackColor = false;
            btnGeri.Click += btnGeri_Click;
            // 
            // lblSonuc
            // 
            lblSonuc.Anchor = AnchorStyles.Left;
            lblSonuc.AutoSize = true;
            lblSonuc.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSonuc.ForeColor = Color.FromArgb(0, 128, 0);
            lblSonuc.Location = new Point(20, 15);
            lblSonuc.Name = "lblSonuc";
            lblSonuc.Size = new Size(0, 19);
            lblSonuc.TabIndex = 0;
            // 
            // panelSonuc
            // 
            panelSonuc.BackColor = Color.White;
            panelSonuc.Controls.Add(lblSonuc);
            panelSonuc.Controls.Add(btnGeri);
            panelSonuc.Dock = DockStyle.Bottom;
            panelSonuc.Location = new Point(0, 650);
            panelSonuc.Name = "panelSonuc";
            panelSonuc.Padding = new Padding(20);
            panelSonuc.Size = new Size(1000, 50);
            panelSonuc.TabIndex = 3;
            // 
            // KitapAramaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 700);
            Controls.Add(dgvKitaplar);
            Controls.Add(panelSonuc);
            Controls.Add(panelArama);
            Controls.Add(lblBaslik);
            FormBorderStyle = FormBorderStyle.None;
            Name = "KitapAramaForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kitap Arama";
            FormClosing += KitapAramaForm_FormClosing;
            panelArama.ResumeLayout(false);
            panelArama.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKitaplar).EndInit();
            panelSonuc.ResumeLayout(false);
            panelSonuc.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblBaslik;
        private Button btnAra;
        private Button btnTemizle;
        private DataGridView dgvKitaplar;
        private Button btnGeri;
        private Label lblSonuc;
        private Panel panelArama;
        private Panel panelSonuc;
        private Label lblKitapAdi;
        private TextBox txtKitapAdi;
        private Label lblYazar;
        private TextBox txtYazar;
        private Label lblYil;
        private TextBox txtYil;
        private Label lblYayinevi;
        private TextBox txtYayinevi;
        private ComboBox cmbAramaTuru;
        private Label lblAramaTuru;
    }
} 