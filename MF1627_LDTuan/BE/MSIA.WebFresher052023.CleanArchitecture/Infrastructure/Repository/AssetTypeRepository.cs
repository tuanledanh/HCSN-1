using Dapper;
using Domain.Entity;
using Domain.Interface.Assettype;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repository
{
    public class AssetTypeRepository : BaseRepository<AssetType, AssetTypeModel>, IAssetTypeRepository
    {
        #region Constructor
        public AssetTypeRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Thêm mới một tài sản
        /// </summary>
        /// <param name="assetType">Thông tin của tài sản</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public async Task<bool> InsertAsync(AssetType assetType)
        {
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_AssetTypeName = assetType.AssetTypeName,
                p_AssertTypeCode = assetType.AssetTypeCode,
                p_CreatedDate = assetType.CreatedDate,
                p_CreatedBy = assetType.CreatedBy,
                p_ModifiedDate = assetType.ModifiedDate,
                p_ModifiedBy = assetType.ModifiedBy
            };
            var affectedRow = await connection.ExecuteAsync("Proc_InsertAssetType", parameters, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return affectedRow > 0;
        }

        /// <summary>
        /// Cập nhật thông tin của một tài sản
        /// </summary>
        /// <param name="assetType">Thông tin mới của tài sản</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (12/07/2023)
        public async Task<bool> UpdateAsync(AssetType assetType)
        {
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_AssetTypeName = assetType.AssetTypeName,
                p_AssertTypeCode = assetType.AssetTypeCode,
                p_CreatedDate = assetType.CreatedDate,
                p_CreatedBy = assetType.CreatedBy,
                p_ModifiedDate = assetType.ModifiedDate,
                p_ModifiedBy = assetType.ModifiedBy
            };
            var affectedRow = await connection.ExecuteAsync("Proc_UpdateAssetType", parameters, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return affectedRow > 0;
        }

        /// <summary>
        /// Xóa thông tin một bản ghi thông qua mã code
        /// </summary>
        /// <param name="assetType">Loại tài sản cần xóa</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public async Task<bool> DeleteAsync(AssetType assetType)
        {
            string procedureName = "Proc_DeleteAssetTypeById";
            var paramName = "p_Id";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, assetType.AssetTypeId);
            var connection = await GetOpenConnectionAsync();
            var affectedRow = await connection.ExecuteAsync(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return affectedRow > 0;
        } 
        #endregion
    }
}
