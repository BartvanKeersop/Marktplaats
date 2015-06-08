using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marktplaats
{
    public class Bestelling
    {
        //Properties
        public int BestellingId { get; set; }
        public bool Topadvertentie { get; set; }
        public bool OmhoogPlaatsen { get; set; }
        public bool DagTopper { get; set; }
        public DateTime Datum { get; set; }
        public decimal Prijs { get; set; }

        //Constructor
        public Bestelling(bool topadvertentie, bool omhoogplaatsen, bool dagtopper, DateTime datum, decimal prijs)
        {
            Topadvertentie = topadvertentie;
            OmhoogPlaatsen = omhoogplaatsen;
            DagTopper = dagtopper;
            Datum = datum;
            Prijs = prijs;
        }
}
}