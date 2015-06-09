using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marktplaats
{
    public partial class Advertenties : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Rout();
        }
        public void Rout()
        {
            string categorieUrl = (string)Page.RouteData.Values["Advertenties"];
            string id = (string)Page.RouteData.Values["id"];

            GetAdvertenties(id);
        }

        public void GetAdvertenties(string id)
        {
            try
            {
                DataSet output = new DataSet();
                Administratie administratie = new Administratie();
                output = administratie.GetData("SELECT p.Email AS EMAIL, p.Naam AS NAAM, a.Titel AS TITEL, a.AdvertentieId AS Id " +
                                               "FROM Persoon p " +
                                               "JOIN Advertentie a ON p.EMAIL = a.EMAIL " +
                                               "WHERE GROEPID = " + "'" + id + "'");

                RepeaterAdvertenties.DataSource = output;
                RepeaterAdvertenties.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
    }
}