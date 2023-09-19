using Dapper;
using MISA.WebFresher052023.Domain.Entity.Receiver;
using MISA.WebFresher052023.Domain.Interface.Receiver;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher052023.Infrastructure.Repository
{
    public class ReceiverRepository : IReceiverRepository
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public ReceiverRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy ban giao nhận của một chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetId">Mã Id của chứng từ điều chuyển</param>
        /// <returns>Danh sách ban giao nhận</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<IEnumerable<ReceiverEntity>> GetManyByTransferAssetIdAsync(Guid transferAssetId)
        {
            var query = $"SELECT * FROM receiver WHERE TransferAssetId = @TransferAssetId ORDER BY ReceiverOrder;";

            var parameters = new DynamicParameters();
            parameters.Add("TransferAssetId", transferAssetId.ToString());

            var receivers = await _unitOfWork.Connection.QueryAsync<ReceiverEntity>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return receivers;
        }

        /// <summary>
        /// Lấy bao giao nhận của nhiều chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetIds">Danh sách mã ID của các chứng từ</param>
        /// <returns>Danh sách ban giao nhận</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<IEnumerable<ReceiverEntity>> FindManyByTransferAssetIdsAsync(List<Guid> transferAssetIds)
        {
            var query = $"SELECT * FROM receiver WHERE TransferAssetId IN @TransferAssetIds;";

            var parameters = new DynamicParameters();
            parameters.Add("TransferAssetIds", transferAssetIds.Select(x => x.ToString()));

            var receivers = await _unitOfWork.Connection.QueryAsync<ReceiverEntity>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return receivers;
        }

        /// <summary>
        /// Lấy nhiều bản ghi theo mã Id
        /// </summary>
        /// <param name="receiverIds">Danh sách mã Id các bản ghi cần lấy</param>
        /// <returns>Danh sách các bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<IEnumerable<ReceiverEntity>> FindManyAsync(List<Guid> receiverIds)
        {
            var query = $"SELECT * FROM receiver WHERE ReceiverId IN @ReceiverIds;";
            var parameters = new DynamicParameters();
            parameters.Add("ReceiverIds", receiverIds.Select(x => x.ToString()));

            var receivers = await _unitOfWork.Connection.QueryAsync<ReceiverEntity>(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return receivers;
        }

        /// <summary>
        /// Tạo mới ban giao nhận của một chứng từ
        /// </summary>
        /// <param name="receivers">Danh sách người giao nhận</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task CreateManyAsync(IEnumerable<ReceiverEntity> receivers)
        {
            var value = "";

            var insert = "";

            foreach (PropertyInfo propertyInfo in typeof(ReceiverEntity).GetProperties())
            {
                if (value != "")
                {
                    value += ", ";
                    insert += ", ";
                }
                value += propertyInfo.Name;
                insert += $"@{propertyInfo.Name}";
            }

            var query = $"INSERT INTO receiver ( {value} ) VALUES ( {insert} );";

            await _unitOfWork.Connection.ExecuteAsync(query, receivers, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);
        }

        /// <summary>
        /// Cập nhật ban giao nhận của một chứng từ
        /// </summary>
        /// <param name="receivers">Danh sách những người cần cập nhật</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task UpdateManyAsync(IEnumerable<ReceiverEntity> receivers)
        {
            var where = "";
            var set = "";

            foreach (PropertyInfo propertyInfo in typeof(ReceiverEntity).GetProperties())
            {
                if (propertyInfo.Name != null)
                {
                    if (propertyInfo.Name == $"ReceiverId")
                    {
                        where = $"{propertyInfo.Name} = @{propertyInfo.Name}";
                    }
                    else
                    {
                        if (propertyInfo.Name != "CreatedBy" && propertyInfo.Name != "CreatedDate")
                        {
                            if (set != "")
                            {
                                set += ", ";
                            }
                            set += $"{propertyInfo.Name} = @{propertyInfo.Name}";
                        }

                    }
                }
            }
            var query = $"UPDATE receiver SET {set} WHERE {where};";
            await _unitOfWork.Connection.ExecuteAsync(query, receivers, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);
        }

        /// <summary>
        /// Xóa nhiều người trong một ban giao nhận của một chứng từ
        /// </summary>
        /// <param name="receivers">Danh sách người cần xóa</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task DeleteManyAsync(IEnumerable<ReceiverEntity> receivers)
        {
            var query = $"DELETE FROM receiver WHERE ReceiverId IN @ReceiverIds;";

            var parameters = new DynamicParameters();

            parameters.Add("ReceiverIds", receivers.Select(e => e.ReceiverId.ToString()));

            await _unitOfWork.Connection.ExecuteAsync(query, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);
        } 
        #endregion
    }
}
