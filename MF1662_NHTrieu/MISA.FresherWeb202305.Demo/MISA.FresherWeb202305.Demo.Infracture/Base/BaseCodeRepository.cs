using Dapper;
using MISA.FresherWeb202305.Demo.Domain.Entity;
using MISA.FresherWeb202305.Demo.Domain.Interface.Base;
using MISA.FresherWeb202305.Demo.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Infracture.Base
{
    public abstract class BaseCodeRepository<TEntity, TModel> : BaseRepository<TEntity, TModel>,IBaseCodeRepository<TEntity,TModel> where TEntity : IHasKey
    {
        protected BaseCodeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<TEntity> FindByCodeAsync(string entityCode)
        {
            var param = new DynamicParameters();
            param.Add($"@p_{TableName}Code", entityCode);

            var sql = $"{Procedure}GetByCode";

            var entity = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(sql, param, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return entity;
        }

        public async Task<string?> FindNewCodeAsync()
        {
            var sql = $"{Procedure}GetNewCode";

            var entity = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<string>(sql, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return entity;
        }
    }
}
