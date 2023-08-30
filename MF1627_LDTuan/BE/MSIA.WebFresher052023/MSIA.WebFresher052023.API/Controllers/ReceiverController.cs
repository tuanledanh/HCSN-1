using Application.DTO;
using Application.Interface;
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
        public ReceiverController(IReceiverService receiverService, IMapper mapper) : base(receiverService, mapper)
        {
        }
    }
}
