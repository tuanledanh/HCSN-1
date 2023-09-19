using MISA.WebFresher052023.Domain.Entity.TransferAssetDetail;
using MISA.WebFresher052023.Domain.Model.TransferAssetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface.TransferAssetDetail
{
    public interface ITransferAssetDetailRepository
    {
        #region Methods
        /// <summary>
        /// Lấy nhiều tài sản điều chuyển theo nhiều mã chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetIds">Danh sách mã chứng từ điều chuyển</param>
        /// <returns>Danh sách tài sản điều chuyển</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<IEnumerable<TransferAssetDetailEntity>> GetManyByTransferAssetIdsAsync(List<Guid> transferAssetIds);

        /// <summary>
        /// Phân trang tài sản điều chuyển theo chứng từ
        /// </summary>
        /// <param name="transferAssetDetailFilter">Các trường lọc</param>
        /// <returns>Danh sách tài sản điều chuyển theo phân trang</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<TransferAssetDetailPagingModel> GetTransferAssetDetailPagingAsync(TransferAssetDetailFilterModel transferAssetDetailFilter);

        /// <summary>
        /// Tìm kiếm nhiều tài sản điều chuyển theo nhiều mã tài sản điều chuyển
        /// </summary>
        /// <param name="transferAssetDetailIds">Danh sách mã Id cần tìm</param>
        /// <returns>Danh sách tài sản điều chuyển</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<IEnumerable<TransferAssetDetailEntity>> FindManyAsync(List<Guid> transferAssetDetailIds);

        /// <summary>
        /// Tạo mới nhiều tài sản điều chuyển của một chứng từ
        /// </summary>
        /// <param name="transferAssetDetails">Danh sách tài sản điều chuyển cần tạo mới</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task CreateManyAsync(IEnumerable<TransferAssetDetailEntity> transferAssetDetails);

        /// <summary>
        /// Cập nhật nhiều tài sản điều chuyển của một chứng từ
        /// </summary>
        /// <param name="transferAssetDetails">Danh sách các tài sản điều chuyển cần cập nhật</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task UpdateManyAsync(IEnumerable<TransferAssetDetailEntity> transferAssetDetails); 

        /// <summary>
        /// Xóa nhiều tài sản điều chuyển của một chứng từ
        /// </summary>
        /// <param name="transferAssetDetails">Danh sách tài sản điều chuyển cần xóa</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task DeleteManyAsync(IEnumerable<TransferAssetDetailEntity> transferAssetDetails);
        #endregion
    }
}
