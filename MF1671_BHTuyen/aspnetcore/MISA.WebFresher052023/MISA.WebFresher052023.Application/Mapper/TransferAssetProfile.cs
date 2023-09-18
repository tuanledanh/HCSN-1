using AutoMapper;
using MISA.WebFresher052023.Application.Dto.TransferAsset;
using MISA.WebFresher052023.Domain.Entity.TransferAsset;
using MISA.WebFresher052023.Domain.Model.TransferAsset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Mapper
{
    public class TransferAssetProfile : Profile
    {
        public TransferAssetProfile() {

            CreateMap<TransferAssetEntity, TransferAssetDto>().ReverseMap();

            CreateMap<TransferAssetFilterDto, TransferAssetFilterModel>();

            CreateMap<TransferAssetPagingModel, TransferAssetPagingDto>().ForMember(destination => destination.TransferAssets, member => member.MapFrom(src => src.TransferAssets));
        
        }
    }
}
