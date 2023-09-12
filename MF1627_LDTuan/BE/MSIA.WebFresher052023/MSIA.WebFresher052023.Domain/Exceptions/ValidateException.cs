namespace MSIA.WebFresher052023.Domain.Exceptions
{
    public class ValidateException : Exception
    {
        #region Fields
        public int ErrorCode { get; set; }
        public string? MoreInfo { get; set; }
        #endregion
        #region Constructor
        public ValidateException() { }
        public ValidateException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        #endregion

        #region Methods
        public ValidateException(string message) : base(message)
        {

        }
        public ValidateException(string message, string moreInfo) : base(message)
        {
            MoreInfo = moreInfo;
        }
        public ValidateException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        #endregion
    }
}