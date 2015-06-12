using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;

namespace Marktplaats
{
    public class Advertentie
    {
        #region Properties
        public int Advertentienummer { get; set; }
        public bool Leveren { get; set; }
        public bool Ophalen { get; set; }
        public string Afmeting { get; set; }
        public double Gewicht { get; set; }
        public decimal Zendprijs { get; set; }
        public string Titel { get; set; }
        public int AantalBezocht { get; set; }
        public int AantalFavouriet { get; set; }
        public DateTime PlaatsingsDatum { get; set; }
        public string Conditie { get; set; }
        public string Merk { get; set; }
        public string Beschrijving { get; set; }
        public string Foto { get; set; }
        public string Website { get; set; }
        public string Type { get; set; }
        public string Eigenschap { get; set; }
        public Aanbieder Aanbiederpersoon { get; set; }
        #endregion

        #region Constructors
        public Advertentie(int advertentieNummer, string titel)
        {
            Advertentienummer = advertentieNummer;
            Titel = titel;
        }

        public Advertentie(int advertentieNummer, string titel, Aanbieder aanbieder, string foto)
        {
            Advertentienummer = advertentieNummer;
            Titel = titel;
            Aanbiederpersoon = aanbieder;
            Foto = foto;
        }
        #endregion
    }
}