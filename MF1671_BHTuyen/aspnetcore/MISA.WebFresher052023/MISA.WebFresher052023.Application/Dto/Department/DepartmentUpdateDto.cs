using MISA.WebFresher052023.Application.Interface.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Dto.Department
{
    public class DepartmentUpdateDto : IHasCodeDto
    {
        #region Properties
        /// <summary>
        /// Mã bộ phận  
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required]
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

        #region Methods
        /// <summary>
        /// Lấy mã bộ phận
        /// </summary>
        /// <returns>Mã bộ phận</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string GetCode()
        {
            return DepartmentCode;
        }
        #endregion

    }
}
