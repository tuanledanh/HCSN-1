using Application.DTO;
using Application.Interface;
using Application.Service;
using AutoMapper;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using MSIA.WebFresher052023.API.Controllers.Base;
using MSIA.WebFresher052023.Domain.Entity;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransferAssetController : BaseController<TransferAsset, TransferAssetModel, TransferAssetDto, TransferAssetCreateDto, TransferAssetUpdateDto, TransferAssetUpdateMultiDto>
    {
        private readonly ITransferAssetService _transferAssetService;

        public TransferAssetController(ITransferAssetService transferAssetService, IMapper mapper) : base(transferAssetService, mapper)
        {
            _transferAssetService = transferAssetService;
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
            var transferAssetList = await _transferAssetService.GetAllCustomAsync(pageNumber, pageLimit, filterName);
            return StatusCode(StatusCodes.Status200OK, transferAssetList);
        }

        /// <summary>
        /// Lấy chứng từ mới nhất của các tài sản trong chứng từ đích
        /// </summary>
        /// <param name="transferId">Id của chứng từ</param>
        /// <returns>Ném ra thông báo lỗi nếu chứng từ mới nhất không phải là chứng từ đích, tức là tài sản bên trong có chứng từ phát sinh</returns>
        /// Created by: ldtuan (12/09/2023)
        [HttpPost("GetNewest/{transferId}")]
        public async Task<IActionResult> GetNewestTransferAsset([FromRoute] Guid transferId)
        {
            await _transferAssetService.GetNewestTransferAsset(transferId);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
