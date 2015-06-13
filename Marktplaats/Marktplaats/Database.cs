using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Oracle.DataAccess;

namespace Marktplaats
{
    /// <summary>
    /// This class contains all the methods used to get or set information from the database.
    /// This class was constructed with a little help from Cas Eliens (how to use parameters).
    /// </summary>
    public class Database
    {
        #region Fields
        private OracleConnection connection = new OracleConnection();
        //Lazy instance of database, code example from cas eliens.
        private static readonly Lazy<Database> instance = new Lazy<Database>(() => new Database());
        #endregion

        #region Properties
        public static Database Instance { get { return instance.Value; } }
        #endregion

        #region Constructor
        private Database()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Opens the connection to the oracle database.
        /// </summary>
        private void OpenConnection()
        {
            connection.ConnectionString = "User Id=MARKTPLAATS;Password=MARKTPLAATS;Data Source=localhost/XE";

            try
            {
                connection.Open();
            }
            catch (OracleException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + ex.ErrorCode);
            }
        }

        private void CloseConnection()
        {
            connection.Close();
        }

        /// <summary>
        /// This method gets a dataset to bind to a repeater
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataSet GetData(string query)
        {
            try
            {
                OpenConnection();
                OracleDataAdapter o_adapter = new OracleDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                o_adapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void InsertData(string query)
        {
            try
            {
                OpenConnection();
                OracleDataAdapter o_adapter = new OracleDataAdapter(query, connection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// gets the subcategories of the selected category
        /// </summary>
        /// <param name="parentCategorieId">the categorieId of selected category</param>
        /// <returns>subcategorieId and Name</returns>
        public List<Dictionary<string, object>> GetSubCategories(int parentCategorieId)
        {
            OracleCommand oc = new OracleCommand("SELECT GROEPNAAM, GROEPID FROM CATEGORIE WHERE PARENTGROEPID = :parentCategorieId");

            oc.Parameters.Add("parentCategorieId", parentCategorieId);

            return ExecuteQuery(oc);
        }

        /// <summary>
        /// Inserts an advert
        /// </summary>
        /// <param name="prijs"></param>
        /// <param name="categorieId"></param>
        /// <param name="titel"></param>
        /// <param name="conditie"></param>
        /// <param name="merk"></param>
        /// <param name="afmetingen"></param>
        /// <param name="gewicht"></param>
        /// <param name="foto"></param>
        /// <param name="naam"></param>
        /// <param name="postcode"></param>
        /// <param name="telnr"></param>
        /// <param name="website"></param>
        /// <param name="persoonId"></param>
        /// <param name="beschrijving"></param>
        public void InsertAdvertentie(int prijs, int categorieId, string titel, string conditie, string merk, string afmetingen, int gewicht, string foto, string naam, string postcode, string telnr, string website, int persoonId, string beschrijving)
        {
            DateTime datum = DateTime.Now;

            OracleCommand oc =
                new OracleCommand(
                    "INSERT INTO Advertentie (Prijs, AdvertentieID, Foto, GroepID, PersoonID, Leveren, Ophalen, Afmeting, Gewicht, Zendprijs, Titel, Contactnaam, Contactpostcode, Contacttelefoon, Aantalbezocht, Aantalfavoriet, Plaatsingsdatum, Conditie, Merk, Beschrijving, Website, Vasteprijs, Biedprijs)" +
                    "VALUES (:prijs, NULL, :foto, :categorieId, :persoonId, 1, 1, :gewicht, :afmetingen, 0, :titel, :naam, :postcode, :telnr, 0, 0, :datum, :conditie, :merk, :beschrijving, :website, 0, 1");
 
            oc.Parameters.Add("prijs", prijs);
            oc.Parameters.Add("foto", foto);
            oc.Parameters.Add("categorieId", categorieId);
            oc.Parameters.Add("persoonId", persoonId);
            oc.Parameters.Add("gewicht", gewicht);
            oc.Parameters.Add("afmetingen", afmetingen);
            oc.Parameters.Add("titel", titel);
            oc.Parameters.Add("naam", naam);
            oc.Parameters.Add("postcode", postcode);
            oc.Parameters.Add("telnr", telnr);
            oc.Parameters.Add("datum", datum);
            oc.Parameters.Add("conditie", conditie);
            oc.Parameters.Add(":merk", merk);
            oc.Parameters.Add("beschrijving", beschrijving);
            oc.Parameters.Add("website", website);

           Execute(oc);
        }

        /// <summary>
        /// Inserts a placed bod into the database.
        /// </summary>
        public void InsertBod(int advertentieId, int persoonId, int bedrag)
        {
            OracleCommand oc =
                new OracleCommand(
                    "INSERT INTO BOD (BODID, ADVERTENTIEID, EMAIL, BEDRAG) VALUES (NULL, :advertentieId, :persoonId");

            oc.Parameters.Add(new OracleParameter("advertentieId", OracleDbType.Varchar2, advertentieId, ParameterDirection.Input));
            oc.Parameters.Add(new OracleParameter("persoonId", OracleDbType.Varchar2, persoonId, ParameterDirection.Input));
            oc.Parameters.Add(new OracleParameter("bedrag", OracleDbType.Int32, bedrag, ParameterDirection.Input));

            Execute(oc);
        }

        /// <summary>
        /// Deletes selected advert
        /// </summary>
        /// <param name="advertentieId">selected advertId</param>
        public void DeleteAdvertentie(int advertentieId)
        {
            OracleCommand oc =
                    new OracleCommand("DELETE FROM advertentie WHERE advertentieID = :advertentieId");

            oc.Parameters.Add("advertentieId", advertentieId);

            ExecuteQuery(oc);
        }

        /// <summary>
        /// Gets the creator of selected advert
        /// </summary>
        /// <param name="advId">id of the selected advert</param>
        /// <returns>persoonId of the creator</returns>
        public List<Dictionary<string, object>> GetGebruikerIdWithAdvId(int advId)
        {
            OracleCommand oc =
                new OracleCommand("SELECT PERSOONID FROM ADVERTENTIE WHERE ADVERTENTIEID = :advId");

            oc.Parameters.Add("advId", advId);

            return ExecuteQuery(oc);
        }

        public List<Dictionary<string, object>> GetBoden(int advId)
        {
            OracleCommand oc =
                new OracleCommand("SELECT p.Naam AS NAAM, p.Email AS EMAIL, b.Bedrag AS BEDRAG, b.Datum AS DATUM" +
                        " FROM PERSOON p" +
                        " JOIN Bod b ON p.PERSOONID = b.PERSOONID" +
                        " WHERE b.AdvertentieId =  :advId" +
                        " ORDER BY b.Bedrag DESC");

            oc.Parameters.Add("advId", advId);

            return ExecuteQuery(oc);
        }

        /// <summary>
        /// Gets all the advert info.
        /// </summary>
        /// <param name="advId">the id of the advert</param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetAdvertentieInfo(int advId)
        {
            OracleCommand oc =
            new OracleCommand(
                "SELECT PersoonID, Prijs, Leveren, Ophalen, Titel, Conditie, Merk, Beschrijving, Foto, Vasteprijs, Biedprijs FROM Advertentie WHERE ADVERTENTIEID = :advId");

            oc.Parameters.Add("advId", advId);

            return ExecuteQuery(oc);
        }

        /// <summary>
        /// This method returns a random recommended advert.
        /// </summary>
        /// <param name="groepId">GroupId of viewed advert</param>
        /// <returns>advert information</returns>
        public List<Dictionary<string, object>> GetAanbevolenAdvDataRandom(int groepId)
        {
            OracleCommand oc =
                new OracleCommand(
                    "SELECT * FROM (" +
                        "SELECT a.Titel, a.AdvertentieId, a.Foto, p.Naam, p.PersoonId FROM Advertentie a " +
                        "JOIN Persoon p on a.PersoonId = p.PersoonId WHERE GROEPID = :groepid " +
                        "ORDER BY dbms_random.value)a " +
                    "WHERE ROWNUM = 1");

            oc.Parameters.Add("groepId", groepId);

            return ExecuteQuery(oc);
        }

        /// <summary>
        /// Gets a group from selected advert
        /// </summary>
        /// <param name="advId">the id of the selected advert</param>
        /// <returns>the categorie of selected advert</returns>
        public List<Dictionary<string, object>> GetGroupIdWithAdvertentieId(int advId)
        {
            OracleCommand oc = new OracleCommand("SELECT GROEPID FROM ADVERTENTIE WHERE ADVERTENTIEID = :advId");

            oc.Parameters.Add("groepId", advId);

            return ExecuteQuery(oc);
        }

        /// <summary>
        /// Executes a Query that returns a dictonairy list
        /// </summary>
        /// <param name="oc"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> ExecuteQuery(OracleCommand oc)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

            try
            {
                OpenConnection();
                oc.Connection = connection;
                using (OracleDataReader reader = oc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, object> row = new Dictionary<string, object>();

                        for (int fieldId = 0; fieldId < reader.FieldCount; fieldId++)
                        {
                            row.Add(reader.GetName(fieldId), reader.GetValue(fieldId));
                        }
                        result.Add(row);
                    }
                    return result;
                }
            }
            catch (OracleException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ErrorCode + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return null;
        }

        /// <summary>
        /// Executes a query without a return value
        /// </summary>
        /// <param name="cmd"></param>
        public void Execute(OracleCommand cmd)
        {
            try
            {
                OpenConnection();
                cmd.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message + ex.ErrorCode);
            }
            finally
            {
                CloseConnection();
            }
        }

        #endregion
    }
}