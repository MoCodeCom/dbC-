using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbconnection
{
    internal class DatabaseStrConnection
    {
        public string providor_1()
        {
            string sqliteStr = ConfigurationManager.ConnectionStrings["dataProvidor"].ConnectionString;
            return sqliteStr;
        }
    }
}
