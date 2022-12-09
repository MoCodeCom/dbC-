using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dbconnection
{
    internal class DbConnection
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["dataProvidor"].ConnectionString;

        public void connDb()
        {
            var connStringBuilder = new SQLiteConnectionStringBuilder {
                //DataSource = "./sqliteDb.db",
                
                ConnectionString = _connectionString
            };
            using (SQLiteConnection conn = new SQLiteConnection())
            {
                //conn.ConnectionString = _connectionString;
                conn.ConnectionString = connStringBuilder.ConnectionString;
                //connStringBuilder.WaitTimeout = 5000;
                //connStringBuilder.BusyTimeout = 1000;
                //Console.WriteLine(connStringBuilder.BusyTimeout);
                //Console.WriteLine(connStringBuilder.WaitTimeout);
                string sqlSelectInventory = "Select * From Inventory;";
                
                conn.Open();
                
                SQLiteCommand cmd = new SQLiteCommand(sqlSelectInventory, conn);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["Item"]);
                    }

                    Console.WriteLine("/*------------------------------*/");
                    /*
                    Console.WriteLine(reader.Depth);
                    Console.WriteLine(reader.FieldCount);
                    Console.WriteLine(reader.HasRows);
                    Console.WriteLine(reader.IsClosed);
                    Console.WriteLine(reader.StepCount);
                    
                    for (int i=0;i<reader.FieldCount ;i++)
                    {
                        Console.WriteLine(reader.GetValue(i));
                    }*/
                }
                Console.WriteLine("this is connection ..");
                /*
                Thread.Sleep(1000);
                Console.WriteLine(conn.ConnectionTimeout);
                Console.WriteLine(conn.Database);
                Console.WriteLine(conn.DataSource);
                Console.WriteLine(conn.FileName);
                Console.WriteLine(conn.FileName);
                Console.WriteLine(conn.Flags);
                Console.WriteLine(conn.State);*/
                if(conn?.State != ConnectionState.Closed)
                {
                    conn?.Close();
                }
        
            }
        }
    }
}
