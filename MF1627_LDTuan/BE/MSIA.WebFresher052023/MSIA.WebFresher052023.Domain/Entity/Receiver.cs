using Domain.Entity;
using MSIA.WebFresher052023.Domain.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.Domain.Entity
{
    public class Receiver : BaseEntity, IHasKey
    {
        /// <summary>
        /// Id của người nhận
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public Guid ReceiverId { get; set; }

        /// <summary>
        /// Mã code của người nhận
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public string ReceiverCode { get; set; }

        /// <summary>
        /// Họ tên đầy đủ của người nhận
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public string ReceiverFullname { get; set; }

        /// <summary>
        /// Người đại diện nhận
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public string ReceiverDelegate { get; set; }

        /// <summary>
        /// Chức vụ của người nhận
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public string ReceiverPosition { get; set; }

        /// <summary>
        /// Id của chứng từ
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public Guid TransferAssetId { get; set; }

        /// <summary>
        /// Nhận được giá trị của thuộc tính khóa trong một đối tượng cụ thể 
        /// mà không cần truy cập trực tiếp vào thuộc tính đó, TEntity có thể xài để lấy đc id
        /// </summary>
        /// <returns>Id của tài sản</returns>
        /// Created by: ldtuan (27/08/2023)
        public Guid GetKey()
        {
            return ReceiverId;
        }

        /// <summary>
        /// Set giá trị của thuộc tính khóa trong một đối tượng cụ thể 
        /// mà không cần truy cập trực tiếp vào thuộc tính đó, TEntity có thể xài để lấy đc id
        /// </summary>
        /// <returns>Id của tài sản</returns>
        /// Created by: ldtuan (19/07/2023)
        public Guid SetKey(Guid id)
        {
            return ReceiverId = id;
        }
    }
}
