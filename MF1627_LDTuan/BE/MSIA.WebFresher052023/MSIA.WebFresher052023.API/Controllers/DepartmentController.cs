using Microsoft.AspNetCore.Mvc;
using MSIA.WebFresher052023.BL_Services.DTO.Assett;
using MSIA.WebFresher052023.BL_Services.DTO.Depart;
using MSIA.WebFresher052023.BL_Services.Service.Depart;
using MSIA.WebFresher052023.DL_Repositories.Entity;

namespace MSIA.WebFresher052023.API.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IConfiguration configuration, IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetAsync(string code)
        {
            try
            {
                var departmentDto = await _departmentService.GetAsync(code);
                return StatusCode(StatusCodes.Status200OK, departmentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int? pageNumber, [FromQuery] int? pageLimit, [FromQuery] string? filterName)
        {
            try
            {
                List<DepartmentDto> departments;
                if (pageNumber == null && pageLimit == null)
                {
                    departments = await _departmentService.GetAllAsync();
                }
                else
                {
                    if (filterName == null)
                    {
                        filterName = "";
                    }
                    departments = await _departmentService.GetFilterAsync(pageNumber, pageLimit, filterName);
                }
                return StatusCode(StatusCodes.Status200OK, departments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] DepartmentCreateDto departmentCreateDto)
        {
            try
            {
                var result = await _departmentService.PostAsync(departmentCreateDto);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] DepartmentUpdateDto departmentUpdateDto)
        {
            try
            {
                var result = await _departmentService.PutAsync(id, departmentUpdateDto);
                var department = await _departmentService.GetAsync(departmentUpdateDto.DepartmentCode);
                return StatusCode(StatusCodes.Status200OK, department);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
