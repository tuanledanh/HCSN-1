using MISA.WebFresher052023.Domain.Entity.Receiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface.Receiver
{
    public interface IReceiverRepository
    {
        public Task<IEnumerable<ReceiverEntity>> GetManyByTransferAssetIdAsync(Guid transferAssetId);

        public Task<IEnumerable<ReceiverEntity>> FindManyByTransferAssetIdsAsync(List<Guid> transferAssetIds);

        public Task<IEnumerable<ReceiverEntity>> FindManyAsync(List<Guid> receiverIds);

        public Task DeleteManyAsync(IEnumerable<ReceiverEntity> receivers);

        public Task CreateManyAsync(IEnumerable<ReceiverEntity> receivers);

        public Task UpdateManyAsync(IEnumerable<ReceiverEntity> receivers);
    }
}
