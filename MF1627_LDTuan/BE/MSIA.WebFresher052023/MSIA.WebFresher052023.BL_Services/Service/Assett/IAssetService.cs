using MSIA.WebFresher052023.BL_Services.DTO.Assett;
using MSIA.WebFresher052023.DL_Repositories.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.BL_Services.Service.Assett
{
    public interface IAssetService : IBaseService<Asset, AssetDto, AssetCreateDto, AssetUpdateDto>
    {
    }
}
