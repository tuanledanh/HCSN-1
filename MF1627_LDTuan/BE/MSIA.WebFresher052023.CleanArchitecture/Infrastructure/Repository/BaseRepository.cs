using Dapper;
using Domain.Exceptions;
using Domain.Interface;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace Infrastructure.Repository
{
    public class BaseRepository<TEntity, TModel> : IBaseRepository<TEntity, TModel>
    {
        #region Fields
        protected readonly string _connectionString;
        #endregion

        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"] ?? "";
        }
        #endregion

        #region Methods
        /// <summary>
        /// Mở kết nối đến database
        /// </summary>
        /// Created by: ldtuan (17/07/2023)
        public async Task<DbConnection> GetOpenConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        /// <summary>
        /// Lấy thông tin của bản ghi dựa vào id
        /// </summary>
        /// <param name="Id">Id của bản ghi</param>
        /// <returns>Một bản ghi</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            var tableName = typeof(TEntity).Name;
            string procedureName = "Proc_Get" + tableName + "ById";
            var paramName = "p_Id";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, id);
            var connection = await GetOpenConnectionAsync();
            var entity = await connection.QueryFirstOrDefaultAsync<TEntity>(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);
            if (entity == null)
            {
                throw new NotFoundException("Repository: Can not found " + typeof(TEntity).Name + " by this id");
            }
            await connection.CloseAsync();
            return entity;
        }

        /// <summary>
        /// Lấy thông tin của bản ghi dựa vào mã
        /// </summary>
        /// <param name="Code">Mã của bản ghi</param>
        /// <returns>Một bản ghi</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<TModel?> FindAsync(string code)
        {
            var tableName = typeof(TEntity).Name;
            string procedureName = "Proc_Get" + tableName + "ByCode";
            var paramName = "p_Code";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, code);
            var connection = await GetOpenConnectionAsync();
            var entity = await connection.QueryFirstOrDefaultAsync<TModel>(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);
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
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<List<TModel>> GetFilterAsync(int? pageNumber, int? pageLimit, string? filterName)
        {
            var tableName = typeof(TEntity).Name;
            string procedureName = "Proc_Filter" + tableName;
            var parameters = new
            {
                p_PageNumber = pageNumber,
                p_PageLimit = pageLimit,
                p_FilterName = filterName
            };
            var connection = await GetOpenConnectionAsync();
            var entities = await connection.QueryAsync<TModel>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return entities.ToList();
        }

        /// <summary>
        /// Lấy danh sách toàn bộ bản ghi
        /// </summary>
        /// <returns>Toàn bộ bản ghi</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<List<TModel>> GetAllAsync()
        {
            var tableName = typeof(TEntity).Name;
            string procedureName = "Proc_GetList" + tableName;
            var connection = await GetOpenConnectionAsync();
            var entities = await connection.QueryAsync<TModel>(procedureName, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return entities.ToList();
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Thông tin của bản ghi</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<bool> InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cập nhật thông tin của bản ghi
        /// </summary>
        /// <param name="entity">Thông tin mới của bản ghi</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Xóa thông tin một bản ghi thông qua mã code
        /// </summary>
        /// <param name="Entity">Bản ghi cần xóa</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
