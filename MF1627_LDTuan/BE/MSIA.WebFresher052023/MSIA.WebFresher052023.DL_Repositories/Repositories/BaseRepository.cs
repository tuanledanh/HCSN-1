using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.DL_Repositories.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected readonly string _connectionString;
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"] ?? "";
        }

        /// <summary>
        /// Mở kết nối đến database
        /// </summary>
        /// Created by: ldtuan (12/07/2023)
        public async Task<DbConnection> GetOpenConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        /// <summary>
        /// Lấy thông tin của bản ghi dựa vào mã code
        /// </summary>
        /// <param name="code">Mã code của bản ghi</param>
        /// <returns>Một bản ghi</returns>
        /// Created by: ldtuan (12/07/2023)
        public virtual async Task<TEntity?> GetAsync(string code)
        {
            var tableName = typeof(TEntity).Name;
            string procedureName = "Proc_Get" + tableName + "ByCode";
            var connection = await GetOpenConnectionAsync();
            var paramName = "p_Code";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, code);
            var entity = await connection.QueryFirstOrDefaultAsync<TEntity>(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return entity;
        }

        /// <summary>
        /// Lấy danh cách bản ghi có phân trang và tìm kiếm
        /// </summary>
        /// <param name="pageNumber">Sô trang</param>
        /// <param name="pageLimit">Giới hạn bản ghi ở mỗi trang</param>
        /// <param name="filterName">Từ khóa để tìm kiếm</param>
        /// <returns>Danh sách bản ghi sau khi phân trang và tìm kiếm</returns>
        /// Created by: ldtuan (12/07/2023)
        public virtual async Task<List<TEntity>> GetFilterAsync(int? pageNumber, int? pageLimit, string? filterName)
        {
            var tableName = typeof(TEntity).Name;
            string procedureName = "Proc_Filter" + tableName;
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_PageNumber = pageNumber,
                p_PageLimit = pageLimit,
                p_FilterName = filterName
            };
            var entities = await connection.QueryAsync<TEntity>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return entities.ToList();
        }

        /// <summary>
        /// Lấy danh sách toàn bộ bản ghi
        /// </summary>
        /// <returns>Toàn bộ bản ghi</returns>
        /// Created by: ldtuan (12/07/2023)
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            var tableName = typeof(TEntity).Name;
            string procedureName = "Proc_GetList" + tableName;
            var connection = await GetOpenConnectionAsync();
            var entities = await connection.QueryAsync<TEntity>(procedureName, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return entities.ToList();
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Thông tin của bản ghi</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (12/07/2023)
        public virtual async Task<bool> PostAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cập nhật thông tin của bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi cần cập nhật</param>
        /// <param name="entity">Thông tin mới của bản ghi</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (12/07/2023)
        public virtual async Task<bool> PutAsync(Guid id, TEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Xóa thông tin một bản ghi thông qua mã code
        /// </summary>
        /// <param name="code">Mã code của bản ghi cần xóa</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (12/07/2023)
        public virtual async Task<bool> DeleteAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}
