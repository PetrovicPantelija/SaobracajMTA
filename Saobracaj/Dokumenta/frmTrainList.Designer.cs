
namespace Saobracaj.Dokumenta
{
    partial class frmTrainList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrainList));
            this.dataGrid1 = new System.Windows.Forms.DataGridView();
            this.btn_ImportExcel = new System.Windows.Forms.Button();
            this.txt_NHM = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_MRN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Seals = new System.Windows.Forms.TextBox();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBoxAddEdit = new System.Windows.Forms.GroupBox();
            this.txt_trainNo = new System.Windows.Forms.TextBox();
            this.departure_time = new System.Windows.Forms.DateTimePicker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddUpdate = new System.Windows.Forms.Button();
            this.txt_note = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_TotalUnitTare = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_TotalGoods = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_TotalWagonTare = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_TotalWeight = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_TotalTrainLength = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_UN = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGrid2 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.sDuzinaKola = new System.Windows.Forms.TextBox();
            this.sBruto = new System.Windows.Forms.TextBox();
            this.sNetoKont = new System.Windows.Forms.TextBox();
            this.sTaraKont = new System.Windows.Forms.TextBox();
            this.sTaraKola = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtBrojKola = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtBrojKontejnera = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.groupBoxAddEdit.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid2)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid1.Location = new System.Drawing.Point(25, 61);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.RowHeadersWidth = 51;
            this.dataGrid1.RowTemplate.Height = 24;
            this.dataGrid1.Size = new System.Drawing.Size(670, 222);
            this.dataGrid1.TabIndex = 0;
            this.dataGrid1.SelectionChanged += new System.EventHandler(this.dataGrid1_SelectionChanged);
            this.dataGrid1.Click += new System.EventHandler(this.dataGrid1_Click);
            // 
            // btn_ImportExcel
            // 
            this.btn_ImportExcel.Location = new System.Drawing.Point(25, 305);
            this.btn_ImportExcel.Name = "btn_ImportExcel";
            this.btn_ImportExcel.Size = new System.Drawing.Size(171, 38);
            this.btn_ImportExcel.TabIndex = 1;
            this.btn_ImportExcel.Text = "Uvezi Train List";
            this.btn_ImportExcel.UseVisualStyleBackColor = true;
            this.btn_ImportExcel.Click += new System.EventHandler(this.btn_ImportExcel_Click);
            // 
            // txt_NHM
            // 
            this.txt_NHM.Location = new System.Drawing.Point(25, 810);
            this.txt_NHM.Multiline = true;
            this.txt_NHM.Name = "txt_NHM";
            this.txt_NHM.ReadOnly = true;
            this.txt_NHM.Size = new System.Drawing.Size(190, 109);
            this.txt_NHM.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 790);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "NHM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 790);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "MRN";
            // 
            // txt_MRN
            // 
            this.txt_MRN.Location = new System.Drawing.Point(247, 810);
            this.txt_MRN.Multiline = true;
            this.txt_MRN.Name = "txt_MRN";
            this.txt_MRN.ReadOnly = true;
            this.txt_MRN.Size = new System.Drawing.Size(190, 109);
            this.txt_MRN.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(464, 790);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Plombe";
            // 
            // txt_Seals
            // 
            this.txt_Seals.Location = new System.Drawing.Point(467, 810);
            this.txt_Seals.Multiline = true;
            this.txt_Seals.Name = "txt_Seals";
            this.txt_Seals.ReadOnly = true;
            this.txt_Seals.Size = new System.Drawing.Size(190, 109);
            this.txt_Seals.TabIndex = 6;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(383, 25);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(100, 30);
            this.btnAddItem.TabIndex = 58;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(489, 25);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 30);
            this.btnEdit.TabIndex = 57;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.IndianRed;
            this.btnDelete.Location = new System.Drawing.Point(595, 25);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 56;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(131, 29);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(117, 22);
            this.textBoxSearch.TabIndex = 55;
            this.textBoxSearch.Text = "0";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(25, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.TabIndex = 54;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBoxAddEdit
            // 
            this.groupBoxAddEdit.Controls.Add(this.txt_trainNo);
            this.groupBoxAddEdit.Controls.Add(this.departure_time);
            this.groupBoxAddEdit.Controls.Add(this.btnCancel);
            this.groupBoxAddEdit.Controls.Add(this.btnAddUpdate);
            this.groupBoxAddEdit.Controls.Add(this.txt_note);
            this.groupBoxAddEdit.Controls.Add(this.label4);
            this.groupBoxAddEdit.Controls.Add(this.label5);
            this.groupBoxAddEdit.Controls.Add(this.label6);
            this.groupBoxAddEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAddEdit.Location = new System.Drawing.Point(728, 29);
            this.groupBoxAddEdit.Name = "groupBoxAddEdit";
            this.groupBoxAddEdit.Size = new System.Drawing.Size(479, 254);
            this.groupBoxAddEdit.TabIndex = 59;
            this.groupBoxAddEdit.TabStop = false;
            this.groupBoxAddEdit.Text = "Insert Train List";
            // 
            // txt_trainNo
            // 
            this.txt_trainNo.Location = new System.Drawing.Point(127, 81);
            this.txt_trainNo.Multiline = true;
            this.txt_trainNo.Name = "txt_trainNo";
            this.txt_trainNo.Size = new System.Drawing.Size(335, 28);
            this.txt_trainNo.TabIndex = 68;
            // 
            // departure_time
            // 
            this.departure_time.CustomFormat = "dd-MM-yyyy HH:mm";
            this.departure_time.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.departure_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.departure_time.Location = new System.Drawing.Point(127, 32);
            this.departure_time.Name = "departure_time";
            this.departure_time.Size = new System.Drawing.Size(335, 27);
            this.departure_time.TabIndex = 66;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.SlateGray;
            this.btnCancel.Location = new System.Drawing.Point(127, 199);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 64;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddUpdate
            // 
            this.btnAddUpdate.Location = new System.Drawing.Point(362, 199);
            this.btnAddUpdate.Name = "btnAddUpdate";
            this.btnAddUpdate.Size = new System.Drawing.Size(100, 30);
            this.btnAddUpdate.TabIndex = 63;
            this.btnAddUpdate.Text = "Add";
            this.btnAddUpdate.UseVisualStyleBackColor = true;
            this.btnAddUpdate.Click += new System.EventHandler(this.btnAddUpdate_Click);
            // 
            // txt_note
            // 
            this.txt_note.Location = new System.Drawing.Point(127, 129);
            this.txt_note.Multiline = true;
            this.txt_note.Name = "txt_note";
            this.txt_note.Size = new System.Drawing.Size(335, 64);
            this.txt_note.TabIndex = 62;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 20);
            this.label4.TabIndex = 61;
            this.label4.Text = "Note";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 59;
            this.label5.Text = "Train No";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 20);
            this.label6.TabIndex = 57;
            this.label6.Text = "Departure Time";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1300, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 17);
            this.label7.TabIndex = 62;
            this.label7.Text = "Tara Kontejnera";
            this.label7.Visible = false;
            // 
            // txt_TotalUnitTare
            // 
            this.txt_TotalUnitTare.Location = new System.Drawing.Point(1303, 46);
            this.txt_TotalUnitTare.Name = "txt_TotalUnitTare";
            this.txt_TotalUnitTare.ReadOnly = true;
            this.txt_TotalUnitTare.Size = new System.Drawing.Size(152, 22);
            this.txt_TotalUnitTare.TabIndex = 61;
            this.txt_TotalUnitTare.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1300, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 17);
            this.label8.TabIndex = 64;
            this.label8.Text = "Neto";
            this.label8.Visible = false;
            // 
            // txt_TotalGoods
            // 
            this.txt_TotalGoods.Location = new System.Drawing.Point(1303, 102);
            this.txt_TotalGoods.Name = "txt_TotalGoods";
            this.txt_TotalGoods.ReadOnly = true;
            this.txt_TotalGoods.Size = new System.Drawing.Size(169, 22);
            this.txt_TotalGoods.TabIndex = 63;
            this.txt_TotalGoods.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1300, 146);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 17);
            this.label9.TabIndex = 66;
            this.label9.Text = "Tara Kola";
            this.label9.Visible = false;
            // 
            // txt_TotalWagonTare
            // 
            this.txt_TotalWagonTare.Location = new System.Drawing.Point(1303, 166);
            this.txt_TotalWagonTare.Name = "txt_TotalWagonTare";
            this.txt_TotalWagonTare.ReadOnly = true;
            this.txt_TotalWagonTare.Size = new System.Drawing.Size(176, 22);
            this.txt_TotalWagonTare.TabIndex = 65;
            this.txt_TotalWagonTare.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1300, 205);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 17);
            this.label10.TabIndex = 68;
            this.label10.Text = "Bruto";
            this.label10.Visible = false;
            // 
            // txt_TotalWeight
            // 
            this.txt_TotalWeight.Location = new System.Drawing.Point(1299, 225);
            this.txt_TotalWeight.Name = "txt_TotalWeight";
            this.txt_TotalWeight.ReadOnly = true;
            this.txt_TotalWeight.Size = new System.Drawing.Size(166, 22);
            this.txt_TotalWeight.TabIndex = 67;
            this.txt_TotalWeight.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1300, 266);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 17);
            this.label11.TabIndex = 70;
            this.label11.Text = "Dužina Voza";
            this.label11.Visible = false;
            // 
            // txt_TotalTrainLength
            // 
            this.txt_TotalTrainLength.Location = new System.Drawing.Point(1299, 286);
            this.txt_TotalTrainLength.Name = "txt_TotalTrainLength";
            this.txt_TotalTrainLength.ReadOnly = true;
            this.txt_TotalTrainLength.Size = new System.Drawing.Size(147, 22);
            this.txt_TotalTrainLength.TabIndex = 69;
            this.txt_TotalTrainLength.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(687, 790);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 17);
            this.label12.TabIndex = 72;
            this.label12.Text = "UN Broj";
            // 
            // txt_UN
            // 
            this.txt_UN.Location = new System.Drawing.Point(690, 810);
            this.txt_UN.Multiline = true;
            this.txt_UN.Name = "txt_UN";
            this.txt_UN.ReadOnly = true;
            this.txt_UN.Size = new System.Drawing.Size(190, 109);
            this.txt_UN.TabIndex = 71;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.IndianRed;
            this.button1.Location = new System.Drawing.Point(1085, 309);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 30);
            this.button1.TabIndex = 73;
            this.button1.Text = "Brisanje stavki";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(25, 366);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1699, 421);
            this.tabControl1.TabIndex = 74;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGrid2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1691, 392);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Importovane stavke";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGrid2
            // 
            this.dataGrid2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid2.Location = new System.Drawing.Point(10, 6);
            this.dataGrid2.Name = "dataGrid2";
            this.dataGrid2.RowHeadersWidth = 51;
            this.dataGrid2.RowTemplate.Height = 24;
            this.dataGrid2.Size = new System.Drawing.Size(1670, 380);
            this.dataGrid2.TabIndex = 61;
            this.dataGrid2.Click += new System.EventHandler(this.dataGrid2_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1691, 392);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TL-Konvertovane";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1670, 362);
            this.dataGridView1.TabIndex = 62;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1691, 392);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Sumarno";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(3, 68);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(1670, 287);
            this.dataGridView2.TabIndex = 63;
            // 
            // sDuzinaKola
            // 
            this.sDuzinaKola.Location = new System.Drawing.Point(921, 313);
            this.sDuzinaKola.Name = "sDuzinaKola";
            this.sDuzinaKola.ReadOnly = true;
            this.sDuzinaKola.Size = new System.Drawing.Size(147, 22);
            this.sDuzinaKola.TabIndex = 79;
            this.sDuzinaKola.Text = "0";
            // 
            // sBruto
            // 
            this.sBruto.Location = new System.Drawing.Point(749, 313);
            this.sBruto.Name = "sBruto";
            this.sBruto.ReadOnly = true;
            this.sBruto.Size = new System.Drawing.Size(166, 22);
            this.sBruto.TabIndex = 78;
            this.sBruto.Text = "0";
            // 
            // sNetoKont
            // 
            this.sNetoKont.Location = new System.Drawing.Point(567, 313);
            this.sNetoKont.Name = "sNetoKont";
            this.sNetoKont.ReadOnly = true;
            this.sNetoKont.Size = new System.Drawing.Size(176, 22);
            this.sNetoKont.TabIndex = 77;
            this.sNetoKont.Text = "0";
            // 
            // sTaraKont
            // 
            this.sTaraKont.Location = new System.Drawing.Point(389, 313);
            this.sTaraKont.Name = "sTaraKont";
            this.sTaraKont.ReadOnly = true;
            this.sTaraKont.Size = new System.Drawing.Size(169, 22);
            this.sTaraKont.TabIndex = 76;
            this.sTaraKont.Text = "0";
            // 
            // sTaraKola
            // 
            this.sTaraKola.Location = new System.Drawing.Point(231, 313);
            this.sTaraKola.Name = "sTaraKola";
            this.sTaraKola.ReadOnly = true;
            this.sTaraKola.Size = new System.Drawing.Size(152, 22);
            this.sTaraKola.TabIndex = 75;
            this.sTaraKola.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(233, 293);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 17);
            this.label13.TabIndex = 80;
            this.label13.Text = "Tara Kola";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(918, 293);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 17);
            this.label14.TabIndex = 81;
            this.label14.Text = "Dužina Voza";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(386, 293);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(111, 17);
            this.label15.TabIndex = 82;
            this.label15.Text = "Tara Kontejnera";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(564, 293);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(38, 17);
            this.label16.TabIndex = 83;
            this.label16.Text = "Neto";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(746, 293);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(46, 17);
            this.label17.TabIndex = 84;
            this.label17.Text = "Bruto ";
            // 
            // txtBrojKola
            // 
            this.txtBrojKola.Location = new System.Drawing.Point(389, 341);
            this.txtBrojKola.Name = "txtBrojKola";
            this.txtBrojKola.ReadOnly = true;
            this.txtBrojKola.Size = new System.Drawing.Size(169, 22);
            this.txtBrojKola.TabIndex = 85;
            this.txtBrojKola.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(320, 341);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(63, 17);
            this.label18.TabIndex = 86;
            this.label18.Text = "Broj kola";
            // 
            // txtBrojKontejnera
            // 
            this.txtBrojKontejnera.Location = new System.Drawing.Point(749, 341);
            this.txtBrojKontejnera.Name = "txtBrojKontejnera";
            this.txtBrojKontejnera.ReadOnly = true;
            this.txtBrojKontejnera.Size = new System.Drawing.Size(169, 22);
            this.txtBrojKontejnera.TabIndex = 87;
            this.txtBrojKontejnera.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(640, 344);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(104, 17);
            this.label19.TabIndex = 88;
            this.label19.Text = "Broj kontejnera";
            // 
            // frmTrainList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1812, 936);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.txtBrojKontejnera);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtBrojKola);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txt_UN);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.sDuzinaKola);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.sBruto);
            this.Controls.Add(this.txt_TotalTrainLength);
            this.Controls.Add(this.sNetoKont);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.sTaraKont);
            this.Controls.Add(this.txt_TotalWeight);
            this.Controls.Add(this.sTaraKola);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_TotalWagonTare);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_TotalGoods);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_TotalUnitTare);
            this.Controls.Add(this.groupBoxAddEdit);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_Seals);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_MRN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_NHM);
            this.Controls.Add(this.btn_ImportExcel);
            this.Controls.Add(this.dataGrid1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTrainList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Train List";
            this.Load += new System.EventHandler(this.frmTrainList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.groupBoxAddEdit.ResumeLayout(false);
            this.groupBoxAddEdit.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid2)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid1;
        private System.Windows.Forms.Button btn_ImportExcel;
        private System.Windows.Forms.TextBox txt_NHM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_MRN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Seals;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBoxAddEdit;
        private System.Windows.Forms.TextBox txt_trainNo;
        private System.Windows.Forms.DateTimePicker departure_time;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddUpdate;
        private System.Windows.Forms.TextBox txt_note;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_TotalUnitTare;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_TotalGoods;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_TotalWagonTare;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_TotalWeight;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_TotalTrainLength;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_UN;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGrid2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox sDuzinaKola;
        private System.Windows.Forms.TextBox sBruto;
        private System.Windows.Forms.TextBox sNetoKont;
        private System.Windows.Forms.TextBox sTaraKont;
        private System.Windows.Forms.TextBox sTaraKola;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtBrojKola;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtBrojKontejnera;
        private System.Windows.Forms.Label label19;
    }
}

