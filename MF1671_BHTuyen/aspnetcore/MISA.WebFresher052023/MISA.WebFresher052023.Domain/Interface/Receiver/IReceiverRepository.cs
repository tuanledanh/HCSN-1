using MISA.WebFresher052023.Domain.Entity.Receiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface.Receiver
{
    public interface IReceiverRepository
    {
        #region Methods
        /// <summary>
        /// Lấy danh sách người nhận theo mã chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetId">Mã chứng từ điều chuyển</param>
        /// <returns>Danh sách người nhận</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<IEnumerable<ReceiverEntity>> GetManyByTransferAssetIdAsync(Guid transferAssetId);

        /// <summary>
        /// Lấy danh sách người nhận của các chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetIds">Danh sách mã các chứng từ điều chuyển</param>
        /// <returns>Danh sách người nhận</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task<IEnumerable<ReceiverEntity>> FindManyByTransferAssetIdsAsync(List<Guid> transferAssetIds);

        /// <summary>
        /// Lấy danh sách người nhận theo nhiều id người nhận
        /// </summary>
        /// <param name="receiverIds">Danh sách id cần tìm</param>
        /// <returns>Danh sách người nhận</returns>
        public Task<IEnumerable<ReceiverEntity>> FindManyAsync(List<Guid> receiverIds);

        /// <summary>
        /// Tạo mới nhiều người nhận của một chứng từ
        /// </summary>
        /// <param name="receivers">Danh sách các người nhận cần tạo</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task CreateManyAsync(IEnumerable<ReceiverEntity> receivers);

        /// <summary>
        /// Cập nhật nhiều người nhận của một chứng từ
        /// </summary>
        /// <param name="receivers">Danh sách các người nhận cần cập nhật</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task UpdateManyAsync(IEnumerable<ReceiverEntity> receivers);

        /// <summary>
        /// Xóa nhiều người nhận của một chứng từ
        /// </summary>
        /// <param name="receivers">Danh sách các người nhận cần xóa</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task DeleteManyAsync(IEnumerable<ReceiverEntity> receivers); 
        #endregion

    }
}
