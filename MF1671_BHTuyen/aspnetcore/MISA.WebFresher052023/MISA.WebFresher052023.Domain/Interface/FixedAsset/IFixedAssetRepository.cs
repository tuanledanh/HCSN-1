using MISA.WebFresher052023.Domain.Entity.FixedAsset;

namespace MISA.WebFresher052023.Domain.Interface.FixedAsset
{
    public interface IFixedAssetRepository : IBaseRepository<FixedAssetEntity>
    {
        #region Tasks
        /// <summary>
        /// Xóa nhiều tài sản
        /// </summary>
        /// <param name="fixedAssetEntities">Danh sách các tài sản cần xoá</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task DeleteManyAsync(IEnumerable<FixedAssetEntity> fixedAssetEntities);

        /// <summary>
        /// Tìm kiếm danh sách tài sản theo mã Id
        /// </summary>
        /// <param name="entityIds"></param>
        /// <returns>Danh sách tài sản</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<IEnumerable<FixedAssetEntity>> FindManyByIdAsync(List<string> fixedAssetEntityIds);

        /// <summary>
        /// Lấy ra số tài sản theo trang và bộ lọc
        /// </summary>
        /// <param name="fixedAssetFilterEntity"></param>
        /// <returns>Danh sách tài sản theo trang và tổng số bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public Task<FixedAssetPagingEntity> GetFixedAssetPagingAsync(FixedAssetFilterEntity fixedAssetFilterEntity);

        /// <summary>
        /// Gợi ý Mã tài sản
        /// </summary>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<string> GetFixedAssetCode(); 
        #endregion

    }
}
