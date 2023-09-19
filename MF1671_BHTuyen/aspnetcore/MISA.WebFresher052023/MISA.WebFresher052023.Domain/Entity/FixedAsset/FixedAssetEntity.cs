using MISA.WebFresher052023.Domain.Interface.Base;

namespace MISA.WebFresher052023.Domain.Entity.FixedAsset
{
    public class FixedAssetEntity : BaseAuditEntity, IHasKeyEntity, IHasCodeEntity
    {
        #region Properties
        /// <summary>
        /// Id tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid FixedAssetId { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string FixedAssetCode { get; set; }

        /// <summary>
        /// Tên Tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string FixedAssetName { get; set; }

        /// <summary>
        /// Id của đơn vị
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid? OrganizationId { get; set; } 

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string? OrganizationCode { get; set; } 

        /// <summary>
        /// Tên của đơn vị
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string? OrganizationName { get; set; }

        /// <summary>
        /// Id bộ phận
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã bộ phận
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên bộ phận
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string DepartmentName { get; set; }

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
        /// Ngày mua tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DateTime UsingStartedDate { get; set; }

        /// <summary>
        /// Nguyên giá Tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public double Cost { get; set; }
        /// <summary>
        /// Giá trị hao mòn khấu hao lũy kế
        /// </summary>
        public double AccumulationDepreciation { get; set; }


        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public double RemainderCost { get; set; }

        /// <summary>
        /// Số lượng Tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public int Quantity { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public float DepreciationRate { get; set; }

        /// <summary>
        /// Giá trị hao mòn năm
        /// </summary>
        public double YearDepreciation { get; set; }

        /// <summary>
        /// Năm theo dõi Tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public int TrackedYear { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public int LifeTime { get; set; }

        /// <summary>
        /// Năm sản xuất
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public int? ProductionYear { get; set; }

        /// <summary>
        /// Sử dụng
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public bool? Active { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy Id của tài sản
        /// </summary>
        /// <returns>Mã Id của tài sản</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid GetKey()
        {
            return this.FixedAssetId;
        }

        /// <summary>
        /// Lấy mã code của tài sản
        /// </summary>
        /// <returns>Mã code của tài sản</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string GetCode()
        {
            return this.FixedAssetCode;
        }

        /// <summary>
        /// Gán mã Id cho tài sản
        /// </summary>
        /// <param name="FixedAssetId">Mã id của tài sản</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public void SetKey(Guid FixedAssetId)
        {
            this.FixedAssetId = FixedAssetId;
        }
        #endregion
    }
}
