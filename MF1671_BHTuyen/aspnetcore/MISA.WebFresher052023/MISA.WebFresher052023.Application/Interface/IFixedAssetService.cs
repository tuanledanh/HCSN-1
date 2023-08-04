using MISA.WebFresher052023.Application.Dto.FixedAsset;
using MISA.WebFresher052023.Domain.Entity.FixedAsset;

namespace MISA.WebFresher052023.Application.Interface
{
    public interface IFixedAssetService : IBaseService<FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>
    {
        /// <summary>
        /// Xóa nhiều Dto
        /// </summary>
        /// <param name="fixedAssetIds">EstateId</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public Task DeleteManyAsync(List<string> fixedAssetIds);

        /// <summary>
        /// Hàm sinh mã tài sản gợi ý
        /// </summary>
        /// <returns>Mã tài sản</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<string> GetFixedAssetCodeAsync();

        public Task<FixedAssetPagingDto> GetFixedAssetPagingAsync(FixedAssetFilterDto fixedAssetFilterDto);

        public Task<IEnumerable<FixedAssetExcel>> FindManyByIdAsync(List<string> fixedAssetIds);
    }
}
