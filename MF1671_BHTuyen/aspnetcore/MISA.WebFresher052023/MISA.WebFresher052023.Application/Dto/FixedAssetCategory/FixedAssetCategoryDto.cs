using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher052023.Application.Dto.FixedAssetCategory
{
    public class FixedAssetCategoryDto
    {
        #region Properties
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
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Tỷ lệ hao mòn không được để trống")]
        [Range(0, 100, ErrorMessage = "Tỷ lệ hao mòn phải nằm trong khoảng 0 - 100")]
        public float DepreciationRate { get; set; }

        /// <summary>
        /// Số năm sửa dụng loại tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Tỷ lệ hao mòn không được để trống")]
        [Range(0, 100, ErrorMessage = "Tỷ lệ hao mòn phải nằm trong khoảng 0 - 100")]
        public int LifeTime { get; set; }

        #endregion
    }
}
