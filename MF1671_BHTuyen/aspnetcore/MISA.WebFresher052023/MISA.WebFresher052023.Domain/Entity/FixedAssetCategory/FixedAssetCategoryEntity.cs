using MISA.WebFresher052023.Domain.Interface.Base;

namespace MISA.WebFresher052023.Domain.Entity.FixedAssetCategory
{
    public class FixedAssetCategoryEntity : BaseAuditEntity, IHasKeyEntity, IHasCodeEntity
    {
        #region Properties
        /// <summary>
        /// Id loại tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid FixedAssetCategoryId { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string FixedAssetCategoryCode { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string FixedAssetCategoryName { get; set; }

        /// <summary>
        /// Id của đơn vị
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid? OrganizationId { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public float DepreciationRate { get; set; }

        /// <summary>
        /// Số năm sửa dụng loại tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
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
        public Guid GetKey()
        {
            return FixedAssetCategoryId;
        }

        /// <summary>
        /// Lấy code mã loại tài sản
        /// </summary>
        /// <returns>Mã loại tài sản</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string GetCode()
        {
            return FixedAssetCategoryCode;
        }

        /// <summary>
        /// Gán mã Id cho loại tài sản
        /// </summary>
        /// <param name="FixedAssetCategoryId">Mã id của loại tài sản</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public void SetKey(Guid FixedAssetCategoryId)
        {
            this.FixedAssetCategoryId = FixedAssetCategoryId;
        }
        #endregion
    }
}
