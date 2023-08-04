using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Domain.Interface.Assets
{
    public interface IAssetManager
    {
        /// <summary>
        /// Check trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên cần check</param>
        /// <param name="oldEmployeeCode">Mã nhân viên cũ (trong trường hợp cập nhật mã)</param>
        /// CreatedBy: nhtrieu (14/07/2023)
        Task CheckExistAssetCode(string assetCode, string? oldAssetCode = null);

        /// <summary>
        /// Check các khoá ngoại có tồn tại hay không
        /// </summary>
        /// <param name="departmentId">Id của đơn vị</param>
        /// <param name="positionId">Id của vị trí</param>
        /// CreatedBy: nhtrieu (17/07/2023)
        Task CheckValidConstraint(Guid departmentId, Guid assetTypeId);
    }
}
