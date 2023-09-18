using MISA.WebFresher052023.Domain.Entity.TransferAsset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Model.FixedAsset
{
    public class FixedAssetTransferInfo
    {
        public string FixedAssetCode { get; set; }

        public string? TransferAssetCode { get; set; }

        public DateTime? TransferDate { get; set; }

        public IEnumerable<TransferAssetEntity> TransferAssets { get; set; }

    }
}
