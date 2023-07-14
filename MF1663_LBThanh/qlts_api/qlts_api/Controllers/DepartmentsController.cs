using Microsoft.AspNetCore.Mvc;
using qlts_api.DTO.Department;
using qlts_api.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace qlts_api.Controllers
{
    [Route("api/v1/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepo departmentsRepo;
        public DepartmentsController(IDepartmentRepo departmentsRepo)
        {
            this.departmentsRepo = departmentsRepo;
        }
        /// <summary>
        /// xử lý kết quả trả về của api lấy tất cả phòng ban
        /// Trả về status code 200 và tất cả phòng ban nếu thành công
        /// Trả về status code 500 cà thông báo lỗi nếu gặp phải bug phía server
        /// Created by: LB.Thành (13/07/2023)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var departments = await departmentsRepo.GetAllAsync();
                return StatusCode(200, departments);
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// xử lý kết quả trả về của api lấy phòng ban theo Id
        /// Trả về status code 200 và 1 phòng ban nếu thành công
        /// Trả về status code 500 cà thông báo lỗi nếu gặp phải bug phía server
        /// Created by: LB.Thành (13/07/2023)
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                var department = await departmentsRepo.Get(id);
                return StatusCode(200, department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// tạo ra 1 phòng ban mới
        /// param: request body
        /// Trả về status code 201 và thông báo thành công 
        /// Trả về status code 500 cà thông báo lỗi nếu gặp phải bug phía server
        /// Created by: LB.Thành (13/07/2023)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] DepartmentRequest value)
        {
            try
            {
                var department = await departmentsRepo.CreateAsync(value);
                return StatusCode(201, "Thêm thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật 1 phòng ban
        /// param = id (id của phòng ban muốn cập nhật)
        /// param = request ( dùng để truy cập các property trong DepartmentRequest)
        /// Trả về status code 201 và thông báo thành công 
        /// Trả về status code 500 cà thông báo lỗi nếu gặp phải bug phía server
        /// Created by: LB.Thành (13/07/2023)
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] DepartmentRequest request)
        {
            try
            {
                await departmentsRepo.Update(id, request);
                return StatusCode(200, "cập nhật thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Xóa 1 phòng ban
        /// param = id (id của phòng ban muốn xóa)
        /// Trả về status code 201 và thông báo thành công 
        /// Trả về status code 500 cà thông báo lỗi nếu gặp phải bug phía server
        /// Created by: LB.Thành (13/07/2023)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await departmentsRepo.Delete(id);
                return StatusCode(200, "Xóa thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
