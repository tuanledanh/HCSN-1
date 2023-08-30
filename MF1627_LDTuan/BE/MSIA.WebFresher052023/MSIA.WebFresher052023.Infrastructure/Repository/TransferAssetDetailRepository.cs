using Domain.Model;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface;
using MSIA.WebFresher052023.Domain.Interface.Repository;
using MSIA.WebFresher052023.Infrastructure.Repository.Base;

namespace Infrastructure.Repository
{
    public class TransferAssetDetailRepository : BaseRepository<TransferAssetDetail, TransferAssetDetailModel>, ITransferAssetDetailRepository
    {
        #region Constructor
        public TransferAssetDetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion
    }
}
