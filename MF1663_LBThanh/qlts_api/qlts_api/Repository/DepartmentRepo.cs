using Dapper;
using Microsoft.AspNetCore.Components.Forms;
using qlts_api.Context;
using qlts_api.DTO.Department;
using System.Data;

namespace qlts_api.Repository
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly DapperConnect db;
        public DepartmentRepo(DapperConnect db)
        {
            this.db = db;
        }
        /// <summary>
        /// lấy toàn bộ phòng ban từ db
        /// Created by: LB.Thành (12/07/2023)
        /// </summary>
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            var procname = "GetAllDeps";
            using (var connection = db.CreateConnection())
            {
                connection.Open();
                var departments = await connection.QueryAsync<Department>(procname, commandType: CommandType.StoredProcedure);
                connection.Close();
                return departments;
            }
        }
        /// <summary>
        /// lấy 1 phòng ban bằng Id 
        /// Param: Id của phòng ban muốn lấy ra
        /// Created by: LB.Thành (13/07/2023)
        /// </summary>
        public async Task<Department> Get(Guid id)
        {
            var procname = "GetDeptsById";
            using (var connection = db.CreateConnection())
            {
                connection.Open();
                var departments = await connection.QueryAsync<Department>(procname,
                    new { p_DepartmentId = id },
                    commandType: CommandType.StoredProcedure);
                connection.Close();
                return departments.FirstOrDefault();
            }
        }

        /// <summary>
        /// Tạo ra 1 phòng ban mới 
        /// Param: các property được khai báo trong DepartmentRequest
        /// Created by: LB.Thành (13/07/2023)
        /// </summary>
        public async Task<Department> CreateAsync(DepartmentRequest request)
        {
            var procname = "CreateDept";
            using (var connection = db.CreateConnection())
            {
                connection.Open();
                var department = await connection.QueryAsync<Department>(procname, new
                {
                    p_DeptName = request.DepartmentName,
                    p_DeptCode = request.DepartmentCode,
                    p_CreatedBy = request.CreatedBy,
                    p_ModifiedBy = request.ModifiedBy
                }, commandType: CommandType.StoredProcedure) ;
                connection.Close();
                return department.FirstOrDefault();
            }
        }

        /// <summary>
        /// Cập nhật 1 phòng ban
        /// Param: các property được khai báo trong DepartmentRequest và id của phòng ban cần update
        /// Created by: LB.Thành (13/07/2023)
        /// </summary>
        public async Task Update(Guid id, DepartmentRequest request)
        {
            var query = "UPDATE department d SET d.DepartmentName = @p_DepartmentName, " +
                "d.ModifiedBy = @p_ModifiedBy, " +
                "d.ModifiedDate = @p_ModifiedDate," +
                "d.DepartmentCode = @p_departmentCode WHERE d.DepartmentId = @p_DepartmentId";
            var parameters = new DynamicParameters();
            parameters.Add("p_DepartmentId", id, DbType.Guid);
            parameters.Add("p_DepartmentName", request.DepartmentName, DbType.String);
            parameters.Add("p_ModifiedBy", request.ModifiedBy, DbType.String);
            parameters.Add("p_ModifiedDate", DateTime.Now, DbType.DateTime);
            parameters.Add("p_departmentCode", request.DepartmentCode, DbType.String);
            using (var connection = db.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        /// <summary>
        /// Xóa 1 phòng ban
        /// Param: id của phòng ban cần xóa
        /// Created by: LB.Thành (13/07/2023)
        /// </summary>
        public async Task Delete(Guid id)
        {
            var query = "DELETE FROM department WHERE department.DepartmentId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Guid);
            using (var connection = db.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

    }
}
