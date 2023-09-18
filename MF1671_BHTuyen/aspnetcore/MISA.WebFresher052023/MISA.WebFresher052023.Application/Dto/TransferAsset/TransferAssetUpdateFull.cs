using MISA.WebFresher052023.Application.Dto.Receiver;
using MISA.WebFresher052023.Application.Dto.TransferAssetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Dto.TransferAsset
{
    public class TransferAssetUpdateFull
    {
        public TransferAssetDto TransferAsset { get; set; }

        public IEnumerable<TransferAssetDetailFlag>? TransferAssetDetails { get; set; }

        public IEnumerable<ReceiverFlag>? Receivers { get; set; }

    }
}
