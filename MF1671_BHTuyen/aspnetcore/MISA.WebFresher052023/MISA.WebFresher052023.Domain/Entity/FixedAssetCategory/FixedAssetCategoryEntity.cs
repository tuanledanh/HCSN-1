using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher052023.Domain.Entity.FixedAssetCategory
{
    public class FixedAssetCategoryEntity : BaseAuditEntity, IHasKeyEntity
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
        /// Id của đơn vị
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string? OrganizationId { get; set; }

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

        /// <summary>
        /// Ghi chú
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string? Description { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy ra mã loại tài sản
        /// </summary>
        /// <returns>Id loại tài sản</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string GetKeyId()
        {
            return this.FixedAssetCategoryId;
        }

        /// <summary>
        /// Lấy code mã loại tài sản
        /// </summary>
        /// <returns>Mã loại tài sản</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string GetKeyCode()
        {
            return this.FixedAssetCategoryCode;
        }

        /// <summary>
        /// Set FixedAssetCategoryId
        /// </summary>
        /// <param name="FixedAssetCategoryId">FixedAssetCategoryId</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public void SetKeyId(string FixedAssetCategoryId)
        {
            this.FixedAssetCategoryId = FixedAssetCategoryId;
        }
        #endregion
    }
}
