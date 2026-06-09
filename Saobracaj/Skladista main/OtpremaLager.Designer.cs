namespace Saobracaj.Skladista_main
{
    partial class OtpremaLager
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboMB = new System.Windows.Forms.ComboBox();
            this.cboVlasnik = new System.Windows.Forms.ComboBox();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnUkoloni = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btnKreiraj = new System.Windows.Forms.Button();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtCarina = new System.Windows.Forms.TextBox();
            this.txtPDV = new System.Windows.Forms.TextBox();
            this.txtVrednost = new System.Windows.Forms.TextBox();
            this.txtPaleta = new System.Windows.Forms.TextBox();
            this.txtNeto = new System.Windows.Forms.TextBox();
            this.txtBruto = new System.Windows.Forms.TextBox();
            this.txtKoleta = new System.Windows.Forms.TextBox();
            this.txtArtikal = new System.Windows.Forms.TextBox();
            this.txtRB = new System.Windows.Forms.TextBox();
            this.btnIzbaciStavku = new System.Windows.Forms.Button();
            this.btnIzmeniStavku = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 78);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(449, 599);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pretrazi po magacinskom broju";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(269, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pretrazi po vlasniku";
            // 
            // cboMB
            // 
            this.cboMB.FormattingEnabled = true;
            this.cboMB.Location = new System.Drawing.Point(15, 28);
            this.cboMB.Name = "cboMB";
            this.cboMB.Size = new System.Drawing.Size(189, 21);
            this.cboMB.TabIndex = 2;
            this.cboMB.SelectionChangeCommitted += new System.EventHandler(this.cboMB_SelectionChangeCommitted);
            // 
            // cboVlasnik
            // 
            this.cboVlasnik.FormattingEnabled = true;
            this.cboVlasnik.Location = new System.Drawing.Point(272, 28);
            this.cboVlasnik.Name = "cboVlasnik";
            this.cboVlasnik.Size = new System.Drawing.Size(189, 21);
            this.cboVlasnik.TabIndex = 2;
            this.cboVlasnik.SelectionChangeCommitted += new System.EventHandler(this.cboVlasnik_SelectionChangeCommitted);
            // 
            // btnDodaj
            // 
            this.btnDodaj.BackColor = System.Drawing.Color.White;
            this.btnDodaj.Image = global::Saobracaj.Properties.Resources._0d828eb824813a7aea0e394aefad0a0a__2_;
            this.btnDodaj.Location = new System.Drawing.Point(467, 331);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(90, 56);
            this.btnDodaj.TabIndex = 3;
            this.btnDodaj.UseVisualStyleBackColor = false;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // btnUkoloni
            // 
            this.btnUkoloni.BackColor = System.Drawing.Color.White;
            this.btnUkoloni.Image = global::Saobracaj.Properties.Resources.e7c495b3543cd44c32878005d7c8e4ff__2_;
            this.btnUkoloni.Location = new System.Drawing.Point(467, 469);
            this.btnUkoloni.Name = "btnUkoloni";
            this.btnUkoloni.Size = new System.Drawing.Size(90, 56);
            this.btnUkoloni.TabIndex = 3;
            this.btnUkoloni.UseVisualStyleBackColor = false;
            this.btnUkoloni.Click += new System.EventHandler(this.btnUkoloni_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(563, 183);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(849, 494);
            this.dataGridView2.TabIndex = 0;
            // 
            // btnKreiraj
            // 
            this.btnKreiraj.BackColor = System.Drawing.Color.SteelBlue;
            this.btnKreiraj.ForeColor = System.Drawing.Color.White;
            this.btnKreiraj.Location = new System.Drawing.Point(1034, 23);
            this.btnKreiraj.Name = "btnKreiraj";
            this.btnKreiraj.Size = new System.Drawing.Size(131, 46);
            this.btnKreiraj.TabIndex = 4;
            this.btnKreiraj.Text = "KREIRAJ";
            this.btnKreiraj.UseVisualStyleBackColor = false;
            this.btnKreiraj.Click += new System.EventHandler(this.btnKreiraj_Click);
            // 
            // btnOdustani
            // 
            this.btnOdustani.BackColor = System.Drawing.Color.Red;
            this.btnOdustani.ForeColor = System.Drawing.Color.White;
            this.btnOdustani.Location = new System.Drawing.Point(1193, 23);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(131, 46);
            this.btnOdustani.TabIndex = 4;
            this.btnOdustani.Text = "ODUSTANI";
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.txtCarina);
            this.panel3.Controls.Add(this.txtPDV);
            this.panel3.Controls.Add(this.txtVrednost);
            this.panel3.Controls.Add(this.txtPaleta);
            this.panel3.Controls.Add(this.txtNeto);
            this.panel3.Controls.Add(this.txtBruto);
            this.panel3.Controls.Add(this.txtKoleta);
            this.panel3.Controls.Add(this.txtArtikal);
            this.panel3.Controls.Add(this.txtRB);
            this.panel3.Controls.Add(this.btnIzbaciStavku);
            this.panel3.Controls.Add(this.btnIzmeniStavku);
            this.panel3.Controls.Add(this.label28);
            this.panel3.Controls.Add(this.label24);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.label22);
            this.panel3.Controls.Add(this.label16);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Location = new System.Drawing.Point(563, 75);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(849, 102);
            this.panel3.TabIndex = 534;
            // 
            // txtCarina
            // 
            this.txtCarina.Location = new System.Drawing.Point(645, 70);
            this.txtCarina.Name = "txtCarina";
            this.txtCarina.Size = new System.Drawing.Size(81, 20);
            this.txtCarina.TabIndex = 2;
            // 
            // txtPDV
            // 
            this.txtPDV.Location = new System.Drawing.Point(645, 27);
            this.txtPDV.Name = "txtPDV";
            this.txtPDV.Size = new System.Drawing.Size(81, 20);
            this.txtPDV.TabIndex = 2;
            // 
            // txtVrednost
            // 
            this.txtVrednost.Location = new System.Drawing.Point(544, 27);
            this.txtVrednost.Name = "txtVrednost";
            this.txtVrednost.Size = new System.Drawing.Size(81, 20);
            this.txtVrednost.TabIndex = 2;
            // 
            // txtPaleta
            // 
            this.txtPaleta.Location = new System.Drawing.Point(447, 27);
            this.txtPaleta.Name = "txtPaleta";
            this.txtPaleta.Size = new System.Drawing.Size(81, 20);
            this.txtPaleta.TabIndex = 2;
            // 
            // txtNeto
            // 
            this.txtNeto.Location = new System.Drawing.Point(349, 70);
            this.txtNeto.Name = "txtNeto";
            this.txtNeto.Size = new System.Drawing.Size(81, 20);
            this.txtNeto.TabIndex = 2;
            // 
            // txtBruto
            // 
            this.txtBruto.Location = new System.Drawing.Point(349, 27);
            this.txtBruto.Name = "txtBruto";
            this.txtBruto.Size = new System.Drawing.Size(81, 20);
            this.txtBruto.TabIndex = 2;
            // 
            // txtKoleta
            // 
            this.txtKoleta.Location = new System.Drawing.Point(242, 27);
            this.txtKoleta.Name = "txtKoleta";
            this.txtKoleta.Size = new System.Drawing.Size(81, 20);
            this.txtKoleta.TabIndex = 2;
            // 
            // txtArtikal
            // 
            this.txtArtikal.Location = new System.Drawing.Point(61, 27);
            this.txtArtikal.Name = "txtArtikal";
            this.txtArtikal.Size = new System.Drawing.Size(163, 20);
            this.txtArtikal.TabIndex = 2;
            // 
            // txtRB
            // 
            this.txtRB.Location = new System.Drawing.Point(3, 27);
            this.txtRB.Name = "txtRB";
            this.txtRB.Size = new System.Drawing.Size(52, 20);
            this.txtRB.TabIndex = 2;
            // 
            // btnIzbaciStavku
            // 
            this.btnIzbaciStavku.BackColor = System.Drawing.Color.Red;
            this.btnIzbaciStavku.ForeColor = System.Drawing.Color.White;
            this.btnIzbaciStavku.Location = new System.Drawing.Point(756, 54);
            this.btnIzbaciStavku.Name = "btnIzbaciStavku";
            this.btnIzbaciStavku.Size = new System.Drawing.Size(75, 27);
            this.btnIzbaciStavku.TabIndex = 1;
            this.btnIzbaciStavku.Text = "Izbaci";
            this.btnIzbaciStavku.UseVisualStyleBackColor = false;
            this.btnIzbaciStavku.Click += new System.EventHandler(this.btnIzbaciStavku_Click);
            // 
            // btnIzmeniStavku
            // 
            this.btnIzmeniStavku.BackColor = System.Drawing.Color.SteelBlue;
            this.btnIzmeniStavku.ForeColor = System.Drawing.Color.White;
            this.btnIzmeniStavku.Location = new System.Drawing.Point(756, 20);
            this.btnIzmeniStavku.Name = "btnIzmeniStavku";
            this.btnIzmeniStavku.Size = new System.Drawing.Size(75, 27);
            this.btnIzmeniStavku.TabIndex = 1;
            this.btnIzmeniStavku.Text = "Izmeni";
            this.btnIzmeniStavku.UseVisualStyleBackColor = false;
            this.btnIzmeniStavku.Click += new System.EventHandler(this.btnIzmeniStavku_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(668, 54);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(37, 13);
            this.label28.TabIndex = 0;
            this.label28.Text = "Carina";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(676, 11);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(29, 13);
            this.label24.TabIndex = 0;
            this.label24.Text = "PDV";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(563, 11);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(49, 13);
            this.label23.TabIndex = 0;
            this.label23.Text = "Vrednost";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(466, 11);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(37, 13);
            this.label22.TabIndex = 0;
            this.label22.Text = "Paleta";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(371, 54);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(30, 13);
            this.label16.TabIndex = 0;
            this.label16.Text = "Neto";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(371, 11);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Bruto";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(267, 11);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Koleta";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(110, 11);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Artikal";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "RB";
            // 
            // OtpremaLager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1496, 689);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnKreiraj);
            this.Controls.Add(this.btnUkoloni);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.cboVlasnik);
            this.Controls.Add(this.cboMB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OtpremaLager";
            this.Text = "OtpremaLager";
            this.Load += new System.EventHandler(this.OtpremaLager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboMB;
        private System.Windows.Forms.ComboBox cboVlasnik;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnUkoloni;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnKreiraj;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtCarina;
        private System.Windows.Forms.TextBox txtPDV;
        private System.Windows.Forms.TextBox txtVrednost;
        private System.Windows.Forms.TextBox txtPaleta;
        private System.Windows.Forms.TextBox txtNeto;
        private System.Windows.Forms.TextBox txtBruto;
        private System.Windows.Forms.TextBox txtKoleta;
        private System.Windows.Forms.TextBox txtArtikal;
        private System.Windows.Forms.TextBox txtRB;
        private System.Windows.Forms.Button btnIzbaciStavku;
        private System.Windows.Forms.Button btnIzmeniStavku;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
    }
}