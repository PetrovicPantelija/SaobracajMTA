using System.Drawing;

namespace Saobracaj.Theme
{
    // Vizuelni stil Sava (docs/SAVA-DESIGN.md): boje i fontovi. Fontovi se prave jednom i dele među kontrolama.
    public static class SavaTheme
    {
        // Osnovna paleta
        public static readonly Color Primary = ColorTranslator.FromHtml("#0A66B8");       // Leget plava
        public static readonly Color Night = ColorTranslator.FromHtml("#12304D");         // Noćna Sava
        public static readonly Color Cyan = ColorTranslator.FromHtml("#3DB5E6");          // Cijan krst
        public static readonly Color Surface = ColorTranslator.FromHtml("#F4F7FA");       // Radna površina
        public static readonly Color Line = ColorTranslator.FromHtml("#D4DDE6");          // Linija
        public static readonly Color Text = ColorTranslator.FromHtml("#172433");          // Tekst

        // Izvedene boje
        public static readonly Color Label = ColorTranslator.FromHtml("#4C5D6E");
        public static readonly Color FieldBorder = ColorTranslator.FromHtml("#B9C6D3");
        public static readonly Color GridHeader = ColorTranslator.FromHtml("#EAF0F6");
        public static readonly Color GridLine = ColorTranslator.FromHtml("#E6ECF1");
        public static readonly Color SelectedRow = ColorTranslator.FromHtml("#DDF1FB");
        public static readonly Color Danger = ColorTranslator.FromHtml("#B3261E");
        public static readonly Color MenuText = ColorTranslator.FromHtml("#C7D3E0");
        public static readonly Color White = ColorTranslator.FromHtml("#FFFFFF");
        public static readonly Color PrimaryHover = ColorTranslator.FromHtml("#0958A0");
        public static readonly Color HoverLight = ColorTranslator.FromHtml("#EEF3F8");
        public static readonly Color DangerHover = ColorTranslator.FromHtml("#FBEAEA");

        // Fontovi (polu-masni je posebna porodica "Segoe UI Semibold", ne FontStyle.Bold)
        public static readonly Font HeaderTitleFont = new Font("Segoe UI Semibold", 11f);
        public static readonly Font FormTitleFont = new Font("Segoe UI Semibold", 14f);
        public static readonly Font FieldFont = new Font("Segoe UI", 10f);
        public static readonly Font LabelFont = new Font("Segoe UI", 9f);
        public static readonly Font GridHeaderFont = new Font("Segoe UI Semibold", 9f);
        public static readonly Font ButtonFont = new Font("Segoe UI", 10f);
        public static readonly Font ButtonFontSemibold = new Font("Segoe UI Semibold", 10f);
        public static readonly Font ComputedFont = new Font("Segoe UI", 10f, FontStyle.Bold);

        // Mere
        public const int ToolbarHeight = 52;
        public const int ButtonHeight = 32;
        public const int GridHeaderHeight = 32;
        public const int GridRowHeight = 30;
    }
}
