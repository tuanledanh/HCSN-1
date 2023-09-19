using MISA.WebFresher052023.Domain.Entity.TransferAsset;
using MISA.WebFresher052023.Domain.Model.FixedAsset;
using MISA.WebFresher052023.Domain.Model.TransferAsset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface.TransferAsset
{
    public interface ITransferAssetRepository : IBaseRepository<TransferAssetEntity>
    {
        #region Methods
        /// <summary>
        /// Lấy ra mã code gợi ý khi thêm chứng từ
        /// </summary>
        /// <returns>Mã code gợi ý</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<string> GetTransferAssetCodeNewAsync();

        /// <summary>
        /// Lấy ra thông tin phát sinh chứng từ điều chuyển theo danh sách tài sản và một chứng từ
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách mã Id của các tài sản điều chuyển</param>
        /// <param name="transferAssetId">Mã id của chứng từ điều chuyển</param>
        /// <returns>Thông tin về các chứng từ phát sinh</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<FixedAssetTransferInfo> GetTransferAssetsByFixedAssetIdsAsync(List<Guid> fixedAssetIds, Guid? transferAssetId);

        /// <summary>
        /// Lấy ra danh thông tin phát sinh chứng từ theo danh sách chứng từ
        /// </summary>
        /// <param name="transferAssetIds">Danh sách mã Id của các chứng từ</param>
        /// <returns>Thông tin về các chứng từ phát sinh</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<FixedAssetTransferInfo> GetTransferAssetsLaterAsync(List<Guid> transferAssetIds);

        /// <summary>
        /// Phân trang chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetFilterModel">Các trường lọc để phân trang</param>
        /// <returns>Danh sách chứng từ điều chuyển theo phân trang</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<TransferAssetPagingModel> GetTransferAssetPagingAsync(TransferAssetFilterModel transferAssetFilterModel);

        /// <summary>
        /// Tìm kiếm nhiều chứng từ điều chuyển theo nhiều mã chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetIds">Danh sách các mã chứng từ điều chuyển</param>
        /// <returns>Danh sách các chứng từ điều chuyển</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<IEnumerable<TransferAssetEntity>> FindManyAsync(List<Guid> transferAssetIds);

        /// <summary>
        /// Xóa nhiều chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssets">Danh sách các chứng từ cần xóa</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task DeleteManyAsync(IEnumerable<TransferAssetEntity> transferAssets);
        #endregion
    }
}
