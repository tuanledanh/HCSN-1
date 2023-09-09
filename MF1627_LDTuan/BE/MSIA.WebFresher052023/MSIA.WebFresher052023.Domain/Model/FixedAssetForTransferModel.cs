using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.Domain.Model
{
    public class FixedAssetForTransferModel
    {
        public int TotalRecords { get; set; }
        public List<FixedAssetModel> FixedAssetModels { get; set; }
    }
}
