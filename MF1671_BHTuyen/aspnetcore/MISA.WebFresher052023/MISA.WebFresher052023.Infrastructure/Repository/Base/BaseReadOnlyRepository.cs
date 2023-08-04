using Dapper;
using MISA.WebFresher052023.Domain.Entity;
using MISA.WebFresher052023.Domain.Exceptions;
using MISA.WebFresher052023.Domain.Interface;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using MISA.WebFresher052023.Infrastructure.ConfigDapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Infrastructure.Repository.Base
{
    public abstract class BaseReadOnlyRepository<TEntity> : IBaseReadOnlyRepository<TEntity>
    {
        #region Field
        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        protected readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Tên bảng
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public virtual string TableName { get; protected set; } = nameof(TEntity);

        /// <summary>
        /// Tên bảng dạng số nhiều
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public virtual string TableNames { get; protected set; } = nameof(TEntity) + "s";

        /// <summary>
        /// ID bảng
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public virtual string TableNameId { get; protected set; } = nameof(TEntity) + "Id";
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        protected BaseReadOnlyRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// <exception cref="NotFoundException"></exception>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                var procname = $"Proc_Get{TableNames}";

                IEnumerable<TEntity>? entities = await _unitOfWork.Connection.QueryAsync<TEntity>(procname, commandType: System.Data.CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

                return entities ?? throw new NotFoundException("Không tìm thấy dữ liệu");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lấy ra bản ghi theo ID
        /// </summary>
        /// <param name="entityId">Id của bản ghi</param>
        /// <returns>Một bản ghi</returns>
        /// <exception cref="NotFoundException"></exception>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<TEntity> GetAsync(string entityId)
        {
            var entity = await FindAsync(entityId);

            return entity == null ? throw new NotFoundException("Không tìm thấy dữ liệu") : entity;
        }

        /// <summary>
        /// Tìm kiếm bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id của bản ghi</param>
        /// <returns>Kết quả tìm kiếm</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<TEntity?> FindAsync(string entityId)
        {
            try
            {
                var procname = $"Proc_Get{TableName}";

                var parameter = new DynamicParameters();

                parameter.Add($"{TableName}Id", entityId);

                var entity = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(procname, parameter, commandType: System.Data.CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

                return entity;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        ///  Tìm kiếm bản ghi theo mã Code
        /// </summary>
        /// <param name="entityCode">Mã code</param>
        /// <returns>Kết quả tìm kiếm</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<TEntity?> FindByCodeAsync(string entityCode)
        {
            try
            {
                var procname = $"Proc_Get{TableName}ByCode";

                var parameter = new DynamicParameters();

                parameter.Add($"{TableName}Code", entityCode);

                var entity = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(procname, parameter, commandType: System.Data.CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

                return entity;
            }
            catch (Exception) { throw; }
        }
        #endregion

    }
}
