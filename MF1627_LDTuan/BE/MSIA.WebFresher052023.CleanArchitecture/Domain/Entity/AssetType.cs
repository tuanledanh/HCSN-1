namespace Domain.Entity
{
    public class AssetType : BaseEntity
    {
        #region Fields
        /// <summary>
        /// Id của loại tài sản
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public Guid AssetTypeId { get; set; }
        /// <summary>
        /// Tên của loại tài sản
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public string AssetTypeName { get; set; }
        /// <summary>
        /// Mã code của loại tài sản
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public string AssetTypeCode { get; set; } 
        #endregion
    }
}
