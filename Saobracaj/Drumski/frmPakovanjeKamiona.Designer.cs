using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    partial class frmPakovanjeKamiona
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonVrati = new System.Windows.Forms.Button();
            this.btnUploadDokumenta = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.panelPosaljiNajavu = new System.Windows.Forms.Panel();
            this.innerLayout = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dugmePanel = new System.Windows.Forms.TableLayoutPanel();
            this.panelLevo = new System.Windows.Forms.Panel();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.cboPrevoznik = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboTipVozila = new System.Windows.Forms.ComboBox();
            this.cboRegistracija = new System.Windows.Forms.ComboBox();
            this.btnFiltriraj = new System.Windows.Forms.Button();
            this.txtIzabran = new System.Windows.Forms.TextBox();
            this.outerPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelPosaljiNajavu.SuspendLayout();
            this.innerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.dugmePanel.SuspendLayout();
            this.panelLevo.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.outerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(10, 82);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(615, 660);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView3.ColumnHeadersHeight = 29;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView3, 2);
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(8, 407);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.Size = new System.Drawing.Size(1191, 283);
            this.dataGridView3.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.tableLayoutPanel1.SetColumnSpan(this.button3, 2);
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(8, 314);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(1191, 36);
            this.button3.TabIndex = 1;
            this.button3.Text = "Dodeli selekcijom";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.tableLayoutPanel1.SetColumnSpan(this.button1, 2);
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(8, 356);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1191, 45);
            this.button1.TabIndex = 2;
            this.button1.Text = "Sačuvaj povezane";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonVrati
            // 
            this.buttonVrati.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.tableLayoutPanel1.SetColumnSpan(this.buttonVrati, 2);
            this.buttonVrati.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonVrati.ForeColor = System.Drawing.Color.White;
            this.buttonVrati.Location = new System.Drawing.Point(8, 696);
            this.buttonVrati.Name = "buttonVrati";
            this.buttonVrati.Size = new System.Drawing.Size(1191, 38);
            this.buttonVrati.TabIndex = 4;
            this.buttonVrati.Text = "Vrati u neraspoređene";
            this.buttonVrati.UseVisualStyleBackColor = false;
            this.buttonVrati.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnUploadDokumenta
            // 
            this.btnUploadDokumenta.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUploadDokumenta.BackColor = System.Drawing.Color.SteelBlue;
            this.btnUploadDokumenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadDokumenta.ForeColor = System.Drawing.Color.White;
            this.btnUploadDokumenta.Location = new System.Drawing.Point(7, 120);
            this.btnUploadDokumenta.Name = "btnUploadDokumenta";
            this.btnUploadDokumenta.Size = new System.Drawing.Size(140, 35);
            this.btnUploadDokumenta.TabIndex = 0;
            this.btnUploadDokumenta.Text = "Najava vozila";
            this.btnUploadDokumenta.UseVisualStyleBackColor = false;
            this.btnUploadDokumenta.Click += new System.EventHandler(this.btnNajavaVozila_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.panelLevo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(10, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.groupBox1.Size = new System.Drawing.Size(1872, 777);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.buttonVrati, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.panelPosaljiNajavu, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(650, 25);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1207, 742);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label5, 2);
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(198, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "NERASPOREDjENI NALOZI";
            // 
            // panelPosaljiNajavu
            // 
            this.panelPosaljiNajavu.BackColor = System.Drawing.Color.LightGray;
            this.panelPosaljiNajavu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.panelPosaljiNajavu, 2);
            this.panelPosaljiNajavu.Controls.Add(this.innerLayout);
            this.panelPosaljiNajavu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPosaljiNajavu.Location = new System.Drawing.Point(8, 25);
            this.panelPosaljiNajavu.Name = "panelPosaljiNajavu";
            this.panelPosaljiNajavu.Size = new System.Drawing.Size(1191, 283);
            this.panelPosaljiNajavu.TabIndex = 6;
            // 
            // innerLayout
            // 
            this.innerLayout.ColumnCount = 2;
            this.innerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.innerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.innerLayout.Controls.Add(this.dataGridView2, 0, 0);
            this.innerLayout.Controls.Add(this.dugmePanel, 1, 0);
            this.innerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.innerLayout.Location = new System.Drawing.Point(0, 0);
            this.innerLayout.Name = "innerLayout";
            this.innerLayout.RowCount = 1;
            this.innerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.innerLayout.Size = new System.Drawing.Size(1189, 281);
            this.innerLayout.TabIndex = 0;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeight = 29;
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(1023, 275);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseDown);
            // 
            // dugmePanel
            // 
            this.dugmePanel.BackColor = System.Drawing.Color.LightGray;
            this.dugmePanel.ColumnCount = 1;
            this.dugmePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 154F));
            this.dugmePanel.Controls.Add(this.btnUploadDokumenta, 0, 1);
            this.dugmePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dugmePanel.Location = new System.Drawing.Point(1032, 3);
            this.dugmePanel.Name = "dugmePanel";
            this.dugmePanel.RowCount = 3;
            this.dugmePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dugmePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dugmePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.dugmePanel.Size = new System.Drawing.Size(154, 275);
            this.dugmePanel.TabIndex = 1;
            // 
            // panelLevo
            // 
            this.panelLevo.Controls.Add(this.dataGridView1);
            this.panelLevo.Controls.Add(this.panelFilter);
            this.panelLevo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLevo.Location = new System.Drawing.Point(15, 25);
            this.panelLevo.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.panelLevo.Name = "panelLevo";
            this.panelLevo.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.panelLevo.Size = new System.Drawing.Size(635, 742);
            this.panelLevo.TabIndex = 1;
            // 
            // panelFilter
            // 
            this.panelFilter.Controls.Add(this.cboPrevoznik);
            this.panelFilter.Controls.Add(this.label6);
            this.panelFilter.Controls.Add(this.label4);
            this.panelFilter.Controls.Add(this.label3);
            this.panelFilter.Controls.Add(this.label2);
            this.panelFilter.Controls.Add(this.cboTipVozila);
            this.panelFilter.Controls.Add(this.cboRegistracija);
            this.panelFilter.Controls.Add(this.btnFiltriraj);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(10, 0);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(615, 82);
            this.panelFilter.TabIndex = 1;
            // 
            // cboPrevoznik
            // 
            this.cboPrevoznik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrevoznik.Location = new System.Drawing.Point(329, 51);
            this.cboPrevoznik.Name = "cboPrevoznik";
            this.cboPrevoznik.Size = new System.Drawing.Size(200, 24);
            this.cboPrevoznik.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(2, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "KAMIONI";
            this.label6.UseCompatibleTextRendering = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(169, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Broj registracije";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(330, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Prevoznik";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tip vozila";
            // 
            // cboTipVozila
            // 
            this.cboTipVozila.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipVozila.Location = new System.Drawing.Point(2, 51);
            this.cboTipVozila.Name = "cboTipVozila";
            this.cboTipVozila.Size = new System.Drawing.Size(150, 24);
            this.cboTipVozila.TabIndex = 0;
            // 
            // cboRegistracija
            // 
            this.cboRegistracija.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRegistracija.Location = new System.Drawing.Point(164, 51);
            this.cboRegistracija.Name = "cboRegistracija";
            this.cboRegistracija.Size = new System.Drawing.Size(150, 24);
            this.cboRegistracija.TabIndex = 2;
            // 
            // btnFiltriraj
            // 
            this.btnFiltriraj.Location = new System.Drawing.Point(532, 51);
            this.btnFiltriraj.Name = "btnFiltriraj";
            this.btnFiltriraj.Size = new System.Drawing.Size(74, 23);
            this.btnFiltriraj.TabIndex = 3;
            this.btnFiltriraj.Text = "Filtriraj";
            this.btnFiltriraj.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtIzabran
            // 
            this.txtIzabran.Location = new System.Drawing.Point(1520, 12);
            this.txtIzabran.Name = "txtIzabran";
            this.txtIzabran.Size = new System.Drawing.Size(132, 22);
            this.txtIzabran.TabIndex = 212;
            // 
            // outerPanel
            // 
            this.outerPanel.Controls.Add(this.groupBox1);
            this.outerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outerPanel.Location = new System.Drawing.Point(0, 0);
            this.outerPanel.Name = "outerPanel";
            this.outerPanel.Padding = new System.Windows.Forms.Padding(10, 15, 10, 10);
            this.outerPanel.Size = new System.Drawing.Size(1892, 802);
            this.outerPanel.TabIndex = 0;
            // 
            // frmPakovanjeKamiona
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1892, 802);
            this.Controls.Add(this.outerPanel);
            this.Controls.Add(this.txtIzabran);
            this.Name = "frmPakovanjeKamiona";
            this.Text = "Pakovanje kamiona";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelPosaljiNajavu.ResumeLayout(false);
            this.innerLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.dugmePanel.ResumeLayout(false);
            this.panelLevo.ResumeLayout(false);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.outerPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.TextBox txtIzabran;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cboTipVozila;
        private System.Windows.Forms.ComboBox cboRegistracijacboPrevoznik;
        private System.Windows.Forms.ComboBox cboRegistracija;
        private System.Windows.Forms.Button btnFiltriraj;
        private System.Windows.Forms.Button buttonVrati;
        private System.Windows.Forms.Button btnUploadDokumenta;
        private System.Windows.Forms.Panel panelPosaljiNajavu;
        private Panel panelLevo;
        private Panel panelFilter;
        private Panel outerPanel;
        private Label label4;
        private Label label3;
        private Label label2;
        private DataGridView dataGridView2;
        private Label label5;
        private Label label6;
        private ComboBox cboPrevoznik;
        private TableLayoutPanel innerLayout;
        private TableLayoutPanel dugmePanel;
    }
}