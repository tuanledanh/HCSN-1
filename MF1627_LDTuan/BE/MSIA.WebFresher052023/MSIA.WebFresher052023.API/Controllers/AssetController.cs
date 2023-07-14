using Microsoft.AspNetCore.Mvc;
using MSIA.WebFresher052023.BL_Services.DTO.Assett;
using MSIA.WebFresher052023.BL_Services.DTO.Assettype;
using MSIA.WebFresher052023.BL_Services.DTO.Depart;
using MSIA.WebFresher052023.BL_Services.Service.Assett;
using MSIA.WebFresher052023.DL_Repositories.Entity;

namespace MSIA.WebFresher052023.API.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        public AssetController(IConfiguration configuration, IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetAsync(string code)
        {
            try
            {
                var assetDto = await _assetService.GetAsync(code);
                return StatusCode(StatusCodes.Status200OK, assetDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int? pageNumber, [FromQuery] int? pageLimit, [FromQuery] string? filterName)
        {
            try
            {
                List<AssetDto> assets;
                if (pageNumber == null && pageLimit == null)
                {
                    assets = await _assetService.GetAllAsync();
                }
                else
                {
                    if (filterName == null)
                    {
                        filterName = "";
                    }
                    assets = await _assetService.GetFilterAsync(pageNumber, pageLimit, filterName);
                }
                return StatusCode(StatusCodes.Status200OK, assets);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AssetCreateDto assetCreateDto)
        {
            try
            {
                var result = await _assetService.PostAsync(assetCreateDto);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] AssetUpdateDto assetUpdateDto)
        {
            try
            {
                var result = await _assetService.PutAsync(id, assetUpdateDto);
                var asset = await _assetService.GetAsync(assetUpdateDto.AssetCode);
                return StatusCode(StatusCodes.Status200OK, asset);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
