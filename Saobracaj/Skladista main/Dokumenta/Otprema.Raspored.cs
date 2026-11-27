using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Saobracaj.Skladista_main.Dokumenta
{
    // Raspored Otpreme kao na Prijemnici: traka sa dugmadima naloga na vrhu, grupe polja koje se skrivaju i pokazuju
    // (stanje se pamti po korisniku na računaru, GrupeForme) i ispod njih dugmad za stavke, polja za izmenu stavke i tabela.
    // Kontrole iz dizajnera se samo premeštaju u nove kontejnere; dizajner se ne menja. Poziva se iz PrimeniSava.
    public partial class Otprema
    {
        GrupeForme grupeForme;
        FlowLayoutPanel trakaNaloga, trakaStavki;

        private void InitRaspored()
        {
            SuspendLayout();

            // dugmad naloga (bila su u koloni desno) idu u traku na vrhu; naslov forme ostaje levo
            trakaNaloga = new FlowLayoutPanel { Dock = DockStyle.Top, WrapContents = false };
            lblTip.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            lblTip.AutoSize = true;
            foreach (Control c in new Control[] { lblTip, btnSnimi, btnStorno, btnIspravka, btnSaglasnost, btnFormiranRN, btnOtpremnica })
            {
                c.Parent.Controls.Remove(c);
                c.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                c.Margin = new Padding(0, 0, 8, 0);
                trakaNaloga.Controls.Add(c);
            }
            lblTip.Margin = new Padding(0, 0, 24, 0);
            Controls.Add(trakaNaloga);

            PoredjajUJedanRed(panelOtprema1, 466, 55);             // Prevoz: dva reda po tri polja -> jedan red
            PoredjajUJedanRed(panelOtprema2, 466, 55, 105);        // Odredište: prvi i drugi red jedan do drugog
            PoredjajDodatneUsluge();

            grupeForme = new GrupeForme(this, "Otprema");
            grupeForme.Dodaj("Osnovni", "Osnovni podaci", true, false, true,
                label1, txtID, label11, textBox2, btnPrijemnica, label2, textBox1, label3, cboMagacinskiBroj, btnMagacinskiBroj,
                label4, cboNalogodavac, label43, cboCarinskiPostupak, label44, txtOpisPosla);
            grupeForme.Dodaj("Vlasnik", "Vlasnik i roba", true, false, true, panel1);
            grupeForme.Dodaj("Prevoz", "Prevoz", true, false, false, panelOtprema1);
            grupeForme.Dodaj("Odrediste", "Odredište i špedicija", true, false, false, panelOtprema2);
            grupeForme.Dodaj("Usluge", "Dodatne usluge i napomene", false, false, false, panel4);

            // donji deo: dugmad za stavke, polja izabrane stavke (Izmeni, Izbaci) i tabela stavki
            trakaStavki = new FlowLayoutPanel { Height = 44, Padding = new Padding(8, 6, 8, 4), WrapContents = false };
            foreach (Button b in new[] { btnRazduzi, button1, btnIzborPaleta })   // Izbor paleta postoji samo uz izlaz po paletama
            {
                if (b == null) continue;
                b.Parent.Controls.Remove(b);
                b.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                b.Margin = new Padding(0, 0, 8, 0);
                trakaStavki.Controls.Add(b);
            }

            var deoStavki = new Panel();
            gridGroupingControl1.Parent.Controls.Remove(gridGroupingControl1);
            gridGroupingControl1.Dock = DockStyle.Fill;
            deoStavki.Controls.Add(gridGroupingControl1);
            panel3.Parent.Controls.Remove(panel3);
            panel3.Dock = DockStyle.Top;
            deoStavki.Controls.Add(panel3);   // polja stavke iznad tabele

            // paneli za izbor (magacinski broj, prijemnica) ostaju iznad rasporeda
            grupeForme.Zavrsi(deoStavki, trakaStavki, panel5, panel2);

            ResumeLayout(true);
        }

        // Panel sa poljima u više redova: kontrole čiji je vrh (u tačkama dizajna) između granica[0] i granica[1] prelaze
        // desno od prvog reda, a redovi ispod se podižu za visinu prebačenog reda. sirinaDizajna: širina panela u dizajneru
        // (forma se skalira po fontu, pa se mere množe razmerom).
        private static void PoredjajUJedanRed(Panel panel, int sirinaDizajna, params int[] granice)
        {
            float s = panel.Width / (float)sirinaDizajna;
            int od = (int)(granice[0] * s), @do = granice.Length > 1 ? (int)(granice[1] * s) : int.MaxValue;
            var red = panel.Controls.Cast<Control>().Where(c => c.Top >= od && c.Top < @do).ToList();
            var ispod = panel.Controls.Cast<Control>().Where(c => c.Top >= @do).ToList();
            if (red.Count == 0)
                return;

            int pomakX = panel.Controls.Cast<Control>().Except(red).Max(c => c.Right) + (int)(20 * s) - red.Min(c => c.Left);
            int pomakY = red.Min(c => c.Top) - panel.Controls.Cast<Control>().Except(red).Except(ispod).Min(c => c.Top);
            foreach (var c in red)
                c.Location = new Point(c.Left + pomakX, c.Top - pomakY);
            foreach (var c in ispod)
                c.Top -= pomakY;

            panel.Size = new Size(panel.Controls.Cast<Control>().Max(c => c.Right) + (int)(8 * s),
                                  panel.Controls.Cast<Control>().Max(c => c.Bottom) + (int)(8 * s));
        }

        // Žuti panel (posebni uslovi, dodatne usluge, napomena) je uzak i visok; u grupi se slaže u jedan red (kao na Prijemnici)
        private void PoredjajDodatneUsluge()
        {
            float s = panel4.Width / 200f;   // dizajn je 200 px širok; forma se skalira po fontu
            txtPosebniUslovi.Size = new Size((int)(300 * s), (int)(60 * s));
            txtNapomena.Size = new Size((int)(300 * s), (int)(60 * s));
            dgvUsluge.Size = new Size((int)(320 * s), (int)(110 * s));

            var grupe = new[]
            {
                new Control[] { label25, txtPosebniUslovi },
                new Control[] { label26, cboDodatneUsluge, btnDodaj, btnIzbaci },
                new Control[] { dgvUsluge },
                new Control[] { label27, txtNapomena }
            };

            int x = (int)(6 * s), vrh = (int)(6 * s), dno = 0;
            foreach (var grupa in grupe)
            {
                int levo = grupa.Min(c => c.Left), gore = grupa.Min(c => c.Top);
                foreach (Control c in grupa)
                    c.Location = new Point(c.Left - levo + x, c.Top - gore + vrh);
                x = grupa.Max(c => c.Right) + (int)(20 * s);
                dno = Math.Max(dno, grupa.Max(c => c.Bottom));
            }
            panel4.Size = new Size(x, dno + (int)(6 * s));
        }
    }
}
