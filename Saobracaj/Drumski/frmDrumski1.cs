using Microsoft.Ajax.Utilities;
using Saobracaj.Izvoz;
using Saobracaj.Sifarnici;
using Saobracaj.Uvoz;
using Syncfusion.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Saobracaj.Izvoz.frmGrupniUnosPoljaIzvoz;
using System.Reflection;

namespace Saobracaj.Drumski
{
    public partial class frmDrumski1: Form
    {
      
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int? Uvoz = null;
        int id = 0;
        bool status = false;
        private string noviZapisSaNovimNalogID = "";
        private int? mainNalogID;
        private readonly List<int> _tipoviIn;
        private readonly List<int> _tipoviNotIn;
        private bool drumskiNew = false;
        private int izborAdr = 0;
        private int daLiJeUvoz = 0;
        private int tipNaloga = 0;
        private int daLiJeCarinskiPostupak = 0;
        int radninalogdrumskiID = 0;
        string sfx = "";
        private int? scenario = null;
        List<PrivremeniNHM> privremenaListaNHM = new List<PrivremeniNHM>();
        string tKorisnik = Saobracaj.Sifarnici.frmLogovanje.user;
        private int lOOdobrio = 0;
        private Dictionary<DateTimePicker, DateTimePicker> povezaniDatumi = new Dictionary<DateTimePicker, DateTimePicker>();
        private decimal staraVrednost;
        private bool promenjenaVrednost;

        private ComboBox cboTipNaloga;
        private ComboBox cboKlijent;
        private ComboBox cboVrstaRobe;
        private ComboBox cboCarinskiPUnutrasniTransport;
        private ComboBox cboPolaznaCarinarnica;
        private ComboBox cboPolaznaSpedicija;
        private ComboBox cboOCarinarnica;
        private ComboBox cbOspedicija;
        private ComboBox cboVrstaKontejnera;
        private ComboBox cboKvalitetKontejnera;
        private ComboBox cboBrodar;
        private ComboBox cboBPlombaVlasnik;
        private ComboBox cboMestoPreuzimanja;
        private ComboBox cboMestoSpustanjaPunog;
        private ComboBox cboNacinPakovanja;
        private ComboBox cboTipTransporta;
        private ComboBox cboADR;
        private ComboBox cboMestoUtovara;
        private ComboBox cboNapomenaPoz;
        private ComboBox cboMestoUtovaraCerade;
        private ComboBox cboAdresaUtovaraCerade;
        private ComboBox cboKontaktUtovaraCerade;
        private ComboBox cboMestoIstovaraCerade;
        private ComboBox cboAdresaIstovaraCerade;
        private ComboBox cboKontaktIstovaraCerade;
        private ComboBox cboAdresaPreuzimanja;
        private ComboBox cbokontaktPreuzimanja;


        private System.Windows.Forms.TextBox txtTipNaloga;
        private System.Windows.Forms.TextBox txtBokingBrodara;
        private System.Windows.Forms.TextBox txtReferenca;
        private System.Windows.Forms.TextBox txtKontaktPolazneSpedicije;
        private System.Windows.Forms.TextBox txtKontaktOSpedicije;
       // private System.Windows.Forms.TextBox cboNapomenaPoz;
        private System.Windows.Forms.TextBox txtDodatniOpis;
        private System.Windows.Forms.TextBox txtBrojKontejnera;
        private System.Windows.Forms.TextBox txtOstalePlombe;
        private System.Windows.Forms.TextBox txtKorisnik;
        private System.Windows.Forms.TextBox txtBrodskaPlomba;
        private System.Windows.Forms.TextBox txtOpisPosla;
        
        private System.Windows.Forms.TextBox txtAdresaUtovara;
        private System.Windows.Forms.TextBox txtkontaktNaUtovaru;
        private System.Windows.Forms.TextBox txtNoviSpediterP;
        private System.Windows.Forms.TextBox txtNoviSpediterO;
        private System.Windows.Forms.TextBox txtGranicniPrelaz;   

        private System.Windows.Forms.Label lblNTTOR;
        private System.Windows.Forms.Label lblNacinPakovanja;
        private System.Windows.Forms.Label lblKorisnik;
        private System.Windows.Forms.Label lblBrodskaPlomba;
        private System.Windows.Forms.Label lblAdr;
        private System.Windows.Forms.Label lblOpisPosla;
        private System.Windows.Forms.Label lblAdresaPreuzimanja;
        private System.Windows.Forms.Label lblkontaktPreuzimanja;
        private System.Windows.Forms.Label lblPreuzimanjaPraznogKontejneraNovi;
        private System.Windows.Forms.Label lblSpustanjePunogNovi;
        private System.Windows.Forms.Label lblVrstaRobe;
        private System.Windows.Forms.Label lblVrstaRobeGrid;

        private System.Windows.Forms.NumericUpDown txtTaraK;
        private System.Windows.Forms.NumericUpDown txtBrutoK;
        private System.Windows.Forms.NumericUpDown txtNTTOR;
        private System.Windows.Forms.NumericUpDown txtTrosak; 
        private System.Windows.Forms.NumericUpDown txtCena; 

        private System.Windows.Forms.DateTimePicker dtPreuzimanjaPraznogKontejneraNovi;
        private System.Windows.Forms.DateTimePicker dtPreuzimanjaPraznogKontejnera;
        private System.Windows.Forms.DateTimePicker dtRealiPreuzimanjaPraznogKont;
        private System.Windows.Forms.DateTimePicker dtpSpustanjePunogReal;
        private System.Windows.Forms.DateTimePicker dtpSpustanjePunogNovi;
        private System.Windows.Forms.DateTimePicker dtpSpustanjePunog;
        private System.Windows.Forms.DateTimePicker dtpUtovara; 
        private System.Windows.Forms.DateTimePicker dtpUtovaraNovi;
        private System.Windows.Forms.DateTimePicker dtpRealiUtovara;
        private System.Windows.Forms.DateTimePicker dtpUtovaraCerade;
        private System.Windows.Forms.DateTimePicker dtpRealiUtovaraCerade;
        private System.Windows.Forms.DateTimePicker dtpIstovaraCerade;
        private System.Windows.Forms.DateTimePicker dtpRealiIstovaraCerade;
        private System.Windows.Forms.DateTimePicker dtpUtovaraCeradeNovi;
        private System.Windows.Forms.DateTimePicker dtpIstovaraCeradeNovi;
         

        private System.Windows.Forms.Button btnVrstaRobe;
        private System.Windows.Forms.Button btnUnesiNHM;
        private System.Windows.Forms.CheckBox chkVaganje;
        private System.Windows.Forms.DataGridView dataGridVrstaRobe;
        
        public frmDrumski1()
        {
            InitializeComponent();
            ChangeTextBox();

            this.BindingContext = new BindingContext();
            if (dtpUtovara != null)
                dtpUtovara.Value = DateTime.Today.AddDays(1);
            if (dtPreuzimanjaPraznogKontejnera != null)
                dtPreuzimanjaPraznogKontejnera.Value = DateTime.Today;
            if (dtPreuzimanjaPraznogKontejneraNovi != null)
                dtPreuzimanjaPraznogKontejneraNovi.Value = DateTime.Today;
            if (dtRealiPreuzimanjaPraznogKont != null)
                dtRealiPreuzimanjaPraznogKont.Value = DateTime.Today;
            if (dtpUtovaraNovi != null)
                dtpUtovaraNovi.Value = DateTime.Today.AddDays(1);
            if (dtpRealiUtovara != null)
                dtpRealiUtovara.Value = DateTime.Today.AddDays(1);
            if (dtpSpustanjePunog != null)
                dtpSpustanjePunog.Value = DateTime.Today.AddDays(1);
            if (dtpSpustanjePunogNovi != null)
                dtpSpustanjePunogNovi.Value = DateTime.Today.AddDays(1);
            if (dtpSpustanjePunogReal != null)
                dtpSpustanjePunogReal.Value = DateTime.Today.AddDays(1);
            if (dtpIstovaraCerade != null)
                dtpIstovaraCerade.Value = DateTime.Today.AddDays(1); ;
            if (dtpIstovaraCeradeNovi != null)
                dtpIstovaraCeradeNovi.Value = DateTime.Today.AddDays(1);
            if (dtpRealiIstovaraCerade != null)
                dtpRealiIstovaraCerade.Value = DateTime.Today.AddDays(1);
            if (dtpUtovaraCerade != null)
                dtpUtovaraCerade.Value = DateTime.Today;
            if (dtpUtovaraCeradeNovi != null)
                dtpUtovaraCeradeNovi.Value = DateTime.Today.AddDays(1);
            if (dtpRealiUtovaraCerade != null)
                dtpRealiUtovaraCerade.Value = DateTime.Today.AddDays(1);
            //  dtIstovara.Value = DateTime.Today;
            //dtPr9yMnTm4NSzvG9rrwjM2ec8xZgh1cafXH8.Value = DateTime.Today;
            ////dtPr9yMnTm4NSzvG9rrwjM2ec8xZgh1cafXH8.Checked = true;
            FillCombo();
            VratiPodatke();
            if (lOOdobrio > 0)
                btnPovrdiLO.Visible = false;
            //txtNapomenaPoz.Visible = false;
            //if (cboTipNaloga.SelectedValue != null && int.TryParse(cboTipNaloga.SelectedValue.ToString(), out int tipNalogaId) && tipNalogaId == 2)
            //{
            //    cboMestoPreuzimanja.SelectedValue = 8;
            //    cboMestoUtovara.SelectedValue = 8;
            //    txtAdresaUtovara.Text = "Jarački put";
            //}
            lbtHederTekst.Text = "";
            lbtHederTekst.Visible = true;
            button1.Visible = true;
            button4.Visible = false;
        }

        public frmDrumski1(string noviNalogID, int? NalogID)
        {
            noviZapisSaNovimNalogID = noviNalogID;
            mainNalogID = NalogID;
            InitializeComponent();
            ChangeTextBox();
            this.BindingContext = new BindingContext();
            
            if (dtpUtovara != null)
                dtpUtovara.Value = DateTime.Today.AddDays(1);
            if (dtPreuzimanjaPraznogKontejnera != null)
                dtPreuzimanjaPraznogKontejnera.Value = DateTime.Today;
            if (dtPreuzimanjaPraznogKontejneraNovi != null)
                dtPreuzimanjaPraznogKontejneraNovi.Value = DateTime.Today;
            if (dtRealiPreuzimanjaPraznogKont != null)
                dtRealiPreuzimanjaPraznogKont.Value = DateTime.Today;
            if (dtpUtovaraNovi != null)
                dtpUtovaraNovi.Value = DateTime.Today.AddDays(1);
            if (dtpRealiUtovara != null)
                dtpRealiUtovara.Value = DateTime.Today.AddDays(1);
            if (dtpSpustanjePunog != null)
                dtpSpustanjePunog.Value = DateTime.Today.AddDays(1); 
            if (dtpSpustanjePunogNovi != null)
                dtpSpustanjePunogNovi.Value = DateTime.Today.AddDays(1);
            if (dtpSpustanjePunogReal != null)
                dtpSpustanjePunogReal.Value = DateTime.Today.AddDays(1);
            if (dtpIstovaraCerade != null)
                dtpIstovaraCerade.Value = DateTime.Today.AddDays(1); ;
            if (dtpIstovaraCeradeNovi != null)
                dtpIstovaraCeradeNovi.Value = DateTime.Today.AddDays(1);
            if (dtpRealiIstovaraCerade != null)
                dtpRealiIstovaraCerade.Value = DateTime.Today.AddDays(1);
            if (dtpUtovaraCerade != null)
                dtpUtovaraCerade.Value = DateTime.Today;
            if (dtpUtovaraCeradeNovi != null)
                dtpUtovaraCeradeNovi.Value = DateTime.Today.AddDays(1);
            if (dtpRealiUtovaraCerade != null)
                dtpRealiUtovaraCerade.Value = DateTime.Today.AddDays(1);
            //dtIstovara.Value = DateTime.Today;
            //dtPr9yMnTm4NSzvG9rrwjM2ec8xZgh1cafXH8.Value = DateTime.Today;
            //dtPr9yMnTm4NSzvG9rrwjM2ec8xZgh1cafXH8.Checked = true;
            FillCombo();

            VratiPodatke();
            if (lOOdobrio > 0)
                btnPovrdiLO.Visible = false;
            //txtNapomenaPoz.Visible = false;

            if (noviNalogID == "NOVINALOG")
            {
                status = true;
                lbtHederTekst.Text = "UNOS NOVOG ZAPISA JE U TOKU!";
                lbtHederTekst.Visible = true;
                button1.Visible = false;
                button4.Visible = false;
            }
            else
            {
                lbtHederTekst.Text = "IZMENA ZAPISA JE U TOKU";
                lbtHederTekst.Visible = true;
            }
        }

        public frmDrumski1(List<int> tipoviIn, List<int> tipoviNotIn, string noviNalogID, int? ID, int IzborADR, int DaLiJeUvoz, int TipNaloga, int DaLiJeCarinskiPostupak)
        {
            InitializeComponent();
            ChangeTextBox();
            _tipoviIn = tipoviIn;
            _tipoviNotIn = tipoviNotIn;
            this.BindingContext = new BindingContext();
            
            izborAdr = IzborADR;
            daLiJeUvoz = DaLiJeUvoz;
            tipNaloga = TipNaloga;
            daLiJeCarinskiPostupak = DaLiJeCarinskiPostupak;

            if (noviNalogID == "NOVINALOG")
            {
                if (daLiJeUvoz == 0) //3PI
                {
                    if (tipNaloga == 1) // direktno pun
                    {
                        if (izborAdr == 0)
                        {
                            scenario = 13;
                        }
                        else if(izborAdr == 1)
                        {
                            scenario = 26;
                        }
                    }
                    else if(tipNaloga == 2) // prazan-pun
                    {
                        if (izborAdr == 0)
                        {
                            scenario = 7;
                        }
                        else if (izborAdr == 1)
                        {
                            scenario = 23;
                        }
                    }
                    else if (tipNaloga == 3) // prazan-pun
                    {
                        if (izborAdr == 0)
                        {
                            scenario = 8;
                        }
                        else if (izborAdr == 1)
                        {
                            scenario = 24;
                        }
                    }
                    //9- adr = 0,25 adr = 1

                }
            }

            FillCombo();
            if (DaLiJeUvoz == 0 || DaLiJeUvoz == 3) //Izvoz ili 3PI
            {
                if (TipNaloga == 1)
                {
                    //  panel2.Visible = false;
                    panelDirektnoPun.Visible = true;
                    panelPlatformaIzvozPrazanPun.Visible = false;
                    panelCeradaIzvozPretovarPun.Visible = false;
                    panelCeradaSkladistePun.Visible = false;
                    sfx = "I1";
                }
                else if (TipNaloga == 2)
                {
                    panelPlatformaIzvozPrazanPun.Visible = true;
                    panelDirektnoPun.Visible = false;
                    panelCeradaIzvozPretovarPun.Visible = false;
                    panelCeradaSkladistePun.Visible = false;
                    sfx = "I2";
                }
                else if (TipNaloga == 3)
                {
                    panelPlatformaIzvozPrazanPun.Visible = false;
                    panelDirektnoPun.Visible = false;
                    panelCeradaIzvozPretovarPun.Visible = true;
                    panelCeradaSkladistePun.Visible = false;
                    sfx = "I3";
                }
                else if (TipNaloga == 4)
                {
                    panelDirektnoPun.Visible = false;
                    panelPlatformaIzvozPrazanPun.Visible = false;
                    panelCeradaIzvozPretovarPun.Visible = false;
                    panelCeradaSkladistePun.Visible = true;
                    sfx = "I4";
                }
                if (noviNalogID == "NOVINALOG")
                {
                    var cboTipNaloga = this.Controls.Find("cboTipNaloga" + sfx, true).FirstOrDefault() as ComboBox;
                    cboTipNaloga.SelectedValue = 3;
                    cboTipNaloga.Enabled = false;
                    Uvoz = 3; //3PI
                }
                else
                {
                    var cboTipNaloga = this.Controls.Find("cboTipNaloga" + sfx, true).FirstOrDefault() as ComboBox;
                    cboTipNaloga.SelectedValue = 5;
                    cboTipNaloga.Enabled = false;
                }

            }
            else
            {
                if (noviNalogID == "NOVINALOG")
                {
                    Uvoz = 2;//3PU
                }
            }
            if (ID.HasValue && ID.Value > 0)
            {
                this.id = ID.Value;
            }
            pronadjiKontrole();
            if (dtpUtovara != null)
                dtpUtovara.Value = DateTime.Today.AddDays(1);
            if (dtPreuzimanjaPraznogKontejnera != null)
                dtPreuzimanjaPraznogKontejnera.Value = DateTime.Today;
            if (dtPreuzimanjaPraznogKontejneraNovi != null)
                dtPreuzimanjaPraznogKontejneraNovi.Value = DateTime.Today;
            if (dtRealiPreuzimanjaPraznogKont != null)
                dtRealiPreuzimanjaPraznogKont.Value = DateTime.Today;
            if (dtpUtovaraNovi != null)
                dtpUtovaraNovi.Value = DateTime.Today.AddDays(1);
            if (dtpRealiUtovara != null)
                dtpRealiUtovara.Value = DateTime.Today.AddDays(1);
            if (dtpSpustanjePunog != null)
                dtpSpustanjePunog.Value = DateTime.Today.AddDays(1);
            if (dtpSpustanjePunogNovi != null)
                dtpSpustanjePunogNovi.Value = DateTime.Today.AddDays(1);
            if (dtpSpustanjePunogReal != null)
                dtpSpustanjePunogReal.Value = DateTime.Today.AddDays(1);
            if (dtpIstovaraCerade != null)
                dtpIstovaraCerade.Value = DateTime.Today.AddDays(1); ;
            if (dtpIstovaraCeradeNovi != null)
                dtpIstovaraCeradeNovi.Value = DateTime.Today.AddDays(1);
            if (dtpRealiIstovaraCerade != null)
                dtpRealiIstovaraCerade.Value = DateTime.Today.AddDays(1);
            if (dtpUtovaraCerade != null)
                dtpUtovaraCerade.Value = DateTime.Today;
            if (dtpUtovaraCeradeNovi != null)
                dtpUtovaraCeradeNovi.Value = DateTime.Today;
            if (dtpRealiUtovaraCerade != null)
                dtpRealiUtovaraCerade.Value = DateTime.Today;


            //if (cboTipNaloga.SelectedValue != null && int.TryParse(cboTipNaloga.SelectedValue.ToString(), out int tipNalogaId) && tipNalogaId == 2)
            //{
            //    cboMestoPreuzimanja.SelectedValue = 8;
            //    cboMestoUtovara.SelectedValue = 8;
            //    txtAdresaUtovara.Text = "Jarački put";
            //}

            VratiPodatke();
            //if(dtpUtovaraCerade != null)
            //    dtpUtovaraCerade.Tag = null;
            //if (dtpUtovaraCeradeNovi != null)
            //    dtpUtovaraCeradeNovi.Tag = null;
            //if (dtpIstovaraCeradeNovi != null)
            //    dtpIstovaraCeradeNovi.Tag = null;
            //if (dtpRealiUtovaraCerade != null)
            //    dtpRealiUtovaraCerade.Tag = null;
            //if (dtpIstovaraCerade != null)
            //    dtpIstovaraCerade.Tag = null;
            //if (dtpRealiIstovaraCerade != null)
            //    dtpRealiIstovaraCerade.Tag = null;
            if (lOOdobrio > 0)
                btnPovrdiLO.Visible = false;

            var NapomenaPoz = this.Controls.Find("txtNapomenaPoz" + sfx, true).FirstOrDefault() as TextBox;
            if (NapomenaPoz != null)
            {
                NapomenaPoz.Visible = false;
            }
            if (noviNalogID == "NOVINALOG")
            {
                status = true;
                lbtHederTekst.Text = "UNOS NOVOG ZAPISA JE U TOKU!";
                lbtHederTekst.Visible = true;
                button1.Visible = false;
                button4.Visible = false;          
            }
            else
            {
                lbtHederTekst.Text = "IZMENA ZAPISA JE U TOKU";
                lbtHederTekst.Visible = true;
            }
            if (cboTipNalogaI2.SelectedValue != null && int.TryParse(cboTipNalogaI2.SelectedValue.ToString(), out int tipNalogaId) && tipNalogaId == 2)
            {
                cboMestoPreuzimanja.SelectedValue = 8;
                cboMestoUtovara.SelectedValue = 8;
                txtAdresaUtovara.Text = "Jarački put";
            }
            if (izborAdr == 0 )
            {
                var lblADR = this.Controls.Find("lblADR" + sfx, true).FirstOrDefault() as Label;
                if (cboADR != null)
                {

                    cboADR.Visible = false;
                    lblADR.Visible = false;
                   
                }
            }
            int? tipTransporta = tipoviIn != null && tipoviIn.Contains(2) ? 2 : (int?)null;
            //PodesiVidljivostPoTipuTransporta(tipTransporta);
      
            drumskiNew = true;
            button3.Visible = false;
            button21.Visible = false;
        }

