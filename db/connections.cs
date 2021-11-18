using System;
using DataConnections;
using MySql.Data.MySqlClient;

namespace Data
{
    public class DBConnection
    {
        private DBConnection()
        {
        }

        string Server = DataConnections.Data.server;
        string DatabaseName = DataConnections.Data.dabaseName;
        string DataSource = DataConnections.Data.source;
        string Port = DataConnections.Data.port;
        string UserName = DataConnections.Data.username;
        string Password = DataConnections.Data.password;

        public MySqlConnection Connection { get; set; }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(DataSource))
                    return false;
                string connstring = string.Format("server={0}; uid={1}; password={2}; database={3}", Server, UserName, Password, DatabaseName);
                Connection = new MySqlConnection(connstring);
                Connection.Open();
            }

            return true;
        }

        public void Close()
        {
            Connection.Close();
        }
    }
}