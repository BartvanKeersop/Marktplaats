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
        public List<Advertentie> BekekenAdvertenties { get; set; }
        public List<Advertentie> AangeradenAdvertenties { get; set; }

        //Constructor
        public Gebruiker(int id, string naam, bool adminrechten) : base(id, naam)
        {
            GebruikerId = id;
            Naam = naam;
            AdminRechten = adminrechten;
            AangeradenAdvertenties = new List<Advertentie>();
            BekekenAdvertenties = new List<Advertentie>();
        }

        public void GenereerAanbevolenAdvertenties(Gebruiker gebruiker)
        {
            Database database = Database.Instance;
            bool advertentiebestaatal = true;

            gebruiker.AangeradenAdvertenties.Clear();

            if (gebruiker.BekekenAdvertenties.Count > 0)
            {
                int counter = gebruiker.BekekenAdvertenties.Count;

                if (counter > 3)
                {
                    counter = 3;
                }
                

                for (int i=0; i < counter; i++)
                {
                    int groepId =
                        Convert.ToInt32(database.GetGroupIdWithAdvertentieId(gebruiker.BekekenAdvertenties[i].Advertentienummer)[0]["GROEPID"]);

                    while (advertentiebestaatal)
                    {
                        List<Dictionary<string, object>> data = database.GetAanbevolenAdvDataRandom(groepId);

                        string titel = Convert.ToString(data[0]["TITEL"]);
                        int advertentieId = Convert.ToInt32(data[0]["ADVERTENTIEID"]);
                        string naam = Convert.ToString(data[0]["NAAM"]);
                        int persoonId = Convert.ToInt32(data[0]["PERSOONID"]);
                        string foto = Convert.ToString(data[0]["FOTO"]);

                        advertentiebestaatal = CheckBestaatAl(advertentieId, gebruiker);

                        if (advertentiebestaatal == false)
                        {
                            Aanbieder aanbieder = new Aanbieder(persoonId, naam);
                            Advertentie advertentie = new Advertentie(advertentieId, titel, aanbieder, foto);
                            gebruiker.AangeradenAdvertenties.Add(advertentie);
                        }
                    }
                }
            }
        }

        public bool CheckBestaatAl(int advertentieId, Gebruiker gebruiker)
        {
            foreach (Advertentie adver in gebruiker.AangeradenAdvertenties)
            {
                if (adver.Advertentienummer == advertentieId)
                {
                    return true;
                }
            }
                return false;
        }

        public void VoegAdvertentieAanBekekenLijstToe(Advertentie advertentie)
        {
            BekekenAdvertenties.RemoveAt(0);
            BekekenAdvertenties.Add(advertentie);
        }
    }
}