using MISA.WebFresher052023.Domain.Entity.Receiver;
using MISA.WebFresher052023.Domain.Entity.TransferAssetDetail;
using MISA.WebFresher052023.Domain.Model.TransferAssetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface.TransferAssetDetail
{
    public interface ITransferAssetDetailRepository
    {

        public Task<TransferAssetDetailEntity> GetTransferAssetDetailLastetAsync(Guid FixedAssetIds);

        public Task<IEnumerable<TransferAssetDetailEntity>> GetManyByTransferAssetIdAsync(Guid transferAssetId);

        public Task<IEnumerable<TransferAssetDetailEntity>> GetManyByTransferAssetIdsAsync(List<Guid> transferAssetIds);

        public Task<IEnumerable<TransferAssetDetailEntity>> FindManyAsync(List<Guid> transferAssetDetailIds);
        public Task<TransferAssetDetailEntity> FindAsync(Guid transferAssetDetailId);


        public Task<TransferAssetDetailPagingModel> GetTransferAssetDetailPagingAsync(TransferAssetDetailFilterModel transferAssetDetailFilterModel);

        public Task DeleteAsync(TransferAssetDetailEntity transferAssetDetail);

        public Task DeleteManyAsync(IEnumerable<TransferAssetDetailEntity> transferAssetDetails);

        public Task CreateManyAsync(IEnumerable<TransferAssetDetailEntity> transferAssetDetails);

        public Task UpdateManyAsync(IEnumerable<TransferAssetDetailEntity> transferAssetDetails);
    }
}
