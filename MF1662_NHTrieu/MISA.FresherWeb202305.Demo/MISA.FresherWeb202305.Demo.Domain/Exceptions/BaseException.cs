using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Domain.Exceptions
{
    public class BaseException 
    {
        /// <summary>
        /// Mã lỗi code
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Thông báo lỗi cho nhà phát triển
        /// </summary>
        public string? DevMessage { get; set; }

        /// <summary>
        /// Thông báo lỗi cho người dùng
        /// </summary>
        public string? UserMessage { get; set; }

        /// <summary>
        /// Mã nhận dạng theo dõi (trace ID)
        /// </summary>
        public string? TraceId { get; set; }

        /// <summary>
        /// Thông tin bổ sung về lỗi
        /// </summary>
        public string? MoreInfo { get; set; }

        /// <summary>
        /// Các thông tin về lỗi liên quan
        /// </summary>
        public object? Errors { get; set; }

        /// <summary>
        ///  Ghi đè phương thức ToString() để trả về chuỗi JSON biểu diễn cho đối tượng BaseException
        /// </summary>
        /// <returns>Biều diễn dưới dạng Json</returns>
        /// CreatedBy: nhtrieu(15/07/2023)
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
