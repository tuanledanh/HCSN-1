using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Interface.Base
{
    public interface IBaseReadonlyService<TEntityDto>
    {
        /// <summary>
        /// Lấy tất cả đối tượng
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        /// CreatedBy: nhtrieu (18/07/2023)
        Task<IEnumerable<TEntityDto>> GetAllAsync();

        /// <summary>
        /// Lấy đối tượng theo Id
        /// </summary>
        /// <param name="entityId">Id của đối tượng</param>
        /// <returns>Đối tượng</returns>
        /// CreatedBy: nhtrieu (18/07/2023)
        Task<TEntityDto> GetByIdAsync(Guid entityId);
    }
}
