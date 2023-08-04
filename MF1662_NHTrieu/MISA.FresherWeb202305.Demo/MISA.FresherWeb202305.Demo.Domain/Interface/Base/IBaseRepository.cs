using MISA.FresherWeb202305.Demo.Domain.Entity;
using MISA.FresherWeb202305.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Domain.Interface.Base
{
    public interface IBaseRepository<TEntity,TModel> : IReadonlyRepository<TEntity,TModel>
    {
        /// <summary>
        /// Thêm tài sản
        /// </summary>
        /// <param name="asset">Thông tin của tài sản</param>
        /// author : nhtrieu(15/07/2023)
        Task<int> CreateAsync(TEntity entity);
        /// <summary>
        /// Cập nhật tài sản
        /// </summary>
        /// <param name="asset">Thông tin tài sản</param>
        /// author: nhtrieu(15/07/2023)
        Task<int> UpdateAsync(TEntity entity);
        /// <summary>
        /// Xóa tài sản
        /// </summary>
        /// <param name="asset">Thông tin tài sản</param>
        /// author: nhtrieu(15/07/2023)
        Task<int> DeleteAsync(TEntity entity);
        /// <summary>
        /// Xóa nhiều tài sản
        /// </summary>
        /// <param name="assets">Thông tin tài sản</param>
        /// author: nhtrieu(15/07/2023)
        Task<int> DeleteAsyncMuliplute(List<Guid> ids);
    }
}
