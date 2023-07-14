using Dapper;
using Microsoft.Extensions.Configuration;
using MSIA.WebFresher052023.DL_Repositories.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.DL_Repositories.Repositories.Assett
{
    public class AssetRepository : BaseRepository<Asset>, IAssetRepository
    {
        public AssetRepository(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Thêm mới một tài sản
        /// </summary>
        /// <param name="asset">Thông tin của tài sản</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (12/07/2023)
        public async Task<bool> PostAsync(Asset asset)
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
        /// <param name="id">Id của tài sản cần cập nhật</param>
        /// <param name="asset">Thông tin mới của tài sản</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (12/07/2023)
        public async Task<bool> PutAsync(Guid id, Asset asset)
        {
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_AssetTypeId = id,
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
    }
}
