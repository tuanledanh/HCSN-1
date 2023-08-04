using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Dto
{
    public class BaseDto
    {
        public DateTime CreatedDate { get; set; }
    /// <summary>
    /// Người tạo
    /// </summary>
    /// 
    [StringLength(50)]
    public string? CreatedBy { get; set; }
    /// <summary>
    /// Ngày chỉnh sửa
    /// </summary>
    public DateTime ModifiedDate { get; set; }
    /// <summary>
    /// Người sửa
    /// </summary>
    /// 
    [StringLength(50)]
    public string? ModifiedBy { get; set; }
}
}
