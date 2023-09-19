using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Dto.FixedAsset
{
    public class FixedAssetPagingDto 
    {
        public int FixedAssetTotal { get; set; }

        public IEnumerable<FixedAssetDto> FixedAssets { get; set; }
    }
}
