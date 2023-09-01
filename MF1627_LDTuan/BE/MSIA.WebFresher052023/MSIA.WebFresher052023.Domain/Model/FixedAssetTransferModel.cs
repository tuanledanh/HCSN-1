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
        /// Phòng ban cần chuyển tài sản đến
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public Guid NewDepartmentId { get; set; }

        /// <summary>
        /// Lý do điều chuyển
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public string Description { get; set; }

        public TransferAssetModel TransferAssetModel { get; set; }
    }
}
