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

        /// <summary>
        /// Ghi đè tên bảng dạng số ít
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023) 
        public override string TableNameProc { get; protected set; } = "TransferAsset";

        /// <summary>
        /// Ghi đè tên khóa chính
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string TableNameId { get; protected set; } = "TransferAssetId";
        #endregion

        #region Constructor
        public TransferAssetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy ra mã code gợi ý khi thêm chứng từ
        /// </summary>
        /// <returns>Mã code gợi ý</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<string> GetTransferAssetCodeNewAsync()
        {
            var procedureName = $"Proc_Get{TableNameProc}CodeNew";
            var fixedAssetCode = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<string>(procedureName, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return fixedAssetCode;
        }

        /// <summary>
        /// Lấy ra thông tin phát sinh chứng từ điều chuyển theo danh sách tài sản và một chứng từ
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách mã Id của các tài sản điều chuyển</param>
        /// <param name="transferAssetId">Mã id của chứng từ điều chuyển</param>
        /// <returns>Thông tin về các chứng từ phát sinh</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<FixedAssetTransferInfo> GetTransferAssetsByFixedAssetIdsAsync(List<Guid> fixedAssetIds, Guid? transferAssetId)
        {
            var procedureName = "Proc_GetTransferAssetsByFixedAssetIds";

            var parameters = new DynamicParameters();

            parameters.Add("FixedAssetIds", string.Join(',', fixedAssetIds));
            parameters.Add("TransferAssetId", transferAssetId);
            parameters.Add("FixedAssetCode", dbType: DbType.String, direction: ParameterDirection.Output);
            parameters.Add("TransferAssetCodeMin", dbType: DbType.String, direction: ParameterDirection.Output);
            parameters.Add("TransferAssetCodeMax", dbType: DbType.String, direction: ParameterDirection.Output);
            parameters.Add("TransferDateMin", dbType: DbType.DateTime, direction: ParameterDirection.Output);
            parameters.Add("TransferDateMax", dbType: DbType.DateTime, direction: ParameterDirection.Output);


            var tranferAssets = await _unitOfWork.Connection.QueryAsync<TransferAssetEntity>(procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            var fixedAssetCode = parameters.Get<string>("FixedAssetCode");
            var transferAssetCodeMin = parameters.Get<string>("TransferAssetCodeMin");
            var transferAssetCodeMax = parameters.Get<string>("TransferAssetCodeMax");
            var transferDateMin = parameters.Get<DateTime?>("TransferDateMin");
            var transferDateMax = parameters.Get<DateTime?>("TransferDateMax");

            var fixedAssetTransferInfo = new FixedAssetTransferInfo
            {
                FixedAssetCode = fixedAssetCode,
                TransferAssets = tranferAssets,
                TransferAssetCodeMin = transferAssetCodeMin,
                TransferAssetCodeMax = transferAssetCodeMax,
                TransferDateMin = transferDateMin,
                TransferDateMax = transferDateMax
            };

            return fixedAssetTransferInfo;
        }

        /// <summary>
        /// Lấy ra danh thông tin phát sinh chứng từ theo danh sách chứng từ
        /// </summary>
        /// <param name="transferAssetIds">Danh sách mã Id của các chứng từ</param>
        /// <returns>Thông tin về các chứng từ phát sinh</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
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

        /// <summary>
        /// Phân trang chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetFilter">Các trường lọc để phân trang</param>
        /// <returns>Danh sách chứng từ điều chuyển theo phân trang</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<TransferAssetPagingModel> GetTransferAssetPagingAsync(TransferAssetFilterModel transferAssetFilter)
        {
            var procedureName = $"Proc_Get{TableNameProc}Paging";

            var parameters = new DynamicParameters();
            parameters.Add("PageLimit", transferAssetFilter.PageLimit);
            parameters.Add("PageNumber", transferAssetFilter.PageNumber);

            parameters.Add("TransferAssetTotal", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var transferAssets = await _unitOfWork.Connection.QueryAsync<TransferAssetEntity>(procedureName, parameters, commandType: CommandType.StoredProcedure);

            var transferAssetTotal = parameters.Get<int>("TransferAssetTotal");

            var transferAssetPagingModel = new TransferAssetPagingModel
            {
                TransferAssetTotal = transferAssetTotal,
                TransferAssets = transferAssets
            };

            return transferAssetPagingModel;
        }


        /// <summary>
        /// Tìm kiếm nhiều chứng từ điều chuyển theo nhiều mã chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetIds">Danh sách các mã chứng từ điều chuyển</param>
        /// <returns>Danh sách các chứng từ điều chuyển</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<IEnumerable<TransferAssetEntity>> FindManyAsync(List<Guid> transferAssetIds)
        {
            var query = $"SELECT * FROM transfer_asset WHERE {TableNameId} IN @TransferAssetIds";

            var parameters = new DynamicParameters();

            parameters.Add("@TransferAssetIds", transferAssetIds.Select(transferAssetId => transferAssetId.ToString()));

            var transferAssets = await _unitOfWork.Connection.QueryAsync<TransferAssetEntity>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return transferAssets;
        }


        /// <summary>
        /// Xóa nhiều chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssets">Danh sách các chứng từ cần xóa</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task DeleteManyAsync(IEnumerable<TransferAssetEntity> transferAssets)
        {
            var query = $"DELETE FROM transfer_asset WHERE TransferAssetId IN @TransferAssetIds;";

            var parameters = new DynamicParameters();

            parameters.Add("TransferAssetIds", transferAssets.Select(transferAsset => transferAsset.TransferAssetId.ToString()));

            await _unitOfWork.Connection.ExecuteAsync(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);
        }
        #endregion
    }
}
