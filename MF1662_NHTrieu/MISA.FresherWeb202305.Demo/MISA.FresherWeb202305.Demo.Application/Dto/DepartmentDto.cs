using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Dto
{
    public class DepartmentDto
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
  
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// 
        

        public string DepartmentCode { get; set; } = String.Empty;
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// 
   
        public string DepartmentName { get; set; } = String.Empty;
    }
}
