using Dapper;
using Microsoft.Extensions.Configuration;
using MSIA.WebFresher052023.DL_Repositories.Entity;
using System.Data;

namespace MSIA.WebFresher052023.DL_Repositories.Repositories.Depart
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Thêm mới một phòng ban
        /// </summary>
        /// <param name="department">Thông tin của phòng ban</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (12/07/2023)
        public async Task<bool> PostAsync(Department department)
        {
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_DepartmentName = department.DepartmentName,
                p_DepartmentCode = department.DepartmentCode,
                p_CreatedDate = DateTime.Now,
                p_CreatedBy = "tuan",
                p_ModifiedDate = department.ModifiedDate,
                p_ModifiedBy = department.ModifiedBy
            };
            var affectedRow = await connection.ExecuteAsync("Proc_InsertDepartment", parameters, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return affectedRow > 0;
        }

        /// <summary>
        /// Cập nhật thông tin của một phòng ban
        /// </summary>
        /// <param name="id">Id của phòng ban cần cập nhật</param>
        /// <param name="department">Thông tin mới của phòng ban</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (12/07/2023)
        public async Task<bool> PutAsync(Guid id, Department department)
        {
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_DepartmentId = id,
                p_DepartmentName = department.DepartmentName,
                p_DepartmentCode = department.DepartmentCode,
                p_CreatedDate = department.CreatedDate,
                p_CreatedBy = department.CreatedBy,
                p_ModifiedDate = department.ModifiedDate,
                p_ModifiedBy = department.ModifiedBy
            };
            var affectedRow = await connection.ExecuteAsync("Proc_UpdateDepartment", parameters, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return affectedRow > 0;
        }
    }
}
