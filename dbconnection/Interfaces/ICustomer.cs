using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbconnection.Interfaces
{
    internal interface ICustomer
    {
        int Id { get; set; }
        string Name { get; set; }
        int CustomerId { get; set; }
    }
}
