using MISA.WebFresher052023.Domain.Entity.TransferAsset;
using MISA.WebFresher052023.Domain.Entity.TransferAssetDetail;
using MISA.WebFresher052023.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface.TransferAsset
{
    public interface ITransferAssetManager : IBaseManager<TransferAssetEntity>
    {
        #region Methods
        /// <summary>
        /// Kiểm tra ngày điều chuyển khi thêm và tạo mới chứng từ điều chuyển
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách mã Id của các tài sản điều chuyển</param>
        /// <param name="transferDate">Ngày điều chuyển</param>
        /// <param name="transactionDate">Ngày chứng từ</param>
        /// <param name="transferAssetId">Mã chứng từ</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task CheckTransferDateAsync(List<Guid> fixedAssetIds, DateTime transferDate, DateTime transactionDate, Guid? transferAssetId, ActionMode actionMode);

        /// <summary>
        /// Kiểm tra khi xóa nhiều chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetIds">Danh sách các mã chứng từ điều chuyển</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task CheckDeleteManyTransferAssetAsync(List<Guid> transferAssetIds); 
        #endregion

    }
}
