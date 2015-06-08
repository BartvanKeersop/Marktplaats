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
            //Checks if the user is logged in.
            CheckIfLoggedIn();
        }

        /// <summary>
        /// This method checks if there is a Gebruiker stored in the viewstate.
        /// If this is the case the user is logged in and gets the option to log out.
        /// If not the case the user can log in if he or she wants.
        /// </summary>
        public void CheckIfLoggedIn()
        {
            //Checks if there is a user stored in viewstate.
            if (ViewState["gebruiker"] != null)
            {
                //Change the login functions to logout functions.
                gebruiker = (Gebruiker)ViewState["gebruiker"];

                lblEmail.Visible = false;
                lblWachtwoord.Visible = false;
                tbEmail.Visible = false;
                tbWachtwoord.Visible = false;

                lblWelkom.Text = "Welkom, ";
                lblWelkom.Visible = true;
                lblNaam.Text = gebruiker.Naam;
                lblNaam.Visible = true;

                btnUitloggen.Visible = true;
            }
            else
            {
                //Change the logout functions to login functions.
                lblEmail.Visible = true;
                lblWachtwoord.Visible = true;
                tbEmail.Visible = true;
                tbWachtwoord.Visible = true;

                lblWelkom.Visible = false;
                lblNaam.Visible = false;

                btnUitloggen.Visible = false;
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
        /// The user is then stored within the viewstate.
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
             output = administratie.GetData("SELECT EMAIL, NAAM, ADMINRECHTEN FROM PERSOON WHERE EMAIL = " + "'" + email + "'" + " AND WACHTWOORD = " +
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
                 //Create user and store in viewstate.
                 string mail = output.Tables[0].Rows[0].ToString();
                 string naam = output.Tables[1].Rows[1].ToString();
                 int adminRechtenInt = Convert.ToInt32(output.Tables[2].Rows[2].ToString());
                 bool adminRechten = false;

                 if (adminRechtenInt == 1)
                 {
                     adminRechten = true;
                 }

                 //Create new user.
                 Gebruiker gebruiker = new Gebruiker(mail, naam, adminRechten);
                 ViewState["gebruiker"] = gebruiker;

                 //Refresh the page.
                 Server.TransferRequest(Request.Url.AbsolutePath, false);
             }

         }

        /// <summary>
        /// Logsout the user by setting the gebruiker viewstate to NULL, and refreshes the page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         protected void btnUitloggen_Click(object sender, EventArgs e)
        {
            //Set viewstate to null.
            ViewState["gebruiker"] = null;

             //Refresh the page.
            Server.TransferRequest(Request.Url.AbsolutePath, false);
        }
    }
}