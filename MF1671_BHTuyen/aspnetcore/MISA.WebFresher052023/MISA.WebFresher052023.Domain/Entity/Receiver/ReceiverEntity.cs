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
        #region Properties
        /// <summary>
        /// Id của người giao nhận
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid ReceiverId { get; set; }

        /// <summary>
        /// Số thứ tự trong ban giao nhận
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public int ReceiverOrder { get; set; }

        /// <summary>
        /// Họ và tên của người giao nhận
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string Fullname { get; set; }

        /// <summary>
        /// Đại diện
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string Delegate { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string Position { get; set; }

        /// <summary>
        /// Id của chứng từ điều chuyển 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Guid TransferAssetId { get; set; } 
        #endregion

        #region Methods
        /// <summary>
        /// Gán mã id chứng từ cho người nhận
        /// </summary>
        /// <param name="transferAssetId">Mã Id của chứng từ</param>
        public void SetTransferAssetId(Guid transferAssetId)
        {
            TransferAssetId = transferAssetId;
        }

        /// <summary>
        /// Lấy mã id của người nhận
        /// </summary>
        /// <returns>Mã Id của người nhận</returns>
        public Guid GetKey()
        {
            return ReceiverId;
        }

        /// <summary>
        /// Gán mã id cho người nhận
        /// </summary
        /// <param name="receiverId">Mã id của người nhận</param>
        public void SetKey(Guid receiverId)
        {
            ReceiverId = receiverId;
        } 
        #endregion
    }
}
