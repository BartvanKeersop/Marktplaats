using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Marktplaats
{
    /// <summary>
    /// This page contains the code for the advertentie page.
    /// On this page the advert is loaded and the user can place a bid, view the advert, and delete the advert if it is his own.
    /// </summary>
    public partial class Advertentie1 : System.Web.UI.Page
    {
        #region Fields
        private Gebruiker gebruiker; //The current user.
        private int hoogsteBod; //Highest bid on the advert, used for checkinf if the placed bid is high enough.
        private int minimaalBod; //Lowest bid a user can place on the advert.
        private int advertentieId; //The id of the current advert.
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Rout();
            gebruiker = (Gebruiker)Session["gebruiker"];

            if (gebruiker != null)
            {
                lblMessageBod.Visible = false;
                ShowEditAndDeleteButtons();
            }

            Rout();
        }
        #endregion

        #region ShowEditAndDeleteButtons
        /// <summary>
        /// This method checks if session stored user created the advert or has adminrights, when he does he can delete the advert.
        /// </summary>
        public void ShowEditAndDeleteButtons()
        {
            Database database = Database.Instance;
            List<Dictionary<string, object>> data = database.GetGebruikerIdWithAdvId(advertentieId);

            int vergelijkId = Convert.ToInt32(data[0]["PERSOONID"]);

            if (gebruiker.AdminRechten)
            {
                btnVerwijderen.Visible = true;
            }
            else if (gebruiker.GebruikerId == vergelijkId)
            {
                btnVerwijderen.Visible = true;
            }
            else
            {
                btnVerwijderen.Visible = false;
            }
        }
        #endregion

        #region Rout
        public void Rout()
        {
            int id = Convert.ToInt32(Page.RouteData.Values["id"]);
            GetAdvertentie(id);
            advertentieId = id;
        }
        #endregion

        #region GetAdvertentie
        /// <summary>
        /// This method loads all the data that needs to be displayed from the database.
        /// </summary>
        /// <param name="id">the id of the advert</param>
        public void GetAdvertentie(int id)
        {
            try
            {
                //Creates datasets
                DataSet output = new DataSet();
                DataSet output2 = new DataSet();
                DataSet output3 = new DataSet();

                //Gets information from databse
                Administratie administratie = new Administratie();
                output = administratie.GetData("SELECT ADVERTENTIEID, PRIJS, PERSOONID, AFMETING, GEWICHT, ZENDPRIJS, TITEL, CONTACTNAAM, CONTACTPOSTCODE, CONTACTTELEFOON, AANTALBEZOCHT, AANTALFAVORIET, PLAATSINGSDATUM, CONDITIE, MERK, BESCHRIJVING, FOTO, WEBSITE" +
                                               " FROM ADVERTENTIE" +
                                               " WHERE ADVERTENTIEID = " + id);

                //If the advert doesn't exist, the user gets redirected to the index page
                if (output == null)
                {
                    Response.Redirect("Index.aspx.aspx", true);
                }

                int advertentieIdAdv = Convert.ToInt32(output.Tables[0].Rows[0]["ADVERTENTIEID"]);
                string titelAdv = Convert.ToString(output.Tables[0].Rows[0]["TITEL"]);

                minimaalBod = Convert.ToInt32(output.Tables[0].Rows[0]["PRIJS"]);

                //Checks what kind of advert it is.
                output2 = administratie.GetData("SELECT LEVEREN, OPHALEN, BIEDPRIJS"+
                                                " FROM ADVERTENTIE" +
                                                " WHERE ADVERTENTIEID = " + id);

                
                int ophalen = Convert.ToInt32(output2.Tables[0].Rows[0]["OPHALEN"]);
                int leveren = Convert.ToInt32(output2.Tables[0].Rows[0]["LEVEREN"]);
                int biedPrijs = Convert.ToInt32(output2.Tables[0].Rows[0]["BIEDPRIJS"]);

                //Gets the bids for the advert, ordered from high to low.
                output3 =
                    administratie.GetData(
                        "SELECT p.Naam AS NAAM, p.Email AS EMAIL, b.Bedrag AS BEDRAG, b.Datum AS DATUM" +
                        " FROM PERSOON p" +
                        " JOIN Bod b ON p.PERSOONID = b.PERSOONID" +
                        " WHERE b.AdvertentieId =" + id +
                        " ORDER BY b.Bedrag DESC");
                
                //Binds the data to the repeater
                RepeaterAdvertentie.DataSource = output;
                RepeaterAdvertentie.DataBind();

                //Binds the data to the repeater
                RepeaterBod.DataSource = output3;
                RepeaterBod.DataBind();

                //Sets hoogstebod
                hoogsteBod = Convert.ToInt32(output3.Tables[0].Rows[0]["BEDRAG"]);

                //Sets the advert type.
                if (ophalen == 1 && leveren == 1)
                {
                    lblType.Text = "Beide";

                }
                else if (ophalen == 1 && leveren == 0)
                {
                    lblType.Text = "Ophalen";

                }
                else if (ophalen == 0 && leveren == 1)
                {
                    lblType.Text = "Leveren";
                }

                if (biedPrijs == 1)
                {
                    lblPrijs.Text = "Bieden";
                }
                else
                {
                    lblPrijs.Text = "Vaste Prijs";
                }

                //Creates an instance of advert and adds it to the users recently watched adverts.
                Advertentie advertentie = new Advertentie(advertentieIdAdv, titelAdv);
                gebruiker.BekekenAdvertenties.Add(advertentie);

            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion

        #region btnBieden
        /// <summary>
        /// Places a bid on the advert, a user can only bid when:
        /// 1. He's logged in.
        /// 2. The bid is higher than the current highest bid.
        /// 3. The bid is higher than the lowest bid possible, set by the creator of the advert.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBieden_Click(object sender, EventArgs e)
        {
            if (gebruiker == null)
            {
                lblMessageBod.Text = "Log in om een bod te plaatsen";
                lblMessageBod.CssClass = "highlight";
            }
            else
            {
                int bod = Convert.ToInt32(tbBieden.Text);

                if (bod < minimaalBod || bod < hoogsteBod)
                {
                    lblMessageBod.Text = "Bod is te laag!";
                    lblMessageBod.CssClass = "highlight";
                }

                else
                {
                    Administratie administratie = new Administratie();
                    DateTime date = DateTime.Now;
                    string datum = Convert.ToString(date, CultureInfo.InvariantCulture);

                    administratie.InsertData("INSERT INTO BOD (NULL, ADVERTENTIEID, EMAIL, BEDRAG, DATUM) VALUES (" +
                                             "NULL" + "," + advertentieId + ", " + gebruiker.Email + ", " + bod + ", " +
                                             "'" + datum + "'" + ")");
                }
            }
        }
        #endregion

        #region btnVerwijderen
        /// <summary>
        /// This method deletes the advert being viewed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnVerwijderen_Click(object sender, EventArgs e)
        {
            Database database = Database.Instance;
            database.DeleteAdvertentie(advertentieId);
        }
        #endregion
    }
}