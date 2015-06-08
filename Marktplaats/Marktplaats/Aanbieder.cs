using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marktplaats
{
    public class Aanbieder : Persoon
    {
        
        //Properties
        public string Email { get; set; }
        public string Naam { get; set; }
        public string Telefoonnummer { get; set; }
        public DateTime InschrijfDatum { get; set; }

        //Constructor
        public Aanbieder(string email, string naam) : base(email, naam)
        {
            Email = email;
            Naam = naam;
        }

        public Aanbieder(string email, string naam, string telefoonnummer, DateTime inschrijfdatum) :base(email, naam, telefoonnummer, inschrijfdatum)
        {
            Email = email;
            Naam = naam;
            Telefoonnummer = telefoonnummer;
            InschrijfDatum = inschrijfdatum;
        }
    }
}