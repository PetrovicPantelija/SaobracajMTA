
namespace Saobracaj.Dokumenta
{
    partial class frmObracunFiksni
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
            this.txtCenaSata = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBrojSati = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPostaviPrviDeo = new MetroFramework.Controls.MetroButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label15 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.dtpVremeOd = new System.Windows.Forms.DateTimePicker();
            this.dtpVremeDo = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.txtCenaSata)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBrojSati)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCenaSata
            // 
            this.txtCenaSata.DecimalPlaces = 2;
            this.txtCenaSata.Location = new System.Drawing.Point(319, 28);
            this.txtCenaSata.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtCenaSata.Name = "txtCenaSata";
            this.txtCenaSata.Size = new System.Drawing.Size(72, 20);
            this.txtCenaSata.TabIndex = 151;
            this.txtCenaSata.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 150;
            this.label1.Text = "Cena sata:";
            // 
            // txtBrojSati
            // 
            this.txtBrojSati.DecimalPlaces = 2;
            this.txtBrojSati.Location = new System.Drawing.Point(134, 28);
            this.txtBrojSati.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtBrojSati.Name = "txtBrojSati";
            this.txtBrojSati.Size = new System.Drawing.Size(98, 20);
            this.txtBrojSati.TabIndex = 149;
            this.txtBrojSati.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 148;
            this.label2.Text = "Broj mesečnih sati:";
            // 
            // btnPostaviPrviDeo
            // 
            this.btnPostaviPrviDeo.Location = new System.Drawing.Point(27, 67);
            this.btnPostaviPrviDeo.Name = "btnPostaviPrviDeo";
            this.btnPostaviPrviDeo.Size = new System.Drawing.Size(159, 23);
            this.btnPostaviPrviDeo.TabIndex = 152;
            this.btnPostaviPrviDeo.Text = "Povuci podatke";
            this.btnPostaviPrviDeo.UseSelectable = true;
            this.btnPostaviPrviDeo.Click += new System.EventHandler(this.btnPostaviPrviDeo_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(5, 114);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1131, 410);
            this.dataGridView1.TabIndex = 153;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(589, 29);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 13);
            this.label15.TabIndex = 209;
            this.label15.Text = "Period do:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(414, 29);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(55, 13);
            this.label21.TabIndex = 208;
            this.label21.Text = "Period od:";
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(258, 67);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(159, 23);
            this.metroButton2.TabIndex = 210;
            this.metroButton2.Text = "Povuci sate";
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // dtpVremeOd
            // 
            this.dtpVremeOd.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpVremeOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVremeOd.Location = new System.Drawing.Point(475, 28);
            this.dtpVremeOd.Name = "dtpVremeOd";
            this.dtpVremeOd.ShowUpDown = true;
            this.dtpVremeOd.Size = new System.Drawing.Size(110, 20);
            this.dtpVremeOd.TabIndex = 215;
            this.dtpVremeOd.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // dtpVremeDo
            // 
            this.dtpVremeDo.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpVremeDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVremeDo.Location = new System.Drawing.Point(650, 28);
            this.dtpVremeDo.Name = "dtpVremeDo";
            this.dtpVremeDo.ShowUpDown = true;
            this.dtpVremeDo.Size = new System.Drawing.Size(110, 20);
            this.dtpVremeDo.TabIndex = 214;
            this.dtpVremeDo.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // frmObracunFiksni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 527);
            this.Controls.Add(this.dtpVremeOd);
            this.Controls.Add(this.dtpVremeDo);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnPostaviPrviDeo);
            this.Controls.Add(this.txtCenaSata);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBrojSati);
            this.Controls.Add(this.label2);
            this.Name = "frmObracunFiksni";
            this.Text = "Obračun zarade";
            ((System.ComponentModel.ISupportInitialize)(this.txtCenaSata)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBrojSati)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown txtCenaSata;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtBrojSati;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroButton btnPostaviPrviDeo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label21;
        private MetroFramework.Controls.MetroButton metroButton2;
        private System.Windows.Forms.DateTimePicker dtpVremeOd;
        private System.Windows.Forms.DateTimePicker dtpVremeDo;
    }
}