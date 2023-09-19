using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Model.FixedAsset
{
    public class FixedAssetFilterModel
    {
        #region Properties
        /// <summary>
        /// Từ khóa tìm kiếm theo mã hoặc tên tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public string? FixedAssetCodeOrName { get; set; }

        /// <summary>
        /// Từ khóa tìm kiếm theo phòng ban
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Từ khóa tìm kiếm theo tên loại tài sản
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public string? FixedAssetCategoryName { get; set; }

        /// <summary>
        /// Danh sách mã tài sản bỏ qua
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public List<Guid?> FixedAssetIdIgnores { get; set; }

        /// <summary>
        /// Danh sách tài sản điều chuyển bỏ qua
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public List<Guid?> TransferAssetDetailIdIgnores { get; set; }

        /// <summary>
        /// Số lượng bản ghi trên một trang
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public int PageLimit { get; set; }

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public int PageNumber { get; set; } 
        #endregion
    }
}
