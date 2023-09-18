using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface.Base
{
    public interface IHasCodeEntity
    {
        /// <summary>
        /// Hàm lấy Code
        /// </summary>
        /// <returns>Mã bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string GetCode();
    }
}
