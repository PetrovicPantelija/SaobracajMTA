namespace Testiranje.Sifarnici
{
    partial class frmVrstaManipulacije
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVrstaManipulacije));
            this.meniHeader = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPrvi = new System.Windows.Forms.ToolStripButton();
            this.tsNazad = new System.Windows.Forms.ToolStripButton();
            this.tsNapred = new System.Windows.Forms.ToolStripButton();
            this.tsPoslednja = new System.Windows.Forms.ToolStripButton();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.chkUticeSkladisno = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboTipManipulacije = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboOrgJed = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOznaka = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRelacija = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCena = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkAdministratvna = new System.Windows.Forms.CheckBox();
            this.chkDrumski = new System.Windows.Forms.CheckBox();
            this.chkDodatna = new System.Windows.Forms.CheckBox();
            this.panelDodatnaUluga = new System.Windows.Forms.Panel();
            this.chkPotvrdaUradio = new System.Windows.Forms.CheckBox();
            this.txtApstrakt2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtApstrakt1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cboGrupaVrsteManipulacije = new Syncfusion.WinForms.ListView.SfComboBox();
            this.txtTipKont = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtJM = new System.Windows.Forms.ComboBox();
            this.txtJM2 = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cboRLTerminal3 = new System.Windows.Forms.ComboBox();
            this.cboRLTerminal2 = new System.Windows.Forms.ComboBox();
            this.cboRLTerminal = new System.Windows.Forms.ComboBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.meniHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCena)).BeginInit();
            this.panelDodatnaUluga.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboGrupaVrsteManipulacije)).BeginInit();
            this.panelHeader.SuspendLayout();
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
            this.meniHeader.Size = new System.Drawing.Size(1173, 27);
            this.meniHeader.TabIndex = 107;
            this.meniHeader.Text = "Štampaj izveštaj";
            this.meniHeader.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
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
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtSifra.Location = new System.Drawing.Point(64, 85);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(86, 22);
            this.txtSifra.TabIndex = 108;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 109;
            this.label1.Text = "Šifra:";
            // 
            // txtNaziv
            // 
            this.txtNaziv.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtNaziv.Location = new System.Drawing.Point(64, 114);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(229, 22);
            this.txtNaziv.TabIndex = 110;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(7, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 15);
            this.label2.TabIndex = 111;
            this.label2.Text = "Naziv:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(12, 329);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1149, 197);
            this.dataGridView1.TabIndex = 112;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(7, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 115;
            this.label3.Text = "JM 1:";
            // 
            // chkUticeSkladisno
            // 
            this.chkUticeSkladisno.AutoSize = true;
            this.chkUticeSkladisno.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUticeSkladisno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkUticeSkladisno.ForeColor = System.Drawing.Color.Black;
            this.chkUticeSkladisno.Location = new System.Drawing.Point(12, 218);
            this.chkUticeSkladisno.Name = "chkUticeSkladisno";
            this.chkUticeSkladisno.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkUticeSkladisno.Size = new System.Drawing.Size(109, 19);
            this.chkUticeSkladisno.TabIndex = 238;
            this.chkUticeSkladisno.Text = "Utiče skladišno";
            this.chkUticeSkladisno.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(8, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 240;
            this.label4.Text = "JM 2:";
            // 
            // cboTipManipulacije
            // 
            this.cboTipManipulacije.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboTipManipulacije.FormattingEnabled = true;
            this.cboTipManipulacije.Location = new System.Drawing.Point(748, 132);
            this.cboTipManipulacije.Name = "cboTipManipulacije";
            this.cboTipManipulacije.Size = new System.Drawing.Size(229, 24);
            this.cboTipManipulacije.TabIndex = 242;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(671, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 241;
            this.label5.Text = "Tip usluge:";
            // 
            // cboOrgJed
            // 
            this.cboOrgJed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboOrgJed.FormattingEnabled = true;
            this.cboOrgJed.Location = new System.Drawing.Point(748, 67);
            this.cboOrgJed.Name = "cboOrgJed";
            this.cboOrgJed.Size = new System.Drawing.Size(229, 24);
            this.cboOrgJed.TabIndex = 244;
            this.cboOrgJed.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(585, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(157, 28);
            this.label6.TabIndex = 243;
            this.label6.Text = "OJ koja izvšava zadatak:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // txtOznaka
            // 
            this.txtOznaka.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtOznaka.Location = new System.Drawing.Point(748, 161);
            this.txtOznaka.Name = "txtOznaka";
            this.txtOznaka.Size = new System.Drawing.Size(229, 22);
            this.txtOznaka.TabIndex = 245;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(681, 161);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 246;
            this.label7.Text = "Oznaka:";
            // 
            // txtRelacija
            // 
            this.txtRelacija.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtRelacija.Location = new System.Drawing.Point(748, 191);
            this.txtRelacija.Name = "txtRelacija";
            this.txtRelacija.Size = new System.Drawing.Size(229, 22);
            this.txtRelacija.TabIndex = 247;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(683, 189);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 15);
            this.label8.TabIndex = 248;
            this.label8.Text = "Relacija:";
            // 
            // txtCena
            // 
            this.txtCena.DecimalPlaces = 2;
            this.txtCena.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtCena.Location = new System.Drawing.Point(989, 101);
            this.txtCena.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtCena.Name = "txtCena";
            this.txtCena.Size = new System.Drawing.Size(133, 22);
            this.txtCena.TabIndex = 249;
            this.txtCena.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(986, 83);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 15);
            this.label13.TabIndex = 250;
            this.label13.Text = "Cena JM1:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(1128, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 16);
            this.label9.TabIndex = 251;
            this.label9.Text = "EUR";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(299, 86);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 15);
            this.label10.TabIndex = 252;
            this.label10.Text = "Grupa usluge:";
            // 
            // chkAdministratvna
            // 
            this.chkAdministratvna.AutoSize = true;
            this.chkAdministratvna.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAdministratvna.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkAdministratvna.ForeColor = System.Drawing.Color.Black;
            this.chkAdministratvna.Location = new System.Drawing.Point(12, 242);
            this.chkAdministratvna.Name = "chkAdministratvna";
            this.chkAdministratvna.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkAdministratvna.Size = new System.Drawing.Size(109, 19);
            this.chkAdministratvna.TabIndex = 254;
            this.chkAdministratvna.Text = "Administrativna";
            this.chkAdministratvna.UseVisualStyleBackColor = true;
            // 
            // chkDrumski
            // 
            this.chkDrumski.AutoSize = true;
            this.chkDrumski.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDrumski.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkDrumski.ForeColor = System.Drawing.Color.Black;
            this.chkDrumski.Location = new System.Drawing.Point(5, 267);
            this.chkDrumski.Name = "chkDrumski";
            this.chkDrumski.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkDrumski.Size = new System.Drawing.Size(116, 19);
            this.chkDrumski.TabIndex = 255;
            this.chkDrumski.Text = "Drumska usluga";
            this.chkDrumski.UseVisualStyleBackColor = true;
            // 
            // chkDodatna
            // 
            this.chkDodatna.AutoSize = true;
            this.chkDodatna.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDodatna.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkDodatna.ForeColor = System.Drawing.Color.Black;
            this.chkDodatna.Location = new System.Drawing.Point(8, 291);
            this.chkDodatna.Name = "chkDodatna";
            this.chkDodatna.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkDodatna.Size = new System.Drawing.Size(113, 19);
            this.chkDodatna.TabIndex = 256;
            this.chkDodatna.Text = "Dodatna usluga";
            this.chkDodatna.UseVisualStyleBackColor = true;
            this.chkDodatna.CheckedChanged += new System.EventHandler(this.chkDodatna_CheckedChanged);
            // 
            // panelDodatnaUluga
            // 
            this.panelDodatnaUluga.Controls.Add(this.chkPotvrdaUradio);
            this.panelDodatnaUluga.Controls.Add(this.txtApstrakt2);
            this.panelDodatnaUluga.Controls.Add(this.label12);
            this.panelDodatnaUluga.Controls.Add(this.txtApstrakt1);
            this.panelDodatnaUluga.Controls.Add(this.label11);
            this.panelDodatnaUluga.Location = new System.Drawing.Point(293, 218);
            this.panelDodatnaUluga.Name = "panelDodatnaUluga";
            this.panelDodatnaUluga.Size = new System.Drawing.Size(322, 98);
            this.panelDodatnaUluga.TabIndex = 257;
            this.panelDodatnaUluga.Visible = false;
            this.panelDodatnaUluga.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDodatnaUluga_Paint);
            // 
            // chkPotvrdaUradio
            // 
            this.chkPotvrdaUradio.AutoSize = true;
            this.chkPotvrdaUradio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPotvrdaUradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkPotvrdaUradio.ForeColor = System.Drawing.Color.Black;
            this.chkPotvrdaUradio.Location = new System.Drawing.Point(9, 6);
            this.chkPotvrdaUradio.Name = "chkPotvrdaUradio";
            this.chkPotvrdaUradio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkPotvrdaUradio.Size = new System.Drawing.Size(107, 19);
            this.chkPotvrdaUradio.TabIndex = 261;
            this.chkPotvrdaUradio.Text = "Potvrda Uradio";
            this.chkPotvrdaUradio.UseVisualStyleBackColor = true;
            // 
            // txtApstrakt2
            // 
            this.txtApstrakt2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtApstrakt2.Location = new System.Drawing.Point(81, 62);
            this.txtApstrakt2.Name = "txtApstrakt2";
            this.txtApstrakt2.Size = new System.Drawing.Size(229, 22);
            this.txtApstrakt2.TabIndex = 259;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(6, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 15);
            this.label12.TabIndex = 260;
            this.label12.Text = "Naziv kol 2:";
            // 
            // txtApstrakt1
            // 
            this.txtApstrakt1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtApstrakt1.Location = new System.Drawing.Point(81, 34);
            this.txtApstrakt1.Name = "txtApstrakt1";
            this.txtApstrakt1.Size = new System.Drawing.Size(229, 22);
            this.txtApstrakt1.TabIndex = 257;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(6, 37);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 15);
            this.label11.TabIndex = 258;
            this.label11.Text = "Naziv kol 1:";
            // 
            // cboGrupaVrsteManipulacije
            // 
            this.cboGrupaVrsteManipulacije.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.cboGrupaVrsteManipulacije.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboGrupaVrsteManipulacije.Location = new System.Drawing.Point(302, 108);
            this.cboGrupaVrsteManipulacije.Name = "cboGrupaVrsteManipulacije";
            this.cboGrupaVrsteManipulacije.Size = new System.Drawing.Size(226, 28);
            this.cboGrupaVrsteManipulacije.Style.EditorStyle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboGrupaVrsteManipulacije.Style.ReadOnlyEditorStyle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboGrupaVrsteManipulacije.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cboGrupaVrsteManipulacije.Style.TokenStyle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboGrupaVrsteManipulacije.TabIndex = 259;
            // 
            // txtTipKont
            // 
            this.txtTipKont.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtTipKont.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtTipKont.FormattingEnabled = true;
            this.txtTipKont.ItemHeight = 13;
            this.txtTipKont.Location = new System.Drawing.Point(748, 101);
            this.txtTipKont.Margin = new System.Windows.Forms.Padding(2);
            this.txtTipKont.Name = "txtTipKont";
            this.txtTipKont.Size = new System.Drawing.Size(229, 21);
            this.txtTipKont.TabIndex = 260;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(649, 101);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 13);
            this.label14.TabIndex = 261;
            this.label14.Text = "Vrsta kontejnera";
            // 
            // txtJM
            // 
            this.txtJM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtJM.FormattingEnabled = true;
            this.txtJM.Location = new System.Drawing.Point(64, 140);
            this.txtJM.Name = "txtJM";
            this.txtJM.Size = new System.Drawing.Size(229, 24);
            this.txtJM.TabIndex = 262;
            // 
            // txtJM2
            // 
            this.txtJM2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtJM2.FormattingEnabled = true;
            this.txtJM2.Location = new System.Drawing.Point(64, 174);
            this.txtJM2.Name = "txtJM2";
            this.txtJM2.Size = new System.Drawing.Size(229, 24);
            this.txtJM2.TabIndex = 263;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(617, 222);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(121, 15);
            this.label15.TabIndex = 264;
            this.label15.Text = "Terminalska relacija:";
            // 
            // cboRLTerminal3
            // 
            this.cboRLTerminal3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboRLTerminal3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboRLTerminal3.FormattingEnabled = true;
            this.cboRLTerminal3.ItemHeight = 13;
            this.cboRLTerminal3.Location = new System.Drawing.Point(748, 274);
            this.cboRLTerminal3.Margin = new System.Windows.Forms.Padding(2);
            this.cboRLTerminal3.Name = "cboRLTerminal3";
            this.cboRLTerminal3.Size = new System.Drawing.Size(229, 21);
            this.cboRLTerminal3.TabIndex = 462;
            // 
            // cboRLTerminal2
            // 
            this.cboRLTerminal2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboRLTerminal2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboRLTerminal2.FormattingEnabled = true;
            this.cboRLTerminal2.ItemHeight = 13;
            this.cboRLTerminal2.Location = new System.Drawing.Point(748, 249);
            this.cboRLTerminal2.Margin = new System.Windows.Forms.Padding(2);
            this.cboRLTerminal2.Name = "cboRLTerminal2";
            this.cboRLTerminal2.Size = new System.Drawing.Size(229, 21);
            this.cboRLTerminal2.TabIndex = 461;
            // 
            // cboRLTerminal
            // 
            this.cboRLTerminal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboRLTerminal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboRLTerminal.FormattingEnabled = true;
            this.cboRLTerminal.ItemHeight = 13;
            this.cboRLTerminal.Location = new System.Drawing.Point(748, 224);
            this.cboRLTerminal.Margin = new System.Windows.Forms.Padding(2);
            this.cboRLTerminal.Name = "cboRLTerminal";
            this.cboRLTerminal.Size = new System.Drawing.Size(229, 21);
            this.cboRLTerminal.TabIndex = 460;
            // 
            // panelHeader
            // 
            this.panelHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.panel3);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 27);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1173, 34);
            this.panelHeader.TabIndex = 472;
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
            this.panel3.Size = new System.Drawing.Size(108, 32);
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
            // frmVrstaManipulacije
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1173, 537);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.cboRLTerminal3);
            this.Controls.Add(this.cboRLTerminal2);
            this.Controls.Add(this.cboRLTerminal);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtJM2);
            this.Controls.Add(this.txtJM);
            this.Controls.Add(this.txtTipKont);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cboGrupaVrsteManipulacije);
            this.Controls.Add(this.panelDodatnaUluga);
            this.Controls.Add(this.chkDrumski);
            this.Controls.Add(this.chkAdministratvna);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.chkDodatna);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCena);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtRelacija);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtOznaka);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboOrgJed);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboTipManipulacije);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkUticeSkladisno);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.meniHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVrstaManipulacije";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vrsta usluge";
            this.Load += new System.EventHandler(this.frmVrstaManipulacije_Load);
            this.meniHeader.ResumeLayout(false);
            this.meniHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCena)).EndInit();
            this.panelDodatnaUluga.ResumeLayout(false);
            this.panelDodatnaUluga.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboGrupaVrsteManipulacije)).EndInit();
            this.panelHeader.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkUticeSkladisno;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboTipManipulacije;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboOrgJed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOznaka;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRelacija;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown txtCena;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkAdministratvna;
        private System.Windows.Forms.CheckBox chkDrumski;
        private System.Windows.Forms.CheckBox chkDodatna;
        private System.Windows.Forms.Panel panelDodatnaUluga;
        private System.Windows.Forms.CheckBox chkPotvrdaUradio;
        private System.Windows.Forms.TextBox txtApstrakt2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtApstrakt1;
        private System.Windows.Forms.Label label11;
        private Syncfusion.WinForms.ListView.SfComboBox cboGrupaVrsteManipulacije;
        private System.Windows.Forms.ComboBox txtTipKont;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox txtJM;
        private System.Windows.Forms.ComboBox txtJM2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cboRLTerminal3;
        private System.Windows.Forms.ComboBox cboRLTerminal2;
        private System.Windows.Forms.ComboBox cboRLTerminal;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button22;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
    }
}