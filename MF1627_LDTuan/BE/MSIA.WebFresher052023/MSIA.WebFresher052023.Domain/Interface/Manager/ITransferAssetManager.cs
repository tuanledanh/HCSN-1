using Domain.Entity;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface.Manager.Base;

namespace MSIA.WebFresher052023.Domain.Interface.Manager
{
    public interface ITransferAssetManager : IBaseManager<TransferAsset, TransferAssetModel>
    {
        void CheckNullRequest<T>(TransferAsset? transferAsset, List<T>? listTransferAssetDetail);
        void CheckDate(TransferAsset? transferAsset);
        Task CheckExistAsset(List<TransferAssetDetail> listTransferAssetDetails);
    }
}
