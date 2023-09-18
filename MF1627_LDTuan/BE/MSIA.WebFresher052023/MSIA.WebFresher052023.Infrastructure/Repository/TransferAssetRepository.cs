using Dapper;
using Domain.Entity;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface;
using MSIA.WebFresher052023.Domain.Interface.Repository;
using MSIA.WebFresher052023.Domain.Model;
using MSIA.WebFresher052023.Infrastructure.Repository.Base;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;
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

        #region Methods
        /// <summary>
        /// Hàm chung cho việc lấy object theo mã code
        /// </summary>
        /// <typeparam name="T">Loại đối tượng muốn trả về</typeparam>
        /// <param name="code">Mã code cần kiểm tra</param>
        /// <returns>Một đối tượng</returns>
        /// Created by: ldtuan (02/09/2023)
        public override async Task<TransferAssetModel?> FindByCodeAsync(string code)
        {
            var tableName = TableName;
            string procedureName = "Proc_Get" + tableName + "ByCode";
            var paramName = "p_Code";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, code);
            // Làm như này vẫn ok: QueryAsync<FixedAssetTransferModel> + bỏ splitOn
            // Nhưng làm như bên dưới thì có thể lấy được cả thông tin của transferAsset ra ngoài
            // Đây là cách map với bảng quan hệ 1 nhiều
            
            var results = await _unitOfWork.Connection.QueryAsync<FixedAssetTransferModel, TransferAssetModel, ReceiverTransferModel, TransferAssetModel>(
                    procedureName,
                    (fixedAssetTransferModel, transferAssetModel, receiverTransferModel) =>
                    {
                        transferAssetModel.FixedAssetTransfers ??= new();
                        transferAssetModel.FixedAssetTransfers.Add(fixedAssetTransferModel);

                        transferAssetModel.ReceiverTransfers ??= new();
                        transferAssetModel.ReceiverTransfers.Add(receiverTransferModel);

                        return transferAssetModel;
                    },
                    dynamicParams,
                    commandType: CommandType.StoredProcedure,
                    splitOn: "TransferAssetId, ReceiverId", // Chia kết quả dựa trên cột TransferAssetId, dùng cái này để xác định 
                                                            // Tham số SplitOn cho Dapper biết nên sử dụng (các) cột nào để phân chia dữ liệu thành nhiều đối tượng
                                                            // SplitOn yêu cầu Dapper phân chia dữ liệu trên cột TransferAssetId.
                                                            // Mọi thứ trong cột đó sẽ ánh xạ tới tham số đầu tiên fixedAssetTransferModel
                                                            // và mọi thứ khác từ cột đó trở đi sẽ được ánh xạ tới tham số đầu vào thứ hai transferAssetModel
                    transaction: _unitOfWork.Transaction
                );
            if (results != null && results.Any())
            {
                var transferAsset = new TransferAssetModel();
                transferAsset = results.FirstOrDefault();
                foreach (var result in results.Skip(1))
                {
                    transferAsset.FixedAssetTransfers ??= new();
                    if(result.FixedAssetTransfers != null)
                    {
                        transferAsset.FixedAssetTransfers.AddRange(result.FixedAssetTransfers);
                    }
                    transferAsset.ReceiverTransfers ??= new();
                    if(result.ReceiverTransfers != null)
                    {
                        transferAsset.ReceiverTransfers.AddRange(result.ReceiverTransfers);
                    }
                }
                // Loại bỏ các đối tượng trùng lặp từ FixedAssetTransfers
                if(transferAsset.FixedAssetTransfers != null &&  transferAsset.FixedAssetTransfers.FirstOrDefault() != null && transferAsset.FixedAssetTransfers.Count > 0)
                {
                    transferAsset.FixedAssetTransfers = transferAsset.FixedAssetTransfers.DistinctBy(fixedAsset => fixedAsset.FixedAssetId).ToList();
                }

                // Loại bỏ các đối tượng trùng lặp từ ReceiverTransfers
                if (transferAsset.ReceiverTransfers != null &&  transferAsset.ReceiverTransfers.FirstOrDefault() != null && transferAsset.ReceiverTransfers.Count > 0)
                {
                    transferAsset.ReceiverTransfers = transferAsset.ReceiverTransfers.DistinctBy(recei => recei.ReceiverId).ToList();
                }
                return transferAsset;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Lấy chứng từ mới nhất trong những chứng từ mà tài sản có
        /// </summary>
        /// <param name="assetIds">Danh sách id của tài sản</param>
        /// <returns>Chứng từ mới nhất trong đống tài sản</returns>
        /// Created by: ldtuan (07/09/2023)
        public async Task<List<TransferAsset>> GetNewestTransferAssetByAssetId(List<Guid> assetIds)
        {
            string listAssetId = "";
            if (assetIds != null && assetIds.Count > 0)
            {
                listAssetId = string.Join(",", assetIds.Select(assetId => assetId.ToString()));
            }

            string procedureName = "Proc_GetNewestTransferAssetByAssetId";
            var paramName = "p_List";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, listAssetId);
            
            var transferAsset = await _unitOfWork.Connection.QueryAsync<TransferAsset>(procedureName, dynamicParams, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return transferAsset.ToList();
        }

        /// <summary>
        /// Lấy các chứng từ của các tài sản truyền đến
        /// </summary>
        /// <param name="assetIds">Danh sách id tài sản</param>
        /// <returns>Danh sách chứng từ, có chứa id tài sản</returns>
        /// Created by: ldtuan (17/09/2023)
        public async Task<List<TransferAssetDeleteModel>> GetAllTransferAssetByAssetId(List<Guid> assetIds)
        {
            string listAssetId = "";
            if (assetIds != null && assetIds.Count > 0)
            {
                listAssetId = string.Join(",", assetIds.Select(assetId => assetId.ToString()));
            }

            string procedureName = "Proc_GetTransferAssetByAssetId";
            var paramName = "p_List";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, listAssetId);

            var transferAsset = await _unitOfWork.Connection.QueryAsync<TransferAssetDeleteModel>(procedureName, dynamicParams, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return transferAsset.ToList();
        }

        /// <summary>
        /// Truyền vào 1 danh sách chứng từ, tìm các tài sản trong chứng từ đó, rồi tìm tất cả các chứng từ có chứa các tài sản này
        /// </summary>
        /// <param name="transferAssetIds">Danh sách id chứng từ</param>
        /// <returns>Danh sách chứng từ của các tài sản trong các chứng từ truyền vào</returns>
        /// Created by: ldtuan (09/09/2023)
        public async Task<List<TransferAssetDeleteModel>> GetAllTransferAssetOfAsset(List<Guid> transferAssetIds)
        {
            string listAssetId = "";
            if (transferAssetIds != null && transferAssetIds.Count > 0)
            {
                listAssetId = string.Join(",", transferAssetIds.Select(transferAssetId => transferAssetId.ToString()));
            }

            string procedureName = "Proc_GetAllTransferAssetOfAsset";
            var paramName = "p_List";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, listAssetId);

            var transferAsset = await _unitOfWork.Connection.QueryAsync<TransferAssetDeleteModel>(procedureName, dynamicParams, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return transferAsset.ToList();
        }
        #endregion
    }
}
