using Dapper;
using Domain.Entity;
using Domain.Interface.Depart;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department, DepartmentModel>, IDepartmentRepository
    {
        #region Constructor
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Thêm mới một phòng ban
        /// </summary>
        /// <param name="department">Thông tin của phòng ban</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public async Task<bool> InsertAsync(Department department)
        {
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_DepartmentName = department.DepartmentName,
                p_DepartmentCode = department.DepartmentCode,
                p_CreatedDate = department.CreatedDate,
                p_CreatedBy = department.CreatedBy,
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
        /// <param name="department">Thông tin mới của phòng ban</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (12/07/2023)
        public async Task<bool> UpdateAsync(Department department)
        {
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
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

        /// <summary>
        /// Xóa thông tin một bản ghi thông qua mã code
        /// </summary>
        /// <param name="department">Phòng ban cần xóa</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public async Task<bool> DeleteAsync(Department department)
        {
            string procedureName = "Proc_DeleteDepartmentById";
            var paramName = "p_Id";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, department.DepartmentId);
            var connection = await GetOpenConnectionAsync();
            var affectedRow = await connection.ExecuteAsync(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return affectedRow > 0;
        } 
        #endregion
    }
}
