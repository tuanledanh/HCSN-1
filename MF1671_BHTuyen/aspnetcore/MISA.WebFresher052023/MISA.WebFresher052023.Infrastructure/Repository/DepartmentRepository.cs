using Dapper;
using MISA.WebFresher052023.Domain.Entity.Department;
using MISA.WebFresher052023.Domain.Interface.department;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using MISA.WebFresher052023.Infrastructure.Repository.Base;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<DepartmentEntity>, IDepartmentRepository
    {
        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DepartmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Ghi đè tên bảng
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023) 
        public override string TableNameProc { get; protected set; } = "Department";

        /// <summary>
        /// Ghi đè tên bảng dạng số nhiều
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string TableNamesProc { get; protected set; } = "Departments";

        /// <summary>
        /// Ghi đè tên khóa chính
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string TableNameId { get; protected set; } = "DepartmentId";

        #endregion

        #region Methods
        public async Task<IEnumerable<DepartmentEntity>> FindManyDepartmentAsync(List<Guid> departmentIds)
        {
            var query = $"SELECT * FROM department WHERE DepartmentId IN @DepartmentIds";

            var parameters = new DynamicParameters();
            parameters.Add("@DepartmentIds", departmentIds.Select(departmentId=>departmentId.ToString()));

            var departments =await _unitOfWork.Connection.QueryAsync<DepartmentEntity>(query,  parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return departments;
        } 
        #endregion

    }
}
