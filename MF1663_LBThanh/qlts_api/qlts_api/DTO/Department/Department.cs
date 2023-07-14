namespace qlts_api.DTO.Department
{
    public class Department
    {
        /// <summary>
        /// Id của phòng ban
        /// </summary>
        /// Created by: LB.Thành (12/07/2023)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created by: LB.Thành (12/07/2023)
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Mã code của phòng ban
        /// </summary>
        /// Created by: LB.Thành (12/07/2023)
        public string? DepartmentCode { get; set; }

    }
}
