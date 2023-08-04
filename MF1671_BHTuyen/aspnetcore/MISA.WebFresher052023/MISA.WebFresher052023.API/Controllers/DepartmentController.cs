using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.API.Controllers.Base;
using MISA.WebFresher052023.Application.Dto.Department;
using MISA.WebFresher052023.Application.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher052023.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DepartmentController : BaseController<DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="departmentService"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DepartmentController(IDepartmentService departmentService) : base(departmentService)
        {
        } 
        #endregion
    }
}
