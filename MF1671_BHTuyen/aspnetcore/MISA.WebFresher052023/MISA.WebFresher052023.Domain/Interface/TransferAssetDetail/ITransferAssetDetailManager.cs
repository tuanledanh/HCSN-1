using MISA.WebFresher052023.Domain.Entity.TransferAssetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface.TransferAssetDetail
{
    public interface ITransferAssetDetailManager
    {
        public Task DeleteCheckerAsync(Guid FixedAssetId, Guid TransferAssetId);

        public Task CheckDepartmentAsync(IEnumerable<TransferAssetDetailEntity> transferAssetDetailEntity);

    }
}
