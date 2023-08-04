using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace MISA.FresherWeb202305.Demo.Domain.Entity
{
    public class Department : Base,IHasKey
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        /// 
        [Key]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// 
        [Required(ErrorMessage ="Mã loại phòng ban không được để trống")]
        [MaxLength(30,ErrorMessage ="Mã loại phòng ban có tối đa 50 kí tự")]
        [RegularExpression(@"^MLPB\d{6}$", ErrorMessage = "Mã loại tài sản phải có định dạng MLPBxxxxx")]

        public string DepartmentCode {get;set;}=String.Empty;
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// 
        [Required(ErrorMessage ="Tên loại phòng ban không được để trống")]
        [MaxLength (255,ErrorMessage ="Tên loại phòng ban có tối đa 255 kí tự")]
        public string DepartmentName {get;set;} =String.Empty;

        
        public Guid GetKey()
        {
            return DepartmentId;
        }
    }
}
