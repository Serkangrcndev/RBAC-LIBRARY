namespace Seker_kutuphane
{
    partial class KitaplarimForm
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
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Label lblBilgi;
        private System.Windows.Forms.DataGridView dgvKitaplarim;
        private System.Windows.Forms.Button btnGeri;
        private System.Windows.Forms.Button btnYenile;
        private System.Windows.Forms.Panel panelEmptyState;
        private System.Windows.Forms.Label lblEmptyIcon;
        private System.Windows.Forms.Label lblEmptyTitle;
        private System.Windows.Forms.Label lblEmptyMessage;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblKitapSayisi;
        private System.Windows.Forms.Label lblMaxKitap;
        private System.Windows.Forms.ProgressBar progressBar;

        private void InitializeComponent()
        {
            panelMain = new Panel();
            lblBaslik = new Label();
            panelStats = new Panel();
            lblKitapSayisi = new Label();
            lblMaxKitap = new Label();
            progressBar = new ProgressBar();
            panelEmptyState = new Panel();
            lblEmptyIcon = new Label();
            lblEmptyTitle = new Label();
            lblEmptyMessage = new Label();
            lblBilgi = new Label();
            dgvKitaplarim = new DataGridView();
            btnGeri = new Button();
            btnYenile = new Button();
            panelMain.SuspendLayout();
            panelStats.SuspendLayout();
            panelEmptyState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKitaplarim).BeginInit();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(248, 249, 250);
            panelMain.Controls.Add(lblBaslik);
            panelMain.Controls.Add(panelStats);
            panelMain.Controls.Add(panelEmptyState);
            panelMain.Controls.Add(lblBilgi);
            panelMain.Controls.Add(dgvKitaplarim);
            panelMain.Controls.Add(btnGeri);
            panelMain.Controls.Add(btnYenile);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(900, 650);
            panelMain.TabIndex = 0;
            // 
            // lblBaslik
            // 
            lblBaslik.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblBaslik.ForeColor = Color.FromArgb(0, 128, 0);
            lblBaslik.Location = new Point(20, 20);
            lblBaslik.Name = "lblBaslik";
            lblBaslik.Size = new Size(860, 50);
            lblBaslik.TabIndex = 0;
            lblBaslik.Text = "📚 Kitaplarım";
            lblBaslik.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelStats
            // 
            panelStats.BackColor = Color.White;
            panelStats.Controls.Add(lblKitapSayisi);
            panelStats.Controls.Add(lblMaxKitap);
            panelStats.Controls.Add(progressBar);
            panelStats.Location = new Point(20, 80);
            panelStats.Name = "panelStats";
            panelStats.Size = new Size(860, 80);
            panelStats.TabIndex = 1;
            // 
            // lblKitapSayisi
            // 
            lblKitapSayisi.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblKitapSayisi.ForeColor = Color.FromArgb(0, 128, 0);
            lblKitapSayisi.Location = new Point(20, 15);
            lblKitapSayisi.Name = "lblKitapSayisi";
            lblKitapSayisi.Size = new Size(200, 30);
            lblKitapSayisi.TabIndex = 0;
            lblKitapSayisi.Text = "Ödünç Alınan: 0";
            // 
            // lblMaxKitap
            // 
            lblMaxKitap.Font = new Font("Segoe UI", 12F);
            lblMaxKitap.ForeColor = Color.FromArgb(100, 100, 100);
            lblMaxKitap.Location = new Point(20, 45);
            lblMaxKitap.Name = "lblMaxKitap";
            lblMaxKitap.Size = new Size(200, 25);
            lblMaxKitap.TabIndex = 1;
            lblMaxKitap.Text = "Maksimum: 3 kitap";
            // 
            // progressBar
            // 
            progressBar.BackColor = Color.FromArgb(240, 240, 240);
            progressBar.ForeColor = Color.FromArgb(0, 128, 0);
            progressBar.Location = new Point(240, 20);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(600, 20);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 2;
            // 
            // panelEmptyState
            // 
            panelEmptyState.BackColor = Color.White;
            panelEmptyState.Controls.Add(lblEmptyIcon);
            panelEmptyState.Controls.Add(lblEmptyTitle);
            panelEmptyState.Controls.Add(lblEmptyMessage);
            panelEmptyState.Location = new Point(20, 180);
            panelEmptyState.Name = "panelEmptyState";
            panelEmptyState.Size = new Size(860, 350);
            panelEmptyState.TabIndex = 2;
            // 
            // lblEmptyIcon
            // 
            lblEmptyIcon.Font = new Font("Segoe UI", 72F);
            lblEmptyIcon.ForeColor = Color.FromArgb(200, 200, 200);
            lblEmptyIcon.Location = new Point(0, 50);
            lblEmptyIcon.Name = "lblEmptyIcon";
            lblEmptyIcon.Size = new Size(860, 100);
            lblEmptyIcon.TabIndex = 0;
            lblEmptyIcon.Text = "📖";
            lblEmptyIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblEmptyTitle
            // 
            lblEmptyTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblEmptyTitle.ForeColor = Color.FromArgb(100, 100, 100);
            lblEmptyTitle.Location = new Point(0, 160);
            lblEmptyTitle.Name = "lblEmptyTitle";
            lblEmptyTitle.Size = new Size(860, 40);
            lblEmptyTitle.TabIndex = 1;
            lblEmptyTitle.Text = "Henüz Kitap Ödünç Almamışsınız";
            lblEmptyTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblEmptyMessage
            // 
            lblEmptyMessage.Font = new Font("Segoe UI", 12F);
            lblEmptyMessage.ForeColor = Color.FromArgb(150, 150, 150);
            lblEmptyMessage.Location = new Point(0, 200);
            lblEmptyMessage.Name = "lblEmptyMessage";
            lblEmptyMessage.Size = new Size(860, 60);
            lblEmptyMessage.TabIndex = 2;
            lblEmptyMessage.Text = "Kitap arama sayfasından istediğiniz kitapları ödünç alabilirsiniz.\nEn fazla 3 adet kitap ödünç alabilirsiniz.";
            lblEmptyMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblBilgi
            // 
            lblBilgi.Font = new Font("Segoe UI", 12F);
            lblBilgi.ForeColor = Color.FromArgb(0, 128, 0);
            lblBilgi.Location = new Point(20, 170);
            lblBilgi.Name = "lblBilgi";
            lblBilgi.Size = new Size(860, 30);
            lblBilgi.TabIndex = 3;
            lblBilgi.Text = "Kitaplarınız yükleniyor...";
            lblBilgi.TextAlign = ContentAlignment.MiddleCenter;
            lblBilgi.Visible = false;
            // 
            // dgvKitaplarim
            // 
            dgvKitaplarim.AllowUserToAddRows = false;
            dgvKitaplarim.AllowUserToDeleteRows = false;
            dgvKitaplarim.AllowUserToResizeRows = false;
            dgvKitaplarim.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvKitaplarim.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKitaplarim.BackgroundColor = Color.White;
            dgvKitaplarim.BorderStyle = BorderStyle.None;
            dgvKitaplarim.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKitaplarim.GridColor = Color.FromArgb(224, 224, 224);
            dgvKitaplarim.Location = new Point(20, 180);
            dgvKitaplarim.Name = "dgvKitaplarim";
            dgvKitaplarim.ReadOnly = true;
            dgvKitaplarim.RowHeadersVisible = false;
            dgvKitaplarim.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKitaplarim.Size = new Size(860, 350);
            dgvKitaplarim.TabIndex = 4;
            dgvKitaplarim.Visible = false;
            dgvKitaplarim.CellFormatting += DgvKitaplarim_CellFormatting;
            // 
            // btnGeri
            // 
            btnGeri.BackColor = Color.FromArgb(100, 100, 100);
            btnGeri.FlatAppearance.BorderSize = 0;
            btnGeri.FlatStyle = FlatStyle.Flat;
            btnGeri.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnGeri.ForeColor = Color.White;
            btnGeri.Location = new Point(20, 580);
            btnGeri.Name = "btnGeri";
            btnGeri.Size = new Size(120, 45);
            btnGeri.TabIndex = 5;
            btnGeri.Text = "← Geri";
            btnGeri.UseVisualStyleBackColor = false;
            btnGeri.Click += btnGeri_Click;
            // 
            // btnYenile
            // 
            btnYenile.BackColor = Color.FromArgb(0, 128, 0);
            btnYenile.FlatAppearance.BorderSize = 0;
            btnYenile.FlatStyle = FlatStyle.Flat;
            btnYenile.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnYenile.ForeColor = Color.White;
            btnYenile.Location = new Point(760, 580);
            btnYenile.Name = "btnYenile";
            btnYenile.Size = new Size(120, 45);
            btnYenile.TabIndex = 6;
            btnYenile.Text = "🔄 Yenile";
            btnYenile.UseVisualStyleBackColor = false;
            btnYenile.Click += btnYenile_Click;
            // 
            // KitaplarimForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 650);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.None;
            Name = "KitaplarimForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ye";
            panelMain.ResumeLayout(false);
            panelStats.ResumeLayout(false);
            panelEmptyState.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvKitaplarim).EndInit();
            ResumeLayout(false);
        }

        private void DgvKitaplarim_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Tarih formatını düzenle
            if (e.Value is DateTime dateTime)
            {
                e.Value = dateTime.ToString("dd.MM.yyyy");
                e.FormattingApplied = true;
            }
        }

        #endregion
    }
} 