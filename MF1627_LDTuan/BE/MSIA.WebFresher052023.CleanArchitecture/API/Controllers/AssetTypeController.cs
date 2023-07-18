using Application.DTO.Assettype;
using Application.DTO.Depart;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AssetTypeController : ControllerBase
    {
        #region Fields
        private readonly IAssetTypeService _assetTypeService;
        #endregion

        #region Constructor
        public AssetTypeController(IConfiguration configuration, IAssetTypeService assetTypeService)
        {
            _assetTypeService = assetTypeService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Api lấy 1 bản ghi theo mã code
        /// </summary>
        /// <param name="code">Mã code của bản ghi cần tìm</param>
        /// <returns>Một bản ghi</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpGet("{code}")]
        public async Task<IActionResult> GetAsync([FromRoute] string code)
        {
            var assetType = await _assetTypeService.GetAsync(code);
            return StatusCode(StatusCodes.Status200OK, assetType);

        }


        /// <summary>
        /// Api lấy danh sách bản ghi, có phân trang và lọc
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageLimit">Số bản ghi tối đa</param>
        /// <param name="filterName">Tên của bản ghi để thực hiện lọc</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int? pageNumber, [FromQuery] int? pageLimit, [FromQuery] string? filterName)
        {
            var assetTypeList = await _assetTypeService.GetAllAsync(pageNumber, pageLimit, filterName);
            return StatusCode(StatusCodes.Status200OK, assetTypeList);
        }


        /// <summary>
        /// Api thêm mới 1 bản ghi
        /// </summary>
        /// <param name="assetTypeCreateDto">Dữ liệu của bản ghi muốn thêm mới</param>
        /// <returns>True hoặc false tương ứng với thêm mới thành công hay thất bại</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AssetTypeCreateDto assetTypeCreateDto)
        {
            var result = await _assetTypeService.InsertAsync(assetTypeCreateDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Api cập nhật 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi muốn cập nhật</param>
        /// <param name="assetTypeUpdateDto">Dữ liệu của bản ghi muốn cập nhật</param>
        /// <returns>True hoặc false tương ứng với cập nhật thành công hay thất bại</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] AssetTypeUpdateDto assetTypeUpdateDto)
        {
            var result = await _assetTypeService.UpdateAsync(id, assetTypeUpdateDto);
            return StatusCode(StatusCodes.Status200OK, result);
        }


        /// <summary>
        /// Api xóa 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi muốn xóa</param>
        /// <returns>True hoặc false tương ứng với xóa thành công hay thất bại</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
        {
            var result = await _assetTypeService.DeleteAsync(id);

            return Ok(result);
        }
        #endregion
    }
}