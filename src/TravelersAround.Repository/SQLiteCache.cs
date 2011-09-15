using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace TravelersAround.Model
{
    public class SQLiteCache : ICache
    {
        private const string DATABASE_FILE = "cache.db";

        private SQLiteConnection GetConnection()
        {
            if (!File.Exists(DATABASE_FILE))
            {
                return CreateDataBase();
            }
            return new SQLiteConnection(String.Format("Data Source={0}", DATABASE_FILE));
        }

        private SQLiteConnection CreateDataBase()
        {
            SQLiteConnection.CreateFile(DATABASE_FILE);
            SQLiteConnection connection = new SQLiteConnection(String.Format("Data Source={0}", DATABASE_FILE));
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(String.Format("create table cache(key varchar(255) primary key not null, value varchar(255) not null);"), connection);
            command.ExecuteNonQuery();
            connection.Close();
            return connection;
        }

        public object GetValue(string key)
        {
            DataTable table = new DataTable();
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(String.Format("select value from cache where key = '{0}';", key), connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    table.Load(reader);
                    object result = null;
                    if (table.Rows.Count > 0)
                    {
                        result = table.Rows[0].ItemArray[0];
                    }
                    reader.Close();
                    connection.Close();
                    return result;
                }
            }
        }


        public string GetKey(object value)
        {
            DataTable table = new DataTable();
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(String.Format("select key from cache where value = '{0}';", value), connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    table.Load(reader);
                    string result = null;
                    if (table.Rows.Count > 0)
                    {
                        result = (string)table.Rows[0].ItemArray[0];
                    }
                    reader.Close();
                    connection.Close();
                    return result;
                }
            }
        }
        
        public int Add(string key, object value)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(String.Format("insert into cache values('{0}','{1}');", key, value), connection);
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected;
            }
        }
       
        public int Remove(string key)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(String.Format("delete from cache where key = '{0}';", key), connection);
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected;
            }
        }
    }
}
