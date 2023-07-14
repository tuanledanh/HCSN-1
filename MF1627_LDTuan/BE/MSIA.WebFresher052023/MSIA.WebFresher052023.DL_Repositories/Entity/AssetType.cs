using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.DL_Repositories.Entity
{
    public class AssetType : BaseEntity
    {
        /// <summary>
        /// Id của loại tài sản
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public Guid AssetTypeId { get; set; }
        /// <summary>
        /// Tên của loại tài sản
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public string AssetTypeName { get; set; }
        /// <summary>
        /// Mã code của loại tài sản
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public string AssetTypeCode { get; set; }
    }
}
