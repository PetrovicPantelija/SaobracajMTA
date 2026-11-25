namespace Saobracaj.Izvoz
{
    partial class frmIzvozUslugeNaloga
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
            this.pnlLevo = new System.Windows.Forms.Panel();
            this.pnlUsluge = new System.Windows.Forms.Panel();
            this.sfUsluge = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.lblUsluge = new System.Windows.Forms.Label();
            this.pnlRazmakLevo = new System.Windows.Forms.Panel();
            this.pnlKontejneri = new System.Windows.Forms.Panel();
            this.sfKontejneri = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.lblKontejneri = new System.Windows.Forms.Label();
            this.pnlRazmakGore = new System.Windows.Forms.Panel();
            this.pnlFilteri = new System.Windows.Forms.Panel();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnPoScenariju = new System.Windows.Forms.Button();
            this.cboScenario = new System.Windows.Forms.ComboBox();
            this.lblScenario = new System.Windows.Forms.Label();
            this.btnAdministrativne = new System.Windows.Forms.Button();
            this.btnPoGrupi = new System.Windows.Forms.Button();
            this.cboGrupa = new System.Windows.Forms.ComboBox();
            this.lblGrupa = new System.Windows.Forms.Label();
            this.btnPromeniPlatioca = new System.Windows.Forms.Button();
            this.cboPartner = new System.Windows.Forms.ComboBox();
            this.lblPartner = new System.Windows.Forms.Label();
            this.pnlSnimi = new System.Windows.Forms.Panel();
            this.btnSnimi = new System.Windows.Forms.Button();
            this.pnlDesno = new System.Windows.Forms.Panel();
            this.tlpUsluge = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTerminalske = new System.Windows.Forms.Panel();
            this.sfTerminalske = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.lblTerminalske = new System.Windows.Forms.Label();
            this.pnlDodatne = new System.Windows.Forms.Panel();
            this.sfDodatne = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.lblDodatne = new System.Windows.Forms.Label();
            this.pnlAdministrativne = new System.Windows.Forms.Panel();
            this.sfAdministrativne = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.lblAdministrativne = new System.Windows.Forms.Label();
            this.pnlLevo.SuspendLayout();
            this.pnlUsluge.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sfUsluge)).BeginInit();
            this.pnlKontejneri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sfKontejneri)).BeginInit();
            this.pnlFilteri.SuspendLayout();
            this.pnlSnimi.SuspendLayout();
            this.pnlDesno.SuspendLayout();
            this.tlpUsluge.SuspendLayout();
            this.pnlTerminalske.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sfTerminalske)).BeginInit();
            this.pnlDodatne.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sfDodatne)).BeginInit();
            this.pnlAdministrativne.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sfAdministrativne)).BeginInit();
            this.SuspendLayout();
            //
            // pnlLevo (filteri gore, ispod kontejneri naloga i katalog usluga)
            //
            this.pnlLevo.Controls.Add(this.pnlUsluge);
            this.pnlLevo.Controls.Add(this.pnlRazmakLevo);
            this.pnlLevo.Controls.Add(this.pnlKontejneri);
            this.pnlLevo.Controls.Add(this.pnlRazmakGore);
            this.pnlLevo.Controls.Add(this.pnlFilteri);
            this.pnlLevo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLevo.Location = new System.Drawing.Point(16, 16);
            this.pnlLevo.Name = "pnlLevo";
            this.pnlLevo.Size = new System.Drawing.Size(600, 828);
            this.pnlLevo.TabIndex = 0;
            //
            // pnlUsluge
            //
            this.pnlUsluge.Controls.Add(this.sfUsluge);
            this.pnlUsluge.Controls.Add(this.lblUsluge);
            this.pnlUsluge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUsluge.Location = new System.Drawing.Point(198, 192);
            this.pnlUsluge.Name = "pnlUsluge";
            this.pnlUsluge.Size = new System.Drawing.Size(402, 636);
            this.pnlUsluge.TabIndex = 4;
            //
            // sfUsluge
            //
            this.sfUsluge.AccessibleName = "Table";
            this.sfUsluge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sfUsluge.Location = new System.Drawing.Point(0, 24);
            this.sfUsluge.Name = "sfUsluge";
            this.sfUsluge.Size = new System.Drawing.Size(402, 612);
            this.sfUsluge.TabIndex = 1;
            this.sfUsluge.Text = "sfUsluge";
            //
            // lblUsluge
            //
            this.lblUsluge.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUsluge.Location = new System.Drawing.Point(0, 0);
            this.lblUsluge.Name = "lblUsluge";
            this.lblUsluge.Size = new System.Drawing.Size(402, 24);
            this.lblUsluge.TabIndex = 0;
            this.lblUsluge.Text = "Usluge";
            this.lblUsluge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlRazmakLevo
            //
            this.pnlRazmakLevo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlRazmakLevo.Location = new System.Drawing.Point(190, 192);
            this.pnlRazmakLevo.Name = "pnlRazmakLevo";
            this.pnlRazmakLevo.Size = new System.Drawing.Size(8, 636);
            this.pnlRazmakLevo.TabIndex = 3;
            //
            // pnlKontejneri
            //
            this.pnlKontejneri.Controls.Add(this.sfKontejneri);
            this.pnlKontejneri.Controls.Add(this.lblKontejneri);
            this.pnlKontejneri.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlKontejneri.Location = new System.Drawing.Point(0, 192);
            this.pnlKontejneri.Name = "pnlKontejneri";
            this.pnlKontejneri.Size = new System.Drawing.Size(190, 636);
            this.pnlKontejneri.TabIndex = 2;
            //
            // sfKontejneri
            //
            this.sfKontejneri.AccessibleName = "Table";
            this.sfKontejneri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sfKontejneri.Location = new System.Drawing.Point(0, 24);
            this.sfKontejneri.Name = "sfKontejneri";
            this.sfKontejneri.Size = new System.Drawing.Size(190, 612);
            this.sfKontejneri.TabIndex = 1;
            this.sfKontejneri.Text = "sfKontejneri";
            this.sfKontejneri.SelectionChanged += new Syncfusion.WinForms.DataGrid.Events.SelectionChangedEventHandler(this.sfKontejneri_SelectionChanged);
            //
            // lblKontejneri
            //
            this.lblKontejneri.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblKontejneri.Location = new System.Drawing.Point(0, 0);
            this.lblKontejneri.Name = "lblKontejneri";
            this.lblKontejneri.Size = new System.Drawing.Size(190, 24);
            this.lblKontejneri.TabIndex = 0;
            this.lblKontejneri.Text = "Kontejneri naloga";
            this.lblKontejneri.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlRazmakGore
            //
            this.pnlRazmakGore.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRazmakGore.Location = new System.Drawing.Point(0, 180);
            this.pnlRazmakGore.Name = "pnlRazmakGore";
            this.pnlRazmakGore.Size = new System.Drawing.Size(600, 12);
            this.pnlRazmakGore.TabIndex = 1;
            //
            // pnlFilteri (Partner, grupa usluga, scenario i dugmad)
            //
            this.pnlFilteri.BackColor = System.Drawing.Color.White;
            this.pnlFilteri.Controls.Add(this.btnObrisi);
            this.pnlFilteri.Controls.Add(this.btnPoScenariju);
            this.pnlFilteri.Controls.Add(this.cboScenario);
            this.pnlFilteri.Controls.Add(this.lblScenario);
            this.pnlFilteri.Controls.Add(this.btnAdministrativne);
            this.pnlFilteri.Controls.Add(this.btnPoGrupi);
            this.pnlFilteri.Controls.Add(this.cboGrupa);
            this.pnlFilteri.Controls.Add(this.lblGrupa);
            this.pnlFilteri.Controls.Add(this.btnPromeniPlatioca);
            this.pnlFilteri.Controls.Add(this.cboPartner);
            this.pnlFilteri.Controls.Add(this.lblPartner);
            this.pnlFilteri.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilteri.Location = new System.Drawing.Point(0, 0);
            this.pnlFilteri.Name = "pnlFilteri";
            this.pnlFilteri.Size = new System.Drawing.Size(600, 180);
            this.pnlFilteri.TabIndex = 0;
            //
            // lblPartner
            //
            this.lblPartner.AutoSize = true;
            this.lblPartner.Location = new System.Drawing.Point(14, 10);
            this.lblPartner.Name = "lblPartner";
            this.lblPartner.Size = new System.Drawing.Size(41, 13);
            this.lblPartner.TabIndex = 0;
            this.lblPartner.Text = "Partner";
            //
            // cboPartner
            //
            this.cboPartner.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPartner.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPartner.FormattingEnabled = true;
            this.cboPartner.Location = new System.Drawing.Point(14, 29);
            this.cboPartner.Name = "cboPartner";
            this.cboPartner.Size = new System.Drawing.Size(290, 21);
            this.cboPartner.TabIndex = 1;
            //
            // btnPromeniPlatioca
            //
            this.btnPromeniPlatioca.Location = new System.Drawing.Point(314, 25);
            this.btnPromeniPlatioca.Name = "btnPromeniPlatioca";
            this.btnPromeniPlatioca.Size = new System.Drawing.Size(150, 32);
            this.btnPromeniPlatioca.TabIndex = 2;
            this.btnPromeniPlatioca.Text = "Promeni platioca";
            this.btnPromeniPlatioca.Click += new System.EventHandler(this.btnPromeniPlatioca_Click);
            //
            // lblGrupa
            //
            this.lblGrupa.AutoSize = true;
            this.lblGrupa.Location = new System.Drawing.Point(14, 66);
            this.lblGrupa.Name = "lblGrupa";
            this.lblGrupa.Size = new System.Drawing.Size(70, 13);
            this.lblGrupa.TabIndex = 3;
            this.lblGrupa.Text = "Grupa usluga";
            //
            // cboGrupa
            //
            this.cboGrupa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGrupa.FormattingEnabled = true;
            this.cboGrupa.Location = new System.Drawing.Point(14, 85);
            this.cboGrupa.Name = "cboGrupa";
            this.cboGrupa.Size = new System.Drawing.Size(290, 21);
            this.cboGrupa.TabIndex = 4;
            //
            // btnPoGrupi
            //
            this.btnPoGrupi.Location = new System.Drawing.Point(314, 81);
            this.btnPoGrupi.Name = "btnPoGrupi";
            this.btnPoGrupi.Size = new System.Drawing.Size(116, 32);
            this.btnPoGrupi.TabIndex = 5;
            this.btnPoGrupi.Text = "Po grupi";
            this.btnPoGrupi.Click += new System.EventHandler(this.btnPoGrupi_Click);
            //
            // btnAdministrativne
            //
            this.btnAdministrativne.Location = new System.Drawing.Point(438, 81);
            this.btnAdministrativne.Name = "btnAdministrativne";
            this.btnAdministrativne.Size = new System.Drawing.Size(150, 32);
            this.btnAdministrativne.TabIndex = 6;
            this.btnAdministrativne.Text = "Administrativne";
            this.btnAdministrativne.Click += new System.EventHandler(this.btnAdministrativne_Click);
            //
            // lblScenario
            //
            this.lblScenario.AutoSize = true;
            this.lblScenario.Location = new System.Drawing.Point(14, 122);
            this.lblScenario.Name = "lblScenario";
            this.lblScenario.Size = new System.Drawing.Size(49, 13);
            this.lblScenario.TabIndex = 7;
            this.lblScenario.Text = "Scenario";
            //
            // cboScenario
            //
            this.cboScenario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScenario.FormattingEnabled = true;
            this.cboScenario.Location = new System.Drawing.Point(14, 141);
            this.cboScenario.Name = "cboScenario";
            this.cboScenario.Size = new System.Drawing.Size(290, 21);
            this.cboScenario.TabIndex = 8;
            //
            // btnPoScenariju
            //
            this.btnPoScenariju.Location = new System.Drawing.Point(314, 137);
            this.btnPoScenariju.Name = "btnPoScenariju";
            this.btnPoScenariju.Size = new System.Drawing.Size(116, 32);
            this.btnPoScenariju.TabIndex = 9;
            this.btnPoScenariju.Text = "Po scenariju";
            this.btnPoScenariju.Click += new System.EventHandler(this.btnPoScenariju_Click);
            //
            // btnObrisi (briše označene usluge iz tabela desno)
            //
            this.btnObrisi.Location = new System.Drawing.Point(438, 137);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(150, 32);
            this.btnObrisi.TabIndex = 10;
            this.btnObrisi.Tag = "danger";
            this.btnObrisi.Text = "Obriši selektovane";
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            //
            // pnlSnimi
            //
            this.pnlSnimi.Controls.Add(this.btnSnimi);
            this.pnlSnimi.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSnimi.Location = new System.Drawing.Point(616, 16);
            this.pnlSnimi.Name = "pnlSnimi";
            this.pnlSnimi.Padding = new System.Windows.Forms.Padding(10, 216, 10, 0);
            this.pnlSnimi.Size = new System.Drawing.Size(84, 828);
            this.pnlSnimi.TabIndex = 1;
            //
            // btnSnimi (označene usluge za označene kontejnere)
            //
            this.btnSnimi.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSnimi.Location = new System.Drawing.Point(10, 216);
            this.btnSnimi.Name = "btnSnimi";
            this.btnSnimi.Size = new System.Drawing.Size(64, 180);
            this.btnSnimi.TabIndex = 0;
            this.btnSnimi.Tag = "primary";
            this.btnSnimi.Text = "Snimi\r\n»";
            this.btnSnimi.Click += new System.EventHandler(this.btnSnimi_Click);
            //
            // pnlDesno
            //
            this.pnlDesno.Controls.Add(this.tlpUsluge);
            this.pnlDesno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDesno.Location = new System.Drawing.Point(700, 16);
            this.pnlDesno.Name = "pnlDesno";
            this.pnlDesno.Size = new System.Drawing.Size(668, 828);
            this.pnlDesno.TabIndex = 2;
            //
            // tlpUsluge (Terminalske gore, ispod Dodatne i Administrativne; menjaju veličinu sa prozorom)
            //
            this.tlpUsluge.ColumnCount = 1;
            this.tlpUsluge.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpUsluge.Controls.Add(this.pnlTerminalske, 0, 0);
            this.tlpUsluge.Controls.Add(this.pnlDodatne, 0, 1);
            this.tlpUsluge.Controls.Add(this.pnlAdministrativne, 0, 2);
            this.tlpUsluge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpUsluge.Location = new System.Drawing.Point(0, 0);
            this.tlpUsluge.Name = "tlpUsluge";
            this.tlpUsluge.RowCount = 3;
            this.tlpUsluge.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpUsluge.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpUsluge.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpUsluge.Size = new System.Drawing.Size(668, 828);
            this.tlpUsluge.TabIndex = 0;
            //
            // pnlTerminalske
            //
            this.pnlTerminalske.Controls.Add(this.sfTerminalske);
            this.pnlTerminalske.Controls.Add(this.lblTerminalske);
            this.pnlTerminalske.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTerminalske.Location = new System.Drawing.Point(0, 0);
            this.pnlTerminalske.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.pnlTerminalske.Name = "pnlTerminalske";
            this.pnlTerminalske.Size = new System.Drawing.Size(668, 321);
            this.pnlTerminalske.TabIndex = 0;
            //
            // sfTerminalske
            //
            this.sfTerminalske.AccessibleName = "Table";
            this.sfTerminalske.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sfTerminalske.Location = new System.Drawing.Point(0, 24);
            this.sfTerminalske.Name = "sfTerminalske";
            this.sfTerminalske.Size = new System.Drawing.Size(668, 297);
            this.sfTerminalske.TabIndex = 1;
            this.sfTerminalske.Text = "sfTerminalske";
            this.sfTerminalske.CurrentCellEndEdit += new Syncfusion.WinForms.DataGrid.Events.CurrentCellEndEditEventHandler(this.sfTerminalske_CurrentCellEndEdit);
            this.sfTerminalske.CellCheckBoxClick += new Syncfusion.WinForms.DataGrid.Events.CellCheckBoxClickEventHandler(this.sfTerminalske_CellCheckBoxClick);
            //
            // lblTerminalske
            //
            this.lblTerminalske.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTerminalske.Location = new System.Drawing.Point(0, 0);
            this.lblTerminalske.Name = "lblTerminalske";
            this.lblTerminalske.Size = new System.Drawing.Size(668, 24);
            this.lblTerminalske.TabIndex = 0;
            this.lblTerminalske.Text = "Terminalske usluge";
            this.lblTerminalske.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlDodatne
            //
            this.pnlDodatne.Controls.Add(this.sfDodatne);
            this.pnlDodatne.Controls.Add(this.lblDodatne);
            this.pnlDodatne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDodatne.Location = new System.Drawing.Point(0, 331);
            this.pnlDodatne.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.pnlDodatne.Name = "pnlDodatne";
            this.pnlDodatne.Size = new System.Drawing.Size(668, 238);
            this.pnlDodatne.TabIndex = 1;
            //
            // sfDodatne
            //
            this.sfDodatne.AccessibleName = "Table";
            this.sfDodatne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sfDodatne.Location = new System.Drawing.Point(0, 24);
            this.sfDodatne.Name = "sfDodatne";
            this.sfDodatne.Size = new System.Drawing.Size(668, 214);
            this.sfDodatne.TabIndex = 1;
            this.sfDodatne.Text = "sfDodatne";
            //
            // lblDodatne
            //
            this.lblDodatne.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDodatne.Location = new System.Drawing.Point(0, 0);
            this.lblDodatne.Name = "lblDodatne";
            this.lblDodatne.Size = new System.Drawing.Size(668, 24);
            this.lblDodatne.TabIndex = 0;
            this.lblDodatne.Text = "Dodatne usluge";
            this.lblDodatne.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlAdministrativne
            //
            this.pnlAdministrativne.Controls.Add(this.sfAdministrativne);
            this.pnlAdministrativne.Controls.Add(this.lblAdministrativne);
            this.pnlAdministrativne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAdministrativne.Location = new System.Drawing.Point(0, 579);
            this.pnlAdministrativne.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAdministrativne.Name = "pnlAdministrativne";
            this.pnlAdministrativne.Size = new System.Drawing.Size(668, 249);
            this.pnlAdministrativne.TabIndex = 2;
            //
            // sfAdministrativne
            //
            this.sfAdministrativne.AccessibleName = "Table";
            this.sfAdministrativne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sfAdministrativne.Location = new System.Drawing.Point(0, 24);
            this.sfAdministrativne.Name = "sfAdministrativne";
            this.sfAdministrativne.Size = new System.Drawing.Size(668, 225);
            this.sfAdministrativne.TabIndex = 1;
            this.sfAdministrativne.Text = "sfAdministrativne";
            //
            // lblAdministrativne
            //
            this.lblAdministrativne.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAdministrativne.Location = new System.Drawing.Point(0, 0);
            this.lblAdministrativne.Name = "lblAdministrativne";
            this.lblAdministrativne.Size = new System.Drawing.Size(668, 24);
            this.lblAdministrativne.TabIndex = 0;
            this.lblAdministrativne.Text = "Administrativne usluge";
            this.lblAdministrativne.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // frmIzvozUslugeNaloga
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 860);
            this.Controls.Add(this.pnlDesno);
            this.Controls.Add(this.pnlSnimi);
            this.Controls.Add(this.pnlLevo);
            this.MinimumSize = new System.Drawing.Size(1100, 650);
            this.Name = "frmIzvozUslugeNaloga";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unos usluge izvoz";
            this.Load += new System.EventHandler(this.frmIzvozUslugeNaloga_Load);
            this.Shown += new System.EventHandler(this.frmIzvozUslugeNaloga_Shown);
            this.pnlLevo.ResumeLayout(false);
            this.pnlUsluge.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sfUsluge)).EndInit();
            this.pnlKontejneri.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sfKontejneri)).EndInit();
            this.pnlFilteri.ResumeLayout(false);
            this.pnlFilteri.PerformLayout();
            this.pnlSnimi.ResumeLayout(false);
            this.pnlDesno.ResumeLayout(false);
            this.tlpUsluge.ResumeLayout(false);
            this.pnlTerminalske.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sfTerminalske)).EndInit();
            this.pnlDodatne.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sfDodatne)).EndInit();
            this.pnlAdministrativne.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sfAdministrativne)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLevo;
        private System.Windows.Forms.Panel pnlFilteri;
        private System.Windows.Forms.Label lblPartner;
        private System.Windows.Forms.ComboBox cboPartner;
        private System.Windows.Forms.Button btnPromeniPlatioca;
        private System.Windows.Forms.Label lblGrupa;
        private System.Windows.Forms.ComboBox cboGrupa;
        private System.Windows.Forms.Button btnPoGrupi;
        private System.Windows.Forms.Button btnAdministrativne;
        private System.Windows.Forms.Label lblScenario;
        private System.Windows.Forms.ComboBox cboScenario;
        private System.Windows.Forms.Button btnPoScenariju;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Panel pnlRazmakGore;
        private System.Windows.Forms.Panel pnlKontejneri;
        private System.Windows.Forms.Label lblKontejneri;
        private Syncfusion.WinForms.DataGrid.SfDataGrid sfKontejneri;
        private System.Windows.Forms.Panel pnlRazmakLevo;
        private System.Windows.Forms.Panel pnlUsluge;
        private System.Windows.Forms.Label lblUsluge;
        private Syncfusion.WinForms.DataGrid.SfDataGrid sfUsluge;
        private System.Windows.Forms.Panel pnlSnimi;
        private System.Windows.Forms.Button btnSnimi;
        private System.Windows.Forms.Panel pnlDesno;
        private System.Windows.Forms.TableLayoutPanel tlpUsluge;
        private System.Windows.Forms.Panel pnlTerminalske;
        private System.Windows.Forms.Label lblTerminalske;
        private Syncfusion.WinForms.DataGrid.SfDataGrid sfTerminalske;
        private System.Windows.Forms.Panel pnlDodatne;
        private System.Windows.Forms.Label lblDodatne;
        private Syncfusion.WinForms.DataGrid.SfDataGrid sfDodatne;
        private System.Windows.Forms.Panel pnlAdministrativne;
        private System.Windows.Forms.Label lblAdministrativne;
        private Syncfusion.WinForms.DataGrid.SfDataGrid sfAdministrativne;
    }
}
