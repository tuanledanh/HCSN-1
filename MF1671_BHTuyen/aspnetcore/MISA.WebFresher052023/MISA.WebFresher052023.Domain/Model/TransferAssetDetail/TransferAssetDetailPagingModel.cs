using MISA.WebFresher052023.Domain.Entity.TransferAssetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Model.TransferAssetDetail
{
    public class TransferAssetDetailPagingModel
    {
        public int TransferAssetDetailTotal { get; set; }

        public IEnumerable<TransferAssetDetailEntity> TransferAssetDetails { get; set; }
     }
}
