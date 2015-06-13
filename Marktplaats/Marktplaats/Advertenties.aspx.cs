using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marktplaats
{
    /// <summary>
    /// This class contains the code to display the adverts relatedto the selected Categorie.
    /// </summary>
    public partial class Advertenties : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Rout();
        }

       /// <summary>
       /// This methods routs the user and takes the Category out of the Url.
       /// </summary>
        public void Rout()
        {
            string id = (string)Page.RouteData.Values["id"];
            GetAdvertenties(id);
        }

        /// <summary>
        /// Gets the adverts for the categorieID passed by the rout method.
        /// </summary>
        /// <param name="id"></param>
        public void GetAdvertenties(string id)
        {
            try
            {
                DataSet output = new DataSet();
                Administratie administratie = new Administratie();
                output = administratie.GetData("SELECT p.PERSOONID AS PERSOONID, p.Naam AS NAAM, a.Titel AS TITEL, a.AdvertentieId AS Id " +
                                               "FROM Persoon p " +
                                               "JOIN Advertentie a ON p.PERSOONID = a.PERSOONID " +
                                               "WHERE GROEPID = " + "'" + id + "'");

                RepeaterAdvertenties.DataSource = output;
                RepeaterAdvertenties.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}