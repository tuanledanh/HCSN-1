using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface
{
    public interface IBaseManager<TEntity>
    {
        #region Tasks
        /// <summary>
        /// Kiểm tra mã bản ghi đã tồn tại hay không để thêm mới
        /// </summary>
        /// <param name="entityCode">Mã của bản ghi</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        Task CheckExistByCode(string entityCode);
        #endregion
    }
}
