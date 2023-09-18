using Application.DTO;
using Application.Interface;
using AutoMapper;
using Domain.Model;
using MSIA.WebFresher052023.Application.Service.Base;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface;
using MSIA.WebFresher052023.Domain.Interface.Manager;
using MSIA.WebFresher052023.Domain.Interface.Repository;

namespace Application.Service
{
    public class ReceiverService : BaseService<Receiver, ReceiverModel, ReceiverDto, ReceiverCreateDto, ReceiverUpdateDto, ReceiverUpdateMultiDto>, IReceiverService
    {
        #region Fields
        private readonly IReceiverRepository _receiverRepository;
        private readonly IReceiverManager _receiverManager;
        #endregion

        #region Constructor
        public ReceiverService(IReceiverRepository receiverRepository, IMapper mapper, IReceiverManager receiverManager, IUnitOfWork unitOfWork) : base(receiverRepository, mapper, receiverManager, unitOfWork)
        {
            _receiverRepository = receiverRepository;
            _receiverManager = receiverManager;
        }
        #endregion

        #region Methods
        public override async Task<bool> InsertAsync(ReceiverCreateDto receiverCreateDto)
        {
            await _receiverManager.CheckDuplicateCodeAsync(receiverCreateDto.ReceiverCode);
            var entity = _mapper.Map<Receiver>(receiverCreateDto);
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            var id = Guid.NewGuid();
            entity.SetKey(id);
            bool result = await _receiverRepository.InsertAsync(entity);
            return result;
        } 

        public async Task<List<ReceiverDto>?> GetNewestReceiver()
        {
            var receivers = await _receiverRepository.GetNewestReceiver();
            if(receivers == null)
            {
                return null;
            }
            else
            {
                var receiverDtos = _mapper.Map<List<ReceiverDto>>(receivers);
                return receiverDtos;
            }
        }
        #endregion
    }
}
