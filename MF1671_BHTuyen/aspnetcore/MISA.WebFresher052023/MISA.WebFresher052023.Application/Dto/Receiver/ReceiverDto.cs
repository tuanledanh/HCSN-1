using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Dto.Receiver
{
    public class ReceiverDto
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
    }
}
