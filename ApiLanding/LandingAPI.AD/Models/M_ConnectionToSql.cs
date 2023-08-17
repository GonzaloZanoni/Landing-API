using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingApi.AD.Models
{
    public class M_ConnectionToSql
    {
        private readonly string _connection;

        public M_ConnectionToSql(string connection)
        {
            _connection = connection;
        }

        public string GetConnection()
        {
            return _connection;
        }
    }
}
