using qlts_api.DTO.Asset;
using Dapper;
using Microsoft.AspNetCore.Components.Forms;
using qlts_api.Context;
using qlts_api.DTO.Asset;
using System.Data;

namespace qlts_api.Repository
{
    public class AssetRepo : IAssetRepo
    {
        private readonly DapperConnect db;
        public AssetRepo(DapperConnect db)
        {
            this.db = db;
        }
        /// <summary>
        /// lấy toàn bộ tài sản từ db
        /// param: các property trong AssetRequest
        /// Created by: LB.Thành (13/07/2023)
        /// </summary>
        public async Task<IEnumerable<Asset>> GetAllAsync()
        {
            var procname = "GetAssets";
            using(var connection = db.CreateConnection())
            {
                connection.Open();
                var assets = await connection.QueryAsync<Asset>(procname, commandType: CommandType.StoredProcedure);
                connection.Close();
                return assets;
            }
        }
        public Task<Asset> CreateAsync(AssetRequest request)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Asset> Get(Guid id)
        {
            throw new NotImplementedException();
        }


        public Task Update(Guid id, AssetRequest value)
        {
            throw new NotImplementedException();
        }
    }
}
