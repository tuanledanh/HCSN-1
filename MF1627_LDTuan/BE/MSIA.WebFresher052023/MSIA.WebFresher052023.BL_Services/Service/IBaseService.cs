using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.BL_Services.Service
{
    public interface IBaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        /// <summary>
        /// Hàm lấy 1 bản ghi
        /// </summary>
        /// <param name="code">Mã code của bản ghi</param>
        /// <returns>TEntityDto</returns>
        /// Created by: ldtuan (11/07/2023)
        Task<TEntityDto> GetAsync(string code);

        /// <summary>
        /// Hàm lấy danh sách bản ghi, có phân trang và lọc
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageLimit">Số bản ghi tối đa</param>
        /// <param name="filterName">Tên của bản ghi để thực hiện lọc</param>
        /// <returns>Danh sách TEntityDto</returns>
        /// Created by: ldtuan (11/07/2023)
        Task<List<TEntityDto>> GetFilterAsync(int? pageNumber, int? pageLimit, string filterName);

        /// <summary>
        /// Hàm lấy danh sách toàn bộ bản ghi
        /// </summary>
        /// <returns>Danh sách TEntityDto</returns>
        /// Created by: ldtuan (11/07/2023)
        Task<List<TEntityDto>> GetAllAsync();

        /// <summary>
        /// Hàm thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Dữ liệu của bản ghi muốn thêm mới</param>
        /// <returns>True hoặc false tương ứng với thêm mới thành công hay thất bại</returns>
        /// Created by: ldtuan (11/07/2023)
        Task<bool> PostAsync(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Hàm cập nhật 1 bản ghi có sẵn
        /// </summary>
        /// <param name="id">Id của bản ghi cần cập nhật</param>
        /// <param name="entityUpdateDto">Thông tin mới muốn cập nhật vào bản ghi cũ</param>
        /// <returns>True hoặc false tương ứng với cập nhật thành công hay thất bại</returns>
        /// Created by: ldtuan (11/07/2023)
        Task<bool> PutAsync(Guid id, TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// Hàm xóa 1 bản ghi có sẵn
        /// </summary>
        /// <param name="code">Mã code của bản ghi cần xóa</param>
        /// <returns>True hoặc false tương ứng với xóa thành công hay thất bại</returns>
        /// Created by: ldtuan (11/07/2023)
        Task<bool> DeleteAsync(string code);
    }
}
