using Microsoft.ReportingServices.Diagnostics.Internal;
using Saobracaj.Pantheon_Export;
using Saobracaj.Sifarnici;
using Saobracaj.Uvoz;
using Syncfusion.XlsIO.Implementation.XmlSerialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    class InsertRadniNalogDrumski
    {
        public string connect = frmLogovanje.connectionString;
            
        public void UpdateRadniNalogDrumski(int ID, int AutoDan, string Ref, string MestoPreuzimanja, int? MestoUtovara, string AdresaUtovara,
                    int? MestoIstovara, DateTime? DatumUtovara, DateTime? DatumIstovara, string AdresaIstovara, DateTime? DtPreuzimanjaPraznogKontejnera,
                    string GranicniPrelaz, decimal? Trosak, string Valuta, int? KamionID, int? StatusID, string DodatniOpis, decimal? Cena, string KontaktOsobaNaIstovaru,
                    int? PDV, int? TipTransporta, int? BookingBrodara, int? Klijent, decimal? BttoKontejnera, decimal? BttoRobe, string BrojVoza, string BrojKontejnera, string BrojKontejnera2, string BrodskaTeretnica, string BrodskaPlomba, string NapomenaPoz,
                    string PolaznaCarinarnica, string OdredisnaCarinarnica, string PolaznaSpedicijaKontakt, string OdredisnaSpedicijaKontakt)

        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRadniNalogDrumski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter iD = new SqlParameter();
            iD.ParameterName = "@ID";
            iD.SqlDbType = SqlDbType.Int;
            iD.Direction = ParameterDirection.Input;
            iD.Value = ID;
            cmd.Parameters.Add(iD);

            SqlParameter autoDan = new SqlParameter();
            autoDan.ParameterName = "@AutoDan";
            autoDan.SqlDbType = SqlDbType.Int;
            autoDan.Direction = ParameterDirection.Input;
            autoDan.Value = AutoDan;
            cmd.Parameters.Add(autoDan);

            SqlParameter ref3 = new SqlParameter();
            ref3.ParameterName = "@Ref";
            ref3.SqlDbType = SqlDbType.NVarChar;
            ref3.Size = 100;
            ref3.Direction = ParameterDirection.Input;
            ref3.Value = (object)Ref ?? DBNull.Value;
            cmd.Parameters.Add(ref3);

            SqlParameter mestoPreuzimanjaKontejnera = new SqlParameter();
            mestoPreuzimanjaKontejnera.ParameterName = "@MestoPreuzimanja";
            mestoPreuzimanjaKontejnera.SqlDbType = SqlDbType.NVarChar;
            mestoPreuzimanjaKontejnera.Size = 100;
            mestoPreuzimanjaKontejnera.Direction = ParameterDirection.Input;
            mestoPreuzimanjaKontejnera.Value = (object)MestoPreuzimanja ?? DBNull.Value;
            cmd.Parameters.Add(mestoPreuzimanjaKontejnera);

            SqlParameter mestoUtovara = new SqlParameter();
            mestoUtovara.ParameterName = "@MestoUtovara";
            mestoUtovara.SqlDbType = SqlDbType.Int;
            mestoUtovara.Direction = ParameterDirection.Input;
            mestoUtovara.Value = MestoUtovara.HasValue ? (object)MestoUtovara.Value : DBNull.Value;
            cmd.Parameters.Add(mestoUtovara);

            SqlParameter adresaUtovara = new SqlParameter();
            adresaUtovara.ParameterName = "@AdresaUtovara";
            adresaUtovara.SqlDbType = SqlDbType.NVarChar;
            adresaUtovara.Size = 100;
            adresaUtovara.Direction = ParameterDirection.Input;
            adresaUtovara.Value = (object)AdresaUtovara ?? DBNull.Value;
            cmd.Parameters.Add(adresaUtovara);

            SqlParameter mestoIstovara = new SqlParameter();
            mestoIstovara.ParameterName = "@MestoIstovara";
            mestoIstovara.SqlDbType = SqlDbType.Int;
            mestoIstovara.Direction = ParameterDirection.Input;
            mestoIstovara.Value = MestoIstovara.HasValue ? (object)MestoIstovara.Value : DBNull.Value;
            cmd.Parameters.Add(mestoIstovara);

            SqlParameter datumUtovara = new SqlParameter();
            datumUtovara.ParameterName = "@DatumUtovara";
            datumUtovara.SqlDbType = SqlDbType.DateTime;
            datumUtovara.Direction = ParameterDirection.Input;
            datumUtovara.Value = DatumUtovara.HasValue ? (object)DatumUtovara.Value : DBNull.Value;
            cmd.Parameters.Add(datumUtovara);

            SqlParameter datumIstovara = new SqlParameter();
            datumIstovara.ParameterName = "@DatumIstovara";
            datumIstovara.SqlDbType = SqlDbType.DateTime;
            datumIstovara.Direction = ParameterDirection.Input;
            datumIstovara.Value = DatumIstovara.HasValue ? (object)DatumIstovara.Value : DBNull.Value;
            cmd.Parameters.Add(datumIstovara);

            SqlParameter adresaIstovara = new SqlParameter();
            adresaIstovara.ParameterName = "@AdresaIstovara";
            adresaIstovara.SqlDbType = SqlDbType.NVarChar;
            adresaIstovara.Size = 100;
            adresaIstovara.Direction = ParameterDirection.Input;
            adresaIstovara.Value = (object)AdresaIstovara ?? DBNull.Value;
            cmd.Parameters.Add(adresaIstovara);

            SqlParameter dtPreuzimanjaPraznogKontejnera = new SqlParameter();
            dtPreuzimanjaPraznogKontejnera.ParameterName = "@DtPreuzimanjaPraznogKontejnera";
            dtPreuzimanjaPraznogKontejnera.SqlDbType = SqlDbType.DateTime;
            dtPreuzimanjaPraznogKontejnera.Direction = ParameterDirection.Input;
            dtPreuzimanjaPraznogKontejnera.Value = DtPreuzimanjaPraznogKontejnera.HasValue ? (object)DtPreuzimanjaPraznogKontejnera.Value : DBNull.Value;
            cmd.Parameters.Add(dtPreuzimanjaPraznogKontejnera);

            SqlParameter granicniPrelaz = new SqlParameter();
            granicniPrelaz.ParameterName = "@GranicniPrelaz";
            granicniPrelaz.SqlDbType = SqlDbType.NVarChar;
            granicniPrelaz.Size = 100;
            granicniPrelaz.Direction = ParameterDirection.Input;
            granicniPrelaz.Value = (object)GranicniPrelaz ?? DBNull.Value;
            cmd.Parameters.Add(granicniPrelaz);

            SqlParameter trosak = new SqlParameter();
            trosak.ParameterName = "@Trosak";
            trosak.SqlDbType = SqlDbType.Decimal;
            trosak.Direction = ParameterDirection.Input;
            trosak.Value = Trosak.HasValue ? (object)Trosak.Value : DBNull.Value;
            cmd.Parameters.Add(trosak);

            SqlParameter valuta = new SqlParameter();
            valuta.ParameterName = "@Valuta";
            valuta.SqlDbType = SqlDbType.NVarChar;
            valuta.Size = 50;
            valuta.Direction = ParameterDirection.Input;
            valuta.Value = (object)Valuta ?? DBNull.Value;
            cmd.Parameters.Add(valuta);

            SqlParameter kamion = new SqlParameter();
            kamion.ParameterName = "@KamionID";
            kamion.SqlDbType = SqlDbType.Int;
            kamion.Direction = ParameterDirection.Input;
            kamion.Value = KamionID.HasValue ? (object)KamionID.Value : DBNull.Value;
            cmd.Parameters.Add(kamion);

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@StatusID";
            status.SqlDbType = SqlDbType.Int;
            status.Direction = ParameterDirection.Input;
            status.Value = (StatusID.HasValue && StatusID.Value > 0) ? (object)StatusID.Value : DBNull.Value;
            cmd.Parameters.Add(status);

            SqlParameter dodatniOpis = new SqlParameter();
            dodatniOpis.ParameterName = "@DodatniOpis";
            dodatniOpis.SqlDbType = SqlDbType.NVarChar;
            dodatniOpis.Size = 100;
            dodatniOpis.Direction = ParameterDirection.Input;
            dodatniOpis.Value = (object)DodatniOpis ?? DBNull.Value;
            cmd.Parameters.Add(dodatniOpis);

            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Decimal;
            cena.Direction = ParameterDirection.Input;
            cena.Value = Cena.HasValue ? (object)Cena.Value : DBNull.Value;
            cmd.Parameters.Add(cena);

            SqlParameter kontaktOsobaNaIstovaru = new SqlParameter();
            kontaktOsobaNaIstovaru.ParameterName = "@KontaktOsobaNaIstovaru";
            kontaktOsobaNaIstovaru.SqlDbType = SqlDbType.NVarChar;
            kontaktOsobaNaIstovaru.Size = 50;
            kontaktOsobaNaIstovaru.Direction = ParameterDirection.Input;
            kontaktOsobaNaIstovaru.Value = (object)(KontaktOsobaNaIstovaru?.Trim()) ?? DBNull.Value;
            cmd.Parameters.Add(kontaktOsobaNaIstovaru);

            SqlParameter pdv = new SqlParameter();
            pdv.ParameterName = "@PDV";
            pdv.SqlDbType = SqlDbType.Int;
            pdv.Direction = ParameterDirection.Input;
            pdv.Value = PDV.HasValue ? (object)PDV.Value : DBNull.Value;
            cmd.Parameters.Add(pdv);

            SqlParameter tipTransporta = new SqlParameter();
            tipTransporta.ParameterName = "@TipTransporta";
            tipTransporta.SqlDbType = SqlDbType.Int;
            tipTransporta.Direction = ParameterDirection.Input;
            tipTransporta.Value = TipTransporta.HasValue ? (object)TipTransporta.Value : DBNull.Value;
            cmd.Parameters.Add(tipTransporta); 

            SqlParameter bookingBrodara = new SqlParameter();
            bookingBrodara.ParameterName = "@BookingBrodara";
            bookingBrodara.SqlDbType = SqlDbType.Int;
            bookingBrodara.Direction = ParameterDirection.Input;
            bookingBrodara.Value = BookingBrodara.HasValue ? (object)BookingBrodara.Value : DBNull.Value;
            cmd.Parameters.Add(bookingBrodara);

            SqlParameter klijent = new SqlParameter();
            klijent.ParameterName = "@Klijent";
            klijent.SqlDbType = SqlDbType.Int;
            klijent.Direction = ParameterDirection.Input;
            klijent.Value = Klijent.HasValue ? (object)Klijent.Value : DBNull.Value;
            cmd.Parameters.Add(klijent);

            SqlParameter bttoKontejnera = new SqlParameter();
            bttoKontejnera.ParameterName = "@BttoKontejnera";
            bttoKontejnera.SqlDbType = SqlDbType.Decimal;
            bttoKontejnera.Direction = ParameterDirection.Input;
            bttoKontejnera.Value = BttoKontejnera.HasValue ? (object)BttoKontejnera.Value : DBNull.Value;
            cmd.Parameters.Add(bttoKontejnera);


            SqlParameter bttoRobe = new SqlParameter();
            bttoRobe.ParameterName = "@BttoRobe";
            bttoRobe.SqlDbType = SqlDbType.Decimal;
            bttoRobe.Direction = ParameterDirection.Input;
            bttoRobe.Value = BttoRobe.HasValue ? (object)BttoRobe.Value : DBNull.Value;
            cmd.Parameters.Add(bttoRobe);

            SqlParameter brojVoza = new SqlParameter();
            brojVoza.ParameterName = "@BrojVoza";
            brojVoza.SqlDbType = SqlDbType.NVarChar;
            brojVoza.Size = 50;
            brojVoza.Direction = ParameterDirection.Input;
            brojVoza.Value = (object)BrojVoza ?? DBNull.Value;
            cmd.Parameters.Add(brojVoza);

            SqlParameter brojKontejnera = new SqlParameter();
            brojKontejnera.ParameterName = "@BrojKontejnera";
            brojKontejnera.SqlDbType = SqlDbType.NVarChar;
            brojKontejnera.Size = 30;
            brojKontejnera.Direction = ParameterDirection.Input;
            brojKontejnera.Value = (object)BrojKontejnera ?? DBNull.Value;
            cmd.Parameters.Add(brojKontejnera);

            SqlParameter brojKontejnera2 = new SqlParameter();
            brojKontejnera2.ParameterName = "@BrojKontejnera2";
            brojKontejnera2.SqlDbType = SqlDbType.NVarChar;
            brojKontejnera2.Size = 50;
            brojKontejnera2.Direction = ParameterDirection.Input;
            brojKontejnera2.Value = (object)BrojKontejnera2 ?? DBNull.Value;
            cmd.Parameters.Add(brojKontejnera2);

            SqlParameter brodskaTeretnica = new SqlParameter();
            brodskaTeretnica.ParameterName = "@BrodskaTeretnica";
            brodskaTeretnica.SqlDbType = SqlDbType.NVarChar;
            brodskaTeretnica.Size = 50;
            brodskaTeretnica.Direction = ParameterDirection.Input;
            brodskaTeretnica.Value = (object)BrodskaTeretnica ?? DBNull.Value;
            cmd.Parameters.Add(brodskaTeretnica);

            SqlParameter brodskaPlomba = new SqlParameter();
            brodskaPlomba.ParameterName = "@BrodskaPlomba";
            brodskaPlomba.SqlDbType = SqlDbType.NVarChar;
            brodskaPlomba.Size = 50;
            brodskaPlomba.Direction = ParameterDirection.Input;
            brodskaPlomba.Value = (object)BrodskaPlomba ?? DBNull.Value;
            cmd.Parameters.Add(brodskaPlomba);
            
            SqlParameter napomenaPoz = new SqlParameter();
            napomenaPoz.ParameterName = "@NapomenaPoz";
            napomenaPoz.SqlDbType = SqlDbType.NVarChar;
            napomenaPoz.Size = 100;
            napomenaPoz.Direction = ParameterDirection.Input;
            napomenaPoz.Value = (object)NapomenaPoz ?? DBNull.Value;
            cmd.Parameters.Add(napomenaPoz);

            SqlParameter polaznaCarinarnica = new SqlParameter();
            polaznaCarinarnica.ParameterName = "@PolaznaCarinarnica";
            polaznaCarinarnica.SqlDbType = SqlDbType.NVarChar;
            polaznaCarinarnica.Size = 250;
            polaznaCarinarnica.Direction = ParameterDirection.Input;
            polaznaCarinarnica.Value = (object)PolaznaCarinarnica ?? DBNull.Value;
            cmd.Parameters.Add(polaznaCarinarnica);

            SqlParameter odredisnaCarinarnica = new SqlParameter();
            odredisnaCarinarnica.ParameterName = "@OdredisnaCarinarnica";
            odredisnaCarinarnica.SqlDbType = SqlDbType.NVarChar;
            odredisnaCarinarnica.Size = 250;
            odredisnaCarinarnica.Direction = ParameterDirection.Input;
            odredisnaCarinarnica.Value = (object)OdredisnaCarinarnica ?? DBNull.Value;
            cmd.Parameters.Add(odredisnaCarinarnica);

            SqlParameter polaznaSpedicijaKontakt = new SqlParameter();
            polaznaSpedicijaKontakt.ParameterName = "@PolaznaSpedicijaKontakt";
            polaznaSpedicijaKontakt.SqlDbType = SqlDbType.NVarChar;
            polaznaSpedicijaKontakt.Size = 500;
            polaznaSpedicijaKontakt.Direction = ParameterDirection.Input;
            polaznaSpedicijaKontakt.Value = (object)PolaznaSpedicijaKontakt ?? DBNull.Value;
            cmd.Parameters.Add(polaznaSpedicijaKontakt);

            SqlParameter odredisnaSpedicijaKontakt = new SqlParameter();
            odredisnaSpedicijaKontakt.ParameterName = "@OdredisnaSpedicijaKontakt";
            odredisnaSpedicijaKontakt.SqlDbType = SqlDbType.NVarChar;
            odredisnaSpedicijaKontakt.Size = 500;
            odredisnaSpedicijaKontakt.Direction = ParameterDirection.Input;
            odredisnaSpedicijaKontakt.Value = (object)OdredisnaSpedicijaKontakt ?? DBNull.Value;
            cmd.Parameters.Add(odredisnaSpedicijaKontakt);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                error = false;
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
            }
            catch (SqlException ex)
            {
                //throw new Exception("Neuspešan upis");
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tran.Rollback(); // Ne zaboravi i rollback
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Ažuriranje je uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }


        public int InsRadniNalogDrumski(int? TipNaloga, int AutoDan, string Ref, string MestoPreuzimanja,int? Klijent, int? MestoUtovara, string AdresaUtovara,
                 int? MestoIstovara, DateTime? DatumUtovara, DateTime? DatumIstovara, string AdresaIstovara, DateTime? DtPreuzimanjaPraznogKontejnera,
                 string GranicniPrelaz, decimal? Trosak, string Valuta, int? KamionID, int? StatusID, string DodatniOpis, decimal? Cena, string KontaktOsobaNaIstovaru, int? PDV, int? TipTransporta,
                 string BrojVoza, decimal? BttoKontejnera, decimal? BttoRobe, string BrojKontejnera, string BrojKontejnera2, int? BookingBrodara,string BrodskaTeretnica, string BrodskaPlomba, string NapomenaPoz,
                 string PolaznaCarinarnica, string OdredisnaCarinarnica, string PolaznaSpedicijaKontakt, string OdredisnaSpedicijaKontakt)

        {
            int IDPom = 0;

            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRadniNalogDrumski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter tipNaloga = new SqlParameter();
            tipNaloga.ParameterName = "@TipNaloga";
            tipNaloga.SqlDbType = SqlDbType.Int;
            tipNaloga.Direction = ParameterDirection.Input;
            tipNaloga.Value = TipNaloga.HasValue ? (object)TipNaloga.Value : DBNull.Value;
            cmd.Parameters.Add(tipNaloga);

            SqlParameter autoDan = new SqlParameter();
            autoDan.ParameterName = "@AutoDan";
            autoDan.SqlDbType = SqlDbType.Int;
            autoDan.Direction = ParameterDirection.Input;
            autoDan.Value = AutoDan;
            cmd.Parameters.Add(autoDan);

            SqlParameter ref3 = new SqlParameter();
            ref3.ParameterName = "@Ref";
            ref3.SqlDbType = SqlDbType.NVarChar;
            ref3.Size = 100;
            ref3.Direction = ParameterDirection.Input;
            ref3.Value = (object)Ref ?? DBNull.Value;
            cmd.Parameters.Add(ref3);

            SqlParameter mestoPreuzimanjaKontejnera = new SqlParameter();
            mestoPreuzimanjaKontejnera.ParameterName = "@MestoPreuzimanja";
            mestoPreuzimanjaKontejnera.SqlDbType = SqlDbType.NVarChar;
            mestoPreuzimanjaKontejnera.Size = 100;
            mestoPreuzimanjaKontejnera.Direction = ParameterDirection.Input;
            mestoPreuzimanjaKontejnera.Value = (object)MestoPreuzimanja ?? DBNull.Value;
            cmd.Parameters.Add(mestoPreuzimanjaKontejnera);

            SqlParameter klijent = new SqlParameter();
            klijent.ParameterName = "@Klijent";
            klijent.SqlDbType = SqlDbType.Int;
            klijent.Direction = ParameterDirection.Input;
            klijent.Value = Klijent.HasValue ? (object)Klijent.Value : DBNull.Value;
            cmd.Parameters.Add(klijent);

            SqlParameter mestoUtovara = new SqlParameter();
            mestoUtovara.ParameterName = "@MestoUtovara";
            mestoUtovara.SqlDbType = SqlDbType.Int;
            mestoUtovara.Direction = ParameterDirection.Input;
            mestoUtovara.Value = MestoUtovara.HasValue ? (object)MestoUtovara.Value : DBNull.Value;
            cmd.Parameters.Add(mestoUtovara);

            SqlParameter adresaUtovara = new SqlParameter();
            adresaUtovara.ParameterName = "@AdresaUtovara";
            adresaUtovara.SqlDbType = SqlDbType.NVarChar;
            adresaUtovara.Size = 100;
            adresaUtovara.Direction = ParameterDirection.Input;
            adresaUtovara.Value = (object)AdresaUtovara ?? DBNull.Value;
            cmd.Parameters.Add(adresaUtovara);

            SqlParameter mestoIstovara = new SqlParameter();
            mestoIstovara.ParameterName = "@MestoIstovara";
            mestoIstovara.SqlDbType = SqlDbType.Int;
            mestoIstovara.Direction = ParameterDirection.Input;
            mestoIstovara.Value = MestoIstovara.HasValue ? (object)MestoIstovara.Value : DBNull.Value;
            cmd.Parameters.Add(mestoIstovara);

            SqlParameter datumUtovara = new SqlParameter();
            datumUtovara.ParameterName = "@DatumUtovara";
            datumUtovara.SqlDbType = SqlDbType.DateTime;
            datumUtovara.Direction = ParameterDirection.Input;
            datumUtovara.Value = DatumUtovara.HasValue ? (object)DatumUtovara.Value : DBNull.Value;
            cmd.Parameters.Add(datumUtovara);

            SqlParameter datumIstovara = new SqlParameter();
            datumIstovara.ParameterName = "@DatumIstovara";
            datumIstovara.SqlDbType = SqlDbType.DateTime;
            datumIstovara.Direction = ParameterDirection.Input;
            datumIstovara.Value = DatumIstovara.HasValue ? (object)DatumIstovara.Value : DBNull.Value;
            cmd.Parameters.Add(datumIstovara);

            SqlParameter adresaIstovara = new SqlParameter();
            adresaIstovara.ParameterName = "@AdresaIstovara";
            adresaIstovara.SqlDbType = SqlDbType.NVarChar;
            adresaIstovara.Size = 100;
            adresaIstovara.Direction = ParameterDirection.Input;
            adresaIstovara.Value = (object)AdresaIstovara ?? DBNull.Value;
            cmd.Parameters.Add(adresaIstovara);

            SqlParameter dtPreuzimanjaPraznogKontejnera = new SqlParameter();
            dtPreuzimanjaPraznogKontejnera.ParameterName = "@DtPreuzimanjaPraznogKontejnera";
            dtPreuzimanjaPraznogKontejnera.SqlDbType = SqlDbType.DateTime;
            dtPreuzimanjaPraznogKontejnera.Direction = ParameterDirection.Input;
            dtPreuzimanjaPraznogKontejnera.Value = DtPreuzimanjaPraznogKontejnera.HasValue ? (object)DtPreuzimanjaPraznogKontejnera.Value : DBNull.Value;
            cmd.Parameters.Add(dtPreuzimanjaPraznogKontejnera);

            SqlParameter granicniPrelaz = new SqlParameter();
            granicniPrelaz.ParameterName = "@GranicniPrelaz";
            granicniPrelaz.SqlDbType = SqlDbType.NVarChar;
            granicniPrelaz.Size = 100;
            granicniPrelaz.Direction = ParameterDirection.Input;
            granicniPrelaz.Value = (object)GranicniPrelaz ?? DBNull.Value;
            cmd.Parameters.Add(granicniPrelaz);

            SqlParameter trosak = new SqlParameter();
            trosak.ParameterName = "@Trosak";
            trosak.SqlDbType = SqlDbType.Decimal;
            trosak.Direction = ParameterDirection.Input;
            trosak.Value = Trosak.HasValue ? (object)Trosak.Value : DBNull.Value;
            cmd.Parameters.Add(trosak);

            SqlParameter valuta = new SqlParameter();
            valuta.ParameterName = "@Valuta";
            valuta.SqlDbType = SqlDbType.NVarChar;
            valuta.Size = 50;
            valuta.Direction = ParameterDirection.Input;
            valuta.Value = (object)Valuta ?? DBNull.Value;
            cmd.Parameters.Add(valuta);

            SqlParameter kamion = new SqlParameter();
            kamion.ParameterName = "@KamionID";
            kamion.SqlDbType = SqlDbType.Int;
            kamion.Direction = ParameterDirection.Input;
            kamion.Value = KamionID.HasValue ? (object)KamionID.Value : DBNull.Value;
            cmd.Parameters.Add(kamion);

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@StatusID";
            status.SqlDbType = SqlDbType.Int;
            status.Direction = ParameterDirection.Input;
            status.Value = (StatusID.HasValue && StatusID.Value > 0) ? (object)StatusID.Value : DBNull.Value;
            cmd.Parameters.Add(status);

            SqlParameter dodatniOpis = new SqlParameter();
            dodatniOpis.ParameterName = "@DodatniOpis";
            dodatniOpis.SqlDbType = SqlDbType.NVarChar;
            dodatniOpis.Size = 100;
            dodatniOpis.Direction = ParameterDirection.Input;
            dodatniOpis.Value = (object)DodatniOpis ?? DBNull.Value;
            cmd.Parameters.Add(dodatniOpis);

            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Decimal;
            cena.Direction = ParameterDirection.Input;
            cena.Value = Cena.HasValue ? (object)Cena.Value : DBNull.Value;
            cmd.Parameters.Add(cena);

            SqlParameter kontaktOsobaNaIstovaru = new SqlParameter();
            kontaktOsobaNaIstovaru.ParameterName = "@KontaktOsobaNaIstovaru";
            kontaktOsobaNaIstovaru.SqlDbType = SqlDbType.NVarChar;
            kontaktOsobaNaIstovaru.Size = 50;
            kontaktOsobaNaIstovaru.Direction = ParameterDirection.Input;
            kontaktOsobaNaIstovaru.Value = (object)KontaktOsobaNaIstovaru ?? DBNull.Value;
            cmd.Parameters.Add(kontaktOsobaNaIstovaru);

            SqlParameter pdv = new SqlParameter();
            pdv.ParameterName = "@PDV";
            pdv.SqlDbType = SqlDbType.Int;
            pdv.Direction = ParameterDirection.Input;
            pdv.Value = PDV.HasValue ? (object)PDV.Value : DBNull.Value;
            cmd.Parameters.Add(pdv);

            SqlParameter tipTransporta = new SqlParameter();
            tipTransporta.ParameterName = "@TipTransporta";
            tipTransporta.SqlDbType = SqlDbType.Int;
            tipTransporta.Direction = ParameterDirection.Input;
            tipTransporta.Value = TipTransporta.HasValue ? (object)TipTransporta.Value : DBNull.Value;
            cmd.Parameters.Add(tipTransporta);

            SqlParameter brojVoza = new SqlParameter();
            brojVoza.ParameterName = "@BrojVoza";
            brojVoza.SqlDbType = SqlDbType.NVarChar;
            brojVoza.Size = 50;
            brojVoza.Direction = ParameterDirection.Input;
            brojVoza.Value = (object)BrojVoza ?? DBNull.Value;
            cmd.Parameters.Add(brojVoza);

            SqlParameter bttoKontejnera = new SqlParameter();
            bttoKontejnera.ParameterName = "@BttoKontejnera";
            bttoKontejnera.SqlDbType = SqlDbType.Decimal;
            bttoKontejnera.Direction = ParameterDirection.Input;
            bttoKontejnera.Value = BttoKontejnera.HasValue ? (object)BttoKontejnera.Value : DBNull.Value;
            cmd.Parameters.Add(bttoKontejnera);

            SqlParameter bttoRobe = new SqlParameter();
            bttoRobe.ParameterName = "@BttoRobe";
            bttoRobe.SqlDbType = SqlDbType.Decimal;
            bttoRobe.Direction = ParameterDirection.Input;
            bttoRobe.Value = BttoRobe.HasValue ? (object)BttoRobe.Value : DBNull.Value;
            cmd.Parameters.Add(bttoRobe);

            SqlParameter brojKontejnera = new SqlParameter();
            brojKontejnera.ParameterName = "@BrojKontejnera";
            brojKontejnera.SqlDbType = SqlDbType.NVarChar;
            brojKontejnera.Size = 50;
            brojKontejnera.Direction = ParameterDirection.Input;
            brojKontejnera.Value = (object)BrojKontejnera ?? DBNull.Value;
            cmd.Parameters.Add(brojKontejnera);

            SqlParameter brojKontejnera2 = new SqlParameter();
            brojKontejnera2.ParameterName = "@BrojKontejnera2";
            brojKontejnera2.SqlDbType = SqlDbType.NVarChar;
            brojKontejnera2.Size = 50;
            brojKontejnera2.Direction = ParameterDirection.Input;
            brojKontejnera2.Value = (object)BrojKontejnera2 ?? DBNull.Value;
            cmd.Parameters.Add(brojKontejnera2);

            SqlParameter bookingBrodara = new SqlParameter();
            bookingBrodara.ParameterName = "@BookingBrodara";
            bookingBrodara.SqlDbType = SqlDbType.Int;
            bookingBrodara.Direction = ParameterDirection.Input;
            bookingBrodara.Value = BookingBrodara.HasValue ? (object)BookingBrodara.Value : DBNull.Value;
            cmd.Parameters.Add(bookingBrodara);

            SqlParameter brodskaTeretnica = new SqlParameter();
            brodskaTeretnica.ParameterName = "@BrodskaTeretnica";
            brodskaTeretnica.SqlDbType = SqlDbType.NVarChar;
            brodskaTeretnica.Size = 50;
            brodskaTeretnica.Direction = ParameterDirection.Input;
            brodskaTeretnica.Value = (object)BrodskaTeretnica ?? DBNull.Value;
            cmd.Parameters.Add(brodskaTeretnica);

            SqlParameter brodskaPlomba = new SqlParameter();
            brodskaPlomba.ParameterName = "@BrodskaPlomba";
            brodskaPlomba.SqlDbType = SqlDbType.NVarChar;
            brodskaPlomba.Size = 50;
            brodskaPlomba.Direction = ParameterDirection.Input;
            brodskaPlomba.Value = (object)BrodskaPlomba ?? DBNull.Value;
            cmd.Parameters.Add(brodskaPlomba);

            SqlParameter napomenaPoz = new SqlParameter();
            napomenaPoz.ParameterName = "@NapomenaPoz";
            napomenaPoz.SqlDbType = SqlDbType.NVarChar;
            napomenaPoz.Size = 100;
            napomenaPoz.Direction = ParameterDirection.Input;
            napomenaPoz.Value = (object)NapomenaPoz ?? DBNull.Value;
            cmd.Parameters.Add(napomenaPoz);

            SqlParameter polaznaCarinarnica = new SqlParameter();
            polaznaCarinarnica.ParameterName = "@PolaznaCarinarnica";
            polaznaCarinarnica.SqlDbType = SqlDbType.NVarChar;
            polaznaCarinarnica.Size = 250;
            polaznaCarinarnica.Direction = ParameterDirection.Input;
            polaznaCarinarnica.Value = (object)PolaznaCarinarnica ?? DBNull.Value;
            cmd.Parameters.Add(polaznaCarinarnica);

            SqlParameter odredisnaCarinarnica = new SqlParameter();
            odredisnaCarinarnica.ParameterName = "@OdredisnaCarinarnica";
            odredisnaCarinarnica.SqlDbType = SqlDbType.NVarChar;
            odredisnaCarinarnica.Size = 250;
            odredisnaCarinarnica.Direction = ParameterDirection.Input;
            odredisnaCarinarnica.Value = (object)OdredisnaCarinarnica ?? DBNull.Value;
            cmd.Parameters.Add(odredisnaCarinarnica);

            SqlParameter polaznaSpedicijaKontakt = new SqlParameter();
            polaznaSpedicijaKontakt.ParameterName = "@PolaznaSpedicijaKontakt";
            polaznaSpedicijaKontakt.SqlDbType = SqlDbType.NVarChar;
            polaznaSpedicijaKontakt.Size = 500;
            polaznaSpedicijaKontakt.Direction = ParameterDirection.Input;
            polaznaSpedicijaKontakt.Value = (object)PolaznaSpedicijaKontakt ?? DBNull.Value;
            cmd.Parameters.Add(polaznaSpedicijaKontakt); 

            SqlParameter odredisnaSpedicijaKontakt = new SqlParameter();
            odredisnaSpedicijaKontakt.ParameterName = "@OdredisnaSpedicijaKontakt";
            odredisnaSpedicijaKontakt.SqlDbType = SqlDbType.NVarChar;
            odredisnaSpedicijaKontakt.Size = 500;
            odredisnaSpedicijaKontakt.Direction = ParameterDirection.Input;
            odredisnaSpedicijaKontakt.Value = (object)OdredisnaSpedicijaKontakt ?? DBNull.Value;
            cmd.Parameters.Add(odredisnaSpedicijaKontakt);

            SqlParameter idParam = new SqlParameter("@IDPom", SqlDbType.Int);
            idParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(idParam);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                error = false;
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
                IDPom = (int)cmd.Parameters["@IDPom"].Value;
                
            }
            catch (SqlException ex)
            {
                //throw new Exception("Neuspešan upis");
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tran.Rollback(); // Ne zaboravi i rollback
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Kreiranje je uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
            return IDPom;
        }


        public void DodeliKamion(int ID, int KamionID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DodeliKamionZaRadniNalogDrumski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter kamionID = new SqlParameter();
            kamionID.ParameterName = "@KamionID";
            kamionID.SqlDbType = SqlDbType.Int;
            kamionID.Direction = ParameterDirection.Input;
            kamionID.Value = KamionID;
            cmd.Parameters.Add(kamionID);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis ");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }

        }

        public void DodeliKamionP(int ID, string RegBr)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DodeliKamionZaRadniNalogDrumskiP";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter kamionID = new SqlParameter();
            kamionID.ParameterName = "@RegBr";
            kamionID.SqlDbType = SqlDbType.NVarChar;
            kamionID.Size = 50;
            kamionID.Direction = ParameterDirection.Input;
            kamionID.Value = RegBr;
            cmd.Parameters.Add(kamionID);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis ");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }

        }

        public void VratiKamionUNerasporedjene(int ID, int KamionID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRadniNalogDrumskiKamionID";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter kamionID = new SqlParameter();
            kamionID.ParameterName = "@KamionID";
            kamionID.SqlDbType = SqlDbType.Int;
            kamionID.Direction = ParameterDirection.Input;
            kamionID.Value = KamionID;
            cmd.Parameters.Add(kamionID);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis ");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }

        }

        public void ObrisiKamion(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PbrisiKamionZaRadniNalogDrumski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis ");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }

        }

        public void UpdateStatusRadniNalogDrumski(int ID, int? Status)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateStatusRadniNalogDrumski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter iD = new SqlParameter();
            iD.ParameterName = "@ID";
            iD.SqlDbType = SqlDbType.Int;
            iD.Direction = ParameterDirection.Input;
            iD.Value = ID;
            cmd.Parameters.Add(iD);  

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@Status";
            status.SqlDbType = SqlDbType.Int;
            status.Direction = ParameterDirection.Input;
            status.Value = Status.HasValue ? (object)Status.Value : DBNull.Value; 
            cmd.Parameters.Add(status);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
            }
            catch (SqlException)
            {
                throw new Exception("Neuspešan upis");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Ažuriranje radnog naloga broja je uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }

        public void PostaviNalogIDNaRedove(List<int> idrnLista)
        {
            DataTable tvp = new DataTable();
            tvp.Columns.Add("IDRN", typeof(int));

            foreach (var id in idrnLista)
            {
                tvp.Rows.Add(id);
            }

            using (SqlConnection conn = new SqlConnection(connect))
            using (SqlCommand cmd = new SqlCommand("NapraviRadniNalogDrumskiIzDrumskog", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@IDRNLista", tvp);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "TVP_IDRN";

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Nalog ID je uspešno postavljen.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void UpdateNalogIDRadniNalogDrumski(List<int> idrnLista, int nalogID)
        {
            DataTable tvp = new DataTable();
            tvp.Columns.Add("IDRN", typeof(int));

            foreach (var id in idrnLista)
            {
                tvp.Rows.Add(id);
            }

            using (SqlConnection conn = new SqlConnection(connect))
            using (SqlCommand cmd = new SqlCommand("DodajStavkeURadniNalogDrumski", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@IDRNLista", tvp);
                tvpParam.SqlDbType = SqlDbType.Structured;
                tvpParam.TypeName = "TVP_IDRN";

                SqlParameter nalogParam = new SqlParameter();
                nalogParam.ParameterName = "@NalogID";
                nalogParam.SqlDbType = SqlDbType.Int;
                nalogParam.Direction = ParameterDirection.Input;
                nalogParam.Value = nalogID;
                cmd.Parameters.Add(nalogParam);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Stavke su uspešno dodate u postojeći nalog.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public int DuplirajRadniNalogDrumski(int? TipNaloga,int? AutoDan, string MestoPreuzimanja, int? Klijent, int? MestoUtovara, string AdresaUtovara, int? MestoIstovara, DateTime? DatumIstovara, string AdresaIstovara, 
                        DateTime? DtPreuzimanjaPraznogKontejnera, string GranicniPrelaz, decimal? Trosak, string Valuta, int? StatusID, string DodatniOpis, decimal? Cena, string KontaktOsobaNaIstovaru, 
                        int? PDV, int? TipTransporta, string BrojVoza, decimal? BttoKontejnera, decimal? BttoRobe, int? BookingBrodara, string BrodskaTeretnica, string BrodskaPlomba, string NapomenaPoz)

        {
            int IDPom = 0;

            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DuplirajZapisRadniNalogDrumski";
            cmd.CommandType = CommandType.StoredProcedure;


            SqlParameter uvoz = new SqlParameter();
            uvoz.ParameterName = "@TipNaloga";
            uvoz.SqlDbType = SqlDbType.Int;
            uvoz.Direction = ParameterDirection.Input;
            uvoz.Value = (object)TipNaloga;
            cmd.Parameters.Add(uvoz);

            SqlParameter autoDan = new SqlParameter();
            autoDan.ParameterName = "@AutoDan";
            autoDan.SqlDbType = SqlDbType.Int;
            autoDan.Direction = ParameterDirection.Input;
            autoDan.Value = AutoDan.HasValue ? (object)AutoDan.Value : DBNull.Value;
            cmd.Parameters.Add(autoDan);

            SqlParameter mestoPreuzimanjaKontejnera = new SqlParameter();
            mestoPreuzimanjaKontejnera.ParameterName = "@MestoPreuzimanja";
            mestoPreuzimanjaKontejnera.SqlDbType = SqlDbType.NVarChar;
            mestoPreuzimanjaKontejnera.Size = 100;
            mestoPreuzimanjaKontejnera.Direction = ParameterDirection.Input;
            mestoPreuzimanjaKontejnera.Value = (object)MestoPreuzimanja ?? DBNull.Value;
            cmd.Parameters.Add(mestoPreuzimanjaKontejnera);

            SqlParameter klijent = new SqlParameter();
            klijent.ParameterName = "@Klijent";
            klijent.SqlDbType = SqlDbType.Int;
            klijent.Direction = ParameterDirection.Input;
            klijent.Value = Klijent.HasValue ? (object)Klijent.Value : DBNull.Value;
            cmd.Parameters.Add(klijent);

            SqlParameter mestoUtovara = new SqlParameter();
            mestoUtovara.ParameterName = "@MestoUtovara";
            mestoUtovara.SqlDbType = SqlDbType.Int;
            mestoUtovara.Direction = ParameterDirection.Input;
            mestoUtovara.Value = MestoUtovara.HasValue ? (object)MestoUtovara.Value : DBNull.Value;
            cmd.Parameters.Add(mestoUtovara);

            SqlParameter adresaUtovara = new SqlParameter();
            adresaUtovara.ParameterName = "@AdresaUtovara";
            adresaUtovara.SqlDbType = SqlDbType.NVarChar;
            adresaUtovara.Size = 100;
            adresaUtovara.Direction = ParameterDirection.Input;
            adresaUtovara.Value = (object)AdresaUtovara ?? DBNull.Value;
            cmd.Parameters.Add(adresaUtovara);

            SqlParameter mestoIstovara = new SqlParameter();
            mestoIstovara.ParameterName = "@MestoIstovara";
            mestoIstovara.SqlDbType = SqlDbType.Int;
            mestoIstovara.Direction = ParameterDirection.Input;
            mestoIstovara.Value = MestoIstovara.HasValue ? (object)MestoIstovara.Value : DBNull.Value;
            cmd.Parameters.Add(mestoIstovara);

            SqlParameter datumIstovara = new SqlParameter();
            datumIstovara.ParameterName = "@DatumIstovara";
            datumIstovara.SqlDbType = SqlDbType.DateTime;
            datumIstovara.Direction = ParameterDirection.Input;
            datumIstovara.Value = DatumIstovara.HasValue ? (object)DatumIstovara.Value : DBNull.Value;
            cmd.Parameters.Add(datumIstovara);

            SqlParameter adresaIstovara = new SqlParameter();
            adresaIstovara.ParameterName = "@AdresaIstovara";
            adresaIstovara.SqlDbType = SqlDbType.NVarChar;
            adresaIstovara.Size = 100;
            adresaIstovara.Direction = ParameterDirection.Input;
            adresaIstovara.Value = (object)AdresaIstovara ?? DBNull.Value;
            cmd.Parameters.Add(adresaIstovara);

            SqlParameter dtPreuzimanjaPraznogKontejnera = new SqlParameter();
            dtPreuzimanjaPraznogKontejnera.ParameterName = "@DtPreuzimanjaPraznogKontejnera";
            dtPreuzimanjaPraznogKontejnera.SqlDbType = SqlDbType.DateTime;
            dtPreuzimanjaPraznogKontejnera.Direction = ParameterDirection.Input;
            dtPreuzimanjaPraznogKontejnera.Value = DtPreuzimanjaPraznogKontejnera.HasValue ? (object)DtPreuzimanjaPraznogKontejnera.Value : DBNull.Value;
            cmd.Parameters.Add(dtPreuzimanjaPraznogKontejnera);

            SqlParameter granicniPrelaz = new SqlParameter();
            granicniPrelaz.ParameterName = "@GranicniPrelaz";
            granicniPrelaz.SqlDbType = SqlDbType.NVarChar;
            granicniPrelaz.Size = 100;
            granicniPrelaz.Direction = ParameterDirection.Input;
            granicniPrelaz.Value = (object)GranicniPrelaz ?? DBNull.Value;
            cmd.Parameters.Add(granicniPrelaz);

            SqlParameter trosak = new SqlParameter();
            trosak.ParameterName = "@Trosak";
            trosak.SqlDbType = SqlDbType.Decimal;
            trosak.Direction = ParameterDirection.Input;
            trosak.Value = Trosak.HasValue ? (object)Trosak.Value : DBNull.Value;
            cmd.Parameters.Add(trosak);

            SqlParameter valuta = new SqlParameter();
            valuta.ParameterName = "@Valuta";
            valuta.SqlDbType = SqlDbType.NVarChar;
            valuta.Size = 50;
            valuta.Direction = ParameterDirection.Input;
            valuta.Value = (object)Valuta ?? DBNull.Value;
            cmd.Parameters.Add(valuta);

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@StatusID";
            status.SqlDbType = SqlDbType.Int;
            status.Direction = ParameterDirection.Input;
            status.Value = (StatusID.HasValue && StatusID.Value > 0) ? (object)StatusID.Value : DBNull.Value;
            cmd.Parameters.Add(status);

            SqlParameter dodatniOpis = new SqlParameter();
            dodatniOpis.ParameterName = "@DodatniOpis";
            dodatniOpis.SqlDbType = SqlDbType.NVarChar;
            dodatniOpis.Size = 100;
            dodatniOpis.Direction = ParameterDirection.Input;
            dodatniOpis.Value = (object)DodatniOpis ?? DBNull.Value;
            cmd.Parameters.Add(dodatniOpis);

            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Decimal;
            cena.Direction = ParameterDirection.Input;
            cena.Value = Cena.HasValue ? (object)Cena.Value : DBNull.Value;
            cmd.Parameters.Add(cena);

            SqlParameter kontaktOsobaNaIstovaru = new SqlParameter();
            kontaktOsobaNaIstovaru.ParameterName = "@KontaktOsobaNaIstovaru";
            kontaktOsobaNaIstovaru.SqlDbType = SqlDbType.NVarChar;
            kontaktOsobaNaIstovaru.Size = 50;
            kontaktOsobaNaIstovaru.Direction = ParameterDirection.Input;
            kontaktOsobaNaIstovaru.Value = (object)(KontaktOsobaNaIstovaru?.Trim()) ?? DBNull.Value;
            cmd.Parameters.Add(kontaktOsobaNaIstovaru);

            SqlParameter pdv = new SqlParameter();
            pdv.ParameterName = "@PDV";
            pdv.SqlDbType = SqlDbType.Int;
            pdv.Direction = ParameterDirection.Input;
            pdv.Value = PDV.HasValue ? (object)PDV.Value : DBNull.Value; 
            cmd.Parameters.Add(pdv);

            SqlParameter tipTransporta = new SqlParameter();
            tipTransporta.ParameterName = "@TipTransporta";
            tipTransporta.SqlDbType = SqlDbType.Int;
            tipTransporta.Direction = ParameterDirection.Input;
            tipTransporta.Value = TipTransporta.HasValue ? (object)TipTransporta.Value : DBNull.Value;
            cmd.Parameters.Add(tipTransporta);
    
            SqlParameter brojVoza = new SqlParameter();
            brojVoza.ParameterName = "@BrojVoza";
            brojVoza.SqlDbType = SqlDbType.NVarChar;
            brojVoza.Size = 50;
            brojVoza.Direction = ParameterDirection.Input;
            brojVoza.Value = (object)BrojVoza ?? DBNull.Value;
            cmd.Parameters.Add(brojVoza);

            SqlParameter bttoKontejnera = new SqlParameter();
            bttoKontejnera.ParameterName = "@BttoKontejnera";
            bttoKontejnera.SqlDbType = SqlDbType.Decimal;
            bttoKontejnera.Direction = ParameterDirection.Input;
            bttoKontejnera.Value = BttoKontejnera.HasValue ? (object)BttoKontejnera.Value : DBNull.Value;
            cmd.Parameters.Add(bttoKontejnera);


            SqlParameter bttoRobe = new SqlParameter();
            bttoRobe.ParameterName = "@BttoRobe";
            bttoRobe.SqlDbType = SqlDbType.Decimal;
            bttoRobe.Direction = ParameterDirection.Input;
            bttoRobe.Value = BttoRobe.HasValue ? (object)BttoRobe.Value : DBNull.Value;
            cmd.Parameters.Add(bttoRobe);

            SqlParameter bookingBrodara = new SqlParameter();
            bookingBrodara.ParameterName = "@BookingBrodara";
            bookingBrodara.SqlDbType = SqlDbType.Int;
            bookingBrodara.Direction = ParameterDirection.Input;
            bookingBrodara.Value = BookingBrodara.HasValue ? (object)BookingBrodara.Value : DBNull.Value;
            cmd.Parameters.Add(bookingBrodara);


            SqlParameter brodskaTeretnica = new SqlParameter();
            brodskaTeretnica.ParameterName = "@BrodskaTeretnica";
            brodskaTeretnica.SqlDbType = SqlDbType.NVarChar;
            brodskaTeretnica.Size = 50;
            brodskaTeretnica.Direction = ParameterDirection.Input;
            brodskaTeretnica.Value = (object)BrodskaTeretnica ?? DBNull.Value;
            cmd.Parameters.Add(brodskaTeretnica);

            SqlParameter brodskaPlomba = new SqlParameter();
            brodskaPlomba.ParameterName = "@BrodskaPlomba";
            brodskaPlomba.SqlDbType = SqlDbType.NVarChar;
            brodskaPlomba.Size = 50;
            brodskaPlomba.Direction = ParameterDirection.Input;
            brodskaPlomba.Value = (object)BrodskaPlomba ?? DBNull.Value;
            cmd.Parameters.Add(brodskaPlomba);

            SqlParameter napomenaPoz = new SqlParameter();
            napomenaPoz.ParameterName = "@NapomenaPoz";
            napomenaPoz.SqlDbType = SqlDbType.NVarChar;
            napomenaPoz.Size = 100;
            napomenaPoz.Direction = ParameterDirection.Input;
            napomenaPoz.Value = (object)NapomenaPoz ?? DBNull.Value;
            cmd.Parameters.Add(napomenaPoz);

            SqlParameter idParam = new SqlParameter("@IDPom", SqlDbType.Int);
            idParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(idParam);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                error = false;
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
                IDPom = (int)cmd.Parameters["@IDPom"].Value;

            }
            catch (SqlException ex)
            {
                throw new Exception("Neuspešan upis");
                //MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //tran.Rollback(); // Ne zaboravi i rollback
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    //MessageBox.Show("Kreiranje stavke je uspešno završeno", "",
                    //MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
            return IDPom;
        }

        public void DelRadniNalogDrumski(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteRadniNalogDrumski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis ");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }


        public void UpdateRadniNalogDrumskiPoslataNajava(int ID, int? NajavuPoslaoKorisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRadniNalogDrumskiPoslataNajava";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter iD = new SqlParameter();
            iD.ParameterName = "@ID";
            iD.SqlDbType = SqlDbType.Int;
            iD.Direction = ParameterDirection.Input;
            iD.Value = ID;
            cmd.Parameters.Add(iD);

            SqlParameter korisnikID = new SqlParameter();
            korisnikID.ParameterName = "@NajavuPoslaoKorisnik";
            korisnikID.SqlDbType = SqlDbType.Int;
            korisnikID.Direction = ParameterDirection.Input;
            korisnikID.Value = NajavuPoslaoKorisnik.HasValue ? (object)NajavuPoslaoKorisnik.Value : DBNull.Value;
            cmd.Parameters.Add(korisnikID);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
            }
            catch (SqlException)
            {
                throw new Exception("Neuspešan upis");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Ažuriranje radnog naloga broja je uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }

        public void SnimiUFajlBazu(int RadniNalogDrumskiID, string NazivFajla, string Putanja, int DodaoKorisnik)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertDokumentaRadniNalogDrumski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter iD = new SqlParameter();
            iD.ParameterName = "@RadniNalogDrumskiID";
            iD.SqlDbType = SqlDbType.Int;
            iD.Direction = ParameterDirection.Input;
            iD.Value = RadniNalogDrumskiID;
            cmd.Parameters.Add(iD);

            SqlParameter nazivFajla = new SqlParameter();
            nazivFajla.ParameterName = "@NazivFajla";
            nazivFajla.SqlDbType = SqlDbType.NVarChar;
            nazivFajla.Size = 225;
            nazivFajla.Direction = ParameterDirection.Input;
            nazivFajla.Value = (object)NazivFajla ?? DBNull.Value;
            cmd.Parameters.Add(nazivFajla);

            SqlParameter putanja = new SqlParameter();
            putanja.ParameterName = "@Putanja";
            putanja.SqlDbType = SqlDbType.NVarChar;
            putanja.Size = 500;
            putanja.Direction = ParameterDirection.Input;
            putanja.Value = (object)Putanja ?? DBNull.Value;
            cmd.Parameters.Add(putanja);

            SqlParameter dodaoKorisnik = new SqlParameter();
            dodaoKorisnik.ParameterName = "@DodaoKorisnik";
            dodaoKorisnik.SqlDbType = SqlDbType.Int;
            dodaoKorisnik.Direction = ParameterDirection.Input;
            dodaoKorisnik.Value = DodaoKorisnik;
            cmd.Parameters.Add(dodaoKorisnik);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
            }
            catch (SqlException ex)
            {
                //throw new Exception("Neuspešan upis");
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tran.Rollback(); // Ne zaboravi i rollback
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Ažuriranje radnog naloga broja je uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }
        

    }
}
