using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbconnection
{
    public interface IInventory
    {
        int id { get; set; }
        string item { get; set; }
        int itemCode { get; set; }

    }
}
