using AutoMapper;
using MISA.WebFresher052023.Application.Dto.Department;
using MISA.WebFresher052023.Application.Interface;
using MISA.WebFresher052023.Application.Service.Base;
using MISA.WebFresher052023.Domain.Entity.Department;
using MISA.WebFresher052023.Domain.Interface.department;

namespace MISA.WebFresher052023.Application.Service
{
    public class DepartmentService : BaseService<DepartmentEntity, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>, IDepartmentService
    {
        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="departmentRespostory"></param>
        /// <param name="departmentManger"></param>
        /// <param name="mapper"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DepartmentService(IDepartmentRepository departmentRespostory, IDepartmentManager departmentManger, IMapper mapper) : base(departmentRespostory, departmentManger, mapper)
        {
        } 
        #endregion



    }
}
