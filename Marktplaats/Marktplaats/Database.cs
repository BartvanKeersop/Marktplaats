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

        private Database()
        {
            
        }


        private void OpenConnection()
        {

            //Dataconnection
            connection.ConnectionString = "User Id=MARKTPLAATS;Password=MARKTPLAATS;Data Source=localhost/XE";

            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CloseConnection()
        {
            connection.Close();
        }

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

        public List<Dictionary<string, object>> GetSubCategories(int parentCategorieId)
        {
            OracleCommand oc = new OracleCommand("SELECT GROEPNAAM, GROEPID FROM CATEGORIE WHERE PARENTGROEPID = :parentCategorieId");

            oc.Parameters.Add("parentCategorieId", parentCategorieId);

            return ExecuteQuery(oc);
        }

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
            oc.Parameters.Add("persoonId", persoonId;
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

        public List<Dictionary<string, object>> GetGroupIdWithAdvertentieId(int advId)
        {
            OracleCommand oc = new OracleCommand("SELECT GROEPID FROM ADVERTENTIE WHERE ADVERTENTIEID = :advId");

            oc.Parameters.Add("groepId", advId);

            return ExecuteQuery(oc);
        }

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

        public bool Execute(OracleCommand cmd)
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
    }
}