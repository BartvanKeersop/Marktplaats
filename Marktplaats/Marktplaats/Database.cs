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
    }
}