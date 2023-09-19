using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface
{
    public interface IBaseRepository<TEntity> : IBaseReadOnlyRepository<TEntity>
    {
        #region Methods
        /// <summary>
        /// Tạo mới một bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu bản ghi</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public Task CreateAsync(TEntity entity);

        /// <summary>
        /// Cập nhật một bản ghi 
        /// </summary>
        /// <param name="entity">Dữ liệu bản ghi</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Xóa một bản ghi 
        /// </summary>
        /// <param name="entity">Dữ liệu bản ghi</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public Task DeleteAsync(TEntity entity);
        #endregion
    }
}
