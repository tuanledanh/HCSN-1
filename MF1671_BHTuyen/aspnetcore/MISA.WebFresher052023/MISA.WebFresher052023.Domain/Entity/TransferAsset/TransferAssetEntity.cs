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
        /// <summary>
        /// Id của chứng từ
        /// </summary>
        public Guid TransferAssetId { get; set; }

        /// <summary>
        /// Mã chứng từ
        /// </summary>
        public string TransferAssetCode { get; set; }

        /// <summary>
        /// Ngày điều chuyển
        /// </summary>
        public DateTime TransferDate { get; set; }

        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Nguyên giá
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// Giá trị còn lại 
        /// </summary>
        public double RemainderCost { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }

        public string GetCode()
        {
            return TransferAssetCode;
        }

        public Guid GetKey()
        {
            return TransferAssetId;
        }

        public void SetKey(Guid id)
        {
            TransferAssetId = id;
        }
    }
}
