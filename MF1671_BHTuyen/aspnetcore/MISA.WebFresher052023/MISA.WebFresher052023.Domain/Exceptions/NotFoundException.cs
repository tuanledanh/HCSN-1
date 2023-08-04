namespace MISA.WebFresher052023.Domain.Exceptions
{
    public class NotFoundException : Exception
    {

        #region Properties
        /// <summary>
        /// Mã lỗi
        /// </summary>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public int ErrorCode { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Khởi tạo mặc định
        /// </summary>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public NotFoundException() { }

        /// <summary>
        /// Khởi tạo với mã lỗi
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public NotFoundException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Khởi tạo mới với message lỗi
        /// </summary>
        /// <param name="message">Message lỗi</param>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public NotFoundException(string message) : base(message) { }

        /// <summary>
        /// Khởi tạo mới với message lỗi và mã lỗi
        /// </summary>
        /// <param name="message">Message lỗi</param>
        /// <param name="errorCode">Mã lỗi</param>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public NotFoundException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        #endregion
    }
}
