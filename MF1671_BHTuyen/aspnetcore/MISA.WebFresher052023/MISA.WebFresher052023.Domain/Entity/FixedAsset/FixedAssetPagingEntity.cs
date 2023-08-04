using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Entity.FixedAsset
{
    public class FixedAssetPagingEntity
    {
        #region Properties
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        [Required(ErrorMessage ="Tổng số bản ghi không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Tổng số bản ghi không được âm")]
        public int FixedAssetTotal { get; set; }

        /// <summary>
        /// Danh sách tài sản trong một trang
        /// </summary>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        [Required(ErrorMessage ="Danh sách các tài sản lọc không được để trống")]
        public IEnumerable<FixedAssetEntity> FixedAssetEntities { get; set; } 
        #endregion
    }
}
