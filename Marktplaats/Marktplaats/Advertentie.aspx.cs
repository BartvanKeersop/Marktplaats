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
    public partial class Advertentie1 : System.Web.UI.Page
    {
        private Gebruiker gebruiker;
        protected void Page_Load(object sender, EventArgs e)
        {
            gebruiker = (Gebruiker)Session["gebruiker"];
            Rout();
        }

        private int hoogsteBod;
        private int minimaalBod;
        private int advertentieId;

        public void Rout()
        {
            int id = Convert.ToInt32(Page.RouteData.Values["id"]);
            GetAdvertentie(id);
            advertentieId = id;
        }

        public void GetAdvertentie(int id)
        {
            try
            {
                DataSet output = new DataSet();
                DataSet output2 = new DataSet();
                DataSet output3 = new DataSet();

                Administratie administratie = new Administratie();
                output = administratie.GetData("SELECT ADVERTENTIEID, PRIJS, EMAIL, AFMETING, GEWICHT, ZENDPRIJS, TITEL, CONTACTNAAM, CONTACTPOSTCODE, CONTACTTELEFOON, AANTALBEZOCHT, AANTALFAVORIET, PLAATSINGSDATUM, CONDITIE, MERK, BESCHRIJVING, FOTO, WEBSITE" +
                                               " FROM ADVERTENTIE" +
                                               " WHERE ADVERTENTIEID = " + id);

                minimaalBod = Convert.ToInt32(output.Tables[0].Rows[0]["PRIJS"]);

                output2 = administratie.GetData("SELECT LEVEREN, OPHALEN, BIEDPRIJS"+
                                                " FROM ADVERTENTIE" +
                                                " WHERE ADVERTENTIEID = " + id);

                int ophalen = Convert.ToInt32(output2.Tables[0].Rows[0]["OPHALEN"]);
                int leveren = Convert.ToInt32(output2.Tables[0].Rows[0]["LEVEREN"]);
                int biedPrijs = Convert.ToInt32(output2.Tables[0].Rows[0]["BIEDPRIJS"]);

                output3 =
                    administratie.GetData(
                        "SELECT p.Naam AS NAAM, p.Email AS EMAIL, b.Bedrag AS BEDRAG, b.Datum AS DATUM" +
                        " FROM PERSOON p" +
                        " JOIN Bod b ON p.EMAIL = b.EMAIL" +
                        " WHERE b.AdvertentieId =" + id +
                        " ORDER BY b.Bedrag DESC");

                RepeaterAdvertentie.DataSource = output;
                RepeaterAdvertentie.DataBind();

                RepeaterBod.DataSource = output3;
                RepeaterBod.DataBind();

                hoogsteBod = Convert.ToInt32(output3.Tables[0].Rows[0]["BEDRAG"]);

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

            }
            catch (Exception ex)
            {

            }
        }

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
    }
}