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
        #region Properties
        /// <summary>
        /// Id của chứng từ điều chuyển
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid TransferAssetId { get; set; }

        /// <summary>
        /// Số lượng tài sản điều chuyển tối đa trên một trang
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public int PageLimit { get; set; }

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public int PageNumber { get; set; } 
        #endregion
    }
}
