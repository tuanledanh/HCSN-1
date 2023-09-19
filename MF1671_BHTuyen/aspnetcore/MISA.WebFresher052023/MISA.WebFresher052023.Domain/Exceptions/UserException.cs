using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Exceptions
{
    public class UserException : Exception
    {
        #region Properties
        /// <summary>
        /// Thông tin thêm về lỗi
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public List<string>? MoreInfo { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Hàm khởi tạo mặc định
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public UserException() { }

        /// <summary>
        /// Hàm khởi tạo với tên lỗi
        /// </summary>
        /// <param name="message">Tên lỗi</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public UserException(string message) : base(message) { }

        /// <summary>
        /// Hàm khởi tạo với tên lỗi và thông tin thêm
        /// </summary>
        /// <param name="message">Tên lỗi</param>
        /// <param name="moreInfo">Thông tin thêm về lỗi</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public UserException(string message, List<string> moreInfo) : base(message)
        {
            MoreInfo = moreInfo;
        } 
        #endregion
    }
}
