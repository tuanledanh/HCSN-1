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
        #endregion
    }
}
