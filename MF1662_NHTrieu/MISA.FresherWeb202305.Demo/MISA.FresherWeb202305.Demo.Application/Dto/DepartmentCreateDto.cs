using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Dto
{
    public class DepartmentCreateDto
    {
       
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// 
        [Required(ErrorMessage = "Mã loại phòng ban không được để trống")]
        [StringLength(30, ErrorMessage = "Mã loại phòng ban có tối đa 30 kí tự")]
        [RegularExpression(@"^MLPB\d{6}$", ErrorMessage = "Mã loại tài sản phải có định dạng MLPBxxxxx")]

        public string DepartmentCode { get; set; } = String.Empty;
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// 
        [Required(ErrorMessage = "Tên loại phòng ban không được để trống")]
        [StringLength(255, ErrorMessage = "Tên loại phòng ban có tối đa 255 kí tự")]
        public string DepartmentName { get; set; } = String.Empty;
    }
}
