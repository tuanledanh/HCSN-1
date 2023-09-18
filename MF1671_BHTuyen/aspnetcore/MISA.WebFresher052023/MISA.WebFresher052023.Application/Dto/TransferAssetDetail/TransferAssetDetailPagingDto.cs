using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Dto.TransferAssetDetail
{
    public class TransferAssetDetailPagingDto
    {
        public int TransferAssetDetailTotal { get; set; }

        public IEnumerable<TransferAssetDetailDto> TransferAssetDetails { get; set; }
    }
}
