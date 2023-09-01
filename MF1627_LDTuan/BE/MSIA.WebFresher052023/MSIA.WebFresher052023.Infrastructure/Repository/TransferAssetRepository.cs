using Dapper;
using Domain.Entity;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface;
using MSIA.WebFresher052023.Domain.Interface.Repository;
using MSIA.WebFresher052023.Domain.Model;
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

        /// <summary>
        /// Hàm chung cho việc lấy object theo mã code
        /// </summary>
        /// <typeparam name="T">Loại đối tượng muốn trả về</typeparam>
        /// <param name="code">Mã code cần kiểm tra</param>
        /// <returns>Một đối tượng</returns>
        /// Created by: ldtuan (20/07/2023)
        public override async Task<TransferAssetModel> FindByCodeAsync(string code)
        {
            var tableName = TableName;
            string procedureName = "Proc_Get" + tableName + "ByCode";
            var paramName = "p_Code";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, code);
            // Làm như này vẫn ok: QueryAsync<FixedAssetTransferModel> + bỏ splitOn
            // Nhưng làm như bên dưới thì có thể lấy được cả thông tin của transferAsset ra ngoài
            // Đây là cách map với bảng quan hệ 1 nhiều
            var results = await _unitOfWork.Connection.QueryAsync<FixedAssetTransferModel, TransferAssetModel, FixedAssetTransferModel>(
                    procedureName,
                    (fixedAssetTransferModel, transferAssetModel) =>
                    {
                        fixedAssetTransferModel.TransferAssetModel = transferAssetModel;
                        return fixedAssetTransferModel;
                    },
                    dynamicParams,
                    commandType: CommandType.StoredProcedure,
                    splitOn: "TransferAssetId", // Chia kết quả dựa trên cột TransferAssetId, dùng cái này để xác định 
                                                // Tham số SplitOn cho Dapper biết nên sử dụng (các) cột nào để phân chia dữ liệu thành nhiều đối tượng
                                                // SplitOn yêu cầu Dapper phân chia dữ liệu trên cột TransferAssetId.
                                                // Mọi thứ trong cột đó sẽ ánh xạ tới tham số đầu tiên fixedAssetTransferModel
                                                // và mọi thứ khác từ cột đó trở đi sẽ được ánh xạ tới tham số đầu vào thứ hai transferAssetModel
                    transaction: _unitOfWork.Transaction
                );
            var transferAsset = new TransferAssetModel();
            transferAsset = results.FirstOrDefault().TransferAssetModel;
            transferAsset.FixedAssetTranfers = new List<FixedAssetTransferModel>();
            transferAsset.FixedAssetTranfers.AddRange(results);
            return transferAsset;
        }
    }
}
