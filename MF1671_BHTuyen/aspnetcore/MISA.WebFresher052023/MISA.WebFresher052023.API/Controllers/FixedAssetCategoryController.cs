using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.API.Controllers.Base;
using MISA.WebFresher052023.Application.Dto.FixedAssetCategory;
using MISA.WebFresher052023.Application.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher052023.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FixedAssetCategoryController : BaseController<FixedAssetCategoryDto, FixedAssetCategoryCreateDto, FixedAssetCategoryUpdateDto>
    {
        #region Constructor
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="fixedAssetCategoryService"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public FixedAssetCategoryController(IFixedAssetCategoryService fixedAssetCategoryService) : base(fixedAssetCategoryService)
        {
        } 
        #endregion
    }
}
