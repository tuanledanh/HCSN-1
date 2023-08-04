using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher052023.Domain.Entity.Department
{
    public class DepartmentEntity : BaseAuditEntity, IHasKeyEntity
    {
        #region Properties
        /// <summary>
        /// Id của phòng ban
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Id của phòng ban không được để trống")]
        [StringLength(36, MinimumLength = 36, ErrorMessage = "Id phòng ban phải có độ dài 36 ký tự")]
        public string DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Mã của phòng ban không được để trống")]
        [MaxLength(20, ErrorMessage = "Mã phòng ban không được vượt quá 20 ký tự")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [Required(ErrorMessage = "Tên phòng ban không được để trống")]
        [MaxLength(100, ErrorMessage = "Tên phòng ban không được vượt quá 100 ký tự")]
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
        public string? ParentId { get; set; }

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
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string GetKeyId()
        {
            return this.DepartmentId;
        }

        /// <summary>
        /// Hàm lấy DepartmentCode
        /// </summary>
        /// <returns>Mã phòng ban</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string GetKeyCode()
        {
            return this.DepartmentCode;
        }

        /// <summary>
        /// Set DepartmentId
        /// </summary>
        /// <param name="departmentId">DepartmentId</param>
        /// <return></return>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public void SetKeyId(string departmentId)
        {
            this.DepartmentId = departmentId;
        }
        #endregion
    }
}
