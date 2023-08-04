using MISA.FresherWeb202305.Demo.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Domain
{
    public class NotFoundException : System.Exception
    {
        /// <summary>
        /// Mã lỗi code để xác định loại lỗi.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Constructor của lớp NotFoundException, chấp nhận thông báo lỗi làm tham số.
        /// </summary>
        /// <param name="message">Thông báo lỗi.</param>
        public NotFoundException(string message) : base(message) { }

        /// <summary>
        /// Constructor của lớp NotFoundException, chấp nhận thông báo lỗi và mã lỗi để xác định loại lỗi làm tham số.
        /// </summary>
        /// <param name="message">Thông báo lỗi.</param>
        /// <param name="errorCode">Mã lỗi code để xác định loại lỗi.</param>
        public NotFoundException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
