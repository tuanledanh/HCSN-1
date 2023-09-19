using MISA.WebFresher052023.Application.Dto.TransferAssetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Interface
{
    public interface ITransferAssetDetailService
    {
        #region Methods
        /// <summary>
        /// Phân trang tài sản điều chuyển theo chứng từ
        /// </summary>
        /// <param name="transferAssetDetailFilter">Các trường phân trang</param>
        /// <returns>Danh sách tài sản điều chuyển</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<TransferAssetDetailPagingDto> GetTransferAssetDetailPagingAsync(TransferAssetDetailFilterDto transferAssetDetailFilter);

        /// <summary>
        /// Lấy nhiều tài sản điều chuyển theo một chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetId">Mã id của chứng từ điều chuyển</param>
        /// <returns>Danh sách tài sản điều chuyển</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<IEnumerable<TransferAssetDetailDto>> GetManyByTransferAssetIdAsync(Guid transferAssetId);

        /// <summary>
        /// Lấy nhiều tài sản điều chuyển theo nhiều một chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetIds">Mã id của chứng từ điều chuyển</param>
        /// <returns>Danh sách tài sản điều chuyển</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<IEnumerable<TransferAssetDetailDto>> GetManyByTransferAssetIdsAsync(List<Guid> transferAssetIds);

        /// <summary>
        /// Tạo mới nhiều tài sản điều chuyển của một chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetDetails">Danh sách tài sản điều chuyển cần tạo</param>
        /// <param name="TransferAssetId">Mã id của chứng từ</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task CreateManyAsync(IEnumerable<TransferAssetDetailDto> transferAssetDetails, Guid TransferAssetId);

        /// <summary>
        /// Cập nhật nhiều tài sản điều chuyển của một chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetDetails">Danh sách tài sản điều chuyển cần tạo</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task UpdateManyAsync(IEnumerable<TransferAssetDetailDto> transferAssetDetails);

        /// <summary>
        /// Xóa nhiều tài sản điều chuyển của một chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetDetailIds">Danh sách mã Id của các chứng từ cần xóa</param>
        /// <returns></returns>
        public Task DeleteManyAsync(List<Guid> transferAssetDetailIds);
        #endregion
    }
}
