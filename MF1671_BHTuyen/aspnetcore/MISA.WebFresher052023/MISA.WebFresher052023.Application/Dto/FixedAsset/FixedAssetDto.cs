using MISA.WebFresher052023.Application.Dto.Base;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MISA.WebFresher052023.Application.Dto.FixedAsset
{
    public class FixedAssetDto
    {
        #region Properties
        /// <summary>
        /// Id tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Id của tài sản không được để trống")]
        [StringLength(36, MinimumLength = 36, ErrorMessage = "Id tài sản phải có độ dài 36 ký tự")]
        public string FixedAssetId { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Mã tài sản không được để trống")]
        [MaxLength(20, ErrorMessage = "Mã tài sản không được vượt quá 20 ký tự")]
        public string FixedAssetCode { get; set; }

        /// <summary>
        /// Tên Tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Tên của tài sản không được để trống")]
        [MaxLength(100, ErrorMessage = "Tên tài sản không được vượt quá 100 ký tự")]
        public string FixedAssetName { get; set; }

        /// <summary>
        /// Id bộ phận
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

        /// <summary>
        /// Id loại tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Id loại tài sản không được để trống")]
        [StringLength(36, MinimumLength = 36, ErrorMessage = "Id loại tài sản phải có độ dài 36 ký tự")]
        public string FixedAssetCategoryId { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Mã loại tài sản không được để trống")]
        [MaxLength(20, ErrorMessage = "Mã loại tài sản không được vượt quá 20 ký tự")]
        public string FixedAssetCategoryCode { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Tên loại tài sản không được để trống")]
        [MaxLength(100, ErrorMessage = "Tên loại tài sản không được vượt quá 100 ký tự")]
        public string FixedAssetCategoryName { get; set; }

        /// <summary>
        /// Ngày mua tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Ngày mua tài sản không được để trống")]
        [DataType(DataType.DateTime, ErrorMessage = "Ngày mua phải có dạng DateTime")]
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Ngày bắt đầu sử dụng không được để trống")]
        [DataType(DataType.DateTime, ErrorMessage = "Ngày bắt đầu sử dụng phải có dạng DateTime")]
        public DateTime UsingStartedDate { get; set; }

        /// <summary>
        /// Nguyên giá Tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Nguyên giá tài sản không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Nguyên giá tài sản phải lớn hơn 0")]
        public double Cost { get; set; }

        /// <summary>
        /// Số lượng Tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Số lượng tài sản không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng tài sản phải lớn hơn 0")]
        public int Quantity { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Tỷ lệ hao mòn không được để trống")]
        [Range(0, 100, ErrorMessage = "Tỷ lệ hao mòn phải nằm trong khoảng 0 - 100")]
        public float DepreciationRate { get; set; }

        /// <summary>
        /// Năm theo dõi Tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Năm theo dõi tài sản không được để trống")]
        [Range(1900, 9999, ErrorMessage = "Năm theo dõi tài sản phải nằm trong khoảng 1900 - 9999")]
        public int TrackedYear { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Tỷ lệ hao mòn không được để trống")]
        [Range(0, 100, ErrorMessage = "Tỷ lệ hao mòn phải nằm trong khoảng 0 - 100")]
        public int LifeTime { get; set; }

        #endregion
    }
}
