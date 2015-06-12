using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marktplaats
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        private Gebruiker gebruiker;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Updates navigation.

            GetCategories();

            CheckIfLoggedIn();

            BindCategoriesToList();
        }

        public void BindCategoriesToList()
        {
            DataSet output = new DataSet();
            Administratie administratie = new Administratie();
            output = administratie.GetData("SELECT GROEPNAAM, GROEPID FROM GROEP");

            ddlCategorie.DataSource = output;
            ddlCategorie.DataTextField = "GROEPNAAM";
            ddlCategorie.DataValueField = "GROEPID";
            ddlCategorie.DataBind();
            ddlCategorie.Items.Insert(0, "Alle");
            ddlCategorie.SelectedIndex = 0;
        }

        /// <summary>
        /// This method checks if there is a Gebruiker stored in the session.
        /// If this is the case the user is logged in and gets the option to log out.
        /// If not the case the user can log in if he or she wants.
        /// </summary>
        public void CheckIfLoggedIn()
        {
            //Checks if there is a user stored in session.
            if (Session["gebruiker"] != null)
            {
                //Change the login functions to logout functions.
                gebruiker = (Gebruiker)Session["gebruiker"];

                lblEmail.Visible = false;
                lblWachtwoord.Visible = false;
                tbEmail.Visible = false;
                tbWachtwoord.Visible = false;
                lblWelkom.Visible = true;
                lblWelkom.Text = "Welkom, ";
                hpNaam.Visible = true;

                btnUitloggen.Visible = true;
                btnInloggen.Visible = false;
                hpNaam.Text = gebruiker.Naam;
                hpNaam.NavigateUrl = "Instellingen/" + gebruiker.GebruikerId;

                LoadAanbevolenAdvertenties();

                //Shows the control to the adminpanel when user is an admin.
                if (gebruiker.AdminRechten)
                {
                    hpAdminpaneel.Visible = true;
                }
            }
            else
            {
                //Change the logout functions to login functions.
                lblEmail.Visible = true;
                lblWachtwoord.Visible = true;
                tbEmail.Visible = true;
                tbWachtwoord.Visible = true;

                lblWelkom.Visible = false;

                hpNaam.Visible = false;
                btnUitloggen.Visible = false;
                btnInloggen.Visible = true;
            }
        }

        public void LoadAanbevolenAdvertenties()
        {
                gebruiker.GenereerAanbevolenAdvertenties(gebruiker);

            int caseSwitch = gebruiker.AangeradenAdvertenties.Count;
            switch (caseSwitch)
            {
                case 0:
                    FotoAdv1.Visible = false;
                    TitelAdv1.Visible = false;
                    NaamAdv1.Visible = false;

                    FotoAdv2.Visible = false;
                    TitelAdv2.Visible = false;
                    NaamAdv2.Visible = false;

                    FotoAdv3.Visible = false;
                    TitelAdv3.Visible = false;
                    NaamAdv3.Visible = false;
                    break;
                case 1:
                    FotoAdv1.ImageUrl = gebruiker.AangeradenAdvertenties[0].Foto;
                    TitelAdv1.Text = gebruiker.AangeradenAdvertenties[0].Titel;
                    TitelAdv1.ImageUrl = "Advertentie/" + gebruiker.AangeradenAdvertenties[0].Advertentienummer;
                    NaamAdv1.Text = gebruiker.AangeradenAdvertenties[0].Aanbiederpersoon.Naam;
                    FotoAdv1.Visible = true;
                    TitelAdv1.Visible = true;
                    NaamAdv1.Visible = true;

                    FotoAdv2.Visible = false;
                    TitelAdv2.Visible = false;
                    NaamAdv2.Visible = false;

                    FotoAdv3.Visible = false;
                    TitelAdv3.Visible = false;
                    NaamAdv3.Visible = false;
                    break;
                case 2:
                    FotoAdv1.ImageUrl = gebruiker.AangeradenAdvertenties[0].Foto;
                    TitelAdv1.Text = gebruiker.AangeradenAdvertenties[0].Titel;
                    TitelAdv1.ImageUrl = "Advertentie/" + gebruiker.AangeradenAdvertenties[0].Advertentienummer;
                    NaamAdv1.Text = gebruiker.AangeradenAdvertenties[0].Aanbiederpersoon.Naam;
                    FotoAdv1.Visible = true;
                    TitelAdv1.Visible = true;
                    NaamAdv1.Visible = true;

                    FotoAdv2.ImageUrl = gebruiker.AangeradenAdvertenties[1].Foto;
                    TitelAdv2.Text = gebruiker.AangeradenAdvertenties[1].Titel;
                    TitelAdv2.ImageUrl = "Advertentie/" + gebruiker.AangeradenAdvertenties[1].Advertentienummer;
                    NaamAdv2.Text = gebruiker.AangeradenAdvertenties[1].Aanbiederpersoon.Naam;
                    FotoAdv2.Visible = true;
                    TitelAdv2.Visible = true;
                    NaamAdv2.Visible = true;

                    FotoAdv3.Visible = false;
                    TitelAdv3.Visible = false;
                    NaamAdv3.Visible = false;
                    break;
                case 3:
                    FotoAdv1.ImageUrl = gebruiker.AangeradenAdvertenties[0].Foto;
                    TitelAdv1.Text = gebruiker.AangeradenAdvertenties[0].Titel;
                    TitelAdv1.ImageUrl = "Advertentie/" + gebruiker.AangeradenAdvertenties[0].Advertentienummer;
                    NaamAdv1.Text = gebruiker.AangeradenAdvertenties[0].Aanbiederpersoon.Naam;
                    FotoAdv1.Visible = true;
                    TitelAdv1.Visible = true;
                    NaamAdv1.Visible = true;

                    FotoAdv2.ImageUrl = gebruiker.AangeradenAdvertenties[1].Foto;
                    TitelAdv2.Text = gebruiker.AangeradenAdvertenties[1].Titel;
                    TitelAdv2.ImageUrl = "Advertentie/" + gebruiker.AangeradenAdvertenties[1].Advertentienummer;
                    NaamAdv2.Text = gebruiker.AangeradenAdvertenties[1].Aanbiederpersoon.Naam;
                    FotoAdv2.Visible = true;
                    TitelAdv2.Visible = true;
                    NaamAdv2.Visible = true;

                    FotoAdv3.ImageUrl = gebruiker.AangeradenAdvertenties[2].Foto;
                    TitelAdv3.Text = gebruiker.AangeradenAdvertenties[2].Titel;
                    TitelAdv3.ImageUrl = "Advertentie/" + gebruiker.AangeradenAdvertenties[2].Advertentienummer;
                    NaamAdv3.Text = gebruiker.AangeradenAdvertenties[2].Aanbiederpersoon.Naam;
                    FotoAdv3.Visible = true;
                    TitelAdv3.Visible = true;
                    NaamAdv3.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// This method checks wheter the given dataset is empty or not.
        /// Returns true when empty, false when not.
        /// </summary>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        bool IsEmpty(DataSet dataSet)
        {
            return dataSet.Tables.Cast<DataTable>().All(table => table.Rows.Count == 0);
        }

        /// <summary>
        /// This method gets all the main categories that do not have a parentgroepid,
        /// and shows them in the navigation on the left side of the webpage.
        /// </summary>
         public void GetCategories()
        {
            try
            {
                DataSet output = new DataSet();
                Administratie administratie = new Administratie();
                output = administratie.GetData("SELECT GROEPNAAM, GROEPID FROM GROEP WHERE PARENTGROEPID IS NULL");

                Repeater1.DataSource = output;
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// This method checks if the username and password exist and match within the oracle database.
        /// When it does, it gets the email, username and rights from the database and creates a user.
        /// The user is then stored within the session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         protected void btnInloggen_Click(object sender, EventArgs e)
         {
             string email = tbEmail.Text;
             string password = tbWachtwoord.Text;

             //Gets information from the database.
             DataSet output = new DataSet();
             Administratie administratie = new Administratie();
             output = administratie.GetData("SELECT PERSOONID, EMAIL, NAAM, ADMINRECHTEN FROM PERSOON WHERE EMAIL = " + "'" + email + "'" + " AND WACHTWOORD = " +
                                  "'" + password + "'");

             //Checks if data is returned.
             if (IsEmpty(output))
             {
                 //Wrong id and pasword combination!
                 lblWelkom.Visible = true;
                 lblWelkom.Text = "Foute gebruikersnaam en wachtwoord combinatie!";
                 lblWelkom.CssClass = "highlight";
             }
             else
             {
                 //Create user and store in session.
                 int persoonid = Convert.ToInt32(output.Tables[0].Rows[0]["PERSOONID"].ToString());
                 string naam = output.Tables[0].Rows[0]["NAAM"].ToString();
                 int adminRechtenInt = Convert.ToInt32(output.Tables[0].Rows[0]["ADMINRECHTEN"].ToString());
                 bool adminRechten = false;

                 if (adminRechtenInt == 1)
                 {
                     adminRechten = true;
                 }

                 //Create new user.
                 Gebruiker gebruiker = new Gebruiker(persoonid, naam, adminRechten);
                 Session["gebruiker"] = gebruiker;

                 CheckIfLoggedIn();
             }

         }

        /// <summary>
        /// Logsout the user by setting the gebruiker session to NULL, and refreshes the page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         protected void btnUitloggen_Click(object sender, EventArgs e)
        {
            //Set session to null.
            Session["gebruiker"] = null;
            CheckIfLoggedIn();
        }
    }
}