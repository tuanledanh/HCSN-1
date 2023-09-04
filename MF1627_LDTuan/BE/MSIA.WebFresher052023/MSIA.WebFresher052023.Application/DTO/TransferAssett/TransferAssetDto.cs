using MSIA.WebFresher052023.Application.DTO;
using MSIA.WebFresher052023.Domain.Model;

namespace Application.DTO
{
    public class TransferAssetDto
    {
        #region Fields
        /// <summary>
        /// Id của chứng từ
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public Guid TransferAssetId { get; set; }
        /// <summary>
        /// Mã chứng từ
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public string TransferAssetCode { get; set; }

        /// <summary>
        /// Ngày điều chuyển
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public DateTime TransferDate { get; set; }

        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Lý do điều chuyển
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public string Description { get; set; }

        /// <summary>
        /// Nguyên giá của tài sản trong chứng từ
        /// </summary>
        /// Created by: ldtuan (01/09/2023)
        public int? Cost { get; set; }

        /// <summary>
        /// Giá tài sản còn lại
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public decimal RemainingCost { get; set; }

        /// <summary>
        /// Danh sách tài sản cùng phòng ban từ chi tiết chứng từ
        /// </summary>
        /// Created by: ldtuan (01/09/2023)
        public List<FixedAssetTransferDto>? FixedAssetTransfers { get; set; }

        /// <summary>
        /// Danh sách người nhận
        /// </summary>
        /// Created by: ldtuan (03/09/2023)
        public List<ReceiverTransferDto>? ReceiverTransfers { get; set; }
        #endregion
    }
}
