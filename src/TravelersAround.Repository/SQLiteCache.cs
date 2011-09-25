using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Reflection;

namespace TravelersAround.Model
{
    public class SQLiteCache : ICache
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private readonly string DATABASE_FILE = Path.Combine(Path.GetDirectoryName(new System.Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), "cache.db");
        
        //Expiration time in minutes
        private readonly int _idleAfter;
        private readonly int _idleUsersCleanUpTime;

        public int IdleTime
        {
            get { return _idleAfter; }
        }

        public int IdleUsersCleanUpTime
        {
            get { return _idleUsersCleanUpTime; }
        }
            
        public SQLiteCache(int idleTime, int idleUsersCleanUpTime)
        {
            _idleAfter = idleTime;
            _idleUsersCleanUpTime = idleUsersCleanUpTime;
        }

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
            SQLiteCommand command = new SQLiteCommand(String.Format(@"CREATE TABLE [cache] (
                                                                    [key] varchar(255)  PRIMARY KEY NOT NULL,
                                                                    [value] varchar(255)  NOT NULL,
                                                                    [expiration] datetime  NOT NULL,
                                                                    [online] BOOLEAN  NOT NULL
                                                                    );"), connection);
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
                SQLiteCommand command = new SQLiteCommand(
                    String.Format("insert into cache values('{0}','{1}','{2}', 1);", key, value, DateTime.Now.AddMinutes(_idleAfter).ToString(DATE_FORMAT)), 
                    connection);
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


        public int SetOnline(string key)
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(
                    String.Format("update cache set expiration = '{0}', online = 1 where key = '{1}';", DateTime.Now.AddMinutes(_idleAfter).ToString(DATE_FORMAT), key), 
                    connection);
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected;
            }
        }

        /// <summary>
        /// Gets all the records that haven't expired yet
        /// </summary>
        /// <returns>List of OnlineUser</returns>
        public IEnumerable<OnlineUser> GetAll()
        {
            DataTable table = new DataTable();
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(String.Format("select * from cache where expiration >= '{0}';", DateTime.Now.ToString(DATE_FORMAT)), connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    table.Load(reader);
                    List<OnlineUser> result = new List<OnlineUser>();
                    if (table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
			            {
                            result.Add(new OnlineUser
                            {
                                Key = (string)table.Rows[i].ItemArray[0],
                                Value = (string)table.Rows[i].ItemArray[1],
                                Expiration = DateTime.Parse((string)table.Rows[i].ItemArray[2]),
                                IsOnline = (bool)table.Rows[i].ItemArray[3],
                            });

			            }
                    }
                    reader.Close();
                    connection.Close();
                    return result;
                }
            }
        }

        /// <summary>
        /// Removes all records which expired X hours ago, as defined in configuration file
        /// </summary>
        /// <returns>Number of rows affected</returns>
        public int RemoveExpired()
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(String.Format("delete from cache where expiration < '{0}';", DateTime.Now.AddHours(-_idleUsersCleanUpTime).ToString(DATE_FORMAT)), connection);
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected;
            }
        }

        public int SetIdleUserOffline()
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(
                    String.Format("update cache set online = 0 where expiration < '{0}';", DateTime.Now.ToString(DATE_FORMAT)),
                    connection);
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected;
            }
        }
    }
}
