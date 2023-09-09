namespace Application.DTO
{
    public class TransferAssetDetailDto
    {
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
    }
}
