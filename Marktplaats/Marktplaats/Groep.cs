using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Microsoft.SqlServer.Server;

namespace Marktplaats
{
    public abstract class Groep
    {
        //Properties
        public int Id { get; set; }
        public string Naam { get; set; }

        //Constructor
        protected Groep(string naam)
        {
            
        }

        protected Groep(string naam, int id)
        {
            
        }
    }
}