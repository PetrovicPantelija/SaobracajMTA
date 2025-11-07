namespace Saobracaj.Drumski
{
    partial class frmPodesavanjeRaspolozivosti
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
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.cboPrevoznikFilter = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFiltriraj = new System.Windows.Forms.Button();
            this.lblVremeSlanjaSMS = new System.Windows.Forms.Label();
            this.lblPrevoznik = new System.Windows.Forms.Label();
            this.lblTelefon = new System.Windows.Forms.Label();
            this.lblVozac = new System.Windows.Forms.Label();
            this.lblBrojNaloga = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.button1);
            this.panel6.Controls.Add(this.cboPrevoznikFilter);
            this.panel6.Controls.Add(this.button2);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Controls.Add(this.btnFiltriraj);
            this.panel6.Controls.Add(this.lblVremeSlanjaSMS);
            this.panel6.Controls.Add(this.lblPrevoznik);
            this.panel6.Controls.Add(this.lblTelefon);
            this.panel6.Controls.Add(this.lblVozac);
            this.panel6.Controls.Add(this.lblBrojNaloga);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1188, 47);
            this.panel6.TabIndex = 726;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(945, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(235, 38);
            this.button1.TabIndex = 14;
            this.button1.Text = "VRATI U RASPOLOŽIVE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cboPrevoznikFilter
            // 
            this.cboPrevoznikFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrevoznikFilter.Location = new System.Drawing.Point(94, 15);
            this.cboPrevoznikFilter.Name = "cboPrevoznikFilter";
            this.cboPrevoznikFilter.Size = new System.Drawing.Size(200, 24);
            this.cboPrevoznikFilter.TabIndex = 12;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(704, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(235, 38);
            this.button2.TabIndex = 13;
            this.button2.Text = "OZNAČI KAO NERASPOLOŽIVE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Prevoznik";
            // 
            // btnFiltriraj
            // 
            this.btnFiltriraj.Location = new System.Drawing.Point(313, 16);
            this.btnFiltriraj.Name = "btnFiltriraj";
            this.btnFiltriraj.Size = new System.Drawing.Size(74, 23);
            this.btnFiltriraj.TabIndex = 10;
            this.btnFiltriraj.Text = "Filtriraj";
            // 
            // lblVremeSlanjaSMS
            // 
            this.lblVremeSlanjaSMS.AutoSize = true;
            this.lblVremeSlanjaSMS.Location = new System.Drawing.Point(623, 57);
            this.lblVremeSlanjaSMS.Name = "lblVremeSlanjaSMS";
            this.lblVremeSlanjaSMS.Size = new System.Drawing.Size(0, 16);
            this.lblVremeSlanjaSMS.TabIndex = 9;
            // 
            // lblPrevoznik
            // 
            this.lblPrevoznik.AutoSize = true;
            this.lblPrevoznik.Location = new System.Drawing.Point(469, 57);
            this.lblPrevoznik.Name = "lblPrevoznik";
            this.lblPrevoznik.Size = new System.Drawing.Size(0, 16);
            this.lblPrevoznik.TabIndex = 7;
            // 
            // lblTelefon
            // 
            this.lblTelefon.AutoSize = true;
            this.lblTelefon.Location = new System.Drawing.Point(320, 57);
            this.lblTelefon.Name = "lblTelefon";
            this.lblTelefon.Size = new System.Drawing.Size(0, 16);
            this.lblTelefon.TabIndex = 5;
            // 
            // lblVozac
            // 
            this.lblVozac.AutoSize = true;
            this.lblVozac.Location = new System.Drawing.Point(142, 57);
            this.lblVozac.Name = "lblVozac";
            this.lblVozac.Size = new System.Drawing.Size(0, 16);
            this.lblVozac.TabIndex = 3;
            // 
            // lblBrojNaloga
            // 
            this.lblBrojNaloga.AutoSize = true;
            this.lblBrojNaloga.Location = new System.Drawing.Point(17, 57);
            this.lblBrojNaloga.Name = "lblBrojNaloga";
            this.lblBrojNaloga.Size = new System.Drawing.Size(0, 16);
            this.lblBrojNaloga.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 51);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1180, 545);
            this.dataGridView1.TabIndex = 725;
            // 
            // frmPodesavanjeRaspolozivosti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 604);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmPodesavanjeRaspolozivosti";
            this.Text = "frmPodesavanjeRaspolozivosti";
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ComboBox cboPrevoznikFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFiltriraj;
        private System.Windows.Forms.Label lblVremeSlanjaSMS;
        private System.Windows.Forms.Label lblPrevoznik;
        private System.Windows.Forms.Label lblTelefon;
        private System.Windows.Forms.Label lblVozac;
        private System.Windows.Forms.Label lblBrojNaloga;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}