using Saobracaj.Administracija;
using System.Net;
using System.Net.Mail;

namespace Saobracaj.HR
{
    // Slanje maila preko podešavanja Mail.* iz tabele Podesavanja
    public static class HRMail
    {
        public static void Posalji(string kome, string naslov, string tekst, string prilog, bool kopija = false)
        {
            if (!Podesavanja.Ucitano)
                Podesavanja.Reload();
            string server = Podesavanja.Get("Mail.SmtpServer");
            string posiljalac = Podesavanja.Get("Mail.Posiljalac");
            int port = Podesavanja.Postoji("Mail.SmtpPort") ? Podesavanja.GetInt("Mail.SmtpPort") : 25;
            bool ssl = Podesavanja.Postoji("Mail.SSL") && Podesavanja.GetBool("Mail.SSL");

            using (var poruka = new MailMessage(posiljalac, kome))
            {
                poruka.Subject = naslov;
                poruka.Body = tekst;
                poruka.IsBodyHtml = false;
                if (kopija && Podesavanja.Postoji("Mail.KopijaResenja"))
                    poruka.CC.Add(Podesavanja.Get("Mail.KopijaResenja"));
                if (!string.IsNullOrEmpty(prilog))
                    poruka.Attachments.Add(new Attachment(prilog));

                using (var smtp = new SmtpClient(server, port) { EnableSsl = ssl })
                {
                    if (Podesavanja.Postoji("Mail.Korisnik"))
                        smtp.Credentials = new NetworkCredential(Podesavanja.Get("Mail.Korisnik"), Podesavanja.Get("Mail.Lozinka", ""));
                    else
                        smtp.UseDefaultCredentials = true;
                    smtp.Send(poruka);
                }
            }
        }
    }
}
