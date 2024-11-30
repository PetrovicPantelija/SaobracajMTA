
namespace Saobracaj.Dokumenta
{
    partial class frmAutomobiliPregledPrijava
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAutomobiliPregledPrijava));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.label3 = new System.Windows.Forms.Label();
            this.combo_Zaposleni = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDatumPrijave = new MetroFramework.Controls.MetroDateTime();
            this.label2 = new System.Windows.Forms.Label();
            this.combo_Automobil = new System.Windows.Forms.ComboBox();
            this.cb_DirPredZad = new System.Windows.Forms.CheckBox();
            this.txt_KmZaduzenje = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.combo_CistocaSpoljaZad = new System.Windows.Forms.ComboBox();
            this.combo_CistocaUnutraZad = new System.Windows.Forms.ComboBox();
            this.combo_NivoUljaZad = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dt_Odjava = new MetroFramework.Controls.MetroDateTime();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_KmRazduzenje = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.combo_CistocaSpoljaRaz = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.combo_CistocaUnutraRaz = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.combo_NivoUljaRaz = new System.Windows.Forms.ComboBox();
            this.cb_DirPredRaz = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_Sifra = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_OtvoriSliku = new System.Windows.Forms.Button();
            this.btn_nazad = new System.Windows.Forms.Button();
            this.btn_Napred = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtPosao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNGRazduzenje = new System.Windows.Forms.TextBox();
            this.txtNGZaduzenje = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cboMestoPolaska = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cboMestoDolaska = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtUloga = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtAktivnostID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(19, 263);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1339, 334);
            this.dataGridView1.TabIndex = 205;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.DodgerBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsSave,
            this.tsDelete,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1370, 27);
            this.toolStrip1.TabIndex = 206;
            this.toolStrip1.Text = "Osveži";
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = ((System.Drawing.Image)(resources.GetObject("tsNew.Image")));
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(24, 24);
            this.tsNew.Text = "Novi";
            this.tsNew.Click += new System.EventHandler(this.tsNew_Click);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(24, 24);
            this.tsSave.Text = "tsSave";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(24, 24);
            this.tsDelete.Text = "toolStripButton1";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 207;
            this.label3.Text = "Zaposleni:";
            // 
            // combo_Zaposleni
            // 
            this.combo_Zaposleni.FormattingEnabled = true;
            this.combo_Zaposleni.Location = new System.Drawing.Point(108, 67);
            this.combo_Zaposleni.Name = "combo_Zaposleni";
            this.combo_Zaposleni.Size = new System.Drawing.Size(255, 21);
            this.combo_Zaposleni.TabIndex = 208;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 209;
            this.label5.Text = "Datum prijave:";
            // 
            // dtpDatumPrijave
            // 
            this.dtpDatumPrijave.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpDatumPrijave.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumPrijave.Location = new System.Drawing.Point(108, 97);
            this.dtpDatumPrijave.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtpDatumPrijave.Name = "dtpDatumPrijave";
            this.dtpDatumPrijave.Size = new System.Drawing.Size(200, 30);
            this.dtpDatumPrijave.TabIndex = 210;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 211;
            this.label2.Text = "Automobil:";
            // 
            // combo_Automobil
            // 
            this.combo_Automobil.FormattingEnabled = true;
            this.combo_Automobil.Location = new System.Drawing.Point(108, 171);
            this.combo_Automobil.Name = "combo_Automobil";
            this.combo_Automobil.Size = new System.Drawing.Size(255, 21);
            this.combo_Automobil.TabIndex = 212;
            // 
            // cb_DirPredZad
            // 
            this.cb_DirPredZad.AutoSize = true;
            this.cb_DirPredZad.Location = new System.Drawing.Point(371, 176);
            this.cb_DirPredZad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cb_DirPredZad.Name = "cb_DirPredZad";
            this.cb_DirPredZad.Size = new System.Drawing.Size(182, 17);
            this.cb_DirPredZad.TabIndex = 214;
            this.cb_DirPredZad.Text = "Direktna primopredaja Zaduženje";
            this.cb_DirPredZad.UseVisualStyleBackColor = true;
            // 
            // txt_KmZaduzenje
            // 
            this.txt_KmZaduzenje.Location = new System.Drawing.Point(507, 34);
            this.txt_KmZaduzenje.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_KmZaduzenje.Name = "txt_KmZaduzenje";
            this.txt_KmZaduzenje.Size = new System.Drawing.Size(144, 20);
            this.txt_KmZaduzenje.TabIndex = 213;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(368, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 211;
            this.label4.Text = "KM Zaduženje:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(368, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 13);
            this.label6.TabIndex = 211;
            this.label6.Text = "Čistoća Spolja Zaduživanje";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(368, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 13);
            this.label7.TabIndex = 211;
            this.label7.Text = "Čistoća Unutra Zaduživanje";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(368, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 13);
            this.label8.TabIndex = 211;
            this.label8.Text = "Nivo Ulja Zaduživanje:";
            // 
            // combo_CistocaSpoljaZad
            // 
            this.combo_CistocaSpoljaZad.FormattingEnabled = true;
            this.combo_CistocaSpoljaZad.Location = new System.Drawing.Point(507, 69);
            this.combo_CistocaSpoljaZad.Name = "combo_CistocaSpoljaZad";
            this.combo_CistocaSpoljaZad.Size = new System.Drawing.Size(144, 21);
            this.combo_CistocaSpoljaZad.TabIndex = 212;
            // 
            // combo_CistocaUnutraZad
            // 
            this.combo_CistocaUnutraZad.FormattingEnabled = true;
            this.combo_CistocaUnutraZad.Location = new System.Drawing.Point(507, 106);
            this.combo_CistocaUnutraZad.Name = "combo_CistocaUnutraZad";
            this.combo_CistocaUnutraZad.Size = new System.Drawing.Size(144, 21);
            this.combo_CistocaUnutraZad.TabIndex = 212;
            // 
            // combo_NivoUljaZad
            // 
            this.combo_NivoUljaZad.FormattingEnabled = true;
            this.combo_NivoUljaZad.Location = new System.Drawing.Point(507, 141);
            this.combo_NivoUljaZad.Name = "combo_NivoUljaZad";
            this.combo_NivoUljaZad.Size = new System.Drawing.Size(144, 21);
            this.combo_NivoUljaZad.TabIndex = 212;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 209;
            this.label9.Text = "Datum odjave:";
            // 
            // dt_Odjava
            // 
            this.dt_Odjava.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dt_Odjava.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_Odjava.Location = new System.Drawing.Point(108, 133);
            this.dt_Odjava.MinimumSize = new System.Drawing.Size(0, 29);
            this.dt_Odjava.Name = "dt_Odjava";
            this.dt_Odjava.Size = new System.Drawing.Size(200, 30);
            this.dt_Odjava.TabIndex = 210;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(659, 35);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 13);
            this.label10.TabIndex = 211;
            this.label10.Text = "KM Razduženje:";
            // 
            // txt_KmRazduzenje
            // 
            this.txt_KmRazduzenje.Location = new System.Drawing.Point(806, 35);
            this.txt_KmRazduzenje.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_KmRazduzenje.Name = "txt_KmRazduzenje";
            this.txt_KmRazduzenje.Size = new System.Drawing.Size(144, 20);
            this.txt_KmRazduzenje.TabIndex = 213;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(659, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(141, 13);
            this.label11.TabIndex = 211;
            this.label11.Text = "Čistoća Spolja Razduživanje";
            // 
            // combo_CistocaSpoljaRaz
            // 
            this.combo_CistocaSpoljaRaz.FormattingEnabled = true;
            this.combo_CistocaSpoljaRaz.Location = new System.Drawing.Point(806, 67);
            this.combo_CistocaSpoljaRaz.Name = "combo_CistocaSpoljaRaz";
            this.combo_CistocaSpoljaRaz.Size = new System.Drawing.Size(144, 21);
            this.combo_CistocaSpoljaRaz.TabIndex = 212;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(659, 103);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(144, 13);
            this.label12.TabIndex = 211;
            this.label12.Text = "Čistoća Unutra Razduživanje";
            // 
            // combo_CistocaUnutraRaz
            // 
            this.combo_CistocaUnutraRaz.FormattingEnabled = true;
            this.combo_CistocaUnutraRaz.Location = new System.Drawing.Point(806, 100);
            this.combo_CistocaUnutraRaz.Name = "combo_CistocaUnutraRaz";
            this.combo_CistocaUnutraRaz.Size = new System.Drawing.Size(144, 21);
            this.combo_CistocaUnutraRaz.TabIndex = 212;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(659, 138);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(120, 13);
            this.label13.TabIndex = 211;
            this.label13.Text = "Nivo Ulja Razduživanje:";
            // 
            // combo_NivoUljaRaz
            // 
            this.combo_NivoUljaRaz.FormattingEnabled = true;
            this.combo_NivoUljaRaz.Location = new System.Drawing.Point(806, 135);
            this.combo_NivoUljaRaz.Name = "combo_NivoUljaRaz";
            this.combo_NivoUljaRaz.Size = new System.Drawing.Size(144, 21);
            this.combo_NivoUljaRaz.TabIndex = 212;
            // 
            // cb_DirPredRaz
            // 
            this.cb_DirPredRaz.AutoSize = true;
            this.cb_DirPredRaz.Location = new System.Drawing.Point(661, 174);
            this.cb_DirPredRaz.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cb_DirPredRaz.Name = "cb_DirPredRaz";
            this.cb_DirPredRaz.Size = new System.Drawing.Size(196, 17);
            this.cb_DirPredRaz.TabIndex = 214;
            this.cb_DirPredRaz.Text = "Direktna primopredaja Razduživanje";
            this.cb_DirPredRaz.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 38);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 13);
            this.label15.TabIndex = 207;
            this.label15.Text = "Šifra:";
            // 
            // txt_Sifra
            // 
            this.txt_Sifra.Location = new System.Drawing.Point(108, 38);
            this.txt_Sifra.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_Sifra.Name = "txt_Sifra";
            this.txt_Sifra.Size = new System.Drawing.Size(140, 20);
            this.txt_Sifra.TabIndex = 216;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox1.Location = new System.Drawing.Point(955, 24);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(313, 234);
            this.pictureBox1.TabIndex = 217;
            this.pictureBox1.TabStop = false;
            // 
            // btn_OtvoriSliku
            // 
            this.btn_OtvoriSliku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(168)))), ((int)(((byte)(82)))));
            this.btn_OtvoriSliku.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_OtvoriSliku.Location = new System.Drawing.Point(1277, 30);
            this.btn_OtvoriSliku.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_OtvoriSliku.Name = "btn_OtvoriSliku";
            this.btn_OtvoriSliku.Size = new System.Drawing.Size(82, 28);
            this.btn_OtvoriSliku.TabIndex = 218;
            this.btn_OtvoriSliku.Text = "Otvori";
            this.btn_OtvoriSliku.UseVisualStyleBackColor = false;
            this.btn_OtvoriSliku.Click += new System.EventHandler(this.btn_OtvoriSliku_Click);
            // 
            // btn_nazad
            // 
            this.btn_nazad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(168)))), ((int)(((byte)(82)))));
            this.btn_nazad.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_nazad.Location = new System.Drawing.Point(1277, 92);
            this.btn_nazad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_nazad.Name = "btn_nazad";
            this.btn_nazad.Size = new System.Drawing.Size(82, 28);
            this.btn_nazad.TabIndex = 218;
            this.btn_nazad.Text = "<<";
            this.btn_nazad.UseVisualStyleBackColor = false;
            this.btn_nazad.Click += new System.EventHandler(this.btn_nazad_Click);
            // 
            // btn_Napred
            // 
            this.btn_Napred.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(168)))), ((int)(((byte)(82)))));
            this.btn_Napred.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Napred.Location = new System.Drawing.Point(1277, 62);
            this.btn_Napred.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Napred.Name = "btn_Napred";
            this.btn_Napred.Size = new System.Drawing.Size(82, 28);
            this.btn_Napred.TabIndex = 218;
            this.btn_Napred.Text = ">>";
            this.btn_Napred.UseVisualStyleBackColor = false;
            this.btn_Napred.Click += new System.EventHandler(this.btn_Napred_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(168)))), ((int)(((byte)(82)))));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(1277, 177);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 28);
            this.button1.TabIndex = 218;
            this.button1.Text = "Otvori folder";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtPosao
            // 
            this.txtPosao.Location = new System.Drawing.Point(108, 202);
            this.txtPosao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPosao.Name = "txtPosao";
            this.txtPosao.Size = new System.Drawing.Size(140, 20);
            this.txtPosao.TabIndex = 220;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 219;
            this.label1.Text = "Posao:";
            // 
            // txtNGRazduzenje
            // 
            this.txtNGRazduzenje.Location = new System.Drawing.Point(806, 203);
            this.txtNGRazduzenje.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNGRazduzenje.Name = "txtNGRazduzenje";
            this.txtNGRazduzenje.Size = new System.Drawing.Size(144, 20);
            this.txtNGRazduzenje.TabIndex = 223;
            // 
            // txtNGZaduzenje
            // 
            this.txtNGZaduzenje.Location = new System.Drawing.Point(507, 202);
            this.txtNGZaduzenje.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNGZaduzenje.Name = "txtNGZaduzenje";
            this.txtNGZaduzenje.Size = new System.Drawing.Size(144, 20);
            this.txtNGZaduzenje.TabIndex = 224;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(659, 203);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(123, 13);
            this.label14.TabIndex = 221;
            this.label14.Text = "Nivo goriva Razduženje:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(368, 206);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(117, 13);
            this.label16.TabIndex = 222;
            this.label16.Text = "Nivo goriva Zaduženje:";
            // 
            // cboMestoPolaska
            // 
            this.cboMestoPolaska.FormattingEnabled = true;
            this.cboMestoPolaska.Location = new System.Drawing.Point(506, 225);
            this.cboMestoPolaska.Name = "cboMestoPolaska";
            this.cboMestoPolaska.Size = new System.Drawing.Size(146, 21);
            this.cboMestoPolaska.TabIndex = 226;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(369, 228);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 13);
            this.label17.TabIndex = 225;
            this.label17.Text = "Mesto polaska:";
            // 
            // cboMestoDolaska
            // 
            this.cboMestoDolaska.FormattingEnabled = true;
            this.cboMestoDolaska.Location = new System.Drawing.Point(805, 225);
            this.cboMestoDolaska.Name = "cboMestoDolaska";
            this.cboMestoDolaska.Size = new System.Drawing.Size(146, 21);
            this.cboMestoDolaska.TabIndex = 228;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(665, 225);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 13);
            this.label18.TabIndex = 227;
            this.label18.Text = "Mesto dolaska:";
            // 
            // txtUloga
            // 
            this.txtUloga.Location = new System.Drawing.Point(108, 225);
            this.txtUloga.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtUloga.Name = "txtUloga";
            this.txtUloga.Size = new System.Drawing.Size(140, 20);
            this.txtUloga.TabIndex = 230;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(16, 230);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(38, 13);
            this.label19.TabIndex = 229;
            this.label19.Text = "Uloga:";
            // 
            // txtAktivnostID
            // 
            this.txtAktivnostID.Location = new System.Drawing.Point(251, 202);
            this.txtAktivnostID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAktivnostID.Name = "txtAktivnostID";
            this.txtAktivnostID.Size = new System.Drawing.Size(97, 20);
            this.txtAktivnostID.TabIndex = 231;
            // 
            // frmAutomobiliPregledPrijava
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1370, 609);
            this.Controls.Add(this.txtAktivnostID);
            this.Controls.Add(this.txtUloga);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.cboMestoDolaska);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.cboMestoPolaska);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtNGRazduzenje);
            this.Controls.Add(this.txtNGZaduzenje);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtPosao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Napred);
            this.Controls.Add(this.btn_nazad);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_OtvoriSliku);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txt_Sifra);
            this.Controls.Add(this.cb_DirPredRaz);
            this.Controls.Add(this.cb_DirPredZad);
            this.Controls.Add(this.txt_KmRazduzenje);
            this.Controls.Add(this.txt_KmZaduzenje);
            this.Controls.Add(this.combo_NivoUljaRaz);
            this.Controls.Add(this.combo_NivoUljaZad);
            this.Controls.Add(this.combo_CistocaUnutraRaz);
            this.Controls.Add(this.combo_CistocaUnutraZad);
            this.Controls.Add(this.combo_CistocaSpoljaRaz);
            this.Controls.Add(this.combo_CistocaSpoljaZad);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.combo_Automobil);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dt_Odjava);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dtpDatumPrijave);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.combo_Zaposleni);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAutomobiliPregledPrijava";
            this.Text = "Automobili pregled Zaduzenja/Razduzduzenja";
            this.Load += new System.EventHandler(this.frmAutomobiliPregledPrijava_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox combo_Zaposleni;
        private System.Windows.Forms.Label label5;
        private MetroFramework.Controls.MetroDateTime dtpDatumPrijave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combo_Automobil;
        private System.Windows.Forms.CheckBox cb_DirPredZad;
        private System.Windows.Forms.TextBox txt_KmZaduzenje;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox combo_CistocaSpoljaZad;
        private System.Windows.Forms.ComboBox combo_CistocaUnutraZad;
        private System.Windows.Forms.ComboBox combo_NivoUljaZad;
        private System.Windows.Forms.Label label9;
        private MetroFramework.Controls.MetroDateTime dt_Odjava;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_KmRazduzenje;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox combo_CistocaSpoljaRaz;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox combo_CistocaUnutraRaz;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox combo_NivoUljaRaz;
        private System.Windows.Forms.CheckBox cb_DirPredRaz;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_Sifra;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_OtvoriSliku;
        private System.Windows.Forms.Button btn_nazad;
        private System.Windows.Forms.Button btn_Napred;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtPosao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNGRazduzenje;
        private System.Windows.Forms.TextBox txtNGZaduzenje;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cboMestoPolaska;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cboMestoDolaska;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtUloga;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtAktivnostID;
    }
}