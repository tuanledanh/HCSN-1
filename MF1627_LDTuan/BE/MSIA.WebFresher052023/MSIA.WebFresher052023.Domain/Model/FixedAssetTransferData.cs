using Domain.Entity;
using MSIA.WebFresher052023.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.Domain.Model
{
    public class FixedAssetTransferData
    {
        public List<FixedAsset> FixedAsset { get; set; }
        public List<TransferAssetDetail> TransferAssetDetail { get; set; }
    }
}
