using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Application.DTO.AssetDTO;
using MISA.WebFresher052023.HCSN.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Interface
{
    public interface IAssetService
    {
        /// <summary>
        /// Lấy tất cả tài sản từ db
        /// </summary>
        /// <returns>tất cả tài sản</returns>
        /// created by: LB.Thành (16/07/2023)
        Task<IEnumerable<AssetDto>> GetAllAsync();

        /// <summary>
        /// Lấy về 1 tài sản theo Id
        /// </summary>
        /// <param name="id">Id của tài sản muốn lấy</param>
        /// <returns>1 tài sản</returns>
        /// created by: LB.Thành (16/07/2023)
        Task<AssetDto> GetAsync(Guid id);

        /// <summary>
        /// Thêm 1 tài sản vào db
        /// </summary>
        /// <param name="entity">chi tiết của tài sản mới</param>
        /// created by: LB.Thành (16/07/2023)
        Task CreateAsync(AssetCreateDto assetCreateDto);

        /// <summary>
        /// Chỉnh sửa 1 tài sản
        /// </summary>
        /// <param name="entity">chi tiết của tài cần sửa</param>
        /// created by: LB.Thành (16/07/2023)
        Task UpdateAsync(AssetUpdateDto assetUpdateDto);

        /// <summary>
        /// Xóa 1 tài sản
        /// </summary>
        /// <param name="id">Id tài sản cần xóa</param>
        /// created by: LB.Thành (16/07/2023)
        Task DeleteAsync(Guid id);

    }
}
