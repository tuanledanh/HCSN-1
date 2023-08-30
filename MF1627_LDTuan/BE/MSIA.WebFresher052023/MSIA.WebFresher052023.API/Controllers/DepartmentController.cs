using Application.DTO;
using Application.Interface;
using AutoMapper;
using Domain.Entity;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using MSIA.WebFresher052023.API.Controllers.Base;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : BaseSearchPagingController<Department, DepartmentModel, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto, DepartmentUpdateMultiDto>
    {
        public DepartmentController(IDepartmentService departmentService, IMapper mapper) : base(departmentService, mapper)
        {
        }
    }
}
