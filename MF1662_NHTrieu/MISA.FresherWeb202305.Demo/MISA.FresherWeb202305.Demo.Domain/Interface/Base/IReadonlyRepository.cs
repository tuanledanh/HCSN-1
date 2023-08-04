using MISA.FresherWeb202305.Demo.Domain.Entity;
using MISA.FresherWeb202305.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Domain.Interface.Base
{
    public interface IReadonlyRepository<TEntity,TModel>
    {
        /// <summary>
        /// Lấy ra danh sách tài sản
        /// </summary>
        /// <returns>Danh sách tài sản</returns>
        /// author:nhtrieu(15/07/2023)
        Task<IEnumerable<TModel>> GetAllAsync();
        /// <summary>
        /// Lấy ra tài sản theo mã đinh danh
        /// </summary>
        /// <param name="id">Mã định dạng của tài sản</param>
        /// <returns>Tài sản của 1 người</returns>
        /// author: nhtrieu(15/07/2023)
        /// 
        Task<TEntity> GetByIdAsync(Guid entityId);
        /// <summary>
        /// Tìm kiếm đối object theo id
        /// </summary>
        /// <param name="entityId">Trả về đối tượng tìm thấy(nếu k tìm thấy trả về null)</param>
        /// <returns></returns>
        /// author:nhtrieu(18/07/2023)
        Task<TEntity?> FindByIdAsync(Guid entityId);
       
    }
}
