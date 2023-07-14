using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.BL_Services.DTO.Depart
{
    public class DepartmentUpdateDto
    {
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public string DepartmentName { get; set; }
        /// <summary>
        /// Mã code của phòng ban
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public string DepartmentCode { get; set; }
    }
}
