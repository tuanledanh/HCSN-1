using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Interface.Base
{
    public interface IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> :
        IBaseReadonlyService<TEntityDto> 
       

    {
        /// <summary>
        /// Tạo đối tượng mới
        /// </summary>
        /// <param name="entityCreateDto">Thông tin đối tượng</param>
        /// CreatedBy: nhtrieu (18/07/2023)
        Task<int> InsertAsync(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Cập nhật đối tượng
        /// </summary>
        /// <param name="entityId">Id đối tượng</param>
        /// <param name="entityUpdateDto">Thông tin đối tượng</param>
        /// CreatedBy: nhtrieu (18/07/2023)
        Task<int> UpdateAsync(Guid entityId, TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// Xoá đối tượng theo Id
        /// </summary>
        /// <param name="entityId">Id của đối tượng</param>
        /// CreatedBy: nhtrieu (18/07/2023)
        Task<int> DeleteByIdAsync(Guid entityId);

        /// <summary>
        /// Xoá nhiều đối tượng
        /// </summary>
        /// <param name="employeeIds">Danh sách Id của các đối tượng cần xoá</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: nhtrieu (18/07/2023)
        Task<int> DeleteAsync(List<Guid> entityIds);
    }
}
