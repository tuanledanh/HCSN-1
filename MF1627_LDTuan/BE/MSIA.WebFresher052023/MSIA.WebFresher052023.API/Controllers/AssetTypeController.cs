using Microsoft.AspNetCore.Mvc;
using MSIA.WebFresher052023.BL_Services.DTO.Assett;
using MSIA.WebFresher052023.BL_Services.DTO.Assettype;
using MSIA.WebFresher052023.BL_Services.Service.Assettype;
using MSIA.WebFresher052023.DL_Repositories.Entity;

namespace MSIA.WebFresher052023.API.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class AssetTypeController : ControllerBase
    {
        private readonly IAssetTypeService _assetTypeService;
        public AssetTypeController(IConfiguration configuration, IAssetTypeService assetTypeService)
        {
            _assetTypeService = assetTypeService;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetAsync(string code)
        {
            try
            {
                var assetTypeDto = await _assetTypeService.GetAsync(code);
                return StatusCode(StatusCodes.Status200OK, assetTypeDto);
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
                List<AssetTypeDto> assetTypes;
                if (pageNumber == null && pageLimit == null)
                {
                    assetTypes = await _assetTypeService.GetAllAsync();
                }
                else
                {
                    if (filterName == null)
                    {
                        filterName = "";
                    }
                    assetTypes = await _assetTypeService.GetFilterAsync(pageNumber, pageLimit, filterName);
                }
                return StatusCode(StatusCodes.Status200OK, assetTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AssetTypeCreateDto assetTypeCreateDto)
        {
            try
            {
                var result = await _assetTypeService.PostAsync(assetTypeCreateDto);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] AssetTypeUpdateDto assetTypeUpdateDto)
        {
            try
            {
                var result = await _assetTypeService.PutAsync(id, assetTypeUpdateDto);
                var assetType = await _assetTypeService.GetAsync(assetTypeUpdateDto.AssetTypeCode);
                return StatusCode(StatusCodes.Status200OK, assetType);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}

