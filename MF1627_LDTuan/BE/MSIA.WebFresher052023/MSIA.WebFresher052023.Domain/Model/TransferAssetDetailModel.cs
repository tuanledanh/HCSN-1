using MSIA.WebFresher052023.Domain.Model.Base;

namespace Domain.Model
{
    public class TransferAssetDetailModel : IHasKeyModel
    {
        #region Fields
        /// <summary>
        /// Id của chi tiết tài sản điều chuyển
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public Guid TransferAssetDetailId { get; set; }

        /// <summary>
        /// Id của tài sản
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public Guid FixedAssetId { get; set; }

        /// <summary>
        /// Id của chứng từ
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public Guid TransferAssetId { get; set; }

        /// <summary>
        /// Mã chứng từ
        /// </summary>
        /// Created by: ldtuan (06/09/2023)
        public string? TransferAssetCode { get; set; }

        /// <summary>
        /// Ngày điều chuyển
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public DateTime TransferDate { get; set; }

        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public DateTime? TransactionDate { get; set; }

        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public DateTime? ModifiedDate { get; set; }

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

        /// <summary>
        /// Nhận được giá trị của thuộc tính khóa trong một đối tượng cụ thể 
        /// mà không cần truy cập trực tiếp vào thuộc tính đó, TEntity có thể xài để lấy đc id
        /// </summary>
        /// <returns>Id của chứng từ</returns>
        /// Created by: ldtuan (28/08/2023)
        public string GetKey()
        {
            return Description;
        }
        #endregion
    }
}
