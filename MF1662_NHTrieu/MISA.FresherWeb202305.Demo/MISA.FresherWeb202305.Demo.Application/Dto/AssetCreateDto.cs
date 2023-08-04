using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MISA.FresherWeb202305.Demo.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using MISA.FresherWeb202305.Demo.Domain;

namespace MISA.FresherWeb202305.Demo.Application.Dto
{
    public class AssetCreateDto
    {
        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        [Required(ErrorMessage = "Không được để trống mã loại tài sản")]
        [RegularExpression(@"[A-Z]{2,6}\d{6}", ErrorMessage = "Mã loại tài sản phải có định dạng TSxxxxxxx")]
        public string AssetCode { get; set; } = string.Empty;
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        [Required(ErrorMessage = "Không được để trống tên loại tài sản")]
        [MaxLength(255, ErrorMessage = "Dộ dài tối đa cho tên tài sản là 255 kí tự")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// Khoa ngoại kết nối bảng phòng ban
        /// </summary>
        [ForeignKey("department")]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Khoa loại kết nối bảng mã loại tài sản
        /// </summary>
        [ForeignKey("fixed_asset_category")]
        public Guid AssetTypeId { get; set; }
        /// <summary>
        /// Ngày mua
        /// </summary>
        [Required(ErrorMessage = "Không được để trống ngày mua")]
        public DateTime PurchaseDate { get; set; }
        /// <summary>
        /// Nguyên gia
        /// </summary>
        [Required(ErrorMessage = "Không được để trống nguyên giá")]
        public float Cost { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        [Required(ErrorMessage = "Không được để trống số lượng")]
        public int Quantity { get; set; }
        
        /// <summary>
        /// Năm bắt đầu sử dụng
        /// </summary>
        [Required(ErrorMessage = "Không được để trống năm bắt đầu sử dụng")]
        public int ProductionYear { get; set; }
        /// <summary>
        /// Năm bắt đầu theo dõi sản phẩm phần mềm
        /// 
        /// </summary>
        public int TrackedYear { get; set; }


    }
}
