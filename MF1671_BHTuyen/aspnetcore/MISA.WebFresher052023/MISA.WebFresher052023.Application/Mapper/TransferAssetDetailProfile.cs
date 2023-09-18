using AutoMapper;
using MISA.WebFresher052023.Application.Dto.TransferAssetDetail;
using MISA.WebFresher052023.Domain.Entity.TransferAssetDetail;
using MISA.WebFresher052023.Domain.Model.TransferAssetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Mapper
{
    public class TransferAssetDetailProfile : Profile
    {
        public TransferAssetDetailProfile()
        {

            CreateMap<TransferAssetDetailEntity, TransferAssetDetailDto>().ReverseMap();

            CreateMap<TransferAssetDetailFilterDto,TransferAssetDetailFilterModel>();

            CreateMap<TransferAssetDetailPagingModel, TransferAssetDetailPagingDto>().ForMember(destination => destination.TransferAssetDetails, member => member.MapFrom(src => src.TransferAssetDetails));
        }
    }
}
