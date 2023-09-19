using MISA.WebFresher052023.Domain.Entity.TransferAsset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Model.TransferAsset
{
    public class TransferAssetPagingModel
    {
        #region Properties
        /// <summary>
        /// Tổng số chứng từ điều chuyển
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public int TransferAssetTotal { get; set; }

        /// <summary>
        /// Danh sách chứng từ điều chuyển cần hiển thị
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public IEnumerable<TransferAssetEntity> TransferAssets { get; set; } 
        #endregion
    }
}
