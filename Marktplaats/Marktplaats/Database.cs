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
        private OracleConnection connection = new OracleConnection();

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
    }
}