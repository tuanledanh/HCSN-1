using MISA.WebFresher052023.Domain.Entity.TransferAssetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Model.TransferAssetDetail
{
    public class TransferAssetDetailPagingModel
    {
        #region Properties
        /// <summary>
        /// Tổng số tài sản điều chuyển
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public int TransferAssetDetailTotal { get; set; }

        /// <summary>
        /// Danh sách tài sản điều chuyển cần hiển thị 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public IEnumerable<TransferAssetDetailEntity> TransferAssetDetails { get; set; } 
        #endregion
    }
}
