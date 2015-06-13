using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marktplaats
{
    [Serializable]
    public class Gebruiker : Persoon
    {
        #region Properties
        public int GebruikerId { get; set; }
        public string Email { get; set; }
        public string Naam { get; set; }
        public string Telefoonnummer { get; set; }
        public DateTime InschrijfDatum { get; set; }
        public bool AdminRechten { get; set; }
        public List<Advertentie> BekekenAdvertenties { get; set; }
        public List<Advertentie> AangeradenAdvertenties { get; set; }
        #endregion

        #region Constructor
        public Gebruiker(int id, string naam, bool adminrechten) : base(id, naam)
        {
            GebruikerId = id;
            Naam = naam;
            AdminRechten = adminrechten;
            AangeradenAdvertenties = new List<Advertentie>();
            BekekenAdvertenties = new List<Advertentie>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method contains an algorythm to generate random ads based on what adverts the user has viewed.
        /// </summary>
        /// <param name="gebruiker">The current user</param>
        public void GenereerAanbevolenAdvertenties(Gebruiker gebruiker)
        {
            Database database = Database.Instance;

            //clears the list
            gebruiker.AangeradenAdvertenties.Clear();

            //The method doesnt run if the user hasn't viewed any adverts yet
            if (gebruiker.BekekenAdvertenties.Count > 0)
            {
                int counter = gebruiker.BekekenAdvertenties.Count;

                //No more than 3 adverts will be generated
                if (counter > 3)
                {
                    counter = 3;
                }
                
                //Loops from 0 to 3(max)
                for (int i=0; i < counter; i++)
                {
                    //Gets the categoryId for each viewed advert
                    int groepId =
                        Convert.ToInt32(database.GetGroupIdWithAdvertentieId(gebruiker.BekekenAdvertenties[i].Advertentienummer)[0]["GROEPID"]);

                    //True to enter the loop
                   bool advertentiebestaatal = true;

                    while (advertentiebestaatal)
                    {
                        //Gets a random advert
                        List<Dictionary<string, object>> data = database.GetAanbevolenAdvDataRandom(groepId);

                        //Stores the data in parameters
                        string titel = Convert.ToString(data[0]["TITEL"]);
                        int advertentieId = Convert.ToInt32(data[0]["ADVERTENTIEID"]);
                        string naam = Convert.ToString(data[0]["NAAM"]);
                        int persoonId = Convert.ToInt32(data[0]["PERSOONID"]);
                        string foto = Convert.ToString(data[0]["FOTO"]);

                        //Calls a method to check if the advert already exists in the AangeradenAdvertentie List
                        advertentiebestaatal = CheckBestaatAl(advertentieId, gebruiker);

                        //If the advert doesn't exist it get's added to the AangeradenAdvert List
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

        /// <summary>
        /// Thi method checks if the passed advert is already in the passed gebruikers AangeradenAdvertentie List
        /// </summary>
        /// <param name="advertentieId"></param>
        /// <param name="gebruiker"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This method removes the first advert in the list, and ads another one
        /// </summary>
        /// <param name="advertentie"></param>
        public void VoegAdvertentieAanBekekenLijstToe(Advertentie advertentie)
        {
            BekekenAdvertenties.RemoveAt(0);
            BekekenAdvertenties.Add(advertentie);
        }
        #endregion
    }
}