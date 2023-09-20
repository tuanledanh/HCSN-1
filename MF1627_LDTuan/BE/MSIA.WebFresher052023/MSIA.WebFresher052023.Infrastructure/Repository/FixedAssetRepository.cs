using Dapper;
using Domain.Entity;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using MSIA.WebFresher052023.Domain.Interface;
using MSIA.WebFresher052023.Domain.Interface.Repository;
using MSIA.WebFresher052023.Domain.Model;
using MSIA.WebFresher052023.Infrastructure.Repository.Base;
using System.Data;
using static Dapper.SqlMapper;

namespace Infrastructure.Repository
{
    public class FixedAssetRepository : BaseRepository<FixedAsset, FixedAssetModel>, IFixedAssetRepository
    {
        #region Constructor
        public FixedAssetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy danh sách tài sản, có phân trang, tìm kiếm theo code và lọc theo phòng ban, loại tài sản
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageLimit">Số lượng tối đa bản ghi mỗi trang</param>
        /// <param name="filterName">Mã code dùng để search</param>
        /// <param name="departmentId">Id của phòng ban, dùng để lọc</param>
        /// <param name="assetTypeId">Id của loại tài sản, dùng để lọc</param>
        /// <returns>Danh sách loại tài sản đáp ứng đúng các điều kiện trên</returns>
        /// Created by: ldtuan (23/07/2023)
        public async Task<List<FixedAssetModel>> GetFilterSearchAsync(int? pageNumber, int? pageLimit, string? filterName, string departmentId, string assetTypeId)
        {
            var tableName = TableName;
            string procedureName = "Proc_Filter" + tableName;
            var parameters = new
            {
                p_PageNumber = pageNumber,
                p_PageLimit = pageLimit,
                p_FilterName = filterName,
                p_DepartmentId = departmentId,
                p_FixedAssetCategoryId = assetTypeId
            };
            
            var entities = await _unitOfWork.Connection.QueryAsync<FixedAssetModel>(procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return entities.ToList();
        }

        /// <summary>
        /// Lấy danh sách tài sản có loại những bản ghi đã chọn để hiện thị trên form chọn tài sản cho chứng từ
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageLimit">Số lượng tối đa bản ghi mỗi trang</param>
        /// <param name="ids">Danh sách id truyền vào để loại những bản ghi có id đó ra</param>
        /// <returns>Danh sách loại tài sản đáp ứng đúng các điều kiện trên</returns>
        /// Created by: ldtuan (05/09/2023)
        public async Task<FixedAssetForTransferModel> FilterFixedAssetForTransfer(int? pageNumber, int? pageLimit, string filterName, string ids, string detailIds)
        {
            var procedureName = "Proc_FilterFixedAssetForTransfer";
            var parameters = new DynamicParameters();
            parameters.Add("p_PageNumber", pageNumber);
            parameters.Add("p_PageLimit", pageLimit);
            parameters.Add("p_FilterName", filterName);
            parameters.Add("p_List", ids);
            parameters.Add("p_ListDetail", detailIds);

            parameters.Add("p_Count", dbType: DbType.Int32, direction: ParameterDirection.Output);

            
            var entities = await _unitOfWork.Connection.QueryAsync<FixedAssetModel>(procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            var total = parameters.Get<int>("p_Count");
            var fixedAssetForTransferModel = new FixedAssetForTransferModel
            {
                TotalRecords = total,
                FixedAssetModels = entities.ToList()
            };
            return fixedAssetForTransferModel;
        }

        /// <summary>
        /// Lấy tổng số bản ghi thỏa mãn điều kiện lọc
        /// </summary>
        /// <param name="filterName">Mã code dùng để search</param>
        /// <param name="departmentId">Id của phòng ban, dùng để lọc</param>
        /// <param name="assetTypeId">Id của loại tài sản, dùng để lọc</param>
        /// <returns>Tổng bản ghi</returns>
        /// Created by: ldtuan (28/07/2023)
        public async Task<int>  GetCountFilterAsync(string? filterName, string departmentId, string assetTypeId)
        {
            var tableName = TableName;
            string procedureName = "Proc_CountFilter" + tableName;
            var parameters = new
            {
                p_FilterName = filterName,
                p_DepartmentId = departmentId,
                p_FixedAssetCategoryId = assetTypeId
            };
            
            var count = await _unitOfWork.Connection.ExecuteScalarAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return count;
        }
        #endregion
    }
}
