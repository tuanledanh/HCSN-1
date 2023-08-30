using Application.DTO;
using Application.Interface;
using AutoMapper;
using ClosedXML.Excel;
using Domain.Entity;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using MSIA.WebFresher052023.API.Controllers.Base;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FixedAssetController : BaseController<FixedAsset, FixedAssetModel, FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto, FixedAssetUpdateMultiDto>
    {
        private readonly IFixedAssetService _fixedAssetService;

        public FixedAssetController(IFixedAssetService fixedAssetService, IMapper mapper) : base(fixedAssetService, mapper)
        {
            _fixedAssetService = fixedAssetService;
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
        public async Task<IActionResult> GetList([FromQuery] int? pageNumber, [FromQuery] int? pageLimit, [FromQuery] string? filterName, [FromQuery] string? departId, [FromQuery] string? aTypeId)
        {
            var assetList = await _fixedAssetService.GetAllCustomAsync(pageNumber, pageLimit, filterName, departId, aTypeId);
            return StatusCode(StatusCodes.Status200OK, assetList);
        }

        /// <summary>
        /// Xuất thông tin ra file excel
        /// </summary>
        /// <param name="idsQuery">Danh sách id các bản ghi</param>
        /// <returns>File</returns>
        /// Created by: ldtuan (06/08/2023)
        [HttpGet("export")]
        public async Task<ActionResult> ExportExcel([FromQuery] string idsQuery)
        {
            var assetData = await _fixedAssetService.ExportExcel(idsQuery);
            using (XLWorkbook workBook = new XLWorkbook())
            {
                var workSheet = workBook.AddWorksheet(assetData, "Tài sản");
                workSheet.Columns().AdjustToContents();
                workSheet.Columns(7, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.CenterContinuous;
                workSheet.Columns(9, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                using (var memoryStream = new MemoryStream())
                {
                    workBook.SaveAs(memoryStream);
                    memoryStream.Position = 0;
                    var result = File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Asset.xlsx");
                    return result;
                }
            }
        }

        [HttpPost("insertMulti")]
        public async Task<ActionResult> InsertMulti([FromBody] List<FixedAssetCreateDto> fixedAssetCreateDtos)
        {
            var result = await _fixedAssetService.InsertMultiAsync(fixedAssetCreateDtos);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut("updateMulti")]
        public async Task<ActionResult> UpdateMulti([FromBody] List<FixedAssetUpdateMultiDto> fixedAssetUpdateMultipleDtos)
        {
            var result = await _fixedAssetService.UpdateMultiAsync(fixedAssetUpdateMultipleDtos);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}