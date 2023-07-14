using AutoMapper;
using MSIA.WebFresher052023.BL_Services.DTO.Depart;
using MSIA.WebFresher052023.DL_Repositories.Entity;
using MSIA.WebFresher052023.DL_Repositories.Repositories.Depart;

namespace MSIA.WebFresher052023.BL_Services.Service.Depart
{
    public class DepartmentService : BaseService<Department, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>, IDepartmentService
    {
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper) : base(departmentRepository, mapper)
        {
        }
    }
}
