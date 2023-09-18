using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.API.Controllers.Base;
using MISA.WebFresher052023.Application.Dto.FixedAsset;
using MISA.WebFresher052023.Application.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher052023.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FixedAssetController : BaseController<FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        private readonly IFixedAssetService _fixedAssetService;
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="fixedAssetService"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public FixedAssetController(IFixedAssetService fixedAssetService) : base(fixedAssetService)
        {
            _fixedAssetService = fixedAssetService;
        }
        #endregion

        #region Methods
        [HttpGet]
        [Route("Paging")]
        public async Task<IActionResult> GetFixedAssetPaginAsync([FromQuery] FixedAssetFilterDto fixedAssetFilterDto)
        {
            var fixedAssetPagingDto = await _fixedAssetService.GetFixedAssetPagingAsync(fixedAssetFilterDto);

            return StatusCode( StatusCodes.Status200OK, fixedAssetPagingDto);
        }
        
        [HttpPost]
        [Route("TransferPaging")]
        public async Task<IActionResult> GetFixedAssetTransferPaginAsync([FromBody] FixedAssetFilterDto fixedAssetFilterDto)
        {
            var fixedAssetPagingDto = await _fixedAssetService.GetFixedAssetTransferPagingAsync(fixedAssetFilterDto);

            return StatusCode( StatusCodes.Status200OK, fixedAssetPagingDto);
        }

        /// <summary>
        /// Lấy mã tài sản mới gợi ý
        /// </summary>
        /// <returns>Mã tài sản</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [HttpGet]
        [Route("NewCode")]
        public async Task<IActionResult> GetEstateCodeAsync()
        {
            var fixedAssetCode = await _fixedAssetService.GetFixedAssetCodeAsync();

            return StatusCode(statusCode: StatusCodes.Status200OK, fixedAssetCode);
        }

        /// <summary>
        /// Xuất danh sách tài sản ra file excel
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách Id của tài sản</param>
        /// <returns>File Excel trả về</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        [HttpPost()]
        [Route("Export")]
        public async Task<IActionResult> ExportFixedAssetAsync([FromBody] List<Guid> fixedAssetIds)
        {
            var contentFile = await _fixedAssetService.ExportListFixedAssetToExcelAsync(fixedAssetIds);

            return File(contentFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_Sach_Tai_San.xlsx");
        }

        /// <summary>
        /// Lọc tài sản theo tên, mã, phòng ban, loại tài sản
        /// </summary>
        /// <param name="fixedAssetCodeOrName">Tên hoặc mã tài sản</param>
        /// <param name="departmentName">Tên phòng ban</param>
        /// <param name="fixedAssetCategoryName">Tên loại tài sản</param>
        /// <returns>Danh sách tài sản</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        [HttpGet()]
        [Route("Filter")]
        public async Task<IActionResult> GetFixedAssetFilterAsync([FromQuery] string? fixedAssetCodeOrName, [FromQuery] string? departmentName, [FromQuery] string? fixedAssetCategoryName)
        {
            var fixedAssetFilter = await _fixedAssetService.GetFixedAssetFilterAsync(fixedAssetCodeOrName, departmentName, fixedAssetCategoryName);

            return StatusCode( StatusCodes.Status200OK, fixedAssetFilter);
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách các mã ID của các tài sản cần xóa</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [HttpDelete]
        public async Task<IActionResult> DeleteManyAsync([FromBody] List<Guid> fixedAssetIds)
        {
            await _fixedAssetService.DeleteManyAsync(fixedAssetIds);

            return StatusCode(StatusCodes.Status200OK);
        }
        #endregion
    }


}
