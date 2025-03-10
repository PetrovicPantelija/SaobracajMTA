
namespace Saobracaj.Sifarnici
{
    partial class frmDelavci
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDelavci));
            this.meniHeader = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.txtDeKrajBivS = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDeUlHisStBivS = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDeEMail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDeTelefon2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDeTelefon1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDeIme = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDeSifra = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDePriimek = new System.Windows.Forms.TextBox();
            this.lblNaziv = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDeSifStat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkManevrista = new System.Windows.Forms.CheckBox();
            this.chkPregledacKola = new System.Windows.Forms.CheckBox();
            this.chkVozovodja = new System.Windows.Forms.CheckBox();
            this.chkPomocnik = new System.Windows.Forms.CheckBox();
            this.chkMasinovodja = new System.Windows.Forms.CheckBox();
            this.txtDeSifDelMes = new System.Windows.Forms.ComboBox();
            this.txtERPID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button23 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
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
            this.toolStripButton1});
            this.meniHeader.Location = new System.Drawing.Point(0, 0);
            this.meniHeader.Name = "meniHeader";
            this.meniHeader.Size = new System.Drawing.Size(819, 27);
            this.meniHeader.TabIndex = 197;
            this.meniHeader.Text = "Osveži";
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
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.ForeColor = System.Drawing.Color.White;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(106, 24);
            this.toolStripButton1.Text = "SACUVAJ STAROG";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // txtDeKrajBivS
            // 
            this.txtDeKrajBivS.Location = new System.Drawing.Point(333, 183);
            this.txtDeKrajBivS.Name = "txtDeKrajBivS";
            this.txtDeKrajBivS.Size = new System.Drawing.Size(229, 20);
            this.txtDeKrajBivS.TabIndex = 215;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(335, 164);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 214;
            this.label10.Text = "Mesto";
            // 
            // txtDeUlHisStBivS
            // 
            this.txtDeUlHisStBivS.Location = new System.Drawing.Point(333, 139);
            this.txtDeUlHisStBivS.Name = "txtDeUlHisStBivS";
            this.txtDeUlHisStBivS.Size = new System.Drawing.Size(229, 20);
            this.txtDeUlHisStBivS.TabIndex = 213;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(332, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 212;
            this.label9.Text = "Adresa";
            // 
            // txtDeEMail
            // 
            this.txtDeEMail.Location = new System.Drawing.Point(333, 92);
            this.txtDeEMail.Name = "txtDeEMail";
            this.txtDeEMail.Size = new System.Drawing.Size(229, 20);
            this.txtDeEMail.TabIndex = 209;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(334, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 208;
            this.label7.Text = "E Mail";
            // 
            // txtDeTelefon2
            // 
            this.txtDeTelefon2.Location = new System.Drawing.Point(15, 275);
            this.txtDeTelefon2.Name = "txtDeTelefon2";
            this.txtDeTelefon2.Size = new System.Drawing.Size(231, 20);
            this.txtDeTelefon2.TabIndex = 207;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 256);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 206;
            this.label6.Text = "Telefon 2";
            // 
            // txtDeTelefon1
            // 
            this.txtDeTelefon1.Location = new System.Drawing.Point(15, 233);
            this.txtDeTelefon1.Name = "txtDeTelefon1";
            this.txtDeTelefon1.Size = new System.Drawing.Size(231, 20);
            this.txtDeTelefon1.TabIndex = 205;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 204;
            this.label5.Text = "Telefon 1";
            // 
            // txtDeIme
            // 
            this.txtDeIme.Location = new System.Drawing.Point(15, 186);
            this.txtDeIme.Name = "txtDeIme";
            this.txtDeIme.Size = new System.Drawing.Size(229, 20);
            this.txtDeIme.TabIndex = 203;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 202;
            this.label4.Text = "Ime";
            // 
            // txtDeSifra
            // 
            this.txtDeSifra.Location = new System.Drawing.Point(12, 92);
            this.txtDeSifra.Name = "txtDeSifra";
            this.txtDeSifra.Size = new System.Drawing.Size(87, 20);
            this.txtDeSifra.TabIndex = 201;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 200;
            this.label3.Text = "Šifra";
            // 
            // txtDePriimek
            // 
            this.txtDePriimek.Location = new System.Drawing.Point(13, 140);
            this.txtDePriimek.Name = "txtDePriimek";
            this.txtDePriimek.Size = new System.Drawing.Size(231, 20);
            this.txtDePriimek.TabIndex = 199;
            // 
            // lblNaziv
            // 
            this.lblNaziv.AutoSize = true;
            this.lblNaziv.Location = new System.Drawing.Point(12, 119);
            this.lblNaziv.Name = "lblNaziv";
            this.lblNaziv.Size = new System.Drawing.Size(44, 13);
            this.lblNaziv.TabIndex = 198;
            this.lblNaziv.Text = "Prezime";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 309);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(797, 132);
            this.dataGridView1.TabIndex = 216;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(334, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 217;
            this.label1.Text = "Radno mesto";
            // 
            // txtDeSifStat
            // 
            this.txtDeSifStat.Location = new System.Drawing.Point(333, 280);
            this.txtDeSifStat.Name = "txtDeSifStat";
            this.txtDeSifStat.Size = new System.Drawing.Size(87, 20);
            this.txtDeSifStat.TabIndex = 220;
            this.txtDeSifStat.Text = "A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(330, 260);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 219;
            this.label2.Text = "Status";
            // 
            // chkManevrista
            // 
            this.chkManevrista.AutoSize = true;
            this.chkManevrista.Location = new System.Drawing.Point(723, 194);
            this.chkManevrista.Name = "chkManevrista";
            this.chkManevrista.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkManevrista.Size = new System.Drawing.Size(78, 17);
            this.chkManevrista.TabIndex = 225;
            this.chkManevrista.Text = "Manevrista";
            this.chkManevrista.UseVisualStyleBackColor = true;
            // 
            // chkPregledacKola
            // 
            this.chkPregledacKola.AutoSize = true;
            this.chkPregledacKola.Location = new System.Drawing.Point(704, 166);
            this.chkPregledacKola.Name = "chkPregledacKola";
            this.chkPregledacKola.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkPregledacKola.Size = new System.Drawing.Size(97, 17);
            this.chkPregledacKola.TabIndex = 224;
            this.chkPregledacKola.Text = "Pregledač kola";
            this.chkPregledacKola.UseVisualStyleBackColor = true;
            // 
            // chkVozovodja
            // 
            this.chkVozovodja.AutoSize = true;
            this.chkVozovodja.Location = new System.Drawing.Point(726, 142);
            this.chkVozovodja.Name = "chkVozovodja";
            this.chkVozovodja.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkVozovodja.Size = new System.Drawing.Size(75, 17);
            this.chkVozovodja.TabIndex = 223;
            this.chkVozovodja.Text = "Vozovođa";
            this.chkVozovodja.UseVisualStyleBackColor = true;
            // 
            // chkPomocnik
            // 
            this.chkPomocnik.AutoSize = true;
            this.chkPomocnik.Location = new System.Drawing.Point(728, 119);
            this.chkPomocnik.Name = "chkPomocnik";
            this.chkPomocnik.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkPomocnik.Size = new System.Drawing.Size(73, 17);
            this.chkPomocnik.TabIndex = 222;
            this.chkPomocnik.Text = "Pomoćnik";
            this.chkPomocnik.UseVisualStyleBackColor = true;
            // 
            // chkMasinovodja
            // 
            this.chkMasinovodja.AutoSize = true;
            this.chkMasinovodja.Location = new System.Drawing.Point(717, 95);
            this.chkMasinovodja.Name = "chkMasinovodja";
            this.chkMasinovodja.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkMasinovodja.Size = new System.Drawing.Size(85, 17);
            this.chkMasinovodja.TabIndex = 221;
            this.chkMasinovodja.Text = "Mašinovođa";
            this.chkMasinovodja.UseVisualStyleBackColor = true;
            // 
            // txtDeSifDelMes
            // 
            this.txtDeSifDelMes.FormattingEnabled = true;
            this.txtDeSifDelMes.Location = new System.Drawing.Point(333, 231);
            this.txtDeSifDelMes.Margin = new System.Windows.Forms.Padding(4);
            this.txtDeSifDelMes.Name = "txtDeSifDelMes";
            this.txtDeSifDelMes.Size = new System.Drawing.Size(229, 21);
            this.txtDeSifDelMes.TabIndex = 226;
            // 
            // txtERPID
            // 
            this.txtERPID.Location = new System.Drawing.Point(444, 280);
            this.txtERPID.Name = "txtERPID";
            this.txtERPID.Size = new System.Drawing.Size(87, 20);
            this.txtERPID.TabIndex = 227;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(441, 260);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 228;
            this.label8.Text = "ERP ID";
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
            this.panelHeader.Size = new System.Drawing.Size(819, 33);
            this.panelHeader.TabIndex = 466;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.button23);
            this.panel2.Location = new System.Drawing.Point(103, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(345, 31);
            this.panel2.TabIndex = 7;
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
            this.button23.Size = new System.Drawing.Size(111, 31);
            this.button23.TabIndex = 16;
            this.button23.Text = "Sacuvaj starog";
            this.button23.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.toolStripButton1_Click);
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
            // frmDelavci
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(819, 453);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtERPID);
            this.Controls.Add(this.txtDeSifDelMes);
            this.Controls.Add(this.chkManevrista);
            this.Controls.Add(this.chkPregledacKola);
            this.Controls.Add(this.chkVozovodja);
            this.Controls.Add(this.chkPomocnik);
            this.Controls.Add(this.chkMasinovodja);
            this.Controls.Add(this.txtDeSifStat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtDeKrajBivS);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtDeUlHisStBivS);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDeEMail);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDeTelefon2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDeTelefon1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDeIme);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDeSifra);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDePriimek);
            this.Controls.Add(this.lblNaziv);
            this.Controls.Add(this.meniHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmDelavci";
            this.Text = "Zaposleni";
            this.Load += new System.EventHandler(this.frmDelavci_Load);
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
        private System.Windows.Forms.TextBox txtDeKrajBivS;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDeUlHisStBivS;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDeEMail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDeTelefon2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDeTelefon1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDeIme;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDeSifra;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDePriimek;
        private System.Windows.Forms.Label lblNaziv;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDeSifStat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkManevrista;
        private System.Windows.Forms.CheckBox chkPregledacKola;
        private System.Windows.Forms.CheckBox chkVozovodja;
        private System.Windows.Forms.CheckBox chkPomocnik;
        private System.Windows.Forms.CheckBox chkMasinovodja;
        private System.Windows.Forms.ComboBox txtDeSifDelMes;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TextBox txtERPID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button22;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
    }
}