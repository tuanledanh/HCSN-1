using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Dto.TransferAsset
{
    public class TransferAssetPagingDto
    {
        public int TransferAssetTotal { get; set; }

        public IEnumerable<TransferAssetDto> TransferAssets { get; set; }
    }
}
