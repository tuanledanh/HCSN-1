using Dapper;
using MISA.WebFresher052023.Domain.Entity.Receiver;
using MISA.WebFresher052023.Domain.Entity.TransferAssetDetail;
using MISA.WebFresher052023.Domain.Interface.TransferAssetDetail;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using MISA.WebFresher052023.Domain.Model.TransferAssetDetail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Infrastructure.Repository
{
    public class TransferAssetDetailRepository : ITransferAssetDetailRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransferAssetDetailRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<TransferAssetDetailEntity>> FindManyAsync(List<Guid> transferAssetDetailIds)
        {
            var query = $"SELECT * FROM transfer_asset_detail WHERE TransferAssetDetailId IN @TransferAssetDetailIds;";
            var parameters = new DynamicParameters();
            parameters.Add("TransferAssetDetailIds", transferAssetDetailIds.Select(x => x.ToString()));

            var transferAssetDetails = await _unitOfWork.Connection.QueryAsync<TransferAssetDetailEntity>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return transferAssetDetails;
        }

        public async Task<TransferAssetDetailPagingModel> GetTransferAssetDetailPagingAsync(TransferAssetDetailFilterModel transferAssetDetailFilterModel)
        {
            var procedureName = "Proc_GetTransferAssetDetailPaging";

            var parameters = new DynamicParameters();
            parameters.Add("PageLimit", transferAssetDetailFilterModel.PageLimit);
            parameters.Add("PageNumber", transferAssetDetailFilterModel.PageNumber);
            parameters.Add("TransferAssetId", transferAssetDetailFilterModel.TransferAssetId);

            parameters.Add("TransferAssetDetailTotal", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var transferAssetDetails = await _unitOfWork.Connection.QueryAsync<TransferAssetDetailEntity>(procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            var transferAssetDetailTotal = parameters.Get<int>("TransferAssetDetailTotal");

            var TransferAssetDetailPagingModel = new TransferAssetDetailPagingModel
            {
                TransferAssetDetails = transferAssetDetails,
                TransferAssetDetailTotal = transferAssetDetailTotal
            };

            return TransferAssetDetailPagingModel;
        }

        public async Task CreateManyAsync(IEnumerable<TransferAssetDetailEntity> transferAssetDetails)
        {
            var value = "";

            var insert = "";

            foreach (PropertyInfo propertyInfo in typeof(TransferAssetDetailEntity).GetProperties())
            {
                if (value != "")
                {
                    value += ", ";
                    insert += ", ";
                }
                value += propertyInfo.Name;
                insert += $"@{propertyInfo.Name}";
            }

            var query = $"INSERT INTO transfer_asset_detail ( {value} ) VALUES ( {insert} );";

            await _unitOfWork.Connection.ExecuteAsync(query, transferAssetDetails, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);
        }

        public async Task UpdateManyAsync(IEnumerable<TransferAssetDetailEntity> transferAssetDetails)
        {
            var where = "";
            var set = "";

            foreach (PropertyInfo propertyInfo in typeof(TransferAssetDetailEntity).GetProperties())
            {
                if (propertyInfo.Name != null)
                {
                    if (propertyInfo.Name == $"TransferAssetDetailId")
                    {
                        where = $"{propertyInfo.Name} = @{propertyInfo.Name}";
                    }
                    else
                    {
                        if (propertyInfo.Name != "CreatedBy" && propertyInfo.Name != "CreatedDate")
                        {
                            if (set != "")
                            {
                                set += ", ";
                            }
                            set += $"{propertyInfo.Name} = @{propertyInfo.Name}";
                        }
                    }
                }
            }
            var query = $"UPDATE transfer_asset_detail SET {set} WHERE {where};";
            await _unitOfWork.Connection.ExecuteAsync(query, transferAssetDetails, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);
        }

        public async Task DeleteManyAsync(IEnumerable<TransferAssetDetailEntity> transferAssetDetails)
        {
            var query = $"DELETE FROM transfer_asset_detail WHERE TransferAssetDetailId IN @TransferAssetDetailIds;";

            var parameters = new DynamicParameters();

            parameters.Add("TransferAssetDetailIds", transferAssetDetails.Select(e => e.TransferAssetDetailId.ToString()));

            await _unitOfWork.Connection.ExecuteAsync(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);
        }

        public async Task DeleteAsync(TransferAssetDetailEntity transferAssetDetail)
        {
            var query = $"DELETE FROM transfer_asset_detail WHERE TransferAssetDetailId = @TransferAssetDetailId;";

            var parameters = new DynamicParameters();
            parameters.Add("TransferAssetDetailId", transferAssetDetail.TransferAssetDetailId.ToString());

            await _unitOfWork.Connection.ExecuteAsync(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<TransferAssetDetailEntity>> GetManyByTransferAssetIdsAsync(List<Guid> transferAssetIds)
        {
            var query = $"SELECT * FROM transfer_asset_detail WHERE TransferAssetId IN @TransferAssetIds;";
            var parameters = new DynamicParameters();
            parameters.Add("TransferAssetIds", transferAssetIds.Select(x => x.ToString()));

            var transferAssetDetails = await _unitOfWork.Connection.QueryAsync<TransferAssetDetailEntity>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return transferAssetDetails;
        }

        public async Task<TransferAssetDetailEntity> FindAsync(Guid transferAssetDetailId)
        {
            var query = $"SELECT * FROM transfer_asset_detail WHERE TransferAssetDetailId = @TransferAssetDetailId;";

            var parameters = new DynamicParameters();
            parameters.Add("TransferAssetDetailId", transferAssetDetailId.ToString());

            var transferAssetDetail = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TransferAssetDetailEntity>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return transferAssetDetail;
        }

        public Task<TransferAssetDetailEntity> GetTransferAssetDetailLastetAsync(Guid FixedAssetId)
        {
            var query = $"SELECT * FROM transfer_asset_detail tad WHERE tad.FixedAssetId = @FixedAssetId ORDER BY tad.ModifiedDate DESC LIMIT 1;";

            var parameters = new DynamicParameters();
            parameters.Add("FixedAssetId", FixedAssetId.ToString());

            var transferAssetDetail = _unitOfWork.Connection.QueryFirstOrDefaultAsync<TransferAssetDetailEntity>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return transferAssetDetail;
        }

        public async Task<IEnumerable<TransferAssetDetailEntity>> GetManyByTransferAssetIdAsync(Guid transferAssetId)
        {
            var query = $"SELECT * FROM transfer_asset_detail WHERE TransferAssetId = @TransferAssetId;";

            var parameters = new DynamicParameters();
            parameters.Add("TransferAssetId", transferAssetId.ToString());

            var transferAssetDetailEntities = await _unitOfWork.Connection.QueryAsync<TransferAssetDetailEntity>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return transferAssetDetailEntities;
        }
    }
}
