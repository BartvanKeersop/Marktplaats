using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Marktplaats
{
    public abstract class Persoon
    {
        //Properties
        public string Email { get; set; }
        public string Naam { get; set; }
        public string Telefoonnummer { get; set; }
        public DateTime InschrijfDatum { get; set; }

        //Constructor
        protected Persoon(string email, string naam)
        {
            Email = email;
            Naam = naam;
        }

        protected Persoon(string email, string naam, string telefoonnummer, DateTime inschrijfdatum)
        {
            Email = email;
            Naam = naam;
            Telefoonnummer = telefoonnummer;
            InschrijfDatum = inschrijfdatum;
        }
    }
}