using MISA.WebFresher052023.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Entity.Receiver
{
    public class ReceiverEntity : BaseAuditEntity, IHasKeyEntity
    {
        /// <summary>
        /// Id của người giao nhận
        /// </summary>
        public Guid ReceiverId { get; set; }

        /// <summary>
        /// Số thứ tự trong ban giao nhận
        /// </summary>
        public int ReceiverOrder { get; set; }

        /// <summary>
        /// Họ và tên của người giao nhận
        /// </summary>
        public string Fullname { get; set; }

        /// <summary>
        /// Đại diện
        /// </summary>
        public string Delegate { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Id của chứng từ điều chuyển 
        /// </summary>
        public Guid TransferAssetId { get; set; }

        public void SetTransferAssetId(Guid id)
        {
            TransferAssetId = id;
        }

        public Guid GetKey()
        {
            return ReceiverId;
        }

        public void SetKey(Guid id)
        {
            ReceiverId = id;
        }
    }
}
