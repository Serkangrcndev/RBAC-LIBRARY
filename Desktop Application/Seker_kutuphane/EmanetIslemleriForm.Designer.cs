namespace Seker_kutuphane
{
    partial class OduncIslemleriForm
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
            panelHeader = new Panel();
            lblTitle = new Label();
            btnYeniEmanet = new Button();
            btnAnaSayfa = new Button();
            panelStats = new Panel();
            lblGecikmisEmanet = new Label();
            lblAktifEmanet = new Label();
            lblToplamEmanet = new Label();
            lblGecikmisEmanetTitle = new Label();
            lblAktifEmanetTitle = new Label();
            lblToplamEmanetTitle = new Label();
            panelSearch = new Panel();
            txtArama = new TextBox();
            lblArama = new Label();
            btnYenile = new Button();
            dataGridViewEmanetler = new DataGridView();
            panelActions = new Panel();
            btnIadeEt = new Button();
            panelHeader.SuspendLayout();
            panelStats.SuspendLayout();
            panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmanetler).BeginInit();
            panelActions.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(76, 175, 80);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(btnYeniEmanet);
            panelHeader.Controls.Add(btnAnaSayfa);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1200, 80);
            panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(194, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Ödünç İşlemleri";
            // 
            // btnYeniEmanet
            // 
            btnYeniEmanet.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnYeniEmanet.BackColor = Color.FromArgb(129, 199, 132);
            btnYeniEmanet.FlatAppearance.BorderSize = 0;
            btnYeniEmanet.FlatStyle = FlatStyle.Flat;
            btnYeniEmanet.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnYeniEmanet.ForeColor = Color.White;
            btnYeniEmanet.Location = new Point(1050, 20);
            btnYeniEmanet.Name = "btnYeniEmanet";
            btnYeniEmanet.Size = new Size(130, 40);
            btnYeniEmanet.TabIndex = 1;
            btnYeniEmanet.Text = "+ Yeni Ödünç";
            btnYeniEmanet.UseVisualStyleBackColor = false;
            btnYeniEmanet.Click += btnYeniOdunc_Click;
            // 
            // btnAnaSayfa
            // 
            btnAnaSayfa.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAnaSayfa.BackColor = Color.FromArgb(33, 150, 243);
            btnAnaSayfa.FlatAppearance.BorderSize = 0;
            btnAnaSayfa.FlatStyle = FlatStyle.Flat;
            btnAnaSayfa.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnAnaSayfa.ForeColor = Color.White;
            btnAnaSayfa.Location = new Point(900, 20);
            btnAnaSayfa.Name = "btnAnaSayfa";
            btnAnaSayfa.Size = new Size(130, 40);
            btnAnaSayfa.TabIndex = 2;
            btnAnaSayfa.Text = "← Ana Sayfa";
            btnAnaSayfa.UseVisualStyleBackColor = false;
            btnAnaSayfa.Click += btnAnaSayfa_Click;
            // 
            // panelStats
            // 
            panelStats.BackColor = Color.White;
            panelStats.Controls.Add(lblGecikmisEmanet);
            panelStats.Controls.Add(lblAktifEmanet);
            panelStats.Controls.Add(lblToplamEmanet);
            panelStats.Controls.Add(lblGecikmisEmanetTitle);
            panelStats.Controls.Add(lblAktifEmanetTitle);
            panelStats.Controls.Add(lblToplamEmanetTitle);
            panelStats.Dock = DockStyle.Top;
            panelStats.Location = new Point(0, 80);
            panelStats.Name = "panelStats";
            panelStats.Size = new Size(1200, 100);
            panelStats.TabIndex = 1;
            // 
            // lblGecikmisEmanet
            // 
            lblGecikmisEmanet.AutoSize = true;
            lblGecikmisEmanet.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblGecikmisEmanet.ForeColor = Color.FromArgb(244, 67, 54);
            lblGecikmisEmanet.Location = new Point(900, 30);
            lblGecikmisEmanet.Name = "lblGecikmisEmanet";
            lblGecikmisEmanet.Size = new Size(38, 45);
            lblGecikmisEmanet.TabIndex = 5;
            lblGecikmisEmanet.Text = "0";
            // 
            // lblAktifEmanet
            // 
            lblAktifEmanet.AutoSize = true;
            lblAktifEmanet.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblAktifEmanet.ForeColor = Color.FromArgb(76, 175, 80);
            lblAktifEmanet.Location = new Point(500, 30);
            lblAktifEmanet.Name = "lblAktifEmanet";
            lblAktifEmanet.Size = new Size(38, 45);
            lblAktifEmanet.TabIndex = 4;
            lblAktifEmanet.Text = "0";
            // 
            // lblToplamEmanet
            // 
            lblToplamEmanet.AutoSize = true;
            lblToplamEmanet.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblToplamEmanet.ForeColor = Color.FromArgb(33, 150, 243);
            lblToplamEmanet.Location = new Point(100, 30);
            lblToplamEmanet.Name = "lblToplamEmanet";
            lblToplamEmanet.Size = new Size(38, 45);
            lblToplamEmanet.TabIndex = 3;
            lblToplamEmanet.Text = "0";
            // 
            // lblGecikmisEmanetTitle
            // 
            lblGecikmisEmanetTitle.AutoSize = true;
            lblGecikmisEmanetTitle.Font = new Font("Segoe UI", 10F);
            lblGecikmisEmanetTitle.ForeColor = Color.Gray;
            lblGecikmisEmanetTitle.Location = new Point(800, 75);
            lblGecikmisEmanetTitle.Name = "lblGecikmisEmanetTitle";
            lblGecikmisEmanetTitle.Size = new Size(108, 19);
            lblGecikmisEmanetTitle.TabIndex = 2;
            lblGecikmisEmanetTitle.Text = "Gecikmiş Ödünç";
            // 
            // lblAktifEmanetTitle
            // 
            lblAktifEmanetTitle.AutoSize = true;
            lblAktifEmanetTitle.Font = new Font("Segoe UI", 10F);
            lblAktifEmanetTitle.ForeColor = Color.Gray;
            lblAktifEmanetTitle.Location = new Point(400, 75);
            lblAktifEmanetTitle.Name = "lblAktifEmanetTitle";
            lblAktifEmanetTitle.Size = new Size(82, 19);
            lblAktifEmanetTitle.TabIndex = 1;
            lblAktifEmanetTitle.Text = "Aktif Ödünç";
            // 
            // lblToplamEmanetTitle
            // 
            lblToplamEmanetTitle.AutoSize = true;
            lblToplamEmanetTitle.Font = new Font("Segoe UI", 10F);
            lblToplamEmanetTitle.ForeColor = Color.Gray;
            lblToplamEmanetTitle.Location = new Point(0, 75);
            lblToplamEmanetTitle.Name = "lblToplamEmanetTitle";
            lblToplamEmanetTitle.Size = new Size(98, 19);
            lblToplamEmanetTitle.TabIndex = 0;
            lblToplamEmanetTitle.Text = "Toplam Ödünç";
            lblToplamEmanetTitle.Click += lblToplamEmanetTitle_Click;
            // 
            // panelSearch
            // 
            panelSearch.BackColor = Color.White;
            panelSearch.Controls.Add(txtArama);
            panelSearch.Controls.Add(lblArama);
            panelSearch.Controls.Add(btnYenile);
            panelSearch.Dock = DockStyle.Top;
            panelSearch.Location = new Point(0, 180);
            panelSearch.Name = "panelSearch";
            panelSearch.Size = new Size(1200, 60);
            panelSearch.TabIndex = 2;
            // 
            // txtArama
            // 
            txtArama.Font = new Font("Segoe UI", 12F);
            txtArama.Location = new Point(20, 20);
            txtArama.Name = "txtArama";
            txtArama.PlaceholderText = "🔍 Emanet ara...";
            txtArama.Size = new Size(300, 29);
            txtArama.TabIndex = 2;
            txtArama.TextChanged += txtArama_TextChanged;
            // 
            // lblArama
            // 
            lblArama.AutoSize = true;
            lblArama.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblArama.ForeColor = Color.FromArgb(64, 64, 64);
            lblArama.Location = new Point(340, 23);
            lblArama.Name = "lblArama";
            lblArama.Size = new Size(64, 21);
            lblArama.TabIndex = 1;
            lblArama.Text = "Arama:";
            // 
            // btnYenile
            // 
            btnYenile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnYenile.BackColor = Color.FromArgb(33, 150, 243);
            btnYenile.FlatAppearance.BorderSize = 0;
            btnYenile.FlatStyle = FlatStyle.Flat;
            btnYenile.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnYenile.ForeColor = Color.White;
            btnYenile.Location = new Point(1100, 20);
            btnYenile.Name = "btnYenile";
            btnYenile.Size = new Size(80, 30);
            btnYenile.TabIndex = 0;
            btnYenile.Text = "Yenile";
            btnYenile.UseVisualStyleBackColor = false;
            btnYenile.Click += btnYenile_Click;
            // 
            // dataGridViewEmanetler
            // 
            dataGridViewEmanetler.AllowUserToAddRows = false;
            dataGridViewEmanetler.AllowUserToDeleteRows = false;
            dataGridViewEmanetler.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewEmanetler.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewEmanetler.BackgroundColor = Color.White;
            dataGridViewEmanetler.BorderStyle = BorderStyle.None;
            dataGridViewEmanetler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEmanetler.Location = new Point(20, 20);
            dataGridViewEmanetler.MultiSelect = false;
            dataGridViewEmanetler.Name = "dataGridViewEmanetler";
            dataGridViewEmanetler.ReadOnly = true;
            dataGridViewEmanetler.RowHeadersVisible = false;
            dataGridViewEmanetler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewEmanetler.Size = new Size(1160, 400);
            dataGridViewEmanetler.TabIndex = 0;
            dataGridViewEmanetler.CellFormatting += dataGridViewEmanetler_CellFormatting;
            // 
            // panelActions
            // 
            panelActions.BackColor = Color.White;
            panelActions.Controls.Add(btnIadeEt);
            panelActions.Controls.Add(dataGridViewEmanetler);
            panelActions.Dock = DockStyle.Fill;
            panelActions.Location = new Point(0, 240);
            panelActions.Name = "panelActions";
            panelActions.Size = new Size(1200, 460);
            panelActions.TabIndex = 3;
            // 
            // btnIadeEt
            // 
            btnIadeEt.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnIadeEt.BackColor = Color.FromArgb(76, 175, 80);
            btnIadeEt.FlatAppearance.BorderSize = 0;
            btnIadeEt.FlatStyle = FlatStyle.Flat;
            btnIadeEt.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnIadeEt.ForeColor = Color.White;
            btnIadeEt.Location = new Point(1050, 420);
            btnIadeEt.Name = "btnIadeEt";
            btnIadeEt.Size = new Size(130, 35);
            btnIadeEt.TabIndex = 1;
            btnIadeEt.Text = "İade Et";
            btnIadeEt.UseVisualStyleBackColor = false;
            btnIadeEt.Click += btnIadeEt_Click;
            // 
            // OduncIslemleriForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1200, 700);
            Controls.Add(panelActions);
            Controls.Add(panelSearch);
            Controls.Add(panelStats);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.None;
            Name = "OduncIslemleriForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Emanet İşlemleri";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelStats.ResumeLayout(false);
            panelStats.PerformLayout();
            panelSearch.ResumeLayout(false);
            panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEmanetler).EndInit();
            panelActions.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private Panel panelHeader;
        private Label lblTitle;
        private Button btnYeniEmanet;
        private Button btnAnaSayfa;
        private Panel panelStats;
        private Label lblGecikmisEmanet;
        private Label lblAktifEmanet;
        private Label lblToplamEmanet;
        private Label lblGecikmisEmanetTitle;
        private Label lblAktifEmanetTitle;
        private Label lblToplamEmanetTitle;
        private Panel panelSearch;
        private TextBox txtArama;
        private Label lblArama;
        private Button btnYenile;
        private DataGridView dataGridViewEmanetler;
        private Panel panelActions;
        private Button btnIadeEt;
    }
} 