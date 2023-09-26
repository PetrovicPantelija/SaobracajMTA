namespace Saobracaj.eDokumenta
{
    partial class PrimljenaDokumenta
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
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btn_Datum = new System.Windows.Forms.Button();
            this.btn_Ospori = new System.Windows.Forms.Button();
            this.btn_Preuzmi = new System.Windows.Forms.Button();
            this.btn_Status = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbo_Status = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Ospori = new System.Windows.Forms.TextBox();
            this.txt_Response = new System.Windows.Forms.TextBox();
            this.txt_EID = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(967, 24);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(180, 20);
            this.dateTimePicker2.TabIndex = 34;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(750, 24);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(180, 20);
            this.dateTimePicker1.TabIndex = 35;
            // 
            // btn_Datum
            // 
            this.btn_Datum.Location = new System.Drawing.Point(1036, 48);
            this.btn_Datum.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Datum.Name = "btn_Datum";
            this.btn_Datum.Size = new System.Drawing.Size(111, 26);
            this.btn_Datum.TabIndex = 30;
            this.btn_Datum.Text = "Pretrazi po datumu";
            this.btn_Datum.UseVisualStyleBackColor = true;
            this.btn_Datum.Click += new System.EventHandler(this.btn_Datum_Click);
            // 
            // btn_Ospori
            // 
            this.btn_Ospori.Location = new System.Drawing.Point(431, 52);
            this.btn_Ospori.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Ospori.Name = "btn_Ospori";
            this.btn_Ospori.Size = new System.Drawing.Size(111, 26);
            this.btn_Ospori.TabIndex = 31;
            this.btn_Ospori.Text = "Ospori";
            this.btn_Ospori.UseVisualStyleBackColor = true;
            this.btn_Ospori.Click += new System.EventHandler(this.btn_Ospori_Click);
            // 
            // btn_Preuzmi
            // 
            this.btn_Preuzmi.Location = new System.Drawing.Point(11, 63);
            this.btn_Preuzmi.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Preuzmi.Name = "btn_Preuzmi";
            this.btn_Preuzmi.Size = new System.Drawing.Size(111, 26);
            this.btn_Preuzmi.TabIndex = 32;
            this.btn_Preuzmi.Text = "Preuzmi";
            this.btn_Preuzmi.UseVisualStyleBackColor = true;
            this.btn_Preuzmi.Click += new System.EventHandler(this.btn_Preuzmi_Click);
            // 
            // btn_Status
            // 
            this.btn_Status.Location = new System.Drawing.Point(592, 19);
            this.btn_Status.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Status.Name = "btn_Status";
            this.btn_Status.Size = new System.Drawing.Size(111, 26);
            this.btn_Status.TabIndex = 33;
            this.btn_Status.Text = "Pretrazi po statusu";
            this.btn_Status.UseVisualStyleBackColor = true;
            this.btn_Status.Click += new System.EventHandler(this.btn_Status_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(964, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Do:";
            // 
            // cbo_Status
            // 
            this.cbo_Status.FormattingEnabled = true;
            this.cbo_Status.Location = new System.Drawing.Point(431, 24);
            this.cbo_Status.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_Status.Name = "cbo_Status";
            this.cbo_Status.Size = new System.Drawing.Size(146, 21);
            this.cbo_Status.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(747, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Od:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(428, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Status:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Response:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Electronic ID";
            // 
            // txt_Ospori
            // 
            this.txt_Ospori.Location = new System.Drawing.Point(431, 82);
            this.txt_Ospori.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Ospori.Multiline = true;
            this.txt_Ospori.Name = "txt_Ospori";
            this.txt_Ospori.Size = new System.Drawing.Size(476, 67);
            this.txt_Ospori.TabIndex = 21;
            // 
            // txt_Response
            // 
            this.txt_Response.Location = new System.Drawing.Point(177, 9);
            this.txt_Response.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Response.Multiline = true;
            this.txt_Response.Name = "txt_Response";
            this.txt_Response.Size = new System.Drawing.Size(226, 140);
            this.txt_Response.TabIndex = 22;
            // 
            // txt_EID
            // 
            this.txt_EID.Location = new System.Drawing.Point(11, 29);
            this.txt_EID.Margin = new System.Windows.Forms.Padding(2);
            this.txt_EID.Name = "txt_EID";
            this.txt_EID.Size = new System.Drawing.Size(76, 20);
            this.txt_EID.TabIndex = 23;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(11, 153);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1384, 585);
            this.dataGridView1.TabIndex = 20;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // PrimljenaDokumenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1406, 749);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btn_Datum);
            this.Controls.Add(this.btn_Ospori);
            this.Controls.Add(this.btn_Preuzmi);
            this.Controls.Add(this.btn_Status);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbo_Status);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Ospori);
            this.Controls.Add(this.txt_Response);
            this.Controls.Add(this.txt_EID);
            this.Controls.Add(this.dataGridView1);
            this.Name = "PrimljenaDokumenta";
            this.Text = "PrimljenaDokumenta";
            this.Load += new System.EventHandler(this.PrimljenaDokumenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btn_Datum;
        private System.Windows.Forms.Button btn_Ospori;
        private System.Windows.Forms.Button btn_Preuzmi;
        private System.Windows.Forms.Button btn_Status;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbo_Status;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Ospori;
        private System.Windows.Forms.TextBox txt_Response;
        private System.Windows.Forms.TextBox txt_EID;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}