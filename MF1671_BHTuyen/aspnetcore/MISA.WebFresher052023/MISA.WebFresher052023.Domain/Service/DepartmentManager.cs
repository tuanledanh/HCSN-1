using MISA.WebFresher052023.Domain.Entity.Department;
using MISA.WebFresher052023.Domain.Interface.department;
using MISA.WebFresher052023.Domain.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain
{
    public class DepartmentManager : BaseManager<DepartmentEntity>, IDepartmentManager
    {
        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="departmentRespostory"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DepartmentManager(IDepartmentRepository departmentRespostory) : base(departmentRespostory)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Message lỗi
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string MessageError { get; set; } = "Mã bộ phận đã tồn tại"; 
        #endregion
    }
}
