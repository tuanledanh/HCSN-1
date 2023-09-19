using MISA.WebFresher052023.Domain.Entity.FixedAsset;
using MISA.WebFresher052023.Domain.Model.FixedAsset;

namespace MISA.WebFresher052023.Domain.Interface.FixedAsset
{
    public interface IFixedAssetRepository : IBaseRepository<FixedAssetEntity>
    {
        #region Methods
        /// <summary>
        /// Sinh mã tài sản
        /// </summary>
        /// <returns>Mã tài sản</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<string> GetFixedAssetCodeNewAsync();

        /// <summary>
        /// Tìm kiếm nhiều tài sản
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách mã Id của tài sản</param>
        /// <returns>Danh sách tài sản</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public Task<IEnumerable<FixedAssetEntity>> FindManyFixedAssetAsync(List<Guid> fixedAssetIds);

        /// <summary>
        /// Phân trang tài sản
        /// </summary>
        /// <param name="fixedAssetFilter">Điều kiện phân trang</param>
        /// <returns>Danh sách tài sản</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public Task<FixedAssetPagingModel> GetFixedAssetPagingAsync(FixedAssetFilterModel fixedAssetFilter);

        /// <summary>
        /// Lấy danh sách tài sản cập nhật phòng ban mới nhất
        /// </summary>
        /// <param name="fixedAssetFilter">Các trường lọc</param>
        /// <returns>Danh sách tài sản</returns>
        public Task<FixedAssetPagingModel> GetFixedAssetTransferPagingAsync(FixedAssetFilterModel fixedAssetFilter);

        /// <summary>
        /// Xuất danh sách tài sản ra file excel
        /// </summary>
        /// <param name="fixedAssetExcelModels">Danh sách các tài sản cần export</param>
        /// <returns>File Excel trả về</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public byte[] ExportListFixedAssetToExcel(IEnumerable<FixedAssetExcelModel> fixedAssetExcelModels);

        /// <summary>
        /// Filter tài sản
        /// </summary>
        /// <param name="FixedAssetCodeOrName">Mã hoặc tên tài sản</param>
        /// <param name="DepartmentName">Tên phòng ban</param>
        /// <param name="FixedAssetCategoryName">Tên loại tài sản</param>
        /// <returns>Danh sách tài sản</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public Task<IEnumerable<FixedAssetEntity>> GetFixedAssetFilterAsync(string? FixedAssetCodeOrName, string? DepartmentName, string? FixedAssetCategoryName);

        /// <summary>
        /// Xóa nhiều tài sản
        /// </summary>
        /// <param name="fixedAssets">Danh sách tài sản cần xóa</param>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public Task DeleteManyAsync(IEnumerable<FixedAssetEntity> fixedAssets);
        #endregion
    }
}
