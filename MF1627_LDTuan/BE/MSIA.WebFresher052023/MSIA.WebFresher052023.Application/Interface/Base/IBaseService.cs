using Application.DTO;
using MSIA.WebFresher052023.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.Application.Interface.Base
{
    public interface IBaseService<TEntity, TModel, TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntityUpdateMultiDto> : IBaseReadOnlyService<TEntity, TModel, TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntityUpdateMultiDto>
    {
        #region Methods
        /// <summary>
        /// Hàm thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Dữ liệu của bản ghi muốn thêm mới</param>
        /// <returns>True hoặc false tương ứng với thêm mới thành công hay thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        Task<bool> InsertAsync(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Thêm mới nhiều bản ghi
        /// </summary>
        /// <param name="entityCreateDtos">Thông tin cần thêm mới</param>
        /// Created by: ldtuan (02/09/2023)
        Task<ApiResponse> InsertMultiAsync(List<TEntityCreateDto> entityCreateDtos);

        /// <summary>
        /// Hàm cập nhật 1 bản ghi có sẵn
        /// </summary>
        /// <param name="id">Id của bản ghi cũ</param>
        /// <param name="entityUpdateDto">Thông tin mới muốn cập nhật vào bản ghi cũ</param>
        /// <returns>True hoặc false tương ứng với cập nhật thành công hay thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        Task<bool> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// Cập nhật nhiều bản ghi
        /// </summary>
        /// <param name="entityUpdateMultiDtos">Thông tin cần cập nhật</param>
        /// Created by: ldtuan (02/09/2023)
        Task<ApiResponse> UpdateMultiAsync(List<TEntityUpdateMultiDto> entityUpdateMultiDtos);

        /// <summary>
        /// Hàm xóa 1 bản ghi có sẵn
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// <returns>True hoặc false tương ứng với xóa thành công hay thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Hàm xóa danh sách bản ghi
        /// </summary>
        /// <param name="ids">Danh sách id của các bản ghi cần xóa</param>
        /// <returns>True hoặc false tương ứng với xóa thành công hay thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        Task<bool> DeleteManyAsync(List<Guid> ids);
        #endregion
    }
}
