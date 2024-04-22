namespace Saobracaj.Dokumenta.TrainListItem
{
    public class TrainListItemModel
    {
        public int Id { get; set; }
        public int TrainListId { get; set; }
        public int RedniBroj { get; set; }
        public string OznakaKola { get; set; }
        public string SerijaKola { get; set; }
        public decimal TaraKola { get; set; }
        public decimal GKocnaMasa { get; set; }
        public decimal PKocnaMasa { get; set; }
        public decimal DuzinaKola { get; set; }
        public int BrojOsovina { get; set; }
        public string KontBroj { get; set; }
        public string KontTip { get; set; }
        public decimal KontTara { get; set; }
        public decimal Neto { get; set; }
        public decimal RIDRobaMasa { get; set; }
        public int BrojKomada { get; set; }
        public int CIM { get; set; }
        public string OtpStanicaTerminal { get; set; }
        public string PolStanicaTerminal { get; set; }
        public string Posiljac { get; set; }
        public string Primalac { get; set; }
        public string Proizvod { get; set; }
        public string T1 { get; set; }
        public string Klient { get; set; }
        public string Buking { get; set; }
    }
}
