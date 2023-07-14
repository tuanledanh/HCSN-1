using AutoMapper;
using MSIA.WebFresher052023.BL_Services.DTO.Assettype;
using MSIA.WebFresher052023.DL_Repositories.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.BL_Services.AutoMapper
{
    public class AssetTypeProfile : Profile
    {
        public AssetTypeProfile()
        {
            CreateMap<AssetType, AssetTypeDto>();
            CreateMap<AssetTypeCreateDto, AssetType>();
            CreateMap<AssetTypeUpdateDto, AssetType>();
        }
    }
}
