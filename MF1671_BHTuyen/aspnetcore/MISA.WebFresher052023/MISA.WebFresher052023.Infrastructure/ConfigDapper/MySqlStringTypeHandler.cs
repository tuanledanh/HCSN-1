using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Infrastructure.ConfigDapper
{
    public class MySqlStringTypeHandler : SqlMapper.TypeHandler<string>
    {
        public override string Parse(object value)
        {
            return value.ToString();
        }

        public override void SetValue(IDbDataParameter parameter, string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = value;
            }
        }
    }
}
