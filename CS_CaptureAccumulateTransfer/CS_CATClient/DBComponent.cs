using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using System.IO;

namespace CS_CATClient
{

    public class DBComponent
    {
        public SqlCeConnection mySqlConnection;
        private SqlCeCommand mySqlCommand = null;
        private SqlCeDataAdapter mySqlDataAdapter = null;
        private SqlCeEngine mySqlEngine = null;
        public DataSet myDataSet = null;

        private string myDBName;
        // Gets or sets the database name
        public string DBName
        {
            get
            {
                return myDBName;
            }
            set
            {
                myDBName = value;
            }
        }

        private string myDBPassword;
        // Gets or sets the password for database
        public string DBPassword
        {
            get
            {
                return myDBPassword;
            }
            set
            {
                myDBPassword = value;
            }
        }

        private bool myDBEncrypt = true;
        // Enable or disable database encryption
        public bool DBEncrypt
        {
            get
            {
                return myDBEncrypt;
            }
            set
            {
                myDBEncrypt = value;
            }
        }

        // If set to true, delete the the database in DBCreate() if already exists and then create a new one. 
        // If set to false, use the existing one.  
        private bool myDBDelete = false;
        public bool DBDelete
        {
            get
            {
                return myDBDelete;
            }
            set
            {
                myDBDelete = value;
            }
        }

        //Create a databse using the name provided in DBName
        public void DBCreate()
        {
            string connStr = "Data Source='" + myDBName + "'; LCID=1033; Password=\"" + myDBPassword + "\"; Encrypt = ";
            if (myDBEncrypt == true) connStr += "TRUE;";
            else connStr += "FALSE;";

            if (myDBEncrypt == false && File.Exists(myDBName)) return;
            else File.Delete(myDBName);

            try
            {
                mySqlEngine = new SqlCeEngine(connStr);
                mySqlEngine.CreateDatabase();
            }
            catch 
            { 
            
            }
            finally
            {
                mySqlEngine.Dispose();
            }
        }


        // Open the database and get it ready for transactions
        public void DBOpen()
        {
            string connStr = "Data Source='" + myDBName + "'; LCID=1033; Password=\"" + myDBPassword + "\"; Encrypt = ";
            if (myDBEncrypt == true) connStr += "TRUE;";
            else connStr += "FALSE;";

            myDataSet = new DataSet();
            mySqlCommand = new SqlCeCommand();
            mySqlDataAdapter = new SqlCeDataAdapter();

            mySqlConnection = new SqlCeConnection(connStr);
            mySqlConnection.Open();
            mySqlCommand.Connection = mySqlConnection;
        }

        // Provides the state of the connection 
        public ConnectionState DBConnectionState()
        {
            return mySqlConnection.State;
        }

        
        public void DBClose()
        {
            mySqlCommand.Dispose();
            mySqlDataAdapter.Dispose();
            mySqlConnection.Close();
            mySqlConnection.Dispose();
        }

        // Query the database and return the resultset
        public DataSet DBQuery(string queryStr)
        {
            mySqlCommand.CommandText = queryStr;
            myDataSet.Clear();

            mySqlDataAdapter.SelectCommand = mySqlCommand;
            mySqlDataAdapter.Fill(myDataSet);

            return myDataSet;
        }

        // Execute Delete, Insert and Update commands
        public int DBExecute(string executeStr)
        {
            mySqlCommand.CommandText = executeStr;
            int rowsAffected = mySqlCommand.ExecuteNonQuery();

            return rowsAffected;
        }
    }
}
