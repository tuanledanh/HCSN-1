using Application.DTO.Assett;
using Domain.Entity;
using Domain.Model;

namespace Application.Interface
{
    public interface IAssetService : IBaseService<Asset, AssetModel, AssetDto, AssetCreateDto, AssetUpdateDto>
    {
    }
}
