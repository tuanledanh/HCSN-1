using MISA.WebFresher052023.Application.Dto.TransferAssetDetail;
using MISA.WebFresher052023.Domain.Entity.TransferAssetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Interface
{
    public interface ITransferAssetDetailService
    {
        public Task<TransferAssetDetailPagingDto> GetTransferAssetDetailPagingAsync(TransferAssetDetailFilterDto transferAssetDetailFilterDto);

        public Task<IEnumerable<TransferAssetDetailDto>> GetManyByTransferAssetIdAsync(Guid transferAssetId);

        public Task<IEnumerable<TransferAssetDetailEntity>> GetManyByTransferAssetIdsAsync(List<Guid> transferAssetIds);

        public Task CreateManyAsync(IEnumerable<TransferAssetDetailDto> transferAssetDetails, Guid TransferAssetId);

        public Task UpdateManyAsync(IEnumerable<TransferAssetDetailDto> transferAssetDetails);

        public Task DeleteManyAsync(List<Guid> transferAssetDetailIds);
    }
}
