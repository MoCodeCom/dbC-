using System;
using System.Data;

namespace dbconnection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DbConnection dbc = new DbConnection();
            InventoryDb invDb = new InventoryDb();
            DisconnectLayers disconnectLayers = new DisconnectLayers();


            //invDb.InsertInventory(new DataInventory { item = "orange", itemCode=0820});
            //invDb.InsertInventory(new DataInventory { item = "apple ", itemCode = 0120 });

            /*
            List<DataInventory> InventoryItems = invDb.GetAllInventory();
            foreach (DataInventory item in InventoryItems) {
                Console.WriteLine("ID: "+item.id +" ---  "+item.item + "  ---   "+  item.itemCode);
            }
            */
            //dbc.connDb();
            /*
            int code = 125002;
            for (int x=0;x<100 ;x++)
            {
                code++;
                disconnectLayers.InsertCustomer("Mohammed", code);
            }*/

            //disconnectLayers.SelectAllCustomer();
            //disconnectLayers.adapTest();
            //DataTable dt = disconnectLayers.datatable();

            /*print data form datatable dirctlly*/
            /*
            using (DataTableReader dtr = new DataTableReader(dt))
            {
                while (dtr.Read()) 
                {
                    for (int i=0;i<dtr.FieldCount ;i++)
                    {
                        Console.WriteLine("{0}\t",dtr.GetValue(i).ToString().Trim());
                    }
                }
                
            }
            */

            //DataSet ds = disconnectLayers.dataset();
            //disconnectLayers.FillDataSet(ds,dt);
            //disconnectLayers.PrintDataSet(ds);


            //disconnectLayers.SaveDataSetXML(ds);
            //DataSet dsXML = disconnectLayers.GetDataSetFromXML();
            //disconnectLayers.PrintDataSet(dsXML);
            /*convert to binary file bin*/
            //disconnectLayers.SaveDataSetBinary(dsXML);
            /*bring data from binary file*/
            //DataSet dsBinary = disconnectLayers.GetDataFromBinary();
            //disconnectLayers.PrintDataSet(dsBinary);
            //disconnectLayers.MyDir();


            /*Using dataadpter with database*/
            DisconnectLayers dsl = new DisconnectLayers();
            //DataSet dsp = dsl.AdapterDatabase();
            //DataTable dtp = dsl.AdapterDataTable();
            //dsl.PrintDataSet(dsp);
            //dsl.SaveDataSetXML(dsp);
            //dsl.PrintDataTable(dtp);
            dsl.SaveDatabaseXls();

            DataTable dt = dsl.GetDataFromXls();
            dsl.PrintDataTable(dt);
            Console.ReadLine();
        }
    }
}
