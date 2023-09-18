using Dapper;
using MISA.WebFresher052023.Domain.Exceptions;
using MISA.WebFresher052023.Domain.Interface;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Infrastructure.Repository.Base
{
    public abstract class BaseReadOnlyRepository<TEntity> : IBaseReadOnlyRepository<TEntity>
    {
        #region Field
        /// <summary>
        /// UnitOfWork
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        protected readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Tên bảng trong DB
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public virtual string TableNameProc { get; protected set; } = nameof(TEntity);

        /// <summary>
        /// Tên bảng dạng số nhiều trong Proc
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public virtual string TableNamesProc { get; protected set; } = nameof(TEntity) + "s";

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

        #region Methods

        /// <summary>
        /// lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// <exception cref="NotFoundException">Ngoại lệ khi không tìm thấy dữ liệu</exception>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var procname = $"Proc_Get{TableNamesProc}";

            var entities = await _unitOfWork.Connection.QueryAsync<TEntity>(procname, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return entities ?? throw new NotFoundException("Không tìm thấy dữ liệu");
        }

        /// <summary>
        /// Lấy ra bản ghi theo ID
        /// </summary>
        /// <param name="entityId">Id của bản ghi</param>
        /// <returns>Một bản ghi</returns>
        /// <exception cref="NotFoundException">Ngoại lệ khi không tìm thấy bản ghi</exception>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<TEntity> GetAsync(Guid entityId)
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
        public async Task<TEntity> FindAsync(Guid entityId)
        {
            var procname = $"Proc_Get{TableNameProc}";

            var parameter = new DynamicParameters();
            parameter.Add($"{TableNameId}", entityId);

            var entity = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(procname, parameter, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return entity;
        }

        /// <summary>
        ///  Tìm kiếm bản ghi theo mã Code
        /// </summary>
        /// <param name="entityCode">Mã code</param>
        /// <returns>Kết quả tìm kiếm</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<TEntity> FindByCodeAsync(string entityCode)
        {
            var procname = $"Proc_Get{TableNameProc}ByCode";

            var parameter = new DynamicParameters();
            parameter.Add($"{TableNameProc}Code", entityCode);

            var entity = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(procname, parameter, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return entity;
        }
        #endregion

    }
}
