using MISA.WebFresher052023.Application.Dto.Receiver;
using MISA.WebFresher052023.Domain.Entity.Receiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Interface
{
    public interface IReceiverService
    {
        public Task<IEnumerable<ReceiverDto>> GetReceiverAsync(Guid transferAssetId);

        public Task<IEnumerable<ReceiverEntity>> FindManyByTransferAssetIdsAsync(List<Guid> transferAssetIds);

        public Task CreateManyAsync(IEnumerable<ReceiverDto> receivers, Guid TransferAssetId);

        public Task UpdateManyAsync(IEnumerable<ReceiverDto> receivers);

        public Task DeleteManyAsync(List<Guid> receiverIds);
    }
}
