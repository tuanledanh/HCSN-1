using Application.DTO.Assett;
using AutoMapper;
using Domain.Entity;
using Domain.Model;

namespace Application.AutoMapper
{
    public class AssetProfile : Profile
    {
        public AssetProfile()
        {
            CreateMap<Asset, AssetDto>();
            CreateMap<AssetModel, AssetDto>();
            CreateMap<AssetCreateDto, Asset>();
            CreateMap<AssetUpdateDto, Asset>();
        }
    }
}
