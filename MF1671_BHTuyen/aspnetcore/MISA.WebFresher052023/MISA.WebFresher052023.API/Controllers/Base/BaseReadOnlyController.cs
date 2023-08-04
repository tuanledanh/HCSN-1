using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.Application.Interface;

namespace MISA.WebFresher052023.API.Controllers.Base
{
    public class BaseReadOnlyController<TDto> : ControllerBase
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        private readonly IBaseReadOnlyService<TDto> _baseReadOnlyService;
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="baseReadOnlyService"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public BaseReadOnlyController(IBaseReadOnlyService<TDto> baseReadOnlyService)
        {
            _baseReadOnlyService = baseReadOnlyService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả các bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var dtos = await _baseReadOnlyService.GetAllAsync();
            return StatusCode(statusCode: StatusCodes
                .Status200OK, dtos);
        }

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="id">Id (Khóa chính) của đối tượng</param>
        /// <returns>Một bản ghi với Id truyền vào</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var dto = await _baseReadOnlyService.GetAsync(id);
            return StatusCode(StatusCodes.Status200OK, dto);
        }
        #endregion
    }
}
