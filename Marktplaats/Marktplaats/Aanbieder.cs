using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marktplaats
{
    public class Aanbieder : Persoon
    {
        
        //Properties
        public int Id { get; set; }
        public string Email { get; set; }
        public string Naam { get; set; }
        public string Telefoonnummer { get; set; }
        public DateTime InschrijfDatum { get; set; }


        //Constructor
        public Aanbieder(int id, string naam) : base(id, naam)
        {
            Id = id;
            Naam = naam;
        }

        public Aanbieder(int id, string naam, string telefoonnummer, DateTime inschrijfdatum) :base(id, naam, telefoonnummer, inschrijfdatum)
        {
            Id = id;
            Naam = naam;
            Telefoonnummer = telefoonnummer;
            InschrijfDatum = inschrijfdatum;
        }
    }
}