using MSIA.WebFresher052023.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class ReceiverUpdateDto
    {
        #region Fields
        /// <summary>
        /// Cờ hiển thị trạng thái hành động
        /// </summary>
        /// Created by: ldtuan (30/08/2023)
        [Required]
        public ActionMode Flag { get; set; }

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
        #endregion
    }
}

