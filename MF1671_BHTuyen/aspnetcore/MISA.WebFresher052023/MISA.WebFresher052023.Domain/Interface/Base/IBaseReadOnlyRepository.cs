using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface
{
    public interface IBaseReadOnlyRepository<TEntity>
    {
        #region Tasks
        /// <summary>
        /// Lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Lấy ra một bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id của bản ghi</param>
        /// <returns>Một bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        Task<TEntity> GetAsync(Guid entityId);

        /// <summary>
        /// Tìm kiếm một bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id của bản ghi</param>
        /// <returns>Một bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        Task<TEntity> FindAsync(Guid entityId);

        /// <summary>
        /// Lấy ra một bản ghi theo Code
        /// </summary>
        /// <param name="entityCode">Mã của bản ghi</param>
        /// <returns>Một bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        Task<TEntity> FindByCodeAsync(string entityCode);
        #endregion

    }
}
