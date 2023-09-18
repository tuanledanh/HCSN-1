using MISA.WebFresher052023.Application.Dto.TransferAsset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Interface
{
    public interface ITransferAssetService
    {
        public Task<string> GetTransferAssetCodeNewAsync();

        public Task<TransferAssetPagingDto> GetTransferAssetPagingAsync(TransferAssetFilterDto transferAssetFilterDto);

        public Task CreateAsync(TransferAssetDto transferAssetDto, Guid TransferAssetId);

        public Task CreateTransferAssetFullAsync(TransferAssetCreateFull transferAssetCreateFull);

        public Task UpdateAsync(TransferAssetDto transferAssetDto);

        public Task UpdateTransferAssetFullAsync(TransferAssetUpdateFull transferAssetUpdateFull);

        public Task DeleteAsync(Guid transferAssetId);

        public Task DeleteManyAsync(List<Guid> transferAssetIds);

    }
}
