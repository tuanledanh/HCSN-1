using Domain.Entity;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Enum;
using MSIA.WebFresher052023.Domain.Interface.Manager.Base;

namespace MSIA.WebFresher052023.Domain.Interface.Manager
{
    public interface ITransferAssetManager : IBaseManager<TransferAsset, TransferAssetModel>
    {

        /// <summary>
        /// Kiểm tra chứng từ và chi tiết chứng từ có tồn tại hay không
        /// </summary>
        /// <param name="transferAsset">Chứng từ</param>
        /// <param name="listTransferAssetDetail">Danh sách tài sản chứng từ</param>
        /// <exception cref="DataException">Lỗi data truyền về</exception>
        /// Created by: ldtuan (30/08/2023)
        void CheckNullRequest<T>(TransferAsset? transferAsset, List<T>? listTransferAssetDetail);

        /// <summary>
        /// Kiểm tra ngày điều chuyển có lớn hơn ngày chứng từ hay không, và ngày chứng điều chuyển cũng phải lớn hơn mọi ngày điều chuyển của các tài sản bên trong
        /// </summary>
        /// <param name="transferAsset">Chứng từ</param>
        /// <exception cref="DataException">Lỗi data truyền về</exception>
        /// Created by: ldtuan (30/08/2023)
        Task CheckDateAsync(TransferAsset? transferAsset, List<Guid> listAssetIds, ActionMode actionMode);

        /// <summary>
        /// Kiểm tra tài sản và phòng ban  xem tồn tại trong db hay không
        /// </summary>
        /// <param name="listTransferAssetDetails">Danh sách chi tiết chứng từ</param>
        /// <exception cref="DataException">Lỗi dữ liệu truyền vào</exception>
        /// Created by: ldtuan (31/08/2023)
        Task CheckExistAssetAsync(List<TransferAssetDetail> listTransferAssetDetails);
    }
}
