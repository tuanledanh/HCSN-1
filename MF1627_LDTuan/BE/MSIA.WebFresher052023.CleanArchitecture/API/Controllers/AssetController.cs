using Application.DTO.Assett;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        #region Fields
        private readonly IAssetService _assetService;
        #endregion

        #region Constructor
        public AssetController(IConfiguration configuration, IAssetService assetService)
        {
            _assetService = assetService;
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
            var asset = await _assetService.GetAsync(code);
            return StatusCode(StatusCodes.Status200OK, asset);

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
            var assetList = await _assetService.GetAllAsync(pageNumber, pageLimit, filterName);
            return StatusCode(StatusCodes.Status200OK, assetList);
        }

        /// <summary>
        /// Api thêm mới 1 bản ghi
        /// </summary>
        /// <param name="assetCreateDto">Dữ liệu của bản ghi muốn thêm mới</param>
        /// <returns>True hoặc false tương ứng với thêm mới thành công hay thất bại</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AssetCreateDto assetCreateDto)
        {
            var result = await _assetService.InsertAsync(assetCreateDto);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Api cập nhật 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi muốn cập nhật</param>
        /// <param name="assetUpdateDto">Dữ liệu của bản ghi muốn cập nhật</param>
        /// <returns>True hoặc false tương ứng với cập nhật thành công hay thất bại</returns>
        /// Created by: ldtuan (18/07/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] Guid id, [FromBody] AssetUpdateDto assetUpdateDto)
        {
            var result = await _assetService.UpdateAsync(id, assetUpdateDto);
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
            var result = await _assetService.DeleteAsync(id);

            return Ok(result);
        }
        #endregion
    }
}