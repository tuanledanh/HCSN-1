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

        public List<FixedAssetTransferModel>? FixedAssetTranfers { get; set; }

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
