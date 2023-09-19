using MISA.WebFresher052023.Domain.Interface.Base;

namespace MISA.WebFresher052023.Domain.Entity.Department
{
    public class DepartmentEntity : BaseAuditEntity, IHasKeyEntity, IHasCodeEntity
    {
        #region Properties
        /// <summary>
        /// Id của phòng ban
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string DepartmentName { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string? Description { get; set; }

        /// <summary>
        /// Có phải là cha không
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public bool? IsParent { get; set; }

        /// <summary>
        /// Id của phòng ban cha
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Id của đơn vị
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string? OrganizationId { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy Id của phòng ban
        /// </summary>
        /// <returns>Mã Id của phòng ban</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid GetKey()
        {
            return this.DepartmentId;
        }

        /// <summary>
        /// Hàm lấy DepartmentCode
        /// </summary>
        /// <returns>Mã code của phòng ban</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string GetCode()
        {
            return this.DepartmentCode;
        }

        /// <summary>
        /// Set DepartmentId
        /// </summary>
        /// <param name="departmentId">DepartmentId</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public void SetKey(Guid departmentId)
        {
            this.DepartmentId = departmentId;
        }
        #endregion
    }
}
