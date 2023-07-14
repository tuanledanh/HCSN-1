using qlts_api.DTO.Asset;
namespace qlts_api.Repository
{
    public interface IAssetRepo:IGenericRepository<Asset, AssetRequest, Guid>
    {
    }
}
