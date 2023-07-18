using Dapper;
using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Infrastructure
{
    public class AssetRepository : IAssetRepository
    {
        #region Fields
        private readonly DbConnection _connection;
        #endregion

        #region Constructor
        /// <summary>
        /// Khởi tạo một đối tượng AssetRepository mới.
        /// </summary>
        /// <param name="connectionString">Chuỗi kết nối đến cơ sở dữ liệu.</param>
        /// Created by: LB.Thành (16/07/2023)
        public AssetRepository(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả các tài sản từ cơ sở dữ liệu.
        /// </summary>
        /// <returns>Danh sách tài sản.</returns>
        /// Created by: LB.Thành (16/07/2023)
        public async Task<IEnumerable<Asset>> GetAllAsync()
        {
            var query = $"SELECT * FROM asset a \r\n       " +
                $"LEFT JOIN assettype a1 ON a.AssetTypeId = a1.AssetTypeId\r\n       " +
                $"LEFT JOIN department d ON a.DepartmentId = d.DepartmentId\r\n       " +
                $"ORDER BY a.CreatedDate;";

            var result = await _connection.QueryAsync<Asset>(query);
            return result;
        }

        /// <summary>
        /// Lấy thông tin một tài sản dựa trên ID.
        /// </summary>
        /// <param name="id">ID của tài sản cần lấy.</param>
        /// <returns>Thông tin tài sản.</returns>
        /// Created by: LB.Thành (16/07/2023)
        public async Task<Asset> GetAsync(Guid id)
        {
            var query = $"SELECT * FROM asset a WHERE a.AssetId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var result = await _connection.QueryFirstOrDefaultAsync<Asset>(query, parameters);
            if (result == null)
            {
                throw new NotFoundException("Không tìm thấy tài sản");
            }
            return (Asset)result;
        }

        /// <summary>
        /// Tìm kiếm một tài sản dựa trên ID.
        /// </summary>
        /// <param name="id">ID của tài sản cần tìm.</param>
        /// <returns>Thông tin tài sản hoặc null nếu không tìm thấy.</returns>
        /// Created by: LB.Thành (16/07/2023)
        public async Task<Asset?> FindAsync(Guid id)
        {
            var query = $"SELECT * FROM asset a WHERE a.AssetId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var result = await _connection.QueryFirstOrDefaultAsync<Asset>(query, parameters);
            return result;
        }

        /// <summary>
        /// Thêm một tài sản mới vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="entity">Thông tin tài sản cần thêm.</param>
        /// Created by: LB.Thành (16/07/2023)
        public async Task InsertAsync(Asset entity)
        {
            var query = $"INSERT INTO asset VALUES " +
                "(@id, @name, @quantity, @price, @depreciation, @yearOfUse, " +
                "NOW(), NOW(), NOW(), @createdBy, NOW(), @modifiedBy, @assetCode, NOW(), @assetTypeId, @departmentId);";

            var parameters = new DynamicParameters();
            parameters.Add("id", entity.AssetId);
            parameters.Add("name", entity.AssetName);
            parameters.Add("quantity", entity.AssetQuantity);
            parameters.Add("price", entity.AssetPrice);
            parameters.Add("depreciation", entity.AssetDepreciation);
            parameters.Add("yearOfUse", entity.YearOfUse);
            parameters.Add("createdBy", entity.CreatedBy);
            parameters.Add("modifiedBy", entity.ModifiedBy);
            parameters.Add("assetCode", entity.AssetCode);
            parameters.Add("assetTypeId", entity.AssetTypeId);
            parameters.Add("departmentId", entity.DepartmentId);

            await _connection.QueryAsync<Asset>(query, parameters);
        }

        /// <summary>
        /// Cập nhật thông tin một tài sản trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="entity">Thông tin tài sản cần cập nhật.</param>
        /// <returns>Task.</returns>
        /// Created by: LB.Thành (16/07/2023)
        public async Task UpdateAsync(Asset entity)
        {
            var query = $"UPDATE asset SET " +
                " AssetName = '@name', AssetQuantity = @quantity, AssetPrice = @price, AssetDepreciation = @depreciation," +
                "YearOfUse = @yearOfUse, PurchaseDate = NOW()," +
                "StartUsingDate = NOW(),CreatedDate = NOW(),CreatedBy = @createdBy," +
                "ModifiedDate = NOW(),ModifiedBy = @modifiedBy,AssetCode = @assetCode," +
                "YearOfTracking = NOW(),AssetTypeId = @assetTypeId, DepartmentId = @departmentId WHERE AssetId = @id; ";

            var parameters = new DynamicParameters();
            parameters.Add("id", entity.AssetId);
            parameters.Add("name", entity.AssetName);
            parameters.Add("quantity", entity.AssetQuantity);
            parameters.Add("price", entity.AssetPrice);
            parameters.Add("depreciation", entity.AssetDepreciation);
            parameters.Add("yearOfUse", entity.YearOfUse);
            parameters.Add("createdBy", entity.CreatedBy);
            parameters.Add("modifiedBy", entity.ModifiedBy);
            parameters.Add("assetCode", entity.AssetCode);
            parameters.Add("assetTypeId", entity.AssetTypeId);
            parameters.Add("departmentId", entity.DepartmentId);

            await _connection.QueryAsync<Asset>(query, parameters);
        }

        /// <summary>
        /// Xóa một tài sản khỏi cơ sở dữ liệu.
        /// </summary>
        /// <param name="entity">Tài sản cần xóa.</param>
        /// Created by: LB.Thành (16/07/2023)
        public async Task DeleteAsync(Asset entity)
        {
            var query = $"DELETE FROM asset a WHERE a.AssetId = @id";
            var param = new DynamicParameters();

            param.Add("id", entity.AssetId);
            await _connection.ExecuteAsync(query, param);
        }
        #endregion

    }
}
