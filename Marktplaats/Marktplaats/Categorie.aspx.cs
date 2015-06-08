using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marktplaats
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Rout();
        }

        public void Rout()
        {
            string categorieUrl = (string)Page.RouteData.Values["Categorie"];
            string id = (string)Page.RouteData.Values["id"];

            GetSubCategories(id);
        }

        public void GetSubCategories(string id)
        {
            try
            {
                DataSet output = new DataSet();
                Administratie administratie = new Administratie();
                output = administratie.GetData("SELECT GROEPNAAM, GROEPID FROM GROEP WHERE PARENTGROEPID = " + "'" + id + "'");

               RepeaterSubCategorie.DataSource = output;
               RepeaterSubCategorie.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
    }
}