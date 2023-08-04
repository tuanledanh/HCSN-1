using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Domain.Models
{
    public class Pagination
    {
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        /// CreatedBy: nhtrieu (15/07/2023)
        public object? Data { get; set; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        /// CreatedBy: nhtrieu (15/07/2023)
        public int? TotalRecords { get; set; }
    }
}
