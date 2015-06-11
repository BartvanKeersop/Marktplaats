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
    public partial class AdvertentieAanmaken : System.Web.UI.Page
    {
        //Fields
        private Gebruiker gebruiker;

        protected void Page_Load(object sender, EventArgs e)
        {
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
                    ChangeSubcategorieDdl(groepId);
                }
            }
        }

        public void CheckIfLoggedIn()
        {
            if (Session["gebruiker"] == null)
            {
                lblWaarschuwing.Text = "Let op, u moet ingelogd zijn om een advertentie te kunnen plaatsen!";
                lblWaarschuwing.Visible = true;
            }
            else lblWaarschuwing.Visible = false;
        }

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
        }

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

        protected void btnPlaatsAdvertentie_Click(object sender, EventArgs e)
        {
            if (Session["gebruiker"] != null)
            {
                string imagepath = UploadAfbeelding();

                int number;

                if (Int32.TryParse(ddlSubCategorie.SelectedValue, out number) && tbTitel.Text != null &&
                    ddlSubCategorie.SelectedValue != null && tbNaam != null && tbPostcode != null)
                {
                    Administratie administratie = Administratie.Instance;
                    administratie.
                }

                lblMeldingen.Text = "Vul alle verplichte velden in";
            }
        }

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
                            DataSet output = administratie.GetData("SELECT MAX(ADVERTENTIEID) FROM ADVERTENTIE");
                            string imagename = "Image" + output.Tables[0].Rows[0]["PERSOONID"];

                            string filename = Path.GetFileName(fuFoto.FileName);
                            string savepath = "@Uploads/" + filename;

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

        protected void ddlCategorie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategorie.SelectedIndex != 0)
            {
                Session["ddlIndex"] = ddlCategorie.SelectedIndex;
            }
        }
    }
}