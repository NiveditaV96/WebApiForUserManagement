using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.DAL
{
    interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}
