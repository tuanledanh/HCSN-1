using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Z.Dapper.Plus;
namespace MISA.FresherWeb202305.Demo.Domain.Entity
{
    [Table("fixed_asset")]
    public class Asset : Base,IHasKey
    {
        /// <summary>
        /// Khóa chính bảng tài sản
        /// </summary>
        [Key]
       
        public Guid AssetId { get; set; }
        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        [Required(ErrorMessage = "Không được để trống mã loại tài sản")]
        [RegularExpression(@"[A-Z]{2,6}\d{6}", ErrorMessage = "Mã loại tài sản phải có định dạng TSxxxxxxx")]
        [Column("fixed_asset_code")]
        public string AssetCode { get; set; } = string.Empty;
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        [Column("fixed_asset_name")]
        [Required(ErrorMessage = "Không được để trống tên loại tài sản")]
        [MaxLength(255, ErrorMessage = "Dộ dài tối đa cho tên tài sản là 255 kí tự")]
       
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// Khoa ngoại kết nối bảng phòng ban
        /// </summary>
        [ForeignKey("department")]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã loại phòng ban
        /// </summary>
        [Required(ErrorMessage = "Không được để trống mã loại phòng ban")]
        public string DepartmentCode { get; set; } = string.Empty;
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;
        /// <summary>
        /// Khoa loại kết nối bảng mã loại tài sản
        /// </summary>
        [ForeignKey("fixed_asset_category")]
        public Guid AssetTypeId { get; set; }
        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        [Required(ErrorMessage = "Không được để trông mã loại tài sản")]
        public string AssetTypeCode { get; set; } = string.Empty;
        /// <summary>
        /// Tên loại phòng ban
        /// </summary>
        public string AssetTypeName { get; set; } = string.Empty;
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
        /// Ti lệ hao mòn
        /// </summary>
        [Required(ErrorMessage = "Không được để trống tỉ lệ hao mòn")]
        [RegularExpression(@"([0-9]|[1-9][0-9]|100)", ErrorMessage = "Tỉ lệ hao mòn phải từ 0-100 %")]
        public float DepreciationRate { get; set; }
        /// <summary>
        /// Năm theo dõi
        /// </summary>
        
        public int TrackedYear { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        [Required(ErrorMessage = "Không được để trống số năm sử dụng")]
        public int LifeTime { get; set; }
        /// <summary>
        /// Năm bắt đầu sử dụng
        /// </summary>
        [Required(ErrorMessage ="Không được để trống năm bắt đầu sử dụng")]
        public int ProductionYear { get; set; }
        /// <summary>
        /// Lấy ra mã của bảng
        /// </summary>
        /// <returns>Trả về id của bảng</returns>
        /// Createdby: nhtrieu(15/07/2023)
        public Guid GetKey()
        {
            return AssetId;
        }
    }
}
