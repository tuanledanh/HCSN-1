using Application.DTO;
using Domain.Model;
using MSIA.WebFresher052023.Application.Interface.Base;
using MSIA.WebFresher052023.Application.Response.Base;
using MSIA.WebFresher052023.Domain.Entity;

namespace Application.Interface
{
    public interface ITransferAssetService : IBaseService<TransferAsset, TransferAssetModel, TransferAssetDto, TransferAssetCreateDto, TransferAssetUpdateDto, TransferAssetUpdateMultiDto>
    {
        /// <summary>
        /// Lấy danh sách các bản ghi, có phân trang, tìm kiếm theo mã code, lọc theo phòng ban và loại tài sản
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageLimit">Số lượng tối đa bản ghi mỗi trang</param>
        /// <param name="filterName">Mã code để tìm kiếm</param>
        /// <returns>Danh sách tài sản đáp ứng đúng các điều kiện</returns>
        /// Created by: ldtuan (01/09/2023)
        Task<BaseFilterResponse<TransferAssetDto>> GetAllCustomAsync(int? pageNumber, int? pageLimit, string filterName);
        Task GetNewestTransferAsset(Guid transferId, List<Guid> assetIds);
    }
}
