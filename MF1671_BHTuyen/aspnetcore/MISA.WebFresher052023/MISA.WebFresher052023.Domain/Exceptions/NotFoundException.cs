namespace MISA.WebFresher052023.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        #region Constructor
        /// <summary>
        /// Khởi tạo mặc định
        /// </summary>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public NotFoundException() { }

        /// <summary>
        /// Khởi tạo mới với message lỗi
        /// </summary>
        /// <param name="message">Message lỗi</param>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public NotFoundException(string message) : base(message) { }
        #endregion
    }
}
