using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;

namespace Marktplaats
{
    public class Advertentie
    {
        //Properties
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
        //Constructor, nog bepalen welke waarde in de constructor moeten worden meegegeven.
        public Advertentie(int advertentienummer, bool leveren, bool ophalen, string afmetingen, double gewicht, decimal zendprijs, string titel, int aantalBezocht, int aantalFavouriet, DateTime plaatsingsDatum, string conditie, string merk, string beschrijving, string foto, string website, string type, string eigenschap)
        {
            Advertentienummer = advertentienummer;
            Leveren = leveren;
            Ophalen = ophalen;
            Afmeting = Afmeting;
            Gewicht = gewicht;
            Zendprijs = zendprijs;
            Titel = titel;
            AantalBezocht = aantalBezocht;
            AantalFavouriet = aantalFavouriet;
            PlaatsingsDatum = plaatsingsDatum;
            Conditie = conditie;
            Merk = merk;
            Beschrijving = beschrijving;
            Foto = foto;
            Website = website;
            Type = type;
            Eigenschap = eigenschap;
        }
    }
}