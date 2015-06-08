using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marktplaats
{
    public class Subgroep : Groep
    {
       //Properties
       public int Id { get; set; }
       public string Naam { get; set; }
       public int ParentGroepId { get; set; }

        //Constructor
       public Subgroep(string naam, int parentGroepId) : base(naam)   
       {
            
       }

       public Subgroep(string naam, int id, int parentGroepId) : base(naam, id)
       {
            
       }
    }
}