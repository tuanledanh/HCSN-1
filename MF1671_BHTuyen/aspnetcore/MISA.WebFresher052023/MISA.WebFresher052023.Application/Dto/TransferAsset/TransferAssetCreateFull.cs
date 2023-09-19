using MISA.WebFresher052023.Application.Dto.Receiver;
using MISA.WebFresher052023.Application.Dto.TransferAssetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Dto.TransferAsset
{
    public class TransferAssetCreateFull
    {
        public TransferAssetDto TransferAsset { get; set; }

        public IEnumerable<TransferAssetDetailDto> TransferAssetDetails { get; set;}

        public IEnumerable<ReceiverDto>? Receivers { get; set; }
    }
}
