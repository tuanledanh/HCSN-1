using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Interface
{
    public interface IAssetRepository
    {
        /// <summary>
        /// Lấy tất cả tài sản từ db
        /// </summary>
        /// <returns>tất cả tài sản</returns>
        /// created by: LB.Thành (16/07/2023)
        Task<IEnumerable<Asset>> GetAllAsync();

        /// <summary>
        /// Lấy về 1 tài sản theo Id
        /// </summary>
        /// <param name="id">Id của tài sản muốn lấy</param>
        /// <returns>1 tài sản</returns>
        /// created by: LB.Thành (16/07/2023)
        Task<Asset> GetAsync(Guid id);

        /// <summary>
        /// Tìm kiếm 1 tài sản theo Id 
        /// </summary>
        /// <param name="id">Id của tài sản muốn tìm kiếm</param>
        /// <returns>Tài sản muốn tìm kiếm (có thể null)</returns>
        /// created by: LB.Thành (16/07/2023)
        Task<Asset?> FindAsync(Guid id);

        /// <summary>
        /// Thêm 1 tài sản vào db
        /// </summary>
        /// <param name="entity">chi tiết của tài sản mới</param>
        /// created by: LB.Thành (16/07/2023)
        Task InsertAsync(Asset entity);

        /// <summary>
        /// Chỉnh sửa 1 tài sản
        /// </summary>
        /// <param name="entity">chi tiết của tài cần sửa</param>
        /// created by: LB.Thành (16/07/2023)
        Task UpdateAsync(Asset entity);

        /// <summary>
        /// Xóa 1 tài sản
        /// </summary>
        /// <param name="entity">tài sản cần xóa</param>
        /// created by: LB.Thành (16/07/2023)
        Task DeleteAsync(Asset entity);

    }
}
