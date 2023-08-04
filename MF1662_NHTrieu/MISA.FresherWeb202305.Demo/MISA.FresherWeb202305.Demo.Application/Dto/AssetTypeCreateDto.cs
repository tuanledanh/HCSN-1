using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Dto
{
    public class AssetTypeCreateDto
    {
       
        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        /// 
        [Required(ErrorMessage = "Mã loại tài sản không được để trống")]
        [StringLength(30, ErrorMessage = "Mã loại tài sản có tối đa 30 kí tự")]
        [RegularExpression(@"^MLTS\d{6}$", ErrorMessage = "Mã loại tài sản phải có định dạng MLTSxxxxx")]
        public string AssetTypeCode { get; set; } = string.Empty;
        /// <summary>
        /// Tên loại tài sản
        /// </summary>

        /// <summary>
        /// Tỉ lệ hao mòn
        /// </summary>
        /// 
        [Required(ErrorMessage = "Tên loại tài sản không được để trống")]
        [StringLength(255, ErrorMessage = "Tên loại tài sản có tối đa 255 kí tự")]
        public string AssetTypeName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Không được để trống số năm sử dụng")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Số năm sử dụng phải là số nguyên lớn hơn 0")]
        public int YearofUse { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// 
        [Required(ErrorMessage = "Không được để trống tỉ lệ hao mòn")]
        [Range(0, 100, ErrorMessage = "Tỉ lệ hao mòn phải từ 0 đến 100%")]
        public double OriginalPrice { get; set; }
    }
}
