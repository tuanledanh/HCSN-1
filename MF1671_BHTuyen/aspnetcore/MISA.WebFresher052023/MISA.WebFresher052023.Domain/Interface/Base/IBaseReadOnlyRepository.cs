using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface
{
    public interface IBaseReadOnlyRepository<TEntity>
    {
        #region Methods
        /// <summary>
        /// Lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Lấy ra một bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Một bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// Tìm kiếm một bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Một bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public Task<TEntity> FindAsync(Guid id);

        /// <summary>
        /// Lấy ra một bản ghi theo Code
        /// </summary>
        /// <param name="code">Mã của bản ghi</param>
        /// <returns>Một bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public Task<TEntity> FindByCodeAsync(string code);
        #endregion

    }
}
