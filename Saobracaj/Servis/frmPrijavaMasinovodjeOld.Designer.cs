
namespace Saobracaj.Servis
{
    partial class frmPrijavaMasinovodjeOld
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrijavaMasinovodjeOld));
            this.metroLabel11 = new MetroFramework.Controls.MetroLabel();
            this.txtDana = new MetroFramework.Controls.MetroTextBox();
            this.dtpDatum = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new MetroFramework.Controls.MetroGrid();
            this.metroButton5 = new MetroFramework.Controls.MetroButton();
            this.metroButton4 = new MetroFramework.Controls.MetroButton();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.txtAktivnostID = new MetroFramework.Controls.MetroTextBox();
            this.txtLokomotiva = new MetroFramework.Controls.MetroComboBox();
            this.metroButton3 = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.txtNapomena = new MetroFramework.Controls.MetroTextBox();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroGrid1 = new MetroFramework.Controls.MetroGrid();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.txtGorivo = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.txtKM = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.txtMotoSati = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.cboStanica = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.cboZaposleni = new MetroFramework.Controls.MetroComboBox();
            this.chkPrijava = new MetroFramework.Controls.MetroCheckBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtSifra = new MetroFramework.Controls.MetroTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // metroLabel11
            // 
            this.metroLabel11.AutoSize = true;
            this.metroLabel11.Location = new System.Drawing.Point(1100, 109);
            this.metroLabel11.Name = "metroLabel11";
            this.metroLabel11.Size = new System.Drawing.Size(39, 19);
            this.metroLabel11.TabIndex = 62;
            this.metroLabel11.Text = "Dana";
            this.metroLabel11.UseCustomBackColor = true;
            // 
            // txtDana
            // 
            this.txtDana.Lines = new string[] {
        "-2"};
            this.txtDana.Location = new System.Drawing.Point(1183, 109);
            this.txtDana.MaxLength = 32767;
            this.txtDana.Name = "txtDana";
            this.txtDana.PasswordChar = '\0';
            this.txtDana.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDana.SelectedText = "";
            this.txtDana.Size = new System.Drawing.Size(82, 23);
            this.txtDana.TabIndex = 61;
            this.txtDana.Text = "-2";
            this.txtDana.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDana.UseSelectable = true;
            // 
            // dtpDatum
            // 
            this.dtpDatum.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpDatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatum.Location = new System.Drawing.Point(444, 25);
            this.dtpDatum.Name = "dtpDatum";
            this.dtpDatum.ShowUpDown = true;
            this.dtpDatum.Size = new System.Drawing.Size(200, 20);
            this.dtpDatum.TabIndex = 60;
            this.dtpDatum.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
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
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridView1.Location = new System.Drawing.Point(12, 155);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1303, 576);
            this.dataGridView1.TabIndex = 59;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged_1);
            // 
            // metroButton5
            // 
            this.metroButton5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.metroButton5.ForeColor = System.Drawing.Color.White;
            this.metroButton5.Location = new System.Drawing.Point(850, 54);
            this.metroButton5.Name = "metroButton5";
            this.metroButton5.Size = new System.Drawing.Size(197, 26);
            this.metroButton5.TabIndex = 58;
            this.metroButton5.Text = "Odjavi prijavu";
            this.metroButton5.UseCustomBackColor = true;
            this.metroButton5.UseCustomForeColor = true;
            this.metroButton5.UseSelectable = true;
            this.metroButton5.Click += new System.EventHandler(this.metroButton5_Click);
            // 
            // metroButton4
            // 
            this.metroButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.metroButton4.ForeColor = System.Drawing.Color.White;
            this.metroButton4.Location = new System.Drawing.Point(1068, 25);
            this.metroButton4.Name = "metroButton4";
            this.metroButton4.Size = new System.Drawing.Size(197, 26);
            this.metroButton4.TabIndex = 57;
            this.metroButton4.Text = "Dodaj zapis";
            this.metroButton4.UseCustomBackColor = true;
            this.metroButton4.UseCustomForeColor = true;
            this.metroButton4.UseSelectable = true;
            this.metroButton4.Click += new System.EventHandler(this.metroButton4_Click);
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.Location = new System.Drawing.Point(661, 126);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(77, 19);
            this.metroLabel10.TabIndex = 56;
            this.metroLabel10.Text = "Aktivnost ID";
            this.metroLabel10.UseCustomBackColor = true;
            // 
            // txtAktivnostID
            // 
            this.txtAktivnostID.Lines = new string[] {
        "0"};
            this.txtAktivnostID.Location = new System.Drawing.Point(746, 126);
            this.txtAktivnostID.MaxLength = 32767;
            this.txtAktivnostID.Name = "txtAktivnostID";
            this.txtAktivnostID.PasswordChar = '\0';
            this.txtAktivnostID.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtAktivnostID.SelectedText = "";
            this.txtAktivnostID.Size = new System.Drawing.Size(82, 23);
            this.txtAktivnostID.TabIndex = 55;
            this.txtAktivnostID.Text = "0";
            this.txtAktivnostID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAktivnostID.UseSelectable = true;
            // 
            // txtLokomotiva
            // 
            this.txtLokomotiva.FormattingEnabled = true;
            this.txtLokomotiva.ItemHeight = 23;
            this.txtLokomotiva.Location = new System.Drawing.Point(94, 56);
            this.txtLokomotiva.Name = "txtLokomotiva";
            this.txtLokomotiva.Size = new System.Drawing.Size(200, 29);
            this.txtLokomotiva.TabIndex = 54;
            this.txtLokomotiva.UseSelectable = true;
            // 
            // metroButton3
            // 
            this.metroButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.metroButton3.ForeColor = System.Drawing.Color.White;
            this.metroButton3.Location = new System.Drawing.Point(850, 90);
            this.metroButton3.Name = "metroButton3";
            this.metroButton3.Size = new System.Drawing.Size(197, 26);
            this.metroButton3.TabIndex = 53;
            this.metroButton3.Text = "Promeni zapis";
            this.metroButton3.UseCustomBackColor = true;
            this.metroButton3.UseCustomForeColor = true;
            this.metroButton3.UseSelectable = true;
            this.metroButton3.Click += new System.EventHandler(this.metroButton3_Click);
            // 
            // metroButton2
            // 
            this.metroButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.metroButton2.ForeColor = System.Drawing.Color.White;
            this.metroButton2.Location = new System.Drawing.Point(1068, 59);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(197, 26);
            this.metroButton2.TabIndex = 52;
            this.metroButton2.Text = "Briši zapis";
            this.metroButton2.UseCustomBackColor = true;
            this.metroButton2.UseCustomForeColor = true;
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.Location = new System.Drawing.Point(329, 97);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(75, 19);
            this.metroLabel9.TabIndex = 51;
            this.metroLabel9.Text = "Napomena";
            this.metroLabel9.UseCustomBackColor = true;
            // 
            // txtNapomena
            // 
            this.txtNapomena.Lines = new string[0];
            this.txtNapomena.Location = new System.Drawing.Point(444, 97);
            this.txtNapomena.MaxLength = 32767;
            this.txtNapomena.Multiline = true;
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.PasswordChar = '\0';
            this.txtNapomena.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNapomena.SelectedText = "";
            this.txtNapomena.Size = new System.Drawing.Size(200, 47);
            this.txtNapomena.TabIndex = 50;
            this.txtNapomena.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNapomena.UseSelectable = true;
            // 
            // metroButton1
            // 
            this.metroButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.metroButton1.ForeColor = System.Drawing.Color.White;
            this.metroButton1.Location = new System.Drawing.Point(850, 22);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(197, 26);
            this.metroButton1.TabIndex = 49;
            this.metroButton1.Text = "Ažuriraj podatke";
            this.metroButton1.UseCustomBackColor = true;
            this.metroButton1.UseCustomForeColor = true;
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // metroGrid1
            // 
            this.metroGrid1.AllowUserToResizeRows = false;
            this.metroGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.metroGrid1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.metroGrid1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.metroGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.metroGrid1.DefaultCellStyle = dataGridViewCellStyle11;
            this.metroGrid1.EnableHeadersVisualStyles = false;
            this.metroGrid1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.metroGrid1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.metroGrid1.Location = new System.Drawing.Point(-16, 210);
            this.metroGrid1.Name = "metroGrid1";
            this.metroGrid1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.metroGrid1.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.metroGrid1.RowHeadersWidth = 51;
            this.metroGrid1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.metroGrid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.metroGrid1.Size = new System.Drawing.Size(240, 150);
            this.metroGrid1.TabIndex = 48;
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.Location = new System.Drawing.Point(661, 93);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(48, 19);
            this.metroLabel8.TabIndex = 47;
            this.metroLabel8.Text = "Gorivo";
            this.metroLabel8.UseCustomBackColor = true;
            // 
            // txtGorivo
            // 
            this.txtGorivo.Lines = new string[] {
        "0"};
            this.txtGorivo.Location = new System.Drawing.Point(746, 93);
            this.txtGorivo.MaxLength = 32767;
            this.txtGorivo.Name = "txtGorivo";
            this.txtGorivo.PasswordChar = '\0';
            this.txtGorivo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtGorivo.SelectedText = "";
            this.txtGorivo.Size = new System.Drawing.Size(82, 23);
            this.txtGorivo.TabIndex = 46;
            this.txtGorivo.Text = "0";
            this.txtGorivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtGorivo.UseSelectable = true;
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(661, 22);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(28, 19);
            this.metroLabel7.TabIndex = 45;
            this.metroLabel7.Text = "KM";
            this.metroLabel7.UseCustomBackColor = true;
            // 
            // txtKM
            // 
            this.txtKM.Lines = new string[] {
        "0"};
            this.txtKM.Location = new System.Drawing.Point(746, 25);
            this.txtKM.MaxLength = 32767;
            this.txtKM.Name = "txtKM";
            this.txtKM.PasswordChar = '\0';
            this.txtKM.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtKM.SelectedText = "";
            this.txtKM.Size = new System.Drawing.Size(82, 23);
            this.txtKM.TabIndex = 44;
            this.txtKM.Text = "0";
            this.txtKM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKM.UseSelectable = true;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(661, 58);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(66, 19);
            this.metroLabel6.TabIndex = 43;
            this.metroLabel6.Text = "Moto Sati";
            this.metroLabel6.UseCustomBackColor = true;
            // 
            // txtMotoSati
            // 
            this.txtMotoSati.Lines = new string[] {
        "0"};
            this.txtMotoSati.Location = new System.Drawing.Point(746, 58);
            this.txtMotoSati.MaxLength = 32767;
            this.txtMotoSati.Name = "txtMotoSati";
            this.txtMotoSati.PasswordChar = '\0';
            this.txtMotoSati.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtMotoSati.SelectedText = "";
            this.txtMotoSati.Size = new System.Drawing.Size(82, 23);
            this.txtMotoSati.TabIndex = 42;
            this.txtMotoSati.Text = "0";
            this.txtMotoSati.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMotoSati.UseSelectable = true;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(329, 62);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(50, 19);
            this.metroLabel5.TabIndex = 41;
            this.metroLabel5.Text = "Stanica";
            this.metroLabel5.UseCustomBackColor = true;
            // 
            // cboStanica
            // 
            this.cboStanica.FormattingEnabled = true;
            this.cboStanica.ItemHeight = 23;
            this.cboStanica.Location = new System.Drawing.Point(444, 62);
            this.cboStanica.Name = "cboStanica";
            this.cboStanica.Size = new System.Drawing.Size(200, 29);
            this.cboStanica.TabIndex = 40;
            this.cboStanica.UseSelectable = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(329, 22);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(96, 19);
            this.metroLabel4.TabIndex = 39;
            this.metroLabel4.Text = "Datum i vreme";
            this.metroLabel4.UseCustomBackColor = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(11, 113);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(65, 19);
            this.metroLabel3.TabIndex = 38;
            this.metroLabel3.Text = "Zaposleni";
            this.metroLabel3.UseCustomBackColor = true;
            // 
            // cboZaposleni
            // 
            this.cboZaposleni.FormattingEnabled = true;
            this.cboZaposleni.ItemHeight = 23;
            this.cboZaposleni.Location = new System.Drawing.Point(94, 115);
            this.cboZaposleni.Name = "cboZaposleni";
            this.cboZaposleni.Size = new System.Drawing.Size(200, 29);
            this.cboZaposleni.TabIndex = 37;
            this.cboZaposleni.UseSelectable = true;
            // 
            // chkPrijava
            // 
            this.chkPrijava.AutoSize = true;
            this.chkPrijava.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPrijava.Location = new System.Drawing.Point(93, 93);
            this.chkPrijava.Name = "chkPrijava";
            this.chkPrijava.Size = new System.Drawing.Size(106, 15);
            this.chkPrijava.Style = MetroFramework.MetroColorStyle.Lime;
            this.chkPrijava.TabIndex = 36;
            this.chkPrijava.Text = "Prijava - Odjava";
            this.chkPrijava.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPrijava.UseCustomBackColor = true;
            this.chkPrijava.UseSelectable = true;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(11, 62);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(77, 19);
            this.metroLabel2.TabIndex = 35;
            this.metroLabel2.Text = "Lokomotiva";
            this.metroLabel2.UseCustomBackColor = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(11, 22);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(21, 19);
            this.metroLabel1.TabIndex = 34;
            this.metroLabel1.Text = "ID";
            this.metroLabel1.UseCustomBackColor = true;
            // 
            // txtSifra
            // 
            this.txtSifra.Lines = new string[0];
            this.txtSifra.Location = new System.Drawing.Point(94, 25);
            this.txtSifra.MaxLength = 32767;
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.PasswordChar = '\0';
            this.txtSifra.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSifra.SelectedText = "";
            this.txtSifra.Size = new System.Drawing.Size(75, 23);
            this.txtSifra.TabIndex = 33;
            this.txtSifra.UseSelectable = true;
            // 
            // frmPrijavaMasinovodjeOld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(226)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(1327, 743);
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
            this.Name = "frmPrijavaMasinovodjeOld";
            this.Text = "Prijave masinovodja prikaz bez posla";
            this.Load += new System.EventHandler(this.frmPrijavaMasinovodje_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.metroGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel11;
        private MetroFramework.Controls.MetroTextBox txtDana;
        private System.Windows.Forms.DateTimePicker dtpDatum;
        private MetroFramework.Controls.MetroGrid dataGridView1;
        private MetroFramework.Controls.MetroButton metroButton5;
        private MetroFramework.Controls.MetroButton metroButton4;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroTextBox txtAktivnostID;
        private MetroFramework.Controls.MetroComboBox txtLokomotiva;
        private MetroFramework.Controls.MetroButton metroButton3;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroTextBox txtNapomena;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroGrid metroGrid1;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroTextBox txtGorivo;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroTextBox txtKM;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroTextBox txtMotoSati;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroComboBox cboStanica;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroComboBox cboZaposleni;
        private MetroFramework.Controls.MetroCheckBox chkPrijava;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox txtSifra;
    }
}