namespace MSIA.WebFresher052023.Domain.Model
{
    public class ReceiverTransferModel
    {
        #region Fields
        /// <summary>
        /// Id của người nhận
        /// </summary>
        /// Created by: ldtuan (03/09/2023)
        public Guid ReceiverId { get; set; }

        /// <summary>
        /// Mã code của người nhận
        /// </summary>
        /// Created by: ldtuan (04/09/2023)
        public string ReceiverCode { get; set; }

        /// <summary>
        /// Họ tên đầy đủ của người nhận
        /// </summary>
        /// Created by: ldtuan (03/09/2023)
        public string ReceiverFullname { get; set; }

        /// <summary>
        /// Người đại diện nhận
        /// </summary>
        /// Created by: ldtuan (03/09/2023)
        public string ReceiverDelegate { get; set; }

        /// <summary>
        /// Chức vụ của người nhận
        /// </summary>
        /// Created by: ldtuan (03/09/2023)
        public string ReceiverPosition { get; set; } 
        #endregion
    }
}
