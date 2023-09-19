using MISA.WebFresher052023.Domain.Entity.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface.department
{
    public interface IDepartmentRepository : IBaseRepository<DepartmentEntity>
    {
        #region Methods
        /// <summary>
        /// Tìm kiếm nhiều phòng ban theo mã id của phòng ban
        /// </summary>
        /// <param name="departmentIds">Danh sách mã Id các phòng ban cần tìm kiếm</param>
        /// <returns>Danh sách phòng ban</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<IEnumerable<DepartmentEntity>> FindManyAsync(List<Guid> departmentIds); 
        #endregion
    }
}
