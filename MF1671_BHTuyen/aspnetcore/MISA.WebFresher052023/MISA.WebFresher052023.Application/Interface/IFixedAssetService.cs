using MISA.WebFresher052023.Application.Dto.FixedAsset;

namespace MISA.WebFresher052023.Application.Interface
{
    public interface IFixedAssetService : IBaseService<FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>
    {
        /// <summary>
        /// Hàm sinh mã tài sản gợi ý
        /// </summary>
        /// <returns>Mã tài sản</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<string> GetFixedAssetCodeAsync();

        /// <summary>
        /// Xuất danh sách tài sản ra file excel
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách Id của tài sản</param>
        /// <returns>File Excel trả về</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public Task<byte[]> ExportListFixedAssetToExcelAsync(List<Guid> fixedAssetIds);

        public Task<FixedAssetPagingDto> GetFixedAssetPagingAsync(FixedAssetFilterDto fixedAssetFilterDto);

        public Task<FixedAssetPagingDto> GetFixedAssetTransferPagingAsync(FixedAssetFilterDto fixedAssetFilterDto);

        public Task<IEnumerable<FixedAssetDto>> GetFixedAssetFilterAsync(string? FixedAssetCodeOrName, string? DepartmentName, string? FixedAssetCategoryName);

        public Task DeleteManyAsync(List<Guid> fixedAssetIds);

    }
}
