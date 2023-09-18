using MISA.WebFresher052023.Domain.Entity.TransferAsset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Model.TransferAsset
{
    public class TransferAssetPagingModel
    {
        public int TransferAssetTotal { get; set; }

        public IEnumerable<TransferAssetEntity> TransferAssets { get; set; }
    }
}
