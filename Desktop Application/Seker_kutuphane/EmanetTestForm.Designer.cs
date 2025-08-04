namespace Seker_kutuphane
{
    partial class EmanetTestForm
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnTestTumEmanetler = new System.Windows.Forms.Button();
            this.btnTestGecikmisEmanetler = new System.Windows.Forms.Button();
            this.btnTestEmanetAra = new System.Windows.Forms.Button();
            this.btnTestEmanetIade = new System.Windows.Forms.Button();
            this.btnTestEmanetEkle = new System.Windows.Forms.Button();
            this.btnTestEmanetler = new System.Windows.Forms.Button();
            this.panelInputs = new System.Windows.Forms.Panel();
            this.txtAramaTerm = new System.Windows.Forms.TextBox();
            this.lblAramaTerm = new System.Windows.Forms.Label();
            this.txtEmanetId = new System.Windows.Forms.TextBox();
            this.lblEmanetId = new System.Windows.Forms.Label();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.panelHeader.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelInputs.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(800, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(200, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Emanet API Test";
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.White;
            this.panelButtons.Controls.Add(this.btnTestTumEmanetler);
            this.panelButtons.Controls.Add(this.btnTestGecikmisEmanetler);
            this.panelButtons.Controls.Add(this.btnTestEmanetAra);
            this.panelButtons.Controls.Add(this.btnTestEmanetIade);
            this.panelButtons.Controls.Add(this.btnTestEmanetEkle);
            this.panelButtons.Controls.Add(this.btnTestEmanetler);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelButtons.Location = new System.Drawing.Point(0, 60);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(200, 540);
            this.panelButtons.TabIndex = 1;
            // 
            // btnTestTumEmanetler
            // 
            this.btnTestTumEmanetler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnTestTumEmanetler.FlatAppearance.BorderSize = 0;
            this.btnTestTumEmanetler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestTumEmanetler.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTestTumEmanetler.ForeColor = System.Drawing.Color.White;
            this.btnTestTumEmanetler.Location = new System.Drawing.Point(10, 250);
            this.btnTestTumEmanetler.Name = "btnTestTumEmanetler";
            this.btnTestTumEmanetler.Size = new System.Drawing.Size(180, 35);
            this.btnTestTumEmanetler.TabIndex = 5;
            this.btnTestTumEmanetler.Text = "Tüm Emanetler";
            this.btnTestTumEmanetler.UseVisualStyleBackColor = false;
            this.btnTestTumEmanetler.Click += new System.EventHandler(this.btnTestTumEmanetler_Click);
            // 
            // btnTestGecikmisEmanetler
            // 
            this.btnTestGecikmisEmanetler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnTestGecikmisEmanetler.FlatAppearance.BorderSize = 0;
            this.btnTestGecikmisEmanetler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestGecikmisEmanetler.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTestGecikmisEmanetler.ForeColor = System.Drawing.Color.White;
            this.btnTestGecikmisEmanetler.Location = new System.Drawing.Point(10, 200);
            this.btnTestGecikmisEmanetler.Name = "btnTestGecikmisEmanetler";
            this.btnTestGecikmisEmanetler.Size = new System.Drawing.Size(180, 35);
            this.btnTestGecikmisEmanetler.TabIndex = 4;
            this.btnTestGecikmisEmanetler.Text = "Gecikmiş Emanetler";
            this.btnTestGecikmisEmanetler.UseVisualStyleBackColor = false;
            this.btnTestGecikmisEmanetler.Click += new System.EventHandler(this.btnTestGecikmisEmanetler_Click);
            // 
            // btnTestEmanetAra
            // 
            this.btnTestEmanetAra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnTestEmanetAra.FlatAppearance.BorderSize = 0;
            this.btnTestEmanetAra.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestEmanetAra.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTestEmanetAra.ForeColor = System.Drawing.Color.White;
            this.btnTestEmanetAra.Location = new System.Drawing.Point(10, 150);
            this.btnTestEmanetAra.Name = "btnTestEmanetAra";
            this.btnTestEmanetAra.Size = new System.Drawing.Size(180, 35);
            this.btnTestEmanetAra.TabIndex = 3;
            this.btnTestEmanetAra.Text = "Emanet Ara";
            this.btnTestEmanetAra.UseVisualStyleBackColor = false;
            this.btnTestEmanetAra.Click += new System.EventHandler(this.btnTestEmanetAra_Click);
            // 
            // btnTestEmanetIade
            // 
            this.btnTestEmanetIade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.btnTestEmanetIade.FlatAppearance.BorderSize = 0;
            this.btnTestEmanetIade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestEmanetIade.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTestEmanetIade.ForeColor = System.Drawing.Color.White;
            this.btnTestEmanetIade.Location = new System.Drawing.Point(10, 100);
            this.btnTestEmanetIade.Name = "btnTestEmanetIade";
            this.btnTestEmanetIade.Size = new System.Drawing.Size(180, 35);
            this.btnTestEmanetIade.TabIndex = 2;
            this.btnTestEmanetIade.Text = "Emanet İade";
            this.btnTestEmanetIade.UseVisualStyleBackColor = false;
            this.btnTestEmanetIade.Click += new System.EventHandler(this.btnTestEmanetIade_Click);
            // 
            // btnTestEmanetEkle
            // 
            this.btnTestEmanetEkle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnTestEmanetEkle.FlatAppearance.BorderSize = 0;
            this.btnTestEmanetEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestEmanetEkle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTestEmanetEkle.ForeColor = System.Drawing.Color.White;
            this.btnTestEmanetEkle.Location = new System.Drawing.Point(10, 50);
            this.btnTestEmanetEkle.Name = "btnTestEmanetEkle";
            this.btnTestEmanetEkle.Size = new System.Drawing.Size(180, 35);
            this.btnTestEmanetEkle.TabIndex = 1;
            this.btnTestEmanetEkle.Text = "Emanet Ekle";
            this.btnTestEmanetEkle.UseVisualStyleBackColor = false;
            this.btnTestEmanetEkle.Click += new System.EventHandler(this.btnTestEmanetEkle_Click);
            // 
            // btnTestEmanetler
            // 
            this.btnTestEmanetler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.btnTestEmanetler.FlatAppearance.BorderSize = 0;
            this.btnTestEmanetler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestEmanetler.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTestEmanetler.ForeColor = System.Drawing.Color.White;
            this.btnTestEmanetler.Location = new System.Drawing.Point(10, 10);
            this.btnTestEmanetler.Name = "btnTestEmanetler";
            this.btnTestEmanetler.Size = new System.Drawing.Size(180, 35);
            this.btnTestEmanetler.TabIndex = 0;
            this.btnTestEmanetler.Text = "Tüm Endpoint'ler";
            this.btnTestEmanetler.UseVisualStyleBackColor = false;
            this.btnTestEmanetler.Click += new System.EventHandler(this.btnTestEmanetler_Click);
            // 
            // panelInputs
            // 
            this.panelInputs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelInputs.Controls.Add(this.txtAramaTerm);
            this.panelInputs.Controls.Add(this.lblAramaTerm);
            this.panelInputs.Controls.Add(this.txtEmanetId);
            this.panelInputs.Controls.Add(this.lblEmanetId);
            this.panelInputs.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInputs.Location = new System.Drawing.Point(200, 60);
            this.panelInputs.Name = "panelInputs";
            this.panelInputs.Size = new System.Drawing.Size(600, 80);
            this.panelInputs.TabIndex = 2;
            // 
            // txtAramaTerm
            // 
            this.txtAramaTerm.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtAramaTerm.Location = new System.Drawing.Point(320, 40);
            this.txtAramaTerm.Name = "txtAramaTerm";
            this.txtAramaTerm.PlaceholderText = "Arama terimi...";
            this.txtAramaTerm.Size = new System.Drawing.Size(200, 25);
            this.txtAramaTerm.TabIndex = 3;
            this.txtAramaTerm.Text = "test";
            // 
            // lblAramaTerm
            // 
            this.lblAramaTerm.AutoSize = true;
            this.lblAramaTerm.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAramaTerm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblAramaTerm.Location = new System.Drawing.Point(320, 20);
            this.lblAramaTerm.Name = "lblAramaTerm";
            this.lblAramaTerm.Size = new System.Drawing.Size(90, 19);
            this.lblAramaTerm.TabIndex = 2;
            this.lblAramaTerm.Text = "Arama Terimi:";
            // 
            // txtEmanetId
            // 
            this.txtEmanetId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtEmanetId.Location = new System.Drawing.Point(20, 40);
            this.txtEmanetId.Name = "txtEmanetId";
            this.txtEmanetId.PlaceholderText = "Emanet ID...";
            this.txtEmanetId.Size = new System.Drawing.Size(200, 25);
            this.txtEmanetId.TabIndex = 1;
            this.txtEmanetId.Text = "1";
            // 
            // lblEmanetId
            // 
            this.lblEmanetId.AutoSize = true;
            this.lblEmanetId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEmanetId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblEmanetId.Location = new System.Drawing.Point(20, 20);
            this.lblEmanetId.Name = "lblEmanetId";
            this.lblEmanetId.Size = new System.Drawing.Size(80, 19);
            this.lblEmanetId.TabIndex = 0;
            this.lblEmanetId.Text = "Emanet ID:";
            // 
            // txtResults
            // 
            this.txtResults.BackColor = System.Drawing.Color.Black;
            this.txtResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResults.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtResults.ForeColor = System.Drawing.Color.Lime;
            this.txtResults.Location = new System.Drawing.Point(200, 140);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResults.Size = new System.Drawing.Size(600, 460);
            this.txtResults.TabIndex = 3;
            // 
            // EmanetTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.panelInputs);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelHeader);
            this.Name = "EmanetTestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Emanet API Test";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelInputs.ResumeLayout(false);
            this.panelInputs.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panelHeader;
        private Label lblTitle;
        private Panel panelButtons;
        private Button btnTestTumEmanetler;
        private Button btnTestGecikmisEmanetler;
        private Button btnTestEmanetAra;
        private Button btnTestEmanetIade;
        private Button btnTestEmanetEkle;
        private Button btnTestEmanetler;
        private Panel panelInputs;
        private TextBox txtAramaTerm;
        private Label lblAramaTerm;
        private TextBox txtEmanetId;
        private Label lblEmanetId;
        private TextBox txtResults;
    }
} 