using Dapper;
using Domain.Entity;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface;
using MSIA.WebFresher052023.Domain.Interface.Repository;
using MSIA.WebFresher052023.Infrastructure.Repository.Base;
using System.Data;

namespace Infrastructure.Repository
{
    public class TransferAssetRepository : BaseRepository<TransferAsset, TransferAssetModel>, ITransferAssetRepository
    {
        #region Constructor
        public TransferAssetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion
    }
}
