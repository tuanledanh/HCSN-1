using Application.DTO.Assettype;
using AutoMapper;
using Domain.Entity;
using Domain.Model;

namespace Application.AutoMapper
{
    public class AssetTypeProfile : Profile
    {
        public AssetTypeProfile()
        {
            CreateMap<AssetType, AssetTypeDto>();
            CreateMap<AssetTypeModel, AssetTypeDto>();
            CreateMap<AssetTypeCreateDto, AssetType>();
            CreateMap<AssetTypeUpdateDto, AssetType>();
        }
    }
}
