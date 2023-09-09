using MSIA.WebFresher052023.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class TransferAssetDetailUpdateDto
    {
        /// <summary>
        /// Cờ hiển thị trạng thái hành động
        /// </summary>
        /// Created by: ldtuan (30/08/2023)
        [Required]
        public ActionMode Flag { get; set; }

        /// <summary>
        /// Id của chi tiết tài sản điều chuyển
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public Guid TransferAssetDetailId { get; set; }

        /// <summary>
        /// Id của tài sản
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        [Required]
        public Guid FixedAssetId { get; set; }

        /// <summary>
        /// Id của chứng từ
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        [Required]
        public Guid TransferAssetId { get; set; }

        /// <summary>
        /// Phòng ban hiện tại của tài sản
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        [Required]
        public Guid OldDepartmentId { get; set; }

        /// <summary>
        /// Phòng ban cần chuyển tài sản đến
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        [Required]
        public Guid NewDepartmentId { get; set; }

        /// <summary>
        /// Lý do điều chuyển
        /// </summary>
        /// Created by: ldtuan (27/08/2023)
        public string Description { get; set; }
    }
}
