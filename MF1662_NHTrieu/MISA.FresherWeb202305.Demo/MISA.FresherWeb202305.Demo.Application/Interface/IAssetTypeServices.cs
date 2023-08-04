using MISA.FresherWeb202305.Demo.Application.Dto;
using MISA.FresherWeb202305.Demo.Application.Interface.Base;
using MISA.FresherWeb202305.Demo.Domain.Entity;
using MISA.FresherWeb202305.Demo.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Interface
{
    public interface IAssetTypeServices :IBaseCodeService<AssetTypeDto,AssetTypeCreateDto,AssetTypeUpdateDto>
    {
       
    }
}
