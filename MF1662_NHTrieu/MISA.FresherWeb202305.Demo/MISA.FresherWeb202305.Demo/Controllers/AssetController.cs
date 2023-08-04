using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.FresherWeb202305.Demo.Application.Dto;
using MISA.FresherWeb202305.Demo.Application.Interface;
using MISA.FresherWeb202305.Demo.Application.Interface.Base;
using MISA.FresherWeb202305.Demo.Application.Services;
using MISA.FresherWeb202305.Demo.Domain.Entity;



namespace MISA.FresherWeb202305.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : BaseCodeController<AssetDto, AssetCreateDto, AssetUpdateDto>
    {
        private readonly IAssetServices _assetServices; 
        public AssetController(IAssetServices assetServices) : base(assetServices)
        {
            _assetServices = assetServices;
        }
        [HttpGet("Filter")]
        public async Task<IActionResult> FilterAsync([FromQuery] string? search, [FromQuery] int? pageNumber, [FromQuery] int? pageSize, [FromQuery] string ? departmentCode, [FromQuery] string? assetTypeCode)
        {
            var pageAsset = await _assetServices.FilterAsync(search, pageNumber, pageSize,departmentCode,assetTypeCode);

            return Ok(value: pageAsset);
        }
    }
}
