using qlts_api.DTO.Department;

namespace qlts_api.Repository
{
    /// <summary>
    /// Sử dụng để định nghĩa các phương thức và thuộc tính để tương tác với đối tượng Department
    /// Created by: LB.Thành (12/07/2023)
    /// </summary>
    public interface IDepartmentRepo:IGenericRepository<Department, DepartmentRequest, Guid>
    {
    }
}
