using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class ExportFixedAsset
    {
        #region Fields
        /// <summary>
        /// Mã code của tài sản
        /// </summary>
        /// Created by: ldtuan (31/07/2023)
        [Display(Name ="Mã code tài sản")]
        public string FixedAssetCode { get; set; }
        /// <summary>
        /// Tên tài sản
        /// </summary>
        /// Created by: ldtuan (31/07/2023)
        [Display(Name = "Tên tài sản")]
        public string FixedAssetName { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// Created by: ldtuan (31/07/2023)
        [Display(Name = "Mã phòng ban")]
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created by: ldtuan (31/07/2023)
        [Display(Name = "Tên phòng ban")]
        public string DepartmentName { get; set; }
        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        /// Created by: ldtuan (31/07/2023)
        [Display(Name = "Mã loại tài sản")]
        public string FixedAssetCategoryCode { get; set; }
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        /// Created by: ldtuan (31/07/2023)
        [Display(Name = "Tên loại tài sản")]
        public string FixedAssetCategoryName { get; set; }
        /// <summary>
        /// Ngày mua
        /// </summary>
        /// Created by: ldtuan (31/07/2023)
        [Display(Name = "Ngày mua")]
        public DateTime PurchaseDate { get; set; }
        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        /// Created by: ldtuan (31/07/2023)
        [Display(Name = "Ngày bắt đầu sử dụng")]
        public DateTime StartUsingDate { get; set; }
        /// <summary>
        /// Nguyên giá
        /// </summary>
        /// Created by: ldtuan (31/07/2023)
        [Display(Name = "Nguyên giá")]
        public decimal Cost { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        /// Created by: ldtuan (31/07/2023)
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }
        /// <summary>
        /// Năm bắt đầu theo dõi tài sản trên phần mềm
        /// </summary>
        /// Created by: ldtuan (31/07/2023)
        [Display(Name = "Năm bắt đầu theo dõi tài sản trên phần mềm")]
        public int TrackedYear { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// Created by: ldtuan (31/07/2023)
        [Display(Name = "Số năm sử dụng")]
        public int LifeTime { get; set; }
        /// <summary>
        /// Năm sử dụng
        /// </summary>
        /// Created by: ldtuan (31/07/2023)
        [Display(Name = "Năm sử dụng")]
        public int ProductionYear { get; set; }
        #endregion
    }
}
