using Dapper;
using MISA.WebFresher052023.Domain.Entity.Department;
using MISA.WebFresher052023.Domain.Entity.FixedAsset;
using MISA.WebFresher052023.Domain.Interface.FixedAsset;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using MISA.WebFresher052023.Infrastructure.Repository.Base;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher052023.Infrastructure.Repository
{
    public class FixedAssetRepository : BaseRepository<FixedAssetEntity>, IFixedAssetRepository
    {
        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="unitOfWork">unitOfWork</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public FixedAssetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Ghi đè tên bảng
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023) 
        public override string TableName { get; protected set; } = "FixedAsset";

        /// <summary>
        /// Ghi đè tên bảng dạng số nhiều
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023) 
        public override string TableNames { get; protected set; } = "FixedAssets";

        /// <summary>
        /// Ghi đè tên khóa chính
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string TableNameId { get; protected set; } = "FixedAssetId";
        #endregion

        #region Methods

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="fixedAssetEntities">estateEntities</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task DeleteManyAsync(IEnumerable<FixedAssetEntity> fixedAssetEntities)
        {
            try
            {
                var sql = $"DELETE FROM fixed_asset WHERE {TableNameId} IN @fixedAssetEntityIds";

                var parameters = new DynamicParameters();

                parameters.Add("fixedAssetEntityIds", fixedAssetEntities.Select(x => x.GetKeyId().ToString()));
                await _unitOfWork.Connection.ExecuteAsync(sql, parameters, transaction: _unitOfWork.Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sinh mã tài sản
        /// </summary>
        /// <returns>Mã tài sản</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<string> GetFixedAssetCode()
        {
            try
            {
                var procname = $"Proc_Get{TableName}CodeNew";
                var fixedAssetCode = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<string>(procname, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
                return fixedAssetCode;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Tìm kiếm danh sách tài sản theo mã Id
        /// </summary>
        /// <param name="fixedAssetEntityIds"></param>
        /// <returns>Danh sách tài sản</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<IEnumerable<FixedAssetEntity>> FindManyByIdAsync(List<string> fixedAssetEntityIds)
        {
            try
            {
                var sql = $"SELECT * FROM fixed_asset AS fa WHERE {TableNameId} IN @fixedAssetEntityIds ORDER BY fa.ModifiedDate DESC";

                var parameters = new DynamicParameters();
                parameters.Add("fixedAssetEntityIds", fixedAssetEntityIds.Select(x=>x.ToString()));

                var fixedAssetEntities = await _unitOfWork.Connection.QueryAsync<FixedAssetEntity>(sql, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

                return fixedAssetEntities;
            }
            catch (Exception) { throw; }

        }

        /// <summary>
        /// Lấy ra số tài sản theo trang và bộ lọc
        /// </summary>
        /// <param name="fixedAssetFilterEntity"></param>
        /// <returns>Danh sách tài sản theo trang và tổng số bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public async Task<FixedAssetPagingEntity> GetFixedAssetPagingAsync(FixedAssetFilterEntity fixedAssetFilterEntity)
        {
            var procnameTotal = $"Proc_Get{TableName}TotalFilter";

            var parameters = new DynamicParameters();

            foreach (var property in typeof(FixedAssetFilterEntity).GetProperties())
            {
                parameters.Add(property.Name, property.GetValue(fixedAssetFilterEntity));
            }

            var totalRecord = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<int>(procnameTotal, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            var procnamePaging = $"Proc_Get{TableName}Paging";
           

            var fixedAssetEntitis = await _unitOfWork.Connection.QueryAsync<FixedAssetEntity>(procnamePaging, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            var fixedAssetPagingEntity = new FixedAssetPagingEntity
            {
                FixedAssetTotal = totalRecord,
                FixedAssetEntities = fixedAssetEntitis
            };
            return fixedAssetPagingEntity;
        }
        #endregion
    }
}
