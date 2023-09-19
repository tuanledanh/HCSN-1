using Dapper;
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
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public TransferAssetDetailRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy ra danh sách tài sản điều chuyển của nhiều chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetIds">Danh sách mã Id của các chứng từ điều chuyển</param>
        /// <returns>Danh sách tài sản điều chuyển</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<IEnumerable<TransferAssetDetailEntity>> GetManyByTransferAssetIdsAsync(List<Guid> transferAssetIds)
        {
            var query = $"SELECT * FROM transfer_asset_detail tad WHERE TransferAssetId IN @TransferAssetIds ORDER BY tad.ModifiedDate DESC;";
            var parameters = new DynamicParameters();
            parameters.Add("TransferAssetIds", transferAssetIds.Select(x => x.ToString()));

            var transferAssetDetails = await _unitOfWork.Connection.QueryAsync<TransferAssetDetailEntity>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return transferAssetDetails;
        }

        /// <summary>
        /// Lấy ra danh sách tài sản điều chuyển theo chứng từ điều chuyển phân trang
        /// </summary>
        /// <param name="transferAssetDetailFilter">Các trường phân trang</param>
        /// <returns>Danh sách tài sản điều chuyển</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<TransferAssetDetailPagingModel> GetTransferAssetDetailPagingAsync(TransferAssetDetailFilterModel transferAssetDetailFilter)
        {
            var procedureName = "Proc_GetTransferAssetDetailPaging";

            var parameters = new DynamicParameters();
            parameters.Add("PageLimit", transferAssetDetailFilter.PageLimit);
            parameters.Add("PageNumber", transferAssetDetailFilter.PageNumber);
            parameters.Add("TransferAssetId", transferAssetDetailFilter.TransferAssetId);

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

        /// <summary>
        /// Tìm kiếm nhiều tài sản điều chuyển theo nhiều mã Id
        /// </summary>
        /// <param name="transferAssetDetailIds">Danh sách các mã Id cần tìm kiếm</param>
        /// <returns>Danh sách tài sản điều chuyển</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<IEnumerable<TransferAssetDetailEntity>> FindManyAsync(List<Guid> transferAssetDetailIds)
        {
            var query = $"SELECT * FROM transfer_asset_detail WHERE TransferAssetDetailId IN @TransferAssetDetailIds;";
            var parameters = new DynamicParameters();
            parameters.Add("TransferAssetDetailIds", transferAssetDetailIds.Select(x => x.ToString()));

            var transferAssetDetails = await _unitOfWork.Connection.QueryAsync<TransferAssetDetailEntity>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return transferAssetDetails;
        }

        /// <summary>
        /// Tạo mới nhiều tài sản điều chuyển của một chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetDetails">Danh sách tài sản cần tạo</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
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

        /// <summary>
        /// Cập nhật nhiều tài sản điều chuyển của một chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetDetails">Danh sách các tài sản cần cập nhật</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
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

        /// <summary>
        /// Xóa nhiều tài sản điều chuyển của một chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetDetails">Danh sách các tài sản cần xóa</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task DeleteManyAsync(IEnumerable<TransferAssetDetailEntity> transferAssetDetails)
        {
            var query = $"DELETE FROM transfer_asset_detail WHERE TransferAssetDetailId IN @TransferAssetDetailIds;";

            var parameters = new DynamicParameters();

            parameters.Add("TransferAssetDetailIds", transferAssetDetails.Select(e => e.TransferAssetDetailId.ToString()));

            await _unitOfWork.Connection.ExecuteAsync(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);
        } 
        #endregion
    }
}
