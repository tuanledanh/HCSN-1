using AutoMapper;
using MSIA.WebFresher052023.BL_Services.DTO.Assett;
using MSIA.WebFresher052023.DL_Repositories.Entity;
using MSIA.WebFresher052023.DL_Repositories.Repositories.Assett;

namespace MSIA.WebFresher052023.BL_Services.Service.Assett
{
    public class AssetService : BaseService<Asset, AssetDto, AssetCreateDto, AssetUpdateDto>, IAssetService
    {
        public AssetService(IAssetRepository assetRepository, IMapper mapper) : base(assetRepository, mapper)
        {
        }
    }
}
