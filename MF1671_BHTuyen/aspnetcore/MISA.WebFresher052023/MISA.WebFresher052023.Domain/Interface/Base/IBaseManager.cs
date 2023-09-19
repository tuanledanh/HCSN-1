using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Interface
{
    public interface IBaseManager<TEntity>
    {
        #region Methods
        /// <summary>
        /// Kiểm tra mã code của bản ghi đã tồn tại hay không để thêm mới hoặc cập nhật
        /// </summary>
        /// <param name="code">Mã code của bản ghi</param>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public Task CheckCodeConflictAsync(string code);
        #endregion
    }
}
