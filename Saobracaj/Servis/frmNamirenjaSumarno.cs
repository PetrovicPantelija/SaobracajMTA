﻿using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
namespace Saobracaj.Servis
{
    public partial class frmNamirenjaSumarno : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmNamirenjaSumarno()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();

        }
        string niz = "";

        public static string code = "frmNamirenjaSumarno";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();

        private void frmNamirenjaSumarno_Load(object sender, EventArgs e)
        {
            var select = "select  LokomotivaNamirenje.ID as IDNamirenja, LokomotivaVrstaNamirenja.Naziv as VrstaNamirenja,LokomotivaPrijava.Lokomotiva,  (Rtrim(Delavci.DeIme) + ' ' + Rtrim(DElavci.DePriimek)) as Zaposleni , LokomotivaNamirenje.DatumNamirenja as DatumNamirenja, Cast(LokomotivaNamirenje.Kolicina as integer) as Kolicina, LokomotivaNamirenje.Napomena from LokomotivaNamirenje " +
            " inner join lokomotivaVrstaNamirenja on lokomotivaVrstaNamirenja.ID = LokomotivaNamirenje.VrstaNamirenjaID " +
            " inner join LokomotivaPrijava on LokomotivaPrijava.ID = LokomotivaNamirenje.LokomotivaPrijavaID " +
            " inner join Delavci on Delavci.DeSifra = LokomotivaPrijava.Zaposleni ";

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            // dataGridView1.ReadOnly = true;
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
            GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(192, 255, 162));
            summaryColumnDescriptor.DataMember = "Kolicina";
            summaryColumnDescriptor.Format = "{Sum}";
            summaryColumnDescriptor.Name = "SumKolicina";
            summaryColumnDescriptor.SummaryType = Syncfusion.Grouping.SummaryType.Int32Aggregate;

            GridSummaryRowDescriptor summaryRowDescriptor = new GridSummaryRowDescriptor();
            summaryRowDescriptor.SummaryColumns.Add(summaryColumnDescriptor);
            summaryRowDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(255, 231, 162));

            this.gridGroupingControl1.TableDescriptor.SummaryRows.Add(summaryRowDescriptor);

        }

    }

    /*  public class TotalSummary : SummaryBase
      {
          private double _total;
          public static readonly TotalSummary Empty = new TotalSummary(0);

          public static ITreeTableSummary CreateSummaryMethod(SummaryDescriptor summaryDescriptor, Record record)
          {
              object obj = summaryDescriptor.GetValue(record);
              bool isNull = (obj == null || obj is DBNull);

              if (isNull)
                  return Empty;

              else
              {
                  double val = Convert.ToDouble(obj);
                  return new TotalSummary(val);
              }
          }

          public double Total
          {
              get
              {
                  return _total;
              }
          }

          public TotalSummary(double total)
          {
              _total = total;
          }

          public TotalSummary Combine(TotalSummary other)
          {

              // Summary objects are immutable. That means properties cannot be modified for an 

              // existing object. Instead every time a change is made a new object must be created (just like 

              // System.String). 

              //

              // This allows following optimization: return existing summary object if either one of the values is 0. -->

              if (other.Total == 0)
                  return this;

              else if (Total == 0)
                  return other;

              // <-- end of optimization

              else
                  return new TotalSummary(this.Total + other.Total);
          }

          // To return the custom calculated value for the summary field

          public override SummaryBase Combine(SummaryBase other)
          {
              return Combine((TotalSummary)other);
          }
      }
    */
}
