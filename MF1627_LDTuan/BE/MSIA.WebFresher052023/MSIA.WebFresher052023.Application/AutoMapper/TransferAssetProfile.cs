using Application.DTO;
using AutoMapper;
using Domain.Entity;
using Domain.Model;
using MSIA.WebFresher052023.Application.DTO;
using MSIA.WebFresher052023.Application.DTO.TransferAssett;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Model;

namespace Application.AutoMapper
{
    public class TransferAssetProfile : Profile
    {
        public TransferAssetProfile()
        {
            CreateMap<TransferAsset, TransferAssetDto>();
            CreateMap<TransferAsset, TransferAssetModel>();
            CreateMap<TransferAssetModel, TransferAssetDto>()
                .ForMember(dest => dest.FixedAssetTransfers, opt => opt.Ignore());
            CreateMap<TransferAssetCreateDto, TransferAsset>();
            CreateMap<TransferAssetUpdateDto, TransferAsset>();
            CreateMap<TransferAssetCreateDto, TransferAssetModel>();
            CreateMap<TransferAssetUpdateDto, TransferAssetModel>();

            CreateMap<FixedAssetTransferModel, FixedAssetTransferDto>()
                .ForMember(dest => dest.TransferAsset, opt => opt.Ignore());
            CreateMap<ReceiverTransferModel, ReceiverTransferDto>();

            CreateMap<TransferAssetDeleteModel, TransferAssetDeleteDto>();
        }
    }
}
