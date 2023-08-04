using AutoMapper;
using MISA.FresherWeb202305.Demo.Application.Dto;
using MISA.FresherWeb202305.Demo.Application.Interface;
using MISA.FresherWeb202305.Demo.Application.Services.Base;
using MISA.FresherWeb202305.Demo.Domain.Entity;
using MISA.FresherWeb202305.Demo.Domain.Interface.Base;
using MISA.FresherWeb202305.Demo.Domain.Interface.department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Services
{
    public class DepartmentServices : BaseCodeService<Department, Department, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>, IDepartmentServices
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentManager _departmentManager;

        public DepartmentServices(IDepartmentRepository departmentRepository, IMapper mapper, IDepartmentManager departmentManager)
            : base(departmentRepository, mapper)
        {
            _departmentManager = departmentManager;
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Ánh xạ thông tin từ DTO sang Entity khi tạo mới đơn vị
        /// </summary>
        /// <param name="entityCreateDto">DTO chứa thông tin đơn vị cần tạo mới</param>
        /// <returns>Đối tượng Department đã được ánh xạ thông tin từ DTO</returns>
        /// CreatedBy: nhtrieu (15/07/2023)
        protected async override Task<Department> MapCreateDtoToEntityAsync(DepartmentCreateDto entityCreateDto)
        {
            // Kiểm tra trùng mã đơn vị
            await _departmentManager.CheckExistDepartmentCode(entityCreateDto.DepartmentCode);

            // Ánh xạ thông tin từ DTO sang Entity
            var department = _mapper.Map<Department>(entityCreateDto);
            department.DepartmentId = Guid.NewGuid();
            department.CreatedDate = DateTime.Now;
            department.CreatedBy = "htrieu";
            department.ModifiedDate = DateTime.Now;
            department.ModifiedBy = "htrieu";

            return department;
        }

        /// <summary>
        /// Ánh xạ thông tin từ DTO sang Entity khi cập nhật đơn vị
        /// </summary>
        /// <param name="entityId">Id của đơn vị cần cập nhật</param>
        /// <param name="entityUpdateDto">DTO chứa thông tin đơn vị cần cập nhật</param>
        /// <returns>Đối tượng Department đã được ánh xạ thông tin từ DTO</returns>
        /// CreatedBy: nhtrieu (15/07/2023)
        protected async override Task<Department> MapUpdateDtoToEntityAsync(Guid entityId, DepartmentUpdateDto entityUpdateDto)
        {
            // Lấy thông tin đơn vị cũ từ cơ sở dữ liệu
            var oldDepartment = await _departmentRepository.GetByIdAsync(entityId);

            // Kiểm tra trùng mã đơn vị khi cập nhật
            await _departmentManager.CheckExistDepartmentCode(entityUpdateDto.DepartmentCode, oldDepartment.DepartmentCode);

            // Ánh xạ thông tin từ DTO sang Entity sử dụng phương thức Map của AutoMapper
            var newDepartment = _mapper.Map<Department>(oldDepartment);
            newDepartment.ModifiedDate = DateTime.Now;
            newDepartment.ModifiedBy = "htrieu";

            return newDepartment;
        }
    }
}
