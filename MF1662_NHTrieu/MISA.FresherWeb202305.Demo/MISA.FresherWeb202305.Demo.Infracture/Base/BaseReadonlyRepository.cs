using Dapper;
using MISA.FresherWeb202305.Demo.Domain.Enums;
using MISA.FresherWeb202305.Demo.Domain.UnitOfWork;
using MISA.FresherWeb202305.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.FresherWeb202305.Demo.Domain.Interface.Base;
using System.Data.Common;

namespace MISA.FresherWeb202305.Demo.Infracture.Base
{
    public abstract class BaseReadOnlyRepository<TEntity, TModel> : IReadonlyRepository<TEntity, TModel>
    {
        protected readonly IUnitOfWork _unitOfWork;
        public virtual string TableName { get; protected set; } =  typeof(TEntity).Name ; // tên bảng
        public virtual string TableId { get; protected set; } = $"{typeof(TEntity).Name}Id";
        public virtual string Procedure { get; protected set; } = $"Proc_{typeof(TEntity).Name}_";

        protected BaseReadOnlyRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Tìm kiếm bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id của bản ghi cần tìm</param>
        /// <returns>Bản ghi có Id tương ứng nếu tìm thấy, ngược lại trả về null</returns>
        /// CreatedBy: nhtrieu (15/07/2023)
        public async Task<TEntity?> FindByIdAsync(Guid entityId)
        {
            var param = new DynamicParameters();
            param.Add($"p_{TableId}", entityId);
            var procedureName = $"{Procedure}GetById";
            var entity = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(procedureName, param, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return entity;
        }

        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// CreatedBy: nhtrieu (15/07/2023)
        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            var procedureName = $"{Procedure}GetAll";
            var entitiesModel = await _unitOfWork.Connection.QueryAsync<TModel>(procedureName, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return entitiesModel;
        }

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id của bản ghi cần lấy</param>
        /// <returns>Bản ghi có Id tương ứng nếu tìm thấy</returns>
        /// <exception cref="NotFoundException">Nếu không tìm thấy bản ghi với Id tương ứng</exception>
        /// CreatedBy: nhtrieu (15/07/2023)
        public async Task<TEntity> GetByIdAsync(Guid entityId)
        {
            var entity = await FindByIdAsync(entityId);
            if (entity == null)
            {
                    throw new NotFoundException($"Không tìm thấy {entityId}");
            }
            return entity;
        }
    }
}
