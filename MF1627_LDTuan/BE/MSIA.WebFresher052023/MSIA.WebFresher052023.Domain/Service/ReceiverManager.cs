using Domain.Entity;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface.Manager;
using MSIA.WebFresher052023.Domain.Interface.Repository;

namespace MSIA.WebFresher052023.Domain.Service
{
    public class ReceiverManager : BaseManager<Receiver, ReceiverModel>, IReceiverManager
    {
        public ReceiverManager(IReceiverRepository receiverRepository) : base(receiverRepository)
        {
        }
    }
}
