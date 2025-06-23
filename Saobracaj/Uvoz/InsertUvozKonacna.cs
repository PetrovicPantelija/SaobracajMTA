using Syncfusion.Windows.Forms.Diagram;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    class InsertUvozKonacna
    {
        string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        public void InsUvozKonacna(int ID, int IDNadredjeni, DateTime ETABroda, DateTime ATABroda, string Status, string BrojKont, int TipKont, DateTime DobijenNalog, string DobijeBZ, string Napomena,
            string PIN, int DirigacijaKont, int NazivBroda, string BTeretnica, int ADR, int Vlasnik, int Buking, string Nalogodavac, string VrstaUsluge, int Uvoznik, int NHM,
            int NazivRobe, int SpedicijaGranicna, int SpedicijaRTC, int CarinskiPostupak, int PostupakRoba, int NacinPakovanja, int OdredisnaCarina, int OdredisnaSpedicija,
            int MestoIstovara, int KontaktOsoba, string Mail, string Plomba1, string Plomba2, decimal NetoRoba, decimal BrutoRoba, decimal TaraKont, decimal BrutoKont,
            int NapomenaPoz, DateTime ATAOtpreme, int BrojVoza, string Relacija, DateTime ATADolazak, decimal Koleta, int RLTerminali
            , string Napomena1, int VrstaPregleda, int Nalogodavac1, string Ref1, int Nalogodavac2,
string Ref2, int Nalogodavac3, string Ref3, int Brodar, string NaslovStatusaVozila, int DobijenBZ, int Prioritet, decimal TaraKontejneraT,int RLTerminali2,int RLTerminali3, int PotvrdioKlijent, int UradilaCarina)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertUvozKonacna";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter idnadredjeni = new SqlParameter();
            idnadredjeni.ParameterName = "@IdNadredjeni";
            idnadredjeni.SqlDbType = SqlDbType.Int;
            idnadredjeni.Direction = ParameterDirection.Input;
            idnadredjeni.Value = IDNadredjeni;
            cmd.Parameters.Add(idnadredjeni);

            SqlParameter etaBroda = new SqlParameter();
            etaBroda.ParameterName = "@EtaBroda";
            etaBroda.SqlDbType = SqlDbType.DateTime;
            etaBroda.Direction = ParameterDirection.Input;
            etaBroda.Value = ETABroda;
            cmd.Parameters.Add(etaBroda);

            SqlParameter ataBroda = new SqlParameter();
            ataBroda.ParameterName = "@AtaBroda";
            ataBroda.SqlDbType = SqlDbType.DateTime;
            ataBroda.Direction = ParameterDirection.Input;
            ataBroda.Value = ATABroda;
            cmd.Parameters.Add(ataBroda);

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@StatusPrijema";
            status.SqlDbType = SqlDbType.NVarChar;
            status.Size = 50;
            status.Direction = ParameterDirection.Input;
            status.Value = Status;
            cmd.Parameters.Add(status);

            SqlParameter brojKont = new SqlParameter();
            brojKont.ParameterName = "@BrojKontejnera";
            brojKont.SqlDbType = SqlDbType.NVarChar;
            brojKont.Size = 50;
            brojKont.Direction = ParameterDirection.Input;
            brojKont.Value = BrojKont;
            cmd.Parameters.Add(brojKont);

            SqlParameter tipKont = new SqlParameter();
            tipKont.ParameterName = "@TipKontejnera";
            tipKont.SqlDbType = SqlDbType.Int;
            tipKont.Direction = ParameterDirection.Input;
            tipKont.Value = TipKont;
            cmd.Parameters.Add(tipKont);

            SqlParameter nalogBrod = new SqlParameter();
            nalogBrod.ParameterName = "@DobijenNalogBrodara";
            nalogBrod.SqlDbType = SqlDbType.DateTime;
            nalogBrod.Direction = ParameterDirection.Input;
            nalogBrod.Value = DobijenNalog;
            cmd.Parameters.Add(nalogBrod);

            SqlParameter bz = new SqlParameter();
            bz.ParameterName = "@DobijeBZ";
            bz.SqlDbType = SqlDbType.NVarChar;
            bz.Size = 50;
            bz.Direction = ParameterDirection.Input;
            bz.Value = DobijeBZ;
            cmd.Parameters.Add(bz);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 50;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter pin = new SqlParameter();
            pin.ParameterName = "@PIN";
            pin.SqlDbType = SqlDbType.NVarChar;
            pin.Size = 50;
            pin.Direction = ParameterDirection.Input;
            pin.Value = PIN;
            cmd.Parameters.Add(pin);

            SqlParameter dirigacija = new SqlParameter();
            dirigacija.ParameterName = "@DirigacijaKontejeraZa";
            dirigacija.SqlDbType = SqlDbType.Int;
            dirigacija.Direction = ParameterDirection.Input;
            dirigacija.Value = DirigacijaKont;
            cmd.Parameters.Add(dirigacija);

            SqlParameter brod = new SqlParameter();
            brod.ParameterName = "@NazivBroda";
            brod.SqlDbType = SqlDbType.Int;
            brod.Direction = ParameterDirection.Input;
            brod.Value = NazivBroda;
            cmd.Parameters.Add(brod);

            SqlParameter teretnica = new SqlParameter();
            teretnica.ParameterName = "@BrodskaTeretnica";
            teretnica.SqlDbType = SqlDbType.NVarChar;
            teretnica.Size = 50;
            teretnica.Direction = ParameterDirection.Input;
            teretnica.Value = BTeretnica;
            cmd.Parameters.Add(teretnica);

            SqlParameter adr = new SqlParameter();
            adr.ParameterName = "@ADR";
            adr.SqlDbType = SqlDbType.Int;
            adr.Direction = ParameterDirection.Input;
            adr.Value = ADR;
            cmd.Parameters.Add(adr);

            SqlParameter vlasnik = new SqlParameter();
            vlasnik.ParameterName = "@VlasnikKontejnera";
            vlasnik.SqlDbType = SqlDbType.Int;
            vlasnik.Direction = ParameterDirection.Input;
            vlasnik.Value = Vlasnik;
            cmd.Parameters.Add(vlasnik);

            SqlParameter buking = new SqlParameter();
            buking.ParameterName = "@BukingBrodara";
            buking.SqlDbType = SqlDbType.Int;
            buking.Direction = ParameterDirection.Input;
            buking.Value = Buking;
            cmd.Parameters.Add(buking);

            SqlParameter nalogodavac = new SqlParameter();
            nalogodavac.ParameterName = "@Nalogodavac";
            nalogodavac.SqlDbType = SqlDbType.NVarChar;
            nalogodavac.Size = 50;
            nalogodavac.Direction = ParameterDirection.Input;
            nalogodavac.Value = Nalogodavac;
            cmd.Parameters.Add(nalogodavac);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@VrstaUsluge";
            usluga.SqlDbType = SqlDbType.NVarChar;
            usluga.Size = 50;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = VrstaUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter uvoznik = new SqlParameter();
            uvoznik.ParameterName = "@Uvoznik";
            uvoznik.SqlDbType = SqlDbType.Int;
            uvoznik.Direction = ParameterDirection.Input;
            uvoznik.Value = Uvoznik;
            cmd.Parameters.Add(uvoznik);

            SqlParameter nhm = new SqlParameter();
            nhm.ParameterName = "@NHMBroj";
            nhm.SqlDbType = SqlDbType.Int;
            nhm.Direction = ParameterDirection.Input;
            nhm.Value = NHM;
            cmd.Parameters.Add(nhm);

            SqlParameter nazivRobe = new SqlParameter();
            nazivRobe.ParameterName = "@NazivRobe";
            nazivRobe.SqlDbType = SqlDbType.Int;

            nazivRobe.Direction = ParameterDirection.Input;
            nazivRobe.Value = NazivRobe;
            cmd.Parameters.Add(nazivRobe);

            SqlParameter spedicijaG = new SqlParameter();
            spedicijaG.ParameterName = "@SpedicijaGranica";
            spedicijaG.SqlDbType = SqlDbType.Int;
            spedicijaG.Direction = ParameterDirection.Input;
            spedicijaG.Value = SpedicijaGranicna;
            cmd.Parameters.Add(spedicijaG);

            SqlParameter spedicijaR = new SqlParameter();
            spedicijaR.ParameterName = "@SpedicijaRTC";
            spedicijaR.SqlDbType = SqlDbType.Int;
            spedicijaR.Direction = ParameterDirection.Input;
            spedicijaR.Value = SpedicijaRTC;
            cmd.Parameters.Add(spedicijaR);

            SqlParameter carinskiP = new SqlParameter();
            carinskiP.ParameterName = "@CarinskiPostupak";
            carinskiP.SqlDbType = SqlDbType.Int;
            carinskiP.Direction = ParameterDirection.Input;
            carinskiP.Value = CarinskiPostupak;
            cmd.Parameters.Add(carinskiP);

            SqlParameter postupakRoba = new SqlParameter();
            postupakRoba.ParameterName = "@PostupakSaRobom";
            postupakRoba.SqlDbType = SqlDbType.Int;
            postupakRoba.Direction = ParameterDirection.Input;
            postupakRoba.Value = PostupakRoba;
            cmd.Parameters.Add(postupakRoba);

            SqlParameter pakovanje = new SqlParameter();
            pakovanje.ParameterName = "@NacinPakovanja";
            pakovanje.SqlDbType = SqlDbType.Int;
            pakovanje.Direction = ParameterDirection.Input;
            pakovanje.Value = NacinPakovanja;
            cmd.Parameters.Add(pakovanje);

            SqlParameter odCarina = new SqlParameter();
            odCarina.ParameterName = "@OdredisnaCarina";
            odCarina.SqlDbType = SqlDbType.Int;
            odCarina.Direction = ParameterDirection.Input;
            odCarina.Value = OdredisnaCarina;
            cmd.Parameters.Add(odCarina);

            SqlParameter odSpedicija = new SqlParameter();
            odSpedicija.ParameterName = "@OdredisnaSpedicija";
            odSpedicija.SqlDbType = SqlDbType.Int;
            odSpedicija.Direction = ParameterDirection.Input;
            odSpedicija.Value = OdredisnaSpedicija;
            cmd.Parameters.Add(odSpedicija);

            SqlParameter mesto = new SqlParameter();
            mesto.ParameterName = "@MestoIstovara";
            mesto.SqlDbType = SqlDbType.Int;
            // mesto.Size = 50;
            mesto.Direction = ParameterDirection.Input;
            mesto.Value = MestoIstovara;
            cmd.Parameters.Add(mesto);

            SqlParameter kontakt = new SqlParameter();
            kontakt.ParameterName = "@KontaktOsoba";
            kontakt.SqlDbType = SqlDbType.Int;
            //  kontakt.Size = 50;
            kontakt.Direction = ParameterDirection.Input;
            kontakt.Value = KontaktOsoba;
            cmd.Parameters.Add(kontakt);

            SqlParameter mail = new SqlParameter();
            mail.ParameterName = "@Email";
            mail.SqlDbType = SqlDbType.NVarChar;
            mail.Size = 50;
            mail.Direction = ParameterDirection.Input;
            mail.Value = Mail;
            cmd.Parameters.Add(mail);

            SqlParameter plomba1 = new SqlParameter();
            plomba1.ParameterName = "@BrojPlombe1";
            plomba1.SqlDbType = SqlDbType.NVarChar;
            plomba1.Size = 50;
            plomba1.Direction = ParameterDirection.Input;
            plomba1.Value = Plomba1;
            cmd.Parameters.Add(plomba1);

            SqlParameter plomba2 = new SqlParameter();
            plomba2.ParameterName = "@BrojPlombe2";
            plomba2.SqlDbType = SqlDbType.NVarChar;
            plomba2.Size = 50;
            plomba2.Direction = ParameterDirection.Input;
            plomba2.Value = Plomba2;
            cmd.Parameters.Add(plomba2);

            SqlParameter netoR = new SqlParameter();
            netoR.ParameterName = "@NetoRobe";
            netoR.SqlDbType = SqlDbType.Decimal;
            netoR.Direction = ParameterDirection.Input;
            netoR.Value = NetoRoba;
            cmd.Parameters.Add(netoR);

            SqlParameter brutoR = new SqlParameter();
            brutoR.ParameterName = "@BrutoRobe";
            brutoR.SqlDbType = SqlDbType.Decimal;
            brutoR.Direction = ParameterDirection.Input;
            brutoR.Value = BrutoRoba;
            cmd.Parameters.Add(brutoR);

            SqlParameter taraK = new SqlParameter();
            taraK.ParameterName = "@TaraKontejnera";
            taraK.SqlDbType = SqlDbType.Decimal;
            taraK.Direction = ParameterDirection.Input;
            taraK.Value = TaraKont;
            cmd.Parameters.Add(taraK);

            SqlParameter brutoK = new SqlParameter();
            brutoK.ParameterName = "@BrutoKontejnera";
            brutoK.SqlDbType = SqlDbType.Decimal;
            brutoK.Direction = ParameterDirection.Input;
            brutoK.Value = BrutoKont;
            cmd.Parameters.Add(brutoK);

            SqlParameter napomenaP = new SqlParameter();
            napomenaP.ParameterName = "@NapomenaZaPozicioniranje";
            napomenaP.SqlDbType = SqlDbType.Int;
            napomenaP.Direction = ParameterDirection.Input;
            napomenaP.Value = NapomenaPoz;
            cmd.Parameters.Add(napomenaP);

            SqlParameter ataO = new SqlParameter();
            ataO.ParameterName = "@AtaOtpreme";
            ataO.SqlDbType = SqlDbType.Date;
            ataO.Direction = ParameterDirection.Input;
            ataO.Value = ATAOtpreme;
            cmd.Parameters.Add(ataO);

            SqlParameter voz = new SqlParameter();
            voz.ParameterName = "@BrojVoza";
            voz.SqlDbType = SqlDbType.Int;
            voz.Direction = ParameterDirection.Input;
            voz.Value = BrojVoza;
            cmd.Parameters.Add(voz);

            SqlParameter relacija = new SqlParameter();
            relacija.ParameterName = "@RelacijaVoza";
            relacija.SqlDbType = SqlDbType.NVarChar;
            relacija.Size = 50;
            relacija.Direction = ParameterDirection.Input;
            relacija.Value = Relacija;
            cmd.Parameters.Add(relacija);

            SqlParameter ataD = new SqlParameter();
            ataD.ParameterName = "@AtaDolazak";
            ataD.SqlDbType = SqlDbType.DateTime;
            ataD.Direction = ParameterDirection.Input;
            ataD.Value = ATADolazak;
            cmd.Parameters.Add(ataD);

            SqlParameter kol = new SqlParameter();
            kol.ParameterName = "@koleta";
            kol.SqlDbType = SqlDbType.Decimal;
            kol.Direction = ParameterDirection.Input;
            kol.Value = Koleta;
            cmd.Parameters.Add(kol);

            SqlParameter rlterminali = new SqlParameter();
            rlterminali.ParameterName = "@RLTerminali";
            rlterminali.SqlDbType = SqlDbType.Int;
            rlterminali.Direction = ParameterDirection.Input;
            rlterminali.Value = RLTerminali;
            cmd.Parameters.Add(rlterminali);

            SqlParameter napomena1 = new SqlParameter();
            napomena1.ParameterName = "@Napomena1";
            napomena1.SqlDbType = SqlDbType.NVarChar;
            napomena1.Size = 100;
            napomena1.Direction = ParameterDirection.Input;
            napomena1.Value = Napomena1;
            cmd.Parameters.Add(napomena1);

            SqlParameter vrstapregleda = new SqlParameter();
            vrstapregleda.ParameterName = "@VrstaPregleda";
            vrstapregleda.SqlDbType = SqlDbType.Int;
            // vrstapregleda.Size = 100;
            vrstapregleda.Direction = ParameterDirection.Input;
            vrstapregleda.Value = VrstaPregleda;
            cmd.Parameters.Add(vrstapregleda);

            SqlParameter nalogodavac1 = new SqlParameter();
            nalogodavac1.ParameterName = "@Nalogodavac1";
            nalogodavac1.SqlDbType = SqlDbType.Int;
            nalogodavac1.Direction = ParameterDirection.Input;
            nalogodavac1.Value = Nalogodavac1;
            cmd.Parameters.Add(nalogodavac1);

            SqlParameter ref1 = new SqlParameter();
            ref1.ParameterName = "@Ref1";
            ref1.SqlDbType = SqlDbType.NVarChar;
            ref1.Size = 100;
            ref1.Direction = ParameterDirection.Input;
            ref1.Value = Ref1;
            cmd.Parameters.Add(ref1);

            SqlParameter nalogodavac2 = new SqlParameter();
            nalogodavac2.ParameterName = "@Nalogodavac2";
            nalogodavac2.SqlDbType = SqlDbType.Int;
            nalogodavac2.Direction = ParameterDirection.Input;
            nalogodavac2.Value = Nalogodavac2;
            cmd.Parameters.Add(nalogodavac2);

            SqlParameter ref2 = new SqlParameter();
            ref2.ParameterName = "@Ref2";
            ref2.SqlDbType = SqlDbType.NVarChar;
            ref2.Size = 100;
            ref2.Direction = ParameterDirection.Input;
            ref2.Value = Ref2;
            cmd.Parameters.Add(ref2);

            SqlParameter nalogodavac3 = new SqlParameter();
            nalogodavac3.ParameterName = "@Nalogodavac3";
            nalogodavac3.SqlDbType = SqlDbType.Int;
            nalogodavac3.Direction = ParameterDirection.Input;
            nalogodavac3.Value = Nalogodavac3;
            cmd.Parameters.Add(nalogodavac3);

            SqlParameter ref3 = new SqlParameter();
            ref3.ParameterName = "@Ref3";
            ref3.SqlDbType = SqlDbType.NVarChar;
            ref3.Size = 100;
            ref3.Direction = ParameterDirection.Input;
            ref3.Value = Ref3;
            cmd.Parameters.Add(ref3);

            SqlParameter brodar = new SqlParameter();
            brodar.ParameterName = "@Brodar";
            brodar.SqlDbType = SqlDbType.Int;
            brodar.Direction = ParameterDirection.Input;
            brodar.Value = Brodar;
            cmd.Parameters.Add(brodar);

            //NaslovStatusaVozila
            SqlParameter naslovStatusaVozila = new SqlParameter();
            naslovStatusaVozila.ParameterName = "@NaslovStatusaVozila";
            naslovStatusaVozila.SqlDbType = SqlDbType.NVarChar;
            naslovStatusaVozila.Size = 100;
            naslovStatusaVozila.Direction = ParameterDirection.Input;
            naslovStatusaVozila.Value = NaslovStatusaVozila;
            cmd.Parameters.Add(naslovStatusaVozila);

            SqlParameter dobijenbz = new SqlParameter();
            dobijenbz.ParameterName = "@DobijenBZ";
            dobijenbz.SqlDbType = SqlDbType.Int;
            dobijenbz.Direction = ParameterDirection.Input;
            dobijenbz.Value = DobijenBZ;
            cmd.Parameters.Add(dobijenbz);

            SqlParameter prioritet = new SqlParameter();
            prioritet.ParameterName = "@Prioritet";
            prioritet.SqlDbType = SqlDbType.Int;
            prioritet.Direction = ParameterDirection.Input;
            prioritet.Value = Prioritet;
            cmd.Parameters.Add(prioritet);

            SqlParameter tarakontejnerat = new SqlParameter();
            tarakontejnerat.ParameterName = "@TaraKontejneraT";
            tarakontejnerat.SqlDbType = SqlDbType.Decimal;
            tarakontejnerat.Direction = ParameterDirection.Input;
            tarakontejnerat.Value = TaraKontejneraT;
            cmd.Parameters.Add(tarakontejnerat);

            SqlParameter rlterminali2 = new SqlParameter();
            rlterminali2.ParameterName = "@RLTerminali2";
            rlterminali2.SqlDbType = SqlDbType.Int;
            rlterminali2.Direction = ParameterDirection.Input;
            rlterminali2.Value = RLTerminali2;
            cmd.Parameters.Add(rlterminali2);


            SqlParameter rlterminali3 = new SqlParameter();
            rlterminali3.ParameterName = "@RLTerminali3";
            rlterminali3.SqlDbType = SqlDbType.Int;
            rlterminali3.Direction = ParameterDirection.Input;
            rlterminali3.Value = RLTerminali3;
            cmd.Parameters.Add(rlterminali3);

            SqlParameter potvrdioKlijent= new SqlParameter();
            potvrdioKlijent.ParameterName = "@PotvrdioKlijent";
            potvrdioKlijent.SqlDbType = SqlDbType.Int;
            potvrdioKlijent.Direction = ParameterDirection.Input;
            potvrdioKlijent.Value = PotvrdioKlijent;
            cmd.Parameters.Add(potvrdioKlijent);

            SqlParameter uradilaCarina = new SqlParameter();
            uradilaCarina.ParameterName = "@UradilaCarina";
            uradilaCarina.SqlDbType = SqlDbType.Int;
            uradilaCarina.Direction = ParameterDirection.Input;
            uradilaCarina.Value = UradilaCarina;
            cmd.Parameters.Add(uradilaCarina);


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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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
        public void UpdUvozKonacna(int ID, int IDNadredjeni, DateTime ETABroda, DateTime ATABroda, string Status, string BrojKont, int TipKont, DateTime DobijenNalog, string DobijeBZ, string Napomena,
            string PIN, int DirigacijaKont, int NazivBroda, string BTeretnica, int ADR, int Vlasnik, int Buking, string Nalogodavac, string VrstaUsluge, int Uvoznik, int NHM,
            int NazivRobe, int SpedicijaGranicna, int SpedicijaRTC, int CarinskiPostupak, int PostupakRoba, int NacinPakovanja, int OdredisnaCarina, int OdredisnaSpedicija,
            int MestoIstovara, int KontaktOsoba, string Mail, string Plomba1, string Plomba2, decimal NetoRoba, decimal BrutoRoba, decimal TaraKont, decimal BrutoKont,
            int NapomenaPoz, DateTime ATAOtpreme, int BrojVoza, string Relacija, DateTime ATADolazak, decimal Koleta, int RLTerminali
            , string Napomena1, int VrstaPregleda, int Nalogodavac1, string Ref1, int Nalogodavac2,
string Ref2, int Nalogodavac3, string Ref3, int Brodar, string NaslovStatusaVozila, int DobijenBZ, int Prioritet, int AdresaMestaUtovara, string KontaktOsobe, decimal TaraKontejneraT, decimal KoletaTer, int Scenario, int RLTerminali2, int RLTerminali3,
int PotvrdioKlijent, int UradilaCarina,
             int TFDobijenNalog, int TFDobijenNalogodavac1, DateTime dtpDobijenNalogodavac1, int TFDobijenNalogodavac2, DateTime dtpDobijenNalogodavac2,
                int TFDobijenNalogodavac3, DateTime dtpDobijenNalogodavac3, int TFFCL, int TFLCL, DateTime dtpPotvrdioKlijent, DateTime dtpSlobodanDaNapusti)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateUvozKonacna";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter idnadredjeni = new SqlParameter();
            idnadredjeni.ParameterName = "@IDNadredjeni";
            idnadredjeni.SqlDbType = SqlDbType.Int;
            idnadredjeni.Direction = ParameterDirection.Input;
            idnadredjeni.Value = IDNadredjeni;
            cmd.Parameters.Add(idnadredjeni);

            SqlParameter etaBroda = new SqlParameter();
            etaBroda.ParameterName = "@EtaBroda";
            etaBroda.SqlDbType = SqlDbType.DateTime;
            etaBroda.Direction = ParameterDirection.Input;
            etaBroda.Value = ETABroda;
            cmd.Parameters.Add(etaBroda);

            SqlParameter ataBroda = new SqlParameter();
            ataBroda.ParameterName = "@AtaBroda";
            ataBroda.SqlDbType = SqlDbType.DateTime;
            ataBroda.Direction = ParameterDirection.Input;
            ataBroda.Value = ATABroda;
            cmd.Parameters.Add(ataBroda);

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@StatusPrijema";
            status.SqlDbType = SqlDbType.NVarChar;
            status.Size = 50;
            status.Direction = ParameterDirection.Input;
            status.Value = Status;
            cmd.Parameters.Add(status);

            SqlParameter brojKont = new SqlParameter();
            brojKont.ParameterName = "@BrojKontejnera";
            brojKont.SqlDbType = SqlDbType.NVarChar;
            brojKont.Size = 50;
            brojKont.Direction = ParameterDirection.Input;
            brojKont.Value = BrojKont;
            cmd.Parameters.Add(brojKont);

            SqlParameter tipKont = new SqlParameter();
            tipKont.ParameterName = "@TipKontejnera";
            tipKont.SqlDbType = SqlDbType.Int;
            tipKont.Direction = ParameterDirection.Input;
            tipKont.Value = TipKont;
            cmd.Parameters.Add(tipKont);

            SqlParameter nalogBrod = new SqlParameter();
            nalogBrod.ParameterName = "@DobijenNalogBrodara";
            nalogBrod.SqlDbType = SqlDbType.DateTime;
            nalogBrod.Direction = ParameterDirection.Input;
            nalogBrod.Value = DobijenNalog;
            cmd.Parameters.Add(nalogBrod);

            SqlParameter bz = new SqlParameter();
            bz.ParameterName = "@DobijeBZ";
            bz.SqlDbType = SqlDbType.NVarChar;
            bz.Size = 50;
            bz.Direction = ParameterDirection.Input;
            bz.Value = DobijeBZ;
            cmd.Parameters.Add(bz);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 50;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter pin = new SqlParameter();
            pin.ParameterName = "@PIN";
            pin.SqlDbType = SqlDbType.NVarChar;
            pin.Size = 50;
            pin.Direction = ParameterDirection.Input;
            pin.Value = PIN;
            cmd.Parameters.Add(pin);

            SqlParameter dirigacija = new SqlParameter();
            dirigacija.ParameterName = "@DirigacijaKontejeraZa";
            dirigacija.SqlDbType = SqlDbType.Int;
            dirigacija.Direction = ParameterDirection.Input;
            dirigacija.Value = DirigacijaKont;
            cmd.Parameters.Add(dirigacija);

            SqlParameter brod = new SqlParameter();
            brod.ParameterName = "@NazivBroda";
            brod.SqlDbType = SqlDbType.Int;
            brod.Direction = ParameterDirection.Input;
            brod.Value = NazivBroda;
            cmd.Parameters.Add(brod);

            SqlParameter teretnica = new SqlParameter();
            teretnica.ParameterName = "@BrodskaTeretnica";
            teretnica.SqlDbType = SqlDbType.NVarChar;
            teretnica.Size = 50;
            teretnica.Direction = ParameterDirection.Input;
            teretnica.Value = BTeretnica;
            cmd.Parameters.Add(teretnica);

            SqlParameter adr = new SqlParameter();
            adr.ParameterName = "@ADR";
            adr.SqlDbType = SqlDbType.Int;
            adr.Direction = ParameterDirection.Input;
            adr.Value = ADR;
            cmd.Parameters.Add(adr);

            SqlParameter vlasnik = new SqlParameter();
            vlasnik.ParameterName = "@VlasnikKontejnera";
            vlasnik.SqlDbType = SqlDbType.Int;
            vlasnik.Direction = ParameterDirection.Input;
            vlasnik.Value = Vlasnik;
            cmd.Parameters.Add(vlasnik);

            SqlParameter buking = new SqlParameter();
            buking.ParameterName = "@BukingBrodara";
            buking.SqlDbType = SqlDbType.Int;
            buking.Direction = ParameterDirection.Input;
            buking.Value = Buking;
            cmd.Parameters.Add(buking);

            SqlParameter nalogodavac = new SqlParameter();
            nalogodavac.ParameterName = "@Nalogodavac";
            nalogodavac.SqlDbType = SqlDbType.NVarChar;
            nalogodavac.Size = 50;
            nalogodavac.Direction = ParameterDirection.Input;
            nalogodavac.Value = Nalogodavac;
            cmd.Parameters.Add(nalogodavac);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@VrstaUsluge";
            usluga.SqlDbType = SqlDbType.NVarChar;
            usluga.Size = 50;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = VrstaUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter uvoznik = new SqlParameter();
            uvoznik.ParameterName = "@Uvoznik";
            uvoznik.SqlDbType = SqlDbType.Int;
            uvoznik.Direction = ParameterDirection.Input;
            uvoznik.Value = Uvoznik;
            cmd.Parameters.Add(uvoznik);

            SqlParameter nhm = new SqlParameter();
            nhm.ParameterName = "@NHMBroj";
            nhm.SqlDbType = SqlDbType.Int;
            nhm.Direction = ParameterDirection.Input;
            nhm.Value = NHM;
            cmd.Parameters.Add(nhm);

            SqlParameter nazivRobe = new SqlParameter();
            nazivRobe.ParameterName = "@NazivRobe";
            nazivRobe.SqlDbType = SqlDbType.Int;
            nazivRobe.Direction = ParameterDirection.Input;
            nazivRobe.Value = NazivRobe;
            cmd.Parameters.Add(nazivRobe);

            SqlParameter spedicijaG = new SqlParameter();
            spedicijaG.ParameterName = "@SpedicijaGranica";
            spedicijaG.SqlDbType = SqlDbType.Int;
            spedicijaG.Direction = ParameterDirection.Input;
            spedicijaG.Value = SpedicijaGranicna;
            cmd.Parameters.Add(spedicijaG);

            SqlParameter spedicijaR = new SqlParameter();
            spedicijaR.ParameterName = "@SpedicijaRTC";
            spedicijaR.SqlDbType = SqlDbType.Int;
            spedicijaR.Direction = ParameterDirection.Input;
            spedicijaR.Value = SpedicijaRTC;
            cmd.Parameters.Add(spedicijaR);

            SqlParameter carinskiP = new SqlParameter();
            carinskiP.ParameterName = "@CarinskiPostupak";
            carinskiP.SqlDbType = SqlDbType.Int;
            carinskiP.Direction = ParameterDirection.Input;
            carinskiP.Value = CarinskiPostupak;
            cmd.Parameters.Add(carinskiP);

            SqlParameter postupakRoba = new SqlParameter();
            postupakRoba.ParameterName = "@PostupakSaRobom";
            postupakRoba.SqlDbType = SqlDbType.Int;
            postupakRoba.Direction = ParameterDirection.Input;
            postupakRoba.Value = PostupakRoba;
            cmd.Parameters.Add(postupakRoba);

            SqlParameter pakovanje = new SqlParameter();
            pakovanje.ParameterName = "@NacinPakovanja";
            pakovanje.SqlDbType = SqlDbType.Int;
            pakovanje.Direction = ParameterDirection.Input;
            pakovanje.Value = NacinPakovanja;
            cmd.Parameters.Add(pakovanje);

            SqlParameter odCarina = new SqlParameter();
            odCarina.ParameterName = "@OdredisnaCarina";
            odCarina.SqlDbType = SqlDbType.Int;
            odCarina.Direction = ParameterDirection.Input;
            odCarina.Value = OdredisnaCarina;
            cmd.Parameters.Add(odCarina);

            SqlParameter odSpedicija = new SqlParameter();
            odSpedicija.ParameterName = "@OdredisnaSpedicija";
            odSpedicija.SqlDbType = SqlDbType.Int;
            odSpedicija.Direction = ParameterDirection.Input;
            odSpedicija.Value = OdredisnaSpedicija;
            cmd.Parameters.Add(odSpedicija);

            SqlParameter mesto = new SqlParameter();
            mesto.ParameterName = "@MestoIstovara";
            mesto.SqlDbType = SqlDbType.Int;
            // mesto.Size = 50;
            mesto.Direction = ParameterDirection.Input;
            mesto.Value = MestoIstovara;
            cmd.Parameters.Add(mesto);

            SqlParameter kontakt = new SqlParameter();
            kontakt.ParameterName = "@KontaktOsoba";
            kontakt.SqlDbType = SqlDbType.Int;
            //  kontakt.Size = 50;
            kontakt.Direction = ParameterDirection.Input;
            kontakt.Value = KontaktOsoba;
            cmd.Parameters.Add(kontakt);

            SqlParameter mail = new SqlParameter();
            mail.ParameterName = "@Email";
            mail.SqlDbType = SqlDbType.NVarChar;
            mail.Size = 50;
            mail.Direction = ParameterDirection.Input;
            mail.Value = Mail;
            cmd.Parameters.Add(mail);

            SqlParameter plomba1 = new SqlParameter();
            plomba1.ParameterName = "@BrojPlombe1";
            plomba1.SqlDbType = SqlDbType.NVarChar;
            plomba1.Size = 50;
            plomba1.Direction = ParameterDirection.Input;
            plomba1.Value = Plomba1;
            cmd.Parameters.Add(plomba1);

            SqlParameter plomba2 = new SqlParameter();
            plomba2.ParameterName = "@BrojPlombe2";
            plomba2.SqlDbType = SqlDbType.NVarChar;
            plomba2.Size = 50;
            plomba2.Direction = ParameterDirection.Input;
            plomba2.Value = Plomba2;
            cmd.Parameters.Add(plomba2);

            SqlParameter netoR = new SqlParameter();
            netoR.ParameterName = "@NetoRobe";
            netoR.SqlDbType = SqlDbType.Decimal;
            netoR.Direction = ParameterDirection.Input;
            netoR.Value = NetoRoba;
            cmd.Parameters.Add(netoR);

            SqlParameter brutoR = new SqlParameter();
            brutoR.ParameterName = "@BrutoRobe";
            brutoR.SqlDbType = SqlDbType.Decimal;
            brutoR.Direction = ParameterDirection.Input;
            brutoR.Value = BrutoRoba;
            cmd.Parameters.Add(brutoR);

            SqlParameter taraK = new SqlParameter();
            taraK.ParameterName = "@TaraKontejnera";
            taraK.SqlDbType = SqlDbType.Decimal;
            taraK.Direction = ParameterDirection.Input;
            taraK.Value = TaraKont;
            cmd.Parameters.Add(taraK);

            SqlParameter brutoK = new SqlParameter();
            brutoK.ParameterName = "@BrutoKontejnera";
            brutoK.SqlDbType = SqlDbType.Decimal;
            brutoK.Direction = ParameterDirection.Input;
            brutoK.Value = BrutoKont;
            cmd.Parameters.Add(brutoK);

            SqlParameter napomenaP = new SqlParameter();
            napomenaP.ParameterName = "@NapomenaZaPozicioniranje";
            napomenaP.SqlDbType = SqlDbType.Int;
            napomenaP.Direction = ParameterDirection.Input;
            napomenaP.Value = NapomenaPoz;
            cmd.Parameters.Add(napomenaP);

            SqlParameter ataO = new SqlParameter();
            ataO.ParameterName = "@AtaOtpreme";
            ataO.SqlDbType = SqlDbType.Date;
            ataO.Direction = ParameterDirection.Input;
            ataO.Value = ATAOtpreme;
            cmd.Parameters.Add(ataO);

            SqlParameter voz = new SqlParameter();
            voz.ParameterName = "@BrojVoza";
            voz.SqlDbType = SqlDbType.Int;
            voz.Direction = ParameterDirection.Input;
            voz.Value = BrojVoza;
            cmd.Parameters.Add(voz);

            SqlParameter relacija = new SqlParameter();
            relacija.ParameterName = "@RelacijaVoza";
            relacija.SqlDbType = SqlDbType.NVarChar;
            relacija.Size = 50;
            relacija.Direction = ParameterDirection.Input;
            relacija.Value = Relacija;
            cmd.Parameters.Add(relacija);

            SqlParameter ataD = new SqlParameter();
            ataD.ParameterName = "@AtaDolazak";
            ataD.SqlDbType = SqlDbType.DateTime;
            ataD.Direction = ParameterDirection.Input;
            ataD.Value = ATADolazak;
            cmd.Parameters.Add(ataD);

            SqlParameter kol = new SqlParameter();
            kol.ParameterName = "@koleta";
            kol.SqlDbType = SqlDbType.Decimal;
            kol.Direction = ParameterDirection.Input;
            kol.Value = Koleta;
            cmd.Parameters.Add(kol);

            SqlParameter rlterminali = new SqlParameter();
            rlterminali.ParameterName = "@RLTerminali";
            rlterminali.SqlDbType = SqlDbType.Decimal;
            rlterminali.Direction = ParameterDirection.Input;
            rlterminali.Value = RLTerminali;
            cmd.Parameters.Add(rlterminali);

            SqlParameter napomena1 = new SqlParameter();
            napomena1.ParameterName = "@Napomena1";
            napomena1.SqlDbType = SqlDbType.NVarChar;
            napomena1.Size = 100;
            napomena1.Direction = ParameterDirection.Input;
            napomena1.Value = Napomena1;
            cmd.Parameters.Add(napomena1);

            SqlParameter vrstapregleda = new SqlParameter();
            vrstapregleda.ParameterName = "@VrstaPregleda";
            vrstapregleda.SqlDbType = SqlDbType.Int;
            //  vrstapregleda.Size = 100;
            vrstapregleda.Direction = ParameterDirection.Input;
            vrstapregleda.Value = VrstaPregleda;
            cmd.Parameters.Add(vrstapregleda);

            SqlParameter nalogodavac1 = new SqlParameter();
            nalogodavac1.ParameterName = "@Nalogodavac1";
            nalogodavac1.SqlDbType = SqlDbType.Int;
            nalogodavac1.Direction = ParameterDirection.Input;
            nalogodavac1.Value = Nalogodavac1;
            cmd.Parameters.Add(nalogodavac1);

            SqlParameter ref1 = new SqlParameter();
            ref1.ParameterName = "@Ref1";
            ref1.SqlDbType = SqlDbType.NVarChar;
            ref1.Size = 100;
            ref1.Direction = ParameterDirection.Input;
            ref1.Value = Ref1;
            cmd.Parameters.Add(ref1);

            SqlParameter nalogodavac2 = new SqlParameter();
            nalogodavac2.ParameterName = "@Nalogodavac2";
            nalogodavac2.SqlDbType = SqlDbType.Int;
            nalogodavac2.Direction = ParameterDirection.Input;
            nalogodavac2.Value = Nalogodavac2;
            cmd.Parameters.Add(nalogodavac2);

            SqlParameter ref2 = new SqlParameter();
            ref2.ParameterName = "@Ref2";
            ref2.SqlDbType = SqlDbType.NVarChar;
            ref2.Size = 100;
            ref2.Direction = ParameterDirection.Input;
            ref2.Value = Ref2;
            cmd.Parameters.Add(ref2);

            SqlParameter nalogodavac3 = new SqlParameter();
            nalogodavac3.ParameterName = "@Nalogodavac3";
            nalogodavac3.SqlDbType = SqlDbType.Int;
            nalogodavac3.Direction = ParameterDirection.Input;
            nalogodavac3.Value = Nalogodavac3;
            cmd.Parameters.Add(nalogodavac3);

            SqlParameter ref3 = new SqlParameter();
            ref3.ParameterName = "@Ref3";
            ref3.SqlDbType = SqlDbType.NVarChar;
            ref3.Size = 100;
            ref3.Direction = ParameterDirection.Input;
            ref3.Value = Ref3;
            cmd.Parameters.Add(ref3);

            SqlParameter brodar = new SqlParameter();
            brodar.ParameterName = "@Brodar";
            brodar.SqlDbType = SqlDbType.Int;
            brodar.Direction = ParameterDirection.Input;
            brodar.Value = Brodar;
            cmd.Parameters.Add(brodar);

            SqlParameter naslovStatusaVozila = new SqlParameter();
            naslovStatusaVozila.ParameterName = "@NaslovStatusaVozila";
            naslovStatusaVozila.SqlDbType = SqlDbType.NVarChar;
            naslovStatusaVozila.Size = 100;
            naslovStatusaVozila.Direction = ParameterDirection.Input;
            naslovStatusaVozila.Value = NaslovStatusaVozila;
            cmd.Parameters.Add(naslovStatusaVozila);

            SqlParameter dobijenbz = new SqlParameter();
            dobijenbz.ParameterName = "@DobijenBZ";
            dobijenbz.SqlDbType = SqlDbType.Int;
            dobijenbz.Direction = ParameterDirection.Input;
            dobijenbz.Value = DobijenBZ;
            cmd.Parameters.Add(dobijenbz);

            SqlParameter prioritet = new SqlParameter();
            prioritet.ParameterName = "@Prioritet";
            prioritet.SqlDbType = SqlDbType.Int;
            prioritet.Direction = ParameterDirection.Input;
            prioritet.Value = Prioritet;
            cmd.Parameters.Add(prioritet);

            SqlParameter adresamestautovara = new SqlParameter();
            adresamestautovara.ParameterName = "@AdresaMestaUtovara";
            adresamestautovara.SqlDbType = SqlDbType.Int;
            adresamestautovara.Direction = ParameterDirection.Input;
            adresamestautovara.Value = AdresaMestaUtovara;
            cmd.Parameters.Add(adresamestautovara);


            SqlParameter kontaktosobe = new SqlParameter();
            kontaktosobe.ParameterName = "@KontaktOsobe";
            kontaktosobe.SqlDbType = SqlDbType.NVarChar;
            kontaktosobe.Size = 500;
            kontaktosobe.Direction = ParameterDirection.Input;
            kontaktosobe.Value = KontaktOsobe;
            cmd.Parameters.Add(kontaktosobe);

            SqlParameter tarakontejnerat = new SqlParameter();
            tarakontejnerat.ParameterName = "@TaraKontejneraT";
            tarakontejnerat.SqlDbType = SqlDbType.Decimal;
            tarakontejnerat.Direction = ParameterDirection.Input;
            tarakontejnerat.Value = TaraKontejneraT;
            cmd.Parameters.Add(tarakontejnerat);


            SqlParameter koletater = new SqlParameter();
            koletater.ParameterName = "@KoletaTer";
            koletater.SqlDbType = SqlDbType.Decimal;
            koletater.Direction = ParameterDirection.Input;
            koletater.Value = KoletaTer;
            cmd.Parameters.Add(koletater);

            SqlParameter scenario = new SqlParameter();
            scenario.ParameterName = "@Scenario";
            scenario.SqlDbType = SqlDbType.Int;
            scenario.Direction = ParameterDirection.Input;
            scenario.Value = Scenario;
            cmd.Parameters.Add(scenario);

            SqlParameter rlterminali2 = new SqlParameter();
            rlterminali2.ParameterName = "@RLTerminali2";
            rlterminali2.SqlDbType = SqlDbType.Int;
            rlterminali2.Direction = ParameterDirection.Input;
            rlterminali2.Value = RLTerminali2;
            cmd.Parameters.Add(rlterminali2);

            SqlParameter rlterminali3 = new SqlParameter();
            rlterminali3.ParameterName = "@RLTerminali3";
            rlterminali3.SqlDbType = SqlDbType.Int;
            rlterminali3.Direction = ParameterDirection.Input;
            rlterminali3.Value = RLTerminali3;
            cmd.Parameters.Add(rlterminali3);

            SqlParameter potvrdioKlijent = new SqlParameter();
            potvrdioKlijent.ParameterName = "@PotvrdioKlijent";
            potvrdioKlijent.SqlDbType = SqlDbType.Int;
            potvrdioKlijent.Direction = ParameterDirection.Input;
            potvrdioKlijent.Value = PotvrdioKlijent;
            cmd.Parameters.Add(potvrdioKlijent);

            SqlParameter uradilaCarina = new SqlParameter();
            uradilaCarina.ParameterName = "@UradilaCarina";
            uradilaCarina.SqlDbType = SqlDbType.Int;
            uradilaCarina.Direction = ParameterDirection.Input;
            uradilaCarina.Value = UradilaCarina;
            cmd.Parameters.Add(uradilaCarina);

            SqlParameter tfDobijenNalog = new SqlParameter();
            tfDobijenNalog.ParameterName = "@chkDobijenNalogBrodara";
            tfDobijenNalog.SqlDbType = SqlDbType.Int;
            tfDobijenNalog.Direction = ParameterDirection.Input;
            tfDobijenNalog.Value = TFDobijenNalog;
            cmd.Parameters.Add(tfDobijenNalog);

            SqlParameter tfDobijenNalogodavac1 = new SqlParameter();
            tfDobijenNalogodavac1.ParameterName = "@chkDobijenNalogodavac1";
            tfDobijenNalogodavac1.SqlDbType = SqlDbType.Int;
            tfDobijenNalogodavac1.Direction = ParameterDirection.Input;
            tfDobijenNalogodavac1.Value = TFDobijenNalogodavac1;
            cmd.Parameters.Add(tfDobijenNalogodavac1);


            SqlParameter dtpdobijenNalogodavac1 = new SqlParameter();
            dtpdobijenNalogodavac1.ParameterName = "@DatumNalogodavac1";
            dtpdobijenNalogodavac1.SqlDbType = SqlDbType.DateTime;
            dtpdobijenNalogodavac1.Direction = ParameterDirection.Input;
            dtpdobijenNalogodavac1.Value = dtpDobijenNalogodavac1;
            cmd.Parameters.Add(dtpdobijenNalogodavac1);


            SqlParameter tfDobijenNalogodavac2 = new SqlParameter();
            tfDobijenNalogodavac2.ParameterName = "@chkDobijenNalogodavac2";
            tfDobijenNalogodavac2.SqlDbType = SqlDbType.Int;
            tfDobijenNalogodavac2.Direction = ParameterDirection.Input;
            tfDobijenNalogodavac2.Value = TFDobijenNalogodavac2;
            cmd.Parameters.Add(tfDobijenNalogodavac2);


            SqlParameter dtpdobijenNalogodavac2 = new SqlParameter();
            dtpdobijenNalogodavac2.ParameterName = "@DatumNalogodavac2";
            dtpdobijenNalogodavac2.SqlDbType = SqlDbType.DateTime;
            dtpdobijenNalogodavac2.Direction = ParameterDirection.Input;
            dtpdobijenNalogodavac2.Value = dtpDobijenNalogodavac2;
            cmd.Parameters.Add(dtpdobijenNalogodavac2);


            SqlParameter tfDobijenNalogodavac3 = new SqlParameter();
            tfDobijenNalogodavac3.ParameterName = "@chkDobijenNalogodavac3";
            tfDobijenNalogodavac3.SqlDbType = SqlDbType.Int;
            tfDobijenNalogodavac3.Direction = ParameterDirection.Input;
            tfDobijenNalogodavac3.Value = TFDobijenNalogodavac3;
            cmd.Parameters.Add(tfDobijenNalogodavac3);


            SqlParameter dtpdobijenNalogodavac3 = new SqlParameter();
            dtpdobijenNalogodavac3.ParameterName = "@DatumNalogodavac3";
            dtpdobijenNalogodavac3.SqlDbType = SqlDbType.DateTime;
            dtpdobijenNalogodavac3.Direction = ParameterDirection.Input;
            dtpdobijenNalogodavac3.Value = dtpDobijenNalogodavac3;
            cmd.Parameters.Add(dtpdobijenNalogodavac3);


            SqlParameter datumPotvrdioKlijent = new SqlParameter();
            datumPotvrdioKlijent.ParameterName = "@DatumPotvrdioKlijent";
            datumPotvrdioKlijent.SqlDbType = SqlDbType.DateTime;
            datumPotvrdioKlijent.Direction = ParameterDirection.Input;
            datumPotvrdioKlijent.Value = dtpPotvrdioKlijent;
            cmd.Parameters.Add(datumPotvrdioKlijent);

            SqlParameter dtpslobodanDaNapusti = new SqlParameter();
            dtpslobodanDaNapusti.ParameterName = "@DatumSlobasodanDaNapusti";
            dtpslobodanDaNapusti.SqlDbType = SqlDbType.DateTime;
            dtpslobodanDaNapusti.Direction = ParameterDirection.Input;
            dtpslobodanDaNapusti.Value = dtpSlobodanDaNapusti;
            cmd.Parameters.Add(dtpslobodanDaNapusti);


            SqlParameter fcl = new SqlParameter();
            fcl.ParameterName = "@FCL";
            fcl.SqlDbType = SqlDbType.Int;
            fcl.Direction = ParameterDirection.Input;
            fcl.Value = TFFCL;
            cmd.Parameters.Add(fcl);


            SqlParameter lcl = new SqlParameter();
            lcl.ParameterName = "@LCL";
            lcl.SqlDbType = SqlDbType.Int;
            lcl.Direction = ParameterDirection.Input;
            lcl.Value = TFLCL;
            cmd.Parameters.Add(lcl);

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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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
        public void DelUvozKonacna(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteUvozKonacna";
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

        public void DelUvozUsluga(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteUvozVrstaManipulacije";
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

        public void DelUvozUslugaTerminalskih(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteUvozVrstaManTerminalskih";
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


        public void DelUvozKonacnaUsluga(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteUvozKonacnaVrstaManipulacije";
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


        public void UpdPlatiocaUvozKonacnaUsluga(int ID, int Platioc)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdPlatiocaUvozKonacnaVrstaManipulacije";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter platioc = new SqlParameter();
            platioc.ParameterName = "@Platioc";
            platioc.SqlDbType = SqlDbType.Int;
            platioc.Direction = ParameterDirection.Input;
            platioc.Value = Platioc;
            cmd.Parameters.Add(platioc);

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

        public void UpdCenaUvozKonacnaUsluga(int ID, decimal Cena)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdCenaUvozKonacnaVrstaManipulacije";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Decimal;
            cena.Direction = ParameterDirection.Input;
            cena.Value = Cena;
            cmd.Parameters.Add(cena);

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


        public void UpdPlatiocaUvozUsluga(int ID, int Platioc)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdPlatiocaUvozVrstaManipulacije";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter platioc = new SqlParameter();
            platioc.ParameterName = "@Platioc";
            platioc.SqlDbType = SqlDbType.Int;
            platioc.Direction = ParameterDirection.Input;
            platioc.Value = Platioc;
            cmd.Parameters.Add(platioc);

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

        public void UpdCenaUvozUsluga(int ID, double Cena)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdCenaUvozVrstaManipulacije";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Decimal;
            cena.Direction = ParameterDirection.Input;
            cena.Value = Cena;
            cmd.Parameters.Add(cena);

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


        public void UpdPokretFormaStatusi(int ID, string Pokret, int StatusKontejnera, string Forma)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdPokretFormaStatus";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Pokret";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 50;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Pokret;
            cmd.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@StatusKontejnera";
            parameter4.SqlDbType = SqlDbType.Int;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = StatusKontejnera;
            cmd.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@Forma";
            parameter5.SqlDbType = SqlDbType.NVarChar;
            parameter5.Size = 50;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Forma;
            cmd.Parameters.Add(parameter5);

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

        public void InsUbaciUslugu(int IDNadredjena, int IDVrstaManipulacije, double Cena, double Kolicina, int OrgJed, int Platilac, int SaPDV, string Pokret, int StatusKontejnera, string Korisnik, string pomForma)
        {
            //  @IdNadredjena int,
            //@IDVrstaManipulacije int,
            //@Cena numeric(18, 2)

            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertUvozVrstaManipulacije";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idVrstaManipulacije = new SqlParameter();
            idVrstaManipulacije.ParameterName = "@IDVrstaManipulacije";
            idVrstaManipulacije.SqlDbType = SqlDbType.Int;
            idVrstaManipulacije.Direction = ParameterDirection.Input;
            idVrstaManipulacije.Value = IDVrstaManipulacije;
            cmd.Parameters.Add(idVrstaManipulacije);


            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Decimal;
            cena.Direction = ParameterDirection.Input;
            cena.Value = Cena;
            cmd.Parameters.Add(cena);

            SqlParameter kolicina = new SqlParameter();
            kolicina.ParameterName = "@Kolicina";
            kolicina.SqlDbType = SqlDbType.Decimal;
            kolicina.Direction = ParameterDirection.Input;
            kolicina.Value = Kolicina;
            cmd.Parameters.Add(kolicina);

            SqlParameter orgjed = new SqlParameter();
            orgjed.ParameterName = "@OrgJed";
            orgjed.SqlDbType = SqlDbType.Int;
            orgjed.Direction = ParameterDirection.Input;
            orgjed.Value = OrgJed;
            cmd.Parameters.Add(orgjed);

            SqlParameter platilac = new SqlParameter();
            platilac.ParameterName = "@Platilac";
            platilac.SqlDbType = SqlDbType.Int;
            platilac.Direction = ParameterDirection.Input;
            platilac.Value = Platilac;
            cmd.Parameters.Add(platilac);

            SqlParameter sapdv = new SqlParameter();
            sapdv.ParameterName = "@SaPDV";
            sapdv.SqlDbType = SqlDbType.Int;
            sapdv.Direction = ParameterDirection.Input;
            sapdv.Value = SaPDV;
            cmd.Parameters.Add(sapdv);

            SqlParameter pokret = new SqlParameter();
            pokret.ParameterName = "@Pokret";
            pokret.SqlDbType = SqlDbType.NVarChar;
            pokret.Size = 50;
            pokret.Direction = ParameterDirection.Input;
            pokret.Value = Pokret;
            cmd.Parameters.Add(pokret);

            SqlParameter statuskontejnera = new SqlParameter();
            statuskontejnera.ParameterName = "@StatusKontejnera";
            statuskontejnera.SqlDbType = SqlDbType.Int;
            statuskontejnera.Direction = ParameterDirection.Input;
            statuskontejnera.Value = StatusKontejnera;
            cmd.Parameters.Add(statuskontejnera);

            SqlParameter pomforma = new SqlParameter();
            pomforma.ParameterName = "@Forma";
            pomforma.SqlDbType = SqlDbType.NVarChar;
            pomforma.Size = 50;
            pomforma.Direction = ParameterDirection.Input;
            pomforma.Value = pomForma;
            cmd.Parameters.Add(pomforma);

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

        public void InsUbaciUsluguKonacna(int IDNadredjena, int IDVrstaManipulacije, double Cena, double Kolicina, int OrgJed, int Platilac, int SaPDV, string Pokret, int StatusKontejnera, string Korisnik, string pomForma)
        {
            //  @IdNadredjena int,
            //@IDVrstaManipulacije int,
            //@Cena numeric(18, 2)

            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertUvozKonacnaVrstaManipulacije";
            cmd.CommandType = CommandType.StoredProcedure;


            //IDNadredjena treba da bude ID svake stavke tj. svakog kontejnera
            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idVrstaManipulacije = new SqlParameter();
            idVrstaManipulacije.ParameterName = "@IDVrstaManipulacije";
            idVrstaManipulacije.SqlDbType = SqlDbType.Int;
            idVrstaManipulacije.Direction = ParameterDirection.Input;
            idVrstaManipulacije.Value = IDVrstaManipulacije;
            cmd.Parameters.Add(idVrstaManipulacije);


            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Decimal;
            cena.Direction = ParameterDirection.Input;
            cena.Value = Cena;
            cmd.Parameters.Add(cena);

            SqlParameter kolicina = new SqlParameter();
            kolicina.ParameterName = "@Kolicina";
            kolicina.SqlDbType = SqlDbType.Decimal;
            kolicina.Direction = ParameterDirection.Input;
            kolicina.Value = Kolicina;
            cmd.Parameters.Add(kolicina);

            SqlParameter orgjed = new SqlParameter();
            orgjed.ParameterName = "@OrgJed";
            orgjed.SqlDbType = SqlDbType.Int;
            orgjed.Direction = ParameterDirection.Input;
            orgjed.Value = OrgJed;
            cmd.Parameters.Add(orgjed);

            SqlParameter platilac = new SqlParameter();
            platilac.ParameterName = "@Platilac";
            platilac.SqlDbType = SqlDbType.Int;
            platilac.Direction = ParameterDirection.Input;
            platilac.Value = Platilac;
            cmd.Parameters.Add(platilac);

            SqlParameter sapdv = new SqlParameter();
            sapdv.ParameterName = "@SaPDV";
            sapdv.SqlDbType = SqlDbType.Int;
            sapdv.Direction = ParameterDirection.Input;
            sapdv.Value = SaPDV;
            cmd.Parameters.Add(sapdv);

            SqlParameter pokret = new SqlParameter();
            pokret.ParameterName = "@Pokret";
            pokret.SqlDbType = SqlDbType.NVarChar;
            pokret.Size = 50;
            pokret.Direction = ParameterDirection.Input;
            pokret.Value = Pokret;
            cmd.Parameters.Add(pokret);

            SqlParameter statuskontejnera = new SqlParameter();
            statuskontejnera.ParameterName = "@StatusKontejnera";
            statuskontejnera.SqlDbType = SqlDbType.Int;
            statuskontejnera.Direction = ParameterDirection.Input;
            statuskontejnera.Value = StatusKontejnera;
            cmd.Parameters.Add(statuskontejnera);

            SqlParameter pomforma = new SqlParameter();
            pomforma.ParameterName = "@Forma";
            pomforma.SqlDbType = SqlDbType.NVarChar;
            pomforma.Size = 50;
            pomforma.Direction = ParameterDirection.Input;
            pomforma.Value = pomForma;
            cmd.Parameters.Add(pomforma);
            /*
            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 50;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);
            */
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

        public void InsUbaciDodatnuUsluguUvoz(int IDNadredjena, int IDVrstaManipulacije,  double Kolicina,   string NapomenaZaUslugu, string Korisnik, int PotvrdiUradjen, decimal Kolicina2)
        {
            //  @IdNadredjena int,
            //@IDVrstaManipulacije int,
            //@Cena numeric(18, 2)

            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsUbaciDodatnuUsluguUvoz";
            cmd.CommandType = CommandType.StoredProcedure;


            //IDNadredjena treba da bude ID svake stavke tj. svakog kontejnera
            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idVrstaManipulacije = new SqlParameter();
            idVrstaManipulacije.ParameterName = "@IDVrstaManipulacije";
            idVrstaManipulacije.SqlDbType = SqlDbType.Int;
            idVrstaManipulacije.Direction = ParameterDirection.Input;
            idVrstaManipulacije.Value = IDVrstaManipulacije;
            cmd.Parameters.Add(idVrstaManipulacije);


     

            SqlParameter kolicina = new SqlParameter();
            kolicina.ParameterName = "@Kolicina";
            kolicina.SqlDbType = SqlDbType.Decimal;
            kolicina.Direction = ParameterDirection.Input;
            kolicina.Value = Kolicina;
            cmd.Parameters.Add(kolicina);

          
            SqlParameter napomenazauslugu = new SqlParameter();
            napomenazauslugu.ParameterName = "@NapomenaZaUslugu";
            napomenazauslugu.SqlDbType = SqlDbType.NVarChar;
            napomenazauslugu.Size = 500;
            napomenazauslugu.Direction = ParameterDirection.Input;
            napomenazauslugu.Value = NapomenaZaUslugu;
            cmd.Parameters.Add(napomenazauslugu);



            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik.ToString().TrimEnd();
            cmd.Parameters.Add(korisnik);


            SqlParameter potvrdiuradjen = new SqlParameter();
            potvrdiuradjen.ParameterName = "@PotvrdiUradjen";
            potvrdiuradjen.SqlDbType = SqlDbType.Int;
            potvrdiuradjen.Direction = ParameterDirection.Input;
            potvrdiuradjen.Value = PotvrdiUradjen;
            cmd.Parameters.Add(potvrdiuradjen);



            SqlParameter kolicina2 = new SqlParameter();
            kolicina2.ParameterName = "@Kolicina2";
            kolicina2.SqlDbType = SqlDbType.Decimal;
            kolicina2.Direction = ParameterDirection.Input;
            kolicina2.Value = Kolicina2;
            cmd.Parameters.Add(kolicina2);
            /*
            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 50;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);
            */
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

        public void InsUbaciDodatnuUsluguIzvoz(int IDNadredjena, int IDVrstaManipulacije, double Kolicina, string NapomenaZaUslugu, string Korisnik, int PotvrdiUradjen, decimal Kolicina2)
        {
            //  @IdNadredjena int,
            //@IDVrstaManipulacije int,
            //@Cena numeric(18, 2)

            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsUbaciDodatnuUsluguIzvoz";
            cmd.CommandType = CommandType.StoredProcedure;


            //IDNadredjena treba da bude ID svake stavke tj. svakog kontejnera
            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idVrstaManipulacije = new SqlParameter();
            idVrstaManipulacije.ParameterName = "@IDVrstaManipulacije";
            idVrstaManipulacije.SqlDbType = SqlDbType.Int;
            idVrstaManipulacije.Direction = ParameterDirection.Input;
            idVrstaManipulacije.Value = IDVrstaManipulacije;
            cmd.Parameters.Add(idVrstaManipulacije);




            SqlParameter kolicina = new SqlParameter();
            kolicina.ParameterName = "@Kolicina";
            kolicina.SqlDbType = SqlDbType.Decimal;
            kolicina.Direction = ParameterDirection.Input;
            kolicina.Value = Kolicina;
            cmd.Parameters.Add(kolicina);


            SqlParameter napomenazauslugu = new SqlParameter();
            napomenazauslugu.ParameterName = "@NapomenaZaUslugu";
            napomenazauslugu.SqlDbType = SqlDbType.NVarChar;
            napomenazauslugu.Size = 500;
            napomenazauslugu.Direction = ParameterDirection.Input;
            napomenazauslugu.Value = NapomenaZaUslugu;
            cmd.Parameters.Add(napomenazauslugu);



            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik.ToString().TrimEnd();
            cmd.Parameters.Add(korisnik);


            SqlParameter potvrdiuradjen = new SqlParameter();
            potvrdiuradjen.ParameterName = "@PotvrdiUradjen";
            potvrdiuradjen.SqlDbType = SqlDbType.Int;
            potvrdiuradjen.Direction = ParameterDirection.Input;
            potvrdiuradjen.Value = PotvrdiUradjen;
            cmd.Parameters.Add(potvrdiuradjen);



            SqlParameter kolicina2 = new SqlParameter();
            kolicina2.ParameterName = "@Kolicina2";
            kolicina2.SqlDbType = SqlDbType.Decimal;
            kolicina2.Direction = ParameterDirection.Input;
            kolicina2.Value = Kolicina2;
            cmd.Parameters.Add(kolicina2);
            /*
            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 50;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);
            */
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

        public void InsUvozKonacnaNHM(int IDNadredjena, int idNHM)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertUvozKonacnaNHM";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idnhm = new SqlParameter();
            idnhm.ParameterName = "@IDNHM";
            idnhm.SqlDbType = SqlDbType.Int;
            idnhm.Direction = ParameterDirection.Input;
            idnhm.Value = idNHM;
            cmd.Parameters.Add(idnhm);

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

        public void InsUvozKonacnaVrstaRobeHS(int IDNadredjena, int idVrstaRobeHS)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertUvozKonacbaVrstaRobeHS";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idvrstarobehs = new SqlParameter();
            idvrstarobehs.ParameterName = "@IDVrstaRobeHS";
            idvrstarobehs.SqlDbType = SqlDbType.Int;
            idvrstarobehs.Direction = ParameterDirection.Input;
            idvrstarobehs.Value = idVrstaRobeHS;
            cmd.Parameters.Add(idvrstarobehs);

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

        public void InsUvozNHM(int IDNadredjena, int idNHM)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertUvozNHM";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idnhm = new SqlParameter();
            idnhm.ParameterName = "@IDNHM";
            idnhm.SqlDbType = SqlDbType.Int;
            idnhm.Direction = ParameterDirection.Input;
            idnhm.Value = idNHM;
            cmd.Parameters.Add(idnhm);

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

        public void InsUvozNHMDiana(int IDNadredjena, string KomercijalniNaziv, string TarifniBroj, double Brojkoleta, double Neto , double Bruto, double Vrednost, string Valuta)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertUvozNHMDiana";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter komercijalnibroj = new SqlParameter();
            komercijalnibroj.ParameterName = "@KomercijalniNaziv";
            komercijalnibroj.SqlDbType = SqlDbType.NVarChar;
            komercijalnibroj.Size = 500;
            komercijalnibroj.Direction = ParameterDirection.Input;
            komercijalnibroj.Value = KomercijalniNaziv;
            cmd.Parameters.Add(komercijalnibroj);

            SqlParameter tarifnibroj = new SqlParameter();
            tarifnibroj.ParameterName = "@TarifniBroj";
            tarifnibroj.SqlDbType = SqlDbType.NVarChar;
            tarifnibroj.Size = 10;
            tarifnibroj.Direction = ParameterDirection.Input;
            tarifnibroj.Value = TarifniBroj;
            cmd.Parameters.Add(tarifnibroj);

            SqlParameter brojkoleta = new SqlParameter();
            brojkoleta.ParameterName = "@Brojkoleta";
            brojkoleta.SqlDbType = SqlDbType.Decimal;
            brojkoleta.Direction = ParameterDirection.Input;
            brojkoleta.Value = Brojkoleta;
            cmd.Parameters.Add(brojkoleta);

            SqlParameter neto = new SqlParameter();
            neto.ParameterName = "@Neto";
            neto.SqlDbType = SqlDbType.Decimal;
            neto.Direction = ParameterDirection.Input;
            neto.Value = Neto;
            cmd.Parameters.Add(neto);

            SqlParameter bruto = new SqlParameter();
            bruto.ParameterName = "@Bruto";
            bruto.SqlDbType = SqlDbType.Decimal;
            bruto.Direction = ParameterDirection.Input;
            bruto.Value = Bruto;
            cmd.Parameters.Add(bruto);

            SqlParameter vrednost = new SqlParameter();
            vrednost.ParameterName = "@Vrednost";
            vrednost.SqlDbType = SqlDbType.Decimal;
            vrednost.Direction = ParameterDirection.Input;
            vrednost.Value = Vrednost;
            cmd.Parameters.Add(vrednost);

            SqlParameter valuta = new SqlParameter();
            valuta.ParameterName = "@Valuta";
            valuta.SqlDbType = SqlDbType.NVarChar;
            valuta.Size = 10;
            valuta.Direction = ParameterDirection.Input;
            valuta.Value = Valuta;
            cmd.Parameters.Add(valuta);

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

        public void InsUvozKonacnaNHMDiana(int IDNadredjena, string KomercijalniNaziv, string TarifniBroj, double Brojkoleta, double Neto, double Bruto, double Vrednost, string Valuta)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertUvozKonacnaNHMDiana";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter komercijalnibroj = new SqlParameter();
            komercijalnibroj.ParameterName = "@KomercijalniNaziv";
            komercijalnibroj.SqlDbType = SqlDbType.NVarChar;
            komercijalnibroj.Size = 500;
            komercijalnibroj.Direction = ParameterDirection.Input;
            komercijalnibroj.Value = KomercijalniNaziv;
            cmd.Parameters.Add(komercijalnibroj);

            SqlParameter tarifnibroj = new SqlParameter();
            tarifnibroj.ParameterName = "@TarifniBroj";
            tarifnibroj.SqlDbType = SqlDbType.NVarChar;
            tarifnibroj.Size = 10;
            tarifnibroj.Direction = ParameterDirection.Input;
            tarifnibroj.Value = TarifniBroj;
            cmd.Parameters.Add(tarifnibroj);

            SqlParameter brojkoleta = new SqlParameter();
            brojkoleta.ParameterName = "@Brojkoleta";
            brojkoleta.SqlDbType = SqlDbType.Decimal;
            brojkoleta.Direction = ParameterDirection.Input;
            brojkoleta.Value = Brojkoleta;
            cmd.Parameters.Add(brojkoleta);

            SqlParameter neto = new SqlParameter();
            neto.ParameterName = "@Neto";
            neto.SqlDbType = SqlDbType.Decimal;
            neto.Direction = ParameterDirection.Input;
            neto.Value = Neto;
            cmd.Parameters.Add(neto);

            SqlParameter bruto = new SqlParameter();
            bruto.ParameterName = "@Bruto";
            bruto.SqlDbType = SqlDbType.Decimal;
            bruto.Direction = ParameterDirection.Input;
            bruto.Value = Bruto;
            cmd.Parameters.Add(bruto);

            SqlParameter vrednost = new SqlParameter();
            vrednost.ParameterName = "@Vrednost";
            vrednost.SqlDbType = SqlDbType.Decimal;
            vrednost.Direction = ParameterDirection.Input;
            vrednost.Value = Vrednost;
            cmd.Parameters.Add(vrednost);

            SqlParameter valuta = new SqlParameter();
            valuta.ParameterName = "@Valuta";
            valuta.SqlDbType = SqlDbType.NVarChar;
            valuta.Size = 10;
            valuta.Direction = ParameterDirection.Input;
            valuta.Value = Valuta;
            cmd.Parameters.Add(valuta);

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

        public void DelUvozNHMDiana(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteUvozNHMDiana";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@ID";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = ID;
            cmd.Parameters.Add(idnadredjena);

          

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

        public void DelUvozKonacnaNHMDiana(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteUvozkonacnaNHMDiana";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@ID";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = ID;
            cmd.Parameters.Add(idnadredjena);



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


        public void InsUvozUvoznici(int IDNadredjena, string Naziv)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertUvozUvoznici";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter komercijalnibroj = new SqlParameter();
            komercijalnibroj.ParameterName = "@Naziv";
            komercijalnibroj.SqlDbType = SqlDbType.NVarChar;
            komercijalnibroj.Size = 500;
            komercijalnibroj.Direction = ParameterDirection.Input;
            komercijalnibroj.Value = Naziv;
            cmd.Parameters.Add(komercijalnibroj);


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

        public void DelUvozUvoznici(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteUvozUvoznici";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@ID";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = ID;
            cmd.Parameters.Add(idnadredjena);

          


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


        public void InsUvozVrstaRobeHS(int IDNadredjena, int idVrstaRobeHS)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertUvozVrstaRobeHS";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idvrstarobehs = new SqlParameter();
            idvrstarobehs.ParameterName = "@IDVrstaRobeHS";
            idvrstarobehs.SqlDbType = SqlDbType.Int;
            idvrstarobehs.Direction = ParameterDirection.Input;
            idvrstarobehs.Value = idVrstaRobeHS;
            cmd.Parameters.Add(idvrstarobehs);

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


        public void InsUvozNapomenePozicioniranja(int IDNadredjena, int IDNapomene, string stNapomene)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertUvozNapomenePozicioniranja";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idapomene = new SqlParameter();
            idapomene.ParameterName = "@IDNapomene";
            idapomene.SqlDbType = SqlDbType.Int;
            idapomene.Direction = ParameterDirection.Input;
            idapomene.Value = IDNapomene;
            cmd.Parameters.Add(idapomene);

            SqlParameter stnapomene = new SqlParameter();
            stnapomene.ParameterName = "@stNapomene";
            stnapomene.SqlDbType = SqlDbType.NVarChar;
            stnapomene.Size = 500;
            stnapomene.Direction = ParameterDirection.Input;
            stnapomene.Value = stNapomene;
            cmd.Parameters.Add(stnapomene);

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

        public void InsUvozKonacnaNapomenePozicioniranja(int IDNadredjena, int IDNapomene, string stNapomene)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertUvozKonacnaNapomenePozicioniranja";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idapomene = new SqlParameter();
            idapomene.ParameterName = "@IDNapomene";
            idapomene.SqlDbType = SqlDbType.Int;
            idapomene.Direction = ParameterDirection.Input;
            idapomene.Value = IDNapomene;
            cmd.Parameters.Add(idapomene);


            SqlParameter stnapomene = new SqlParameter();
            stnapomene.ParameterName = "@stNapomene";
            stnapomene.SqlDbType = SqlDbType.NVarChar;
            stnapomene.Size = 500;
            stnapomene.Direction = ParameterDirection.Input;
            stnapomene.Value = stNapomene;
            cmd.Parameters.Add(stnapomene);

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

        public void DelUvozKonacnaNapomenePozicioniranja(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteUvozKonacnaNapomenePozicioniranja";
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

        public void DelUvozNapomenePozicioniranja(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteUvozNapomenePozicioniranja";
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

        public void DelUvozKonacnaNHM(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteUvozKonacnaNHM";
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

        public void DelUvozKonacnaVrstaRobeHS(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteUvozKonacnaVrstaRobeHS";
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

        public void DelUvozNHM(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteUvozNHM";
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

        public void DelUvozVrstaRobeHS(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteUvozVrstaRobeHS";
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

        public void PrebaciNHMUvozUvozKonacna(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PrebaciNHMUvozUvozKonacna";
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

        public void PrebaciVrsterobeHSUvozUvozKonacna(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PrebaciVrsterobeHSUvozUvozKonacna";
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
        public void PrenesiIzPlanUtovaraUPlanUtovara(int ID, int PlanIz, int PlanU)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PrenesiIzPlanUtovaraUPlanUtovara";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@PlanIz";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = PlanIz;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idnadredjena2 = new SqlParameter();
            idnadredjena2.ParameterName = "@PlanU";
            idnadredjena2.SqlDbType = SqlDbType.Int;
            idnadredjena2.Direction = ParameterDirection.Input;
            idnadredjena2.Value = PlanU;
            cmd.Parameters.Add(idnadredjena2);

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

        public void PrenesiUPlanUtovara(int ID, int IDNadredjena)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PrenesiUPlanUtovaraSelektovano";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjeni";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

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

        public void VratiIzPlanaUtovara(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "VratiIzPlanaUtovaraSelektovano";
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

        public void UpdateBrojKolaIzExcela(int IDPlan, string BrojKola, string Kontejner1, string Kontejner2)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateBrojKolaIzExcela";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDPlan";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = IDPlan;
            cmd.Parameters.Add(id);


            SqlParameter brojkola = new SqlParameter();
            brojkola.ParameterName = "@BrojKola";
            brojkola.SqlDbType = SqlDbType.NVarChar;
            brojkola.Size = 50;
            brojkola.Direction = ParameterDirection.Input;
            brojkola.Value = BrojKola;
            cmd.Parameters.Add(brojkola);

            SqlParameter kontejner1 = new SqlParameter();
            kontejner1.ParameterName = "@Kontejner1";
            kontejner1.SqlDbType = SqlDbType.NVarChar;
            kontejner1.Size = 50;
            kontejner1.Direction = ParameterDirection.Input;
            kontejner1.Value = Kontejner1;
            cmd.Parameters.Add(kontejner1);

            SqlParameter kontejner2 = new SqlParameter();
            kontejner2.ParameterName = "@Kontejner2";
            kontejner2.SqlDbType = SqlDbType.NVarChar;
            kontejner2.Size = 50;
            kontejner2.Direction = ParameterDirection.Input;
            kontejner2.Value = Kontejner2;
            cmd.Parameters.Add(kontejner2);

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

        public void UpdateBrojKolaIzExcelaVoz(int IDPlan, string Tipkontejnera, string Kontejner1, string Plomba, double Koleta, double Tara, double Masa, double Ukupno)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateBrojKolaIzExcelaVoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDPlan";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = IDPlan;
            cmd.Parameters.Add(id);


            SqlParameter brojkola = new SqlParameter();
            brojkola.ParameterName = "@Tipkontejnera";
            brojkola.SqlDbType = SqlDbType.NVarChar;
            brojkola.Size = 50;
            brojkola.Direction = ParameterDirection.Input;
            brojkola.Value = Tipkontejnera;
            cmd.Parameters.Add(brojkola);

            SqlParameter kontejner1 = new SqlParameter();
            kontejner1.ParameterName = "@Kontejner1";
            kontejner1.SqlDbType = SqlDbType.NVarChar;
            kontejner1.Size = 50;
            kontejner1.Direction = ParameterDirection.Input;
            kontejner1.Value = Kontejner1;
            cmd.Parameters.Add(kontejner1);

            SqlParameter plomba = new SqlParameter();
            plomba.ParameterName = "@Plomba";
            plomba.SqlDbType = SqlDbType.NVarChar;
            plomba.Size = 50;
            plomba.Direction = ParameterDirection.Input;
            plomba.Value = Plomba;
            cmd.Parameters.Add(plomba);


            SqlParameter koleta = new SqlParameter();
            koleta.ParameterName = "@Koleta";
            koleta.SqlDbType = SqlDbType.Decimal;
            koleta.Direction = ParameterDirection.Input;
            koleta.Value = koleta;
            cmd.Parameters.Add(koleta);

            SqlParameter tara = new SqlParameter();
            tara.ParameterName = "@Tara";
            tara.SqlDbType = SqlDbType.Decimal;
            tara.Direction = ParameterDirection.Input;
            tara.Value = Tara;
            cmd.Parameters.Add(tara);

            SqlParameter masa = new SqlParameter();
            masa.ParameterName = "@Masa";
            masa.SqlDbType = SqlDbType.Decimal;
            masa.Direction = ParameterDirection.Input;
            masa.Value = Masa;
            cmd.Parameters.Add(masa);

            SqlParameter ukupno = new SqlParameter();
            ukupno.ParameterName = "@ukupno";
            ukupno.SqlDbType = SqlDbType.Decimal;
            ukupno.Direction = ParameterDirection.Input;
            ukupno.Value = Ukupno;
            cmd.Parameters.Add(ukupno);



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

        public void PrenesiPlanUtovaraUPrijemVozIzvoz(int IDPrijema, int IDNaloga)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "spPrenesiPlanUtovaraUPrijemPlatformaIzvoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDPrijema";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = IDPrijema;
            cmd.Parameters.Add(id);


            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNaloga";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNaloga;
            cmd.Parameters.Add(idnadredjena);

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

        public void PrenesiPlanUtovaraUPrijemVoz(int IDPrijema, int IDPlanUtovara)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "spPrenesiPlanUtovaraUPrijemVoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDPrijema";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = IDPrijema;
            cmd.Parameters.Add(id);


            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDPlanUtovara";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDPlanUtovara;
            cmd.Parameters.Add(idnadredjena);

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

        public void PrenesiPlanUtovaraUPrijemPLatforma(int IDPrijema, int IDNaloga)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "spPrenesiPlanUtovaraUPrijemPlatforma";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@IDPrijema";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = IDPrijema;
            cmd.Parameters.Add(id);


            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNaloga";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNaloga;
            cmd.Parameters.Add(idnadredjena);

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

        public void Storniraj(string RNI)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "spStornijaRNI";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@RNI";
            id.SqlDbType = SqlDbType.NVarChar;
            id.Size = 50;
            id.Direction = ParameterDirection.Input;
            id.Value = RNI;
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

        public void IzbrisiUvozKonacna(int KonkretnaUslugaID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "spIzbrisiKUScenario";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@KonkretnaUslugaID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = KonkretnaUslugaID;
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

        public void PrenesiKontejnerIzPlanaNaPrijemnicu(int KontejnerID, int NalogID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "spPrenesiKontejnerIzPlanaNaPrijemnicu";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@KontejnerID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = KontejnerID;
            cmd.Parameters.Add(id);

            SqlParameter nalogid = new SqlParameter();
            nalogid.ParameterName = "@NalogID";
            nalogid.SqlDbType = SqlDbType.Int;
            nalogid.Direction = ParameterDirection.Input;
            nalogid.Value = NalogID;
            cmd.Parameters.Add(nalogid);


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

        public void PrenesiKontejnerIzPlanaNaPrijemnicuIzvoz(int KontejnerID, int NalogID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "spPrenesiKontejnerIzPlanaNaPrijemnicuIzvoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@KontejnerID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = KontejnerID;
            cmd.Parameters.Add(id);

            SqlParameter nalogid = new SqlParameter();
            nalogid.ParameterName = "@NalogID";
            nalogid.SqlDbType = SqlDbType.Int;
            nalogid.Direction = ParameterDirection.Input;
            nalogid.Value = NalogID;
            cmd.Parameters.Add(nalogid);


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

        public void PromeniSaPrijemnice(int KontejnerID, string Plomba1, string Plomba2, string BrojKontejnera, string BrojVagona, double TaraKontejnera, int VrstaKontejnera, decimal TaraKontejneraT)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PromeniSaPrijemniceUK";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@KontejnerID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = KontejnerID;
            cmd.Parameters.Add(id);

            SqlParameter plomba1 = new SqlParameter();
            plomba1.ParameterName = "@Plomba1";
            plomba1.SqlDbType = SqlDbType.NVarChar;
            plomba1.Size = 50;
            plomba1.Direction = ParameterDirection.Input;
            plomba1.Value = Plomba1;
            cmd.Parameters.Add(plomba1);

            SqlParameter plomba2 = new SqlParameter();
            plomba2.ParameterName = "@Plomba2";
            plomba2.SqlDbType = SqlDbType.NVarChar;
            plomba2.Size = 50;
            plomba2.Direction = ParameterDirection.Input;
            plomba2.Value = Plomba2;
            cmd.Parameters.Add(plomba2);

            SqlParameter brojkontejnera = new SqlParameter();
            brojkontejnera.ParameterName = "@BrojKontejnera";
            brojkontejnera.SqlDbType = SqlDbType.NVarChar;
            brojkontejnera.Size = 50;
            brojkontejnera.Direction = ParameterDirection.Input;
            brojkontejnera.Value = BrojKontejnera;
            cmd.Parameters.Add(brojkontejnera);

            SqlParameter brojvagona = new SqlParameter();
            brojvagona.ParameterName = "@BrojVagona";
            brojvagona.SqlDbType = SqlDbType.NVarChar;
            brojvagona.Size = 50;
            brojvagona.Direction = ParameterDirection.Input;
            brojvagona.Value = BrojVagona;
            cmd.Parameters.Add(brojvagona);

            SqlParameter tarakontejnera = new SqlParameter();
            tarakontejnera.ParameterName = "@TaraKontejnera";
            tarakontejnera.SqlDbType = SqlDbType.Decimal;
            tarakontejnera.Direction = ParameterDirection.Input;
            tarakontejnera.Value = TaraKontejnera;
            cmd.Parameters.Add(tarakontejnera);

            SqlParameter vrstakontejnera = new SqlParameter();
            vrstakontejnera.ParameterName = "@VrstaKontejnera";
            vrstakontejnera.SqlDbType = SqlDbType.Int;
            vrstakontejnera.Direction = ParameterDirection.Input;
            vrstakontejnera.Value = VrstaKontejnera;
            cmd.Parameters.Add(vrstakontejnera);


            SqlParameter tarakontejnerat = new SqlParameter();
            tarakontejnerat.ParameterName = "@TaraKontejneraT";
            tarakontejnerat.SqlDbType = SqlDbType.Decimal;
            tarakontejnerat.Direction = ParameterDirection.Input;
            tarakontejnerat.Value = TaraKontejneraT;
            cmd.Parameters.Add(tarakontejnerat);



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

        public void ZavrsiDodatnuUsluguUvozZadata(int NalogID, int IDKonkretnaUsluga, decimal Kolicina, string NapomenaZaUslugu, int PotvrdiUradjen, decimal Kolicina2)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "ZavrsiDodatnuUsluguUvozZadata";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter nalogid = new SqlParameter();
            nalogid.ParameterName = "@NalogID";
            nalogid.SqlDbType = SqlDbType.Int;
            nalogid.Direction = ParameterDirection.Input;
            nalogid.Value = NalogID;
            cmd.Parameters.Add(nalogid);


            SqlParameter idKonkretnaUsluga = new SqlParameter();
            idKonkretnaUsluga.ParameterName = "@IDKonkretnaUsluga";
            idKonkretnaUsluga.SqlDbType = SqlDbType.Int;
            idKonkretnaUsluga.Direction = ParameterDirection.Input;
            idKonkretnaUsluga.Value = IDKonkretnaUsluga;
            cmd.Parameters.Add(idKonkretnaUsluga);

            SqlParameter kolicina = new SqlParameter();
            kolicina.ParameterName = "@Kolicina";
            kolicina.SqlDbType = SqlDbType.Decimal;
            kolicina.Direction = ParameterDirection.Input;
            kolicina.Value = Kolicina;
            cmd.Parameters.Add(kolicina);

            SqlParameter napomenazauslugu = new SqlParameter();
            napomenazauslugu.ParameterName = "@NapomenaZaUslugu";
            napomenazauslugu.SqlDbType = SqlDbType.NVarChar;
            napomenazauslugu.Size = 500;
            napomenazauslugu.Direction = ParameterDirection.Input;
            napomenazauslugu.Value = NapomenaZaUslugu;
            cmd.Parameters.Add(napomenazauslugu);


            SqlParameter potvrdiuradjen = new SqlParameter();
            potvrdiuradjen.ParameterName = "@PotvrdiUradjen";
            potvrdiuradjen.SqlDbType = SqlDbType.Int;
            potvrdiuradjen.Direction = ParameterDirection.Input;
            potvrdiuradjen.Value = PotvrdiUradjen;
            cmd.Parameters.Add(potvrdiuradjen);



            SqlParameter kolicina2 = new SqlParameter();
            kolicina2.ParameterName = "@Kolicina2";
            kolicina2.SqlDbType = SqlDbType.Int;
            kolicina2.Direction = ParameterDirection.Input;
            kolicina2.Value = Kolicina2;
            cmd.Parameters.Add(kolicina2);

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

        public void ZavrsiDodatnuUsluguIzvozZadata(int NalogID, int IDKonkretnaUsluga, decimal Kolicina, string NapomenaZaUslugu, int PotvrdiUradjen, decimal Kolicina2)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "ZavrsiDodatnuUsluguIzvozZadata";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter nalogid = new SqlParameter();
            nalogid.ParameterName = "@NalogID";
            nalogid.SqlDbType = SqlDbType.Int;
            nalogid.Direction = ParameterDirection.Input;
            nalogid.Value = NalogID;
            cmd.Parameters.Add(nalogid);


            SqlParameter idKonkretnaUsluga = new SqlParameter();
            idKonkretnaUsluga.ParameterName = "@IDKonkretnaUsluga";
            idKonkretnaUsluga.SqlDbType = SqlDbType.Int;
            idKonkretnaUsluga.Direction = ParameterDirection.Input;
            idKonkretnaUsluga.Value = IDKonkretnaUsluga;
            cmd.Parameters.Add(idKonkretnaUsluga);

            SqlParameter kolicina = new SqlParameter();
            kolicina.ParameterName = "@Kolicina";
            kolicina.SqlDbType = SqlDbType.Decimal;
            kolicina.Direction = ParameterDirection.Input;
            kolicina.Value = Kolicina;
            cmd.Parameters.Add(kolicina);

            SqlParameter napomenazauslugu = new SqlParameter();
            napomenazauslugu.ParameterName = "@NapomenaZaUslugu";
            napomenazauslugu.SqlDbType = SqlDbType.NVarChar;
            napomenazauslugu.Size = 500;
            napomenazauslugu.Direction = ParameterDirection.Input;
            napomenazauslugu.Value = NapomenaZaUslugu;
            cmd.Parameters.Add(napomenazauslugu);


            SqlParameter potvrdiuradjen = new SqlParameter();
            potvrdiuradjen.ParameterName = "@PotvrdiUradjen";
            potvrdiuradjen.SqlDbType = SqlDbType.Int;
            potvrdiuradjen.Direction = ParameterDirection.Input;
            potvrdiuradjen.Value = PotvrdiUradjen;
            cmd.Parameters.Add(potvrdiuradjen);



            SqlParameter kolicina2 = new SqlParameter();
            kolicina2.ParameterName = "@Kolicina2";
            kolicina2.SqlDbType = SqlDbType.Int;
            kolicina2.Direction = ParameterDirection.Input;
            kolicina2.Value = Kolicina2;
            cmd.Parameters.Add(kolicina2);



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

        public void InsUvozUslugaDokumenta(int IDUsluga, string Putanja)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertUvozUslugaDokument";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@IDUsluga";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = IDUsluga;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Putanja";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 500;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Putanja;
            myCommand.Parameters.Add(parameter2);

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

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis dokumenta u Bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis dokumenta u bazu", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }

        public void DelUvozUslugaDokumenta(int ID)
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeleteUvozUslugaDokumenta";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);

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

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis dokumenta u Bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis dokumenta u bazu", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }


        public void UpdUvozNHMTezine(int ID, int Tip)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateUvozNHMTezine";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter0 = new SqlParameter();
            parameter0.ParameterName = "@ID";
            parameter0.SqlDbType = SqlDbType.Int;
            parameter0.Direction = ParameterDirection.Input;
            parameter0.Value = ID;
            myCommand.Parameters.Add(parameter0);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@Tip";
            parameter9.SqlDbType = SqlDbType.Int;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = Tip;
            myCommand.Parameters.Add(parameter9);

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

            catch (SqlException)
            {
                throw new Exception("Neuspešna promena uradila carina");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Uradila carina uspesno snimljena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }


        public void UpdUvozKonacnaUC(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateUvozKonacnaUC";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter0 = new SqlParameter();
            parameter0.ParameterName = "@ID";
            parameter0.SqlDbType = SqlDbType.Int;
            parameter0.Direction = ParameterDirection.Input;
            parameter0.Value = ID;
            myCommand.Parameters.Add(parameter0);

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

            catch (SqlException)
            {
                throw new Exception("Neuspešna promena uradila carina");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Uradila carina uspesno snimljena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }





    }
}
