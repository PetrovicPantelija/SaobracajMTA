
namespace Saobracaj.SyncForm
{
    partial class frmKalendar
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonAdv1 = new Syncfusion.Windows.Forms.ButtonAdv();
            this.comboBoxAdv5 = new Syncfusion.Windows.Forms.Tools.ComboBoxAdv();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAdv6 = new Syncfusion.Windows.Forms.ButtonAdv();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scheduleControl1 = new Syncfusion.Windows.Forms.Schedule.ScheduleControl();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxAdv5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonAdv1);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.comboBoxAdv5);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.buttonAdv6);
            this.panel2.Location = new System.Drawing.Point(13, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(972, 62);
            this.panel2.TabIndex = 34;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // buttonAdv1
            // 
            this.buttonAdv1.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.buttonAdv1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonAdv1.BeforeTouchSize = new System.Drawing.Size(84, 25);
            this.buttonAdv1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdv1.ForeColor = System.Drawing.Color.White;
            this.buttonAdv1.Location = new System.Drawing.Point(323, 28);
            this.buttonAdv1.Name = "buttonAdv1";
            this.buttonAdv1.Size = new System.Drawing.Size(84, 25);
            this.buttonAdv1.TabIndex = 29;
            this.buttonAdv1.Text = "Save";
            this.buttonAdv1.ThemeName = "Metro";
            this.buttonAdv1.UseVisualStyle = true;
            this.buttonAdv1.Click += new System.EventHandler(this.buttonAdv1_Click);
            // 
            // comboBoxAdv5
            // 
            this.comboBoxAdv5.AllowNewText = false;
            this.comboBoxAdv5.BackColor = System.Drawing.Color.White;
            this.comboBoxAdv5.BeforeTouchSize = new System.Drawing.Size(188, 19);
            this.comboBoxAdv5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxAdv5.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.comboBoxAdv5.Location = new System.Drawing.Point(6, 34);
            this.comboBoxAdv5.Name = "comboBoxAdv5";
            this.comboBoxAdv5.Size = new System.Drawing.Size(188, 19);
            this.comboBoxAdv5.Style = Syncfusion.Windows.Forms.VisualStyle.Metro;
            this.comboBoxAdv5.TabIndex = 27;
            this.comboBoxAdv5.ThemeName = "Metro";
            this.comboBoxAdv5.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(2, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Choose Data Source";
            this.label1.Visible = false;
            // 
            // buttonAdv6
            // 
            this.buttonAdv6.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Metro;
            this.buttonAdv6.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonAdv6.BeforeTouchSize = new System.Drawing.Size(84, 25);
            this.buttonAdv6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdv6.ForeColor = System.Drawing.Color.White;
            this.buttonAdv6.Location = new System.Drawing.Point(233, 28);
            this.buttonAdv6.Name = "buttonAdv6";
            this.buttonAdv6.Size = new System.Drawing.Size(84, 25);
            this.buttonAdv6.TabIndex = 10;
            this.buttonAdv6.Text = "Osveži";
            this.buttonAdv6.ThemeName = "Metro";
            this.buttonAdv6.UseVisualStyle = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(496, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(123, 56);
            this.pictureBox2.TabIndex = 28;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.scheduleControl1);
            this.panel1.Location = new System.Drawing.Point(12, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 534);
            this.panel1.TabIndex = 35;
            // 
            // scheduleControl1
            // 
            this.scheduleControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scheduleControl1.Appearance.WeekHeaderFormat = "MMMM dd";
            this.scheduleControl1.Appearance.WeekMonthFullFormat = "dddd, dd MMMM yyyy";
            this.scheduleControl1.BackColor = System.Drawing.Color.Gray;
            this.scheduleControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scheduleControl1.Culture = new System.Globalization.CultureInfo("");
            this.scheduleControl1.DataSource = null;
            this.scheduleControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scheduleControl1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.scheduleControl1.ISO8601CalenderFormat = false;
            this.scheduleControl1.Location = new System.Drawing.Point(0, 3);
            this.scheduleControl1.Name = "scheduleControl1";
            this.scheduleControl1.ShowRoundedCorners = true;
            this.scheduleControl1.Size = new System.Drawing.Size(970, 528);
            this.scheduleControl1.TabIndex = 3;
            this.scheduleControl1.ParseDisplayItem += new Syncfusion.Windows.Forms.Schedule.ParseDisplayItemEventHandler(this.scheduleControl1_ParseDisplayItem);
            // 
            // frmKalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 616);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "frmKalendar";
            this.Text = "Kalendar";
            this.Load += new System.EventHandler(this.frmKalendar_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxAdv5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private Syncfusion.Windows.Forms.ButtonAdv buttonAdv1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Syncfusion.Windows.Forms.Tools.ComboBoxAdv comboBoxAdv5;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.ButtonAdv buttonAdv6;
        private System.Windows.Forms.Panel panel1;
        private Syncfusion.Windows.Forms.Schedule.ScheduleControl scheduleControl1;
    }
}