using Application.DTO.Assettype;
using Domain.Entity;
using Domain.Model;

namespace Application.Interface
{
    public interface IAssetTypeService : IBaseService<AssetType, AssetTypeModel, AssetTypeDto, AssetTypeCreateDto, AssetTypeUpdateDto>
    {
    }
}
