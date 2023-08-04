using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Infrastructure.ConfigDapper
{
    public class MySqlGuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value)
        {
            string? rawValue = value != null ? value.ToString() : string.Empty;
            if (Guid.TryParse(rawValue, out Guid result))
            {
                return result;
            }
            else
            {
                return Guid.Empty;
            }
        }

        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            if(Guid.Empty == value)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = value.ToString();
            }
        }
    }
}
