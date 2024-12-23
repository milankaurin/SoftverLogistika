using System.ComponentModel.DataAnnotations;

namespace DeljeniPodaci
{
    public class Posiljka
    {
        public Guid Id { get; set; }

        
        public string Naziv { get; set; }
        public StatusPosiljke Status { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime? DatumIsporuke { get; set; }

        public string Posiljalac {  get; set; }

        public string Primalac { get; set; }

        public static readonly Dictionary<int, string> Statusi = new()
    {
        { 1, "Na putu" },
        { 3, "Isporučeno" },
        { 2, "U skladištu" }
    };

        public static string VratiStatusKaoTekst(int i)
        {
            if (Statusi.TryGetValue(i, out string tekst)) { return tekst; } else { return "Nepoznat status"; }
        }

    }

    public enum StatusPosiljke
    {
        NaPutu = 1,
        USkladistu = 2,
        Isporuceno = 3,
    }
}
