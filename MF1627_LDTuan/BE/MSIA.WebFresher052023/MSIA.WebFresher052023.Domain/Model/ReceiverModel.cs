using MSIA.WebFresher052023.Domain.Model.Base;

namespace Domain.Model
{
    public class ReceiverModel : IHasKeyModel
    {
        #region Fields
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
        /// <returns>Id của chứng từ</returns>
        /// Created by: ldtuan (28/08/2023)
        public string GetKey()
        {
            return ReceiverCode;
        }
        #endregion
    }
}
