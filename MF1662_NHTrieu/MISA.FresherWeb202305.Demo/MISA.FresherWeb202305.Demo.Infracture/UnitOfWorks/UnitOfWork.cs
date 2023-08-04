using MISA.FresherWeb202305.Demo.Domain.UnitOfWork;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Infracture.UnitOfWorks
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbConnection _connection;
        private DbTransaction? _transaction = null;

        public UnitOfWork(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);

        }

        /// <summary>
        /// Kết nối cơ sở dữ liệu.
        /// </summary>
        public DbConnection Connection => _connection;

        /// <summary>
        /// Giao dịch cơ sở dữ liệu hiện tại.
        /// </summary>
        public DbTransaction? Transaction => _transaction;

        /// <summary>
        /// Bắt đầu một giao dịch mới.
        /// </summary>
        /// <remarks>
        /// CreatedBy: nhtrieu (15/07/2023)
        /// </remarks>
        public void BeginTransaction()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _transaction = _connection.BeginTransaction();
            }
            else
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
            }
        }

        /// <summary>
        /// Bắt đầu một giao dịch mới (bất đồng bộ).
        /// </summary>
        /// <remarks>
        /// CreatedBy: nhtrieu (15/07/2023)
        /// </remarks>
        public async Task BeginTransactionAsync()

        {
            if (_connection.State == ConnectionState.Open)
            {
                _transaction = await _connection.BeginTransactionAsync();
            }
            else
            {
                await _connection.OpenAsync();
                _transaction = await _connection.BeginTransactionAsync();
            }

        }

        /// <summary>
        /// Hoàn thành giao dịch và lưu các thay đổi vào cơ sở dữ liệu.
        /// </summary>
        /// <remarks>
        /// CreatedBy: nhtrieu (15/07/2023)
        /// </remarks>
        public void Commit()
        {
            _transaction?.Commit();
            Dispose();
        }

        /// <summary>
        /// Hoàn thành giao dịch và lưu các thay đổi vào cơ sở dữ liệu (bất đồng bộ).
        /// </summary>
        /// <remarks>
        /// CreatedBy: nhtrieu (15/07/2023)
        /// </remarks>
        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
            await DisposeAsync();
        }

        /// <summary>
        /// Giải phóng tài nguyên được sử dụng trong giao dịch.
        /// </summary>
        /// <remarks>
        /// CreatedBy: nhtrieu (15/07/2023)
        /// </remarks>
        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;
        }

        /// <summary>
        /// Giải phóng tài nguyên được sử dụng trong giao dịch (bất đồng bộ).
        /// </summary>
        /// <remarks>
        /// CreatedBy: nhtrieu (15/07/2023)
        /// </remarks>
        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }
            _transaction = null;
            await _connection.CloseAsync();
        }

        /// <summary>
        /// Huỷ bỏ giao dịch và không lưu các thay đổi vào cơ sở dữ liệu.
        /// </summary>
        /// <remarks>
        /// CreatedBy: nhtrieu (15/07/2023)
        /// </remarks>
        public void Rollback()
        {
            _transaction?.Rollback();
            Dispose();
        }

        /// <summary>
        /// Huỷ bỏ giao dịch và không lưu các thay đổi vào cơ sở dữ liệu.
        /// </summary>
        /// <remarks>
        /// CreatedBy: nhtrieu (15/07/2023)
        /// </remarks>
        public void RollBack()
        {
            _transaction?.Rollback();
            Dispose();
        }

        /// <summary>
        /// Huỷ bỏ giao dịch và không lưu các thay đổi vào cơ sở dữ liệu (bất đồng bộ).
        /// </summary>
        /// <remarks>
        /// CreatedBy: nhtrieu (15/07/2023)
        /// </remarks>
        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
            await DisposeAsync();
        }

        /// <summary>
        /// Huỷ bỏ giao dịch và không lưu các thay đổi vào cơ sở dữ liệu (bất đồng bộ).
        /// </summary>
        /// <remarks>
        /// CreatedBy: nhtrieu (15/07/2023)
        /// </remarks>
        public async Task RollBackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }

            await DisposeAsync();
        }
    }
}
