using Domain.Model;

namespace MSIA.WebFresher052023.Domain.Model
{
    public class FixedAssetTransferModel
    {
        /// <summary>
        /// Id của tài sản
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public Guid FixedAssetId { get; set; }

        /// <summary>
        /// Mã code của tài sản
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public string FixedAssetCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public string FixedAssetName { get; set; }

        /// <summary>
        /// Giá tài sản
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public decimal Cost { get; set; }

        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public int LifeTime { get; set; }

        /// <summary>
        /// Giá tài sản còn lại
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public decimal RemainingCost { get; set; }

        /// <summary>
        /// Id của chi tiết tài sản điều chuyển
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public Guid TransferAssetDetailId { get; set; }

        /// <summary>
        /// Phòng ban hiện tại của tài sản
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public Guid OldDepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban hiện tại của tài sản
        /// </summary>
        /// Created by: ldtuan (02/09/2023)
        public string OldDepartmentName { get; set; }

        /// <summary>
        /// Phòng ban cần chuyển tài sản đến
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public Guid NewDepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban cần chuyển đến
        /// </summary>
        /// Created by: ldtuan (02/09/2023)
        public string NewDepartmentName { get; set; }

        /// <summary>
        /// Lý do điều chuyển
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public string Description { get; set; }

        public TransferAssetModel TransferAsset { get; set; }
    }
}
