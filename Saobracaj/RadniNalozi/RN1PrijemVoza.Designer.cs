﻿
namespace Saobracaj.RadniNalozi
{
    partial class RN1PrijemVoza
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RN1PrijemVoza));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtDatumRasporeda = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDatumRealizacije = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbrojkontejnera = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbovrstakontejnera = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBrojPlombe = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cboUvoznik = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cboVrstaRobe = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboNaSkladiste = new System.Windows.Forms.ComboBox();
            this.txtNAPozicijaskladista = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cboNaPoziciju = new System.Windows.Forms.ComboBox();
            this.cboUsluge = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtNalogRealizovao = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtNapomena = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboSaVoznog = new System.Windows.Forms.ComboBox();
            this.cboBrodar = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.label10 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtNalogIzdao = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.txtPrijemID = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtNalogID = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.chkZavrsen = new System.Windows.Forms.CheckBox();
            this.label41 = new System.Windows.Forms.Label();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.label20 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDatumRasporeda
            // 
            this.txtDatumRasporeda.CustomFormat = "dd.MM.yyyy HH:mm";
            this.txtDatumRasporeda.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDatumRasporeda.Location = new System.Drawing.Point(684, 92);
            this.txtDatumRasporeda.Margin = new System.Windows.Forms.Padding(2);
            this.txtDatumRasporeda.Name = "txtDatumRasporeda";
            this.txtDatumRasporeda.Size = new System.Drawing.Size(141, 20);
            this.txtDatumRasporeda.TabIndex = 179;
            this.txtDatumRasporeda.Value = new System.DateTime(2023, 6, 23, 0, 0, 0, 0);
            this.txtDatumRasporeda.ValueChanged += new System.EventHandler(this.txtDatumRasporeda_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(681, 74);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 180;
            this.label7.Text = "Datum rasporeda";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(681, 33);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 182;
            this.label6.Text = "Nalog izdao";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 243);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 184;
            this.label2.Text = "Sa voznog sredstva - VO";
            // 
            // txtDatumRealizacije
            // 
            this.txtDatumRealizacije.CustomFormat = "dd.MM.yyyy HH:mm";
            this.txtDatumRealizacije.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDatumRealizacije.Location = new System.Drawing.Point(888, 93);
            this.txtDatumRealizacije.Margin = new System.Windows.Forms.Padding(2);
            this.txtDatumRealizacije.Name = "txtDatumRealizacije";
            this.txtDatumRealizacije.Size = new System.Drawing.Size(122, 20);
            this.txtDatumRealizacije.TabIndex = 185;
            this.txtDatumRealizacije.Value = new System.DateTime(2023, 6, 23, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(270, 156);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 186;
            this.label1.Text = " ";
            // 
            // txtbrojkontejnera
            // 
            this.txtbrojkontejnera.Location = new System.Drawing.Point(10, 98);
            this.txtbrojkontejnera.Margin = new System.Windows.Forms.Padding(2);
            this.txtbrojkontejnera.Name = "txtbrojkontejnera";
            this.txtbrojkontejnera.Size = new System.Drawing.Size(142, 20);
            this.txtbrojkontejnera.TabIndex = 187;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 83);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 188;
            this.label3.Text = "Broj kontejnera";
            // 
            // cbovrstakontejnera
            // 
            this.cbovrstakontejnera.FormattingEnabled = true;
            this.cbovrstakontejnera.ItemHeight = 13;
            this.cbovrstakontejnera.Location = new System.Drawing.Point(10, 138);
            this.cbovrstakontejnera.Margin = new System.Windows.Forms.Padding(2);
            this.cbovrstakontejnera.Name = "cbovrstakontejnera";
            this.cbovrstakontejnera.Size = new System.Drawing.Size(209, 21);
            this.cbovrstakontejnera.TabIndex = 189;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 121);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 190;
            this.label4.Text = "Vrsta kontejnera";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(340, 412);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 192;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(361, 79);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 193;
            this.label11.Text = "Broj plombe";
            // 
            // txtBrojPlombe
            // 
            this.txtBrojPlombe.Location = new System.Drawing.Point(364, 94);
            this.txtBrojPlombe.Margin = new System.Windows.Forms.Padding(2);
            this.txtBrojPlombe.Name = "txtBrojPlombe";
            this.txtBrojPlombe.Size = new System.Drawing.Size(101, 20);
            this.txtBrojPlombe.TabIndex = 194;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(361, 33);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 195;
            this.label12.Text = "Uvoznik";
            // 
            // cboUvoznik
            // 
            this.cboUvoznik.FormattingEnabled = true;
            this.cboUvoznik.ItemHeight = 13;
            this.cboUvoznik.Location = new System.Drawing.Point(364, 50);
            this.cboUvoznik.Margin = new System.Windows.Forms.Padding(2);
            this.cboUvoznik.Name = "cboUvoznik";
            this.cboUvoznik.Size = new System.Drawing.Size(265, 21);
            this.cboUvoznik.TabIndex = 196;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(361, 120);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 13);
            this.label13.TabIndex = 197;
            this.label13.Text = "Naziv brodara";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1124, 275);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(55, 13);
            this.label14.TabIndex = 199;
            this.label14.Text = "Vrsta robe";
            this.label14.Visible = false;
            // 
            // cboVrstaRobe
            // 
            this.cboVrstaRobe.FormattingEnabled = true;
            this.cboVrstaRobe.ItemHeight = 13;
            this.cboVrstaRobe.Location = new System.Drawing.Point(1094, 304);
            this.cboVrstaRobe.Margin = new System.Windows.Forms.Padding(2);
            this.cboVrstaRobe.Name = "cboVrstaRobe";
            this.cboVrstaRobe.Size = new System.Drawing.Size(85, 21);
            this.cboVrstaRobe.TabIndex = 200;
            this.cboVrstaRobe.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 39);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 201;
            this.label8.Text = "NA SKLADISTE";
            // 
            // cboNaSkladiste
            // 
            this.cboNaSkladiste.FormattingEnabled = true;
            this.cboNaSkladiste.ItemHeight = 13;
            this.cboNaSkladiste.Location = new System.Drawing.Point(12, 56);
            this.cboNaSkladiste.Margin = new System.Windows.Forms.Padding(2);
            this.cboNaSkladiste.Name = "cboNaSkladiste";
            this.cboNaSkladiste.Size = new System.Drawing.Size(193, 21);
            this.cboNaSkladiste.TabIndex = 202;
            // 
            // txtNAPozicijaskladista
            // 
            this.txtNAPozicijaskladista.AutoSize = true;
            this.txtNAPozicijaskladista.Location = new System.Drawing.Point(9, 82);
            this.txtNAPozicijaskladista.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtNAPozicijaskladista.Name = "txtNAPozicijaskladista";
            this.txtNAPozicijaskladista.Size = new System.Drawing.Size(76, 13);
            this.txtNAPozicijaskladista.TabIndex = 203;
            this.txtNAPozicijaskladista.Text = "NA POZICIJU ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(361, 158);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(131, 13);
            this.label15.TabIndex = 204;
            this.label15.Text = "ID i naziv planirane usluge";
            this.label15.Click += new System.EventHandler(this.label10_Click);
            // 
            // cboNaPoziciju
            // 
            this.cboNaPoziciju.FormattingEnabled = true;
            this.cboNaPoziciju.ItemHeight = 13;
            this.cboNaPoziciju.Location = new System.Drawing.Point(12, 99);
            this.cboNaPoziciju.Margin = new System.Windows.Forms.Padding(2);
            this.cboNaPoziciju.Name = "cboNaPoziciju";
            this.cboNaPoziciju.Size = new System.Drawing.Size(193, 21);
            this.cboNaPoziciju.TabIndex = 205;
            // 
            // cboUsluge
            // 
            this.cboUsluge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.cboUsluge.FormattingEnabled = true;
            this.cboUsluge.ItemHeight = 13;
            this.cboUsluge.Location = new System.Drawing.Point(364, 173);
            this.cboUsluge.Margin = new System.Windows.Forms.Padding(2);
            this.cboUsluge.Name = "cboUsluge";
            this.cboUsluge.Size = new System.Drawing.Size(265, 21);
            this.cboUsluge.TabIndex = 206;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(885, 33);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(86, 13);
            this.label17.TabIndex = 207;
            this.label17.Text = "Nalog realizovao";
            // 
            // txtNalogRealizovao
            // 
            this.txtNalogRealizovao.Location = new System.Drawing.Point(888, 48);
            this.txtNalogRealizovao.Margin = new System.Windows.Forms.Padding(2);
            this.txtNalogRealizovao.Name = "txtNalogRealizovao";
            this.txtNalogRealizovao.Size = new System.Drawing.Size(192, 20);
            this.txtNalogRealizovao.TabIndex = 208;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(885, 129);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 13);
            this.label16.TabIndex = 209;
            this.label16.Text = "Napomena";
            // 
            // txtNapomena
            // 
            this.txtNapomena.Location = new System.Drawing.Point(888, 146);
            this.txtNapomena.Margin = new System.Windows.Forms.Padding(2);
            this.txtNapomena.Multiline = true;
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.Size = new System.Drawing.Size(213, 58);
            this.txtNapomena.TabIndex = 210;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(885, 74);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 211;
            this.label9.Text = "Datum realizacije";
            // 
            // cboSaVoznog
            // 
            this.cboSaVoznog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.cboSaVoznog.FormattingEnabled = true;
            this.cboSaVoznog.ItemHeight = 13;
            this.cboSaVoznog.Location = new System.Drawing.Point(364, 258);
            this.cboSaVoznog.Margin = new System.Windows.Forms.Padding(2);
            this.cboSaVoznog.Name = "cboSaVoznog";
            this.cboSaVoznog.Size = new System.Drawing.Size(265, 21);
            this.cboSaVoznog.TabIndex = 205;
            // 
            // cboBrodar
            // 
            this.cboBrodar.FormattingEnabled = true;
            this.cboBrodar.ItemHeight = 13;
            this.cboBrodar.Location = new System.Drawing.Point(364, 135);
            this.cboBrodar.Margin = new System.Windows.Forms.Padding(2);
            this.cboBrodar.Name = "cboBrodar";
            this.cboBrodar.Size = new System.Drawing.Size(265, 21);
            this.cboBrodar.TabIndex = 205;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.DodgerBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsSave,
            this.tsDelete,
            this.toolStripSeparator1,
            this.toolStripButton1,
            this.toolStripButton3,
            this.toolStripButton2,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1179, 27);
            this.toolStrip1.TabIndex = 212;
            this.toolStrip1.Text = "toolStrip1";
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
            // toolStripButton1
            // 
            this.toolStripButton1.BackColor = System.Drawing.Color.DarkRed;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.ForeColor = System.Drawing.Color.White;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(197, 24);
            this.toolStripButton1.Text = "Formiraj radne naloge prijema voza";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.BackColor = System.Drawing.Color.Gold;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(89, 24);
            this.toolStripButton3.Text = "Ubaci u tekuce";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.BackColor = System.Drawing.Color.SeaGreen;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.ForeColor = System.Drawing.Color.White;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(91, 24);
            this.toolStripButton2.Text = "Potvrdi završen";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 39);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 13);
            this.label10.TabIndex = 188;
            this.label10.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(11, 54);
            this.txtID.Margin = new System.Windows.Forms.Padding(2);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(42, 20);
            this.txtID.TabIndex = 187;
            // 
            // txtNalogIzdao
            // 
            this.txtNalogIzdao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtNalogIzdao.Location = new System.Drawing.Point(684, 48);
            this.txtNalogIzdao.Margin = new System.Windows.Forms.Padding(2);
            this.txtNalogIzdao.Name = "txtNalogIzdao";
            this.txtNalogIzdao.Size = new System.Drawing.Size(177, 20);
            this.txtNalogIzdao.TabIndex = 187;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(11, 378);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1148, 365);
            this.tabControl1.TabIndex = 214;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1140, 339);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Generisani radni nalozi";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 5);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1130, 329);
            this.dataGridView1.TabIndex = 214;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged_1);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(511, 304);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(31, 27);
            this.button1.TabIndex = 215;
            this.button1.Text = "?";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtPrijemID
            // 
            this.txtPrijemID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtPrijemID.Location = new System.Drawing.Point(364, 309);
            this.txtPrijemID.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrijemID.Name = "txtPrijemID";
            this.txtPrijemID.Size = new System.Drawing.Size(142, 20);
            this.txtPrijemID.TabIndex = 216;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(367, 294);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(75, 13);
            this.label18.TabIndex = 217;
            this.label18.Text = "Prijemnica broj";
            // 
            // txtNalogID
            // 
            this.txtNalogID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtNalogID.Location = new System.Drawing.Point(364, 216);
            this.txtNalogID.Margin = new System.Windows.Forms.Padding(2);
            this.txtNalogID.Name = "txtNalogID";
            this.txtNalogID.Size = new System.Drawing.Size(101, 20);
            this.txtNalogID.TabIndex = 219;
            this.txtNalogID.TextChanged += new System.EventHandler(this.txtNalogID_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(361, 201);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(49, 13);
            this.label19.TabIndex = 218;
            this.label19.Text = "Nalog ID";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(229, 45);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 82);
            this.button2.TabIndex = 220;
            this.button2.Text = "Dodela skladista i pozicija";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chkZavrsen
            // 
            this.chkZavrsen.AutoSize = true;
            this.chkZavrsen.Location = new System.Drawing.Point(684, 139);
            this.chkZavrsen.Name = "chkZavrsen";
            this.chkZavrsen.Size = new System.Drawing.Size(65, 17);
            this.chkZavrsen.TabIndex = 221;
            this.chkZavrsen.Text = "Zavrsen";
            this.chkZavrsen.UseVisualStyleBackColor = true;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(50)))));
            this.label41.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label41.ForeColor = System.Drawing.Color.Black;
            this.label41.Location = new System.Drawing.Point(713, 222);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(36, 14);
            this.label41.TabIndex = 261;
            this.label41.Text = "NHM:";
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView3.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView3.Location = new System.Drawing.Point(712, 251);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView3.Name = "dataGridView3";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView3.RowHeadersWidth = 11;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(408, 110);
            this.dataGridView3.TabIndex = 260;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cboNaSkladiste);
            this.panel1.Controls.Add(this.txtNAPozicijaskladista);
            this.panel1.Controls.Add(this.cboNaPoziciju);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(11, 195);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(329, 166);
            this.panel1.TabIndex = 262;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(634, 248);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(38, 40);
            this.button3.TabIndex = 263;
            this.button3.Text = "?";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.BackColor = System.Drawing.Color.DarkTurquoise;
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(168, 24);
            this.toolStripButton4.Text = "Pregled radnih naloga Central";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Maroon;
            this.label20.Location = new System.Drawing.Point(9, 20);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(153, 13);
            this.label20.TabIndex = 221;
            this.label20.Text = "Određivanje skladišta za prijem";
            // 
            // RN1PrijemVoza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1179, 755);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.chkZavrsen);
            this.Controls.Add(this.txtNalogID);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtPrijemID);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboSaVoznog);
            this.Controls.Add(this.txtNapomena);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtNalogRealizovao);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cboUsluge);
            this.Controls.Add(this.cboBrodar);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cboVrstaRobe);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cboUvoznik);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtBrojPlombe);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbovrstakontejnera);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtNalogIzdao);
            this.Controls.Add(this.txtbrojkontejnera);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDatumRealizacije);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDatumRasporeda);
            this.Controls.Add(this.label7);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RN1PrijemVoza";
            this.Text = "RADNI NALOZI - PRIJEM VOZA - RN ZA KALMARISTU";
            this.Load += new System.EventHandler(this.RN1PrijemVoza_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker txtDatumRasporeda;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker txtDatumRealizacije;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbrojkontejnera;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbovrstakontejnera;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBrojPlombe;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboUvoznik;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboVrstaRobe;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboNaSkladiste;
        private System.Windows.Forms.Label txtNAPozicijaskladista;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cboNaPoziciju;
        private System.Windows.Forms.ComboBox cboUsluge;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtNalogRealizovao;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtNapomena;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboSaVoznog;
        private System.Windows.Forms.ComboBox cboBrodar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtNalogIzdao;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtPrijemID;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TextBox txtNalogID;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.CheckBox chkZavrsen;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button button3;
    }
}