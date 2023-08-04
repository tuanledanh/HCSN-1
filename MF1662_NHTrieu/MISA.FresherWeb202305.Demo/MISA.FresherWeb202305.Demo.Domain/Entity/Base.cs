using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace MISA.FresherWeb202305.Demo.Domain.Entity
{
    public abstract class Base
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        /// 
        [StringLength(50)]
        public string CreatedBy { get; set; }=String.Empty;
        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        /// 
        [StringLength(50)]
        public string ModifiedBy { get; set; }=string.Empty;
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Description { get; set; } = String.Empty;
    }
}
