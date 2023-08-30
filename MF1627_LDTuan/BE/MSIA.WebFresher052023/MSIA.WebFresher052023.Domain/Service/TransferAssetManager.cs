using Domain.Entity;
using Domain.Exceptions;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface.Manager;
using MSIA.WebFresher052023.Domain.Interface.Repository;

namespace MSIA.WebFresher052023.Domain.Service
{
    public class TransferAssetManager : BaseManager<TransferAsset, TransferAssetModel>, ITransferAssetManager
    {
        public TransferAssetManager(ITransferAssetRepository transferAssetRepository) : base(transferAssetRepository)
        {
        }
    }
}
