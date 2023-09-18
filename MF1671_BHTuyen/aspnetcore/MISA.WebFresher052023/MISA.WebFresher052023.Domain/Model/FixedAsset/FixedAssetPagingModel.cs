using MISA.WebFresher052023.Domain.Entity.FixedAsset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Model.FixedAsset
{
    public class FixedAssetPagingModel
    {
        public int FixedAssetTotal { get; set; }

        public IEnumerable<FixedAssetEntity> FixedAssets { get; set; }
 
    }
}
