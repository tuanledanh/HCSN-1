using MISA.WebFresher052023.Domain.Entity.TransferAssetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface.TransferAssetDetail
{
    public interface ITransferAssetDetailManager
    {
        #region Methods
        /// <summary>
        /// Kiểm tra xóa nhiều chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetDetailIds">Danh sách mã ID cần xóa</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task CheckDeleteManyAsync(List<Guid> transferAssetDetailIds);

        /// <summary>
        /// Kiểm tra phòng ban khi thêm và cập nhật nhiều tài sản điều chuyển
        /// </summary>
        /// <param name="transferAssetDetail">Danh sách tài sản điều chuyển cần kiêm tra</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task CheckDepartmentAsync(IEnumerable<TransferAssetDetailEntity> transferAssetDetail); 
        #endregion

    }
}
