using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.Application.Dto.TransferAssetDetail;
using MISA.WebFresher052023.Application.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher052023.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransferAssetDetailController : ControllerBase
    {

        private readonly ITransferAssetDetailService _transferAssetDetailService;

        public TransferAssetDetailController(ITransferAssetDetailService transferAssetDetailService)
        {
            _transferAssetDetailService = transferAssetDetailService;
        }

        [HttpGet]
        [Route("Paging")]
        public async Task<IActionResult> GetTransferAssetDetailPagingAsync([FromQuery] TransferAssetDetailFilterDto transferAssetDetailFilterDto)
        {
            var transferAssetDetailPagingDto = await _transferAssetDetailService.GetTransferAssetDetailPagingAsync(transferAssetDetailFilterDto);

            return StatusCode(StatusCodes.Status200OK, transferAssetDetailPagingDto);

        }

        [HttpGet("{transferAssetId}")]
        public async Task<IActionResult> GetManyByTransferAssetIdAsync(Guid transferAssetId)
        {
            var transferAssetDetails = await _transferAssetDetailService.GetManyByTransferAssetIdAsync(transferAssetId);

            return StatusCode(StatusCodes.Status200OK, transferAssetDetails);
        }
    }
}
