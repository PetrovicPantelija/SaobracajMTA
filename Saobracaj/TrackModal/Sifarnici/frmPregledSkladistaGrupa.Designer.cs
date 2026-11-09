namespace Saobracaj.TrackModal.Sifarnici
{
    partial class frmPregledSkladistaGrupa
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
            this.panelFilter = new System.Windows.Forms.Panel();
            this.lblBroj = new System.Windows.Forms.Label();
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.chkNeaktivni = new System.Windows.Forms.CheckBox();
            this.chkAktivni = new System.Windows.Forms.CheckBox();
            this.lblZone = new System.Windows.Forms.Label();
            this.chkSveZone = new System.Windows.Forms.CheckBox();
            this.clbZone = new System.Windows.Forms.CheckedListBox();
            this.lblLokacije = new System.Windows.Forms.Label();
            this.chkSveLokacije = new System.Windows.Forms.CheckBox();
            this.clbLokacije = new System.Windows.Forms.CheckedListBox();
            this.lblObjekti = new System.Windows.Forms.Label();
            this.chkSveObjekti = new System.Windows.Forms.CheckBox();
            this.clbObjekti = new System.Windows.Forms.CheckedListBox();
            this.lblPolja = new System.Windows.Forms.Label();
            this.chkSvePolja = new System.Windows.Forms.CheckBox();
            this.clbPolja = new System.Windows.Forms.CheckedListBox();
            this.gridGroupingControl1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).BeginInit();
            this.SuspendLayout();
            //
            // panelFilter
            //
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelFilter.Controls.Add(this.lblBroj);
            this.panelFilter.Controls.Add(this.btnOsvezi);
            this.panelFilter.Controls.Add(this.chkNeaktivni);
            this.panelFilter.Controls.Add(this.chkAktivni);
            this.panelFilter.Controls.Add(this.lblZone);
            this.panelFilter.Controls.Add(this.chkSveZone);
            this.panelFilter.Controls.Add(this.clbZone);
            this.panelFilter.Controls.Add(this.lblLokacije);
            this.panelFilter.Controls.Add(this.chkSveLokacije);
            this.panelFilter.Controls.Add(this.clbLokacije);
            this.panelFilter.Controls.Add(this.lblObjekti);
            this.panelFilter.Controls.Add(this.chkSveObjekti);
            this.panelFilter.Controls.Add(this.clbObjekti);
            this.panelFilter.Controls.Add(this.lblPolja);
            this.panelFilter.Controls.Add(this.chkSvePolja);
            this.panelFilter.Controls.Add(this.clbPolja);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 0);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1300, 270);
            this.panelFilter.TabIndex = 0;
            //
            // chkAktivni
            //
            this.chkAktivni.AutoSize = true;
            this.chkAktivni.Checked = true;
            this.chkAktivni.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAktivni.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkAktivni.Location = new System.Drawing.Point(12, 10);
            this.chkAktivni.Name = "chkAktivni";
            this.chkAktivni.Size = new System.Drawing.Size(70, 20);
            this.chkAktivni.TabIndex = 1;
            this.chkAktivni.Text = "Aktivni";
            this.chkAktivni.UseVisualStyleBackColor = true;
            this.chkAktivni.CheckedChanged += new System.EventHandler(this.chkAktivnost_CheckedChanged);
            //
            // chkNeaktivni
            //
            this.chkNeaktivni.AutoSize = true;
            this.chkNeaktivni.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkNeaktivni.Location = new System.Drawing.Point(100, 10);
            this.chkNeaktivni.Name = "chkNeaktivni";
            this.chkNeaktivni.Size = new System.Drawing.Size(85, 20);
            this.chkNeaktivni.TabIndex = 2;
            this.chkNeaktivni.Text = "Neaktivni";
            this.chkNeaktivni.UseVisualStyleBackColor = true;
            this.chkNeaktivni.CheckedChanged += new System.EventHandler(this.chkAktivnost_CheckedChanged);
            //
            // btnOsvezi
            //
            this.btnOsvezi.Font = new System.Drawing.Font("Arial", 9.75F);
            this.btnOsvezi.Location = new System.Drawing.Point(210, 6);
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(90, 27);
            this.btnOsvezi.TabIndex = 3;
            this.btnOsvezi.Text = "Osveži";
            this.btnOsvezi.UseVisualStyleBackColor = true;
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            //
            // lblBroj
            //
            this.lblBroj.AutoSize = true;
            this.lblBroj.Font = new System.Drawing.Font("Arial", 9.75F);
            this.lblBroj.Location = new System.Drawing.Point(320, 11);
            this.lblBroj.Name = "lblBroj";
            this.lblBroj.Size = new System.Drawing.Size(0, 16);
            this.lblBroj.TabIndex = 4;
            //
            // lblZone
            //
            this.lblZone.AutoSize = true;
            this.lblZone.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblZone.Location = new System.Drawing.Point(12, 44);
            this.lblZone.Name = "lblZone";
            this.lblZone.Text = "Zone";
            //
            // chkSveZone
            //
            this.chkSveZone.AutoSize = true;
            this.chkSveZone.Checked = true;
            this.chkSveZone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSveZone.Font = new System.Drawing.Font("Arial", 9F);
            this.chkSveZone.Location = new System.Drawing.Point(200, 43);
            this.chkSveZone.Name = "chkSveZone";
            this.chkSveZone.TabIndex = 10;
            this.chkSveZone.Text = "Uključi sve";
            this.chkSveZone.UseVisualStyleBackColor = true;
            //
            // clbZone
            //
            this.clbZone.CheckOnClick = true;
            this.clbZone.Font = new System.Drawing.Font("Arial", 9F);
            this.clbZone.FormattingEnabled = true;
            this.clbZone.IntegralHeight = false;
            this.clbZone.Location = new System.Drawing.Point(12, 66);
            this.clbZone.Name = "clbZone";
            this.clbZone.Size = new System.Drawing.Size(300, 195);
            this.clbZone.TabIndex = 11;
            //
            // lblLokacije
            //
            this.lblLokacije.AutoSize = true;
            this.lblLokacije.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblLokacije.Location = new System.Drawing.Point(330, 44);
            this.lblLokacije.Name = "lblLokacije";
            this.lblLokacije.Text = "Lokacije";
            //
            // chkSveLokacije
            //
            this.chkSveLokacije.AutoSize = true;
            this.chkSveLokacije.Checked = true;
            this.chkSveLokacije.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSveLokacije.Font = new System.Drawing.Font("Arial", 9F);
            this.chkSveLokacije.Location = new System.Drawing.Point(518, 43);
            this.chkSveLokacije.Name = "chkSveLokacije";
            this.chkSveLokacije.TabIndex = 12;
            this.chkSveLokacije.Text = "Uključi sve";
            this.chkSveLokacije.UseVisualStyleBackColor = true;
            //
            // clbLokacije
            //
            this.clbLokacije.CheckOnClick = true;
            this.clbLokacije.Font = new System.Drawing.Font("Arial", 9F);
            this.clbLokacije.FormattingEnabled = true;
            this.clbLokacije.IntegralHeight = false;
            this.clbLokacije.Location = new System.Drawing.Point(330, 66);
            this.clbLokacije.Name = "clbLokacije";
            this.clbLokacije.Size = new System.Drawing.Size(300, 195);
            this.clbLokacije.TabIndex = 13;
            //
            // lblObjekti
            //
            this.lblObjekti.AutoSize = true;
            this.lblObjekti.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblObjekti.Location = new System.Drawing.Point(648, 44);
            this.lblObjekti.Name = "lblObjekti";
            this.lblObjekti.Text = "Objekti";
            //
            // chkSveObjekti
            //
            this.chkSveObjekti.AutoSize = true;
            this.chkSveObjekti.Checked = true;
            this.chkSveObjekti.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSveObjekti.Font = new System.Drawing.Font("Arial", 9F);
            this.chkSveObjekti.Location = new System.Drawing.Point(836, 43);
            this.chkSveObjekti.Name = "chkSveObjekti";
            this.chkSveObjekti.TabIndex = 14;
            this.chkSveObjekti.Text = "Uključi sve";
            this.chkSveObjekti.UseVisualStyleBackColor = true;
            //
            // clbObjekti
            //
            this.clbObjekti.CheckOnClick = true;
            this.clbObjekti.Font = new System.Drawing.Font("Arial", 9F);
            this.clbObjekti.FormattingEnabled = true;
            this.clbObjekti.IntegralHeight = false;
            this.clbObjekti.Location = new System.Drawing.Point(648, 66);
            this.clbObjekti.Name = "clbObjekti";
            this.clbObjekti.Size = new System.Drawing.Size(300, 195);
            this.clbObjekti.TabIndex = 15;
            //
            // lblPolja
            //
            this.lblPolja.AutoSize = true;
            this.lblPolja.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPolja.Location = new System.Drawing.Point(966, 44);
            this.lblPolja.Name = "lblPolja";
            this.lblPolja.Text = "Polja";
            //
            // chkSvePolja
            //
            this.chkSvePolja.AutoSize = true;
            this.chkSvePolja.Checked = true;
            this.chkSvePolja.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSvePolja.Font = new System.Drawing.Font("Arial", 9F);
            this.chkSvePolja.Location = new System.Drawing.Point(1154, 43);
            this.chkSvePolja.Name = "chkSvePolja";
            this.chkSvePolja.TabIndex = 16;
            this.chkSvePolja.Text = "Uključi sve";
            this.chkSvePolja.UseVisualStyleBackColor = true;
            //
            // clbPolja
            //
            this.clbPolja.CheckOnClick = true;
            this.clbPolja.Font = new System.Drawing.Font("Arial", 9F);
            this.clbPolja.FormattingEnabled = true;
            this.clbPolja.IntegralHeight = false;
            this.clbPolja.Location = new System.Drawing.Point(966, 66);
            this.clbPolja.Name = "clbPolja";
            this.clbPolja.Size = new System.Drawing.Size(300, 195);
            this.clbPolja.TabIndex = 17;
            //
            // gridGroupingControl1
            //
            this.gridGroupingControl1.BackColor = System.Drawing.Color.White;
            this.gridGroupingControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridGroupingControl1.Font = new System.Drawing.Font("Arial", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridGroupingControl1.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Office2016;
            this.gridGroupingControl1.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Metro;
            this.gridGroupingControl1.Location = new System.Drawing.Point(0, 270);
            this.gridGroupingControl1.Name = "gridGroupingControl1";
            this.gridGroupingControl1.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl1.Size = new System.Drawing.Size(1300, 430);
            this.gridGroupingControl1.TabIndex = 20;
            this.gridGroupingControl1.TableDescriptor.AllowEdit = false;
            this.gridGroupingControl1.TableDescriptor.AllowNew = false;
            this.gridGroupingControl1.TableDescriptor.AllowRemove = false;
            this.gridGroupingControl1.TableOptions.AllowSelection = Syncfusion.Windows.Forms.Grid.GridSelectionFlags.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionMode = System.Windows.Forms.SelectionMode.One;
            this.gridGroupingControl1.Text = "gridGroupingControl1";
            this.gridGroupingControl1.UseRightToLeftCompatibleTextBox = true;
            //
            // frmPregledSkladistaGrupa
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.gridGroupingControl1);
            this.Controls.Add(this.panelFilter);
            this.Name = "frmPregledSkladistaGrupa";
            this.Text = "Pregled skladišnog prostora";
            this.Load += new System.EventHandler(this.frmPregledSkladistaGrupa_Load);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label lblBroj;
        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.CheckBox chkNeaktivni;
        private System.Windows.Forms.CheckBox chkAktivni;
        private System.Windows.Forms.Label lblZone;
        private System.Windows.Forms.CheckBox chkSveZone;
        private System.Windows.Forms.CheckedListBox clbZone;
        private System.Windows.Forms.Label lblLokacije;
        private System.Windows.Forms.CheckBox chkSveLokacije;
        private System.Windows.Forms.CheckedListBox clbLokacije;
        private System.Windows.Forms.Label lblObjekti;
        private System.Windows.Forms.CheckBox chkSveObjekti;
        private System.Windows.Forms.CheckedListBox clbObjekti;
        private System.Windows.Forms.Label lblPolja;
        private System.Windows.Forms.CheckBox chkSvePolja;
        private System.Windows.Forms.CheckedListBox clbPolja;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl1;
    }
}
