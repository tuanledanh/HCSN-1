using Dapper;
using MISA.FresherWeb202305.Demo.Domain;
using MISA.FresherWeb202305.Demo.Domain.Entity;
using MISA.FresherWeb202305.Demo.Domain.Interface.Base;
using MISA.FresherWeb202305.Demo.Domain.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MISA.FresherWeb202305.Demo.Infracture.Base
{
    public abstract class BaseRepository<TEntity, TModel> : BaseReadOnlyRepository<TEntity, TModel>, IBaseRepository<TEntity, TModel> where TEntity : IHasKey
    {
        protected BaseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Thêm mới một bản ghi vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng bản ghi cần thêm mới</param>
        /// <returns>Số bản ghi được thêm mới</returns>
        /// CreatedBy: nhtrieu (15/07/2023)
        public async Task<int> CreateAsync(TEntity entity)
        {
            var param = MapEntitiesToParams(entity);
            var procname = $"{Procedure}Insert";
            var result = await _unitOfWork.Connection.ExecuteAsync(procname, param, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return result;
        }

        /// <summary>
        /// Xóa một bản ghi khỏi cơ sở dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị xóa</returns>
        /// CreatedBy: nhtrieu (15/07/2023)
        public async Task<int> DeleteAsync(TEntity entity)
        {
            var param = new DynamicParameters();
            param.Add($"@p_{TableId}", entity.GetKey());
            var sql = $"{Procedure}DeleteById";
            var result = await _unitOfWork.Connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return result;
        }

        /// <summary>
        /// Xóa nhiều bản ghi khỏi cơ sở dữ liệu
        /// </summary>
        /// <param name="ids">Danh sách khóa chính của các bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị xóa</returns>
        /// CreatedBy: nhtrieu (15/07/2023)
        public async Task<int> DeleteAsyncMuliplute(List<Guid> ids)
        {

            var sql = $"DELETE FROM fixed_{TableName} WHERE fixed_{TableName}_id IN @ids";
            var param = new DynamicParameters();
            param.Add("@ids", ids);
           
            var result=await _unitOfWork.Connection.ExecuteAsync(sql,param, transaction: _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// Cập nhật thông tin của một bản ghi trong cơ sở dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng bản ghi cần cập nhật</param>
        /// <returns>Số bản ghi được cập nhật</returns>
        /// CreatedBy: nhtrieu (15/07/2023)
        public async Task<int> UpdateAsync(TEntity entity)
        {
            var param = MapEntitiesToParams(entity);
            var sql = $"{Procedure}UpdateById";
            var result = await _unitOfWork.Connection.ExecuteAsync(sql, param, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return result;
        }

        /// <summary>
        /// Ánh xạ các thuộc tính của đối tượng bản ghi thành các tham số của câu lệnh SQL
        /// </summary>
        /// <param name="entity">Đối tượng bản ghi cần ánh xạ</param>
        /// <returns>Tham số của câu lệnh SQL</returns>
        /// CreatedBy: nhtrieu (15/07/2023)
        private DynamicParameters MapEntitiesToParams(TEntity entity)
        {
            var param = new DynamicParameters();

            var properties = typeof(TEntity).GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(entity);
                param.Add($"@p_{property.Name}", value);
            }

            return param;
        }
    }
}
