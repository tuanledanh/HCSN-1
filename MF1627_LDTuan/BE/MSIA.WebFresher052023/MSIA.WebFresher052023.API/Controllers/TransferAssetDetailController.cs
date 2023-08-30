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
    public class TransferAssetDetailController : BaseSearchPagingController<TransferAssetDetail, TransferAssetDetailModel, TransferAssetDetailDto, TransferAssetDetailCreateDto, TransferAssetDetailUpdateDto, TransferAssetDetailUpdateMultiDto>
    {
        public TransferAssetDetailController(ITransferAssetDetailService transferAssetDetailService, IMapper mapper) : base(transferAssetDetailService, mapper)
        {
        }
    }
}
