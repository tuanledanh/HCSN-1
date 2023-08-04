using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Domain.UnitOfWork
{
    /// <summary>
    /// Giao diện đại diện cho một đối tượng có thể làm việc với kết nối cơ sở dữ liệu và giao dịch.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Đối tượng kết nối cơ sở dữ liệu.
        /// </summary>
        DbConnection Connection { get; }

        /// <summary>
        /// Đối tượng giao dịch cơ sở dữ liệu (nếu có).
        /// </summary>
        DbTransaction? Transaction { get; }

        /// <summary>
        /// Bắt đầu một giao dịch mới.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Bắt đầu một giao dịch mới (asynchronous).
        /// </summary>
        Task BeginTransactionAsync();

        /// <summary>
        /// Lưu các thay đổi và kết thúc giao dịch hiện tại.
        /// </summary>
        void Commit();

        /// <summary>
        /// Lưu các thay đổi và kết thúc giao dịch hiện tại (asynchronous).
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// Hủy bỏ giao dịch hiện tại.
        /// </summary>
        void RollBack();

        /// <summary>
        /// Hủy bỏ giao dịch hiện tại (asynchronous).
        /// </summary>
        Task RollBackAsync();
    }
}
