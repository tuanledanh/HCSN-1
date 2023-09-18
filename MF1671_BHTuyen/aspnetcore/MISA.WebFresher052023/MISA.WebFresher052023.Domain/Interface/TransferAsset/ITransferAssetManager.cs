using MISA.WebFresher052023.Domain.Entity.TransferAsset;
using MISA.WebFresher052023.Domain.Entity.TransferAssetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface.TransferAsset
{
    public interface ITransferAssetManager : IBaseManager<TransferAssetEntity>
    {
        public void CheckTransferDateLaterTransTransactionDateAsync(DateTime transferDate, DateTime transactionDate);

        public Task CheckDeleteTransferAssetAsync(List<Guid> transferAssetIds);
       
    }
}
