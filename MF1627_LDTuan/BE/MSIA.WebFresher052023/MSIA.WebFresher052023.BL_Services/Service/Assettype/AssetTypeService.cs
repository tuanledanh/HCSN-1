using AutoMapper;
using MSIA.WebFresher052023.BL_Services.DTO.Assettype;
using MSIA.WebFresher052023.DL_Repositories.Entity;
using MSIA.WebFresher052023.DL_Repositories.Repositories.Assettype;

namespace MSIA.WebFresher052023.BL_Services.Service.Assettype
{
    public class AssetTypeService : BaseService<AssetType, AssetTypeDto, AssetTypeCreateDto, AssetTypeUpdateDto>, IAssetTypeService
    {
        public AssetTypeService(IAssetTypeRepository assetTypeRepository, IMapper mapper) : base(assetTypeRepository, mapper)
        {
        }
    }
}
