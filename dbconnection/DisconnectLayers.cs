using Spire.Xls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dbconnection
{
    internal class DisconnectLayers
    {
        public string commStr = "SELECT * FROM Customer;";

        SQLiteConnection conn = new SQLiteConnection(new DatabaseStrConnection().providor_1());

        public void adapTest()
        {
            SQLiteDataAdapter sadapter = new SQLiteDataAdapter(commStr, conn);
            DataSet ds = new DataSet();
            sadapter.Fill(ds, "Customer");
            Console.WriteLine(ds.Tables.Count);
        }

        public void InsertCustomer(string name, int customerId)
        {
            string sqlCommand = "INSERT INTO Customer (Name, CustomerId) VALUES ('" + name + "' , '" + customerId + "')";
            conn.Open();
            SQLiteCommand comm = new SQLiteCommand(sqlCommand, conn);
            comm.ExecuteNonQuery();
            conn.Close();
        }

        public List<DataCustomer> SelectAllCustomer()
        {
            string sqlCommand = "SELECT * FROM Customer;";
            List<DataCustomer> Customerlist = new List<DataCustomer>();
            conn.Open();
            SQLiteCommand comm = new SQLiteCommand(sqlCommand, conn);
            using (SQLiteDataReader reader = comm.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader["Id"].ToString() + " : " + reader["Name"] + " -- " + reader["CustomerId"].ToString());
                    Customerlist.Add(
                        new DataCustomer()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = Convert.ToString(reader["Name"]),
                            CustomerId = Convert.ToInt32(reader["CustomerId"])
                        });
                }
            }
            conn.Close();
            return Customerlist;    
        }

        public DataSet dataset()
        {
            DataSet ds = new DataSet("InventoryDataSet");
            ds.ExtendedProperties["TimeStamp"] = DateTime.Now;
            ds.ExtendedProperties["DataSetID"] = Guid.NewGuid();
            ds.ExtendedProperties["CompanyName"] = "SeedJs";
            return ds;
        }

        public DataTable datatable()
        {
            /*----------------- Data columns --------------*/
            DataColumn dc1 = new DataColumn("Id",typeof(int));
            dc1.AllowDBNull = false;
            dc1.DataType = typeof(int);
            dc1.ColumnName = "Id";
            dc1.AutoIncrement = true;
            dc1.AutoIncrementSeed = 0;
            dc1.AutoIncrementStep = 1;
            DataColumn dc2 = new DataColumn("productsName", typeof(string));
            DataColumn dc3 = new DataColumn("productsAmounts", typeof(int));
            /*------------------- Data Table ---------------*/
            DataTable dtInventory = new DataTable("products");
            dtInventory.Columns.AddRange(new DataColumn[] {dc1, dc2, dc3});

            /*------------------- Data Rows ----------------*/

            for (int n=0;n<30 ;n++)
            {
                string drName = "dr" + n;
                DataRow dataRow = dtInventory.NewRow();
                dtInventory.Rows.Add(dataRow);

                dataRow["productsName"] = "Apple"+ drName;
                dataRow["productsAmounts"] = 50+n;
            }
            /*
            DataRow dr1 = dtInventory.NewRow();
            DataRow dr2 = dtInventory.NewRow();
            dtInventory.Rows.Add(dr1);
            dtInventory.Rows.Add(dr2);
            
            dr1["productsName"] = "Apple";
            dr1["productsAmounts"] = 50;

            dr2["productsName"] = "Orange";
            dr2["productsAmounts"] = 34;*/

            return dtInventory;

        }

        public void FillDataSet(DataSet ds, DataTable dt)
        {
            ds.Tables.Add(dt);
        }

        public void PrintDataSet(DataSet ds)
        {
            Console.WriteLine("Dataset is name:{0}",ds.DataSetName);
            foreach (DictionaryEntry de in ds.ExtendedProperties)
            {
                Console.WriteLine("Key: {0} ---> Vlaue: {1}",de.Key,de.Value);
            }

            foreach (DataTable dt in ds.Tables)
            {
                Console.WriteLine("Data table name:{0} ",dt.TableName);

                for (int i=0;i<dt.Columns.Count ;i++)
                {
                    Console.WriteLine(dt.Columns[i].ColumnName+"\t");
                }

                Console.WriteLine("\n ----------------------------------");

                for (int r=0;r<dt.Rows.Count ;r++)
                {
                    for (int c=0;c<dt.Columns.Count ;c++)
                    {
                        Console.WriteLine(dt.Rows[r][c]+"\t");
                    }
                }
            }
        }
        public void PrintDataTable(DataTable dt)
        {
            using (DataTableReader dtr = new DataTableReader(dt))
            {
                while (dtr.Read())
                {
                    for (int i=0;i<dtr.FieldCount;i++)
                    {
                        Console.WriteLine("{0}\t",dtr.GetValue(i).ToString().Trim());
                    }
                }
            }
        }

        public void SaveDataSetXML(DataSet ds)
        {
            string DirStr = "database";
            string FilStr = "dataset1.xml";
            bool exists = Directory.Exists(DirStr);
            if (exists)
            {
                bool existsData = File.Exists(FilStr);
                if (existsData)
                {
                    Console.WriteLine("data exist aleady?");
                }
                else
                {
                    ds.WriteXml("database/dataset1.xml");
                    //ds.WriteXmlSchema("database/datasetSchema.xsd");
                    Console.WriteLine("data is saved in dataset1 and dataset2 files.");
                }
                
            }
            else
            {
                MyDir();
                ds.WriteXml("database/dataset1.xml");
                //ds.WriteXmlSchema("database/datasetSchema.xsd");
                Console.WriteLine("data is saved in dataset1 and dataset2 files.");
            }
            
            
            ds.Clear();
        }

        public void SaveDataSetBinary(DataSet ds)
        {
            ds.RemotingFormat = SerializationFormat.Binary;
            FileStream fs = new FileStream("database/binaryDataset.bin", FileMode.Create);
            BinaryFormatter bformat = new BinaryFormatter();
            bformat.Serialize(fs, ds);
            ds.Clear();
            Console.WriteLine("data are convert to Binary...");
        }
        public DataSet GetDataFromBinary()
        {
            FileStream fs = new FileStream("database/binaryDataset.bin",FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            DataSet ds = (DataSet)bf.Deserialize(fs);

            return ds;
            
        }

        public DataSet GetDataSetFromXML() 
        {
            DataSet ds = new DataSet();
            ds.ReadXml("database/dataset1.xml");

            return ds;
        }

        public DataSet DataViewDataSet(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "Id = 3";
            DataTable dtview = new DataTable();
            DataSet ds = new DataSet();
            ds.Tables.Add(dv.ToTable());

            return ds;
        }

        public void MyDir()
        {
            DirectoryInfo dr = new DirectoryInfo(".");
            dr.CreateSubdirectory("database");
            Console.WriteLine("Create dir is done!");
            
        }

        public DataSet AdapterDatabase()
        {
            string commStr = "SELECT * FROM Customer;";
            DataSet ds = new DataSet();
            SQLiteConnection conn = new SQLiteConnection(new DatabaseStrConnection().providor_1());
            SQLiteDataAdapter adp = new SQLiteDataAdapter(commStr, new DatabaseStrConnection().providor_1());

            conn.Open();
            adp.Fill(ds);
            conn.Close();

            return ds;
        }
        public DataTable AdapterDataTable()
        {
            string connStr = "SELECT * FROM Customer;";
            DataSet ds = new DataSet();
            SQLiteConnection conn = new SQLiteConnection(new DatabaseStrConnection().providor_1());
            SQLiteDataAdapter adp = new SQLiteDataAdapter(connStr, new DatabaseStrConnection().providor_1());

            conn.Open();
            DataTable dt = new DataTable();
            adp.Fill(dt);
            //Console.WriteLine("Tables numbers are :{0}",ds.Tables.Count);
            /*
            for (int i= 0;i<ds.Tables.Count ;i++)
            {
                Console.WriteLine(ds.Tables[i]);
            }*/
            //dt = ds.Tables[0];
            conn.Close();

            return dt;
        }

        public void SaveDatabaseXls()
        {
            string connStr = "SELECT * FROM Customer;";
            SQLiteConnection conn = new SQLiteConnection(new DatabaseStrConnection().providor_1());
            SQLiteDataAdapter adp = new SQLiteDataAdapter(connStr, new DatabaseStrConnection().providor_1());

            conn.Open();
            DataTable dt = new DataTable();
            adp.Fill(dt);
            /*... Save in Xls file ...*/

            Workbook book = new Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.InsertDataTable(dt, true, 3, 3);
            book.SaveToFile("database/book1.xls");

            /*........................*/
            conn.Close();
            Console.WriteLine("Save data is done!");
        }
        public DataTable GetDataFromXls()
        {
            
            string connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;
                              Data Source=database/book1.xls
                              ;Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(connStr);
            OleDbCommand comm = new OleDbCommand("SELECT * FROM [sheet1$]",conn);
            conn.Open();
            OleDbDataAdapter adp = new OleDbDataAdapter();
            
            /*------------- NOT working with sqlite just with OLE ----------------*/
            /*
            string connStr = "Data Source=Provider=Microsoft.ACE.OLEDB.12.0;" +
                "Data Source=database/book1.xls;" +
                "Extended Properties=Excel 8.0;";
            SQLiteConnection conn = new SQLiteConnection(connStr);
            SQLiteCommand comm = new SQLiteCommand("SELECT * FROM [sheet1$]", conn);
            conn.Open();
            SQLiteDataAdapter adp = new SQLiteDataAdapter();
            */
            adp.SelectCommand = comm;
            DataTable dt = new DataTable();
            dt.Clear();
            adp.Fill(dt);
            conn.Close();
            return dt;


        }

    }
}
