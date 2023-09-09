using Domain.Entity;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface.Repository.Base;

namespace MSIA.WebFresher052023.Domain.Interface.Repository
{
    public interface ITransferAssetDetailRepository : IBaseRepository<TransferAssetDetail, TransferAssetDetailModel>
    {
        /// <summary>
        /// Lấy danh sách các bản ghi trong danh sách id
        /// </summary>
        /// <param name="ids">Danh sách id của các bản ghi cần lấy dữ liệu</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: ldtuan (06/09/2023)
        Task<List<TransferAssetDetailModel>> GetListDetailByIdsAsync(List<Guid> ids);
    }
}
