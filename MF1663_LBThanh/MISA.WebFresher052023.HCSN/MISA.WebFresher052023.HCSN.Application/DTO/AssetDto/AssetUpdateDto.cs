using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.DTO.AssetDTO
{
    public class AssetUpdateDto
    {
        /// <summary>
        /// Id của tài sản
        /// </summary>
        /// Created by: LB.Thành (13/7/2023)
        public Guid AssetId { get; set; }
        /// <summary>
        /// Tên của tài sản
        /// </summary>
        /// Created by: LB.Thành (13/7/2023)
        [Required]
        [MaxLength(100)]
        public string AssetName { get; set; }
        /// <summary>
        /// Mã của tài sản
        /// </summary>
        /// Created by: LB.Thành (13/7/2023)
        public string AssetCode { get; set; }
        /// <summary>
        /// Số lượng tài sản
        /// </summary>
        /// Created by: LB.Thành (13/7/2023)
        public int AssetQuantity { get; set; }
        /// <summary>
        /// Nguyên giá tài sản
        /// </summary>
        /// Created by: LB.Thành (13/7/2023)
        public double AssetPrice { get; set; }
        /// <summary>
        /// Khấu hao tài sản
        /// </summary>
        /// Created by: LB.Thành (13/7/2023)
        public float AssetDepreciation { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// Created by: LB.Thành (13/7/2023)
        public int YearOfUse { get; set; }
        /// <summary>
        /// Ngày mua
        /// </summary>
        /// Created by: LB.Thành (13/7/2023)
        public DateTime PurchaseDate { get; set; }
        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        /// Created by: LB.Thành (13/7/2023)
        public DateTime StartUsingDate { get; set; }
        /// <summary>
        /// Ngày bắt đàu theo dõi
        /// </summary>
        /// Created by: LB.Thành (13/7/2023)
        public DateTime YearOfTracking { get; set; }
        /// <summary>
        /// Id loại tài sản
        /// </summary>
        /// Created by: LB.Thành (13/7/2023)
        public Guid AssetTypeId { get; set; }
        /// <summary>
        /// Id phòng ban
        /// </summary>
        /// Created by: LB.Thành (13/7/2023)
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created by: LB.Thành (13/7/2023)
        public string DepartmentName { get; set; }
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        /// Created by: LB.Thành (13/7/2023)
        public string AssetTypeName { get; set; }
    }
}
