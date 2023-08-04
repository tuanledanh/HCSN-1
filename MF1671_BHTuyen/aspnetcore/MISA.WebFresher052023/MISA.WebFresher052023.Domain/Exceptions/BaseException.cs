using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Exceptions
{
    public class BaseException
    {
        #region Properties
        /// <summary>
        /// Mã lỗi
        /// </summary>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public int ErrorCode { get; set; }

        /// <summary>
        /// Message lỗi cho Dev
        /// </summary>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public string? DevMessage { get; set; }

        /// <summary>
        /// Message lỗi cho User
        /// </summary>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public string? UserMessage { get; set; }

        /// <summary>
        /// Id lỗi
        /// </summary>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public string? TraceId { get; set; }

        /// <summary>
        /// Thông tin thêm
        /// </summary>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public string? MoreInfo { get; set; }

        /// <summary>
        /// Lỗi
        /// </summary>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public object? Errors { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Ghi đè hàm ToString()
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: Bùi Huy Tuyền (19/07/2023)
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        #endregion
    }
}
