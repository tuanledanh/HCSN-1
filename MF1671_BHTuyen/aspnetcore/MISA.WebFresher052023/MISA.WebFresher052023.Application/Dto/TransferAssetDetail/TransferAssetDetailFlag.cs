using MISA.WebFresher052023.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Dto.TransferAssetDetail
{
    public class TransferAssetDetailFlag
    {
        // <summary>
        /// Id của chi tiết điều chuyển
        /// </summary>
        public TransferAssetDetailDto TransferAssetDetail { get; set; }

        public ActionMode ActionMode { get; set; }
    }
}
