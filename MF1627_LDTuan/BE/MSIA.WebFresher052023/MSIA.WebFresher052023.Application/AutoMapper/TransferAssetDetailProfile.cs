using Application.DTO;
using AutoMapper;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Entity;

namespace Application.AutoMapper
{
    public class TransferAssetDetailProfile : Profile
    {
        public TransferAssetDetailProfile()
        {
            CreateMap<TransferAssetDetail, TransferAssetDetailDto>();
            CreateMap<TransferAssetDetail, TransferAssetDetailModel>();
            CreateMap<TransferAssetDetailModel, TransferAssetDetailDto>();
            CreateMap<TransferAssetDetailCreateDto, TransferAssetDetail>();
            CreateMap<TransferAssetDetailUpdateDto, TransferAssetDetail>();
            CreateMap<TransferAssetDetailCreateDto, TransferAssetDetailModel>();
            CreateMap<TransferAssetDetailUpdateDto, TransferAssetDetailModel>();
        }
    }
}
