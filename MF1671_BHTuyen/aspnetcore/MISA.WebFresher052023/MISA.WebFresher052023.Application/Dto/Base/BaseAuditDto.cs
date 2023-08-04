using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Dto.Base
{
    public abstract class BaseAuditDto
    {
        #region Properties
        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public string? ModifiedBy { get; set; }
        #endregion
    }
}
