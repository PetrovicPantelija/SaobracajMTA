﻿namespace Saobracaj.Sifarnici
{
    partial class frmVrsteAktivnosti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVrsteAktivnosti));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
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
            this.lblNaziv = new System.Windows.Forms.Label();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCena = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOpis = new System.Windows.Forms.TextBox();
            this.chkObracunPoSatu = new System.Windows.Forms.CheckBox();
            this.chkPotrebanRazlog = new System.Windows.Forms.CheckBox();
            this.chkPotrebanNalogodavac = new System.Windows.Forms.CheckBox();
            this.chkPotrebnoVozilo = new System.Windows.Forms.CheckBox();
            this.chkObaveznaNapomena = new System.Windows.Forms.CheckBox();
            this.txtFiksniDeo = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkSmederevo = new System.Windows.Forms.CheckBox();
            this.chkKragujevac = new System.Windows.Forms.CheckBox();
            this.chkCG = new System.Windows.Forms.CheckBox();
            this.chkRemont = new System.Windows.Forms.CheckBox();
            this.chkMilsped = new System.Windows.Forms.CheckBox();
            this.chkUlaziUDnevnicu = new System.Windows.Forms.CheckBox();
            this.txtVremeVagon = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMaxSati = new System.Windows.Forms.NumericUpDown();
            this.txtMaxVagona = new System.Windows.Forms.NumericUpDown();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCena)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFiksniDeo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVremeVagon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxSati)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxVagona)).BeginInit();
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
            this.toolStrip1.Size = new System.Drawing.Size(890, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
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
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(226)))), ((int)(((byte)(230)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 248);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(871, 151);
            this.dataGridView1.TabIndex = 17;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // txtNaziv
            // 
            this.txtNaziv.Location = new System.Drawing.Point(52, 69);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(368, 20);
            this.txtNaziv.TabIndex = 14;
            // 
            // lblNaziv
            // 
            this.lblNaziv.AutoSize = true;
            this.lblNaziv.Location = new System.Drawing.Point(12, 69);
            this.lblNaziv.Name = "lblNaziv";
            this.lblNaziv.Size = new System.Drawing.Size(37, 13);
            this.lblNaziv.TabIndex = 16;
            this.lblNaziv.Text = "Naziv:";
            // 
            // txtSifra
            // 
            this.txtSifra.Location = new System.Drawing.Point(52, 43);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(100, 20);
            this.txtSifra.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Šifra:";
            // 
            // txtCena
            // 
            this.txtCena.DecimalPlaces = 5;
            this.txtCena.Location = new System.Drawing.Point(52, 95);
            this.txtCena.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtCena.Name = "txtCena";
            this.txtCena.Size = new System.Drawing.Size(79, 20);
            this.txtCena.TabIndex = 65;
            this.txtCena.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(133, 97);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(42, 13);
            this.label17.TabIndex = 64;
            this.label17.Text = "( EUR )";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(14, 97);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(35, 13);
            this.label18.TabIndex = 63;
            this.label18.Text = "Cena:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(425, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 160;
            this.label7.Text = "Opis:";
            // 
            // txtOpis
            // 
            this.txtOpis.Location = new System.Drawing.Point(462, 31);
            this.txtOpis.Multiline = true;
            this.txtOpis.Name = "txtOpis";
            this.txtOpis.Size = new System.Drawing.Size(416, 110);
            this.txtOpis.TabIndex = 159;
            // 
            // chkObracunPoSatu
            // 
            this.chkObracunPoSatu.AutoSize = true;
            this.chkObracunPoSatu.Location = new System.Drawing.Point(12, 162);
            this.chkObracunPoSatu.Name = "chkObracunPoSatu";
            this.chkObracunPoSatu.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkObracunPoSatu.Size = new System.Drawing.Size(105, 17);
            this.chkObracunPoSatu.TabIndex = 161;
            this.chkObracunPoSatu.Text = "Obračun po satu";
            this.chkObracunPoSatu.UseVisualStyleBackColor = true;
            // 
            // chkPotrebanRazlog
            // 
            this.chkPotrebanRazlog.AutoSize = true;
            this.chkPotrebanRazlog.Location = new System.Drawing.Point(12, 185);
            this.chkPotrebanRazlog.Name = "chkPotrebanRazlog";
            this.chkPotrebanRazlog.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkPotrebanRazlog.Size = new System.Drawing.Size(132, 17);
            this.chkPotrebanRazlog.TabIndex = 162;
            this.chkPotrebanRazlog.Text = "Potreban unos razloga";
            this.chkPotrebanRazlog.UseVisualStyleBackColor = true;
            // 
            // chkPotrebanNalogodavac
            // 
            this.chkPotrebanNalogodavac.AutoSize = true;
            this.chkPotrebanNalogodavac.Location = new System.Drawing.Point(132, 162);
            this.chkPotrebanNalogodavac.Name = "chkPotrebanNalogodavac";
            this.chkPotrebanNalogodavac.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkPotrebanNalogodavac.Size = new System.Drawing.Size(160, 17);
            this.chkPotrebanNalogodavac.TabIndex = 163;
            this.chkPotrebanNalogodavac.Text = "Potreban unos nalogodavca";
            this.chkPotrebanNalogodavac.UseVisualStyleBackColor = true;
            // 
            // chkPotrebnoVozilo
            // 
            this.chkPotrebnoVozilo.AutoSize = true;
            this.chkPotrebnoVozilo.Location = new System.Drawing.Point(167, 185);
            this.chkPotrebnoVozilo.Name = "chkPotrebnoVozilo";
            this.chkPotrebnoVozilo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkPotrebnoVozilo.Size = new System.Drawing.Size(125, 17);
            this.chkPotrebnoVozilo.TabIndex = 164;
            this.chkPotrebnoVozilo.Text = "Potreban unos vozila";
            this.chkPotrebnoVozilo.UseVisualStyleBackColor = true;
            // 
            // chkObaveznaNapomena
            // 
            this.chkObaveznaNapomena.AutoSize = true;
            this.chkObaveznaNapomena.Location = new System.Drawing.Point(309, 185);
            this.chkObaveznaNapomena.Name = "chkObaveznaNapomena";
            this.chkObaveznaNapomena.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkObaveznaNapomena.Size = new System.Drawing.Size(128, 17);
            this.chkObaveznaNapomena.TabIndex = 165;
            this.chkObaveznaNapomena.Text = "Obavezna napomena";
            this.chkObaveznaNapomena.UseVisualStyleBackColor = true;
            // 
            // txtFiksniDeo
            // 
            this.txtFiksniDeo.DecimalPlaces = 2;
            this.txtFiksniDeo.Location = new System.Drawing.Point(253, 95);
            this.txtFiksniDeo.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtFiksniDeo.Name = "txtFiksniDeo";
            this.txtFiksniDeo.Size = new System.Drawing.Size(79, 20);
            this.txtFiksniDeo.TabIndex = 168;
            this.txtFiksniDeo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(339, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 167;
            this.label2.Text = "( EUR )";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 166;
            this.label3.Text = "Fiksni deo:";
            // 
            // chkSmederevo
            // 
            this.chkSmederevo.AutoSize = true;
            this.chkSmederevo.Location = new System.Drawing.Point(483, 147);
            this.chkSmederevo.Name = "chkSmederevo";
            this.chkSmederevo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkSmederevo.Size = new System.Drawing.Size(80, 17);
            this.chkSmederevo.TabIndex = 169;
            this.chkSmederevo.Text = "Smederevo";
            this.chkSmederevo.UseVisualStyleBackColor = true;
            // 
            // chkKragujevac
            // 
            this.chkKragujevac.AutoSize = true;
            this.chkKragujevac.Location = new System.Drawing.Point(583, 147);
            this.chkKragujevac.Name = "chkKragujevac";
            this.chkKragujevac.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkKragujevac.Size = new System.Drawing.Size(80, 17);
            this.chkKragujevac.TabIndex = 170;
            this.chkKragujevac.Text = "Kragujevac";
            this.chkKragujevac.UseVisualStyleBackColor = true;
            // 
            // chkCG
            // 
            this.chkCG.AutoSize = true;
            this.chkCG.Location = new System.Drawing.Point(681, 147);
            this.chkCG.Name = "chkCG";
            this.chkCG.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkCG.Size = new System.Drawing.Size(41, 17);
            this.chkCG.TabIndex = 171;
            this.chkCG.Text = "Ino";
            this.chkCG.UseVisualStyleBackColor = true;
            // 
            // chkRemont
            // 
            this.chkRemont.AutoSize = true;
            this.chkRemont.Location = new System.Drawing.Point(748, 147);
            this.chkRemont.Name = "chkRemont";
            this.chkRemont.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkRemont.Size = new System.Drawing.Size(63, 17);
            this.chkRemont.TabIndex = 172;
            this.chkRemont.Text = "Remont";
            this.chkRemont.UseVisualStyleBackColor = true;
            // 
            // chkMilsped
            // 
            this.chkMilsped.AutoSize = true;
            this.chkMilsped.Location = new System.Drawing.Point(820, 147);
            this.chkMilsped.Name = "chkMilsped";
            this.chkMilsped.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkMilsped.Size = new System.Drawing.Size(62, 17);
            this.chkMilsped.TabIndex = 173;
            this.chkMilsped.Text = "Milšped";
            this.chkMilsped.UseVisualStyleBackColor = true;
            // 
            // chkUlaziUDnevnicu
            // 
            this.chkUlaziUDnevnicu.AutoSize = true;
            this.chkUlaziUDnevnicu.Location = new System.Drawing.Point(332, 162);
            this.chkUlaziUDnevnicu.Name = "chkUlaziUDnevnicu";
            this.chkUlaziUDnevnicu.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkUlaziUDnevnicu.Size = new System.Drawing.Size(105, 17);
            this.chkUlaziUDnevnicu.TabIndex = 174;
            this.chkUlaziUDnevnicu.Text = "Ulazi u dnevnicu";
            this.chkUlaziUDnevnicu.UseVisualStyleBackColor = true;
            // 
            // txtVremeVagon
            // 
            this.txtVremeVagon.DecimalPlaces = 4;
            this.txtVremeVagon.Location = new System.Drawing.Point(88, 123);
            this.txtVremeVagon.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtVremeVagon.Name = "txtVremeVagon";
            this.txtVremeVagon.Size = new System.Drawing.Size(79, 20);
            this.txtVremeVagon.TabIndex = 176;
            this.txtVremeVagon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 175;
            this.label4.Text = "Vreme vagon:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(339, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 178;
            this.label5.Text = "MaxVagona:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(192, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 180;
            this.label6.Text = "MaxSati:";
            // 
            // txtMaxSati
            // 
            this.txtMaxSati.DecimalPlaces = 2;
            this.txtMaxSati.Location = new System.Drawing.Point(246, 222);
            this.txtMaxSati.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtMaxSati.Name = "txtMaxSati";
            this.txtMaxSati.Size = new System.Drawing.Size(79, 20);
            this.txtMaxSati.TabIndex = 181;
            this.txtMaxSati.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMaxVagona
            // 
            this.txtMaxVagona.DecimalPlaces = 2;
            this.txtMaxVagona.Location = new System.Drawing.Point(412, 224);
            this.txtMaxVagona.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtMaxVagona.Name = "txtMaxVagona";
            this.txtMaxVagona.Size = new System.Drawing.Size(79, 20);
            this.txtMaxVagona.TabIndex = 182;
            this.txtMaxVagona.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmVrsteAktivnosti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 405);
            this.Controls.Add(this.txtMaxVagona);
            this.Controls.Add(this.txtMaxSati);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtVremeVagon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkUlaziUDnevnicu);
            this.Controls.Add(this.chkMilsped);
            this.Controls.Add(this.chkRemont);
            this.Controls.Add(this.chkCG);
            this.Controls.Add(this.chkKragujevac);
            this.Controls.Add(this.chkSmederevo);
            this.Controls.Add(this.txtFiksniDeo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkObaveznaNapomena);
            this.Controls.Add(this.chkPotrebnoVozilo);
            this.Controls.Add(this.chkPotrebanNalogodavac);
            this.Controls.Add(this.chkPotrebanRazlog);
            this.Controls.Add(this.chkObracunPoSatu);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtOpis);
            this.Controls.Add(this.txtCena);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.lblNaziv);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVrsteAktivnosti";
            this.Text = "Vrste aktivnosti";
            this.Load += new System.EventHandler(this.frmVrsteAktivnosti_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCena)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFiksniDeo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVremeVagon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxSati)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxVagona)).EndInit();
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
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label lblNaziv;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtCena;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOpis;
        private System.Windows.Forms.CheckBox chkObracunPoSatu;
        private System.Windows.Forms.CheckBox chkPotrebanRazlog;
        private System.Windows.Forms.CheckBox chkPotrebanNalogodavac;
        private System.Windows.Forms.CheckBox chkPotrebnoVozilo;
        private System.Windows.Forms.CheckBox chkObaveznaNapomena;
        private System.Windows.Forms.NumericUpDown txtFiksniDeo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkSmederevo;
        private System.Windows.Forms.CheckBox chkKragujevac;
        private System.Windows.Forms.CheckBox chkCG;
        private System.Windows.Forms.CheckBox chkRemont;
        private System.Windows.Forms.CheckBox chkMilsped;
        private System.Windows.Forms.CheckBox chkUlaziUDnevnicu;
        private System.Windows.Forms.NumericUpDown txtVremeVagon;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown txtMaxSati;
        private System.Windows.Forms.NumericUpDown txtMaxVagona;
    }
}