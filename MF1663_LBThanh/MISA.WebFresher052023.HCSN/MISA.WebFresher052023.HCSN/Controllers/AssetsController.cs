using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Application.DTO.AssetDTO;
using MISA.WebFresher052023.HCSN.Application.Interface;
using MISA.WebFresher052023.HCSN.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher052023.HCSN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        #region Fields
        private readonly IAssetService _assetService;
        #endregion

        #region Constructor
        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy toàn bộ tài sản
        /// </summary>
        /// <returns>toàn bộ tài sản</returns>
        /// Created by: LB.Thành (16/07/2023)
        [HttpGet]
        public async Task<IEnumerable<AssetDto>> GetAllAsync()
        {
            var result = await _assetService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// Lấy 1 tài sản theo ID
        /// </summary>
        /// <param name="id">Id của tài sản cần tìm</param>
        /// <returns>1 tài sản thỏa mãn</returns>
        /// Created by: LB.Thành (16/07/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _assetService.GetAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Thêm một tài sản mới 
        /// </summary>
        /// <param name="request">Thông tin tài sản cần được tạo.</param>
        /// <returns>ActionResult: Kết quả của việc thêm tài sản.</returns>
        /// Created by: LB.Thành (16/07/2023)
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] AssetCreateDto request)
        {
            await _assetService.CreateAsync(request);
            return StatusCode(StatusCodes.Status201Created, "Thêm thành công");
        }

        /// <summary>
        /// Cập nhật 1 tài sản 
        /// </summary>
        /// <param name="request">Thông tin tài sản cần được cập nhật</param>
        /// <returns>ActionResult: Kết quả của việc cập nhật.</returns>
        /// Created by: LB.Thành (16/07/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpadteAsync([FromBody] AssetUpdateDto request)
        {
            await _assetService.UpdateAsync(request);
            return StatusCode(StatusCodes.Status200OK, "Sửa thành công");
        }

        /// <summary>
        /// Xóa 1 tài sản
        /// </summary>
        /// <param name="id">Id của tài sản muốn xóa</param>
        /// Created by: LB.Thành (16/07/2023)
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _assetService.DeleteAsync(id);
        }
        #endregion
    }
}
