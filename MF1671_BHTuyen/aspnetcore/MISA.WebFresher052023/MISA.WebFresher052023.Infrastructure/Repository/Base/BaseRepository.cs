using Dapper;
using MISA.WebFresher052023.Domain.Interface;
using MISA.WebFresher052023.Domain.Interface.Base;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

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
        /// Entity cần tạo parameters
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Môt DynamicParameters từ Entity truyền vào</returns>
        public DynamicParameters GetDynamicParameters(TEntity entity)
        {
            var type = typeof(TEntity);

            var properties = type.GetProperties();

            var parameters = new DynamicParameters();

            foreach (var property in properties)
            {
                parameters.Add(property.Name, property.GetValue(entity));
            }

            return parameters;
        }

        /// <summary>
        /// Tạo mới một bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task CreateAsync(TEntity entity)
        {
            var procname = $"Proc_Create{TableNameProc}";
            var parameters = GetDynamicParameters(entity);

            await _unitOfWork.Connection.ExecuteAsync(procname, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
        }

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task UpdateAsync(TEntity entity)
        {
            var procname = $"Proc_Update{TableNameProc}";

            var parameters = GetDynamicParameters(entity);

            await _unitOfWork.Connection.ExecuteAsync(procname, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
        }

        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        /// <param name="entity">Một bản ghi</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task DeleteAsync(TEntity entity)
        {
            var procname = $"Proc_Delete{TableNameProc}";

            var parameters = new DynamicParameters();

            parameters.Add($"{TableNameId}", entity.GetKey());

            await _unitOfWork.Connection.ExecuteAsync(procname, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
        }
        #endregion
    }
}
