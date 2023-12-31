﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.FixedAssetCategoryy
{
    public class FixedAssetCategoryUpdateMultiDto
    {
        #region Fields
        /// <summary>
        /// Id của loại tài sản
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public Guid FixedAssetCategoryId { get; set; }
        /// <summary>
        /// Mã code của loại tài sản
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public string FixedAssetCategoryCode { get; set; }
        /// <summary>
        /// Tên của loại tài sản
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public string FixedAssetCategoryName { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// Created by: ldtuan (31/07/2023)
        public int? LifeTime { get; set; }
        #endregion
    }
}