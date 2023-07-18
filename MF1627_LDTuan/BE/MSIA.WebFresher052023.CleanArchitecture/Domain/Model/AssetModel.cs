using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class AssetModel
    {
        #region Fields
        /// <summary>
        /// Id của tài sản
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public Guid AssetId { get; set; }
        /// <summary>
        /// Mã code của tài sản
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public string AssetCode { get; set; }
        /// <summary>
        /// Tên tài sản
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public string AssetName { get; set; }
        /// <summary>
        /// Số lượng tài sản
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public int AssetQuantity { get; set; }
        /// <summary>
        /// Giá tài sản
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public double AssetPrice { get; set; }
        /// <summary>
        /// Khấu hao tài sản
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public double? AssetDepreciation { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public int? YearOfUse { get; set; }
        /// <summary>
        /// Năm theo dõi
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public int? YearOfTracking { get; set; }
        /// <summary>
        /// Ngày mua hàng
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public DateTime? PurchaseDate { get; set; }
        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public DateTime? StartUsingDate { get; set; }
        /// <summary>
        /// Khóa chính của bảng loại tài sản
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public Guid AssetTypeId { get; set; }
        /// <summary>
        /// Tên của loại tài sản
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public string AssetTypeName { get; set; }
        /// <summary>
        /// Mã code của loại tài sản
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public string AssetTypeCode { get; set; }
        /// <summary>
        /// Khóa chính của bảng phòng ban
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public string DepartmentName { get; set; }
        /// <summary>
        /// Mã code của phòng ban
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public string DepartmentCode { get; set; } 
        #endregion
    }
}
