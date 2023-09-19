using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Model.TransferAsset
{
    public class TransferAssetFilterModel
    {
        #region Properties
        /// <summary>
        /// Số lượng chứng từ tối đa trên một trang
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public int PageLimit { get; set; }

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public int PageNumber { get; set; } 
        #endregion
    }
}
