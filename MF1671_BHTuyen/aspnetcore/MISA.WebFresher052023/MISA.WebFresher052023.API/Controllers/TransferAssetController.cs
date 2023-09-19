using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.Application.Dto.TransferAsset;
using MISA.WebFresher052023.Application.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher052023.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransferAssetController : ControllerBase
    {

        private readonly ITransferAssetService _transferAssetService;


        public TransferAssetController(ITransferAssetService transferAssetService)
        {
            _transferAssetService = transferAssetService;
        }

        [HttpGet()]
        [Route("NewCode")]
        public async Task<IActionResult> GetTransferAssetCodeNewAsync()
        {
            var transferAssetCodeNew = await _transferAssetService.GetTransferAssetCodeNewAsync();

            return StatusCode(StatusCodes.Status200OK, transferAssetCodeNew);
        }

        [HttpGet()]
        [Route("Paging")]
        public async Task<IActionResult> GetTransferAssetPagingAsync([FromQuery] TransferAssetFilterDto transferAssetFilterDto)
        {
            var transferAssetPagingDto = await _transferAssetService.GetTransferAssetPagingAsync(transferAssetFilterDto);

            return StatusCode(StatusCodes.Status200OK, transferAssetPagingDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransferAssetFullAsync([FromBody] TransferAssetCreateFull transferAssetCreateFull)
        {
            await _transferAssetService.CreateTransferAssetFullAsync(transferAssetCreateFull);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTransferAssetFullAsync([FromBody] TransferAssetUpdateFull transferAssetUpdateFull)
        {
            await _transferAssetService.UpdateTransferAssetFullAsync(transferAssetUpdateFull);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{transferAssetId}")]
        public async Task<IActionResult> DeleteTransferAssetAsync(Guid transferAssetId)
        {
            await _transferAssetService.DeleteAsync(transferAssetId);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteManyTransferAssetAsync([FromBody] List<Guid> transferAssetIds)
        {
            await _transferAssetService.DeleteManyAsync(transferAssetIds);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
