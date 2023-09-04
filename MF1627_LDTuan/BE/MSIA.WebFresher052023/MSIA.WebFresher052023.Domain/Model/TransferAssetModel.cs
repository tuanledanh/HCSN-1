using MSIA.WebFresher052023.Domain.Model;
using MSIA.WebFresher052023.Domain.Model.Base;

namespace Domain.Model
{
    public class TransferAssetModel : IHasKeyModel
    {
        #region Fields
        /// <summary>
        /// Id của chứng từ
        /// </summary>
        /// Created by: ldtuan (28/08/2023)
        public Guid TransferAssetId { get; set; }

        /// <summary>
        /// Mã chứng từ
        /// </summary>
        /// Created by: ldtuan (28/08/2023)
        public string TransferAssetCode { get; set; }

        /// <summary>
        /// Ngày điều chuyển
        /// </summary>
        /// Created by: ldtuan (28/08/2023)
        public DateTime TransferDate { get; set; }

        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        /// Created by: ldtuan (28/08/2023)
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Lý do điều chuyển
        /// </summary>
        /// Created by: ldtuan (28/08/2023)
        public string Description { get; set; }

        /// <summary>
        /// Nguyên giá của tài sản trong chứng từ
        /// </summary>
        /// Created by: ldtuan (01/09/2023)
        public int? Cost { get; set; }

        /// <summary>
        /// Giá tài sản còn lại
        /// </summary>
        /// Created by: ldtuan (02/09/2023)
        public decimal RemainingCost { get; set; }

        /// <summary>
        /// Danh sách tài sản cùng phòng ban từ chi tiết chứng từ
        /// </summary>
        /// Created by: ldtuan (01/09/2023)
        public List<FixedAssetTransferModel>? FixedAssetTransfers { get; set; }

        /// <summary>
        /// Danh sách người nhận
        /// </summary>
        /// Created by: ldtuan (03/09/2023)
        public List<ReceiverTransferModel>? ReceiverTransfers { get; set; }

        /// <summary>
        /// Nhận được giá trị của thuộc tính khóa trong một đối tượng cụ thể 
        /// mà không cần truy cập trực tiếp vào thuộc tính đó, TEntity có thể xài để lấy đc id
        /// </summary>
        /// <returns>Id của chứng từ</returns>
        /// Created by: ldtuan (28/08/2023)
        public string GetKey()
        {
            return TransferAssetCode;
        }
        #endregion
    }
}
