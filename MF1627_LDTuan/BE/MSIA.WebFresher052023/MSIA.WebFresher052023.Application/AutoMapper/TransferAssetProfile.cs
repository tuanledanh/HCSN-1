using Application.DTO;
using AutoMapper;
using Domain.Entity;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Entity;

namespace Application.AutoMapper
{
    public class TransferAssetProfile : Profile
    {
        public TransferAssetProfile()
        {
            CreateMap<TransferAsset, TransferAssetDto>();
            CreateMap<TransferAsset, TransferAssetModel>();
            CreateMap<TransferAssetModel, TransferAssetDto>();
            CreateMap<TransferAssetCreateDto, TransferAsset>();
            CreateMap<TransferAssetUpdateDto, TransferAsset>();
            CreateMap<TransferAssetCreateDto, TransferAssetModel>();
            CreateMap<TransferAssetUpdateDto, TransferAssetModel>();
        }
    }
}
