using System;
using System.Collections.Generic;
using System.Data;
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

        public void Rout()
        {
            int id = Convert.ToInt32(Page.RouteData.Values["id"]);
            GetAdvertentie(id);
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

                output2 = administratie.GetData("SELECT LEVEREN, OPHALEN, BIEDPRIJS"+
                                                " FROM ADVERTENTIE" +
                                                " WHERE ADVERTENTIEID = " + id);

                output3 = administratie.GetData("SELECT p.Naam, p.Email, b.Bedrag, b.Datum" +
                                                " FROM PERSOON p" +
                                                " JOIN Bod b ON p.EMAIL = b.EMAIL" +
                                                " WHERE b.AdvertentieId =" + id);

                RepeaterAdvertentie.DataSource = output;
                RepeaterAdvertentie.DataBind();

                int ophalen = Convert.ToInt32(output2.Tables[0].Rows[0]["OPHALEN"]);
                int leveren = Convert.ToInt32(output2.Tables[0].Rows[0]["LEVEREN"]);
                int biedPrijs = Convert.ToInt32(output2.Tables[0].Rows[0]["BIEDPRIJS"]);

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
    }
}