using Dapper;
using Domain.Entity;
using Domain.Interface.Assett;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using System.Data;
using static Dapper.SqlMapper;

namespace Infrastructure.Repository
{
    public class AssetRepository : BaseRepository<Asset, AssetModel>, IAssetRepository
    {
        #region Constructor
        public AssetRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Thêm mới một tài sản
        /// </summary>
        /// <param name="asset">Thông tin của tài sản</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public async Task<bool> InsertAsync(Asset asset)
        {
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_AssetName = asset.AssetName,
                p_AssetQuantity = asset.AssetQuantity,
                p_AssetPrice = asset.AssetPrice,
                p_AssetDepreciation = asset.AssetDepreciation,
                p_YearOfUse = asset.YearOfUse,
                p_YearOfTracking = asset.YearOfTracking,
                p_PurchaseDate = asset.PurchaseDate,
                p_StartUsingDate = asset.StartUsingDate,
                p_CreatedDate = DateTime.Now,
                p_CreatedBy = "tuan",
                p_ModifiedDate = asset.ModifiedDate,
                p_ModifiedBy = asset.ModifiedBy,
                p_AssetTypeId = asset.AssetTypeId,
                p_AssetCode = asset.AssetCode,
                p_DepartmentId = asset.DepartmentId
            };
            var affectedRow = await connection.ExecuteAsync("Proc_InsertAsset", parameters, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return affectedRow > 0;
        }

        /// <summary>
        /// Cập nhật thông tin của một tài sản
        /// </summary>
        /// <param name="asset">Thông tin mới của tài sản</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (12/07/2023)
        public async Task<bool> UpdateAsync(Asset asset)
        {
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_AssetName = asset.AssetName,
                p_AssetQuantity = asset.AssetQuantity,
                p_AssetPrice = asset.AssetPrice,
                p_AssetDepreciation = asset.AssetDepreciation,
                p_YearOfUse = asset.YearOfUse,
                p_YearOfTracking = asset.YearOfTracking,
                p_PurchaseDate = asset.PurchaseDate,
                p_StartUsingDate = asset.StartUsingDate,
                p_CreatedDate = asset.CreatedDate,
                p_CreatedBy = asset.CreatedBy,
                p_ModifiedDate = asset.ModifiedDate,
                p_ModifiedBy = asset.ModifiedBy,
                p_AssetTypeIdd = asset.AssetTypeId,
                p_AssetCode = asset.AssetCode,
                p_DepartmentId = asset.DepartmentId
            };
            var affectedRow = await connection.ExecuteAsync("Proc_UpdateAsset", parameters, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return affectedRow > 0;
        }

        /// <summary>
        /// Xóa thông tin một bản ghi thông qua id
        /// </summary>
        /// <param name="asset">Tài sản cần xóa</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public async Task<bool> DeleteAsync(Asset asset)
        {
            string procedureName = "Proc_DeleteAssetById";
            var paramName = "p_Id";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, asset.AssetId);
            var connection = await GetOpenConnectionAsync();
            var affectedRow = await connection.ExecuteAsync(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return affectedRow > 0;
        } 
        #endregion
    }
}
