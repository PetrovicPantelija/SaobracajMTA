namespace Testiranje.Sifarnici
{
    partial class frmSkladista
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSkladista));
            this.meniHeader = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPrvi = new System.Windows.Forms.ToolStripButton();
            this.tsNazad = new System.Windows.Forms.ToolStripButton();
            this.tsNapred = new System.Windows.Forms.ToolStripButton();
            this.tsPoslednja = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKapacitet = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.cboGrupaPolja = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOznaka = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSKNaziv = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cboKvalitet = new System.Windows.Forms.ComboBox();
            this.cboBrodar = new System.Windows.Forms.ComboBox();
            this.label51 = new System.Windows.Forms.Label();
            this.cboTipKontejnera = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkAktivna = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboTipPalete = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.meniHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.SuspendLayout();
            // 
            // meniHeader
            // 
            this.meniHeader.BackColor = System.Drawing.Color.DodgerBlue;
            this.meniHeader.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.meniHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsSave,
            this.tsDelete,
            this.toolStripSeparator1,
            this.tsPrvi,
            this.tsNazad,
            this.tsNapred,
            this.tsPoslednja});
            this.meniHeader.Location = new System.Drawing.Point(0, 0);
            this.meniHeader.Name = "meniHeader";
            this.meniHeader.Size = new System.Drawing.Size(1321, 27);
            this.meniHeader.TabIndex = 109;
            this.meniHeader.Text = "Štampaj izveštaj";
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = ((System.Drawing.Image)(resources.GetObject("tsNew.Image")));
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(24, 24);
            this.tsNew.Text = "Novi";
            this.tsNew.Click += new System.EventHandler(this.tsNew_Click_1);
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
            // tsPrvi
            // 
            this.tsPrvi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPrvi.Image = ((System.Drawing.Image)(resources.GetObject("tsPrvi.Image")));
            this.tsPrvi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrvi.Name = "tsPrvi";
            this.tsPrvi.Size = new System.Drawing.Size(24, 24);
            this.tsPrvi.Text = "toolStripButton1";
            this.tsPrvi.Click += new System.EventHandler(this.tsPrvi_Click);
            // 
            // tsNazad
            // 
            this.tsNazad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNazad.Image = ((System.Drawing.Image)(resources.GetObject("tsNazad.Image")));
            this.tsNazad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNazad.Name = "tsNazad";
            this.tsNazad.Size = new System.Drawing.Size(24, 24);
            this.tsNazad.Text = "toolStripButton1";
            this.tsNazad.Click += new System.EventHandler(this.tsNazad_Click);
            // 
            // tsNapred
            // 
            this.tsNapred.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNapred.Image = ((System.Drawing.Image)(resources.GetObject("tsNapred.Image")));
            this.tsNapred.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNapred.Name = "tsNapred";
            this.tsNapred.Size = new System.Drawing.Size(24, 24);
            this.tsNapred.Text = "toolStripButton1";
            this.tsNapred.Click += new System.EventHandler(this.tsNapred_Click);
            // 
            // tsPoslednja
            // 
            this.tsPoslednja.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPoslednja.Image = ((System.Drawing.Image)(resources.GetObject("tsPoslednja.Image")));
            this.tsPoslednja.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPoslednja.Name = "tsPoslednja";
            this.tsPoslednja.Size = new System.Drawing.Size(24, 24);
            this.tsPoslednja.Text = "toolStripButton1";
            this.tsPoslednja.Click += new System.EventHandler(this.tsPoslednja_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 293);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1297, 323);
            this.dataGridView1.TabIndex = 131;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // txtNaziv
            // 
            this.txtNaziv.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtNaziv.Location = new System.Drawing.Point(18, 192);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(217, 22);
            this.txtNaziv.TabIndex = 129;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(15, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 15);
            this.label2.TabIndex = 130;
            this.label2.Text = "Naziv:";
            // 
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtSifra.Location = new System.Drawing.Point(15, 97);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(83, 22);
            this.txtSifra.TabIndex = 127;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 128;
            this.label1.Text = "Šifra:";
            // 
            // txtKapacitet
            // 
            this.txtKapacitet.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtKapacitet.Location = new System.Drawing.Point(900, 98);
            this.txtKapacitet.Name = "txtKapacitet";
            this.txtKapacitet.Size = new System.Drawing.Size(231, 22);
            this.txtKapacitet.TabIndex = 132;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(897, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 133;
            this.label3.Text = "Kapacitet:";
            // 
            // panelHeader
            // 
            this.panelHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.panel2);
            this.panelHeader.Controls.Add(this.panel3);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 27);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1321, 33);
            this.panelHeader.TabIndex = 470;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button23);
            this.panel2.Location = new System.Drawing.Point(126, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1194, 31);
            this.panel2.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.button20);
            this.panel3.Controls.Add(this.button21);
            this.panel3.Controls.Add(this.button22);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(108, 31);
            this.panel3.TabIndex = 2;
            // 
            // button20
            // 
            this.button20.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button20.BackgroundImage")));
            this.button20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button20.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button20.FlatAppearance.BorderSize = 0;
            this.button20.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button20.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button20.Location = new System.Drawing.Point(70, 3);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(27, 27);
            this.button20.TabIndex = 15;
            this.button20.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // button21
            // 
            this.button21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button21.BackgroundImage")));
            this.button21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button21.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button21.FlatAppearance.BorderSize = 0;
            this.button21.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button21.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button21.Location = new System.Drawing.Point(39, 3);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(27, 27);
            this.button21.TabIndex = 14;
            this.button21.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // button22
            // 
            this.button22.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button22.BackgroundImage")));
            this.button22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button22.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button22.FlatAppearance.BorderSize = 0;
            this.button22.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button22.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button22.Location = new System.Drawing.Point(10, 3);
            this.button22.Margin = new System.Windows.Forms.Padding(9, 6, 6, 6);
            this.button22.Name = "button22";
            this.button22.Padding = new System.Windows.Forms.Padding(9);
            this.button22.Size = new System.Drawing.Size(27, 27);
            this.button22.TabIndex = 13;
            this.button22.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.tsNew_Click);
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // cboGrupaPolja
            // 
            this.cboGrupaPolja.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cboGrupaPolja.FormattingEnabled = true;
            this.cboGrupaPolja.Location = new System.Drawing.Point(299, 99);
            this.cboGrupaPolja.Name = "cboGrupaPolja";
            this.cboGrupaPolja.Size = new System.Drawing.Size(228, 21);
            this.cboGrupaPolja.TabIndex = 471;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(296, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 15);
            this.label4.TabIndex = 472;
            this.label4.Text = "Lokacija:";
            // 
            // txtOznaka
            // 
            this.txtOznaka.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtOznaka.Location = new System.Drawing.Point(18, 146);
            this.txtOznaka.Name = "txtOznaka";
            this.txtOznaka.Size = new System.Drawing.Size(217, 22);
            this.txtOznaka.TabIndex = 473;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(15, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 474;
            this.label5.Text = "Oznaka:";
            // 
            // txtSKNaziv
            // 
            this.txtSKNaziv.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtSKNaziv.Location = new System.Drawing.Point(18, 244);
            this.txtSKNaziv.Name = "txtSKNaziv";
            this.txtSKNaziv.Size = new System.Drawing.Size(80, 22);
            this.txtSKNaziv.TabIndex = 475;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(15, 226);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 15);
            this.label6.TabIndex = 476;
            this.label6.Text = "SKNaziv";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(296, 225);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(114, 16);
            this.label21.TabIndex = 482;
            this.label21.Text = "Kvalitet kontejnera:";
            // 
            // cboKvalitet
            // 
            this.cboKvalitet.BackColor = System.Drawing.Color.White;
            this.cboKvalitet.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboKvalitet.FormattingEnabled = true;
            this.cboKvalitet.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cboKvalitet.Location = new System.Drawing.Point(296, 244);
            this.cboKvalitet.Name = "cboKvalitet";
            this.cboKvalitet.Size = new System.Drawing.Size(231, 24);
            this.cboKvalitet.TabIndex = 481;
            // 
            // cboBrodar
            // 
            this.cboBrodar.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboBrodar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBrodar.FormattingEnabled = true;
            this.cboBrodar.ItemHeight = 13;
            this.cboBrodar.Location = new System.Drawing.Point(296, 144);
            this.cboBrodar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboBrodar.Name = "cboBrodar";
            this.cboBrodar.Size = new System.Drawing.Size(231, 21);
            this.cboBrodar.TabIndex = 479;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.ForeColor = System.Drawing.Color.DarkRed;
            this.label51.Location = new System.Drawing.Point(296, 125);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(41, 13);
            this.label51.TabIndex = 480;
            this.label51.Text = "Brodar ";
            // 
            // cboTipKontejnera
            // 
            this.cboTipKontejnera.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboTipKontejnera.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboTipKontejnera.FormattingEnabled = true;
            this.cboTipKontejnera.ItemHeight = 13;
            this.cboTipKontejnera.Location = new System.Drawing.Point(296, 194);
            this.cboTipKontejnera.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboTipKontejnera.Name = "cboTipKontejnera";
            this.cboTipKontejnera.Size = new System.Drawing.Size(231, 21);
            this.cboTipKontejnera.TabIndex = 477;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(297, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 478;
            this.label7.Text = "Vrsta kontejnera";
            // 
            // chkAktivna
            // 
            this.chkAktivna.AutoSize = true;
            this.chkAktivna.BackColor = System.Drawing.Color.Transparent;
            this.chkAktivna.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAktivna.Enabled = false;
            this.chkAktivna.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.chkAktivna.ForeColor = System.Drawing.Color.Black;
            this.chkAktivna.Location = new System.Drawing.Point(629, 144);
            this.chkAktivna.Margin = new System.Windows.Forms.Padding(2);
            this.chkAktivna.Name = "chkAktivna";
            this.chkAktivna.Size = new System.Drawing.Size(64, 19);
            this.chkAktivna.TabIndex = 486;
            this.chkAktivna.Text = "Aktivna";
            this.chkAktivna.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(626, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 16);
            this.label8.TabIndex = 488;
            this.label8.Text = "Tip palete:";
            // 
            // cboTipPalete
            // 
            this.cboTipPalete.BackColor = System.Drawing.Color.White;
            this.cboTipPalete.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboTipPalete.FormattingEnabled = true;
            this.cboTipPalete.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cboTipPalete.Location = new System.Drawing.Point(626, 96);
            this.cboTipPalete.Name = "cboTipPalete";
            this.cboTipPalete.Size = new System.Drawing.Size(231, 24);
            this.cboTipPalete.TabIndex = 487;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button1.Location = new System.Drawing.Point(66, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 31);
            this.button1.TabIndex = 20;
            this.button1.Text = "Neaktivne";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button23
            // 
            this.button23.AutoSize = true;
            this.button23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button23.Dock = System.Windows.Forms.DockStyle.Left;
            this.button23.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button23.FlatAppearance.BorderSize = 0;
            this.button23.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button23.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button23.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button23.Location = new System.Drawing.Point(0, 0);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(66, 31);
            this.button23.TabIndex = 19;
            this.button23.Text = "Aktivne";
            this.button23.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.button23_Click);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Dock = System.Windows.Forms.DockStyle.Left;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button2.Location = new System.Drawing.Point(145, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 31);
            this.button2.TabIndex = 21;
            this.button2.Text = "Refresh";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmSkladista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1321, 628);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboTipPalete);
            this.Controls.Add(this.chkAktivna);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.cboKvalitet);
            this.Controls.Add(this.cboBrodar);
            this.Controls.Add(this.label51);
            this.Controls.Add(this.cboTipKontejnera);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtSKNaziv);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtOznaka);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboGrupaPolja);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.txtKapacitet);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.meniHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSkladista";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Polja";
            this.Load += new System.EventHandler(this.frmSkladista_Load);
            this.meniHeader.ResumeLayout(false);
            this.meniHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip meniHeader;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsPrvi;
        private System.Windows.Forms.ToolStripButton tsNazad;
        private System.Windows.Forms.ToolStripButton tsNapred;
        private System.Windows.Forms.ToolStripButton tsPoslednja;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKapacitet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button22;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboGrupaPolja;
        private System.Windows.Forms.TextBox txtSKNaziv;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOznaka;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cboKvalitet;
        private System.Windows.Forms.ComboBox cboBrodar;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.ComboBox cboTipKontejnera;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkAktivna;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboTipPalete;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Button button2;
    }
}