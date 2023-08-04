using AutoMapper;
using MISA.WebFresher052023.Application.Dto.FixedAssetCategory;
using MISA.WebFresher052023.Application.Interface;
using MISA.WebFresher052023.Application.Service.Base;
using MISA.WebFresher052023.Domain.Entity.FixedAssetCategory;
using MISA.WebFresher052023.Domain.Interface.FixedAssetCategory;

namespace MISA.WebFresher052023.Application.Service
{
    public class FixedAssetCategoryService : BaseService<FixedAssetCategoryEntity, FixedAssetCategoryDto, FixedAssetCategoryCreateDto, FixedAssetCategoryUpdateDto>, IFixedAssetCategoryService
    {
        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="fixedAssetCategoryRepository"></param>
        /// <param name="fixedAssetCategoryManager"></param>
        /// <param name="mapper"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public FixedAssetCategoryService(IFixedAssetCategoryRepository fixedAssetCategoryRepository, IFixedAssetCategoryManager fixedAssetCategoryManager, IMapper mapper) : base(fixedAssetCategoryRepository, fixedAssetCategoryManager, mapper)
        {
        } 
        #endregion

    }
}
