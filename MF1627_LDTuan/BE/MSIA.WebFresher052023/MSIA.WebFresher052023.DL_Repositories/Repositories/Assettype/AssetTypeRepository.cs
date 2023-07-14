using Dapper;
using Microsoft.Extensions.Configuration;
using MSIA.WebFresher052023.DL_Repositories.Entity;
using System.Data;

namespace MSIA.WebFresher052023.DL_Repositories.Repositories.Assettype
{
    public class AssetTypeRepository : BaseRepository<AssetType>, IAssetTypeRepository
    {
        public AssetTypeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Thêm mới một loại tài sản
        /// </summary>
        /// <param name="assetType">Thông tin của loại tài sản</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (12/07/2023)
        public async Task<bool> PostAsync(AssetType assetType)
        {
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_AssetTypeName = assetType.AssetTypeName,
                p_AssertTypeCode = assetType.AssetTypeCode,
                p_CreatedDate = DateTime.Now,
                p_CreatedBy = "tuan",
                p_ModifiedDate = assetType.ModifiedDate,
                p_ModifiedBy = assetType.ModifiedBy
            };
            var affectedRow = await connection.ExecuteAsync("Proc_InsertAssetType", parameters, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return affectedRow > 0;
        }

        /// <summary>
        /// Cập nhật thông tin của một loại tài sản
        /// </summary>
        /// <param name="id">Id của loại tài sản cần cập nhật</param>
        /// <param name="assetType">Thông tin mới của loại tài sản</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (12/07/2023)
        public async Task<bool> PutAsync(Guid id, AssetType assetType)
        {
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_AssetTypeId = id,
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
    }
}
