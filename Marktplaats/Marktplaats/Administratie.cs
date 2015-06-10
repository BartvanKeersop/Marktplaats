using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Marktplaats
{
    public class Administratie
    {
        //Properties
        public Gebruiker User { get; set; }

        //Constructor
        public Administratie()
        {
        }

        public Administratie(Gebruiker gebruiker)
        {
            User = gebruiker;
        }

        public DataSet GetData(string query)
        {
            Database database = Database.Instance;
            return database.GetData(query);
        }

        public void InsertData(string query)
        {
            Database database = Database.Instance;
            database.InsertData(query);
        }
    }
}