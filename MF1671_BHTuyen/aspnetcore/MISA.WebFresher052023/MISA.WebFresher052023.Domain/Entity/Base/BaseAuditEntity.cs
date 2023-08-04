namespace MISA.WebFresher052023.Domain.Entity
{
    public abstract class BaseAuditEntity
    {
        #region Properties
        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string? ModifiedBy { get; set; }
        #endregion
    }
}
