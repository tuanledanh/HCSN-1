using Application.DTO.Assett;
using Application.DTO.Depart;
using Application.Interface;
using Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        #endregion

        #region Constructor
        public DepartmentController(IConfiguration configuration, IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Api lấy 1 bản ghi theo mã code
        /// </summary>
        /// <param name="code">Mã code của bản ghi cần tìm</param>
        /// <returns>Một bản ghi</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpGet("{code}")]
        public async Task<IActionResult> GetAsync([FromRoute] string code)
        {
            var department = await _departmentService.GetAsync(code);
            return StatusCode(StatusCodes.Status200OK, department);

        }

        /// <summary>
        /// Api lấy danh sách bản ghi, có phân trang và lọc
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageLimit">Số bản ghi tối đa</param>
        /// <param name="filterName">Tên của bản ghi để thực hiện lọc</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int? pageNumber, [FromQuery] int? pageLimit, [FromQuery] string? filterName)
        {
            var departmentList = await _departmentService.GetAllAsync(pageNumber, pageLimit, filterName);
            return StatusCode(StatusCodes.Status200OK, departmentList);
        }

        /// <summary>
        /// Api thêm mới 1 bản ghi
        /// </summary>
        /// <param name="departmentCreateDto">Dữ liệu của bản ghi muốn thêm mới</param>
        /// <returns>True hoặc false tương ứng với thêm mới thành công hay thất bại</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] DepartmentCreateDto departmentCreateDto)
        {
            var result = await _departmentService.InsertAsync(departmentCreateDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Api cập nhật 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi muốn cập nhật</param>
        /// <param name="departmentUpdateDto">Dữ liệu của bản ghi muốn cập nhật</param>
        /// <returns>True hoặc false tương ứng với cập nhật thành công hay thất bại</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] DepartmentUpdateDto departmentUpdateDto)
        {
            var result = await _departmentService.UpdateAsync(id, departmentUpdateDto);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Api xóa 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi muốn xóa</param>
        /// <returns>True hoặc false tương ứng với xóa thành công hay thất bại</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
        {
            var result = await _departmentService.DeleteAsync(id);

            return Ok(result);
        } 
        #endregion
    }
}
