namespace MISA.WebFresher052023.Application.Dto.FixedAsset
{
    public class FixedAssetDto
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
        /// Hao mòn khấu hao lũy kế
        /// </summary>
        public double AccumulationDepreciation { get; set; }

        /// <summary>
        /// Giá trị còn lại
        /// </summary>
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

        #endregion
    }
}
