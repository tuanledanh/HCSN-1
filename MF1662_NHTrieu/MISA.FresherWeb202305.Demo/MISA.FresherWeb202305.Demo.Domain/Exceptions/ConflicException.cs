using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Domain.Exceptions
{
    public class ConflicException : System.Exception
    {
        /// <summary>
        /// Mã lỗi code để xác định loại lỗi.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Constructor của lớp ConflicException, chấp nhận thông báo lỗi làm tham số.
        /// </summary>
        /// <param name="message">Thông báo lỗi.</param>
        public ConflicException(string message) : base(message) { }

        /// <summary>
        /// Constructor của lớp ConflicException, chấp nhận thông báo lỗi và mã lỗi để xác định loại lỗi làm tham số.
        /// </summary>
        /// <param name="message">Thông báo lỗi.</param>
        /// <param name="errorCode">Mã lỗi code để xác định loại lỗi.</param>
        public ConflicException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
