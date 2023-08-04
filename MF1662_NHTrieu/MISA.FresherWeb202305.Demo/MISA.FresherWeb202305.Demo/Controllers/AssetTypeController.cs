using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.FresherWeb202305.Demo.Application.Dto;
using MISA.FresherWeb202305.Demo.Application.Interface;
using MISA.FresherWeb202305.Demo.Application.Interface.Base;
using MISA.FresherWeb202305.Demo.Domain.Entity;

namespace MISA.FresherWeb202305.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetTypeController : BaseCodeController<AssetTypeDto,AssetTypeCreateDto,AssetTypeUpdateDto>
    {
        private readonly IAssetTypeServices _assetTypeServices;
        public AssetTypeController(IAssetTypeServices baseService) : base(baseService)
        {
            _assetTypeServices= baseService;
        }
    }
}
