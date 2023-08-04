using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Domain.Entity
{
    public interface IHasKey
    {
        /// <summary>
        /// Lấy Id của đối tượng từ entity
        /// </summary>
        /// <returns>Id của đối tượng</returns>
        /// CreatedBy: nhtrieu (18/07/2023)
        Guid GetKey();
    }

}
