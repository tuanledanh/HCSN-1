using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.Application.DTO.TransferAssett
{
    public class TransferAssetDeleteDto
    {
        #region Fields
        /// <summary>
        /// Id của chứng từ
        /// </summary>
        /// Created by: ldtuan (09/09/2023)
        public Guid TransferAssetId { get; set; }

        /// <summary>
        /// Mã chứng từ
        /// </summary>
        /// Created by: ldtuan (09/09/2023)
        public string TransferAssetCode { get; set; }

        /// <summary>
        /// Ngày điều chuyển
        /// </summary>
        /// Created by: ldtuan (09/09/2023)
        public DateTime TransferDate { get; set; }

        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        /// Created by: ldtuan (09/09/2023)
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Lý do điều chuyển
        /// </summary>
        /// Created by: ldtuan (09/09/2023)
        public string Description { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Nguyên giá của tài sản trong chứng từ
        /// </summary>
        /// Created by: ldtuan (01/09/2023)
        public int? Cost { get; set; }

        /// <summary>
        /// Giá tài sản còn lại
        /// </summary>
        /// Created by: ldtuan (02/09/2023)
        public decimal RemainingCost { get; set; }

        /// <summary>
        /// Id của tài sản
        /// </summary>
        /// Created by: ldtuan (02/09/2023)
        public Guid FixedAssetId { get; set; }
        #endregion
    }
}
