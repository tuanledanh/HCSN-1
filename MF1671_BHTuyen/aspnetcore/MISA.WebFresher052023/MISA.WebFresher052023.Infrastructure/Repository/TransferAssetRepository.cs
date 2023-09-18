using Dapper;
using MISA.WebFresher052023.Domain.Entity.TransferAsset;
using MISA.WebFresher052023.Domain.Interface.TransferAsset;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using MISA.WebFresher052023.Domain.Model.FixedAsset;
using MISA.WebFresher052023.Domain.Model.TransferAsset;
using MISA.WebFresher052023.Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Infrastructure.Repository
{
    public class TransferAssetRepository : BaseRepository<TransferAssetEntity>, ITransferAssetRepository
    {

        #region Properties
        /// <summary>
        /// Ghi đè tên bảng dạng số nhiều
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023) 
        public override string TableNamesProc { get; protected set; } = "TransferAssets";

        public override string TableNameProc { get; protected set; } = "TransferAsset";

        /// <summary>
        /// Ghi đè tên khóa chính
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string TableNameId { get; protected set; } = "TransferAssetId";
        #endregion

        public TransferAssetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<string> GetTransferAssetCodeNewAsync()
        {
            var procedureName = $"Proc_Get{TableNameProc}CodeNew";
            var fixedAssetCode = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<string>(procedureName, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return fixedAssetCode;
        }

        public async Task<TransferAssetPagingModel> GetTransferAssetPagingAsync(TransferAssetFilterModel transferAssetFilterModel)
        {
            var procdureName = $"Proc_Get{TableNameProc}Paging";

            var parameters = new DynamicParameters();
            parameters.Add("PageLimit", transferAssetFilterModel.PageLimit);
            parameters.Add("PageNumber", transferAssetFilterModel.PageNumber);

            parameters.Add("TransferAssetTotal", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var transferAssets = await _unitOfWork.Connection.QueryAsync<TransferAssetEntity>(procdureName, parameters, commandType: CommandType.StoredProcedure);

            var transferAssetTotal = parameters.Get<int>("TransferAssetTotal");

            var transferAssetPagingModel = new TransferAssetPagingModel
            {
                TransferAssetTotal = transferAssetTotal,
                TransferAssets = transferAssets
            };

            return transferAssetPagingModel;
        }

        public async Task<FixedAssetTransferInfo> GetTransferAssetsByFixedAssetIdsAsync(List<Guid> fixedAssetIds)
        {
            var procedureName = "Proc_GetTransferAssetsByFixedAssetIds";

            var parameters = new DynamicParameters();

            parameters.Add("FixedAssetIds", string.Join(',', fixedAssetIds));
            parameters.Add("TransferAssetId", null);
            parameters.Add("FixedAssetCode", dbType: DbType.String, direction: ParameterDirection.Output);

            var tranferAssets = await _unitOfWork.Connection.QueryAsync<TransferAssetEntity>(procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            var fixedAssetCode = parameters.Get<string>("FixedAssetCode");

            var fixedAssetTransferInfo = new FixedAssetTransferInfo
            {
                FixedAssetCode = fixedAssetCode,
                TransferAssets = tranferAssets
            };

            return fixedAssetTransferInfo;
        }



        public async Task<IEnumerable<TransferAssetEntity>> FindManyAsync(List<Guid> transferAssetIds)
        {
            var query = $"SELECT * FROM transfer_asset WHERE {TableNameId} IN @TransferAssetIds";

            var parameters = new DynamicParameters();

            parameters.Add("@TransferAssetIds", transferAssetIds.Select(x => x.ToString()));

            var transferAssets = await _unitOfWork.Connection.QueryAsync<TransferAssetEntity>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return transferAssets;
        }

        public async Task DeleteManyAsync(IEnumerable<TransferAssetEntity> transferAssets)
        {
            var query = $"DELETE FROM transfer_asset WHERE TransferAssetId IN @TransferAssetIds;";

            var parameters = new DynamicParameters();

            parameters.Add("TransferAssetIds", transferAssets.Select(e => e.TransferAssetId.ToString()));

            await _unitOfWork.Connection.ExecuteAsync(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);
        }

        public async Task<FixedAssetTransferInfo> GetTransferAssetsLaterAsync(List<Guid> transferAssetIds)
        {
            var procedureName = "Proc_GetTransferAssetsLater";

            var parameters = new DynamicParameters();

            parameters.Add("TransferAssetIds", string.Join(",", transferAssetIds));

            parameters.Add("FixedAssetCode", dbType: DbType.String, direction: ParameterDirection.Output);
            parameters.Add("TransferAssetCode", dbType: DbType.String, direction: ParameterDirection.Output);
            parameters.Add("TransferDate", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            var transferAssets = await _unitOfWork.Connection.QueryAsync<TransferAssetEntity>(procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            var fixedAssetCode = parameters.Get<string>("FixedAssetCode");
            var transferAssetCode = parameters.Get<string>("TransferAssetCode");
            var transferDate = parameters.Get<DateTime?>("TransferDate");

            var fixedAssetTransferInfo = new FixedAssetTransferInfo
            {
                FixedAssetCode = fixedAssetCode,
                TransferAssets = transferAssets,
                TransferAssetCode = transferAssetCode,
                TransferDate = transferDate
            };

            return fixedAssetTransferInfo;
        }
    }
}
