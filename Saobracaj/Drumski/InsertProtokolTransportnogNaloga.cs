using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    class InsertProtokolTransportnogNaloga
    {

        public int InsertProtokol(int? RadniNalogDrumskiID, int? TipProtokola, int? TipTransporta, int? PolaznaCarinarnica, int? PolaznaSpedicija,string PolaznaSpedicijaKontakt,
                string PolaznaSpedicijaKontaktNovi, int? OdredisnaCarinarnica, int? OdredisnaSpedicija, string OdredisnaSpedicijaKontakt, string OdredisnaSpedicijaKontaktNovi, int? MestoUtovara,
                string AdresaUtovara, string KontaktOsobaNaUtovaru, DateTime? DatumUtovara, DateTime?  DtNoviUtovaraKontejnera, int? MestoSpustanjaPunog, DateTime? DatSpustanja, DateTime? DtNoviSpustanja,
                int? MestoPreuzimanjaKontejnera, DateTime?  DtPreuzimanjaPraznog, DateTime? DtNoviPreuzimanjaPraznog, string ProtokolKreirao, decimal? Trosak, decimal? Cena, string Opis, int? Situacija,
                int? MestoUtovaraCerade, string AdresaUtovaraCerade,
                string KontaktUtovaraCerade, int? MestoIstovaraCerade, string AdresaIstovaraCerade, string KontaktIstovaraCerade, DateTime? DtUtovaraCerade, DateTime? DtUtovaraCeradeNovi, DateTime? DtRealizacijeUtovaraCerade,
                DateTime? DtIstovaraCerade, DateTime? DtIstovaraCeradeNovi, DateTime? DtRealizacijeIstovaraCerade)
        {
            int IDPom = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertProtokolRadniNalogDrumski";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter radniNalogDrumskiID = new SqlParameter();
            radniNalogDrumskiID.ParameterName = "@RadniNalogDrumskiID";
            radniNalogDrumskiID.SqlDbType = SqlDbType.Int;
            radniNalogDrumskiID.Direction = ParameterDirection.Input;
            radniNalogDrumskiID.Value = RadniNalogDrumskiID.HasValue ? (object)RadniNalogDrumskiID.Value : DBNull.Value; ;
            myCommand.Parameters.Add(radniNalogDrumskiID);


            SqlParameter tipProtokola = new SqlParameter();
            tipProtokola.ParameterName = "@TipProtokola";
            tipProtokola.SqlDbType = SqlDbType.Int;
            tipProtokola.Direction = ParameterDirection.Input;
            tipProtokola.Value = TipProtokola.HasValue ? (object)TipProtokola.Value : DBNull.Value; ;
            myCommand.Parameters.Add(tipProtokola);

            SqlParameter tipTransporta = new SqlParameter();
            tipTransporta.ParameterName = "@TipTransporta";
            tipTransporta.SqlDbType = SqlDbType.Int;
            tipTransporta.Direction = ParameterDirection.Input;
            tipTransporta.Value = TipTransporta.HasValue ? (object)TipTransporta.Value : DBNull.Value; ;
            myCommand.Parameters.Add(tipTransporta);


            SqlParameter polaznaCarinarnica = new SqlParameter();
            polaznaCarinarnica.ParameterName = "@PolaznaCarinarnica";
            polaznaCarinarnica.SqlDbType = SqlDbType.Int;
            polaznaCarinarnica.Direction = ParameterDirection.Input;
            polaznaCarinarnica.Value = PolaznaCarinarnica.HasValue ? (object)PolaznaCarinarnica.Value : DBNull.Value; ;
            myCommand.Parameters.Add(polaznaCarinarnica);

            SqlParameter polaznaSpedicija = new SqlParameter();
            polaznaSpedicija.ParameterName = "@PolaznaSpedicija";
            polaznaSpedicija.SqlDbType = SqlDbType.Int;
            polaznaSpedicija.Direction = ParameterDirection.Input;
            polaznaSpedicija.Value = PolaznaSpedicija.HasValue ? (object)PolaznaSpedicija.Value : DBNull.Value; ;
            myCommand.Parameters.Add(polaznaSpedicija);

            SqlParameter polaznaSpedicijaKontakt = new SqlParameter();
            polaznaSpedicijaKontakt.ParameterName = "@PolaznaSpedicijaKontakt";
            polaznaSpedicijaKontakt.SqlDbType = SqlDbType.NVarChar;
            polaznaSpedicijaKontakt.Size = 100;
            polaznaSpedicijaKontakt.Direction = ParameterDirection.Input;
            polaznaSpedicijaKontakt.Value = (object)PolaznaSpedicijaKontakt ?? DBNull.Value;
            myCommand.Parameters.Add(polaznaSpedicijaKontakt);

            SqlParameter polaznaSpedicijaKontaktNovi = new SqlParameter();
            polaznaSpedicijaKontaktNovi.ParameterName = "@PolaznaSpedicijaKontaktNovi";
            polaznaSpedicijaKontaktNovi.SqlDbType = SqlDbType.NVarChar;
            polaznaSpedicijaKontaktNovi.Size = 100;
            polaznaSpedicijaKontaktNovi.Direction = ParameterDirection.Input;
            polaznaSpedicijaKontaktNovi.Value = (object)PolaznaSpedicijaKontaktNovi ?? DBNull.Value;
            myCommand.Parameters.Add(polaznaSpedicijaKontaktNovi);

            SqlParameter odredisnaCarinarnica = new SqlParameter();
            odredisnaCarinarnica.ParameterName = "@OdredisnaCarinarnica";
            odredisnaCarinarnica.SqlDbType = SqlDbType.Int;
            odredisnaCarinarnica.Direction = ParameterDirection.Input;
            odredisnaCarinarnica.Value = OdredisnaCarinarnica.HasValue ? (object)OdredisnaCarinarnica.Value : DBNull.Value; ;
            myCommand.Parameters.Add(odredisnaCarinarnica);

            SqlParameter odredisnaSpedicija = new SqlParameter();
            odredisnaSpedicija.ParameterName = "@OdredisnaSpedicija";
            odredisnaSpedicija.SqlDbType = SqlDbType.Int;
            odredisnaSpedicija.Direction = ParameterDirection.Input;
            odredisnaSpedicija.Value = OdredisnaSpedicija.HasValue ? (object)OdredisnaSpedicija.Value : DBNull.Value; ;
            myCommand.Parameters.Add(odredisnaSpedicija);

            SqlParameter odredisnaSpedicijaKontakt = new SqlParameter();
            odredisnaSpedicijaKontakt.ParameterName = "@OdredisnaSpedicijaKontakt";
            odredisnaSpedicijaKontakt.SqlDbType = SqlDbType.NVarChar;
            odredisnaSpedicijaKontakt.Size = 100;
            odredisnaSpedicijaKontakt.Direction = ParameterDirection.Input;
            odredisnaSpedicijaKontakt.Value = (object)OdredisnaSpedicijaKontakt ?? DBNull.Value;
            myCommand.Parameters.Add(odredisnaSpedicijaKontakt);

            SqlParameter odredisnaSpedicijaKontaktNovi = new SqlParameter();
            odredisnaSpedicijaKontaktNovi.ParameterName = "@OdredisnaSpedicijaKontaktNovi";
            odredisnaSpedicijaKontaktNovi.SqlDbType = SqlDbType.NVarChar;
            odredisnaSpedicijaKontaktNovi.Size = 100;
            odredisnaSpedicijaKontaktNovi.Direction = ParameterDirection.Input;
            odredisnaSpedicijaKontaktNovi.Value = (object)OdredisnaSpedicijaKontaktNovi ?? DBNull.Value;
            myCommand.Parameters.Add(odredisnaSpedicijaKontaktNovi);

            SqlParameter mestoUtovara = new SqlParameter();
            mestoUtovara.ParameterName = "@MestoUtovara";
            mestoUtovara.SqlDbType = SqlDbType.Int;
            mestoUtovara.Direction = ParameterDirection.Input;
            mestoUtovara.Value = MestoUtovara.HasValue ? (object)MestoUtovara.Value : DBNull.Value; ;
            myCommand.Parameters.Add(mestoUtovara);

            SqlParameter adresaUtovara = new SqlParameter();
            adresaUtovara.ParameterName = "@AdresaUtovara";
            adresaUtovara.SqlDbType = SqlDbType.NVarChar;
            adresaUtovara.Size = 100;
            adresaUtovara.Direction = ParameterDirection.Input;
            adresaUtovara.Value = (object)AdresaUtovara ?? DBNull.Value;
            myCommand.Parameters.Add(adresaUtovara);

            SqlParameter kontaktOsobaNaUtovaru = new SqlParameter();
            kontaktOsobaNaUtovaru.ParameterName = "@KontaktOsobaNaUtovaru";
            kontaktOsobaNaUtovaru.SqlDbType = SqlDbType.NVarChar;
            kontaktOsobaNaUtovaru.Size = 100;
            kontaktOsobaNaUtovaru.Direction = ParameterDirection.Input;
            kontaktOsobaNaUtovaru.Value = (object)KontaktOsobaNaUtovaru ?? DBNull.Value;
            myCommand.Parameters.Add(kontaktOsobaNaUtovaru);


            SqlParameter datumUtovara = new SqlParameter();
            datumUtovara.ParameterName = "@DatumUtovara";
            datumUtovara.SqlDbType = SqlDbType.DateTime;
            datumUtovara.Direction = ParameterDirection.Input;
            datumUtovara.Value = DatumUtovara.HasValue ? (object)DatumUtovara.Value : DBNull.Value;
            myCommand.Parameters.Add(datumUtovara);


            SqlParameter dtNoviUtovaraKontejnera = new SqlParameter();
            dtNoviUtovaraKontejnera.ParameterName = "@DtNoviUtovaraKontejnera";
            dtNoviUtovaraKontejnera.SqlDbType = SqlDbType.DateTime;
            dtNoviUtovaraKontejnera.Direction = ParameterDirection.Input;
            dtNoviUtovaraKontejnera.Value = DtNoviUtovaraKontejnera.HasValue ? (object)DtNoviUtovaraKontejnera.Value : DBNull.Value;
            myCommand.Parameters.Add(dtNoviUtovaraKontejnera);

            SqlParameter mestoSpustanjaPunog = new SqlParameter();
            mestoSpustanjaPunog.ParameterName = "@MestoSpustanjaPunog";
            mestoSpustanjaPunog.SqlDbType = SqlDbType.Int;
            mestoSpustanjaPunog.Direction = ParameterDirection.Input;
            mestoSpustanjaPunog.Value = MestoSpustanjaPunog.HasValue ? (object)MestoSpustanjaPunog.Value : DBNull.Value; ;
            myCommand.Parameters.Add(mestoSpustanjaPunog);

            SqlParameter datSpustanja = new SqlParameter();
            datSpustanja.ParameterName = "@DatSpustanja";
            datSpustanja.SqlDbType = SqlDbType.DateTime;
            datSpustanja.Direction = ParameterDirection.Input;
            datSpustanja.Value = DatSpustanja.HasValue ? (object)DatSpustanja.Value : DBNull.Value;
            myCommand.Parameters.Add(datSpustanja);

            SqlParameter dtNoviSpustanja = new SqlParameter();
            dtNoviSpustanja.ParameterName = "@DtNoviSpustanja";
            dtNoviSpustanja.SqlDbType = SqlDbType.DateTime;
            dtNoviSpustanja.Direction = ParameterDirection.Input;
            dtNoviSpustanja.Value = DtNoviSpustanja.HasValue ? (object)DtNoviSpustanja.Value : DBNull.Value;
            myCommand.Parameters.Add(dtNoviSpustanja);
            

            SqlParameter mestoPreuzimanjaKontejnera = new SqlParameter();
            mestoPreuzimanjaKontejnera.ParameterName = "@MestoPreuzimanjaKontejnera";
            mestoPreuzimanjaKontejnera.SqlDbType = SqlDbType.Int;
            mestoPreuzimanjaKontejnera.Direction = ParameterDirection.Input;
            mestoPreuzimanjaKontejnera.Value = MestoPreuzimanjaKontejnera.HasValue ? (object)MestoPreuzimanjaKontejnera.Value : DBNull.Value; ;
            myCommand.Parameters.Add(mestoPreuzimanjaKontejnera);

            SqlParameter dtPreuzimanjaPraznog = new SqlParameter();
            dtPreuzimanjaPraznog.ParameterName = "@DtPreuzimanjaPraznog";
            dtPreuzimanjaPraznog.SqlDbType = SqlDbType.DateTime;
            dtPreuzimanjaPraznog.Direction = ParameterDirection.Input;
            dtPreuzimanjaPraznog.Value = DtPreuzimanjaPraznog.HasValue ? (object)DtPreuzimanjaPraznog.Value : DBNull.Value;
            myCommand.Parameters.Add(dtPreuzimanjaPraznog);

            SqlParameter dtNoviPreuzimanjaPraznog = new SqlParameter();
            dtNoviPreuzimanjaPraznog.ParameterName = "@DtNoviPreuzimanjaPraznog";
            dtNoviPreuzimanjaPraznog.SqlDbType = SqlDbType.DateTime;
            dtNoviPreuzimanjaPraznog.Direction = ParameterDirection.Input;
            dtNoviPreuzimanjaPraznog.Value = DtNoviPreuzimanjaPraznog.HasValue ? (object)DtNoviPreuzimanjaPraznog.Value : DBNull.Value;
            myCommand.Parameters.Add(dtNoviPreuzimanjaPraznog);

            SqlParameter protokolKreirao = new SqlParameter();
            protokolKreirao.ParameterName = "@ProtokolKreirao";
            protokolKreirao.SqlDbType = SqlDbType.NVarChar;
            protokolKreirao.Size = 100;
            protokolKreirao.Direction = ParameterDirection.Input;
            protokolKreirao.Value = (object)ProtokolKreirao ?? DBNull.Value;
            myCommand.Parameters.Add(protokolKreirao);

            SqlParameter trosak = new SqlParameter();
            trosak.ParameterName = "@Trosak";
            trosak.SqlDbType = SqlDbType.Decimal;
            trosak.Direction = ParameterDirection.Input;
            trosak.Value = Trosak.HasValue ? (object)Trosak.Value : DBNull.Value;
            myCommand.Parameters.Add(trosak);

            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Decimal;
            cena.Direction = ParameterDirection.Input;
            cena.Value = Cena.HasValue ? (object)Cena.Value : DBNull.Value;
            myCommand.Parameters.Add(cena);

            SqlParameter opis = new SqlParameter();
            opis.ParameterName = "@Opis";
            opis.SqlDbType = SqlDbType.NVarChar;
            opis.Size = 500;
            opis.Direction = ParameterDirection.Input;
            opis.Value = (object)Opis ?? DBNull.Value;
            myCommand.Parameters.Add(opis);

            SqlParameter situacija = new SqlParameter();
            situacija.ParameterName = "@Situacija";
            situacija.SqlDbType = SqlDbType.Int;
            situacija.Direction = ParameterDirection.Input;
            situacija.Value = Situacija.HasValue ? (object)Situacija.Value : DBNull.Value; ;
            myCommand.Parameters.Add(situacija);

            SqlParameter mestoUtovaraCerade = new SqlParameter();
            mestoUtovaraCerade.ParameterName = "@MestoUtovaraCerade";
            mestoUtovaraCerade.SqlDbType = SqlDbType.Int;
            mestoUtovaraCerade.Direction = ParameterDirection.Input;
            mestoUtovaraCerade.Value = MestoUtovaraCerade.HasValue ? (object)MestoUtovaraCerade.Value : DBNull.Value; ;
            myCommand.Parameters.Add(mestoUtovaraCerade);

            SqlParameter adresaUtovaraCereade = new SqlParameter();
            adresaUtovaraCereade.ParameterName = "@AdresaUtovaraCerade";
            adresaUtovaraCereade.SqlDbType = SqlDbType.NVarChar;
            adresaUtovaraCereade.Size = 100;
            adresaUtovaraCereade.Direction = ParameterDirection.Input;
            adresaUtovaraCereade.Value = (object)AdresaUtovaraCerade ?? DBNull.Value;
            myCommand.Parameters.Add(adresaUtovaraCereade);

            SqlParameter kontaktOsobaNaUtovaruCerade = new SqlParameter();
            kontaktOsobaNaUtovaruCerade.ParameterName = "@KontaktUtovaraCerade";
            kontaktOsobaNaUtovaruCerade.SqlDbType = SqlDbType.NVarChar;
            kontaktOsobaNaUtovaruCerade.Size = 100;
            kontaktOsobaNaUtovaruCerade.Direction = ParameterDirection.Input;
            kontaktOsobaNaUtovaruCerade.Value = (object)KontaktUtovaraCerade ?? DBNull.Value;
            myCommand.Parameters.Add(kontaktOsobaNaUtovaruCerade);

            SqlParameter mestoIstovaraCerade = new SqlParameter();
            mestoIstovaraCerade.ParameterName = "@MestoIstovaraCerade";
            mestoIstovaraCerade.SqlDbType = SqlDbType.Int;
            mestoIstovaraCerade.Direction = ParameterDirection.Input;
            mestoIstovaraCerade.Value = MestoIstovaraCerade.HasValue ? (object)MestoIstovaraCerade.Value : DBNull.Value; ;
            myCommand.Parameters.Add(mestoIstovaraCerade);

            SqlParameter adresaIstovaraCereade = new SqlParameter();
            adresaIstovaraCereade.ParameterName = "@AdresaIstovaraCerade";
            adresaIstovaraCereade.SqlDbType = SqlDbType.NVarChar;
            adresaIstovaraCereade.Size = 100;
            adresaIstovaraCereade.Direction = ParameterDirection.Input;
            adresaIstovaraCereade.Value = (object)AdresaIstovaraCerade ?? DBNull.Value;
            myCommand.Parameters.Add(adresaIstovaraCereade);

            SqlParameter kontaktOsobaNaIstovaruCerade = new SqlParameter();
            kontaktOsobaNaIstovaruCerade.ParameterName = "@KontaktIstovaraCerade";
            kontaktOsobaNaIstovaruCerade.SqlDbType = SqlDbType.NVarChar;
            kontaktOsobaNaIstovaruCerade.Size = 100;
            kontaktOsobaNaIstovaruCerade.Direction = ParameterDirection.Input;
            kontaktOsobaNaIstovaruCerade.Value = (object)KontaktIstovaraCerade ?? DBNull.Value;
            myCommand.Parameters.Add(kontaktOsobaNaIstovaruCerade);

            SqlParameter dtUtovaraCerade = new SqlParameter();
            dtUtovaraCerade.ParameterName = "@DtUtovaraCerade";
            dtUtovaraCerade.SqlDbType = SqlDbType.DateTime;
            dtUtovaraCerade.Direction = ParameterDirection.Input;
            dtUtovaraCerade.Value = DtUtovaraCerade.HasValue ? (object)DtUtovaraCerade.Value : DBNull.Value;
            myCommand.Parameters.Add(dtUtovaraCerade);

            SqlParameter dtUtovaraCeradeeNovi = new SqlParameter();
            dtUtovaraCeradeeNovi.ParameterName = "@DtUtovaraCeradeNovi";
            dtUtovaraCeradeeNovi.SqlDbType = SqlDbType.DateTime;
            dtUtovaraCeradeeNovi.Direction = ParameterDirection.Input;
            dtUtovaraCeradeeNovi.Value = DtUtovaraCeradeNovi.HasValue ? (object)DtUtovaraCeradeNovi.Value : DBNull.Value;
            myCommand.Parameters.Add(dtUtovaraCeradeeNovi);

            SqlParameter dtRealizacijeUtovaraCerade = new SqlParameter();
            dtRealizacijeUtovaraCerade.ParameterName = "@DtRealizacijeUtovaraCerade";
            dtRealizacijeUtovaraCerade.SqlDbType = SqlDbType.DateTime;
            dtRealizacijeUtovaraCerade.Direction = ParameterDirection.Input;
            dtRealizacijeUtovaraCerade.Value = DtRealizacijeUtovaraCerade.HasValue ? (object)DtRealizacijeUtovaraCerade.Value : DBNull.Value;
            myCommand.Parameters.Add(dtRealizacijeUtovaraCerade);

            SqlParameter dtIstovaraCerade = new SqlParameter();
            dtIstovaraCerade.ParameterName = "@DtIstovaraCerade";
            dtIstovaraCerade.SqlDbType = SqlDbType.DateTime;
            dtIstovaraCerade.Direction = ParameterDirection.Input;
            dtIstovaraCerade.Value = DtIstovaraCerade.HasValue ? (object)DtIstovaraCerade.Value : DBNull.Value;
            myCommand.Parameters.Add(dtIstovaraCerade);


            SqlParameter dtIstovaraCeradeeNovi = new SqlParameter();
            dtIstovaraCeradeeNovi.ParameterName = "@DtIstovaraCeradeNovi";
            dtIstovaraCeradeeNovi.SqlDbType = SqlDbType.DateTime;
            dtIstovaraCeradeeNovi.Direction = ParameterDirection.Input;
            dtIstovaraCeradeeNovi.Value = DtIstovaraCeradeNovi.HasValue ? (object)DtIstovaraCeradeNovi.Value : DBNull.Value;
            myCommand.Parameters.Add(dtIstovaraCeradeeNovi);

            SqlParameter dtRealizacijeIstovaraCerade = new SqlParameter();
            dtRealizacijeIstovaraCerade.ParameterName = "@DtRealizacijeIstovaraCerade";
            dtRealizacijeIstovaraCerade.SqlDbType = SqlDbType.DateTime;
            dtRealizacijeIstovaraCerade.Direction = ParameterDirection.Input;
            dtRealizacijeIstovaraCerade.Value = DtRealizacijeIstovaraCerade.HasValue ? (object)DtRealizacijeIstovaraCerade.Value : DBNull.Value;
            myCommand.Parameters.Add(dtRealizacijeIstovaraCerade);

            SqlParameter idParam = new SqlParameter("@IDPom", SqlDbType.Int);
            idParam.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(idParam);


            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = false;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                IDPom = (int)myCommand.Parameters["@IDPom"].Value;
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException ex)
            {
                throw new Exception("Neuspešan upis");
                //MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //myTransaction.Rollback(); // Ne zaboravi i rollback
            }

            finally
            {

                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Protokol je uspešno kreiran.", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
            return IDPom;
        }

        public void UpdateProtokol(int? ID, int? PolaznaCarinarnica, int? PolaznaSpedicija, string PolaznaSpedicijaKontakt,
              string PolaznaSpedicijaKontaktNovi, int? OdredisnaCarinarnica, int? OdredisnaSpedicija, string OdredisnaSpedicijaKontakt, string OdredisnaSpedicijaKontaktNovi, int? MestoUtovara,
              string AdresaUtovara, string KontaktOsobaNaUtovaru, DateTime? DatumUtovara, DateTime? DtNoviUtovaraKontejnera, int? MestoSpustanjaPunog, DateTime? DatSpustanja, DateTime? DtNoviSpustanja,
              int? MestoPreuzimanjaKontejnera, DateTime? DtPreuzimanjaPraznog, DateTime? DtNoviPreuzimanjaPraznog,  decimal? Trosak, decimal? Cena, string Opis, int? Situacija,
              int? MestoUtovaraCerade, string AdresaUtovaraCerade,
              string KontaktUtovaraCerade, int? MestoIstovaraCerade, string AdresaIstovaraCerade, string KontaktIstovaraCerade, DateTime? DtUtovaraCerade, DateTime? DtUtovaraCeradeNovi, DateTime? DtRealizacijeUtovaraCerade,
              DateTime? DtIstovaraCerade, DateTime? DtIstovaraCeradeNovi, DateTime? DtRealizacijeIstovaraCerade)
        {
           
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateProtokolRadniNalogDrumski";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID.HasValue ? (object)ID.Value : DBNull.Value; ;
            myCommand.Parameters.Add(id);

            SqlParameter polaznaCarinarnica = new SqlParameter();
            polaznaCarinarnica.ParameterName = "@PolaznaCarinarnica";
            polaznaCarinarnica.SqlDbType = SqlDbType.Int;
            polaznaCarinarnica.Direction = ParameterDirection.Input;
            polaznaCarinarnica.Value = PolaznaCarinarnica.HasValue ? (object)PolaznaCarinarnica.Value : DBNull.Value; ;
            myCommand.Parameters.Add(polaznaCarinarnica);

            SqlParameter polaznaSpedicija = new SqlParameter();
            polaznaSpedicija.ParameterName = "@PolaznaSpedicija";
            polaznaSpedicija.SqlDbType = SqlDbType.Int;
            polaznaSpedicija.Direction = ParameterDirection.Input;
            polaznaSpedicija.Value = PolaznaSpedicija.HasValue ? (object)PolaznaSpedicija.Value : DBNull.Value; ;
            myCommand.Parameters.Add(polaznaSpedicija);

            SqlParameter polaznaSpedicijaKontakt = new SqlParameter();
            polaznaSpedicijaKontakt.ParameterName = "@PolaznaSpedicijaKontakt";
            polaznaSpedicijaKontakt.SqlDbType = SqlDbType.NVarChar;
            polaznaSpedicijaKontakt.Size = 100;
            polaznaSpedicijaKontakt.Direction = ParameterDirection.Input;
            polaznaSpedicijaKontakt.Value = (object)PolaznaSpedicijaKontakt ?? DBNull.Value;
            myCommand.Parameters.Add(polaznaSpedicijaKontakt);

            SqlParameter polaznaSpedicijaKontaktNovi = new SqlParameter();
            polaznaSpedicijaKontaktNovi.ParameterName = "@PolaznaSpedicijaKontaktNovi";
            polaznaSpedicijaKontaktNovi.SqlDbType = SqlDbType.NVarChar;
            polaznaSpedicijaKontaktNovi.Size = 100;
            polaznaSpedicijaKontaktNovi.Direction = ParameterDirection.Input;
            polaznaSpedicijaKontaktNovi.Value = (object)PolaznaSpedicijaKontaktNovi ?? DBNull.Value;
            myCommand.Parameters.Add(polaznaSpedicijaKontaktNovi);

            SqlParameter odredisnaCarinarnica = new SqlParameter();
            odredisnaCarinarnica.ParameterName = "@OdredisnaCarinarnica";
            odredisnaCarinarnica.SqlDbType = SqlDbType.Int;
            odredisnaCarinarnica.Direction = ParameterDirection.Input;
            odredisnaCarinarnica.Value = OdredisnaCarinarnica.HasValue ? (object)OdredisnaCarinarnica.Value : DBNull.Value; ;
            myCommand.Parameters.Add(odredisnaCarinarnica);

            SqlParameter odredisnaSpedicija = new SqlParameter();
            odredisnaSpedicija.ParameterName = "@OdredisnaSpedicija";
            odredisnaSpedicija.SqlDbType = SqlDbType.Int;
            odredisnaSpedicija.Direction = ParameterDirection.Input;
            odredisnaSpedicija.Value = OdredisnaSpedicija.HasValue ? (object)OdredisnaSpedicija.Value : DBNull.Value; ;
            myCommand.Parameters.Add(odredisnaSpedicija);

            SqlParameter odredisnaSpedicijaKontakt = new SqlParameter();
            odredisnaSpedicijaKontakt.ParameterName = "@OdredisnaSpedicijaKontakt";
            odredisnaSpedicijaKontakt.SqlDbType = SqlDbType.NVarChar;
            odredisnaSpedicijaKontakt.Size = 100;
            odredisnaSpedicijaKontakt.Direction = ParameterDirection.Input;
            odredisnaSpedicijaKontakt.Value = (object)OdredisnaSpedicijaKontakt ?? DBNull.Value;
            myCommand.Parameters.Add(odredisnaSpedicijaKontakt);

            SqlParameter odredisnaSpedicijaKontaktNovi = new SqlParameter();
            odredisnaSpedicijaKontaktNovi.ParameterName = "@OdredisnaSpedicijaKontaktNovi";
            odredisnaSpedicijaKontaktNovi.SqlDbType = SqlDbType.NVarChar;
            odredisnaSpedicijaKontaktNovi.Size = 100;
            odredisnaSpedicijaKontaktNovi.Direction = ParameterDirection.Input;
            odredisnaSpedicijaKontaktNovi.Value = (object)OdredisnaSpedicijaKontaktNovi ?? DBNull.Value;
            myCommand.Parameters.Add(odredisnaSpedicijaKontaktNovi);

            SqlParameter mestoUtovara = new SqlParameter();
            mestoUtovara.ParameterName = "@MestoUtovara";
            mestoUtovara.SqlDbType = SqlDbType.Int;
            mestoUtovara.Direction = ParameterDirection.Input;
            mestoUtovara.Value = MestoUtovara.HasValue ? (object)MestoUtovara.Value : DBNull.Value; ;
            myCommand.Parameters.Add(mestoUtovara);

            SqlParameter adresaUtovara = new SqlParameter();
            adresaUtovara.ParameterName = "@AdresaUtovara";
            adresaUtovara.SqlDbType = SqlDbType.NVarChar;
            adresaUtovara.Size = 100;
            adresaUtovara.Direction = ParameterDirection.Input;
            adresaUtovara.Value = (object)AdresaUtovara ?? DBNull.Value;
            myCommand.Parameters.Add(adresaUtovara);

            SqlParameter kontaktOsobaNaUtovaru = new SqlParameter();
            kontaktOsobaNaUtovaru.ParameterName = "@KontaktOsobaNaUtovaru";
            kontaktOsobaNaUtovaru.SqlDbType = SqlDbType.NVarChar;
            kontaktOsobaNaUtovaru.Size = 100;
            kontaktOsobaNaUtovaru.Direction = ParameterDirection.Input;
            kontaktOsobaNaUtovaru.Value = (object)KontaktOsobaNaUtovaru ?? DBNull.Value;
            myCommand.Parameters.Add(kontaktOsobaNaUtovaru);


            SqlParameter datumUtovara = new SqlParameter();
            datumUtovara.ParameterName = "@DatumUtovara";
            datumUtovara.SqlDbType = SqlDbType.DateTime;
            datumUtovara.Direction = ParameterDirection.Input;
            datumUtovara.Value = DatumUtovara.HasValue ? (object)DatumUtovara.Value : DBNull.Value;
            myCommand.Parameters.Add(datumUtovara);


            SqlParameter dtNoviUtovaraKontejnera = new SqlParameter();
            dtNoviUtovaraKontejnera.ParameterName = "@DtNoviUtovaraKontejnera";
            dtNoviUtovaraKontejnera.SqlDbType = SqlDbType.DateTime;
            dtNoviUtovaraKontejnera.Direction = ParameterDirection.Input;
            dtNoviUtovaraKontejnera.Value = DtNoviUtovaraKontejnera.HasValue ? (object)DtNoviUtovaraKontejnera.Value : DBNull.Value;
            myCommand.Parameters.Add(dtNoviUtovaraKontejnera);

            SqlParameter mestoSpustanjaPunog = new SqlParameter();
            mestoSpustanjaPunog.ParameterName = "@MestoSpustanjaPunog";
            mestoSpustanjaPunog.SqlDbType = SqlDbType.Int;
            mestoSpustanjaPunog.Direction = ParameterDirection.Input;
            mestoSpustanjaPunog.Value = MestoSpustanjaPunog.HasValue ? (object)MestoSpustanjaPunog.Value : DBNull.Value; ;
            myCommand.Parameters.Add(mestoSpustanjaPunog);

            SqlParameter datSpustanja = new SqlParameter();
            datSpustanja.ParameterName = "@DatSpustanja";
            datSpustanja.SqlDbType = SqlDbType.DateTime;
            datSpustanja.Direction = ParameterDirection.Input;
            datSpustanja.Value = DatSpustanja.HasValue ? (object)DatSpustanja.Value : DBNull.Value;
            myCommand.Parameters.Add(datSpustanja);

            SqlParameter dtNoviSpustanja = new SqlParameter();
            dtNoviSpustanja.ParameterName = "@DtNoviSpustanja";
            dtNoviSpustanja.SqlDbType = SqlDbType.DateTime;
            dtNoviSpustanja.Direction = ParameterDirection.Input;
            dtNoviSpustanja.Value = DtNoviSpustanja.HasValue ? (object)DtNoviSpustanja.Value : DBNull.Value;
            myCommand.Parameters.Add(dtNoviSpustanja);


            SqlParameter mestoPreuzimanjaKontejnera = new SqlParameter();
            mestoPreuzimanjaKontejnera.ParameterName = "@MestoPreuzimanjaKontejnera";
            mestoPreuzimanjaKontejnera.SqlDbType = SqlDbType.Int;
            mestoPreuzimanjaKontejnera.Direction = ParameterDirection.Input;
            mestoPreuzimanjaKontejnera.Value = MestoPreuzimanjaKontejnera.HasValue ? (object)MestoPreuzimanjaKontejnera.Value : DBNull.Value; ;
            myCommand.Parameters.Add(mestoPreuzimanjaKontejnera);

            SqlParameter dtPreuzimanjaPraznog = new SqlParameter();
            dtPreuzimanjaPraznog.ParameterName = "@DtPreuzimanjaPraznog";
            dtPreuzimanjaPraznog.SqlDbType = SqlDbType.DateTime;
            dtPreuzimanjaPraznog.Direction = ParameterDirection.Input;
            dtPreuzimanjaPraznog.Value = DtPreuzimanjaPraznog.HasValue ? (object)DtPreuzimanjaPraznog.Value : DBNull.Value;
            myCommand.Parameters.Add(dtPreuzimanjaPraznog);

            SqlParameter dtNoviPreuzimanjaPraznog = new SqlParameter();
            dtNoviPreuzimanjaPraznog.ParameterName = "@DtNoviPreuzimanjaPraznog";
            dtNoviPreuzimanjaPraznog.SqlDbType = SqlDbType.DateTime;
            dtNoviPreuzimanjaPraznog.Direction = ParameterDirection.Input;
            dtNoviPreuzimanjaPraznog.Value = DtNoviPreuzimanjaPraznog.HasValue ? (object)DtNoviPreuzimanjaPraznog.Value : DBNull.Value;
            myCommand.Parameters.Add(dtNoviPreuzimanjaPraznog);

            SqlParameter trosak = new SqlParameter();
            trosak.ParameterName = "@Trosak";
            trosak.SqlDbType = SqlDbType.Decimal;
            trosak.Direction = ParameterDirection.Input;
            trosak.Value = Trosak.HasValue ? (object)Trosak.Value : DBNull.Value;
            myCommand.Parameters.Add(trosak);

            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Decimal;
            cena.Direction = ParameterDirection.Input;
            cena.Value = Cena.HasValue ? (object)Cena.Value : DBNull.Value;
            myCommand.Parameters.Add(cena);

            SqlParameter opis = new SqlParameter();
            opis.ParameterName = "@Opis";
            opis.SqlDbType = SqlDbType.NVarChar;
            opis.Size = 500;
            opis.Direction = ParameterDirection.Input;
            opis.Value = (object)Opis ?? DBNull.Value;
            myCommand.Parameters.Add(opis);

            SqlParameter situacija = new SqlParameter();
            situacija.ParameterName = "@Situacija";
            situacija.SqlDbType = SqlDbType.Int;
            situacija.Direction = ParameterDirection.Input;
            situacija.Value = Situacija.HasValue ? (object)Situacija.Value : DBNull.Value; ;
            myCommand.Parameters.Add(situacija);

            SqlParameter mestoUtovaraCerade = new SqlParameter();
            mestoUtovaraCerade.ParameterName = "@MestoUtovaraCerade";
            mestoUtovaraCerade.SqlDbType = SqlDbType.Int;
            mestoUtovaraCerade.Direction = ParameterDirection.Input;
            mestoUtovaraCerade.Value = MestoUtovaraCerade.HasValue ? (object)MestoUtovaraCerade.Value : DBNull.Value; ;
            myCommand.Parameters.Add(mestoUtovaraCerade);

            SqlParameter adresaUtovaraCereade = new SqlParameter();
            adresaUtovaraCereade.ParameterName = "@AdresaUtovaraCerade";
            adresaUtovaraCereade.SqlDbType = SqlDbType.NVarChar;
            adresaUtovaraCereade.Size = 100;
            adresaUtovaraCereade.Direction = ParameterDirection.Input;
            adresaUtovaraCereade.Value = (object)AdresaUtovaraCerade ?? DBNull.Value;
            myCommand.Parameters.Add(adresaUtovaraCereade);

            SqlParameter kontaktOsobaNaUtovaruCerade = new SqlParameter();
            kontaktOsobaNaUtovaruCerade.ParameterName = "@KontaktUtovaraCerade";
            kontaktOsobaNaUtovaruCerade.SqlDbType = SqlDbType.NVarChar;
            kontaktOsobaNaUtovaruCerade.Size = 100;
            kontaktOsobaNaUtovaruCerade.Direction = ParameterDirection.Input;
            kontaktOsobaNaUtovaruCerade.Value = (object)KontaktUtovaraCerade ?? DBNull.Value;
            myCommand.Parameters.Add(kontaktOsobaNaUtovaruCerade);

            SqlParameter mestoIstovaraCerade = new SqlParameter();
            mestoIstovaraCerade.ParameterName = "@MestoIstovaraCerade";
            mestoIstovaraCerade.SqlDbType = SqlDbType.Int;
            mestoIstovaraCerade.Direction = ParameterDirection.Input;
            mestoIstovaraCerade.Value = MestoIstovaraCerade.HasValue ? (object)MestoIstovaraCerade.Value : DBNull.Value; ;
            myCommand.Parameters.Add(mestoIstovaraCerade);

            SqlParameter adresaIstovaraCereade = new SqlParameter();
            adresaIstovaraCereade.ParameterName = "@AdresaIstovaraCerade";
            adresaIstovaraCereade.SqlDbType = SqlDbType.NVarChar;
            adresaIstovaraCereade.Size = 100;
            adresaIstovaraCereade.Direction = ParameterDirection.Input;
            adresaIstovaraCereade.Value = (object)AdresaIstovaraCerade ?? DBNull.Value;
            myCommand.Parameters.Add(adresaIstovaraCereade);

            SqlParameter kontaktOsobaNaIstovaruCerade = new SqlParameter();
            kontaktOsobaNaIstovaruCerade.ParameterName = "@KontaktIstovaraCerade";
            kontaktOsobaNaIstovaruCerade.SqlDbType = SqlDbType.NVarChar;
            kontaktOsobaNaIstovaruCerade.Size = 100;
            kontaktOsobaNaIstovaruCerade.Direction = ParameterDirection.Input;
            kontaktOsobaNaIstovaruCerade.Value = (object)KontaktIstovaraCerade ?? DBNull.Value;
            myCommand.Parameters.Add(kontaktOsobaNaIstovaruCerade);

            SqlParameter dtUtovaraCerade = new SqlParameter();
            dtUtovaraCerade.ParameterName = "@DtUtovaraCerade";
            dtUtovaraCerade.SqlDbType = SqlDbType.DateTime;
            dtUtovaraCerade.Direction = ParameterDirection.Input;
            dtUtovaraCerade.Value = DtUtovaraCerade.HasValue ? (object)DtUtovaraCerade.Value : DBNull.Value;
            myCommand.Parameters.Add(dtUtovaraCerade);

            SqlParameter dtUtovaraCeradeeNovi = new SqlParameter();
            dtUtovaraCeradeeNovi.ParameterName = "@DtUtovaraCeradeNovi";
            dtUtovaraCeradeeNovi.SqlDbType = SqlDbType.DateTime;
            dtUtovaraCeradeeNovi.Direction = ParameterDirection.Input;
            dtUtovaraCeradeeNovi.Value = DtUtovaraCeradeNovi.HasValue ? (object)DtUtovaraCeradeNovi.Value : DBNull.Value;
            myCommand.Parameters.Add(dtUtovaraCeradeeNovi);

            SqlParameter dtRealizacijeUtovaraCerade = new SqlParameter();
            dtRealizacijeUtovaraCerade.ParameterName = "@DtRealizacijeUtovaraCerade";
            dtRealizacijeUtovaraCerade.SqlDbType = SqlDbType.DateTime;
            dtRealizacijeUtovaraCerade.Direction = ParameterDirection.Input;
            dtRealizacijeUtovaraCerade.Value = DtRealizacijeUtovaraCerade.HasValue ? (object)DtRealizacijeUtovaraCerade.Value : DBNull.Value;
            myCommand.Parameters.Add(dtRealizacijeUtovaraCerade);

            SqlParameter dtIstovaraCerade = new SqlParameter();
            dtIstovaraCerade.ParameterName = "@DtIstovaraCerade";
            dtIstovaraCerade.SqlDbType = SqlDbType.DateTime;
            dtIstovaraCerade.Direction = ParameterDirection.Input;
            dtIstovaraCerade.Value = DtIstovaraCerade.HasValue ? (object)DtIstovaraCerade.Value : DBNull.Value;
            myCommand.Parameters.Add(dtIstovaraCerade);


            SqlParameter dtIstovaraCeradeeNovi = new SqlParameter();
            dtIstovaraCeradeeNovi.ParameterName = "@DtIstovaraCeradeNovi";
            dtIstovaraCeradeeNovi.SqlDbType = SqlDbType.DateTime;
            dtIstovaraCeradeeNovi.Direction = ParameterDirection.Input;
            dtIstovaraCeradeeNovi.Value = DtIstovaraCeradeNovi.HasValue ? (object)DtIstovaraCeradeNovi.Value : DBNull.Value;
            myCommand.Parameters.Add(dtIstovaraCeradeeNovi);

            SqlParameter dtRealizacijeIstovaraCerade = new SqlParameter();
            dtRealizacijeIstovaraCerade.ParameterName = "@DtRealizacijeIstovaraCerade";
            dtRealizacijeIstovaraCerade.SqlDbType = SqlDbType.DateTime;
            dtRealizacijeIstovaraCerade.Direction = ParameterDirection.Input;
            dtRealizacijeIstovaraCerade.Value = DtRealizacijeIstovaraCerade.HasValue ? (object)DtRealizacijeIstovaraCerade.Value : DBNull.Value;
            myCommand.Parameters.Add(dtRealizacijeIstovaraCerade);


            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
              
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException ex)
            {
                //throw new Exception("Neuspešan upis");
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                myTransaction.Rollback(); // Ne zaboravi i rollback
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Neuspešna promena podataka", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
           
        }

        public void DelProtokolRadniNalogDrumski(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection conn = new SqlConnection(s_connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteProtokolRadniNalogDrumski";
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
    }
}
