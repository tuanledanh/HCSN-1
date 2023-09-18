using Domain.Entity;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface.Repository.Base;
using MSIA.WebFresher052023.Domain.Model;

namespace MSIA.WebFresher052023.Domain.Interface.Repository
{
    public interface ITransferAssetRepository : IBaseRepository<TransferAsset, TransferAssetModel>
    {
        /// <summary>
        /// Lấy chứng từ mới nhất trong những chứng từ mà tài sản có
        /// </summary>
        /// <param name="assetIds">Danh sách id của tài sản</param>
        /// <returns>Chứng từ mới nhất trong đống tài sản</returns>
        /// Created by: ldtuan (07/09/2023)
        Task<List<TransferAsset>> GetNewestTransferAssetByAssetId(List<Guid> assetIds);

        /// <summary>
        /// Lấy các chứng từ của các tài sản truyền đến
        /// </summary>
        /// <param name="assetIds">Danh sách id tài sản</param>
        /// <returns>Danh sách chứng từ, có chứa id tài sản</returns>
        /// Created by: ldtuan (17/09/2023)
        Task<List<TransferAssetDeleteModel>> GetAllTransferAssetByAssetId(List<Guid> asssetIds);

        /// <summary>
        /// Truyền vào 1 danh sách chứng từ, tìm các tài sản trong chứng từ đó, rồi tìm tất cả các chứng từ có chứa các tài sản này
        /// </summary>
        /// <param name="transferAssetIds">Danh sách id chứng từ</param>
        /// <returns>Danh sách chứng từ của các tài sản trong các chứng từ truyền vào</returns>
        /// Created by: ldtuan (09/09/2023)
        Task<List<TransferAssetDeleteModel>> GetAllTransferAssetOfAsset(List<Guid> transferAssetIds);
    }
}
