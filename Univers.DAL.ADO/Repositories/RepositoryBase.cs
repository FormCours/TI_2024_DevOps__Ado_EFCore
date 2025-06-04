using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univers.DAL.ADO.Repositories
{
    public class RepositoryBase
    {
        protected readonly DbConnection _Connection;
        public RepositoryBase(DbConnection connection)
        {
            _Connection = connection;
        }
        protected void AddCommandParameter(DbCommand cmd, string name, object? value)
        {
            DbParameter param = cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;
            cmd.Parameters.Add(param);
        }
    }
}
