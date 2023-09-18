using AutoMapper;
using MISA.WebFresher052023.Application.Dto.FixedAssetCategory;
using MISA.WebFresher052023.Domain.Entity.FixedAssetCategory;

namespace MISA.WebFresher052023.Application.Mapper
{
    public class FixedAssetCategoryProfile : Profile
    {
        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public FixedAssetCategoryProfile()
        {
            CreateMap<FixedAssetCategoryEntity, FixedAssetCategoryDto>();
            CreateMap<FixedAssetCategoryCreateDto, FixedAssetCategoryEntity>();
            CreateMap<FixedAssetCategoryUpdateDto, FixedAssetCategoryEntity>();
        } 
        #endregion
    }
}
