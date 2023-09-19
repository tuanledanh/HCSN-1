using AutoMapper;
using MISA.WebFresher052023.Application.Dto.FixedAsset;
using MISA.WebFresher052023.Domain.Entity.FixedAsset;
using MISA.WebFresher052023.Domain.Model.FixedAsset;

namespace MISA.WebFresher052023.Application.Mapper
{
    public class FixedAssetProfile : Profile
    {
        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public FixedAssetProfile()
        {
            CreateMap<FixedAssetEntity, FixedAssetDto>();

            CreateMap<FixedAssetEntity, FixedAssetExcelModel>();

            CreateMap<FixedAssetFilterDto, FixedAssetFilterModel>();

            CreateMap<FixedAssetPagingModel, FixedAssetPagingDto>().ForMember(
                destination => destination.FixedAssets, opt => opt.MapFrom(src => src.FixedAssets));

            CreateMap<FixedAssetCreateDto, FixedAssetEntity>();

            CreateMap<FixedAssetUpdateDto, FixedAssetEntity>();

        }
        #endregion
    }
}
