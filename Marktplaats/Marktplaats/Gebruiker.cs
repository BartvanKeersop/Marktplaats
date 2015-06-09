using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marktplaats
{
    [Serializable]
    public class Gebruiker : Persoon
    {
        //Properties
        public int GebruikerId { get; set; }
        public string Email { get; set; }
        public string Naam { get; set; }
        public string Telefoonnummer { get; set; }
        public DateTime InschrijfDatum { get; set; }
        public bool AdminRechten { get; set; }

        //Constructor
        public Gebruiker(string email, string naam, bool adminrechten, int gebruikerId) : base(email, naam)
        {
            Email = email;
            Naam = naam;
            AdminRechten = adminrechten;
            GebruikerId = gebruikerId;
        }

        public Gebruiker(string email, string naam, string telefoonnummer, DateTime inschrijfdatum) : base(email, naam, telefoonnummer, inschrijfdatum)
        {
            Email = email;
            Naam = naam;
            Telefoonnummer = telefoonnummer;
            InschrijfDatum = inschrijfdatum;
        }
    }
}