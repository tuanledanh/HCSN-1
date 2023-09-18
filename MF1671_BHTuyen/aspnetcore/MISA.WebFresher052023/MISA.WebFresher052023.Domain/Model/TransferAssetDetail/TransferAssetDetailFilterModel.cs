using MISA.WebFresher052023.Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Model.TransferAssetDetail
{
    public class TransferAssetDetailFilterModel
    {
        /// <summary>
        /// Id của tài sản
        /// </summary>
        public Guid TransferAssetId { get; set; }

        public int PageLimit { get; set; }

        public int PageNumber { get; set; }
    }
}
