using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace qlts_api.DTO.Department
{
    public class DepartmentRequest
    {
        public Guid DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentCode { get; set; }
        public string? CreatedBy{ get; set; }
        public string? ModifiedBy{ get; set; }
    }
}
