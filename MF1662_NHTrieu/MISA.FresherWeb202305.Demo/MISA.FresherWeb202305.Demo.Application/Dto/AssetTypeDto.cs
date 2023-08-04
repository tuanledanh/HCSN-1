using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Dto
{
    public class AssetTypeDto
    {
        /// <summary>
        /// Khóa chính bảng 
        /// </summary>
        [Key]
        public Guid AssetTypeId { get; set; }
        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        [Required(ErrorMessage = "Không được để trống mã loại tài sản")]
        [MaxLength(50, ErrorMessage = "Mã loại tài sản có tối đa 5 kí tự")]
        public string AssetTypeCode { get; set; } = string.Empty;
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        [Required(ErrorMessage = "Không được để trống tên loại tài sản")]
        [MaxLength(100, ErrorMessage = "Tên loại tài sản có tối đa 100 kí tự")]
        public string AssetTypeName { get; set; } = string.Empty;
        /// <summary>
        /// Ti lệ hao mòn
        /// </summary>
        [Required(ErrorMessage = "Không được để trống tỉ lệ hao mòn")]
        [RegularExpression(@"([0-9]|[1-9][0-9]|100)", ErrorMessage = "Tỉ lệ hao mòn phải từ 0-100 %")]
        public float DepreciationRate { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        [Required(ErrorMessage = "Không được để trống số năm sử dụng")]
        public int LifeTime { get; set; }

    }
}
