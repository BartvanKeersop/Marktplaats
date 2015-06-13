using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marktplaats
{
    /// <summary>
    /// This page contains all the code for AdvertentieAanmaken page.
    /// Users can create an add this page by filling in the coresponding parameters.
    /// </summary>
    public partial class AdvertentieAanmaken : System.Web.UI.Page
    {
        #region Fields
        private Gebruiker gebruiker;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            gebruiker = (Gebruiker) Session["gebruiker"];
            CheckIfLoggedIn();

            if (!IsPostBack)
            {
                DataBind();
            }
            else
            {
                if(ddlCategorie.SelectedIndex != 0)
                {
                    int groepId = Convert.ToInt32(ddlCategorie.SelectedValue);

                    if (ddlSubCategorie.SelectedIndex == 0)
                    {
                        ChangeSubcategorieDdl(groepId);
                    }
                }
            }
        }

        #region Methods
        /// <summary>
        /// Checks if the user is logged in. If not, a warning message appears.
        /// </summary>
        public void CheckIfLoggedIn()
        {
            if (Session["gebruiker"] == null)
            {
                lblWaarschuwing.Text = "Let op, u moet ingelogd zijn om een advertentie te kunnen plaatsen!";
                lblWaarschuwing.Visible = true;
            }
            else lblWaarschuwing.Visible = false;
        }

        /// <summary>
        /// Binds the data to the dropdownlists
        /// </summary>
        public void DataBind()
        {
            Administratie administratie = Administratie.Instance;
            DataSet output = administratie.GetData("SELECT GROEPNAAM, GROEPID FROM GROEP WHERE PARENTGROEPID IS NULL");
            ddlCategorie.DataSource = output;
            ddlCategorie.DataTextField = "GROEPNAAM";
            ddlCategorie.DataValueField = "GROEPID";
            ddlCategorie.DataBind();

            ddlCategorie.Items.Insert(0, "Kies een categorie");
            ddlCategorie.SelectedIndex = 0;

            ddlConditie.Items.Insert(0, "Nieuw");
            ddlConditie.Items.Insert(1, "ZGAN");
            ddlConditie.Items.Insert(2, "Gebruikt");
            ddlConditie.SelectedIndex = 2;

            ddlPrijs.Items.Insert(0, "Bieden vanaf");
            ddlPrijs.Items.Insert(1, "Vaste Prijs");
            ddlPrijs.SelectedIndex = 0;

            ddlSubCategorie.Items.Insert(0, "Kies een categorie");
            ddlSubCategorie.SelectedIndex = 0;
        }

        /// <summary>
        /// This method get's called when the CategorieDdl selected index gets changed.
        /// This method changes the content of the SubCategorieDdl.
        /// </summary>
        /// <param name="groepid">The groepId from CategorieDdl</param>
        public void ChangeSubcategorieDdl(int groepid)
        {
            Administratie administratie = Administratie.Instance;
            DataSet output = administratie.GetData("SELECT GROEPNAAM, GROEPID FROM GROEP WHERE PARENTGROEPID = " + groepid);
            ddlSubCategorie.DataSource = output;
            ddlSubCategorie.DataTextField = "GROEPNAAM";
            ddlSubCategorie.DataValueField = "GROEPID";
            ddlSubCategorie.DataBind();
            ddlSubCategorie.Items.Insert(0, "Kies een Subcategorie");
            ddlSubCategorie.SelectedIndex = 0;
        }


        /// <summary>
        /// This method saves the image uploaded by the user onto the server and stores the link into the database.
        /// The path gets returned, if the method fails a link to an errordisplay picture is returned.
        /// </summary>
        /// <returns>string imagepath</returns>
        public string UploadAfbeelding()
        {
            //Deze code heb ik van internet, zelf een klein beetje aangepast!

            if (fuFoto.HasFile)
            {
                try
                {
                    //Checks if file format is correct.
                    if (fuFoto.PostedFile.ContentType == "image/jpeg" || fuFoto.PostedFile.ContentType == "image/png" || fuFoto.PostedFile.ContentType == "image/bmp")
                    {
                        //Checks if the size is less than one megabyte.
                        if (fuFoto.PostedFile.ContentLength < 1024000)
                        {
                            Administratie administratie = Administratie.Instance;
                            DataSet output = administratie.GetData("SELECT MAX(ADVERTENTIEID) AS MAX FROM ADVERTENTIE");
                            string imagename = "Image" + output.Tables[0].Rows[0]["MAX"];

                            string savepath = "Uploads/" + imagename;

                            fuFoto.SaveAs(Server.MapPath(savepath));
                            return savepath;
                        }
                        else
                            lblMeldingen.Text = "De foto is te groot, selecteer een afbeelding van 1 megabyte of minder";
                    }
                    else
                        lblMeldingen.Text = "Alleen JPEG, PNG of BMP bestanden.";
                }
                catch (Exception ex)
                {
                    lblMeldingen.Text = "Upload status: error: " + ex.Message;
                }
            }
            return "@Uploads/Error.png";
        }
        #endregion

        #region Events
        /// <summary>
        /// This event inserts the advert into the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPlaatsAdvertentie_Click(object sender, EventArgs e)
        {
            if (Session["gebruiker"] != null)
            {
                string imagepath = UploadAfbeelding();

                int number;

                if (Int32.TryParse(ddlSubCategorie.SelectedValue, out number) && tbTitel.Text != null &&
                    ddlSubCategorie.SelectedValue != null)
                {
                    Database database = Database.Instance;

                    int prijs = Convert.ToInt32(tbPrijs.Text);
                    int categorieId = number;
                    string titel = tbTitel.Text;
                    string conditie = ddlConditie.SelectedValue;
                    string merk = tbMerk.Text;
                    string naam = gebruiker.Naam;
                    int persoonId = gebruiker.GebruikerId;
                    string beschrijving = tbBeschrijving.Text;

                    //database.InsertAdvertentie(prijs, categorieId, titel, conditie, merk, afmetingen, gewicht, imagepath, naam, postcode, telnr, website, persoonId, beschrijving);
                }
                lblMeldingen.Text = "Vul alle verplichte velden in";
                lblMeldingen.Visible = true;
            }
        }

        /// <summary>
        /// This event fires when the index of the CategorieDdl is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCategorie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategorie.SelectedIndex != 0)
            {
                Session["ddlIndex"] = ddlCategorie.SelectedIndex;
            }
        }

        #endregion
    }
}