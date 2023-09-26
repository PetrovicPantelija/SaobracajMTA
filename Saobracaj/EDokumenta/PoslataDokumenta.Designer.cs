namespace Saobracaj.eDokumenta
{
    partial class PoslataDokumenta
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txt_Opoziv = new System.Windows.Forms.TextBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btn_Datum = new System.Windows.Forms.Button();
            this.btn_Status = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbo_Status = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Response = new System.Windows.Forms.TextBox();
            this.txt_EID = new System.Windows.Forms.TextBox();
            this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_Opoziv
            // 
            this.txt_Opoziv.Location = new System.Drawing.Point(408, 62);
            this.txt_Opoziv.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Opoziv.Multiline = true;
            this.txt_Opoziv.Name = "txt_Opoziv";
            this.txt_Opoziv.Size = new System.Drawing.Size(352, 71);
            this.txt_Opoziv.TabIndex = 23;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(952, 30);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(180, 20);
            this.dateTimePicker2.TabIndex = 21;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(739, 30);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(180, 20);
            this.dateTimePicker1.TabIndex = 22;
            // 
            // btn_Datum
            // 
            this.btn_Datum.Location = new System.Drawing.Point(1021, 54);
            this.btn_Datum.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Datum.Name = "btn_Datum";
            this.btn_Datum.Size = new System.Drawing.Size(111, 26);
            this.btn_Datum.TabIndex = 19;
            this.btn_Datum.Text = "Pretrazi po datumu";
            this.btn_Datum.UseVisualStyleBackColor = true;
            this.btn_Datum.Click += new System.EventHandler(this.btn_Datum_Click);
            // 
            // btn_Status
            // 
            this.btn_Status.Location = new System.Drawing.Point(567, 19);
            this.btn_Status.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Status.Name = "btn_Status";
            this.btn_Status.Size = new System.Drawing.Size(111, 26);
            this.btn_Status.TabIndex = 20;
            this.btn_Status.Text = "Pretrazi po statusu";
            this.btn_Status.UseVisualStyleBackColor = true;
            this.btn_Status.Click += new System.EventHandler(this.btn_Status_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(949, 14);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Do:";
            // 
            // cbo_Status
            // 
            this.cbo_Status.FormattingEnabled = true;
            this.cbo_Status.Location = new System.Drawing.Point(408, 24);
            this.cbo_Status.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_Status.Name = "cbo_Status";
            this.cbo_Status.Size = new System.Drawing.Size(146, 21);
            this.cbo_Status.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(736, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Od:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(406, 46);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Razlog osporavanja";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(406, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Status:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Electronic ID";
            // 
            // txt_Response
            // 
            this.txt_Response.Location = new System.Drawing.Point(155, 11);
            this.txt_Response.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Response.Multiline = true;
            this.txt_Response.Name = "txt_Response";
            this.txt_Response.Size = new System.Drawing.Size(226, 122);
            this.txt_Response.TabIndex = 10;
            // 
            // txt_EID
            // 
            this.txt_EID.Location = new System.Drawing.Point(11, 33);
            this.txt_EID.Margin = new System.Windows.Forms.Padding(2);
            this.txt_EID.Name = "txt_EID";
            this.txt_EID.Size = new System.Drawing.Size(76, 20);
            this.txt_EID.TabIndex = 11;
            // 
            // Column2
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "Ponovo pošalji";
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "Ponovi";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Text = "Ponovo posalji";
            this.Column2.Width = 125;
            // 
            // Column1
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.NullValue = "Opozovi slanje";
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column1.HeaderText = "Ispis";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Text = "Ispisi";
            this.Column1.Width = 125;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Response:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(11, 137);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1350, 567);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // PoslataDokumenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1372, 715);
            this.Controls.Add(this.txt_Opoziv);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btn_Datum);
            this.Controls.Add(this.btn_Status);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbo_Status);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Response);
            this.Controls.Add(this.txt_EID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "PoslataDokumenta";
            this.Text = "PoslataDokumenta";
            this.Load += new System.EventHandler(this.PoslataDokumenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Opoziv;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btn_Datum;
        private System.Windows.Forms.Button btn_Status;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbo_Status;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Response;
        private System.Windows.Forms.TextBox txt_EID;
        private System.Windows.Forms.DataGridViewButtonColumn Column2;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}