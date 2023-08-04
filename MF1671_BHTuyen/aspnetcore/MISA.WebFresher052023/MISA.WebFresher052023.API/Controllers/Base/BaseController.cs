using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.Application.Interface;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher052023.API.Controllers.Base
{
    public class BaseController<TDto, TCreateDto, TUpdateDto> : BaseReadOnlyController<TDto>
    {
        #region Fields
        /// <summary>
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        private readonly IBaseService<TDto, TCreateDto, TUpdateDto> _baseService;
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public BaseController(IBaseService<TDto, TCreateDto, TUpdateDto> baseService) : base(baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tạo mới một bản ghi
        /// </summary>
        /// <param name="createDto">Thông tin đối tượng cần thêm (khóa chính sẽ được cấp tự động - không cần gửi thông tin khóa chính)</param>
        /// <returns>IActionResult</returns>
        [HttpPost()]
        public async Task<IActionResult> CreateAsync([FromBody] TCreateDto createDto)
        {
            await _baseService.CreateAsync(createDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="id">Id (Khóa chính) của đối tượng</param>
        /// <param name="updateDto">Đối tượng đã được chỉnh sửa</param>
        /// <returns>IActionResult</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] TUpdateDto updateDto)
        {
            await _baseService.UpdateAsync(id, updateDto);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="id">Id (Khóa chính) của đối tượng</param>
        /// <returns>IActionResult</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _baseService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }
        #endregion
    }
}
