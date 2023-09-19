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
        #region Pproperties
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public int FixedAssetTotal { get; set; }

        /// <summary>
        /// Danh sách tài sản cần hiển thị
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public IEnumerable<FixedAssetEntity> FixedAssets { get; set; } 
        #endregion

    }
}
