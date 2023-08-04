using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Dto.Department
{
    public class DepartmentDto
    {
        #region Properties
        /// <summary>
        /// ID bộ phận
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Id của phòng ban không được để trống")]
        [StringLength(36, MinimumLength = 36, ErrorMessage = "Id phòng ban phải có độ dài 36 ký tự")]
        public string DepartmentId { get; set; }

        /// <summary>
        /// Mã bộ phận
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Mã của phòng ban không được để trống")]
        [MaxLength(20, ErrorMessage = "Mã phòng ban không được vượt quá 20 ký tự")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên bộ phận
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Tên phòng ban không được để trống")]
        [MaxLength(100, ErrorMessage = "Tên phòng ban không được vượt quá 100 ký tự")]
        public string DepartmentName { get; set; } 
        #endregion
    }
}
