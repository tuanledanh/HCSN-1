using Domain.Entity;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface.Manager.Base;

namespace MSIA.WebFresher052023.Domain.Interface.Manager
{
    public interface ITransferAssetDetailManager : IBaseManager<TransferAssetDetail, TransferAssetDetailModel>
    {
        /// <summary>
        /// Kiểm tra các chi tiết chứng từ này có chứng từ phát sinh trước đó không
        /// </summary>
        /// <param name="ids">Các id của chi tiết chứng từ</param>
        /// <returns>Nếu có chứng từ phát sinh trước đó thì ném ra exception chứa các chứng từ đó</returns>
        /// Created by: ldtuan (06/09/2023)
        Task CheckExistTransferBefore(List<Guid> ids, Guid transferAssetId);
    }
}
