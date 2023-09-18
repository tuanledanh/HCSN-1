using AutoMapper;
using MISA.WebFresher052023.Application.Dto.Receiver;
using MISA.WebFresher052023.Application.Interface;
using MISA.WebFresher052023.Domain.Entity;
using MISA.WebFresher052023.Domain.Entity.Receiver;
using MISA.WebFresher052023.Domain.Exceptions;
using MISA.WebFresher052023.Domain.Interface.Receiver;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using MISA.WebFresher052023.Domain.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Service
{
    public class ReceiverService : IReceiverService
    {
        private readonly IReceiverRepository _receiverRepository;

        private readonly IMapper _mapper;


        public ReceiverService(IReceiverRepository receiverRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _receiverRepository = receiverRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReceiverDto>> GetReceiverAsync(Guid transferAssetId)
        {
            var receiverEntities = await _receiverRepository.GetManyByTransferAssetIdAsync(transferAssetId);

            var receiverDtos = _mapper.Map<IEnumerable<ReceiverDto>>(receiverEntities);

            return receiverDtos;
        }

        public async Task CreateManyAsync(IEnumerable<ReceiverDto> receivers, Guid TransferAssetId)
        {

            if (!receivers.Any())
            {
                throw new UserException(VietNamese.EmptyList);
            }

            var receiverEntities = _mapper.Map<IEnumerable<ReceiverEntity>>(receivers);

            foreach (var receiver in receiverEntities)
            {
                receiver.SetKey(Guid.NewGuid());
                receiver.SetTransferAssetId(TransferAssetId);
                if (receiver is BaseAuditEntity baseAuditEntity)
                {
                    baseAuditEntity.CreatedDate = DateTime.Now;
                    baseAuditEntity.ModifiedDate = DateTime.Now;
                    baseAuditEntity.CreatedBy = VietNamese.Admin;
                    baseAuditEntity.ModifiedBy = VietNamese.Admin;
                }
            }

            await _receiverRepository.CreateManyAsync(receiverEntities);

        }

        public async Task UpdateManyAsync(IEnumerable<ReceiverDto> receivers)
        {

            if (!receivers.Any())
            {
                throw new UserException(VietNamese.EmptyList);
            }

            var receiverEntities = _mapper.Map<IEnumerable<ReceiverEntity>>(receivers);

            foreach (var receiver in receiverEntities)
            {

                if (receiver is BaseAuditEntity baseAuditEntity)
                {
                    baseAuditEntity.ModifiedDate = DateTime.Now;
                    baseAuditEntity.ModifiedBy = VietNamese.Admin;
                }
            }

            await _receiverRepository.UpdateManyAsync(receiverEntities);
        }

        public async Task DeleteManyAsync(List<Guid> receiverIds)
        {
            if (receiverIds.Count == 0)
            {
                throw new UserException(VietNamese.EmptyList);
            }

            var receiverEntities = await _receiverRepository.FindManyAsync(receiverIds);

            if (receiverEntities.Count() != receiverIds.Count)
            {
                throw new UserException(VietNamese.NoDelete);
            }

            await _receiverRepository.DeleteManyAsync(receiverEntities);
        }

        public async Task<IEnumerable<ReceiverEntity>> FindManyByTransferAssetIdsAsync(List<Guid> transferAssetIds)
        {
            var receiverEntities = await _receiverRepository.FindManyByTransferAssetIdsAsync(transferAssetIds);

            return receiverEntities;
        }
    }
}
