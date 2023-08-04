using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.FresherWeb202305.Demo.Application.Dto;
using MISA.FresherWeb202305.Demo.Application.Interface;
using MISA.FresherWeb202305.Demo.Application.Interface.Base;
using MISA.FresherWeb202305.Demo.Domain.Entity;

namespace MISA.FresherWeb202305.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : BaseCodeController<DepartmentDto,DepartmentCreateDto,DepartmentUpdateDto>
    {
        private readonly IDepartmentServices _departmentServices;

        public DepartmentController(IDepartmentServices departmentServices) : base(departmentServices)
        {
            _departmentServices = departmentServices;
        }       
    }
}
