namespace Saobracaj.TerminalMap
{
    partial class frmRN12InterniPrenos
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
            this.label8 = new System.Windows.Forms.Label();
            this.cboNaSklad = new System.Windows.Forms.ComboBox();
            this.cboNaPoz = new System.Windows.Forms.ComboBox();
            this.txtNAPozicijaskladista = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(12, 284);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 436;
            this.label8.Text = "NA Polje";
            // 
            // cboNaSklad
            // 
            this.cboNaSklad.FormattingEnabled = true;
            this.cboNaSklad.ItemHeight = 13;
            this.cboNaSklad.Location = new System.Drawing.Point(15, 299);
            this.cboNaSklad.Margin = new System.Windows.Forms.Padding(2);
            this.cboNaSklad.Name = "cboNaSklad";
            this.cboNaSklad.Size = new System.Drawing.Size(148, 21);
            this.cboNaSklad.TabIndex = 437;
            // 
            // cboNaPoz
            // 
            this.cboNaPoz.FormattingEnabled = true;
            this.cboNaPoz.ItemHeight = 13;
            this.cboNaPoz.Location = new System.Drawing.Point(12, 349);
            this.cboNaPoz.Margin = new System.Windows.Forms.Padding(2);
            this.cboNaPoz.Name = "cboNaPoz";
            this.cboNaPoz.Size = new System.Drawing.Size(150, 21);
            this.cboNaPoz.TabIndex = 439;
            this.cboNaPoz.Visible = false;
            // 
            // txtNAPozicijaskladista
            // 
            this.txtNAPozicijaskladista.AutoSize = true;
            this.txtNAPozicijaskladista.ForeColor = System.Drawing.Color.Black;
            this.txtNAPozicijaskladista.Location = new System.Drawing.Point(12, 334);
            this.txtNAPozicijaskladista.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtNAPozicijaskladista.Name = "txtNAPozicijaskladista";
            this.txtNAPozicijaskladista.Size = new System.Drawing.Size(105, 13);
            this.txtNAPozicijaskladista.TabIndex = 438;
            this.txtNAPozicijaskladista.Text = "NA Pozicija skladista";
            this.txtNAPozicijaskladista.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(11, 30);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(824, 221);
            this.dataGridView1.TabIndex = 440;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 441;
            this.label1.Text = "Kontejneri na polju";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(359, 284);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(84, 40);
            this.button4.TabIndex = 449;
            this.button4.Text = "Sva polja";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(449, 284);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 40);
            this.button1.TabIndex = 450;
            this.button1.Text = "Predefinisana polja";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(11, 389);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 40);
            this.button2.TabIndex = 451;
            this.button2.Text = "Formiraj interni prenos";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // frmRN12InterniPrenos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 493);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboNaSklad);
            this.Controls.Add(this.cboNaPoz);
            this.Controls.Add(this.txtNAPozicijaskladista);
            this.Name = "frmRN12InterniPrenos";
            this.Text = "RN12 - MEdjuskladisni interni prenos";
            this.Load += new System.EventHandler(this.frmRN12InterniPrenos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboNaSklad;
        private System.Windows.Forms.ComboBox cboNaPoz;
        private System.Windows.Forms.Label txtNAPozicijaskladista;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
    }
}