using MISA.WebFresher052023.Domain.Entity.FixedAsset;
using MISA.WebFresher052023.Domain.Model.FixedAsset;

namespace MISA.WebFresher052023.Domain.Interface.FixedAsset
{
    public interface IFixedAssetRepository : IBaseRepository<FixedAssetEntity>
    {
        #region Tasks

        public Task<string> GetFixedAssetCodeNewAsync();

        public Task<IEnumerable<FixedAssetEntity>> FindManyFixedAssetAsync(List<Guid> fixedAssetIds);

        public Task<FixedAssetPagingModel> GetFixedAssetPagingAsync(FixedAssetFilterModel fixedAssetFilterModel);

        public Task<FixedAssetPagingModel> GetFixedAssetTransferPagingAsync(FixedAssetFilterModel fixedAssetFilterModel);

        public Task DeleteManyAsync(IEnumerable<FixedAssetEntity> fixedAssets);

        /// <summary>
        /// Xuất danh sách tài sản ra file excel
        /// </summary>
        /// <param name="fixedAssetExcelModels">Danh sách các tài sản cần export</param>
        /// <returns>File Excel trả về</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public byte[] ExportListFixedAssetToExcel(IEnumerable<FixedAssetExcelModel> fixedAssetExcelModels);

        public Task<IEnumerable<FixedAssetEntity>> GetFixedAssetFilterAsync(string? FixedAssetCodeOrName, string? DepartmentName, string? FixedAssetCategoryName);
        #endregion
    }
}
