namespace Saobracaj.Servis
{
    partial class frmPrijavaMasinovodje
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrijavaMasinovodje));
            this.txtSifra = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.chkPrijava = new MetroFramework.Controls.MetroCheckBox();
            this.cboZaposleni = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.cboStanica = new MetroFramework.Controls.MetroComboBox();
            this.txtMotoSati = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.txtKM = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.txtGorivo = new MetroFramework.Controls.MetroTextBox();
            this.metroGrid1 = new MetroFramework.Controls.MetroGrid();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.txtNapomena = new MetroFramework.Controls.MetroTextBox();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.metroButton3 = new MetroFramework.Controls.MetroButton();
            this.txtLokomotiva = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.txtAktivnostID = new MetroFramework.Controls.MetroTextBox();
            this.metroButton4 = new MetroFramework.Controls.MetroButton();
            this.metroButton5 = new MetroFramework.Controls.MetroButton();
            this.dataGridView1 = new MetroFramework.Controls.MetroGrid();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.txtDana = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel11 = new MetroFramework.Controls.MetroLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.BrZapisa = new System.Windows.Forms.NumericUpDown();
            this.metroButton6 = new MetroFramework.Controls.MetroButton();
            this.dtpRacunajOd = new System.Windows.Forms.DateTimePicker();
            this.metroButton7 = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrZapisa)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSifra
            // 
            this.txtSifra.Lines = new string[0];
            this.txtSifra.Location = new System.Drawing.Point(107, 60);
            this.txtSifra.MaxLength = 32767;
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.PasswordChar = '\0';
            this.txtSifra.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSifra.SelectedText = "";
            this.txtSifra.Size = new System.Drawing.Size(75, 23);
            this.txtSifra.TabIndex = 0;
            this.txtSifra.UseSelectable = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(24, 57);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(21, 19);
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "ID";
            this.metroLabel1.UseCustomBackColor = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(24, 97);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(77, 19);
            this.metroLabel2.TabIndex = 3;
            this.metroLabel2.Text = "Lokomotiva";
            this.metroLabel2.UseCustomBackColor = true;
            // 
            // chkPrijava
            // 
            this.chkPrijava.AutoSize = true;
            this.chkPrijava.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPrijava.Location = new System.Drawing.Point(106, 128);
            this.chkPrijava.Name = "chkPrijava";
            this.chkPrijava.Size = new System.Drawing.Size(106, 15);
            this.chkPrijava.Style = MetroFramework.MetroColorStyle.Lime;
            this.chkPrijava.TabIndex = 4;
            this.chkPrijava.Text = "Prijava - Odjava";
            this.chkPrijava.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPrijava.UseCustomBackColor = true;
            this.chkPrijava.UseSelectable = true;
            // 
            // cboZaposleni
            // 
            this.cboZaposleni.FormattingEnabled = true;
            this.cboZaposleni.ItemHeight = 23;
            this.cboZaposleni.Location = new System.Drawing.Point(107, 150);
            this.cboZaposleni.Name = "cboZaposleni";
            this.cboZaposleni.Size = new System.Drawing.Size(200, 29);
            this.cboZaposleni.TabIndex = 5;
            this.cboZaposleni.UseSelectable = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(24, 148);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(65, 19);
            this.metroLabel3.TabIndex = 6;
            this.metroLabel3.Text = "Zaposleni";
            this.metroLabel3.UseCustomBackColor = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(342, 57);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(96, 19);
            this.metroLabel4.TabIndex = 8;
            this.metroLabel4.Text = "Datum i vreme";
            this.metroLabel4.UseCustomBackColor = true;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(342, 97);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(50, 19);
            this.metroLabel5.TabIndex = 10;
            this.metroLabel5.Text = "Stanica";
            this.metroLabel5.UseCustomBackColor = true;
            // 
            // cboStanica
            // 
            this.cboStanica.FormattingEnabled = true;
            this.cboStanica.ItemHeight = 23;
            this.cboStanica.Location = new System.Drawing.Point(457, 97);
            this.cboStanica.Name = "cboStanica";
            this.cboStanica.Size = new System.Drawing.Size(200, 29);
            this.cboStanica.TabIndex = 9;
            this.cboStanica.UseSelectable = true;
            // 
            // txtMotoSati
            // 
            this.txtMotoSati.Lines = new string[] {
        "0"};
            this.txtMotoSati.Location = new System.Drawing.Point(759, 93);
            this.txtMotoSati.MaxLength = 32767;
            this.txtMotoSati.Name = "txtMotoSati";
            this.txtMotoSati.PasswordChar = '\0';
            this.txtMotoSati.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtMotoSati.SelectedText = "";
            this.txtMotoSati.Size = new System.Drawing.Size(82, 23);
            this.txtMotoSati.TabIndex = 11;
            this.txtMotoSati.Text = "0";
            this.txtMotoSati.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMotoSati.UseSelectable = true;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(674, 93);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(66, 19);
            this.metroLabel6.TabIndex = 12;
            this.metroLabel6.Text = "Moto Sati";
            this.metroLabel6.UseCustomBackColor = true;
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(674, 57);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(28, 19);
            this.metroLabel7.TabIndex = 14;
            this.metroLabel7.Text = "KM";
            this.metroLabel7.UseCustomBackColor = true;
            // 
            // txtKM
            // 
            this.txtKM.Lines = new string[] {
        "0"};
            this.txtKM.Location = new System.Drawing.Point(759, 60);
            this.txtKM.MaxLength = 32767;
            this.txtKM.Name = "txtKM";
            this.txtKM.PasswordChar = '\0';
            this.txtKM.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtKM.SelectedText = "";
            this.txtKM.Size = new System.Drawing.Size(82, 23);
            this.txtKM.TabIndex = 13;
            this.txtKM.Text = "0";
            this.txtKM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKM.UseSelectable = true;
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.Location = new System.Drawing.Point(674, 128);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(48, 19);
            this.metroLabel8.TabIndex = 16;
            this.metroLabel8.Text = "Gorivo";
            this.metroLabel8.UseCustomBackColor = true;
            // 
            // txtGorivo
            // 
            this.txtGorivo.Lines = new string[] {
        "0"};
            this.txtGorivo.Location = new System.Drawing.Point(759, 128);
            this.txtGorivo.MaxLength = 32767;
            this.txtGorivo.Name = "txtGorivo";
            this.txtGorivo.PasswordChar = '\0';
            this.txtGorivo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtGorivo.SelectedText = "";
            this.txtGorivo.Size = new System.Drawing.Size(82, 23);
            this.txtGorivo.TabIndex = 15;
            this.txtGorivo.Text = "0";
            this.txtGorivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGorivo.UseSelectable = true;
            // 
            // metroGrid1
            // 
            this.metroGrid1.AllowUserToResizeRows = false;
            this.metroGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroGrid1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.metroGrid1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.metroGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.metroGrid1.DefaultCellStyle = dataGridViewCellStyle2;
            this.metroGrid1.EnableHeadersVisualStyles = false;
            this.metroGrid1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.metroGrid1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid1.Location = new System.Drawing.Point(-3, 245);
            this.metroGrid1.Name = "metroGrid1";
            this.metroGrid1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.metroGrid1.RowHeadersWidth = 51;
            this.metroGrid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.metroGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGrid1.Size = new System.Drawing.Size(240, 150);
            this.metroGrid1.TabIndex = 17;
            // 
            // metroButton1
            // 
            this.metroButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.metroButton1.ForeColor = System.Drawing.Color.White;
            this.metroButton1.Location = new System.Drawing.Point(863, 12);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(197, 26);
            this.metroButton1.TabIndex = 19;
            this.metroButton1.Text = "Ažuriraj podatke";
            this.metroButton1.UseCustomBackColor = true;
            this.metroButton1.UseCustomForeColor = true;
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.Location = new System.Drawing.Point(342, 132);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(75, 19);
            this.metroLabel9.TabIndex = 21;
            this.metroLabel9.Text = "Napomena";
            this.metroLabel9.UseCustomBackColor = true;
            // 
            // txtNapomena
            // 
            this.txtNapomena.Lines = new string[0];
            this.txtNapomena.Location = new System.Drawing.Point(457, 132);
            this.txtNapomena.MaxLength = 32767;
            this.txtNapomena.Multiline = true;
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.PasswordChar = '\0';
            this.txtNapomena.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNapomena.SelectedText = "";
            this.txtNapomena.Size = new System.Drawing.Size(200, 47);
            this.txtNapomena.TabIndex = 20;
            this.txtNapomena.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNapomena.UseSelectable = true;
            // 
            // metroButton2
            // 
            this.metroButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.metroButton2.ForeColor = System.Drawing.Color.White;
            this.metroButton2.Location = new System.Drawing.Point(1066, 46);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(197, 26);
            this.metroButton2.TabIndex = 22;
            this.metroButton2.Text = "Briši zapis";
            this.metroButton2.UseCustomBackColor = true;
            this.metroButton2.UseCustomForeColor = true;
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // metroButton3
            // 
            this.metroButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.metroButton3.ForeColor = System.Drawing.Color.White;
            this.metroButton3.Location = new System.Drawing.Point(1066, 113);
            this.metroButton3.Name = "metroButton3";
            this.metroButton3.Size = new System.Drawing.Size(197, 26);
            this.metroButton3.TabIndex = 23;
            this.metroButton3.Text = "Promeni zapis";
            this.metroButton3.UseCustomBackColor = true;
            this.metroButton3.UseCustomForeColor = true;
            this.metroButton3.UseSelectable = true;
            this.metroButton3.Click += new System.EventHandler(this.metroButton3_Click);
            // 
            // txtLokomotiva
            // 
            this.txtLokomotiva.FormattingEnabled = true;
            this.txtLokomotiva.ItemHeight = 23;
            this.txtLokomotiva.Location = new System.Drawing.Point(107, 91);
            this.txtLokomotiva.Name = "txtLokomotiva";
            this.txtLokomotiva.Size = new System.Drawing.Size(200, 29);
            this.txtLokomotiva.TabIndex = 24;
            this.txtLokomotiva.UseSelectable = true;
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.Location = new System.Drawing.Point(674, 161);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(77, 19);
            this.metroLabel10.TabIndex = 26;
            this.metroLabel10.Text = "Aktivnost ID";
            this.metroLabel10.UseCustomBackColor = true;
            // 
            // txtAktivnostID
            // 
            this.txtAktivnostID.Lines = new string[] {
        "0"};
            this.txtAktivnostID.Location = new System.Drawing.Point(759, 161);
            this.txtAktivnostID.MaxLength = 32767;
            this.txtAktivnostID.Name = "txtAktivnostID";
            this.txtAktivnostID.PasswordChar = '\0';
            this.txtAktivnostID.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtAktivnostID.SelectedText = "";
            this.txtAktivnostID.Size = new System.Drawing.Size(82, 23);
            this.txtAktivnostID.TabIndex = 25;
            this.txtAktivnostID.Text = "0";
            this.txtAktivnostID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAktivnostID.UseSelectable = true;
            // 
            // metroButton4
            // 
            this.metroButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.metroButton4.ForeColor = System.Drawing.Color.White;
            this.metroButton4.Location = new System.Drawing.Point(1066, 12);
            this.metroButton4.Name = "metroButton4";
            this.metroButton4.Size = new System.Drawing.Size(197, 26);
            this.metroButton4.TabIndex = 27;
            this.metroButton4.Text = "Dodaj zapis";
            this.metroButton4.UseCustomBackColor = true;
            this.metroButton4.UseCustomForeColor = true;
            this.metroButton4.UseSelectable = true;
            this.metroButton4.Click += new System.EventHandler(this.metroButton4_Click);
            // 
            // metroButton5
            // 
            this.metroButton5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.metroButton5.ForeColor = System.Drawing.Color.White;
            this.metroButton5.Location = new System.Drawing.Point(1066, 80);
            this.metroButton5.Name = "metroButton5";
            this.metroButton5.Size = new System.Drawing.Size(197, 26);
            this.metroButton5.TabIndex = 28;
            this.metroButton5.Text = "Odjavi prijavu";
            this.metroButton5.UseCustomBackColor = true;
            this.metroButton5.UseCustomForeColor = true;
            this.metroButton5.UseSelectable = true;
            this.metroButton5.Click += new System.EventHandler(this.metroButton5_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridView1.Location = new System.Drawing.Point(23, 199);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1324, 527);
            this.dataGridView1.TabIndex = 29;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged_1);
            // 
            // dtpDatum
            // 
            this.dtpDatum.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpDatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatum.Location = new System.Drawing.Point(457, 60);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.ShowUpDown = true;
            this.dtpDatum.Size = new System.Drawing.Size(200, 20);
            this.dtpDatum.TabIndex = 30;
            this.dtpDatum.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // txtDana
            // 
            this.txtDana.Lines = new string[] {
        "-2"};
            this.txtDana.Location = new System.Drawing.Point(1284, 53);
            this.txtDana.MaxLength = 32767;
            this.txtDana.Name = "txtDana";
            this.txtDana.PasswordChar = '\0';
            this.txtDana.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDana.SelectedText = "";
            this.txtDana.Size = new System.Drawing.Size(82, 23);
            this.txtDana.TabIndex = 31;
            this.txtDana.Text = "-2";
            this.txtDana.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDana.UseSelectable = true;
            // 
            // metroLabel11
            // 
            this.metroLabel11.AutoSize = true;
            this.metroLabel11.Location = new System.Drawing.Point(1284, 31);
            this.metroLabel11.Name = "metroLabel11";
            this.metroLabel11.Size = new System.Drawing.Size(39, 19);
            this.metroLabel11.TabIndex = 32;
            this.metroLabel11.Text = "Dana";
            this.metroLabel11.UseCustomBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(860, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 282;
            this.label1.Text = "Broj Zapisa:";
            // 
            // BrZapisa
            // 
            this.BrZapisa.Location = new System.Drawing.Point(863, 63);
            this.BrZapisa.Name = "BrZapisa";
            this.BrZapisa.Size = new System.Drawing.Size(120, 20);
            this.BrZapisa.TabIndex = 281;
            // 
            // metroButton6
            // 
            this.metroButton6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.metroButton6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.metroButton6.ForeColor = System.Drawing.Color.White;
            this.metroButton6.Location = new System.Drawing.Point(863, 89);
            this.metroButton6.Name = "metroButton6";
            this.metroButton6.Size = new System.Drawing.Size(197, 23);
            this.metroButton6.TabIndex = 280;
            this.metroButton6.Text = "Prikazi zadnjih n Zapisa";
            this.metroButton6.UseCustomBackColor = true;
            this.metroButton6.UseCustomForeColor = true;
            this.metroButton6.UseSelectable = true;
            this.metroButton6.Click += new System.EventHandler(this.metroButton6_Click);
            // 
            // dtpRacunajOd
            // 
            this.dtpRacunajOd.CustomFormat = "yyyy-MM-dd ";
            this.dtpRacunajOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRacunajOd.Location = new System.Drawing.Point(860, 118);
            this.dtpRacunajOd.Name = "dtpRacunajOd";
            this.dtpRacunajOd.ShowUpDown = true;
            this.dtpRacunajOd.Size = new System.Drawing.Size(200, 20);
            this.dtpRacunajOd.TabIndex = 283;
            this.dtpRacunajOd.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // metroButton7
            // 
            this.metroButton7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.metroButton7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.metroButton7.ForeColor = System.Drawing.Color.White;
            this.metroButton7.Location = new System.Drawing.Point(860, 144);
            this.metroButton7.Name = "metroButton7";
            this.metroButton7.Size = new System.Drawing.Size(197, 23);
            this.metroButton7.TabIndex = 284;
            this.metroButton7.Text = "Prikazi zapise od datuma";
            this.metroButton7.UseCustomBackColor = true;
            this.metroButton7.UseCustomForeColor = true;
            this.metroButton7.UseSelectable = true;
            this.metroButton7.Click += new System.EventHandler(this.metroButton7_Click);
            // 
            // frmPrijavaMasinovodje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(226)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.metroButton7);
            this.Controls.Add(this.dtpRacunajOd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BrZapisa);
            this.Controls.Add(this.metroButton6);
            this.Controls.Add(this.metroLabel11);
            this.Controls.Add(this.txtDana);
            this.Controls.Add(this.dtpDatum);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.metroButton5);
            this.Controls.Add(this.metroButton4);
            this.Controls.Add(this.metroLabel10);
            this.Controls.Add(this.txtAktivnostID);
            this.Controls.Add(this.txtLokomotiva);
            this.Controls.Add(this.metroButton3);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.metroLabel9);
            this.Controls.Add(this.txtNapomena);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.metroGrid1);
            this.Controls.Add(this.metroLabel8);
            this.Controls.Add(this.txtGorivo);
            this.Controls.Add(this.metroLabel7);
            this.Controls.Add(this.txtKM);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.txtMotoSati);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.cboStanica);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.cboZaposleni);
            this.Controls.Add(this.chkPrijava);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.txtSifra);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrijavaMasinovodje";
            this.Text = "Prijave - Odjave Mašinovođe";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPrijavaMasinovodje_Load);
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BrZapisa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox txtSifra;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroCheckBox chkPrijava;
        private MetroFramework.Controls.MetroComboBox cboZaposleni;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroComboBox cboStanica;
        private MetroFramework.Controls.MetroTextBox txtMotoSati;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroTextBox txtKM;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroTextBox txtGorivo;
        private MetroFramework.Controls.MetroGrid metroGrid1;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroTextBox txtNapomena;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroButton metroButton3;
        private MetroFramework.Controls.MetroComboBox txtLokomotiva;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroTextBox txtAktivnostID;
        private MetroFramework.Controls.MetroButton metroButton4;
        private MetroFramework.Controls.MetroButton metroButton5;
        private MetroFramework.Controls.MetroGrid dataGridView1;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private MetroFramework.Controls.MetroTextBox txtDana;
        private MetroFramework.Controls.MetroLabel metroLabel11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown BrZapisa;
        private MetroFramework.Controls.MetroButton metroButton6;
        private System.Windows.Forms.DateTimePicker dtpRacunajOd;
        private MetroFramework.Controls.MetroButton metroButton7;
    }
}