using MISA.FresherWeb202305.Demo.Application.Dto;
using MISA.FresherWeb202305.Demo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace MISA.FresherWeb202305.Demo.Application.Map
{
    public class AssetTypeMapper :Profile
    {
         public AssetTypeMapper()
        {
            // Sử dụng trong trường hợp trả về dữ liệu
            CreateMap<AssetType, AssetTypeDto>();

            // Sử dụng trong trường hợp tạo mới
            CreateMap<AssetTypeCreateDto, AssetType>();

            // Sử dụng trong trường hợp cập nhật
            CreateMap<AssetTypeUpdateDto, Asset>();
        }
    }
}
