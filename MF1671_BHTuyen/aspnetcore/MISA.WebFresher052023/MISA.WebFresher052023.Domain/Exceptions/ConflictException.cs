using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Exceptions
{
    public class ConflictException : Exception
    {
        #region Constructor
        /// <summary>
        /// Khởi tạo mặc định
        /// </summary>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public ConflictException() { }
        
        /// <summary>
        /// Khởi tạo mới với message lỗi
        /// </summary>
        /// <param name="message">Message lỗi</param>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public ConflictException(string message) : base(message) { }

        #endregion
    }
}
