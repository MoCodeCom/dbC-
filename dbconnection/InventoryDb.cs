using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace dbconnection
{
    public class InventoryDb
    {
        DatabaseStrConnection configStr = new DatabaseStrConnection();
        SQLiteConnection conn = new SQLiteConnection();
        
        public List<DataInventory> GetAllInventory()
        {
            
            string selectAllComm = "Select * From Inventory;";
            List<DataInventory> inventory = new List<DataInventory>();
            using (SQLiteConnection conn = new SQLiteConnection())
            {
                conn.ConnectionString = configStr.providor_1();
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                
                SQLiteCommand comm = new SQLiteCommand(selectAllComm, conn);
                using (SQLiteDataReader reader = comm.ExecuteReader())
                {
                    while (reader.Read()) 
                    {
                        Console.WriteLine(reader["id"].ToString() +" "+ reader["Item"] + reader["CodeItem"]) ;
                        
                        inventory.Add(
                            new DataInventory
                            {
                                id = Convert.ToInt32(reader["id"]),
                                item = reader["Item"].ToString(),
                                itemCode = Convert.ToInt32(reader["CodeItem"])
                            }
                        );
                    }
                }

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

                return inventory;
        }
        public void InsertInventory(DataInventory itemDb)
        {
            string InsertComm = "Insert Into Inventory (Item, CodeItem) Values " + $"('{itemDb.item}','{itemDb.itemCode}');";
            
            //SQLiteTransaction sqlTeans = null;
            
            //Using Transaction as TransactionScope .......
            using (TransactionScope transScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                try
                {
                    conn.ConnectionString = configStr.providor_1();
                    
                    conn.Open();

                    using (SQLiteCommand comm = new SQLiteCommand(InsertComm, conn))
                    {
                        comm.CommandType = CommandType.Text;
                        comm.ExecuteNonQueryAsync();

                    }
                    /*
                    for (int x=0;x<10000 ;x++)
                    {
                        using (SQLiteCommand comm = new SQLiteCommand(InsertComm, conn))
                        {
                            comm.CommandType = CommandType.Text;
                            comm.ExecuteNonQueryAsync();

                        }
                        
                    }*/

                    transScope.Complete();
                    conn.Close();
                }
                catch
                {
                    Console.WriteLine("Error!");
                    transScope.Dispose();
                }
            }
            
            /*
             //using sqlite transaciton as BeginTransaction...................
            conn.ConnectionString = configStr.providor_1();
            conn.Open();
            sqlTeans = conn.BeginTransaction();
            using (SQLiteCommand comm = new SQLiteCommand(InsertComm,conn))
            {
                
                try
                {
                    comm.CommandType = CommandType.Text;
                    comm.ExecuteNonQueryAsync();
                    sqlTeans.Commit();
                }
                catch(Exception)
                {
                    sqlTeans.Rollback();
                }
                
            }
                conn.Close();
            */
            

        }
    }
}
