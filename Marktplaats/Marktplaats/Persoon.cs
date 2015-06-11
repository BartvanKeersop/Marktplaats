using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Marktplaats
{
    [Serializable]
    public abstract class Persoon
    {
        //Properties
        public int Id { get; set; }
        public string Email { get; set; }
        public string Naam { get; set; }
        public string Telefoonnummer { get; set; }
        public DateTime InschrijfDatum { get; set; }

        //Constructor
        protected Persoon(int id, string naam)
        {
            Id = id;
            Naam = naam;
        }

        protected Persoon(int id, string naam, string telefoonnummer, DateTime inschrijfdatum)
        {
            Id = id;
            Naam = naam;
            Telefoonnummer = telefoonnummer;
            InschrijfDatum = inschrijfdatum;
        }
    }
}