using dbconnection.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbconnection
{
    internal class DataModel
    {

    }

    public class DataInventory : IInventory
    {
        public int id { get; set; }

        public string item { get; set; }
        public int itemCode { get; set; }
    }


    public class DataCustomer : ICustomer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
    }
}
