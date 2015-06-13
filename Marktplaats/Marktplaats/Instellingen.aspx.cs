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
    /// This page contains all the information about the instellingen page.
    /// A user can change is settings here.
    /// </summary>
    public partial class Instellingen : System.Web.UI.Page
    {
        private Gebruiker gebruiker;
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckGebruiker();
            GetInstellingen(gebruiker.GebruikerId);
        }

        #region Methods
        /// <summary>
        /// This method checks if the email in the url matches with the "gebruiker" in the session.
        /// If not, the session is set to null and the user is redirected to the index page.
        /// </summary>
        public void CheckGebruiker()
        {
            gebruiker = (Gebruiker)Session["gebruiker"];
            if (gebruiker == null)
            {
                Server.Transfer("index.aspx", true);
            }

            string idString = (string)Page.RouteData.Values["id"];
            int id = Convert.ToInt32(idString);
            if (id != gebruiker.GebruikerId)
            {
                Session["gebruiker"] = null;
                Server.Transfer("index.aspx", true);
            }
        }

        /// <summary>
        /// This method gets the current settings for the account that is currently logged in
        /// </summary>
        /// <param name="id"></param>
        public void GetInstellingen(int id)
        {
            try
            {
                DataSet output = new DataSet();
                Administratie administratie = new Administratie();
                output = administratie.GetData("SELECT NAAM, POSTCODE, TELEFOONNUMMER, INSCHRIJFDATUM, DIGITALEFACTUUR FROM PERSOON WHERE PERSOONID = " + id);

                RepeaterInstellingen.DataSource = output;
                RepeaterInstellingen.DataBind();

                int factuurInt = Convert.ToInt32(output.Tables[0].Rows[0]["DIGITALEFACTUUR"].ToString());

                if (factuurInt == 1)
                {
                    
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}