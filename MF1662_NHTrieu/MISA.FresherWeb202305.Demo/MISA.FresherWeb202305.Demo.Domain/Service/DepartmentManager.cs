using MISA.FresherWeb202305.Demo.Domain.Enums;
using MISA.FresherWeb202305.Demo.Domain.Exceptions;
using MISA.FresherWeb202305.Demo.Domain.Interface.department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Domain.Service
{
    public class DepartmentManager : IDepartmentManager
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentManager(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Check trùng mã phòng ban
        /// </summary>
        /// <param name="departmentCode">Mã đơn vị</param>
        /// <param name="oldDepartmentCode">Mã đơn vị cũ (trong trường hợp cập nhật mã)</param>
        /// CreatedBy: nhtrieu (15/07/2023)
        public async Task CheckExistDepartmentCode(string departmentCode, string? oldDepartmentCode = null)
        {
            var existDepartment = await _departmentRepository.FindByCodeAsync(departmentCode);
            if (existDepartment != null && existDepartment.DepartmentCode != oldDepartmentCode)
            {
                throw new ConflicException($"Mã loại phòng ban '{departmentCode}' đã tồn tại");
            }
        }
    }
}
