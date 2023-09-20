using Domain.Entity;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface.Repository.Base;

namespace MSIA.WebFresher052023.Domain.Interface.Repository
{
    public interface IReceiverRepository : IBaseRepository<Receiver, ReceiverModel>
    {
        #region Methods
        /// <summary>
        /// Lấy danh sách ban giao nhận mới nhất
        /// </summary>
        /// <returns>Danh sách ban giao nhận</returns>
        /// Created by: ldtuan (09/09/2023)
        Task<List<Receiver>> GetNewestReceiver(); 
        #endregion
    }
}
