
namespace Saobracaj.RadniNalozi
{
    partial class Otpremnica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Otpremnica));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbo_Skladiste = new System.Windows.Forms.ComboBox();
            this.cbo_MestoTroska = new System.Windows.Forms.ComboBox();
            this.cbo_Partner = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbo_Lokacija = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cboVozac = new System.Windows.Forms.ComboBox();
            this.btn_Povuci = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cboVozilo = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtVozac = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtVozilo = new System.Windows.Forms.TextBox();
            this.dtpVreme = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBrojKontejnera = new System.Windows.Forms.TextBox();
            this.btnDodeli = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txtNoviKontejner = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.tabSplitterContainer1 = new Syncfusion.Windows.Forms.Tools.TabSplitterContainer();
            this.tabSplitterPage1 = new Syncfusion.Windows.Forms.Tools.TabSplitterPage();
            this.tabSplitterPage2 = new Syncfusion.Windows.Forms.Tools.TabSplitterPage();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.btn_Izaberi = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button23 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button22 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.tabSplitterContainer1.SuspendLayout();
            this.tabSplitterPage1.SuspendLayout();
            this.tabSplitterPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(250, 55);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Skladiste";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(780, 104);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mesto troska";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(780, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Partner";
            // 
            // cbo_Skladiste
            // 
            this.cbo_Skladiste.FormattingEnabled = true;
            this.cbo_Skladiste.Location = new System.Drawing.Point(252, 74);
            this.cbo_Skladiste.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_Skladiste.Name = "cbo_Skladiste";
            this.cbo_Skladiste.Size = new System.Drawing.Size(216, 21);
            this.cbo_Skladiste.TabIndex = 1;
            this.cbo_Skladiste.SelectedIndexChanged += new System.EventHandler(this.cbo_Skladiste_SelectedIndexChanged);
            this.cbo_Skladiste.SelectionChangeCommitted += new System.EventHandler(this.cbo_Skladiste_SelectionChangeCommitted);
            // 
            // cbo_MestoTroska
            // 
            this.cbo_MestoTroska.FormattingEnabled = true;
            this.cbo_MestoTroska.Location = new System.Drawing.Point(783, 119);
            this.cbo_MestoTroska.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_MestoTroska.Name = "cbo_MestoTroska";
            this.cbo_MestoTroska.Size = new System.Drawing.Size(218, 21);
            this.cbo_MestoTroska.TabIndex = 1;
            this.cbo_MestoTroska.Visible = false;
            // 
            // cbo_Partner
            // 
            this.cbo_Partner.FormattingEnabled = true;
            this.cbo_Partner.Location = new System.Drawing.Point(783, 74);
            this.cbo_Partner.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_Partner.Name = "cbo_Partner";
            this.cbo_Partner.Size = new System.Drawing.Size(218, 21);
            this.cbo_Partner.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1302, 45);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 40);
            this.button1.TabIndex = 3;
            this.button1.Text = "Sacuvaj otpremnicu";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(478, 55);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Lokacija";
            this.label4.Visible = false;
            // 
            // cbo_Lokacija
            // 
            this.cbo_Lokacija.FormattingEnabled = true;
            this.cbo_Lokacija.Location = new System.Drawing.Point(481, 74);
            this.cbo_Lokacija.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_Lokacija.Name = "cbo_Lokacija";
            this.cbo_Lokacija.Size = new System.Drawing.Size(217, 21);
            this.cbo_Lokacija.TabIndex = 1;
            this.cbo_Lokacija.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 103);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Referent";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(248, 103);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Vozač";
            this.label6.Visible = false;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(5, 120);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(217, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // cboVozac
            // 
            this.cboVozac.FormattingEnabled = true;
            this.cboVozac.Location = new System.Drawing.Point(251, 119);
            this.cboVozac.Margin = new System.Windows.Forms.Padding(2);
            this.cboVozac.Name = "cboVozac";
            this.cboVozac.Size = new System.Drawing.Size(217, 21);
            this.cboVozac.TabIndex = 6;
            this.cboVozac.Visible = false;
            // 
            // btn_Povuci
            // 
            this.btn_Povuci.Location = new System.Drawing.Point(906, 144);
            this.btn_Povuci.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Povuci.Name = "btn_Povuci";
            this.btn_Povuci.Size = new System.Drawing.Size(95, 40);
            this.btn_Povuci.TabIndex = 8;
            this.btn_Povuci.Text = "Povuci iz porudzbina";
            this.btn_Povuci.UseVisualStyleBackColor = true;
            this.btn_Povuci.Click += new System.EventHandler(this.btn_Povuci_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(250, 142);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Vozilo";
            this.label7.Visible = false;
            // 
            // cboVozilo
            // 
            this.cboVozilo.FormattingEnabled = true;
            this.cboVozilo.Location = new System.Drawing.Point(251, 157);
            this.cboVozilo.Margin = new System.Windows.Forms.Padding(2);
            this.cboVozilo.Name = "cboVozilo";
            this.cboVozilo.Size = new System.Drawing.Size(217, 21);
            this.cboVozilo.TabIndex = 7;
            this.cboVozilo.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(696, 55);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(57, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Spoljni";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtVozac
            // 
            this.txtVozac.Location = new System.Drawing.Point(481, 120);
            this.txtVozac.Margin = new System.Windows.Forms.Padding(2);
            this.txtVozac.Name = "txtVozac";
            this.txtVozac.Size = new System.Drawing.Size(217, 20);
            this.txtVozac.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(478, 103);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Vozač";
            this.label8.Click += new System.EventHandler(this.label6_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(478, 144);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Vozilo";
            this.label9.Click += new System.EventHandler(this.label6_Click);
            // 
            // txtVozilo
            // 
            this.txtVozilo.Location = new System.Drawing.Point(481, 159);
            this.txtVozilo.Margin = new System.Windows.Forms.Padding(2);
            this.txtVozilo.Name = "txtVozilo";
            this.txtVozilo.Size = new System.Drawing.Size(217, 20);
            this.txtVozilo.TabIndex = 11;
            // 
            // dtpVreme
            // 
            this.dtpVreme.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpVreme.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVreme.Location = new System.Drawing.Point(758, 159);
            this.dtpVreme.Name = "dtpVreme";
            this.dtpVreme.ShowUpDown = true;
            this.dtpVreme.Size = new System.Drawing.Size(127, 20);
            this.dtpVreme.TabIndex = 19;
            this.dtpVreme.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(755, 144);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Datum i vreme";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 55);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Broj kontejnera";
            // 
            // txtBrojKontejnera
            // 
            this.txtBrojKontejnera.Location = new System.Drawing.Point(7, 75);
            this.txtBrojKontejnera.Name = "txtBrojKontejnera";
            this.txtBrojKontejnera.Size = new System.Drawing.Size(217, 20);
            this.txtBrojKontejnera.TabIndex = 21;
            // 
            // btnDodeli
            // 
            this.btnDodeli.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDodeli.Location = new System.Drawing.Point(630, 3);
            this.btnDodeli.Name = "btnDodeli";
            this.btnDodeli.Size = new System.Drawing.Size(254, 23);
            this.btnDodeli.TabIndex = 22;
            this.btnDodeli.Text = "^ ^ ^ ^\r\n";
            this.btnDodeli.UseVisualStyleBackColor = true;
            this.btnDodeli.Click += new System.EventHandler(this.btnDodeli_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1302, 86);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 40);
            this.button3.TabIndex = 24;
            this.button3.Text = "OTPREMNICE PREGLED";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1302, 130);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(84, 40);
            this.button4.TabIndex = 25;
            this.button4.Text = "Dodeli kontejner";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtNoviKontejner
            // 
            this.txtNoviKontejner.Location = new System.Drawing.Point(372, 8);
            this.txtNoviKontejner.Name = "txtNoviKontejner";
            this.txtNoviKontejner.Size = new System.Drawing.Size(217, 20);
            this.txtNoviKontejner.TabIndex = 26;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(291, 8);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "Novi kontejner";
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // tabSplitterContainer1
            // 
            this.tabSplitterContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSplitterContainer1.Location = new System.Drawing.Point(1, 184);
            this.tabSplitterContainer1.Name = "tabSplitterContainer1";
            this.tabSplitterContainer1.PrimaryPages.AddRange(new Syncfusion.Windows.Forms.Tools.TabSplitterPage[] {
            this.tabSplitterPage1});
            this.tabSplitterContainer1.SecondaryPages.AddRange(new Syncfusion.Windows.Forms.Tools.TabSplitterPage[] {
            this.tabSplitterPage2});
            this.tabSplitterContainer1.Size = new System.Drawing.Size(1396, 541);
            this.tabSplitterContainer1.SplitterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.tabSplitterContainer1.SplitterPosition = 384;
            this.tabSplitterContainer1.TabIndex = 28;
            // 
            // tabSplitterPage1
            // 
            this.tabSplitterPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSplitterPage1.AutoScroll = true;
            this.tabSplitterPage1.Controls.Add(this.panel1);
            this.tabSplitterPage1.Controls.Add(this.dataGridView1);
            this.tabSplitterPage1.Hide = false;
            this.tabSplitterPage1.Location = new System.Drawing.Point(0, 0);
            this.tabSplitterPage1.Name = "tabSplitterPage1";
            this.tabSplitterPage1.Size = new System.Drawing.Size(1396, 384);
            this.tabSplitterPage1.TabIndex = 1;
            this.tabSplitterPage1.Text = "Otpremnica stavke";
            // 
            // tabSplitterPage2
            // 
            this.tabSplitterPage2.AutoScroll = true;
            this.tabSplitterPage2.Controls.Add(this.dataGridView4);
            this.tabSplitterPage2.Controls.Add(this.btnDodeli);
            this.tabSplitterPage2.Hide = false;
            this.tabSplitterPage2.Location = new System.Drawing.Point(0, 404);
            this.tabSplitterPage2.Name = "tabSplitterPage2";
            this.tabSplitterPage2.Size = new System.Drawing.Size(1396, 137);
            this.tabSplitterPage2.TabIndex = 2;
            this.tabSplitterPage2.Text = "Prijemnice stavke";
            // 
            // dataGridView4
            // 
            this.dataGridView4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(5, 43);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(1457, 91);
            this.dataGridView4.TabIndex = 24;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.dataGridView3);
            this.panel1.Controls.Add(this.txt_ID);
            this.panel1.Controls.Add(this.btn_Izaberi);
            this.panel1.Controls.Add(this.dataGridView2);
            this.panel1.Location = new System.Drawing.Point(21, 38);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1075, 327);
            this.panel1.TabIndex = 11;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Location = new System.Drawing.Point(163, 16);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(57, 24);
            this.button2.TabIndex = 5;
            this.button2.Text = "Nazad";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // dataGridView3
            // 
            this.dataGridView3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(565, 54);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(497, 267);
            this.dataGridView3.TabIndex = 4;
            // 
            // txt_ID
            // 
            this.txt_ID.BackColor = System.Drawing.Color.Bisque;
            this.txt_ID.Enabled = false;
            this.txt_ID.Location = new System.Drawing.Point(9, 20);
            this.txt_ID.Margin = new System.Windows.Forms.Padding(2);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(60, 20);
            this.txt_ID.TabIndex = 2;
            // 
            // btn_Izaberi
            // 
            this.btn_Izaberi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.btn_Izaberi.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Izaberi.Location = new System.Drawing.Point(92, 16);
            this.btn_Izaberi.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Izaberi.Name = "btn_Izaberi";
            this.btn_Izaberi.Size = new System.Drawing.Size(56, 24);
            this.btn_Izaberi.TabIndex = 1;
            this.btn_Izaberi.Text = "Izaberi";
            this.btn_Izaberi.UseVisualStyleBackColor = false;
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(9, 54);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(548, 267);
            this.dataGridView2.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 28);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1380, 345);
            this.dataGridView1.TabIndex = 10;
            // 
            // panelHeader
            // 
            this.panelHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.panel3);
            this.panelHeader.Controls.Add(this.panel4);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1409, 33);
            this.panelHeader.TabIndex = 476;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.button23);
            this.panel3.Controls.Add(this.txtNoviKontejner);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Location = new System.Drawing.Point(60, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1089, 31);
            this.panel3.TabIndex = 6;
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
            this.button23.Font = new System.Drawing.Font("Helvetica", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button23.Location = new System.Drawing.Point(0, 0);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(147, 31);
            this.button23.TabIndex = 16;
            this.button23.Text = "Otpremnice pregled";
            this.button23.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // panel4
            // 
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.button22);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(54, 31);
            this.panel4.TabIndex = 2;
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
            this.button22.Location = new System.Drawing.Point(9, 2);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(27, 27);
            this.button22.TabIndex = 17;
            this.button22.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            this.button5.AutoSize = true;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.Dock = System.Windows.Forms.DockStyle.Left;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Helvetica", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button5.Location = new System.Drawing.Point(147, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(120, 31);
            this.button5.TabIndex = 28;
            this.button5.Text = "Dodeli kontejner";
            this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button4_Click);
            // 
            // Otpremnica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1409, 737);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.tabSplitterContainer1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtBrojKontejnera);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dtpVreme);
            this.Controls.Add(this.btn_Povuci);
            this.Controls.Add(this.cbo_MestoTroska);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboVozac);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cboVozilo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbo_Partner);
            this.Controls.Add(this.cbo_Lokacija);
            this.Controls.Add(this.cbo_Skladiste);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtVozilo);
            this.Controls.Add(this.txtVozac);
            this.Controls.Add(this.checkBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Otpremnica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Otpremnica robe";
            this.Load += new System.EventHandler(this.Otpremnica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.tabSplitterContainer1.ResumeLayout(false);
            this.tabSplitterPage1.ResumeLayout(false);
            this.tabSplitterPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbo_Skladiste;
        private System.Windows.Forms.ComboBox cbo_MestoTroska;
        private System.Windows.Forms.ComboBox cbo_Partner;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbo_Lokacija;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox cboVozac;
        private System.Windows.Forms.Button btn_Povuci;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboVozilo;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtVozac;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtVozilo;
        private System.Windows.Forms.DateTimePicker dtpVreme;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBrojKontejnera;
        private System.Windows.Forms.Button btnDodeli;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtNoviKontejner;
        private System.Windows.Forms.Label label12;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private Syncfusion.Windows.Forms.Tools.TabSplitterContainer tabSplitterContainer1;
        private Syncfusion.Windows.Forms.Tools.TabSplitterPage tabSplitterPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Button btn_Izaberi;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Syncfusion.Windows.Forms.Tools.TabSplitterPage tabSplitterPage2;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button22;
    }
}