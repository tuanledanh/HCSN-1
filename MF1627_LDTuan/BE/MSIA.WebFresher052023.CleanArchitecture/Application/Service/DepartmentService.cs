using Application.DTO.Assett;
using Application.DTO.Depart;
using Application.Interface;
using AutoMapper;
using Domain.Entity;
using Domain.Exceptions;
using Domain.Interface.Assett;
using Domain.Interface.Assettype;
using Domain.Interface.Depart;
using Domain.Model;

namespace Application.Service
{
    public class DepartmentService : BaseService<Department, DepartmentModel, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>, IDepartmentService
    {
        #region Constructors
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper) : base(departmentRepository, mapper)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gọi đến hàm InsertAsync ở repository để xử lý thêm mới một bản ghi
        /// </summary>
        /// <param name="departmentCreateDto">Thông tin của bản ghi</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<bool> InsertAsync(DepartmentCreateDto departmentCreateDto)
        {
            // Thêm check mã trùng
            var entity = _mapper.Map<Department>(departmentCreateDto);
            bool result = await _baseRepository.InsertAsync(entity);
            if (!result)
            {
                throw new Exception("Service: Không thể thêm mới phòng ban");
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Gọi đến hàm UpdateAsync ở repository để xử lý cập nhật một bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Thông tin của bản ghi</param>
        /// <returns>True nếu cập nhật thành công, false nếu cập nhật thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<bool> UpdateAsync(Guid id, DepartmentUpdateDto departmentUpdateDto)
        {
            var existDepart = await _baseRepository.GetAsync(id);
            if (existDepart != null)
            {
                // Thêm exception mới là DuplicateException
                throw new DuplicateExeption("Mã đã tồn tại");
            }
            // Thêm check mã trùng
            var entity = _mapper.Map<Department>(departmentUpdateDto);
            bool result = await _baseRepository.UpdateAsync(entity);
            if (!result)
            {
                throw new Exception("Service: Không thể cập nhật phòng ban");
            }
            else
            {
                return result;
            }
        } 
        #endregion
    }
}
