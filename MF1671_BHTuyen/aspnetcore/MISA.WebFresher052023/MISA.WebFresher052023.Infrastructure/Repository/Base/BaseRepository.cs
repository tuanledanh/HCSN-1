using Dapper;
using MISA.WebFresher052023.Domain.Entity;
using MISA.WebFresher052023.Domain.Interface;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using MISA.WebFresher052023.Infrastructure.ConfigDapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Infrastructure.Repository.Base
{
    public abstract class BaseRepository<TEntity> : BaseReadOnlyRepository<TEntity>, IBaseRepository<TEntity> where TEntity : IHasKeyEntity
    {
        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        protected BaseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tạo mới một bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task CreateAsync(TEntity entity)
        {
            try
            {
                var procname = $"Proc_Create{TableName}";

                var type = typeof(TEntity);

                var properties = type.GetProperties();

                var parameters = new DynamicParameters();

                foreach (var property in properties)
                {
                    parameters.Add(property.Name, property.GetValue(entity));
                }

                await _unitOfWork.Connection.ExecuteAsync(procname, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task UpdateAsync(TEntity entity)
        {
            try
            {
                var procname = $"Proc_Update{TableName}";

                var type = typeof(TEntity);

                var properties = type.GetProperties();

                var parameters = new DynamicParameters();

                foreach (var property in properties)
                {
                    parameters.Add(property.Name, property.GetValue(entity));
                }

                await _unitOfWork.Connection.ExecuteAsync(procname, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="entity">Một bản ghi</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task DeleteAsync(TEntity entity)
        {
            try
            {
                var procname = $"Proc_Delete{TableName}";

                var parameters= new DynamicParameters();

                parameters.Add($"{TableName}Id", entity.GetKeyId());

                await _unitOfWork.Connection.ExecuteAsync(procname, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            }
            catch (Exception) { throw; }
        }

        #endregion

    }
}
