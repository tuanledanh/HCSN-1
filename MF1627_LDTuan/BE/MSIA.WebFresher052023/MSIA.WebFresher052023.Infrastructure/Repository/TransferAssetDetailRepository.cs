using Dapper;
using Domain.Entity;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface;
using MSIA.WebFresher052023.Domain.Interface.Repository;
using MSIA.WebFresher052023.Infrastructure.Repository.Base;
using System.Data;
using static Dapper.SqlMapper;

namespace Infrastructure.Repository
{
    public class TransferAssetDetailRepository : BaseRepository<TransferAssetDetail, TransferAssetDetailModel>, ITransferAssetDetailRepository
    {
        #region Constructor
        public TransferAssetDetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy danh sách các bản ghi trong danh sách id
        /// </summary>
        /// <param name="ids">Danh sách id của các bản ghi cần lấy dữ liệu</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: ldtuan (06/09/2023)
        public async Task<List<TransferAssetDetailModel>> GetListDetailByIdsAsync(List<Guid> ids)
        {
            string listId = "";
            if (ids != null && ids.Count > 0)
            {
                listId = string.Join(",", ids.Select(id => id.ToString()));
            }
            string procedureName = "Proc_GetTransferDetaiByAssetId";
            var parameters = new
            {
                p_List = listId
            };

            var entities = await _unitOfWork.Connection.QueryAsync<TransferAssetDetailModel>(procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return entities.ToList();
        } 
        #endregion
    }
}
