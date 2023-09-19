using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.Application.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher052023.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReceiversController : ControllerBase
    {
        private readonly IReceiverService _receiverService;

        public ReceiversController(IReceiverService receiverService)
        {
            _receiverService = receiverService;
        }

        [HttpGet("{transferAssetId}")]
        public async Task<IActionResult> GetReceiversAsync(Guid transferAssetId)
        {
            var receivers = await _receiverService.GetManyByTransferAssetIdAsync(transferAssetId);

            return StatusCode(StatusCodes.Status200OK, receivers);
        }
    }
}