        public void pronadjiKontrole()
        {
            cboTipNaloga = this.Controls.Find("cboTipNaloga" + sfx, true).FirstOrDefault() as ComboBox;
            txtTipNaloga = this.Controls.Find("txtTipNaloga" + sfx, true).FirstOrDefault() as TextBox;
            txtBokingBrodara = this.Controls.Find("txtBokingBrodara" + sfx, true).FirstOrDefault() as TextBox;
            cboKlijent = this.Controls.Find("cboKlijent" + sfx, true).FirstOrDefault() as ComboBox;
            txtReferenca = this.Controls.Find("txtReferenca" + sfx, true).FirstOrDefault() as TextBox;
            cboVrstaRobe = this.Controls.Find("cboVrstaRobe" + sfx, true).FirstOrDefault() as ComboBox;
            cboCarinskiPUnutrasniTransport = this.Controls.Find("cboCarinskiPUnutrasniTransport" + sfx, true).FirstOrDefault() as ComboBox;
            cboPolaznaCarinarnica = this.Controls.Find("cboPolaznaCarinarnica" + sfx, true).FirstOrDefault() as ComboBox;
            cboPolaznaSpedicija = this.Controls.Find("cboPolaznaSpedicija" + sfx, true).FirstOrDefault() as ComboBox;
            txtKontaktPolazneSpedicije = this.Controls.Find("txtKontaktPolazneSpedicije" + sfx, true).FirstOrDefault() as TextBox;
            cboOCarinarnica = this.Controls.Find("cboOCarinarnica" + sfx, true).FirstOrDefault() as ComboBox;
            cbOspedicija = this.Controls.Find("cbOspedicija" + sfx, true).FirstOrDefault() as ComboBox;
            txtKontaktOSpedicije = this.Controls.Find("txtKontaktOSpedicije" + sfx, true).FirstOrDefault() as TextBox;
            txtDodatniOpis = this.Controls.Find("txtDodatniOpis" + sfx, true).FirstOrDefault() as TextBox;
            cboNapomenaPoz = this.Controls.Find("cboNapomenaPoz" + sfx, true).FirstOrDefault() as ComboBox;
            cboMestoUtovaraCerade = this.Controls.Find("cboMestoUtovaraCerade" + sfx, true).FirstOrDefault() as ComboBox;
            cboAdresaUtovaraCerade = this.Controls.Find("cboAdresaUtovaraCerade" + sfx, true).FirstOrDefault() as ComboBox;
            cboKontaktUtovaraCerade = this.Controls.Find("cboKontaktUtovaraCerade" + sfx, true).FirstOrDefault() as ComboBox;
            cboAdresaIstovaraCerade = this.Controls.Find("cboAdresaIstovaraCerade" + sfx, true).FirstOrDefault() as ComboBox;
            cboAdresaPreuzimanja = this.Controls.Find("cboAdresaPreuzimanja" + sfx, true).FirstOrDefault() as ComboBox;
            cboKontaktIstovaraCerade = this.Controls.Find("cboKontaktIstovaraCerade" + sfx, true).FirstOrDefault() as ComboBox;
            cboMestoIstovaraCerade = this.Controls.Find("cboMestoIstovaraCerade" + sfx, true).FirstOrDefault() as ComboBox;
            txtBrojKontejnera = this.Controls.Find("txtBrojKontejnera" + sfx, true).FirstOrDefault() as TextBox;
            cboVrstaKontejnera = this.Controls.Find("cboVrstaKontejnera" + sfx, true).FirstOrDefault() as ComboBox;
            cboKvalitetKontejnera = this.Controls.Find("cboKvalitetKontejnera" + sfx, true).FirstOrDefault() as ComboBox;
            cboBrodar = this.Controls.Find("cboBrodar" + sfx, true).FirstOrDefault() as ComboBox;
            cboBPlombaVlasnik = this.Controls.Find("cboBPlombaVlasnik" + sfx, true).FirstOrDefault() as ComboBox;
            txtOstalePlombe = this.Controls.Find("txtOstalePlombe" + sfx, true).FirstOrDefault() as TextBox;
            txtTaraK = this.Controls.Find("txtTaraK" + sfx, true).FirstOrDefault() as NumericUpDown;
            txtBrutoK = this.Controls.Find("txtBrutoK" + sfx, true).FirstOrDefault() as NumericUpDown;
            txtNTTOR = this.Controls.Find("txtNTTOR" + sfx, true).FirstOrDefault() as NumericUpDown;
            lblNTTOR = this.Controls.Find("lblNTTOR" + sfx, true).FirstOrDefault() as Label;
            txtTrosak = this.Controls.Find("txtTrosak" + sfx, true).FirstOrDefault() as NumericUpDown;
            txtCena = this.Controls.Find("txtCena" + sfx, true).FirstOrDefault() as NumericUpDown;

            cboMestoPreuzimanja = this.Controls.Find("cboMestoPreuzimanja" + sfx, true).FirstOrDefault() as ComboBox;
            cboMestoSpustanjaPunog = this.Controls.Find("cboMestoSpustanjaPunog" + sfx, true).FirstOrDefault() as ComboBox;
            cboNacinPakovanja = this.Controls.Find("cboNacinPakovanja" + sfx, true).FirstOrDefault() as ComboBox;
            lblNacinPakovanja = this.Controls.Find("lblNacinPakovanja" + sfx, true).FirstOrDefault() as Label;
            btnVrstaRobe = this.Controls.Find("btnVrstaRobe" + sfx, true).FirstOrDefault() as Button;
            txtKorisnik = this.Controls.Find("txtKorisnik" + sfx, true).FirstOrDefault() as TextBox;
            lblKorisnik = this.Controls.Find("lblKorisnik" + sfx, true).FirstOrDefault() as Label;
            cboTipTransporta = this.Controls.Find("cboTipTransporta" + sfx, true).FirstOrDefault() as ComboBox;
            txtBrodskaPlomba = this.Controls.Find("txtBrodskaPlomba" + sfx, true).FirstOrDefault() as TextBox;
            lblBrodskaPlomba = this.Controls.Find("lblBrodskaPlomba" + sfx, true).FirstOrDefault() as Label;
            cboADR = this.Controls.Find("cboADR" + sfx, true).FirstOrDefault() as ComboBox;
            lblAdr = this.Controls.Find("lblAdr" + sfx, true).FirstOrDefault() as Label;
            lblOpisPosla = this.Controls.Find("lblOpisPosla" + sfx, true).FirstOrDefault() as Label;
            txtOpisPosla = this.Controls.Find("txtOpisPosla" + sfx, true).FirstOrDefault() as TextBox;
            lblAdresaPreuzimanja = this.Controls.Find("lblAdresaPreuzimanja" + sfx, true).FirstOrDefault() as Label;
            lblkontaktPreuzimanja = this.Controls.Find("lblkontaktPreuzimanja" + sfx, true).FirstOrDefault() as Label;
            cbokontaktPreuzimanja = this.Controls.Find("cbokontaktPreuzimanja" + sfx, true).FirstOrDefault() as ComboBox;
            lblPreuzimanjaPraznogKontejneraNovi = this.Controls.Find("lblPreuzimanjaPraznogKontejneraNovi" + sfx, true).FirstOrDefault() as Label;
            dtPreuzimanjaPraznogKontejneraNovi = this.Controls.Find("dtPreuzimanjaPraznogKontejneraNovi" + sfx, true).FirstOrDefault() as DateTimePicker;
            dtPreuzimanjaPraznogKontejnera = this.Controls.Find("dtPreuzimanjaPraznogKontejnera" + sfx, true).FirstOrDefault() as DateTimePicker;
            dtRealiPreuzimanjaPraznogKont = this.Controls.Find("dtRealiPreuzimanjaPraznogKont" + sfx, true).FirstOrDefault() as DateTimePicker;
            dtpUtovaraCerade = this.Controls.Find("dtpUtovaraCerade" + sfx, true).FirstOrDefault() as DateTimePicker;
            dtpIstovaraCerade = this.Controls.Find("dtpIstovaraCerade" + sfx, true).FirstOrDefault() as DateTimePicker;
            dtpUtovaraCeradeNovi = this.Controls.Find("dtpUtovaraCeradeNovi" + sfx, true).FirstOrDefault() as DateTimePicker;
            dtpIstovaraCeradeNovi = this.Controls.Find("dtpIstovaraCeradeNovi" + sfx, true).FirstOrDefault() as DateTimePicker;
            dtpRealiUtovaraCerade = this.Controls.Find("dtpRealiUtovaraCerade" + sfx, true).FirstOrDefault() as DateTimePicker;
            dtpRealiIstovaraCerade = this.Controls.Find("dtpRealiIstovaraCerade" + sfx, true).FirstOrDefault() as DateTimePicker;
            lblSpustanjePunogNovi = this.Controls.Find("lblSpustanjePunogNovi" + sfx, true).FirstOrDefault() as Label;
            dtpSpustanjePunogReal = this.Controls.Find("dtpSpustanjePunogReal" + sfx, true).FirstOrDefault() as DateTimePicker;
            dtpSpustanjePunogNovi = this.Controls.Find("dtpSpustanjePunogNovi" + sfx, true).FirstOrDefault() as DateTimePicker;
            chkVaganje = this.Controls.Find("chkVaganje" + sfx, true).FirstOrDefault() as CheckBox;
            dtpSpustanjePunog = this.Controls.Find("dtpSpustanjePunog" + sfx, true).FirstOrDefault() as DateTimePicker;
            lblVrstaRobe = this.Controls.Find("lblVrstaRobe" + sfx, true).FirstOrDefault() as Label;
            lblVrstaRobeGrid = this.Controls.Find("lblVrstaRobeGrid" + sfx, true).FirstOrDefault() as Label;
            btnUnesiNHM = this.Controls.Find("btnUnesiNHM" + sfx, true).FirstOrDefault() as Button;
            txtAdresaUtovara = this.Controls.Find("txtAdresaUtovara" + sfx, true).FirstOrDefault() as TextBox;
            txtkontaktNaUtovaru = this.Controls.Find("txtkontaktNaUtovaru" + sfx, true).FirstOrDefault() as TextBox;
            cboMestoUtovara = this.Controls.Find("cboMestoUtovara" + sfx, true).FirstOrDefault() as ComboBox;
            dtpUtovara = this.Controls.Find("dtpUtovara" + sfx, true).FirstOrDefault() as DateTimePicker;
            dtpRealiUtovara = this.Controls.Find("dtpRealiUtovara" + sfx, true).FirstOrDefault() as DateTimePicker;
            dtpUtovaraNovi = this.Controls.Find("dtpUtovaraNovi" + sfx, true).FirstOrDefault() as DateTimePicker;
            dataGridVrstaRobe = this.Controls.Find("dataGridVrstaRobe" + sfx, true).FirstOrDefault() as DataGridView;
            txtNoviSpediterP = this.Controls.Find("txtNoviSpediterP" + sfx, true).FirstOrDefault() as TextBox;
            txtNoviSpediterO = this.Controls.Find("txtNoviSpediterO" + sfx, true).FirstOrDefault() as TextBox;
            txtGranicniPrelaz = this.Controls.Find("txtGranicniPrelaz" + sfx, true).FirstOrDefault() as TextBox;
            
        }
        public frmDrumski1(int ID)
        {
            this.id = ID;
            InitializeComponent();
            FillCombo();
            this.BindingContext = new BindingContext();
            ChangeTextBox();
        }

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            meniHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                meniHeader.Visible = true;
                meniHeader.Visible = false;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                //foreach (Control control in groupBox1.Controls)
                //{
                //    if (control is System.Windows.Forms.Button buttons)
                //    {

                //        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                //        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                //        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                //        buttons.FlatStyle = FlatStyle.Flat;
                //    }
                //}


