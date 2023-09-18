using MISA.WebFresher052023.Domain.Entity.TransferAsset;
using MISA.WebFresher052023.Domain.Model.FixedAsset;
using MISA.WebFresher052023.Domain.Model.TransferAsset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface.TransferAsset
{
    public interface ITransferAssetRepository : IBaseRepository<TransferAssetEntity>
    {
        public Task<string> GetTransferAssetCodeNewAsync();

        public Task<FixedAssetTransferInfo> GetTransferAssetsByFixedAssetIdsAsync(List<Guid> fixedAssetIds);

        public Task<FixedAssetTransferInfo> GetTransferAssetsLaterAsync(List<Guid> transferAssetIds);

        public Task<TransferAssetPagingModel> GetTransferAssetPagingAsync(TransferAssetFilterModel transferAssetFilterModel);
        public Task<IEnumerable<TransferAssetEntity>> FindManyAsync(List<Guid> transferAssetIds);

        public Task DeleteManyAsync(IEnumerable<TransferAssetEntity> transferAssetEntities);
    }
}
