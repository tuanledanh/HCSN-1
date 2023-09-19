using MISA.WebFresher052023.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Entity.TransferAsset
{
    public class TransferAssetEntity : BaseAuditEntity, IHasKeyEntity, IHasCodeEntity
    {
        #region Properties
        /// <summary>
        /// Id của chứng từ
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid TransferAssetId { get; set; }

        /// <summary>
        /// Mã chứng từ
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string TransferAssetCode { get; set; }

        /// <summary>
        /// Ngày điều chuyển
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DateTime TransferDate { get; set; }

        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public double Cost { get; set; }

        /// <summary>
        /// Giá trị còn lại 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public double RemainderCost { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string? Note { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy mã code của chứng từ
        /// </summary>
        /// <returns>Mã chứng từ</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string GetCode()
        {
            return TransferAssetCode;
        }

        /// <summary>
        /// Lấy mã Id của chứng từ
        /// </summary>
        /// <returns>Mã Id của chứng từ<</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid GetKey()
        {
            return TransferAssetId;
        }

        /// <summary>
        /// Gán mã Id cho chứng từ
        /// </summary>
        /// <param name="transferAssetId"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public void SetKey(Guid transferAssetId)
        {
            TransferAssetId = transferAssetId;
        } 
        #endregion
    }
}