                foreach (System.Windows.Forms.Control control in this.Controls)
                {

                    if (control is System.Windows.Forms.TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }

                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }
                }
            }
            else
            {
                meniHeader.Visible = false;
                meniHeader.Visible = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void VratiPodatke()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();
          
            SqlCommand cmd = new SqlCommand("SELECT	rn.ID , ik.ID AS IDNadredjena, " +
             "ISNULL(rn.NalogID, -1) AS NalogID, rn.Uvoz, rn.KontejnerID, rn.Status, rn.IDVrstaManipulacije,  rn.AutoDan, rn.Ref,  ik.MestoPreuzimanja AS MestoPreuzimanjaKontejnera, " +
             "ik.Klijent3 AS Klijent, ik.MesoUtovara AS MestoUtovara, ik.KontaktOsoba as KontaktOsobaUtovarInt, (Rtrim(pko.PaKOOpomba)) as AdresaUtovara,(Rtrim(pko.PaKOIme) + ' ' + Rtrim(pko.PaKoPriimek)) as KontaktOsobaNaUtovaru , rn.MestoIstovara AS MestoIstovara, (Rtrim(pko.PaKOIme) + ' ' + Rtrim(pko.PaKoPriimek)) + ' '  + pko.PaKOTel AS KontaktOsobaUtovarIstovar, ik.PlaniraniDatumUtovara as DatumUtovara, rn.DatumIstovara, rn.AdresaIstovara,  " +
             "ISNULL(ik.PlaniraniDtPreuzimanja, ik.planiranDtPreuzimanjaPunog) AS DtPreuzimanjaPraznogKontejnera, rn.GranicniPrelaz, CAST(ik.Spedicija AS nvarchar) AS KontaktSpeditera, " +
             "rn.Trosak, rn.Valuta, ik.BookingBrodara,  ik.BrojKontejnera,rn.BrojKontejnera2, ik.VrstaKontejnera AS TipKontejnera, ik.BrodskaPlomba AS BrojPlombe,  '' AS BrodskaTeretnica,  " +
             " ik.VGMBrod AS BTTKontejnetra, ik.BrutoRobe AS BTTRobe, " +
             " CAST(ISNULL((SELECT Top(1) IDNapomene FROM IzvozNapomenePozicioniranja where IDNadredjena = ik.ID order by ID desc),0) AS INT) AS NapomenaZaPozicioniranje, a.RegBr,rn.KamionID , a.LicnaKarta, a.Vozac, a.BrojTelefona, pa.PaNaziv AS Prevoznik, rn.Cena, cc.Naziv AS CarinjenjeIzvozno,CAST(ik.Cirada AS VARCHAR) as TipTransporta," +
             "(ccp.Oznaka + ' ' + ccp.Naziv) AS NapomenaCarinskiPostupak , ik.OdredisnaCarinarnica AS OdredisnaCarina, ik.MestoCarinjenja as polaznaCarinarnica, ik.Spedicija as polaznaSpedicija,  ik.SpediterOdredisna as  OdredisnaSpedicija, ik.KontaktSpeditera AS PolaznaSpedicijaKontakt,ik.KontaktSpediteraOdredisna AS OdredisnaSpedicijaKontakt," +
             "ik.DodatneNapomeneDrumski AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV, v.NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski," +
             "rn.DodatniTrosakTransporta, rn.BrojPosiljke, ik.CarinskiPostupakUnutrasnji , ik.VrstaBrodskePlombe, ik.Brodar,   CONVERT(NVARCHAR(50), ik.napomena3ref)  AS ReferencaFakturisanje, ik.Korisnik, " +
             " ik.KvalitetKontejnera, ik.Tara, ik.OstalePlombe, ik.MestoPreuzimanja2 as MestoSpustanjaPunog, ik.NetoRobe, ik.PlaniranDtSpustanjaPunog as PlaniraniDtSpustanjaKontejnera, ik.NacinPakovanja, ik.ADR, ik.Vaganje, ik.OpisPosla,'' AS AdresaPreuzimanjaKontejnera,'' AS KontaktPreuzimanjaKontejnera," +
             "ik.PlaniranDtPreuzimanjaPraznog as DtNoviPreuzimanjaKontejnera, ik.DtRealizacijePreuzimanjaPraznog as DtRealizacijePreuzimanjaKontejnera,ik.PlaniraniDtSpustanjaKontejnera as DtSpustanja,  ik.DtRealizacijeSpustanjaPunog as DtRealizacijeSpustanja,ik.PlaniranDtUtovaraKontejnera AS DtNoviUtovaraKontejnera, ik.DtRealizacijeUtovaraKontejnera AS DtRealizacijeUtovaraKontejnera," +
             "ik.MestoUtovaraCerade AS MestoUtovaraCerade,ik.KontaktOsobaUtovaraCerade AS KontaktOUtovaraCerade, '' AS KontaktOUtovaraCeradeString,  ik.PlaniraniDtUtovaraCerade AS DatumUtovaraCerade, ik.MestoIstovaraCerade AS MestoIstovaraCerade,  ik.KontaktOsobaIstovaraCerade AS KontaktOIstovaraCerade,'' AS KontaktOIstovaraCeradeString,  ik.PlaniraniDtIstovaraCerade AS DatumIstovaraCerade," +
             "ik.PlaniranDtIstovaraCerade as DtIstovaraCeradeNovi, ik.DtRealizacijeIstovaraCerade, ik.PlaniranDtUtovaraCerade As  DtNoviUtovaraCerade, ik.DtRealizacijeUtovaraCerade ,rn.AdresaUtovaraCerade, rn.AdresaIstovaraCerade, rn.PolaznaSpedicijaKontaktNovi, rn.OdredisnaSpedicijaKontaktNovi ," +
             "IsNull(rn.OdobrioPlaner,0) AS  OdobrioPlaner       " +
             "FROM    RadniNalogDrumski rn " +
                      "INNER JOIN IzvozKonacna ik ON rn.KontejnerID = ik.ID " +
                      "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = ik.MesoUtovara AND pko.PaKOZapSt = ik.KontaktOsoba " +
                      "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                      "LEFT JOIN Partnerji pa on a.PartnerID = pa.PaSifra " +
                      "LEFT JOIN VrstaCarinskogPostupka ccp on ccp.ID = ik.NapomenaReexport " +
                      "LEFT JOIN Carinarnice cc on cc.ID = ik.MestoCarinjenja " +
                      "LEFT JOIN IzvozKonacnaZaglavlje ukz ON ukz.ID = ik.IDNadredjena " +
                      "LEFT JOIN Voz v ON v.ID = ukz.IDVoza " +
             "where rn.ID=" + id + " AND rn.Uvoz = 0 AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1 " +
             "UNION " +
             "SELECT	rn.ID , i.ID AS IDNadredjena," +
             "ISNULL(rn.NalogID, -1) AS NalogID, rn.Uvoz, rn.KontejnerID, rn.Status, rn.IDVrstaManipulacije, rn.AutoDan, rn.Ref,i.MestoPreuzimanja AS MestoPreuzimanjaKontejnera, " +
             "i.Klijent3 AS Klijent,  i.MesoUtovara AS MestoUtovara,i.KontaktOsoba  as KontaktOsobaUtovarInt, (Rtrim(pko.PaKOOpomba)) as AdresaUtovara, (Rtrim(pko.PaKOIme) + ' ' + Rtrim(pko.PaKoPriimek)) as KontaktOsobaNaUtovaru ,rn.MestoIstovara AS MestoIstovara, (Rtrim(pko.PaKOIme) + ' ' + Rtrim(pko.PaKoPriimek)) + ' '  + pko.PaKOTel AS KontaktOsobaUtovarIstovar, i.PlaniraniDatumUtovara as DatumUtovara, rn.DatumIstovara, rn.AdresaIstovara, " +
             "ISNULL(i.PlaniraniDtPreuzimanja, i.planiranDtPreuzimanjaPunog) AS DtPreuzimanjaPraznogKontejnera, rn.GranicniPrelaz,CAST(i.Spedicija AS nvarchar) AS KontaktSpeditera, " +
             "rn.Trosak, rn.Valuta, i.BookingBrodara,  i.BrojKontejnera,rn.BrojKontejnera2,i.VrstaKontejnera AS TipKontejnera, i.BrodskaPlomba AS BrojPlombe, '' AS BrodskaTeretnica,  " +
             " i.VGMBrod AS BTTKontejnetra,  i.BrutoRobe AS BTTRobe, " +
             "CAST(ISNULL((SELECT Top(1) IDNapomene FROM IzvozNapomenePozicioniranja where IDNadredjena = i.ID order by ID desc),0) AS INT) AS NapomenaZaPozicioniranje, a.RegBr, rn.KamionID,  a.LicnaKarta, a.Vozac, a.BrojTelefona,pa.PaNaziv AS Prevoznik, rn.Cena, cc.Naziv AS CarinjenjeIzvozno, CAST(i.Cirada AS VARCHAR) as TipTransporta," +
             "(ccp.Oznaka + ' ' + ccp.Naziv) AS NapomenaCarinskiPostupak , i.OdredisnaCarinarnica AS  OdredisnaCarina,i.MestoCarinjenja as polaznaCarinarnica,  i.Spedicija as polaznaSpedicija, i.SpediterOdredisna as OdredisnaSpedicija,i.KontaktSpeditera AS PolaznaSpedicijaKontakt,i.KontaktSpediteraOdredisna AS OdredisnaSpedicijaKontakt," +
             " i.DodatneNapomeneDrumski AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV, '' as NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski ," +
             "rn.DodatniTrosakTransporta, rn.BrojPosiljke, i.CarinskiPostupakUnutrasnji, i.VrstaBrodskePlombe, i.Brodar,  CONVERT(NVARCHAR(50), i.napomena3ref)    AS ReferencaFakturisanje, i.Korisnik, " +
             "i.KvalitetKontejnera, i.Tara, i.OstalePlombe , i.MestoPreuzimanja2 as MestoSpustanjaPunog, i.NetoRobe, i.PlaniranDtSpustanjaPunog as PlaniraniDtSpustanjaKontejnera, i.NacinPakovanja, i.ADR, i.Vaganje, i.OpisPosla,'' AS AdresaPreuzimanjaKontejnera," +
             "'' AS KontaktPreuzimanjaKontejnera ,i.PlaniranDtPreuzimanjaPraznog as DtNoviPreuzimanjaKontejnera, i.DtRealizacijePreuzimanjaPraznog as DtRealizacijePreuzimanjaKontejnera,i.PlaniraniDtSpustanjaKontejnera as DtSpustanja,  i.DtRealizacijeSpustanjaPunog as DtRealizacijeSpustanja ,i.PlaniranDtUtovaraKontejnera AS DtNoviUtovaraKontejnera, i.DtRealizacijeUtovaraKontejnera AS DtRealizacijeUtovaraKontejnera ," +
             "i.MestoUtovaraCerade AS MestoUtovaraCerade,i.KontaktOsobaUtovaraCerade AS KontaktOUtovaraCerade, '' AS KontaktOUtovaraCeradeString,  i.PlaniraniDtUtovaraCerade AS DatumUtovaraCerade, i.MestoIstovaraCerade AS MestoIstovaraCerade,  i.KontaktOsobaIstovaraCerade AS KontaktOIstovaraCerade,'' AS KontaktOIstovaraCeradeString,  i.PlaniraniDtIstovaraCerade AS DatumIstovaraCerade," +
             "i.PlaniranDtIstovaraCerade as DtIstovaraCeradeNovi, i.DtRealizacijeIstovaraCerade, i.PlaniranDtUtovaraCerade As  DtNoviUtovaraCerade , i.DtRealizacijeUtovaraCerade  ,rn.AdresaUtovaraCerade, rn.AdresaIstovaraCerade, rn.PolaznaSpedicijaKontaktNovi, rn.OdredisnaSpedicijaKontaktNovi," +
             "IsNull(rn.OdobrioPlaner,0) AS  OdobrioPlaner        " +
             "FROM    RadniNalogDrumski rn " +
                      "INNER JOIN  Izvoz i ON rn.KontejnerID = i.ID  " +
                      "LEFT JOIN partnerjiKontOsebaMU pko ON  pko.PaKOSifra = i.MesoUtovara AND pko.PaKOZapSt = i.KontaktOsoba " +
                      "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                      "LEFT JOIN Partnerji pa on a.PartnerID = pa.PaSifra " +
                      "LEFT JOIN VrstaCarinskogPostupka ccp on ccp.ID = i.NapomenaReexport " +
                      "LEFT JOIN Carinarnice cc on cc.ID = i.MestoCarinjenja " +
             "where rn.ID=" + id + " AND rn.Uvoz = 0 AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1 " +
             "UNION " +
             "SELECT rn.ID , uk.ID AS IDNadredjena," +
             "ISNULL(rn.NalogID, -1) AS NalogID,rn.Uvoz,rn.KontejnerID,rn.Status,rn.IDVrstaManipulacije,rn.AutoDan,uk.Ref3 AS Ref,  rn.MestoPreuzimanjaKontejnera, " +
             "uk.Nalogodavac3 AS Klijent,  rn.MestoUtovara,-1 as KontaktOsobaUtovarInt, rn.AdresaUtovara,'' as KontaktOsobaNaUtovaru ,uk.MestoIstovara AS MestoIstovara,uk.KontaktOsobe as KontaktOsobaUtovarIstovar, rn.DatumUtovara,rn.DatumIstovara, (Rtrim(pko.PaKOOpomba)) AS AdresaIstovara, " +
             "rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz,rn.KontaktSpeditera, " +
             "rn.Trosak,rn.Valuta,0 AS BookingBrodara,  uk.BrojKontejnera,rn.BrojKontejnera2, uk.TipKontejnera, '' AS BrojPlombe,  uk.BrodskaTeretnica,  " +
             " uk.BrutoKontejnera AS BTTKontejnetra, uk.BrutoRobe AS BTTRobe," +
             " np.Naziv as NapomenaZaPozicioniranje, a.RegBr, rn.KamionID,  a.LicnaKarta, a.Vozac, a.BrojTelefona , pa.PaNaziv AS Prevoznik, rn.Cena, (vcp.Oznaka + ' ' + vcp.Naziv) as CarinjenjeIzvozno, pr.Naziv as TipTransporta, " +
             "'' AS NapomenaCarinskiPostupak,uk.OdredisnaCarina as OdredisnaCarina , 0 as polaznaCarinarnica, 0 as polaznaSpedicija, uk.OdredisnaSpedicija as OdredisnaSpedicija, '' AS PolaznaSpedicijaKontakt,'' AS OdredisnaSpedicijaKontakt, rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV, v.NAzivVoza, rn.TipTransporta AS TipTransportaDrumski," +
             "rn.DodatniTrosakTransporta, rn.BrojPosiljke , 0 AS CarinskiPostupakUnutrasnji, 0 AS VrstaBrodskePlombe, uk.Brodar AS Brodar,   CONVERT(NVARCHAR(50),uk.ref3)   AS ReferencaFakturisanje, " +
             "'' AS Korisnik, 0 AS KvalitetKontejnera, uk.TaraKontejnera AS Tara, '' AS OstalePlombe, '' as MestoSpustanjaPunog, 0 AS NetoRobe ,  rn.DtNoviSpustanja as PlaniraniDtSpustanjaKontejnera, " +
             " rn.NacinPakovanja, uk.ADR, rn.Vaganje,'' AS OpisPosla,'' AS AdresaPreuzimanjaKontejnera,'' AS KontaktPreuzimanjaKontejnera ," +
             "rn.DtNoviPreuzimanjaKontejnera, rn.DtRealizacijePreuzimanjaKontejnera,rn.DtSpustanja, rn.DtRealizacijeSpustanja,rn.DtNoviUtovaraKontejnera, rn.DtRealizacijeUtovaraKontejnera," +
             "rn.MestoUtovaraCerade ,0 AS KontaktOUtovaraCerade, '' AS KontaktOUtovaraCeradeString, rn.DtUtovaraCerade AS DatumUtovaraCerade, rn.MestoIstovaraCerade AS MestoIstovaraCerade,0 AS KontaktOIstovaraCerade, rn.KontaktOsobaIstovaraCerade  AS KontaktOIstovaraCeradeString, " +
             " rn.DtIstovaraCerade AS DatumIstovaraCerade, rn.DtNoviIstovaraCerade as DtIstovaraCeradeNovi, rn.DtRealizacijeIstovaraCerade as DtRealizacijeIstovaraCerade, rn.DtNoviUtovaraCerade, rn.DtRealizacijeUtovaraCerade,rn.AdresaUtovaraCerade, rn.AdresaIstovaraCerade  , rn.PolaznaSpedicijaKontaktNovi, rn.OdredisnaSpedicijaKontaktNovi," +
             "IsNull(rn.OdobrioPlaner,0) AS  OdobrioPlaner              " +
             "FROM  RadniNalogDrumski rn " +
                    "INNER JOIN UvozKonacna uk ON rn.KontejnerID = uk.ID " +
                    "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = uk.MestoIstovara AND PaKOZapSt = uk.AdresaMestaUtovara " + /*AND PaKOSifra = mu.Naziv*/
                    "LEFT JOIN NapomenaZaPozicioniranje np ON np.ID = uk.NapomenaZaPozicioniranje " +
                    "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                    "LEFT JOIN Partnerji pa on a.PartnerID = pa.PaSifra " +
                    "LEFT JOIN VrstePostupakaUvoz pr ON pr.ID = uk.PostupakSaRobom " +
                    "LEFT JOIN VrstaCarinskogPostupka vcp on vcp.ID = uk.CarinskiPostupak " +
                    //"LEFT JOIN Carinarnice c on c.ID = uk.OdredisnaCarina " +
                    //"LEFT JOIN Partnerji p2 on p2.PaSifra = uk.OdredisnaSpedicija " +
                    "LEFT JOIN UvozKonacnaZaglavlje ukz ON ukz.ID = uk.IDNadredjeni " +
                    "LEFT JOIN Voz v ON v.ID = ukz.IDVoza " +
             "where rn.ID= " + id + " AND rn.Uvoz = 1  AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1 " +
             "UNION " +
             "SELECT rn.ID , u.ID AS IDNadredjena," +
             "ISNULL(rn.NalogID, -1) AS NalogID,rn.Uvoz,rn.KontejnerID,rn.Status,rn.IDVrstaManipulacije,rn.AutoDan,u.Ref3 AS Ref,  rn.MestoPreuzimanjaKontejnera, " +
             "u.Nalogodavac3 AS Klijent, rn.MestoUtovara, -1 as KontaktOsobaUtovarInt, rn.AdresaUtovara,''  as KontaktOsobaNaUtovaru ,u.MestoIstovara AS MestoIstovara,u.KontaktOsobe as KontaktOsobaUtovarIstovar, rn.DatumUtovara,rn.DatumIstovara,(Rtrim(pko.PaKOOpomba)) AS AdresaIstovara,  " +
             "rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz,rn.KontaktSpeditera, " +
             "rn.Trosak,rn.Valuta,0 AS BookingBrodara,  u.BrojKontejnera,rn.BrojKontejnera2, u.TipKontejnera AS TipKontejnera, '' AS BrojPlombe,  u.BrodskaTeretnica,   " +
             "u.BrutoKontejnera AS BTTKontejnetra, u.BrutoRobe AS BTTRobe, " +
             " np.Naziv as NapomenaZaPozicioniranje, a.RegBr, rn.KamionID, a.LicnaKarta, a.Vozac, a.BrojTelefona,pa.PaNaziv AS Prevoznik, rn.Cena, (vcp.Oznaka + ' ' + vcp.Naziv) as CarinjenjeIzvozno, pr.Naziv as TipTransporta," +
             " '' AS NapomenaCarinskiPostupak, u.OdredisnaCarina as OdredisnaCarina,0 as polaznaCarinarnica, 0 as polaznaSpedicija, u.OdredisnaSpedicija, '' AS PolaznaSpedicijaKontakt,'' AS OdredisnaSpedicijaKontakt, rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru, rn.PDV,'' as NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski," +
             "rn.DodatniTrosakTransporta, rn.BrojPosiljke, 0 AS CarinskiPostupakUnutrasnji, 0 AS VrstaBrodskePlombe, u.Brodar AS Brodar,   CONVERT(NVARCHAR(50),u.ref3)   AS ReferencaFakturisanje," +
             " '' AS Korisnik, 0 AS KvalitetKontejnera, u.TaraKontejnera AS Tara  , '' AS OstalePlombe , ''  MestoSpustanjaPunog, 0 AS NetoRobe, rn.DtNoviSpustanja as PlaniraniDtSpustanjaKontejnera, rn.NacinPakovanja, u.ADR ,rn.Vaganje,'' OpisPosla,'' AS AdresaPreuzimanjaKontejnera," +
             "'' AS KontaktPreuzimanjaKontejnera,rn.DtNoviPreuzimanjaKontejnera, rn.DtRealizacijePreuzimanjaKontejnera,rn.DtSpustanja, rn.DtRealizacijeSpustanja,rn.DtNoviUtovaraKontejnera, rn.DtRealizacijeUtovaraKontejnera  ," +
            "rn.MestoUtovaraCerade ,0 AS KontaktOUtovaraCerade,'' AS KontaktOUtovaraCeradeString, rn.DtUtovaraCerade AS DatumUtovaraCerade, rn.MestoIstovaraCerade AS MestoIstovaraCerade,rn.KontaktOsobaIstovaraCerade AS KontaktOIstovaraCerade, '' AS KontaktOIstovaraCeradeString," +
            " rn.DtIstovaraCerade AS DatumIstovaraCerade, rn.DtNoviIstovaraCerade as DtIstovaraCeradeNovi, rn.DtRealizacijeIstovaraCerade as DtRealizacijeIstovaraCerade, rn.DtNoviUtovaraCerade , rn.DtRealizacijeUtovaraCerade ,rn.AdresaUtovaraCerade, rn.AdresaIstovaraCerade , rn.PolaznaSpedicijaKontaktNovi, rn.OdredisnaSpedicijaKontaktNovi," +
            "IsNull(rn.OdobrioPlaner,0) AS  OdobrioPlaner            " +
             "FROM  RadniNalogDrumski rn " +
                    "INNER JOIN  Uvoz u ON rn.KontejnerID = u.ID " +
                    "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = u.MestoIstovara AND pko.PaKOZapSt = u.AdresaMestaUtovara " + /*AND PaKOSifra = mu.Naziv*/
                    "LEFT JOIN NapomenaZaPozicioniranje np ON np.ID = u.NapomenaZaPozicioniranje " +
                    "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                    "LEFT JOIN Partnerji pa on a.PartnerID = pa.PaSifra " +
                    "LEFT JOIN VrstePostupakaUvoz pr ON pr.ID = u.PostupakSaRobom " +
                    "LEFT JOIN VrstaCarinskogPostupka vcp on vcp.ID = u.CarinskiPostupak " +
                    "LEFT JOIN Carinarnice c on c.ID = u.OdredisnaCarina " +
                    "LEFT JOIN Partnerji p2 on p2.PaSifra = u.OdredisnaSpedicija " +
             "where rn.ID= " + id + " AND rn.Uvoz = 1 AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1 " +
             "UNION " +
             "SELECT rn.ID ,  rn.ID AS IDNadredjena," +
             "ISNULL(rn.NalogID, -1) AS NalogID,rn.Uvoz,rn.KontejnerID,rn.Status,rn.IDVrstaManipulacije,rn.AutoDan,rn.Ref,  rn.MestoPreuzimanjaKontejnera, " +
             "rn.Klijent, rn.MestoUtovara, -1 as KontaktOsobaUtovarInt, rn.AdresaUtovara,rn.KontaktOsobaNaUtovaru,rn.MestoIstovara AS MestoIstovara,rn.KontaktOsobaNaIstovaru AS KontaktOsobaUtovarIstovar, rn.DatumUtovara,rn.DatumIstovara,rn.AdresaIstovara AS AdresaIstovara,  " +
             "rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz,rn.KontaktSpeditera, " +
             "rn.Trosak,rn.Valuta,rn.BookingBrodara,  rn.BrojKontejnera,rn.BrojKontejnera2, rn.TipKontejnera AS TipKontejnera, rn.BrodskaPlomba AS BrojPlombe,   rn.BrodskaTeretnica,  " +
             " rn.BrutoKontejnera AS BTTKontejnetra, rn.BrutoRobe AS BTTRobe,  " +
             "CAST(rn.NapomenaZaPozicioniranje AS varchar(50)) AS NapomenaZaPozicioniranje, a.RegBr, rn.KamionID, a.LicnaKarta, a.Vozac, a.BrojTelefona, pa.PaNaziv AS Prevoznik, rn.Cena,'' as CarinjenjeIzvozno, '' as TipTransporta," +
             " '' AS NapomenaCarinskiPostupak, rn.OdredisnaCarinarnica as OdredisnaCarina,rn.PolaznaCarinarnica , rn.PolaznaSpedicija ,rn.OdredisnaSpedicija, rn.PolaznaSpedicijaKontakt, rn.OdredisnaSpedicijaKontakt, rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru, " +
             "rn.PDV, rn.BrojVoza as NAzivVoza, rn.TipTransporta  AS TipTransportaDrumski," +
             "rn.DodatniTrosakTransporta, rn.BrojPosiljke, rn.CarinskiPostupakUnutrasnji ,  rn.VrstaBrodskePlombe , rn.Brodar, rn.Ref AS ReferencaFakturisanje ," +
             " ko.Korisnik, rn.KvalitetKontejnera , rn.Tara, rn.OstalePlombe, rn.MestoSpustanjaPunog, rn.NetoRobe , rn.DtNoviSpustanja as PlaniraniDtSpustanjaKontejnera, rn.NacinPakovanja , rn.ADR, rn.Vaganje,rn.OpisPosla AS OpisPosla,rn.AdresaPreuzimanjaKontejnera, " +
             "rn.KontaktPreuzimanjaKontejnera,rn.DtNoviPreuzimanjaKontejnera, rn.DtRealizacijePreuzimanjaKontejnera,rn.DtSpustanja, rn.DtRealizacijeSpustanja,rn.DtNoviUtovaraKontejnera, rn.DtRealizacijeUtovaraKontejnera ," +
             "rn.MestoUtovaraCerade , 0 AS KontaktOUtovaraCerade,rn.KontaktOsobaUtovaraCerade  AS KontaktOUtovaraCeradeString, rn.DtUtovaraCerade AS DatumUtovaraCerade, rn.MestoIstovaraCerade AS MestoIstovaraCerade,0 AS KontaktOIstovaraCerade,rn.KontaktOsobaIstovaraCerade AS KontaktOIstovaraCeradeString, " +
             " rn.DtIstovaraCerade AS DatumIstovaraCerade, rn.DtNoviIstovaraCerade as DtIstovaraCeradeNovi, rn.DtRealizacijeIstovaraCerade as DtRealizacijeIstovaraCerade , rn.DtNoviUtovaraCerade , rn.DtRealizacijeUtovaraCerade ,rn.AdresaUtovaraCerade, rn.AdresaIstovaraCerade , rn.PolaznaSpedicijaKontaktNovi, rn.OdredisnaSpedicijaKontaktNovi," +
             "IsNull(rn.OdobrioPlaner,0) AS  OdobrioPlaner      " +
             "FROM  RadniNalogDrumski rn " +
              "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
              "LEFT JOIN Partnerji pa on a.PartnerID = pa.PaSifra " +
              "OUTER APPLY (SELECT TOP(1) DeSifra, Korisnik FROM Korisnici   WHERE desifra = rn.nalogkreiraokorisnik)  ko " + 
             "where rn.ID= " + id + " AND rn.Uvoz in (-1,2,3, 4, 5) AND ISNULL(rn.RadniNalogOtkazan, 0) <> 1 ", con);

            SqlDataReader dr = cmd.ExecuteReader();
            int NalogID = -1;
            while (dr.Read())
            {
         
                PopuniText("txtID", sfx, dr["ID"]);
                PopuniText("txtKorisnik", sfx, dr["Korisnik"]);

                // Punjenje ComboBox-ova
                PopuniCombo("cboKlijent", sfx, dr["Klijent"]);

                // Punjenje CheckBox-ova
                PopuniCheck("chkAutoDan", sfx, dr["AutoDan"]);
                PopuniCheck("chkPDV", sfx, dr["PDV"]);
                PopuniCheck("chkDodatniTrosak", sfx, dr["DodatniTrosakTransporta"]);
                PopuniCheck("chkVaganje", sfx, dr["Vaganje"]);

                Uvoz = Convert.ToInt32(dr["Uvoz"].ToString());
                string tipNaloga = Uvoz == 1 ? "Uvoz" : "Izvoz";
                int TipNaloga = Convert.ToInt32(dr["Uvoz"].ToString());

                if (dr["ID"] != DBNull.Value && int.TryParse(dr["ID"].ToString(), out int parsedID))
                    radninalogdrumskiID = parsedID;

                PopuniText("txtReferenca", sfx, dr["ReferencaFakturisanje"]);
                PopuniText("txtBokingBrodara", sfx, dr["BookingBrodara"]);
                PopuniText("txtBrodskaPlomba", sfx, dr["BrojPlombe"]);
                PopuniText("txtBrojKontejnera", sfx, dr["BrojKontejnera"]);

                PopuniCombo("cboMestoUtovaraCerade", sfx, dr["MestoUtovaraCerade"]);
                PopuniCombo("cboMestoIstovaraCerade", sfx, dr["MestoIstovaraCerade"]);
                lOOdobrio = Convert.ToInt32(dr["OdobrioPlaner"].ToString()); 


                if (cboMestoUtovaraCerade != null && cboMestoIstovaraCerade != null)
                {
                    if (Uvoz == 3) // samo za interni nalog popunjavamo adresu u posebno polje
                    {
                        // Forsiramo Windows da završi sa obradom DataSource-a pre nego što upišemo tekst
                        // Application.DoEvents();
                        cboAdresaUtovaraCerade.Text = dr["AdresaUtovaraCerade"].ToString();
                        cboKontaktUtovaraCerade.Text = dr["KontaktOUtovaraCeradeString"].ToString(); // Pretpostavljam da i ovo treba

                        cboAdresaIstovaraCerade.Text = dr["AdresaIstovaraCerade"].ToString();
                        cboKontaktIstovaraCerade.Text = dr["KontaktOIstovaraCeradeString"].ToString(); // Pretpostavljam da i ovo treba
                    }
                    else
                    {

                        PopuniAdresu(cboMestoUtovaraCerade, cboAdresaUtovaraCerade);
                        PopuniKontaktOsobu(cboMestoUtovaraCerade, cboKontaktUtovaraCerade);

                        PopuniAdresu(cboMestoIstovaraCerade, cboAdresaIstovaraCerade);
                        PopuniKontaktOsobu(cboMestoIstovaraCerade, cboKontaktIstovaraCerade);
                    }
                }

                if (cboMestoPreuzimanja != null && cboMestoPreuzimanja != null)
                {
                    if (Uvoz == 3) // samo za interni nalog popunjavamo adresu u posebno polje
                    {
                        // Forsiramo Windows da završi sa obradom DataSource-a pre nego što upišemo tekst
                        // Application.DoEvents();
                        if(cboAdresaPreuzimanja != null)
                        {
                            cboAdresaPreuzimanja.Text = dr["AdresaPreuzimanjaKontejnera"].ToString();
                            cbokontaktPreuzimanja.Text = dr["KontaktPreuzimanjaKontejnera"].ToString(); //

                        }
                    }
                    else
                    {
                        if (cboAdresaPreuzimanja != null)
                        {
                            PopuniAdresu(cboMestoPreuzimanja, cboAdresaPreuzimanja);
                            PopuniKontaktOsobu(cboMestoPreuzimanja, cbokontaktPreuzimanja);
                        }
                    }
                }

                PopuniCombo("cboMestoPreuzimanja", sfx, dr["MestoPreuzimanjaKontejnera"]);
                PopuniCombo("cboMestoUtovara", sfx, dr["MestoUtovara"]);
                PopuniText("txtKontaktOsobaUtovara", sfx, dr["KontaktOsobaUtovarInt"]);
                PopuniText("txtOstalePlombe", sfx, dr["OstalePlombe"]);            
                //PopuniText("txtAdresaPreuzimanja", sfx, dr["AdresaPreuzimanjaKontejnera"]);
   
                //PopuniText("txtkontaktPreuzimanja", sfx, dr["KontaktPreuzimanjaKontejnera"]);

                PopuniText("txtAdresaUtovara", sfx, dr["AdresaUtovara"]);
                PopuniText("txtkontaktNaUtovaru", sfx, dr["KontaktOsobaNaUtovaru"]);
                PopuniText("txtAdresaIstovara", sfx, dr["AdresaIstovara"]);

                PopuniDatum("dtIstovara", sfx, dr["DatumIstovara"]);

                // Decimalna polja (Bruto roba, Težina, Cena...)
                PopuniDecimal("txtBrutoK", sfx, dr["BTTRobe"]);
                PopuniDecimal("txtTaraK", sfx, dr["Tara"]);
                PopuniDecimal("txtNTTOR", sfx, dr["NetoRobe"]);
                PopuniDecimal("txtCena", sfx, dr["Cena"]);
                PopuniDecimal("txtTrosak", sfx, dr["Trosak"]);

            
                PopuniDatum("DtPreuzimanjaPraznogKontejnera", sfx, dr["DtPreuzimanjaPraznogKontejnera"]);
                PostaviTag(dr["DtPreuzimanjaPraznogKontejnera"], dtPreuzimanjaPraznogKontejnera);
                PopuniDatum("dtRealiPreuzimanjaPraznogKon", sfx, dr["DtRealizacijePreuzimanjaKontejnera"]);
                PostaviTag(dr["DtRealizacijePreuzimanjaKontejnera"], dtRealiPreuzimanjaPraznogKont);
                PopuniDatum("dtPreuzimanjaPraznogKontejneraNovi", sfx, dr["DtNoviPreuzimanjaKontejnera"]);
                PostaviTag(dr["DtNoviPreuzimanjaKontejnera"], dtPreuzimanjaPraznogKontejneraNovi);
                PopuniDatum("dtpSpustanjePunog", sfx, dr["DtSpustanja"]);
                PostaviTag(dr["DtSpustanja"], dtpSpustanjePunog);
                PopuniDatum("dtpSpustanjePunogNovi", sfx, dr["PlaniraniDtSpustanjaKontejnera"]);
                PostaviTag(dr["PlaniraniDtSpustanjaKontejnera"], dtpSpustanjePunogNovi);
                PopuniDatum("dtpSpustanjePunogReal", sfx, dr["DtRealizacijeSpustanja"]);
                PostaviTag(dr["DtRealizacijeSpustanja"], dtpSpustanjePunogReal);
                PopuniDatum("dtpUtovara", sfx, dr["DatumUtovara"]);
                PostaviTag(dr["DatumUtovara"], dtpUtovara);
                PopuniDatum("dtpUtovaraNovi", sfx, dr["DtNoviUtovaraKontejnera"]);
                PostaviTag(dr["DtNoviUtovaraKontejnera"], dtpUtovaraNovi);
                PopuniDatum("dtpRealiUtovara", sfx, dr["DtRealizacijeUtovaraKontejnera"]);
                PostaviTag(dr["DtRealizacijeUtovaraKontejnera"], dtpRealiUtovara);

                PopuniDatum("dtpUtovaraCerade", sfx, dr["DatumUtovaraCerade"]);
                PostaviTag(dr["DatumUtovaraCerade"], dtpUtovaraCerade);
                PopuniDatum("dtpUtovaraCeradeNovi", sfx, dr["DtNoviUtovaraCerade"]);
                PostaviTag(dr["DtNoviUtovaraCerade"], dtpUtovaraCeradeNovi);
                PopuniDatum("dtpRealiUtovaraCerade", sfx, dr["DtRealizacijeUtovaraCerade"]);
                PostaviTag(dr["DtRealizacijeUtovaraCerade"], dtpRealiUtovaraCerade);
                PopuniDatum("dtpIstovaraCerade", sfx, dr["DatumIstovaraCerade"]);
                PostaviTag(dr["DatumIstovaraCerade"], dtpIstovaraCerade);
                PopuniDatum("dtpIstovaraCeradeNovi", sfx, dr["DtIstovaraCeradeNovi"]);
                PostaviTag(dr["DtIstovaraCeradeNovi"], dtpIstovaraCeradeNovi);
                PopuniDatum("dtpRealiIstovaraCerade", sfx, dr["DtRealizacijeIstovaraCerade"]);
                PostaviTag(dr["DtRealizacijeIstovaraCerade"], dtpRealiIstovaraCerade);


                PopuniCombo("cboTipTransporta", sfx, dr["TipTransportaDrumski"]);
                PopuniCombo("cboKvalitetKontejnera", sfx, dr["KvalitetKontejnera"]);
                PopuniCombo("cboMestoSpustanjaPunog", sfx, dr["MestoSpustanjaPunog"]);
                PopuniCombo("cboNacinPakovanja", sfx, dr["NacinPakovanja"]);
                PopuniCombo("cboADR", sfx, dr["ADR"]);
                PopuniCombo("cboNapomenaPoz", sfx, dr["NapomenaZaPozicioniranje"]);
                PopuniText("txtNapomenaPoz", sfx, dr["NapomenaZaPozicioniranje"]);
                PopuniText("txtOpisPosla", sfx, dr["OpisPosla"]);

                //if (Uvoz != 1 && Uvoz != 0)
                //{
                //    PopuniCombo("cboNapomenaPoz", sfx, dr["NapomenaZaPozicioniranje"]);
                //}
                //else if (Uvoz == 1 || Uvoz == 0)
                //    PopuniText("txtNapomenaPoz", sfx, dr["NapomenaZaPozicioniranje"]);

                PopuniText("txtGranicniPrelaz", sfx, dr["GranicniPrelaz"]);
               
              
                if (dr["Valuta"] != DBNull.Value)
                {
                    PopuniCombo("txtValuta", sfx, dr["Valuta"]);
                }
                PopuniCombo("cboStatus", sfx, dr["Status"]);

                PopuniCombo("cboTipNaloga", sfx, dr["Uvoz"]);
                PopuniText("txtTipNaloga", sfx, tipNaloga);

                PopuniCombo("cboVrstaKontejnera", sfx, dr["TipKontejnera"]);


                PopuniText("txtDodatniOpis", sfx, dr["DodatniOpis"]);

                if (dr["OdredisnaCarina"] != DBNull.Value && int.TryParse(dr["OdredisnaCarina"].ToString(), out int parsedOdredisnaCarina))
                {
                    cboOCarinarnicaI2.SelectedValue = parsedOdredisnaCarina;
                }
                else
                {
                    cboOCarinarnicaI2.SelectedIndex = -1;
                }
                PopuniCombo("cboPolaznaCarinarnica", sfx, dr["PolaznaCarinarnica"]);
                PopuniCombo("cboOCarinarnica", sfx, dr["OdredisnaCarina"]);
                //if (dr["PolaznaCarinarnica"] != DBNull.Value && int.TryParse(dr["PolaznaCarinarnica"].ToString(), out int parsedPolaznaCarinarnica))
                //{
                //    cboPolaznaCarinarnicaI2.SelectedValue = parsedPolaznaCarinarnica;
                //}
                //else
                //{
                //    cboPolaznaCarinarnicaI2.SelectedIndex = -1;
                //}
                PopuniText("txtKontaktOSpedicije", sfx, dr["OdredisnaSpedicijaKontakt"]);
                PopuniText("txtKontaktPolazneSpedicije", sfx, dr["PolaznaSpedicijaKontakt"]);

                PopuniText("txtNoviSpediterP", sfx, dr["PolaznaSpedicijaKontaktNovi"]);
                PopuniText("txtNoviSpediterO", sfx, dr["OdredisnaSpedicijaKontaktNovi"]);

                PopuniCombo("cbOspedicija", sfx, dr["OdredisnaSpedicija"]);
                PopuniCombo("cboPolaznaSpedicija", sfx, dr["PolaznaSpedicija"]);

                PopuniCombo("cboCarinskiPUnutrasniTransport", sfx, dr["CarinskiPostupakUnutrasnji"]);

                PopuniCombo("cboBPlombaVlasnik", sfx, dr["VrstaBrodskePlombe"]);

                PopuniCombo("cboBrodar", sfx, dr["Brodar"]);
               
                if (dr["NalogID"] != DBNull.Value && int.TryParse(dr["NalogID"].ToString(), out int convertedNalogID))
                    NalogID = convertedNalogID;

                int idNadredjena = 0;
                if (dr["idNadredjena"] != DBNull.Value)
                    idNadredjena= Convert.ToInt32(dr["idNadredjena"].ToString());
               

                DataTable dtIzBaze = VratiPodatkeIzBazeNHM(idNadredjena);
                OsveziGridNHM(dtIzBaze);

            }
            con.Close();
            

            if (Uvoz == 0)
            {
                SetVisible(txtBrodskaPlomba, false);
                SetVisible(lblBrodskaPlomba, false);
                SetVisible(cboTipNaloga, false);
                SetVisible(txtTipNaloga, true);
                SetVisible(cboNacinPakovanja, false);
                SetVisible(lblNacinPakovanja, false);
                SetVisible(txtNTTOR, false);
                SetVisible(lblNTTOR, false);
                SetVisible(lblAdresaPreuzimanja, false);
                SetVisible(cboAdresaPreuzimanja, false);
                SetVisible(lblkontaktPreuzimanja, false);
                SetVisible(cbokontaktPreuzimanja, false);
                SetVisible(lblVrstaRobe, false);
                SetVisible(cboVrstaRobe, false);
                SetVisible(btnUnesiNHM, false);

                SetText(lblKorisnik, "Nalog poslao korisnik");
                SetEnabled(chkVaganje, false);
                SetEnabled(txtKorisnik,  false);
                SetEnabled(txtBokingBrodara, false);
                SetEnabled(txtReferenca, false);
                SetEnabled(cboKlijent, false);
                SetEnabled(cboVrstaRobe, false);

                SetEnabled(cboCarinskiPUnutrasniTransport,false);

                SetEnabled(txtDodatniOpis,true);
                SetEnabled(cboNapomenaPoz,false);
                SetEnabled(txtBrojKontejnera,false);
                SetEnabled(cboVrstaKontejnera,false);
                SetEnabled(cboKvalitetKontejnera,false);
                SetEnabled(cboBrodar,false);
                SetEnabled(cboBPlombaVlasnik,false);
                SetEnabled(txtOstalePlombe,false);
                SetEnabled(txtTaraK,false);
                SetEnabled(txtBrutoK,false);
                SetEnabled(cboTipTransporta,false);
                SetEnabled(cboPolaznaCarinarnica,false);
                SetEnabled(cboPolaznaSpedicija,false);
                SetEnabled(txtKontaktPolazneSpedicije,false);
                SetEnabled(cboOCarinarnica,false);
                SetEnabled(cbOspedicija,false);
                SetEnabled(txtKontaktOSpedicije,false);
                SetEnabled(txtDodatniOpis,false);
                SetEnabled(cboMestoPreuzimanja,false);
                SetEnabled(cboMestoSpustanjaPunog,false);
                SetEnabled(dtPreuzimanjaPraznogKontejnera,false);
                SetEnabled(cboADR,false);
                SetEnabled(txtOpisPosla,false);
                SetEnabled(txtAdresaUtovara, false);
                SetEnabled(txtkontaktNaUtovaru, false);
                SetEnabled(cboMestoUtovara, false);
                SetEnabled(dtpUtovara, false);

                if (izborAdr == 0)
                {
                    SetVisible(cboADR, false);
                    SetVisible(lblAdr, false);
                }
                if(tipNaloga == 3)
                {

                    SetEnabled(cboMestoIstovaraCeradeI3, false);
                    SetEnabled(cboAdresaIstovaraCeradeI3, false);
                    SetEnabled(cboKontaktIstovaraCeradeI3, false);
                    SetEnabled(cboMestoUtovaraCeradeI3, false);
                    SetEnabled(cboAdresaUtovaraCeradeI3, false);
                    SetEnabled(cboKontaktUtovaraCeradeI3, false);
                    SetEnabled(dtpUtovaraCeradeI3, false);
                   // SetEnabled(dtpIstovaraCeradeI3, false);
                    SetVisible(lblBrodarI3, false);
                    SetVisible(cboBrodarI3, false);
                    SetVisible(lblKvalitetKontejneraI3, false);
                    SetVisible(cboKvalitetKontejneraI3, false);

                 }
                else if (tipNaloga == 4)
                {

                    SetEnabled(cboMestoIstovaraCeradeI4, false);
                    SetEnabled(cboAdresaIstovaraCeradeI4, false);
                    SetEnabled(cboKontaktIstovaraCeradeI4, false);
                    SetEnabled(cboMestoUtovaraCeradeI4, false);
                    SetEnabled(cboAdresaUtovaraCeradeI4, false);
                    SetEnabled(cboKontaktUtovaraCeradeI4, false);
                    SetEnabled(dtpUtovaraCeradeI4, false);
                //    SetEnabled(dtpIstovaraCeradeI4, false);
                    SetVisible(lblBrodarI4, false);
                    SetVisible(cboBrodarI4, false);
                    SetVisible(lblKvalitetKontejneraI4, false);
                    SetVisible(cboKvalitetKontejneraI4, false);

                }

                //    btnFormiranjeNaloga.Visible = false;
                //    // button3.Visible = NalogID > 0 ? false : true;

            }
            else if (Uvoz == 1)
            {
                //    //label29.Visible = false;
                //    txtBrodskaPlomba.Enabled = false;
                //    txtBokingBrodaraI2.Enabled = false;
                //    txtBL.Visible = true;
                //    txtReferenca.Enabled = false;
                //    txtAdresaUtovara.Enabled = true;
                //    cboMestoIstovara.Enabled = false;
                //    txtAdresaIstovara.Enabled = false;
                //    txtkontaktNaIstovaru.Enabled = false;
                //    txtBL.Enabled = false;
                //    cboKlijent.Enabled = false;
                //    cboVrstaKontejnera.Enabled = false;
                //    txtBrutoK.Enabled = false;
                //    txtBrutoR.Enabled = false;
                //    txtBrojVoza.Enabled = false;
                cboTipNaloga.Visible = false;
                txtTipNaloga.Visible = true;
                //    txtBrojKontejnera.Enabled = false;
                //    label12.Text = "Kontakt osoba na istovaru";
                //    cboPolaznaCarinarnica.Enabled = false;
                //    cboPolaznaSpedicija.Enabled = false;
                //    txtKontaktPolazneSpedicije.Enabled = false;
                //    cboOCarinarnica.Enabled = false;
                //    cbOspedicija.Enabled = false;

                //    txtKontaktOSpedicije.Enabled = true;
                //    cboNapomenaPoz.Visible = false;
                //    txtNapomenaPoz.Visible = true;
                //    btnFormiranjeNaloga.Visible = false;
                //    //   button3.Visible = NalogID > 0 ? false : true;
            }
            else if (Uvoz == 2)
            {
                //    btnFormiranjeNaloga.Visible = !(NalogID > 0 || (mainNalogID.HasValue && mainNalogID > 0));
                //    //btnFormiranjeNaloga.Visible = NalogID > 0 ? false : true;
                //    label12.Text = "Kontakt osoba na istovaru";
                cboTipNaloga.Visible = true;
                txtTipNaloga.Visible = false;



                //    cboKlijent.Enabled = true;
                //    cboVrstaKontejnera.Enabled = true;
                //    if (drumskiNew == false)
                //        button21.Visible = true;
                //    cboNapomenaPoz.Visible = true;
                //    txtNapomenaPoz.Visible = false;
                //    txtBokingBrodaraI2.Enabled = false;

                //    //delete je bilo vidljivo jedino ako nije dodeljeno nekom nalogu
                //    // button21.Visible = NalogID > 0 ? false : true;
                //    //  button3.Visible = NalogID > 0 ? false : true;
            }
            else if (Uvoz == 3)
            {
                //    txtBL.Visible = true;
                //    //btnFormiranjeNaloga.Visible = NalogID > 0 ? false : true;
                btnFormiranjeNaloga.Visible = !(NalogID > 0 || (mainNalogID.HasValue && mainNalogID > 0));
                //    if (drumskiNew == false)
                //        button21.Visible = true;
                //    //button21.Visible = NalogID > 0 ? false : true;
                //    //     button3.Visible = NalogID > 0 ? false : true;
                //    label12.Text = "Kontakt osoba na utovaru";
                SetVisible(cboTipNaloga, true);
                SetVisible(txtTipNaloga, false);
                //SetVisible(txtOpisPosla, false);
                //SetVisible(lblOpisPosla, false);
                SetEnabled(txtKorisnik, false);

                //     SetVisible(lblVrstaRobeGrid, false);

                SetEnabled(cboTipTransporta, false);

                if (izborAdr == 0)
                {
                    SetVisible(cboADR, false);
                    SetVisible(lblAdr, false);
                    if (tipNaloga != 3)
                    {
                        SetVisible(cboNacinPakovanja, false);
                        SetVisible(lblNacinPakovanja, false);
                        SetVisible(dataGridVrstaRobe, false);
                        SetVisible(cboVrstaRobe, false);
                        SetVisible(lblVrstaRobe, false);
                        SetVisible(btnUnesiNHM, false);
                    }
                }
               if (tipNaloga == 3 )
                {
                    SetVisible(lblBrodarI3, false);
                    SetVisible(cboBrodarI3, false);
                    SetVisible(lblKvalitetKontejneraI3, false);
                    SetVisible(cboKvalitetKontejneraI3, false);

                    
                    SetVisible(lblVrstaKontejneraI3, false);
                    SetVisible(cboVrstaKontejneraI3, false);
                    SetVisible(lblOstalePlombeI3, false);
                    SetVisible(txtOstalePlombeI3, false);
                    SetVisible(lblBPlombaVlasnikI3, false);
                    SetVisible(cboBPlombaVlasnikI3, false);
                    
                    SetVisible(lblVrstaRobeI3, false);
                    SetVisible(cboVrstaRobeI3, false);
                    SetVisible(btnUnesiNHMI3, false);
                    SetVisible(dataGridVrstaRobeI3, false);
                 
                    SetVisible(txtBrojKontejneraI3, false);
                    SetVisible(lblBrojKontejneraI3, false);
                    
                    SetVisible(txtBrutoKI3, false);
                    SetVisible(lblBrutoKI3, false);


                    if (daLiJeCarinskiPostupak == 0)
                    {
                        cboCarinskiPUnutrasniTransportI3.Visible = false;
                        cboPolaznaCarinarnicaI3.Visible = false;
                        cboOCarinarnicaI3.Visible = false;
                        cboPolaznaSpedicijaI3.Visible = false;
                        txtKontaktPolazneSpedicijeI3.Visible = false;
                        txtKontaktOSpedicijeI3.Visible = false;
                        cbOspedicijaI3.Visible = false;
                        txtNoviSpediterPI3.Visible = false;
                        txtNoviSpediterOI3.Visible = false;

                        lblCarinskiPUnutrasniTransportI3.Visible = false;
                        lblPolaznaCarinarnicaI3.Visible = false;
                        lblOCarinarnicaI3.Visible = false;
                        lblPolaznaSpedicijaI3.Visible = false;
                        lblKontaktPolazneSpedicijeI3.Visible = false;
                        lblKontaktOSpedicijeI3.Visible = false;
                        lblOspedicijaI3.Visible = false;
                        lblNoviSpediterPI3.Visible = false;
                        lblNoviSpediterOI3.Visible = false;
                    }
                }

               
            }
            //else if (Uvoz == 4)
            //{
            //    txtBokingBrodaraI2.Enabled = false;
            //    btnFormiranjeNaloga.Visible = !(NalogID > 0 || (mainNalogID.HasValue && mainNalogID > 0));
            //    //btnFormiranjeNaloga.Visible = NalogID > 0 ? false : true;
            //    if (drumskiNew == false)
            //        button21.Visible = true;
            //    //button21.Visible = NalogID > 0 ? false : true;
            //    //     button3.Visible = NalogID > 0 ? false : true;
            //    label12.Text = "Kontakt osoba na utovaru";
            //    cboTipNalogaI2.Visible = true;
            //    txtTipNaloga1.Visible = false;
            //    cboKlijent.Enabled = true;
            //    cboVrstaKontejnera.Enabled = true;
            //    cboNapomenaPoz.Visible = true;
            //    txtNapomenaPoz.Visible = false;
            //}
            //else if (Uvoz == 5)
            //{
            //    txtBL.Visible = true;
            //    btnFormiranjeNaloga.Visible = !(NalogID > 0 || (mainNalogID.HasValue && mainNalogID > 0));
            //    //btnFormiranjeNaloga.Visible = NalogID > 0 ? false : true;
            //    if (drumskiNew == false)
            //        button21.Visible = true;
            //    //button21.Visible = NalogID > 0 ? false : true;
            //    //     button3.Visible = NalogID > 0 ? false : true;
            //    label12.Text = "Kontakt osoba na utovaru";
            //    cboTipNalogaI2.Visible = true;
            //    txtTipNaloga1.Visible = false;
            //    cboKlijent.Enabled = true;
            //    cboVrstaKontejnera.Enabled = true;
            //    cboNapomenaPoz.Visible = true;
            //    txtNapomenaPoz.Visible = false;
            //}
            //else
            //{
            //    //  button3.Visible = NalogID > 0 ? false : true;
            //    cboTipNalogaI2.Visible = true;
            //    txtTipNaloga1.Visible = false;
            //    cboKlijent.Enabled = true;
            //    cboVrstaKontejnera.Enabled = true;
            //    cboNapomenaPoz.Visible = true;
            //    txtNapomenaPoz.Visible = false;
            //}      
        }

        private void SetVisible(Control ctrl, bool visible)
        {
            if (ctrl != null)
            {
                ctrl.Visible = visible;
            }
        }

        private void SetEnabled(Control ctrl, bool enabled)
        {
            if (ctrl != null)
            {
                ctrl.Enabled = enabled;
            }
        }

        private void SetText(Control ctrl, string text)
        {
            if (ctrl != null)
            {
                ctrl.Text = text;
            }
        }

        public void FillComboSpedicija()
        {
            SqlConnection conn = new SqlConnection(connection);
            var partner5 = "Select PaSifra,PaNaziv From Partnerji where Spediter = 1";
            SqlDataAdapter partAD5 = new SqlDataAdapter(partner5, conn);
            DataTable dtPart5 = new DataTable();
            partAD5.Fill(dtPart5);

            cbOspedicijaI1.DataSource = dtPart5.Copy();
            cbOspedicijaI1.DisplayMember = "PaNaziv";
            cbOspedicijaI1.ValueMember = "PaSifra";

            cbOspedicijaI2.DataSource = dtPart5.Copy();
            cbOspedicijaI2.DisplayMember = "PaNaziv";
            cbOspedicijaI2.ValueMember = "PaSifra";

            cbOspedicijaI3.DataSource = dtPart5.Copy();
            cbOspedicijaI3.DisplayMember = "PaNaziv";
            cbOspedicijaI3.ValueMember = "PaSifra";

            cbOspedicijaI4.DataSource = dtPart5.Copy();
            cbOspedicijaI4.DisplayMember = "PaNaziv";
            cbOspedicijaI4.ValueMember = "PaSifra";

            var partner4 = "SELECT PaSifra,PaNaziv From Partnerji where Spediter = 1";
            SqlDataAdapter partAD4 = new SqlDataAdapter(partner4, conn);
            DataTable dtPart4 = new DataTable();
            partAD4.Fill(dtPart4);

            cboPolaznaSpedicijaI1.DataSource = dtPart4.Copy();
            cboPolaznaSpedicijaI1.DisplayMember = "PaNaziv";
            cboPolaznaSpedicijaI1.ValueMember = "PaSifra";

            cboPolaznaSpedicijaI2.DataSource = dtPart4.Copy();
            cboPolaznaSpedicijaI2.DisplayMember = "PaNaziv";
            cboPolaznaSpedicijaI2.ValueMember = "PaSifra";

            cboPolaznaSpedicijaI3.DataSource = dtPart4.Copy();
            cboPolaznaSpedicijaI3.DisplayMember = "PaNaziv";
            cboPolaznaSpedicijaI3.ValueMember = "PaSifra";

            cboPolaznaSpedicijaI4.DataSource = dtPart4.Copy();
            cboPolaznaSpedicijaI4.DisplayMember = "PaNaziv";
            cboPolaznaSpedicijaI4.ValueMember = "PaSifra";

        }
        public void FillCombo()
        {
       
            SqlConnection conn = new SqlConnection(connection);
            var val = "Select VaSifra, VaNaziv from Valute order by VaSifra";
            SqlDataAdapter daVal = new SqlDataAdapter(val, conn);
            DataTable dtVal = new DataTable();
            daVal.Fill(dtVal);

            txtValutaI1.DataSource = dtVal.Copy();
            txtValutaI1.DisplayMember = "VaNaziv";
            txtValutaI1.ValueMember = "VaSifra";

            txtValutaI2.DataSource = dtVal.Copy();
            txtValutaI2.DisplayMember = "VaNaziv";
            txtValutaI2.ValueMember = "VaSifra";

            txtValutaI3.DataSource = dtVal.Copy();
            txtValutaI3.DisplayMember = "VaNaziv";
            txtValutaI3.ValueMember = "VaSifra";

            txtValutaI4.DataSource = dtVal.Copy();
            txtValutaI4.DisplayMember = "VaNaziv";
            txtValutaI4.ValueMember = "VaSifra";

            // postavi na default vrednost
            txtValutaI1.SelectedValue = "EUR";
            txtValutaI2.SelectedValue = "EUR";
            txtValutaI3.SelectedValue = "EUR";

            var stv = "select ID, Ltrim(Rtrim(Naziv)) AS Naziv from StatusVozila order by Naziv";
            SqlDataAdapter stvAD = new SqlDataAdapter(stv, conn);
            DataTable dtStv = new DataTable();
            stvAD.Fill(dtStv);

            System.Data.DataTable dt = dtStv.Copy();
            DataRow prazanRed = dt.NewRow();
            prazanRed["ID"] = DBNull.Value;
            prazanRed["Naziv"] = "";
            dt.Rows.InsertAt(prazanRed, 0);

            cboStatusI1.DataSource = dt;
            cboStatusI1.DisplayMember = "Naziv";
            cboStatusI1.ValueMember = "ID";

            cboStatusI2.DataSource = dt;
            cboStatusI2.DisplayMember = "Naziv";
            cboStatusI2.ValueMember = "ID";

            cboStatusI3.DataSource = dt;
            cboStatusI3.DisplayMember = "Naziv";
            cboStatusI3.ValueMember = "ID";

            cboStatusI4.DataSource = dt;
            cboStatusI4.DisplayMember = "Naziv";
            cboStatusI4.ValueMember = "ID";

            string uslovTipVozila = "1 = 1 ";
            if (_tipoviIn?.Any() == true)
            {
                string lista = string.Join(",", _tipoviIn);
                uslovTipVozila += $" AND ID IN ({lista}) ";
            }

            if (_tipoviNotIn?.Any() == true)
            {
                string lista = string.Join(",", _tipoviNotIn);
                uslovTipVozila += $" AND ID NOT IN ({lista}) ";
            }

            var tpv = $" select ID, LTRIM(RTRIM(Naziv)) as Naziv from VrstaVozila  WHERE {uslovTipVozila}";
            var tpvAD = new SqlDataAdapter(tpv, conn);
            var tpvDS = new DataSet();
            tpvAD.Fill(tpvDS);

            System.Data.DataTable dt2 = tpvDS.Tables[0];
            if (_tipoviIn?.Any() == null)
            {
                DataRow prazanRed2 = dt2.NewRow();
                prazanRed2["ID"] = DBNull.Value;
                prazanRed2["Naziv"] = "";
                dt2.Rows.InsertAt(prazanRed2, 0);
            }

            cboTipTransportaI1.DataSource = dt2.Copy(); 
            cboTipTransportaI1.DisplayMember = "Naziv";
            cboTipTransportaI1.ValueMember = "ID";

            cboTipTransportaI2.DataSource = dt2.Copy(); 
            cboTipTransportaI2.DisplayMember = "Naziv";
            cboTipTransportaI2.ValueMember = "ID";

            cboTipTransportaI3.DataSource = dt2.Copy();
            cboTipTransportaI3.DisplayMember = "Naziv";
            cboTipTransportaI3.ValueMember = "ID";

            cboTipTransportaI4.DataSource = dt2.Copy();
            cboTipTransportaI4.DisplayMember = "Naziv";
            cboTipTransportaI4.ValueMember = "ID";

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("ID", typeof(int));
            dt1.Columns.Add("Naziv", typeof(string));

            // Prazan red (opciono)
            DataRow prazan = dt.NewRow();
            prazan["ID"] = DBNull.Value;
            prazan["Naziv"] = "";
            dt.Rows.Add(prazan);

            // Fiksne vrednosti
            dt1.Rows.Add(4, "Uvoz");
            dt1.Rows.Add(5, "Izvoz");
            dt1.Rows.Add(2, "3PU");
            dt1.Rows.Add(3, "3PI");

            cboTipNalogaI1.DataSource = dt1;
            cboTipNalogaI1.DisplayMember = "Naziv";
            cboTipNalogaI1.ValueMember = "ID";

            cboTipNalogaI2.DataSource = dt1;
            cboTipNalogaI2.DisplayMember = "Naziv";
            cboTipNalogaI2.ValueMember = "ID";

            cboTipNalogaI3.DataSource = dt1;
            cboTipNalogaI3.DisplayMember = "Naziv";
            cboTipNalogaI3.ValueMember = "ID";

            cboTipNalogaI4.DataSource = dt1;
            cboTipNalogaI4.DisplayMember = "Naziv";
            cboTipNalogaI4.ValueMember = "ID";

            var klijent = "Select PaSifra,PaNaziv, Brodar, Spediter From Partnerji order by PaNaziv";
            SqlDataAdapter sviPartneriAD = new SqlDataAdapter(klijent, conn);
            DataTable dtSviPartneri = new DataTable();
            sviPartneriAD.Fill(dtSviPartneri);
            cboKlijentI1.DataSource = dtSviPartneri.Copy();
            cboKlijentI1.DisplayMember = "PaNaziv";
            cboKlijentI1.ValueMember = "PaSifra";

            cboKlijentI2.DataSource = dtSviPartneri.Copy();
            cboKlijentI2.DisplayMember = "PaNaziv";
            cboKlijentI2.ValueMember = "PaSifra";

            cboKlijentI3.DataSource = dtSviPartneri.Copy();
            cboKlijentI3.DisplayMember = "PaNaziv";
            cboKlijentI3.ValueMember = "PaSifra";

            cboKlijentI4.DataSource = dtSviPartneri.Copy();
            cboKlijentI4.DisplayMember = "PaNaziv";
            cboKlijentI4.ValueMember = "PaSifra";

            // Brodar (Filtriramo već učitane partnere gde je Brodar = 1)
            DataView dvBrodari = new DataView(dtSviPartneri);
            dvBrodari.RowFilter = "Brodar = 1";

            cboBrodarI1.DataSource = dvBrodari.ToTable(); // Kreira novu tabelu samo sa brodarima
            cboBrodarI1.DisplayMember = "PaNaziv";
            cboBrodarI1.ValueMember = "PaSifra";


            cboBrodarI2.DataSource = dvBrodari.ToTable(); // Kreira novu tabelu samo sa brodarima
            cboBrodarI2.DisplayMember = "PaNaziv";
            cboBrodarI2.ValueMember = "PaSifra";

            cboBrodarI3.DataSource = dvBrodari.ToTable(); // Kreira novu tabelu samo sa brodarima
            cboBrodarI3.DisplayMember = "PaNaziv";
            cboBrodarI3.ValueMember = "PaSifra";

            cboBrodarI4.DataSource = dvBrodari.ToTable(); // Kreira novu tabelu samo sa brodarima
            cboBrodarI4.DisplayMember = "PaNaziv";
            cboBrodarI4.ValueMember = "PaSifra";

            cboBPlombaVlasnikI1.DataSource = dvBrodari.ToTable();
            cboBPlombaVlasnikI1.DisplayMember = "PaNaziv";
            cboBPlombaVlasnikI1.ValueMember = "PaSifra";

            cboBPlombaVlasnikI2.DataSource = dvBrodari.ToTable();
            cboBPlombaVlasnikI2.DisplayMember = "PaNaziv";
            cboBPlombaVlasnikI2.ValueMember = "PaSifra";

            cboBPlombaVlasnikI3.DataSource = dvBrodari.ToTable();
            cboBPlombaVlasnikI3.DisplayMember = "PaNaziv";
            cboBPlombaVlasnikI3.ValueMember = "PaSifra";

            cboBPlombaVlasnikI4.DataSource = dvBrodari.ToTable();
            cboBPlombaVlasnikI4.DisplayMember = "PaNaziv";
            cboBPlombaVlasnikI4.ValueMember = "PaSifra";


            var dip = "Select ID,Naziv from MestaUtovara order by Naziv";
            SqlDataAdapter mestoUAD = new SqlDataAdapter(dip, conn);
            DataTable dtMestoU = new DataTable();
            mestoUAD.Fill(dtMestoU);

            var sqlTerminali = "Select ID, (Naziv + ' - ' + Oznaka) as Naziv From KontejnerskiTerminali order by Naziv, Oznaka";
            SqlDataAdapter daTerminali = new SqlDataAdapter(sqlTerminali, conn);
            DataTable dtTerminali = new DataTable();
            daTerminali.Fill(dtTerminali);

            cboMestoSpustanjaPunogI1.DataSource = dtTerminali.Copy();
            cboMestoSpustanjaPunogI1.DisplayMember = "Naziv";
            cboMestoSpustanjaPunogI1.ValueMember = "ID";

            cboMestoSpustanjaPunogI2.DataSource = dtTerminali.Copy();
            cboMestoSpustanjaPunogI2.DisplayMember = "Naziv";
            cboMestoSpustanjaPunogI2.ValueMember = "ID";   

            cboMestoUtovaraI2.DataSource = dtMestoU.Copy();
            cboMestoUtovaraI2.DisplayMember = "Naziv";
            cboMestoUtovaraI2.ValueMember = "ID";

            cboMestoUtovaraI2.DataSource = dtMestoU.Copy();
            cboMestoUtovaraI2.DisplayMember = "Naziv";
            cboMestoUtovaraI2.ValueMember = "ID";

            cboMestoPreuzimanjaI2.DataSource = dtMestoU.Copy();
            cboMestoPreuzimanjaI2.DisplayMember = "Naziv";
            cboMestoPreuzimanjaI2.ValueMember = "ID";

            cboMestoPreuzimanjaI1.DataSource = dtMestoU.Copy();
            cboMestoPreuzimanjaI1.DisplayMember = "Naziv";
            cboMestoPreuzimanjaI1.ValueMember = "ID";

            cboMestoUtovaraCeradeI3.DataSource = dtMestoU.Copy();
            cboMestoUtovaraCeradeI3.DisplayMember = "Naziv";
            cboMestoUtovaraCeradeI3.ValueMember = "ID";

            cboMestoIstovaraCeradeI3.DataSource = dtMestoU.Copy();
            cboMestoIstovaraCeradeI3.DisplayMember = "Naziv";
            cboMestoIstovaraCeradeI3.ValueMember = "ID";

            cboMestoUtovaraCeradeI4.DataSource = dtMestoU.Copy();
            cboMestoUtovaraCeradeI4.DisplayMember = "Naziv";
            cboMestoUtovaraCeradeI4.ValueMember = "ID";

            cboMestoIstovaraCeradeI4.DataSource = dtMestoU.Copy();
            cboMestoIstovaraCeradeI4.DisplayMember = "Naziv";
            cboMestoIstovaraCeradeI4.ValueMember = "ID";
            //var mu = "select ID, Naziv from MestaUtovara order by Naziv";
            //var muAD = new SqlDataAdapter(mu, conn);
            //var muDS = new DataSet();
            //muAD.Fill(muDS);
            //cboMestoIstovara.DataSource = muDS.Tables[0];
            //cboMestoIstovara.DisplayMember = "Naziv";
            //cboMestoIstovara.ValueMember = "ID";

            var poz = "Select ID,LTRIM(RTRIM(Napomena)) AS Napomena from DrumskiPozicioniranje order by Napomena";
            SqlDataAdapter pozAD = new SqlDataAdapter(poz, conn);
            DataTable dtPoz = new DataTable();
            pozAD.Fill(dtPoz);

            //System.Data.DataTable dt3 = pozDS.Tables[0];
            //DataRow prazanRed3 = dt3.NewRow();
            //prazanRed3["ID"] = DBNull.Value;
            //prazanRed3["Napomena"] = "/";
            //dt3.Rows.InsertAt(prazanRed3, 0);
            var query212 = "Select CAST(ISNULL(ID,0) AS INT) AS ID,Naziv from NapomenaZaPozicioniranje order by Naziv";
            SqlDataAdapter pozIzvozAD = new SqlDataAdapter(query212, conn);
            DataTable dtpozIzvoz = new DataTable();
            pozIzvozAD.Fill(dtpozIzvoz);

            if (tipNaloga == 1 || tipNaloga == 2)
            {
                cboNapomenaPozI1.DataSource = dtpozIzvoz.Copy();
                cboNapomenaPozI1.DisplayMember = "Naziv";
                cboNapomenaPozI1.ValueMember = "ID";

                cboNapomenaPozI2.DataSource = dtpozIzvoz.Copy();
                cboNapomenaPozI2.DisplayMember = "Naziv";
                cboNapomenaPozI2.ValueMember = "ID";
            }

            else
            {
                cboNapomenaPozI1.DataSource = dtPoz.Copy();
                cboNapomenaPozI1.DisplayMember = "Napomena";
                cboNapomenaPozI1.ValueMember = "ID";

                cboNapomenaPozI2.DataSource = dtPoz.Copy();
                cboNapomenaPozI2.DisplayMember = "Napomena";
                cboNapomenaPozI2.ValueMember = "ID";
            }

         

            var car = "Select ID, Naziv From Carinarnice order by Naziv";
            SqlDataAdapter carAD = new SqlDataAdapter(car, conn);
            DataTable dtCar = new DataTable();
            carAD.Fill(dtCar);
            cboOCarinarnicaI1.DataSource = dtCar.Copy();
            cboOCarinarnicaI1.DisplayMember = "Naziv";
            cboOCarinarnicaI1.ValueMember = "ID";

            cboOCarinarnicaI2.DataSource = dtCar.Copy();
            cboOCarinarnicaI2.DisplayMember = "Naziv";
            cboOCarinarnicaI2.ValueMember = "ID";

            cboOCarinarnicaI3.DataSource = dtCar.Copy();
            cboOCarinarnicaI3.DisplayMember = "Naziv";
            cboOCarinarnicaI3.ValueMember = "ID";

            cboOCarinarnicaI4.DataSource = dtCar.Copy();
            cboOCarinarnicaI4.DisplayMember = "Naziv";
            cboOCarinarnicaI4.ValueMember = "ID";

            //// --- AutoComplete podesavanja ---
            //cboOCarinarnica.DropDownStyle = ComboBoxStyle.DropDown;
            //cboOCarinarnica.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //cboOCarinarnica.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //AutoCompleteStringCollection autoSrc = new AutoCompleteStringCollection();
            //foreach (DataRow row in carDS.Tables[0].Rows)
            //{
            //    autoSrc.Add(row["Naziv"].ToString());
            //}
            //cboOCarinarnica.AutoCompleteCustomSource = autoSrc;

            //carinski postupak
            var dir2 = "Select ID, (Oznaka + ' ' + Naziv) as Naziv from VrstaCarinskogPostupka order by Naziv";
            SqlDataAdapter carpoAD = new SqlDataAdapter(dir2, conn);
            DataTable dtCPostupak = new DataTable();
            carpoAD.Fill(dtCPostupak);
            cboCarinskiPUnutrasniTransportI1.DataSource = dtCPostupak.Copy();
            cboCarinskiPUnutrasniTransportI1.DisplayMember = "Naziv";
            cboCarinskiPUnutrasniTransportI1.ValueMember = "ID";

            cboCarinskiPUnutrasniTransportI2.DataSource = dtCPostupak.Copy();
            cboCarinskiPUnutrasniTransportI2.DisplayMember = "Naziv";
            cboCarinskiPUnutrasniTransportI2.ValueMember = "ID";

            cboCarinskiPUnutrasniTransportI3.DataSource = dtCPostupak.Copy();
            cboCarinskiPUnutrasniTransportI3.DisplayMember = "Naziv";
            cboCarinskiPUnutrasniTransportI3.ValueMember = "ID";

            cboCarinskiPUnutrasniTransportI4.DataSource = dtCPostupak.Copy();
            cboCarinskiPUnutrasniTransportI4.DisplayMember = "Naziv";
            cboCarinskiPUnutrasniTransportI4.ValueMember = "ID";

            var carp = "Select ID, Naziv From Carinarnice order by Naziv";
            SqlDataAdapter carpAD = new SqlDataAdapter(carp, conn);
            DataTable dtCarP = new DataTable();
            carpAD.Fill(dtCarP);
            cboPolaznaCarinarnicaI1.DataSource = dtCarP.Copy();
            cboPolaznaCarinarnicaI1.DisplayMember = "Naziv";
            cboPolaznaCarinarnicaI1.ValueMember = "ID";

            cboPolaznaCarinarnicaI2.DataSource = dtCarP.Copy();
            cboPolaznaCarinarnicaI2.DisplayMember = "Naziv";
            cboPolaznaCarinarnicaI2.ValueMember = "ID";

            cboPolaznaCarinarnicaI3.DataSource = dtCarP.Copy();
            cboPolaznaCarinarnicaI3.DisplayMember = "Naziv";
            cboPolaznaCarinarnicaI3.ValueMember = "ID";

            cboPolaznaCarinarnicaI4.DataSource = dtCarP.Copy();
            cboPolaznaCarinarnicaI4.DisplayMember = "Naziv";
            cboPolaznaCarinarnicaI4.ValueMember = "ID";

            FillComboSpedicija();

            var tipkontejnera = "Select ID, SkNaziv From TipKontenjera order by SkNaziv";
            SqlDataAdapter tkAD = new SqlDataAdapter(tipkontejnera, conn);
            DataTable dtTipKont = new DataTable();
            tkAD.Fill(dtTipKont);

            cboVrstaKontejneraI1.DataSource = dtTipKont.Copy();
            cboVrstaKontejneraI1.DisplayMember = "SkNaziv";
            cboVrstaKontejneraI1.ValueMember = "ID";

            cboVrstaKontejneraI2.DataSource = dtTipKont.Copy();
            cboVrstaKontejneraI2.DisplayMember = "SkNaziv";
            cboVrstaKontejneraI2.ValueMember = "ID";

            cboVrstaKontejneraI3.DataSource = dtTipKont.Copy();
            cboVrstaKontejneraI3.DisplayMember = "SkNaziv";
            cboVrstaKontejneraI3.ValueMember = "ID";

            cboVrstaKontejneraI4.DataSource = dtTipKont.Copy();
            cboVrstaKontejneraI4.DisplayMember = "SkNaziv";
            cboVrstaKontejneraI4.ValueMember = "ID";


            var adr = "Select ID, (  UNKod +' - '+ Klasa + ' - ' + Naziv  ) as Naziv From VrstaRobeADR order by UNKod";
            var adrSAD = new SqlDataAdapter(adr, conn);
            DataTable dtADR = new DataTable();
            adrSAD.Fill(dtADR);
            cboADRI1.DataSource = dtADR.Copy();
            cboADRI1.DisplayMember = "Naziv";
            cboADRI1.ValueMember = "ID";
            cboADRI1.SelectedIndex = -1;

            cboADRI2.DataSource = dtADR.Copy();
            cboADRI2.DisplayMember = "Naziv";
            cboADRI2.ValueMember = "ID";
            cboADRI2.SelectedIndex = -1;

            cboADRI3.DataSource = dtADR.Copy();
            cboADRI3.DisplayMember = "Naziv";
            cboADRI3.ValueMember = "ID";
            cboADRI3.SelectedIndex = -1;

            cboADRI4.DataSource = dtADR.Copy();
            cboADRI4.DisplayMember = "Naziv";
            cboADRI4.ValueMember = "ID";
            cboADRI4.SelectedIndex = -1;

            var kvalitetKontejnera = "select ID, LTRIM(LTRIM(Naziv)) AS Naziv from uvKvalitetKontejnera order by ID";
            var kkAD = new SqlDataAdapter(kvalitetKontejnera, conn);
            DataTable dtkk = new DataTable();
            kkAD.Fill(dtkk);

            cboKvalitetKontejneraI1.DataSource = dtkk.Copy();
            cboKvalitetKontejneraI1.DisplayMember = "Naziv";
            cboKvalitetKontejneraI1.ValueMember = "ID";
            cboKvalitetKontejneraI1.Width = 150;
            cboKvalitetKontejneraI1.SelectedIndex = -1;

            cboKvalitetKontejneraI2.DataSource = dtkk.Copy();
            cboKvalitetKontejneraI2.DisplayMember = "Naziv";
            cboKvalitetKontejneraI2.ValueMember = "ID";
            cboKvalitetKontejneraI2.Width = 150;
            cboKvalitetKontejneraI2.SelectedIndex = -1;

            cboKvalitetKontejneraI3.DataSource = dtkk.Copy();
            cboKvalitetKontejneraI3.DisplayMember = "Naziv";
            cboKvalitetKontejneraI3.ValueMember = "ID";
            cboKvalitetKontejneraI3.Width = 150;
            cboKvalitetKontejneraI3.SelectedIndex = -1;

            cboKvalitetKontejneraI4.DataSource = dtkk.Copy();
            cboKvalitetKontejneraI4.DisplayMember = "Naziv";
            cboKvalitetKontejneraI4.ValueMember = "ID";
            cboKvalitetKontejneraI4.Width = 150;
            cboKvalitetKontejneraI4.SelectedIndex = -1;


            var nhm = "Select ID,(RTRIM(Naziv) + '-' + Rtrim(Broj)) as Naziv from NHM order by Naziv";

            var nhmSAD = new SqlDataAdapter(nhm, conn);
            DataTable dtNHM = new DataTable();
            nhmSAD.Fill(dtNHM);
            cboVrstaRobeI1.DataSource = dtNHM.Copy();
            cboVrstaRobeI1.DisplayMember = "Naziv";
            cboVrstaRobeI1.ValueMember = "ID";

            var np4 = "Select ID,(Oznaka + ' ' + Naziv) as Naziv from uvNacinPakovanja order by Naziv";
            var npAD4 = new SqlDataAdapter(np4, conn);
            DataTable dtNPakovanja = new DataTable();

            npAD4.Fill(dtNPakovanja);
            cboNacinPakovanjaI1.DataSource = dtNPakovanja.Copy();
            cboNacinPakovanjaI1.DisplayMember = "Naziv";
            cboNacinPakovanjaI1.ValueMember = "ID";

            cboNacinPakovanjaI2.DataSource = dtNPakovanja.Copy();
            cboNacinPakovanjaI2.DisplayMember = "Naziv";
            cboNacinPakovanjaI2.ValueMember = "ID";
        }

        private void PopuniAdresu(ComboBox cboIzvor, ComboBox cboCilj)
        {
            if (cboIzvor == null || cboIzvor.SelectedValue == null)
            {
                cboCilj.DataSource = null; // Brišemo listu jer mesto nije iz šifarnika
                return; // Ne idemo u bazu, ali polje ostaje slobodno za kucanje
            }
            // Provera da li je vrednost validan broj (da izbegnemo grešku pri konverziji)
            if (cboIzvor.SelectedValue == null || !int.TryParse(cboIzvor.SelectedValue.ToString(), out int sifra))
            {
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    // Upit sa parametrom @Sifra
                    string sql = "SELECT PaKoZapSt, (Rtrim(PaKOOpomba)) as Naziv FROM partnerjiKontOsebaMU WHERE PaKOSifra = @Sifra ORDER BY PaKOIme";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Sifra", sifra);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        cboCilj.DataSource = dt;
                        cboCilj.DisplayMember = "Naziv";
                        cboCilj.ValueMember = "PaKoZapSt";
                        cboCilj.SelectedIndex = 0;
                    }
                    else
                    {
                        // Ako nema rezultata null
                        cboCilj.DataSource = null;
                        cboCilj.Items.Clear();
                        cboCilj.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }

        private void PopuniKontaktOsobu(ComboBox cboMesto, ComboBox cboKontakt)
        {
         
            // Provera selekcije
            if (cboMesto.SelectedValue == null || cboMesto.SelectedValue == DBNull.Value)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {

                    string sql = @"SELECT PaKoZapSt, 
                           (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv 
                           FROM partnerjiKontOsebaMU 
                           WHERE PaKOSifra = @Sifra 
                           ORDER BY PaKOIme";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Sifra", Convert.ToInt32(cboMesto.SelectedValue));

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            cboKontakt.DataSource = dt;
                            cboKontakt.DisplayMember = "Naziv";
                            cboKontakt.ValueMember = "PaKoZapSt";
                            cboKontakt.SelectedIndex = 0;
                        }
                        else
                        {
                            // Ako nema rezultata null
                            cboKontakt.DataSource = null;
                            cboKontakt.Items.Clear();
                            cboKontakt.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška kod kontakta: " + ex.Message);
            }
        }

        private void PopuniText(string ime, string sufiks, object vrednost)
        {
            var ctrl = this.Controls.Find(ime + sufiks, true).FirstOrDefault() as TextBox;
            if (ctrl != null) ctrl.Text = vrednost?.ToString() ?? "";
        }

        private void PopuniCombo(string ime, string sufiks, object vrednost)
        {
            var cbo = this.Controls.Find(ime + sufiks, true).FirstOrDefault() as ComboBox;
            if (cbo != null)
            {
                if (vrednost != DBNull.Value && vrednost != null)
                {
                    // Ako je string (kao Valuta), Trim() uklanja višak razmaka
                    // Ako je int (kao PartnerID), ToString() ga pretvara u tekst koji SelectedValue prepoznaje
                    cbo.SelectedValue = vrednost.ToString().Trim();
                }
                else
                {
                    cbo.SelectedIndex = -1;
                }
            }
        }

        private void PopuniDatum(string ime, string sufiks, object vrednost)
        {
            var dtp = this.Controls.Find(ime + sufiks, true).FirstOrDefault() as DateTimePicker;
            if (dtp != null)
            {
                if (vrednost != DBNull.Value && vrednost != null)
                {
                    dtp.Value = Convert.ToDateTime(vrednost);
                     dtp.Tag = null;
                }
                else
                {
                   // dtp.Value = dtp.MinDate;// Ili neki default datum koji odgovara
                    dtp.Value =  DateTime.Today.AddDays(-5);
                    dtp.Tag = null;
                }
            }
        }
        private void PopuniCheck(string ime, string sufiks, object vrednost)
        {
            var ctrl = this.Controls.Find(ime + sufiks, true).FirstOrDefault() as CheckBox;
            if (ctrl != null)
                ctrl.Checked = (vrednost != DBNull.Value && Convert.ToInt32(vrednost) == 1);
        }

        private void PopuniDecimal(string ime, string sufiks, object vrednost)
        {
            // Ako koristite NumericUpDown kontrolu:
            var nud = this.Controls.Find(ime + sufiks, true).FirstOrDefault() as NumericUpDown;
            if (nud != null)
            {
                nud.Value = (vrednost != DBNull.Value && vrednost != null)
                            ? Convert.ToDecimal(vrednost)
                            : 0;
            }
            // Ako koristite običan TextBox za decimalni broj:
            else
            {
                var txt = this.Controls.Find(ime + sufiks, true).FirstOrDefault() as TextBox;
                if (txt != null)
                    txt.Text = (vrednost != DBNull.Value && vrednost != null)
                               ? Convert.ToDecimal(vrednost).ToString("N2")
                               : "0.00";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string referenca = null;
            int? mestoIstovara = null;
            int? mestoUtovara = null;
            int? klijent = null;
            decimal? bttoKontejnera = null;
            decimal? bttoRobe = null;
            string brojVoza = null;
            string brojKontejnera = null;
            string brodskaPlomba = null;
            string brodskaTeretnica = null;
            DateTime? datumIstovara = null;
            DateTime? datumUtovara = null;
            int? bookingBrodara = null;
            string adresaIstovara = null;
            string adresaUtovara = null;
            string kontaktNaUtovaru = null;
            int? napomenaPoz = null;
            int? polaznaSpedicija = null;
            int? odredisnaSpedicija = null;
            string odredisnaSpedicijaKontakt = null;
            string polaznaSpedicijaKontakt = null;
            int kreirajNalogID = 0;
            int? nalogID = null;
            string brojPosiljke = null;
            int? kvalitetKontejnera = null;
            int? brodar = null;
            int? brodskaPlombaVlasnik = null;
            int vaganje = 0;
            string ostalePlombe = null;
            decimal? taraKontejnera = null;
            decimal? nTTORobe = null;
            int? nacinPakovanja = null;
            int adr = 0;
            int mestoSpustanja = 0;
            string opisPosla = null;
            int? mestoUtovaraCerade = null;
            int? mestoIstovaraCerade = null;
            string NoviSpediterP = null;
            string NoviSpediterO = null;

            int iD = 0;
            iD = GetInt("txtID", sfx);

            if (Uvoz != 1 && Uvoz != 0 && txtBokingBrodara != null && int.TryParse(txtBokingBrodara.Text, out int parsedBookingBrodara))
                bookingBrodara = parsedBookingBrodara;


            int? tipTransportaID = null;
            var cboTip = this.Controls.Find("cboTipTransporta" + sfx, true).FirstOrDefault() as ComboBox;
            if (cboTip != null)
            {
                if (cboTip.SelectedValue == null || !int.TryParse(cboTip.SelectedValue.ToString(), out int parsedID))
                {
                    MessageBox.Show(
                        "Tip transporta je obavezno polje.",
                        "Upozorenje",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    cboTip.Focus(); // Fokusira tačnu kontrolu u aktivnom panelu
                    return;
                }
                tipTransportaID = parsedID;
            }

            if (Uvoz != 1 && Uvoz != 0 && txtReferenca != null)
                referenca = txtReferenca.Text.Trim();


            var cboKl = this.Controls.Find("cboKlijent" + sfx, true).FirstOrDefault() as ComboBox;
            if (cboKl.SelectedValue != null &&
                int.TryParse(cboKl.SelectedValue.ToString(), out int parsedKlijentID))
            {
                klijent = parsedKlijentID;
            }
            else
            {
                // Nije izabrao ništa iz liste → NE sme se snimi
                MessageBox.Show("Morate izabrati nalogodavca iz liste.");
                return; // prekida snimanje
            }

            decimal? trosak = null;
            decimal? cena = null;
            string valutaID = null;
           

            trosak = GetDec("txtTrosak", sfx);// nije definisan na vrhu metode, ne treba nigde osim ovde
            cena = GetDec("txtCena", sfx);// nije definisan na vrhu metode, ne treba nigde osim ovde
            valutaID = GetCboString("txtValuta", sfx);// nije definisan na vrhu metode, ne treba nigde osim ovde

            int autoDan = GetChk("chkAutoDan", sfx); // nije definisan na vrhu metode, ne treba nigde osim ovde
            int PDV = GetChk("chkPDV", sfx); // nije definisan na vrhu metode, ne treba nigde osim ovde
            int dodatniTrosak = GetChk("chkDodatniTrosak", sfx); // nije definisan na vrhu metode, ne treba nigde osim ovde
           
            if(chkVaganje.Enabled)
                vaganje =  (chkVaganje != null && chkVaganje.Checked) ? 1 : 0;

            int? carinskiPUnutrasni = null;
            int? polaznaCarinarnica = null;        
            int? odredisnaCarinarnica = null;
           
            int? vrstaKontejnera = null;

            
            if (Uvoz != 1 && Uvoz != 0 &&  cboCarinskiPUnutrasniTransport != null && cboCarinskiPUnutrasniTransport.SelectedValue != null && cboCarinskiPUnutrasniTransport.Enabled)
            {
                if (int.TryParse(cboCarinskiPUnutrasniTransport.SelectedValue.ToString(), out int cpostupak))
                {
                    carinskiPUnutrasni = cpostupak;
                }
            }
            if (Uvoz != 1 && Uvoz != 0 && cboPolaznaCarinarnica != null && cboPolaznaCarinarnica.SelectedValue != null &&  cboPolaznaCarinarnica.Enabled)
            {
                if (int.TryParse(cboPolaznaCarinarnica.SelectedValue.ToString(), out int polazna))
                {
                    polaznaCarinarnica = polazna;
                }
            }
            if (Uvoz != 1 && Uvoz != 0 && cboPolaznaSpedicija != null && cboPolaznaSpedicija.SelectedValue != null && cboPolaznaSpedicija.Enabled)
            {
                if (int.TryParse(cboPolaznaSpedicija.SelectedValue.ToString(), out int polaznaS))
                {
                    polaznaSpedicija = polaznaS;
                }
            }
            if (Uvoz != 1 && Uvoz != 0 && cboOCarinarnica != null && cboOCarinarnica.SelectedValue != null && cboOCarinarnica.Enabled)
            {

                if (int.TryParse(cboOCarinarnica.SelectedValue.ToString(), out int odredisna))
                {
                    odredisnaCarinarnica = odredisna;
                }
            }
            if (Uvoz != 1 && Uvoz != 0 && cboOCarinarnica != null && cboOCarinarnica.SelectedValue != null && cboOCarinarnica.Enabled)
            {
                if (int.TryParse(cbOspedicija.SelectedValue.ToString(), out int odredisnaS))
                {
                    odredisnaSpedicija = odredisnaS;
                }
            }
            if (Uvoz != 1 && Uvoz != 0)
            {
                polaznaSpedicijaKontakt = string.IsNullOrWhiteSpace(txtKontaktPolazneSpedicije.Text) ? null : txtKontaktPolazneSpedicije.Text.Trim();
                odredisnaSpedicijaKontakt = string.IsNullOrWhiteSpace(txtKontaktOSpedicije.Text) ? null : txtKontaktOSpedicije.Text.Trim();
            }
            if(txtNoviSpediterP!= null)
                NoviSpediterP = string.IsNullOrWhiteSpace(txtNoviSpediterP.Text) ? null : txtNoviSpediterP.Text.Trim();
            if(txtNoviSpediterO != null)
                NoviSpediterO = string.IsNullOrWhiteSpace(txtNoviSpediterO.Text) ? null : txtNoviSpediterO.Text.Trim();

            string dodatniOpis = null;
            if (Uvoz != 1 && Uvoz != 0 && txtDodatniOpis != null)
                dodatniOpis = txtDodatniOpis.Text.Trim();
           

            if (Uvoz != 1 && Uvoz != 0 && cboNapomenaPoz != null && !string.IsNullOrWhiteSpace(cboNapomenaPoz.Text))
            {
                int parsedNapomenaPoz = -1;

                if (cboNapomenaPoz.SelectedValue != null &&
                    int.TryParse(cboNapomenaPoz.SelectedValue.ToString(), out int val))
                {
                    parsedNapomenaPoz = val;
                    napomenaPoz = parsedNapomenaPoz;
                }
                else
                {
                    int pozicija = InsertPozicijaUsifarnik(cboNapomenaPoz.Text.Trim());
                    napomenaPoz = pozicija > 0 ? pozicija : (int?)null;
                }
            }

            if (Uvoz != 1 && Uvoz != 0)
                brojKontejnera = txtBrojKontejnera.Text;

            if ( Uvoz == 3 && !string.IsNullOrWhiteSpace(txtOpisPosla.Text))
                opisPosla = txtOpisPosla.Text.Trim();

            if (Uvoz != 1 && Uvoz != 0 && cboVrstaKontejnera != null &&  cboVrstaKontejnera.SelectedValue != null && cboVrstaKontejnera.Enabled)
            {

                if (int.TryParse(cboVrstaKontejnera.SelectedValue.ToString(), out int vrstaKontejneraConverted))
                {
                    vrstaKontejnera = vrstaKontejneraConverted;
                }
            }
            
            if (Uvoz != 1 && Uvoz != 0 && cboKvalitetKontejnera!= null && cboKvalitetKontejnera.SelectedValue != null && cboKvalitetKontejnera.Enabled)
            {

                if (int.TryParse(cboKvalitetKontejnera.SelectedValue.ToString(), out int kvalitetKontejneraConverted))
                {
                    kvalitetKontejnera = kvalitetKontejneraConverted;
                }
            }
            if (Uvoz != 1 && Uvoz != 0 && cboBrodar != null && cboBrodar.SelectedValue != null && cboBrodar.Enabled)
            {

                if (int.TryParse(cboBrodar.SelectedValue.ToString(), out int brodarConverted))
                {
                    brodar = brodarConverted;
                }
            }
            
            if (Uvoz != 1 && Uvoz != 0 && cboBPlombaVlasnik != null && cboBPlombaVlasnik.SelectedValue != null && cboBPlombaVlasnik.Enabled)
            {

                if (int.TryParse(cboBPlombaVlasnik.SelectedValue.ToString(), out int bPlombaVlasnikConverted))
                {
                    brodskaPlombaVlasnik = bPlombaVlasnikConverted;
                }
            }
                 
            if (Uvoz != 1 && Uvoz != 0 &&  txtBrodskaPlomba != null)
                brodskaPlomba = txtBrodskaPlomba.Text.Trim();

            if (Uvoz != 1 && Uvoz != 0 && txtOstalePlombe != null && txtOstalePlombe.Enabled)
                ostalePlombe = txtOstalePlombe.Text.Trim();

            int? mestoPreuzimanja = null;

            //if (Uvoz != 1 && Uvoz != 0)
            //    if (decimal.TryParse(txtBrutoK.Text, out decimal brutoConverted))
            //        bttoKontejnera = brutoConverted;

            if (Uvoz != 1 && Uvoz != 0 && txtNTTOR != null)
                if (decimal.TryParse(txtNTTOR.Text, out decimal netoConverted))
                    nTTORobe = netoConverted;
            
            if (Uvoz != 1 && Uvoz != 0 && txtTaraK != null)
                if (decimal.TryParse(txtTaraK.Text, out decimal taraConverted))
                    taraKontejnera = taraConverted;

            if (Uvoz != 1 && Uvoz != 0 && txtBrutoK != null)
                if (decimal.TryParse(txtBrutoK.Text, out decimal brutoConverted))
                    bttoRobe = brutoConverted;

            if (Uvoz != 1 && Uvoz != 0 && cboMestoPreuzimanja != null && cboMestoPreuzimanja.Enabled)
            {
                // Scenarijo 1: Nešto je odabrano iz liste
                if (cboMestoPreuzimanja.SelectedValue != null && int.TryParse(cboMestoPreuzimanja.SelectedValue.ToString(), out int parsedID))
                {
                    mestoPreuzimanja = parsedID;
                }
                // Scenarijo 2: Korisnik je ukucao novo mesto koje nije u listi
                else if (!string.IsNullOrWhiteSpace(cboMestoPreuzimanja.Text))
                {
                    // Pozivamo metodu i šaljemo joj TEKST koji je ukucan u aktivni ComboBox
                    int mestoP = InsertMestoUtovaraUSifarnik(cboMestoPreuzimanja.Text.Trim());
                    mestoPreuzimanja = mestoP > -1 ? mestoP : (int?)null;
                }
            }


            if (Uvoz != 1 && Uvoz != 0 && cboNacinPakovanja != null && cboNacinPakovanja.SelectedValue != null && cboNacinPakovanja.Enabled)
            {

                if (int.TryParse(cboNacinPakovanja.SelectedValue.ToString(), out int nacinPakovanjaConverted))
                {
                    nacinPakovanja = nacinPakovanjaConverted;
                }
            }

            string kontaktOsobaistovara = GetTxt("txtkontaktNaIstovaru", sfx);

            //if (dtpUtovara.Checked)
            //    datumUtovara = dtpUtovara.Value;

            //if (dtIstovara.Checked)
            //    datumIstovara = dtIstovara.Value;

            
            if (Uvoz != 1 && Uvoz != 0 && cboMestoUtovaraCerade != null && cboMestoUtovaraCerade.SelectedValue != null && cboMestoUtovaraCerade.Enabled)
            {
                if (int.TryParse(cboMestoUtovaraCerade.SelectedValue.ToString(), out int mestoUtovaraCeradeConvert))
                {
                    mestoUtovaraCerade = mestoUtovaraCeradeConvert;
                }
            }

            if (Uvoz != 1 && Uvoz != 0 && cboMestoIstovaraCerade != null && cboMestoUtovaraCerade.SelectedValue != null && cboMestoIstovaraCerade.Enabled)
            {
                if (int.TryParse(cboMestoIstovaraCerade.SelectedValue.ToString(), out int mestoIstovaraCeradeConvert))
                {
                    mestoIstovaraCerade = mestoIstovaraCeradeConvert;
                }
            }

            if (Uvoz != 1 && Uvoz != 0 && cboMestoSpustanjaPunog != null && cboMestoSpustanjaPunog.SelectedValue != null && cboMestoSpustanjaPunog.Enabled)
            {
                if (int.TryParse(cboMestoSpustanjaPunog.SelectedValue.ToString(), out int mestoSpustanjaConvert))
                {
                    mestoSpustanja = mestoSpustanjaConvert;
                }
            }


            datumIstovara = GetDate("dtIstovara", sfx);
            DateTime? dtPreuzimanjaPraznog = null;
            DateTime? dtRealizacijePreuzimanjaPraznogKont = null;
            DateTime? dtSpustanja = null;
            DateTime? dtRealizacijeSpustanja = null;
            DateTime? dtPreuzimanjePraznogKNovi = null;
            DateTime? dtUtovaraKNovi = null;
            DateTime? dtSpustanjePunogNovi = null;
            DateTime? dtRealiUtovara = null;
            
            DateTime? dtUtovaraCerade = null;
            DateTime? dtIstovaraCeradeNovi = null;
            DateTime? dtUtovaraCeradeNovi = null;
            DateTime? dtRealizacijeUtovaraCerade = null;
            DateTime? dtIstovaraCerade = null;
            DateTime? dtRealizacijeIstovaraCerade = null;
            string adresaUtovaraCerade = null;
            string adresaIstovaraCerade = null;
            string kontaktUtovaraCerade = null;
            string kontaktIstovaraCerade = null;


            if (dtPreuzimanjaPraznogKontejnera != null )
                dtPreuzimanjaPraznog = (dtPreuzimanjaPraznogKontejnera.Tag?.ToString() == "IZMENJEN") ? dtPreuzimanjaPraznogKontejnera.Value : (DateTime?)null;

            if (dtPreuzimanjaPraznogKontejneraNovi != null )
                dtPreuzimanjePraznogKNovi = (dtPreuzimanjaPraznogKontejneraNovi.Tag?.ToString() == "IZMENJEN") ? dtPreuzimanjaPraznogKontejneraNovi.Value : (DateTime?)null;

            if (dtRealiPreuzimanjaPraznogKont != null)
                dtRealizacijePreuzimanjaPraznogKont = (dtRealiPreuzimanjaPraznogKont.Tag?.ToString() == "IZMENJEN") ? dtRealiPreuzimanjaPraznogKont.Value : (DateTime?)null;

            if (dtpSpustanjePunog != null )
                dtSpustanja = (dtpSpustanjePunog.Tag?.ToString() == "IZMENJEN") ? dtpSpustanjePunog.Value : (DateTime?)null;

            if (dtpSpustanjePunogNovi != null )
                dtSpustanjePunogNovi = (dtpSpustanjePunogNovi.Tag?.ToString() == "IZMENJEN") ? dtpSpustanjePunogNovi.Value : (DateTime?)null;

            if (dtpSpustanjePunogReal != null )
                dtRealizacijeSpustanja = (dtpSpustanjePunogReal.Tag?.ToString() == "IZMENJEN") ? dtpSpustanjePunogReal.Value : (DateTime?)null;

            if (dtpUtovaraNovi != null && dtpUtovaraNovi.Checked)
                dtUtovaraKNovi = (dtpUtovaraNovi.Tag?.ToString() == "IZMENJEN") ? dtpUtovaraNovi.Value : (DateTime?)null;

            if (dtpUtovara != null && dtpUtovara.Checked)
                datumUtovara = (dtpUtovara.Tag?.ToString() == "IZMENJEN") ? dtpUtovara.Value : (DateTime?)null;

            if (dtpRealiUtovara != null )
                dtRealiUtovara = (dtpRealiUtovara.Tag?.ToString() == "IZMENJEN") ? dtpRealiUtovara.Value : (DateTime?)null;

            string granicniPrelaz = "";
            if (txtGranicniPrelaz != null && txtGranicniPrelaz.Enabled)
                granicniPrelaz = txtGranicniPrelaz.Text.Trim();

            if (dtpUtovaraCerade != null )
                dtUtovaraCerade = (dtpUtovaraCerade.Tag?.ToString() == "IZMENJEN") ? dtpUtovaraCerade.Value : (DateTime?)null;

            if (dtpUtovaraCeradeNovi  != null )
                dtUtovaraCeradeNovi = (dtpUtovaraCeradeNovi.Tag?.ToString() == "IZMENJEN") ? dtpUtovaraCeradeNovi.Value : (DateTime?)null;

            if (dtpIstovaraCeradeNovi != null )
                dtIstovaraCeradeNovi = (dtpIstovaraCeradeNovi.Tag?.ToString() == "IZMENJEN") ? dtpIstovaraCeradeNovi.Value : (DateTime?)null;

            if (dtpRealiUtovaraCerade != null )
                dtRealizacijeUtovaraCerade = (dtpRealiUtovaraCerade.Tag?.ToString() == "IZMENJEN") ? dtpRealiUtovaraCerade.Value : (DateTime?)null;

            if (dtpIstovaraCerade != null )
                dtIstovaraCerade = (dtpIstovaraCerade.Tag?.ToString() == "IZMENJEN") ? dtpIstovaraCerade.Value : (DateTime?)null;

            if (dtpRealiIstovaraCerade != null)
                dtRealizacijeIstovaraCerade = (dtpRealiIstovaraCerade.Tag?.ToString() == "IZMENJEN") ? dtpRealiIstovaraCerade.Value : (DateTime?)null;

            if (Uvoz == 3)
            {
                if(cboAdresaUtovaraCerade != null && !string.IsNullOrWhiteSpace(cboAdresaUtovaraCerade.Text))
                    adresaUtovaraCerade = cboAdresaUtovaraCerade.Text.Trim();
                if (cboAdresaIstovaraCerade != null && !string.IsNullOrWhiteSpace(cboAdresaIstovaraCerade.Text))
                    adresaIstovaraCerade = cboAdresaIstovaraCerade.Text.Trim();
                if (cboKontaktUtovaraCerade != null && !string.IsNullOrWhiteSpace(cboKontaktUtovaraCerade.Text))
                    kontaktUtovaraCerade = cboKontaktUtovaraCerade.Text.Trim();
                if (cboKontaktIstovaraCerade != null && !string.IsNullOrWhiteSpace(cboKontaktIstovaraCerade.Text))
                    kontaktIstovaraCerade = cboKontaktIstovaraCerade.Text.Trim();

               
            }
            

            int? statusID = null;
            statusID = GetCbo("cboStatus", sfx);

            int? tipNaloga = null;
            if (Uvoz != 1 && Uvoz != 0)
            {
                tipNaloga = GetCbo("cboTipNaloga", sfx);
            }
            else
            {
                tipNaloga = Uvoz;
            }

            if(txtAdresaUtovara != null)
                adresaUtovara = GetTxt("txtAdresaUtovara", sfx);
            if (txtkontaktNaUtovaru != null)
                kontaktNaUtovaru = GetTxt("txtkontaktNaUtovaru", sfx);


            var cbo1 = this.Controls.Find("cboMestoUtovara" + sfx, true).FirstOrDefault() as ComboBox;

            if (cbo1 != null)
            {
                // Scenarijo 1: Nešto je odabrano iz liste
                if (cbo1.SelectedValue != null && Uvoz != 0 && int.TryParse(cbo1.SelectedValue.ToString(), out int parsedMestoUtovaraID))
                {
                    mestoUtovara = parsedMestoUtovaraID;
                }
                // Scenarijo 2: Korisnik je ukucao novo mesto koje nije u listi
                else if (!string.IsNullOrWhiteSpace(cboMestoPreuzimanja.Text))
                {
                    // Pozivamo metodu i šaljemo joj TEKST koji je ukucan u aktivni ComboBox
                    int mestoU = InsertMestoUtovaraUSifarnik(cboMestoPreuzimanja.Text.Trim());
                    mestoUtovara = mestoU > -1 ? mestoU : (int?)null;
                }
            }
            //if (tipNaloga == 2)
            //{
            //    brojPosiljke = string.IsNullOrWhiteSpace(txtBrojPosiljke.Text) ? null : txtBrojPosiljke.Text.Trim();
            //}

            if (izborAdr == 1)
                adr = 1;



            //if (Uvoz != 1 && Uvoz != 0)
            //    brojVoza = string.IsNullOrWhiteSpace(txtBrojVoza.Text) ? null : txtBrojVoza.Text.Trim();

            if (Uvoz != 0)
                brodskaTeretnica = GetCboString("txtBL", sfx); 

           

            //var cboNapomenaPoz = this.Controls.Find("cboNapomenaPoz" + sfx, true).FirstOrDefault() as ComboBox;
  

           
            int zaposleniID = PostaviVrednostZaposleni();

            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
            InsertRadniNalogInterni updi = new InsertRadniNalogInterni();

            string adresaPreuzimanja = null;
            string kontaktPreuzimanja = null;


            if (cboAdresaPreuzimanja != null && !string.IsNullOrWhiteSpace(cboAdresaPreuzimanja.Text))
                adresaPreuzimanja = cboAdresaPreuzimanja.Text.Trim();
            if (cbokontaktPreuzimanja != null && !string.IsNullOrWhiteSpace(cbokontaktPreuzimanja.Text))
                kontaktPreuzimanja = cbokontaktPreuzimanja.Text.Trim();


            //if (txtAdresaPreuzimanja != null)
            //    adresaPreuzimanja = txtAdresaPreuzimanja.Text.Trim();
            //if (txtkontaktPreuzimanja != null)
            //    kontaktPreuzimanja = txtkontaktPreuzimanja.Text.Trim();
            if (status == true)
            {
                var cboTipNaloga = this.Controls.Find("cboTipNaloga" + sfx, true).FirstOrDefault() as ComboBox;
                if (cboTipNaloga.SelectedValue == null || (int)cboTipNaloga.SelectedValue == -1)
                {
                    MessageBox.Show("Polje 'Tip naloga' je obavezno!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (noviZapisSaNovimNalogID == "NOVINALOG")
                {
                    kreirajNalogID = 1;
                }
                else if (mainNalogID != null && mainNalogID > 0)
                {
                    nalogID = mainNalogID;
                }
               



                int noviID = ins.InsRadniNalogDrumskiNovi(kreirajNalogID, scenario, nalogID, tipNaloga, zaposleniID, bookingBrodara,  tipTransportaID, klijent, referenca, valutaID,
               trosak, cena, PDV, dodatniTrosak, carinskiPUnutrasni, autoDan, polaznaCarinarnica, polaznaSpedicija, polaznaSpedicijaKontakt,
               odredisnaCarinarnica, odredisnaSpedicija, odredisnaSpedicijaKontakt, dodatniOpis, napomenaPoz, vaganje, brojKontejnera, vrstaKontejnera,
               kvalitetKontejnera, brodskaPlomba, brodar, brodskaPlombaVlasnik, ostalePlombe, taraKontejnera, bttoRobe, nTTORobe, adr, nacinPakovanja,
               mestoPreuzimanja, adresaPreuzimanja, kontaktPreuzimanja, dtPreuzimanjaPraznog, dtRealizacijePreuzimanjaPraznogKont, dtPreuzimanjePraznogKNovi,
               mestoSpustanja, mestoUtovaraCerade, mestoIstovaraCerade, dtSpustanja, dtRealizacijeSpustanja, dtSpustanjePunogNovi, mestoUtovara, datumUtovara, dtUtovaraKNovi, dtRealiUtovara, adresaUtovara, kontaktNaUtovaru,
                dtUtovaraCerade, dtUtovaraCeradeNovi, dtRealizacijeUtovaraCerade, dtIstovaraCerade,dtIstovaraCeradeNovi, dtRealizacijeIstovaraCerade, granicniPrelaz, kontaktOsobaistovara,
               brojVoza, brodskaTeretnica, brojPosiljke, daLiJeCarinskiPostupak, opisPosla, adresaUtovaraCerade, adresaIstovaraCerade, kontaktUtovaraCerade, kontaktIstovaraCerade, NoviSpediterP, NoviSpediterO);
                //int noviID = ins.InsRadniNalogDrumski(tipNaloga, kreirajNalogID, nalogID, autoDan, referenca, mestoPreuzimanja, klijent, mestoUtovara, adresaUtovara, mestoIstovara, datumUtovara, datumIstovara, adresaIstovara,
                //    dtPreuzimanjaPraznogKont, granicniPrelaz, trosak, valutaID,  statusID, dodatniOpis, cena, kontaktOsobaistovara, PDV, tipTransportaID, brojVoza, bttoKontejnera, bttoRobe, brojKontejnera, 
                //    bookingBrodara, brodskaTeretnica, brodskaPlomba, napomenaPoz, polaznaCarinarnica, odredisnaCarinarnica, polaznaSpedicija, odredisnaSpedicija, polaznaSpedicijaKontakt, odredisnaSpedicijaKontakt, zaposleniID,
                //    vrstaKontejnera, dodatniTrosak, brojPosiljke);
                id = noviID;
                foreach (var stavka in privremenaListaNHM)
                {
                    ins.InsRadniNalogDrumskiNHM(noviID, stavka.IDNHM);
                }

                var txtID_Aktivni = this.Controls.Find("txtID" + sfx, true).FirstOrDefault() as TextBox;

                if (txtID_Aktivni != null)
                {
                    txtID_Aktivni.Text = noviID.ToString();
                }
                lbtHederTekst.Text = "Unos novog zapisa završen!";
                status = false;
                button1.Visible = true;
                button4.Visible = true;
                if (!drumskiNew)
                    button21.Visible = true;
                btnFormiranjeNaloga.Visible = !(nalogID > 0 || (mainNalogID.HasValue && mainNalogID > 0));
            }
            else
            {
                // 1. Proveri stari status i RadniNalogInterniID
                int? stariStatusID = null;
                int? radniNalogInterniID = null;
                using (var connection = new SqlConnection(Sifarnici.frmLogovanje.connectionString))
                {
                    connection.Open();
                    var cmd = new SqlCommand(@"
                                    SELECT ISNULL(rn.Status, 0) AS Status, ri.ID AS RadniNalogInterniID
                                    FROM RadniNalogDrumski rn
                                    LEFT JOIN RadniNalogInterni ri ON ri.KonkretaIDUsluge = rn.UKID
                                    WHERE rn.ID = @ID", connection);
                    cmd.Parameters.AddWithValue("@ID", iD);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader["Status"] != DBNull.Value)
                                stariStatusID = Convert.ToInt32(reader["Status"]);
                            if (reader["RadniNalogInterniID"] != DBNull.Value)
                                radniNalogInterniID = Convert.ToInt32(reader["RadniNalogInterniID"]);
                        }
                    }
                }

                // 2. Učitaj listu završnih statusa
                var statusiZavrsni = new List<int>();
                using (var connection = new SqlConnection(Sifarnici.frmLogovanje.connectionString))
                {
                    connection.Open();
                    var cmd = new SqlCommand("SELECT Vrednost FROM SistemskePostavke WHERE Naziv LIKE 'StatusKamiona%'", connection);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (int.TryParse(reader.GetString(0).Trim(), out int parsed))
                                statusiZavrsni.Add(parsed);
                        }
                    }
                }
                if (Uvoz != 1 && Uvoz != 0)
                {
                       ins.UpdRadniNalogDrumskiNovi( id, bookingBrodara,  klijent, referenca, valutaID,
                       trosak, cena, PDV, dodatniTrosak, carinskiPUnutrasni, autoDan, polaznaCarinarnica, polaznaSpedicija, polaznaSpedicijaKontakt,
                       odredisnaCarinarnica, odredisnaSpedicija, odredisnaSpedicijaKontakt, dodatniOpis, napomenaPoz, vaganje, brojKontejnera, vrstaKontejnera,
                       kvalitetKontejnera, brodskaPlomba, brodar, brodskaPlombaVlasnik, ostalePlombe, taraKontejnera, bttoRobe, nTTORobe, adr, nacinPakovanja,
                       mestoPreuzimanja, adresaPreuzimanja, kontaktPreuzimanja, dtPreuzimanjaPraznog, dtRealizacijePreuzimanjaPraznogKont, dtPreuzimanjePraznogKNovi,
                       mestoSpustanja, dtSpustanja, dtRealizacijeSpustanja, dtSpustanjePunogNovi, mestoUtovara, datumUtovara, dtUtovaraKNovi, dtRealiUtovara, adresaUtovara, kontaktNaUtovaru,
                       granicniPrelaz, kontaktOsobaistovara,
                       brojVoza, brodskaTeretnica, brojPosiljke, zaposleniID, opisPosla, dtUtovaraCerade, dtUtovaraCeradeNovi, dtIstovaraCerade, dtIstovaraCeradeNovi,
                       adresaUtovaraCerade, adresaIstovaraCerade, kontaktUtovaraCerade, kontaktIstovaraCerade, NoviSpediterP, NoviSpediterO);
                }
                else if(Uvoz == 0)
                {
                       ins.UpdRadniNalogDrumskiIzvoz(id, valutaID, trosak, cena, PDV, dodatniTrosak,  autoDan, odredisnaSpedicijaKontakt,
                        polaznaSpedicijaKontakt, dtRealizacijePreuzimanjaPraznogKont, dtPreuzimanjePraznogKNovi, dtRealizacijeSpustanja, dtSpustanjePunogNovi, dtSpustanja, dtUtovaraKNovi, dtRealiUtovara,
                        dtUtovaraCeradeNovi, dtRealizacijeUtovaraCerade, dtIstovaraCerade, dtIstovaraCeradeNovi, dtRealizacijeIstovaraCerade,granicniPrelaz, zaposleniID, NoviSpediterP, NoviSpediterO);
                }

            // 3. Update glavnog naloga
            //ins.UpdateRadniNalogDrumski(iD, tipNaloga, autoDan, referenca, mestoPreuzimanja, mestoUtovara, adresaUtovara, mestoIstovara, datumUtovara, datumIstovara, adresaIstovara,
            //    dtPreuzimanjaPraznog, granicniPrelaz, trosak, valutaID,  statusID, dodatniOpis, cena, kontaktOsobaistovara, PDV, tipTransportaID, bookingBrodara, klijent,
            //    bttoKontejnera, bttoRobe, brojVoza, brojKontejnera, brodskaTeretnica, brodskaPlomba, napomenaPoz, polaznaCarinarnica, odredisnaCarinarnica, polaznaSpedicija, odredisnaSpedicija,
            //    polaznaSpedicijaKontakt, odredisnaSpedicijaKontakt, zaposleniID, vrstaKontejnera, dodatniTrosak, brojPosiljke);

            // 4. Ako se status promenio i novi spada u završne onda ide update internog
            if (statusID.HasValue &&
                   (!stariStatusID.HasValue || statusID.Value != stariStatusID.Value))
                {
                    if (statusiZavrsni.Contains(statusID.Value) && radniNalogInterniID.HasValue)
                    {

                        updi.UpdRadniNalogInterniZavrsen(
                            radniNalogInterniID.Value,
                            Saobracaj.Sifarnici.frmLogovanje.user.Trim()
                        );
                    }
                }
            }
        }

        private int PostaviVrednostZaposleni()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int ulogovaniZaposleniID = 0;

            con.Open();

            SqlCommand cmd = new SqlCommand("Select  k.DeSifra as ID, (RTrim(DeIme) + ' ' + Rtrim(DePriimek)) as Zaposleni " +
                                            " FROM Korisnici k " +
                                            "INNER JOIN Delavci d ON k.DeSifra = d.DeSifra " +
                                            "where Trim(Korisnik) like '" + Saobracaj.Sifarnici.frmLogovanje.user.Trim() + "'", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["ID"] != DBNull.Value)
                    ulogovaniZaposleniID = Convert.ToInt32(dr["ID"].ToString());
            }
            return ulogovaniZaposleniID;

        }

      


        public int InsertPozicijaUsifarnik(string naziv)
        {
            if (string.IsNullOrWhiteSpace(naziv)) return -1;

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int pozicijaID = -1;

            con.Open();
            string napomena =naziv.Trim().ToUpper();
            SqlCommand cmd = new SqlCommand("Select ID,Napomena " +
                "FROM DrumskiPozicioniranje " +
                "WHERE UPPER(LTRIM(RTRIM(Napomena))) LIKE UPPER(@Napomena)", con);

            cmd.Parameters.AddWithValue("@Napomena", napomena);
            SqlDataReader dr = cmd.ExecuteReader();

            if (!dr.HasRows)
            {
                InsertNapomena ins = new InsertNapomena();
                pozicijaID = ins.InsNapomena(naziv.Trim());
            }
            return pozicijaID;
        }

        public int InsertMestoUtovaraUSifarnik( string naziv)
        {
            if (string.IsNullOrWhiteSpace(naziv)) return -1;

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int mestoIstovara = -1;

            con.Open();
            string mesto = naziv.Trim().ToUpper();
            SqlCommand cmd = new SqlCommand("SELECT ID, Naziv  " +
                "FROM   MestaUtovara " +
                "WHERE  UPPER(LTRIM(RTRIM(Naziv))) LIKE UPPER(@mesto)", con);

            cmd.Parameters.AddWithValue("@mesto", mesto);
            SqlDataReader dr = cmd.ExecuteReader();

            if (!dr.HasRows)
            {
                InsertMestaUtovara ins = new InsertMestaUtovara();
                mestoIstovara = ins.InsMestaUtovara(naziv.Trim().ToUpper(), null);
            }
            return mestoIstovara;
        }

        private DateTime? GetDate(string ime, string sfx)
        {
            var dtp = this.Controls.Find(ime + sfx, true).FirstOrDefault() as DateTimePicker;

            if (dtp != null && dtp.Checked)
            {
                return dtp.Value;
            }

            return null; // Ako nije štiklirano ili kontrola ne postoji
        }

        private string GetTxt(string ime, string sfx)
        {
            var ctrl = this.Controls.Find(ime + sfx, true).FirstOrDefault() as TextBox;
            return string.IsNullOrWhiteSpace(ctrl?.Text) ? null : ctrl.Text.Trim();
        }

        private decimal GetDec(string ime, string sfx)
        {
            var ctrl = this.Controls.Find(ime + sfx, true).FirstOrDefault();

            if (ctrl == null) return 0;

            // 1. Ako je kontrola NumericUpDown
            if (ctrl is NumericUpDown nud)
            {
                return nud.Value;
            }

            // 2. Ako je kontrola TextBox
            if (ctrl is TextBox txt)
            {
                return decimal.TryParse(txt.Text, out decimal val) ? val : 0;
            }

            return 0;
        }

        private string GetCboString(string ime, string sfx)
        {
            var cbo = this.Controls.Find(ime + sfx, true).FirstOrDefault() as ComboBox;

            // Proveravamo da li kontrola postoji i da li ima selektovanu vrednost
            if (cbo != null && cbo.SelectedValue != null)
            {
                return cbo.SelectedValue.ToString().Trim();
            }

            return null; // Vraća null ako ništa nije izabrano (vaša SQL procedura će to prepoznati)
        }

        private int GetCbo(string ime, string sfx)
        {
            var ctrl = this.Controls.Find(ime + sfx, true).FirstOrDefault() as ComboBox;
            if (ctrl != null && ctrl.SelectedValue != null && int.TryParse(ctrl.SelectedValue.ToString(), out int val))
                return val;
            return 0; // ili -1 zavisno od vaše logike
        }

        private int GetInt(string ime, string sfx)
        {
            var ctrl = this.Controls.Find(ime + sfx, true).FirstOrDefault();

            // Ako je TextBox
            if (ctrl is TextBox txt && !string.IsNullOrWhiteSpace(txt.Text) && int.TryParse(txt.Text, out int valTxt))
            {
                return valTxt;
            }

            // Ako je ComboBox (za ID-eve partnera, kamiona, valuta koji su int)
            if (ctrl is ComboBox cbo && cbo.SelectedValue != null && int.TryParse(cbo.SelectedValue.ToString(), out int valCbo))
            {
                return valCbo;
            }

            return 0; // Default vrednost ako ništa ne nađe ili je prazno
        }

        private int GetChk(string ime, string sfx)
        {
            var ctrl = this.Controls.Find(ime + sfx, true).FirstOrDefault() as CheckBox;
            if (ctrl != null)
            {
                return ctrl.Checked ? 1 : 0;
            }
            return 0; // Podrazumevano nije štiklirano ako kontrola ne postoji
        }

        private void btnFormiranjeNaloga_Click(object sender, EventArgs e)
        {
            InsertRadniNalogDrumski isu = new InsertRadniNalogDrumski();
            // ostaje logika sa listom da ne bih pravila novu metodu za kreiranje naloga id, u ovom slucaju je jedna stavka jedan nalogId
            List<int> stavke = new List<int>();
            var txtID_Aktivni = this.Controls.Find("txtID" + sfx, true).FirstOrDefault() as TextBox;

            if (txtID_Aktivni != null && int.TryParse(txtID_Aktivni.Text.Trim(), out int parsedID))
            {
                stavke.Add((parsedID));
                isu.PostaviNalogIDNaRedove(stavke);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int radniNalogDrumskiID = 0;
            var txtID_Aktivni = this.Controls.Find("txtID" + sfx, true).FirstOrDefault() as TextBox;

            if (txtID_Aktivni != null && !int.TryParse(txtID_Aktivni.Text.Trim(), out radniNalogDrumskiID))
            {
                MessageBox.Show("ID ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int zaposleniID = PostaviVrednostZaposleni();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Odaberite fajl za upload";
            ofd.Filter = "Svi fajlovi|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string izabraniFajl = ofd.FileName;
                string ekstenzija = Path.GetExtension(izabraniFajl);
                string cleanName = Path.GetFileNameWithoutExtension(izabraniFajl);

                // Očisti naziv fajla od nedozvoljenih karaktera
                string nazivFajla = string.Join("_", cleanName.Split(Path.GetInvalidFileNameChars())) + ekstenzija;

                // Putanja na server   192.168.99.10
                // Leget\DRUMSKI\DOKUMENTA
                string targetPath = $@"\\192.168.150.110\Leget\Drumski\Dokumenta\ID_{radniNalogDrumskiID}";
                string destinacija = Path.Combine(targetPath, nazivFajla);

                try
                {
                    // Ako ne postoji folder, napravi ga
                    if (!Directory.Exists(targetPath))
                        Directory.CreateDirectory(targetPath);

                    // Provera da li fajl već postoji
                    if (File.Exists(destinacija))
                    {
                        DialogResult result = MessageBox.Show("Fajl sa istim imenom već postoji. Da li želite da ga zamenite?",
                                                              "Upozorenje",
                                                              MessageBoxButtons.YesNo,
                                                              MessageBoxIcon.Warning);

                        if (result != DialogResult.Yes)
                            return; // korisnik ne želi da zameni fajl
                    }

                    // Kopiraj fajl
                    File.Copy(izabraniFajl, destinacija, true);

                    // Snimi u bazu
                    InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
                    ins.SnimiUFajlBazu(radniNalogDrumskiID, nazivFajla, destinacija, zaposleniID);

                    MessageBox.Show("Fajl uspešno sačuvan i evidentiran u bazi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška prilikom kopiranja fajla: " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            int radniNalogID = 0;

            var txtID_Aktivni = this.Controls.Find("txtID" + sfx, true).FirstOrDefault() as TextBox;

            if (txtID_Aktivni != null && !int.TryParse(txtID_Aktivni.Text.Trim(), out radniNalogID))
            {
                MessageBox.Show("ID ne postoji.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmPregledFajlova pregled = new frmPregledFajlova(radniNalogID);
            pregled.ShowDialog();
        }


       
        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerji pnd = new frmPartnerji(4);
            pnd.SnimanjeZavrseno += FrmPartnerji_SnimanjeZavrseno;
            pnd.Show();
        }

        private void FrmPartnerji_SnimanjeZavrseno(object sender, EventArgs e)
        {
            FillComboSpedicija();
        }

        private void PodesiVidljivostPoTipuTransporta(int? tipTransporta)
        {
            bool jeTip2 = tipTransporta == 2;
           
            //lblMestoPreuzimanja.Visible = !jeTip2;
            //cboMestoPreuzimanja.Visible = !jeTip2;

            //txtBrojPosiljke.Visible = jeTip2;
            //lblBrojPosiljke.Visible = jeTip2;

            //if (jeTip2)
            //    lblDatumPreuzimanjaPraznog.Text = "Datum carinjenja";
            //else
            //    lblDatumPreuzimanjaPraznog.Text = "Datum preuzimanja praznog kontejnera";
        }

        private void btnVrstaRobeI1_Click(object sender, EventArgs e)
        {
            var cboVrstaRobe = this.Controls.Find("cboVrstaRobe" + sfx, true).FirstOrDefault() as ComboBox;
            if (cboVrstaRobe.SelectedValue == null) return;

            // Kreiramo novi objekat 
            PrivremeniNHM novaStavka = new PrivremeniNHM
            {
                IDNHM = Convert.ToInt32(cboVrstaRobe.SelectedValue),
                Naziv = cboVrstaRobe.Text
            };

            // Dodajemo u listu
            privremenaListaNHM.Add(novaStavka);


            //OsveziGridNHM();
            DataTable dtPrivremeni = new DataTable();
            dtPrivremeni.Columns.Add("IDNHM");
            dtPrivremeni.Columns.Add("Naziv");

            foreach (var stavka in privremenaListaNHM)
            {
                dtPrivremeni.Rows.Add(stavka.IDNHM, stavka.Naziv);
            }

            OsveziGridNHM(dtPrivremeni);

        }

        private void OsveziGridNHM(DataTable dt)
        {
            
             var dataGridView2 = this.Controls.Find("dataGridVrstaRobe" + sfx, true).FirstOrDefault() as DataGridView;
            dataGridView2.DataSource = dt;

            // Provera da li kolone postoje pre formatiranja (zbog sigurnosti)
            if (dataGridView2.Columns.Contains("IDNHM"))
            {
                //dataGridView2.Columns["IDNHM"].Width = 70;
                dataGridView2.Columns["IDNHM"].HeaderText = "ID";
                dataGridView2.Columns["IDNHM"].Visible = false;
            }

            if (dataGridView2.Columns.Contains("Naziv"))
            {
               
                dataGridView2.Columns["Naziv"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView2.Columns["Naziv"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView2.Columns["Naziv"].HeaderText = "Vrsta robe";
            }
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView2.SelectionChanged += (s, e) => dataGridView2.ClearSelection();

            PodesiDatagridView(dataGridView2);

        }

        private void PodesiDatagridView(DataGridView dgv)
        {

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new Font("Helvetica", 12F, GraphicsUnit.Pixel);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);


            //Header
            dgv.EnableHeadersVisualStyles = false;
            //   header.Style.Font = new Font("Arial", 12F, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            // dgv.ColumnHeadersHeight = 30;


        }
        private DataTable VratiPodatkeIzBazeNHM(int idNadredjena)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            DataTable dt = new DataTable();

            // ako nema kontejnerID ili nije iz uvoza prekini izvrsavanje
            if (idNadredjena == 0 || (Uvoz != 0 && Uvoz != 3)) return dt;


            using (SqlConnection con = new SqlConnection(s_connection))
            {
                string query = "";
                if (Uvoz == 0)
                {
                     query = $@"SELECT DISTINCT  nm.ID as IDNHM, (RTRIM(nm.Naziv) + ' - ' + RTRIM(nm.Broj)) as Naziv
                          FROM IzvozNHM inhm 
                          INNER JOIN NHM nm on inhm.IDNHM = nm.ID
                          WHERE inhm.IDNadredjena IN ({idNadredjena})";
                   
                }
                else if (Uvoz == 3)
                {
                     query = $@"SELECT DISTINCT  nm.ID as IDNHM, (RTRIM(nm.Naziv) + ' - ' + RTRIM(nm.Broj)) as Naziv
                          FROM RadniNalogDrumskiNHM inhm 
                          INNER JOIN NHM nm on inhm.IDNHM = nm.ID
                          WHERE inhm.IDNadredjena IN ({idNadredjena})";
                  
                }
                SqlCommand cmd = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    dt.Load(cmd.ExecuteReader());
                }
                catch (Exception ex) { /* log error */ }
            }
            return dt;

        }

        private void btnUnesiNHM_Click(object sender, EventArgs e)
        {
            var cboVrstaRobe = this.Controls.Find("cboVrstaRobe" + sfx, true).FirstOrDefault() as ComboBox;

            if (cboVrstaRobe == null || cboVrstaRobe.SelectedValue == null) return;

            // Kreiramo novi objekat 
            PrivremeniNHM novaStavka = new PrivremeniNHM
            {
                IDNHM = Convert.ToInt32(cboVrstaRobe.SelectedValue),
                Naziv = cboVrstaRobe.Text
            };

            // Dodajemo u listu
            privremenaListaNHM.Add(novaStavka);


            //OsveziGridNHM();
            DataTable dtPrivremeni = new DataTable();
            dtPrivremeni.Columns.Add("IDNHM");
            dtPrivremeni.Columns.Add("Naziv");

            foreach (var stavka in privremenaListaNHM)
            {
                dtPrivremeni.Rows.Add(stavka.IDNHM, stavka.Naziv);
            }

            OsveziGridNHM(dtPrivremeni);

        }

        private void cboMestoUtovaraCeradeI3_Leave(object sender, EventArgs e)
        {
            PopuniAdresu(cboMestoUtovaraCerade, cboAdresaUtovaraCerade);
            PopuniKontaktOsobu(cboMestoUtovaraCerade, cboKontaktUtovaraCerade);
        }

        private void cboMestoIstovaraCeradeI3_Leave(object sender, EventArgs e)
        {
            PopuniAdresu(cboMestoIstovaraCerade, cboAdresaIstovaraCerade);
            PopuniKontaktOsobu(cboMestoIstovaraCerade, cboKontaktIstovaraCerade);

        }

        private void btnPovrdiLO_Click(object sender, EventArgs e)
        {
            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
            List<int> listaIdjeva = new List<int>();
            listaIdjeva.Add(id);
            int temp = PostaviVrednostZaposleni();
            int? NajavuPoslaoKorisnik = temp == 0 ? (int?)null : temp;
            ins.UpdateOdobrioLO(listaIdjeva, NajavuPoslaoKorisnik);
            MessageBox.Show("Nalog je uspešno potvrđen.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PostaviTag(object vrednostIzBaze, Control kontrola)
        {
            if(kontrola != null)
            {
                if (vrednostIzBaze != DBNull.Value && vrednostIzBaze != null)
                {
                    kontrola.Tag = "IZMENJEN";
                }
                else
                {
                    kontrola.Tag = null;
                }
            }
        }

        private void dptDatum_ValueChanged(object sender, EventArgs e)
        {
           DateTimePicker dtp = (DateTimePicker)sender;
            dtp.Tag = "IZMENJEN";

            if (povezaniDatumi.TryGetValue(dtp, out DateTimePicker novi))
            {
                if (novi.Value.Date < dtp.Value.Date)
                {
                    novi.Value = dtp.Value.Date;
                }
            }
        }

        private void cboMestoPreuzimanjaI1_Leave(object sender, EventArgs e)
        {
            PopuniAdresu(cboMestoPreuzimanja, cboAdresaPreuzimanja);
            PopuniKontaktOsobu(cboMestoPreuzimanja, cbokontaktPreuzimanjaI1);
        }

        private void cbo_TextUpdate(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb != null)
            {
                // Ako je lista otvorena (bilo klikom na strelicu ili preko Alt+Down)
                if (cb.DroppedDown)
                {
                    // Zatvaramo veliki padajući meni da bi SuggestAppend mogao normalno da radi
                    cb.DroppedDown = false;

                    // VAŽNO: Kada WinForms zatvori DroppedDown, često resetuje kursor na početak teksta.
                    // Zato ručno pomeramo kursor na kraj teksta kako bi korisnik nastavio da kuca normalno.
                    cb.SelectionStart = cb.Text.Length;
                }
            }
        }

        //private void Numeric_Enter(object sender, EventArgs e)
        //{
        //    if (sender is NumericUpDown nud)
        //    {
        //        // 1. Zapamti trenutnu vrednost
        //        nud.Tag = nud.Value;

        //        // 2. Sakrij tekst tako što ćeš mu dati istu boju kao što je pozadina (npr. bela)
        //        // Korisnik sada vidi potpuno prazno, belo polje
        //        nud.ForeColor = nud.BackColor;

        //        // 3. Pozicioniraj kursor na početak i selektuj sve, 
        //        // tako da čim korisnik počne da kuca, ForeColor se vrati na crnu
        //        BeginInvoke((MethodInvoker)delegate
        //        {
        //            nud.Select(0, nud.Text.Length);
        //        });
        //    }
        //}

        //private void Numeric_Leave(object sender, EventArgs e)
        //{
        //    if (sender is NumericUpDown nud)
        //    {
        //        // Kada korisnik izađe, obavezno vraćamo normalnu boju teksta (npr. crnu)
        //        nud.ForeColor = Color.Black; // ili Color.Black

        //        // Ako je korisnik ručno obrisao sve (BackSpace-om) i ostavio prazno
        //        if (string.IsNullOrWhiteSpace(nud.Text))
        //        {
        //            if (nud.Tag is decimal staraVrednost)
        //            {
        //                nud.Value = staraVrednost;
        //            }
        //        }
        //    }
        //}

        private void Numeric_Enter(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;

            // sacuvaj staru vrednost
            num.Tag = num.Value;

            // oznaci da jos nije menjano
            num.AccessibleDescription = "nijeMenjano";

            BeginInvoke(new Action(() =>
            {
                num.Select(0, num.Text.Length);
            }));
        }

        private void Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;

            // korisnik je poceo unos
            num.AccessibleDescription = "menjano";
        }

        private void Numeric_Leave(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;

            // ako nije menjano vrati staru vrednost
            if (num.AccessibleDescription == "nijeMenjano")
            {
                num.Value = (decimal)num.Tag;
            }
        }

        private void PoveziDatume(DateTimePicker referentni, DateTimePicker novi)
        {
            if (referentni == null || novi == null)
                return;

            povezaniDatumi[referentni] = novi;
        }

        //private void Datum_ValueChanged(object sender, EventArgs e)
        //{
        //    DateTimePicker novi = (DateTimePicker)sender;

        //    if (novi.Tag is DateTimePicker stari)
        //    {
        //        if (novi.Value.Date < stari.Value.Date)
        //        {
        //            novi.Value = stari.Value.Date;
        //        }
        //    }
        //}

        private void frmDrumski1_Load(object sender, EventArgs e)
        {
            if (dtPreuzimanjaPraznogKontejnera != null && dtPreuzimanjaPraznogKontejneraNovi != null)
                PoveziDatume(dtPreuzimanjaPraznogKontejnera, dtPreuzimanjaPraznogKontejneraNovi);
            if (dtpSpustanjePunog != null && dtpSpustanjePunogNovi != null)
                PoveziDatume(dtpSpustanjePunog, dtpSpustanjePunogNovi);
            if (dtpUtovara != null && dtpUtovaraNovi != null)
                PoveziDatume(dtpUtovara, dtpUtovaraNovi);
            if (dtpUtovaraCerade != null && dtpUtovaraCeradeNovi != null)
                PoveziDatume(dtpUtovaraCerade, dtpUtovaraCeradeNovi);
            if (dtpIstovaraCerade != null && dtpIstovaraCeradeNovi != null)
                PoveziDatume(dtpIstovaraCerade, dtpIstovaraCeradeNovi);


            if (txtTaraK != null)
                PoveziNumeric(txtTaraK);
            if (txtBrutoK != null)
                PoveziNumeric(txtBrutoK);
            if (txtNTTOR != null)
                PoveziNumeric(txtNTTOR);
            if (txtTrosak != null)
                PoveziNumeric(txtTrosak);
            if (txtCena != null)
                PoveziNumeric(txtCena);
        }

        private void PoveziNumeric(NumericUpDown num)
        {
            num.Enter += Numeric1_Enter;
            num.Leave += Numeric1_Leave;
            num.ValueChanged += Numeric1_ValueChanged;
        }
        private void Numeric1_Enter(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;

            staraVrednost = num.Value;
            promenjenaVrednost = false;

            BeginInvoke(new Action(() =>
            {
                TextBox tb = num.Controls[1] as TextBox;

                if (tb != null)
                {
                    tb.Clear();
                }
            }));
        }

        private void Numeric1_ValueChanged(object sender, EventArgs e)
        {
            promenjenaVrednost = true;
        }

        private void Numeric1_Leave(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;

            TextBox tb = num.Controls[1] as TextBox;

            // ako nije promenjena vrednost vrati staru
            if (!promenjenaVrednost)
            {
                num.Value = staraVrednost;
            }

            // osvezi prikaz teksta
            if (tb != null)
            {
                tb.Text = num.Value.ToString();
            }
        }
    }
}
