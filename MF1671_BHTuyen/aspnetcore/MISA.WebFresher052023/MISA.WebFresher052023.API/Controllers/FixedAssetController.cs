using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.API.Controllers.Base;
using MISA.WebFresher052023.API.ExcelHelper;
using MISA.WebFresher052023.Application.Dto.FixedAsset;
using MISA.WebFresher052023.Application.Interface;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;

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
        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách các mã ID của các tài sản cần xóa</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [HttpDelete]
        public async Task DeleteManyAsync([FromBody] List<string> fixedAssetIds)
        {
            await _fixedAssetService.DeleteManyAsync(fixedAssetIds);
        }

        /// <summary>
        /// Lấy mã tài sản mới gợi ý
        /// </summary>
        /// <returns>Mã tài sản</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        [HttpGet]
        [Route("Code")]
        public async Task<IActionResult> GetEstateCodeAsync()
        {
            var fixedAssetCode = await _fixedAssetService.GetFixedAssetCodeAsync();

            return StatusCode(statusCode: StatusCodes.Status200OK, fixedAssetCode);
        }

        /// <summary>
        /// Lấy ra danh sách tài sản theo trang và bộ lọc
        /// </summary>
        /// <param name="fixedAssetFilterDto">Các biến lọc</param>
        /// <returns>Danh sách tài sản và tổng số trang</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        [HttpGet()]
        [Route("Paging")]
        public async Task<IActionResult> GetFixedAssetPagingAsync([FromQuery] FixedAssetFilterDto fixedAssetFilterDto)
        {
            var fixedAssetPagingDto = await _fixedAssetService.GetFixedAssetPagingAsync(fixedAssetFilterDto);

            return StatusCode(statusCode: StatusCodes.Status200OK, fixedAssetPagingDto);
        }

        /// <summary>
        /// Xuất danh sách tài sản ra file excel
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách Id của tài sản</param>
        /// <returns>File Excel trả về</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        [HttpPost()]
        [Route("Export")]
        public async Task<IActionResult> ExportFixedAssetAsync([FromBody] List<string> fixedAssetIds)
        {
            var fixedAssetExcels = await _fixedAssetService.FindManyByIdAsync(fixedAssetIds);

            var templateFile = Path.Combine(Directory.GetCurrentDirectory(), "TemplateExcel", "TemplateListFixedAsset.xlsx");

            var stream =  ExportToExcelHelper.UpdateDataIntoExcelTemplate(fixedAssetExcels, templateFile);

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_Sach_Tai_San.xlsx");
        }
        #endregion
    }


}
