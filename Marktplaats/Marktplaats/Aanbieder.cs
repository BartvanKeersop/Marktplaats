using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marktplaats
{

    /// <summary>
    /// This class is a subclass of Persoon. It it used when an advert is loaded to store the person who placed the advert.
    /// </summary>
    public class Aanbieder : Persoon
    {

        #region properties
        public int Id { get; set; }
        public string Email { get; set; }
        public string Naam { get; set; }
        public string Telefoonnummer { get; set; }
        public DateTime InschrijfDatum { get; set; }
        #endregion


        #region contructor
        public Aanbieder(int id, string naam) : base(id, naam)
        {
            Id = id;
            Naam = naam;
        }
        #endregion
    }
}