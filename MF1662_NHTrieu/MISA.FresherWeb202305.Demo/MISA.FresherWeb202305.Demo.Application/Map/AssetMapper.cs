using AutoMapper;
using MISA.FresherWeb202305.Demo.Application.Dto;
using MISA.FresherWeb202305.Demo.Domain.Entity;
using MISA.FresherWeb202305.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Map
{
    public class AssetMapper : Profile
    {
        /// <summary>
        /// Mapper các Entity với Dto
        /// </summary>
        public AssetMapper()
        {

            // Sử dụng trong trường hợp trả về dữ liệu
            CreateMap<Asset, AssetDto>();
            CreateMap<AssetModel, Pagination>();
            // Trường hợp trả về dữ liệu bao gồm tên của đơn vị và vị trí làm việc
            CreateMap<AssetModel, AssetDto>();

            // Trường hợp tạo mới
            CreateMap<AssetCreateDto, Asset>();

            // Trường hợp cập nhật
            CreateMap<AssetUpdateDto, Asset>();
        }

    }
}
