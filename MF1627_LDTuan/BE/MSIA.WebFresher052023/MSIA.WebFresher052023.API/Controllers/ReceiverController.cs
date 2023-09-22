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
    public class ReceiverController : BaseSearchPagingController<Receiver, ReceiverModel, ReceiverDto, ReceiverCreateDto, ReceiverUpdateDto, ReceiverUpdateMultiDto>
    {
        private readonly IReceiverService _receiverService;
        public ReceiverController(IReceiverService receiverService, IMapper mapper) : base(receiverService, mapper)
        {
            _receiverService = receiverService;
        }

        /// <summary>
        /// Lấy ban giao nhận từ lần nhập trước
        /// </summary>
        /// <returns>Danh sách ban giao nhận mới nhất</returns>
        /// Created by: ldtuan (02/09/2023)
        [HttpGet("getNewest")]
        public async Task<IActionResult> GetNewestReceiver()
        {
            var receivers = await _receiverService.GetNewestReceiver();
            return StatusCode(StatusCodes.Status200OK, receivers);
        }
    }
}
