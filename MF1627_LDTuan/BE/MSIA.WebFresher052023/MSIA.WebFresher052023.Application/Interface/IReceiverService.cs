using Application.DTO;
using Domain.Model;
using MSIA.WebFresher052023.Application.Interface.Base;
using MSIA.WebFresher052023.Domain.Entity;

namespace Application.Interface
{
    public interface IReceiverService : IBaseService<Receiver, ReceiverModel, ReceiverDto, ReceiverCreateDto, ReceiverUpdateDto, ReceiverUpdateMultiDto>
    {
        /// <summary>
        /// Lấy ban giao nhận từ lần nhập trước
        /// </summary>
        /// <returns>Danh sách ban giao nhận mới nhất</returns>
        /// Created by: ldtuan (02/09/2023)
        Task<List<ReceiverDto>?> GetNewestReceiver();
    }
}
