using Application.DTO;
using Domain.Entity;
using Domain.Model;
using MSIA.WebFresher052023.Application.Interface.Base;
using MSIA.WebFresher052023.Application.Response.Base;
using System.Data;

namespace Application.Interface
{
    public interface IFixedAssetService : IBaseService<FixedAsset, FixedAssetModel, FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto, FixedAssetUpdateMultiDto>
    {
        /// <summary>
        /// Lấy danh sách các bản ghi, có phân trang, tìm kiếm theo mã code, lọc theo phòng ban và loại tài sản
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageLimit">Số lượng tối đa bản ghi mỗi trang</param>
        /// <param name="filterName">Mã code để tìm kiếm</param>
        /// <param name="departmentId">Id của phòng ban dùng để lọc</param>
        /// <param name="assetTypeId">Id của loại tài sản dùng để lọc</param>
        /// <returns>Danh sách tài sản đáp ứng đúng các điều kiện</returns>
        /// Created by: ldtuan (23/07/2023)
        Task<BaseFilterResponse<FixedAssetDto>> GetAllCustomAsync(int? pageNumber, int? pageLimit, string filterName, string? departmentId, string? assetTypeId);

        /// <summary>
        /// Lấy dữ liệu tài sản dưới dạng dataTable
        /// </summary>
        /// <param name="idsQuery">Danh sách id các bản ghi</param>
        /// <returns>DataTable</returns>
        /// Created by: ldtuan (06/08/2023)
        Task<DataTable> ExportExcel(string idsQuery);
    }
}
