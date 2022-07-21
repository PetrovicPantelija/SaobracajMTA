namespace Saobracaj.Dokumenta
{
    partial class frmDokumentaNajava
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDokumentaNajava));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPrvi = new System.Windows.Forms.ToolStripButton();
            this.tsNazad = new System.Windows.Forms.ToolStripButton();
            this.tsNapred = new System.Windows.Forms.ToolStripButton();
            this.tsPoslednja = new System.Windows.Forms.ToolStripButton();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSifraNajave = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtPutanja = new System.Windows.Forms.TextBox();
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPutanja2 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.btnSacuvajTovarniList = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTovarniList = new System.Windows.Forms.TextBox();
            this.btnTovarniList = new System.Windows.Forms.Button();
            this.btnCIT23Sacuvaj = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCIT23 = new System.Windows.Forms.TextBox();
            this.btnCIT23 = new System.Windows.Forms.Button();
            this.btnRacunSacuvaj = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRacun = new System.Windows.Forms.TextBox();
            this.btnRacun = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPrijemnaTeretnica = new System.Windows.Forms.TextBox();
            this.button14 = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.txtTipPrevoza = new System.Windows.Forms.TextBox();
            this.txtObjedinjen = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsSave,
            this.tsDelete,
            this.toolStripSeparator1,
            this.tsPrvi,
            this.tsNazad,
            this.tsNapred,
            this.tsPoslednja});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(835, 25);
            this.toolStrip1.TabIndex = 33;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = ((System.Drawing.Image)(resources.GetObject("tsNew.Image")));
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(23, 22);
            this.tsNew.Text = "Novi";
            this.tsNew.Click += new System.EventHandler(this.tsNew_Click);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(23, 22);
            this.tsSave.Text = "tsSave";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(23, 22);
            this.tsDelete.Text = "toolStripButton1";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsPrvi
            // 
            this.tsPrvi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPrvi.Image = ((System.Drawing.Image)(resources.GetObject("tsPrvi.Image")));
            this.tsPrvi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrvi.Name = "tsPrvi";
            this.tsPrvi.Size = new System.Drawing.Size(23, 22);
            this.tsPrvi.Text = "toolStripButton1";
            // 
            // tsNazad
            // 
            this.tsNazad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNazad.Image = ((System.Drawing.Image)(resources.GetObject("tsNazad.Image")));
            this.tsNazad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNazad.Name = "tsNazad";
            this.tsNazad.Size = new System.Drawing.Size(23, 22);
            this.tsNazad.Text = "toolStripButton1";
            // 
            // tsNapred
            // 
            this.tsNapred.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNapred.Image = ((System.Drawing.Image)(resources.GetObject("tsNapred.Image")));
            this.tsNapred.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNapred.Name = "tsNapred";
            this.tsNapred.Size = new System.Drawing.Size(23, 22);
            this.tsNapred.Text = "toolStripButton1";
            // 
            // tsPoslednja
            // 
            this.tsPoslednja.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPoslednja.Image = ((System.Drawing.Image)(resources.GetObject("tsPoslednja.Image")));
            this.tsPoslednja.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPoslednja.Name = "tsPoslednja";
            this.tsPoslednja.Size = new System.Drawing.Size(23, 22);
            this.tsPoslednja.Text = "toolStripButton1";
            // 
            // txtSifra
            // 
            this.txtSifra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSifra.Location = new System.Drawing.Point(91, 37);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(56, 20);
            this.txtSifra.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Šifra";
            // 
            // txtSifraNajave
            // 
            this.txtSifraNajave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSifraNajave.Location = new System.Drawing.Point(235, 37);
            this.txtSifraNajave.Name = "txtSifraNajave";
            this.txtSifraNajave.Size = new System.Drawing.Size(86, 20);
            this.txtSifraNajave.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(166, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Šifra najave";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(204)))));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(418, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 20);
            this.button1.TabIndex = 4;
            this.button1.Text = "Pronađi";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtPutanja
            // 
            this.txtPutanja.Location = new System.Drawing.Point(90, 191);
            this.txtPutanja.Name = "txtPutanja";
            this.txtPutanja.Size = new System.Drawing.Size(322, 20);
            this.txtPutanja.TabIndex = 3;
            // 
            // ofd1
            // 
            this.ofd1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Ostala dok";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(226)))), ((int)(((byte)(230)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 254);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(810, 157);
            this.dataGridView1.TabIndex = 41;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(492, 191);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 20);
            this.button2.TabIndex = 42;
            this.button2.Text = "Otvori";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "CIM";
            // 
            // txtPutanja2
            // 
            this.txtPutanja2.Enabled = false;
            this.txtPutanja2.Location = new System.Drawing.Point(90, 92);
            this.txtPutanja2.Name = "txtPutanja2";
            this.txtPutanja2.Size = new System.Drawing.Size(322, 20);
            this.txtPutanja2.TabIndex = 43;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(204)))));
            this.button3.Enabled = false;
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button3.Location = new System.Drawing.Point(418, 91);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(59, 20);
            this.button3.TabIndex = 44;
            this.button3.Text = "Pronađi";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button4.Location = new System.Drawing.Point(574, 190);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(61, 20);
            this.button4.TabIndex = 47;
            this.button4.Text = "Sačuvaj";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button6.Enabled = false;
            this.button6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button6.Location = new System.Drawing.Point(574, 88);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(61, 20);
            this.button6.TabIndex = 48;
            this.button6.Text = "Sačuvaj";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // btnSacuvajTovarniList
            // 
            this.btnSacuvajTovarniList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.btnSacuvajTovarniList.Enabled = false;
            this.btnSacuvajTovarniList.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSacuvajTovarniList.Location = new System.Drawing.Point(574, 62);
            this.btnSacuvajTovarniList.Name = "btnSacuvajTovarniList";
            this.btnSacuvajTovarniList.Size = new System.Drawing.Size(61, 20);
            this.btnSacuvajTovarniList.TabIndex = 52;
            this.btnSacuvajTovarniList.Text = "Sačuvaj";
            this.btnSacuvajTovarniList.UseVisualStyleBackColor = false;
            this.btnSacuvajTovarniList.Click += new System.EventHandler(this.btnSacuvajTovarniList_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 51;
            this.label5.Text = "Tovarni list";
            // 
            // txtTovarniList
            // 
            this.txtTovarniList.Enabled = false;
            this.txtTovarniList.Location = new System.Drawing.Point(90, 66);
            this.txtTovarniList.Name = "txtTovarniList";
            this.txtTovarniList.Size = new System.Drawing.Size(322, 20);
            this.txtTovarniList.TabIndex = 49;
            // 
            // btnTovarniList
            // 
            this.btnTovarniList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(204)))));
            this.btnTovarniList.Enabled = false;
            this.btnTovarniList.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnTovarniList.Location = new System.Drawing.Point(418, 65);
            this.btnTovarniList.Name = "btnTovarniList";
            this.btnTovarniList.Size = new System.Drawing.Size(59, 20);
            this.btnTovarniList.TabIndex = 50;
            this.btnTovarniList.Text = "Pronađi";
            this.btnTovarniList.UseVisualStyleBackColor = false;
            this.btnTovarniList.Click += new System.EventHandler(this.btnTovarniList_Click);
            // 
            // btnCIT23Sacuvaj
            // 
            this.btnCIT23Sacuvaj.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.btnCIT23Sacuvaj.Enabled = false;
            this.btnCIT23Sacuvaj.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCIT23Sacuvaj.Location = new System.Drawing.Point(574, 114);
            this.btnCIT23Sacuvaj.Name = "btnCIT23Sacuvaj";
            this.btnCIT23Sacuvaj.Size = new System.Drawing.Size(61, 20);
            this.btnCIT23Sacuvaj.TabIndex = 56;
            this.btnCIT23Sacuvaj.Text = "Sačuvaj";
            this.btnCIT23Sacuvaj.UseVisualStyleBackColor = false;
            this.btnCIT23Sacuvaj.Click += new System.EventHandler(this.btnCIT23Sacuvaj_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 55;
            this.label6.Text = "CIT23";
            // 
            // txtCIT23
            // 
            this.txtCIT23.Enabled = false;
            this.txtCIT23.Location = new System.Drawing.Point(90, 118);
            this.txtCIT23.Name = "txtCIT23";
            this.txtCIT23.Size = new System.Drawing.Size(322, 20);
            this.txtCIT23.TabIndex = 53;
            // 
            // btnCIT23
            // 
            this.btnCIT23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(204)))));
            this.btnCIT23.Enabled = false;
            this.btnCIT23.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCIT23.Location = new System.Drawing.Point(418, 117);
            this.btnCIT23.Name = "btnCIT23";
            this.btnCIT23.Size = new System.Drawing.Size(59, 20);
            this.btnCIT23.TabIndex = 54;
            this.btnCIT23.Text = "Pronađi";
            this.btnCIT23.UseVisualStyleBackColor = false;
            this.btnCIT23.Click += new System.EventHandler(this.btnCIT23_Click);
            // 
            // btnRacunSacuvaj
            // 
            this.btnRacunSacuvaj.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.btnRacunSacuvaj.Enabled = false;
            this.btnRacunSacuvaj.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRacunSacuvaj.Location = new System.Drawing.Point(574, 140);
            this.btnRacunSacuvaj.Name = "btnRacunSacuvaj";
            this.btnRacunSacuvaj.Size = new System.Drawing.Size(61, 20);
            this.btnRacunSacuvaj.TabIndex = 60;
            this.btnRacunSacuvaj.Text = "Sačuvaj";
            this.btnRacunSacuvaj.UseVisualStyleBackColor = false;
            this.btnRacunSacuvaj.Click += new System.EventHandler(this.btnRacunSacuvaj_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 59;
            this.label7.Text = "Račun";
            // 
            // txtRacun
            // 
            this.txtRacun.Enabled = false;
            this.txtRacun.Location = new System.Drawing.Point(90, 144);
            this.txtRacun.Name = "txtRacun";
            this.txtRacun.Size = new System.Drawing.Size(322, 20);
            this.txtRacun.TabIndex = 57;
            // 
            // btnRacun
            // 
            this.btnRacun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(204)))));
            this.btnRacun.Enabled = false;
            this.btnRacun.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRacun.Location = new System.Drawing.Point(418, 143);
            this.btnRacun.Name = "btnRacun";
            this.btnRacun.Size = new System.Drawing.Size(59, 20);
            this.btnRacun.TabIndex = 58;
            this.btnRacun.Text = "Pronađi";
            this.btnRacun.UseVisualStyleBackColor = false;
            this.btnRacun.Click += new System.EventHandler(this.btnRacun_Click);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(204)))));
            this.button12.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button12.Location = new System.Drawing.Point(12, 217);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(217, 31);
            this.button12.TabIndex = 61;
            this.button12.Text = "Dokumentacija pripremljena formiraj pdf ";
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button13.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button13.Location = new System.Drawing.Point(574, 164);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(61, 20);
            this.button13.TabIndex = 65;
            this.button13.Text = "Sačuvaj";
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(11, 167);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 27);
            this.label8.TabIndex = 64;
            this.label8.Text = "Prijemna teretnica";
            // 
            // txtPrijemnaTeretnica
            // 
            this.txtPrijemnaTeretnica.Location = new System.Drawing.Point(90, 168);
            this.txtPrijemnaTeretnica.Name = "txtPrijemnaTeretnica";
            this.txtPrijemnaTeretnica.Size = new System.Drawing.Size(322, 20);
            this.txtPrijemnaTeretnica.TabIndex = 62;
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(204)))));
            this.button14.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button14.Location = new System.Drawing.Point(418, 167);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(59, 20);
            this.button14.TabIndex = 63;
            this.button14.Text = "Pronađi";
            this.button14.UseVisualStyleBackColor = false;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(347, 37);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(66, 13);
            this.label30.TabIndex = 152;
            this.label30.Text = "Tip prevoza:";
            // 
            // txtTipPrevoza
            // 
            this.txtTipPrevoza.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTipPrevoza.Location = new System.Drawing.Point(419, 37);
            this.txtTipPrevoza.Name = "txtTipPrevoza";
            this.txtTipPrevoza.Size = new System.Drawing.Size(216, 20);
            this.txtTipPrevoza.TabIndex = 153;
            // 
            // txtObjedinjen
            // 
            this.txtObjedinjen.Location = new System.Drawing.Point(235, 223);
            this.txtObjedinjen.Name = "txtObjedinjen";
            this.txtObjedinjen.Size = new System.Drawing.Size(341, 20);
            this.txtObjedinjen.TabIndex = 154;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.button5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button5.Location = new System.Drawing.Point(582, 222);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(61, 20);
            this.button5.TabIndex = 155;
            this.button5.Text = "Otvori";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // frmDokumentaNajava
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 423);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.txtObjedinjen);
            this.Controls.Add(this.txtTipPrevoza);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPrijemnaTeretnica);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.btnRacunSacuvaj);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtRacun);
            this.Controls.Add(this.btnRacun);
            this.Controls.Add(this.btnCIT23Sacuvaj);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCIT23);
            this.Controls.Add(this.btnCIT23);
            this.Controls.Add(this.btnSacuvajTovarniList);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTovarniList);
            this.Controls.Add(this.btnTovarniList);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPutanja2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPutanja);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSifraNajave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDokumentaNajava";
            this.Text = "Dokumenta najava";
            this.Load += new System.EventHandler(this.frmDokumentaNajava_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsPrvi;
        private System.Windows.Forms.ToolStripButton tsNazad;
        private System.Windows.Forms.ToolStripButton tsNapred;
        private System.Windows.Forms.ToolStripButton tsPoslednja;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSifraNajave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtPutanja;
        private System.Windows.Forms.OpenFileDialog ofd1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPutanja2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btnSacuvajTovarniList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTovarniList;
        private System.Windows.Forms.Button btnTovarniList;
        private System.Windows.Forms.Button btnCIT23Sacuvaj;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCIT23;
        private System.Windows.Forms.Button btnCIT23;
        private System.Windows.Forms.Button btnRacunSacuvaj;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRacun;
        private System.Windows.Forms.Button btnRacun;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPrijemnaTeretnica;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtTipPrevoza;
        private System.Windows.Forms.TextBox txtObjedinjen;
        private System.Windows.Forms.Button button5;
    }
}