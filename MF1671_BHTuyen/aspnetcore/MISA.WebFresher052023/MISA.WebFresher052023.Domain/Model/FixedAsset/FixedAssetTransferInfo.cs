using MISA.WebFresher052023.Domain.Entity.TransferAsset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Model.FixedAsset
{
    public class FixedAssetTransferInfo
    {
        #region Properties
        /// <summary>
        /// Mã tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string FixedAssetCode { get; set; }

        /// <summary>
        /// Mã chứng từ
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string? TransferAssetCode { get; set; }

        /// <summary>
        /// Mã của chứng từ ngay sau
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string? TransferAssetCodeMin { get; set; }

        /// <summary>
        /// Mã của chứng từ ngay trước
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string? TransferAssetCodeMax { get; set; }

        /// <summary>
        /// Ngày điều chuyển của chứng từ
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DateTime? TransferDate { get; set; }

        /// <summary>
        /// Ngày điều chuyển của chứng từ ngay sau
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DateTime? TransferDateMin { get; set; }

        /// <summary>
        /// Ngày điều chuyển của chứng từ ngay trước
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DateTime? TransferDateMax { get; set; }

        /// <summary>
        /// Danh sách chứng từ
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public IEnumerable<TransferAssetEntity> TransferAssets { get; set; } 
        #endregion

    }
}
