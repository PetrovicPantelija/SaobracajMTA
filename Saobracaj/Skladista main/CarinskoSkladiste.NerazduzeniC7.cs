namespace Saobracaj.Skladista_main
{
    // Dugme „Pregled nerazduženih C7/SD koje uskoro ističu“ otvara pregled nerazduženih C7 i izveštaj o skladištenju
    // (frmSkladistenjeC7); dugme u dizajneru nije imalo akciju.
    public partial class CarinskoSkladiste
    {
        private void InitNerazduzeniC7()
        {
            btnPregledNerazduzenih.Click += (s, e) =>
            {
                var main = this.TopLevelControl as NewMain;
                if (main == null) return;
                main.OtvoriFormuBezPrava(() => new frmSkladistenjeC7());
            };
        }
    }
}
