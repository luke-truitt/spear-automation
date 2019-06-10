using SpearAutomation.Models.Logger.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.Logger.Data
{
    public class SqlHelper
    {
        private string ConnectionString { get; set; }

        public SqlHelper(string connectionStr)
        {
            ConnectionString = connectionStr;
        }

        private bool ExecuteNonQuery(string commandStr, List<System.Data.SQLite.SQLiteParameter> paramList)
        {
            bool result = false;
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection(ConnectionString))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    try
                    {
                        conn.Open();

                    } catch(System.Data.SQLite.SQLiteException e)
                    {
                        Debug.WriteLine(e);
                    }
                }

                using (System.Data.SQLite.SQLiteCommand command = new System.Data.SQLite.SQLiteCommand(commandStr, conn))
                {
                    command.Parameters.AddRange(paramList.ToArray());
                    int count = command.ExecuteNonQuery();
                    result = count > 0;
                }
            }
            return result;
        }

        public bool InsertLog(EventLog log)
        {
            string command = $@"INSERT INTO [EventLog] ([EventID],[LogLevel],[Message],[CreatedTime]) VALUES (@EventID, @LogLevel, @Message, @CreatedTime)";
            List<System.Data.SQLite.SQLiteParameter> paramList = new List<System.Data.SQLite.SQLiteParameter>();
            paramList.Add(new System.Data.SQLite.SQLiteParameter("EventID", log.EventId));
            paramList.Add(new System.Data.SQLite.SQLiteParameter("LogLevel", log.LogLevel));
            paramList.Add(new System.Data.SQLite.SQLiteParameter("Message", log.Message));
            paramList.Add(new System.Data.SQLite.SQLiteParameter("CreatedTime", log.CreatedTime));
            try
            {
                return ExecuteNonQuery(command, paramList);
                //return false;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
        }
    }
}
