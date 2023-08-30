using Application.DTO;
using Application.Interface;
using AutoMapper;
using Domain.Exceptions;
using Domain.Model;
using MSIA.WebFresher052023.Application.Service.Base;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Entity.Base;
using MSIA.WebFresher052023.Domain.Interface;
using MSIA.WebFresher052023.Domain.Interface.Manager;
using MSIA.WebFresher052023.Domain.Interface.Repository;

namespace Application.Service
{
    public class TransferAssetService : BaseService<TransferAsset, TransferAssetModel, TransferAssetDto, TransferAssetCreateDto, TransferAssetUpdateDto, TransferAssetUpdateMultiDto>, ITransferAssetService
    {
        #region Fields
        private readonly ITransferAssetRepository _transferAssetRepository;
        private readonly ITransferAssetManager _transferAssetManager;
        private readonly IReceiverRepository _receiverRepository;
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFixedAssetRepository _fixedAssetRepository;
        #endregion

        #region Constructor
        public TransferAssetService(ITransferAssetRepository transferAssetRepository, IReceiverRepository receiverRepository, ITransferAssetDetailRepository transferAssetDetailRepository, IDepartmentRepository departmentRepository, IFixedAssetRepository fixedAssetRepository, IMapper mapper, ITransferAssetManager transferAssetManager, IUnitOfWork unitOfWork) : base(transferAssetRepository, mapper, transferAssetManager, unitOfWork)
        {
            _transferAssetRepository = transferAssetRepository;
            _transferAssetManager = transferAssetManager;
            _receiverRepository = receiverRepository;
            _transferAssetDetailRepository = transferAssetDetailRepository;
            _departmentRepository = departmentRepository;
            _fixedAssetRepository = fixedAssetRepository;
        }
        #endregion

        #region Methods
        public override async Task<bool> InsertAsync(TransferAssetCreateDto transferAssetCreateDto)
        {
            // Check tài sản điều chuyển, chi tiết tài sản điều chuyển, bên giao nhận xem có null không
            if (transferAssetCreateDto.TransferAsset == null || transferAssetCreateDto.ListTransferAssetDetail == null)
            {
                throw new NotFoundException();
            }

            var transferAssetDto = transferAssetCreateDto.TransferAsset;
            await _transferAssetManager.CheckDuplicateCode(transferAssetDto.TransferAssetCode);
            // Nếu ngày điều chuyển nhỏ hơn ngày chứng từ thì sai
            if (transferAssetDto.TransferDate < transferAssetDto.TransactionDate)
            {
                throw new Exception();
            }

            var listTransferAssetDetailDtos = transferAssetCreateDto.ListTransferAssetDetail;

            // Kiểm tra xem các tài sản có bị trùng và có tồn tại trong db không
            var uniqueFixedAsset = listTransferAssetDetailDtos.Select(transfer => transfer.FixedAssetId).Distinct().ToList();
            var fixedAssetInDB = await _fixedAssetRepository.GetCountItemInListAsync(uniqueFixedAsset);
            if(uniqueFixedAsset.Count != listTransferAssetDetailDtos.Count || fixedAssetInDB != listTransferAssetDetailDtos.Count)
            {
                throw new DataException();
            }

            // Kiểm tra xem các phòng ban mới và cũ có tồn tại trong db hay không
            // Select many để gộp 2 tập hợp con old và new lại
            var uniqueDepartment = listTransferAssetDetailDtos.SelectMany(transfer => new[] { transfer.OldDepartmentId, transfer.NewDepartmentId }).Distinct().ToList();
            var departmentsInDB = await _departmentRepository.GetCountItemInListAsync(uniqueDepartment);
            if (uniqueDepartment.Count != departmentsInDB)
            {
                throw new DataException();
            }

            // Bắt đầu insert
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Thêm mới tài sản điều chuyển
                var transferAsset = _mapper.Map<TransferAsset>(transferAssetDto);
                transferAsset.CreatedDate = DateTime.Now;
                transferAsset.ModifiedDate = DateTime.Now;
                transferAsset.SetKey(Guid.NewGuid());

                await _transferAssetRepository.InsertAsync(transferAsset);

                // Thêm nhiều bên giao nhận
                var listReceiverDtos = transferAssetCreateDto.ListReceiver;
                if (listReceiverDtos != null)
                {
                    List<Receiver> receiversRaw = _mapper.Map<List<Receiver>>(listReceiverDtos);
                    List<Receiver> receivers = InitializeEntities(receiversRaw, transferAsset);

                    await _receiverRepository.InsertMultiAsync(receivers);
                }

                // Thêm chi tiết điều chuyển tài sản
                List<TransferAssetDetail> transferAssetDetailsRaw = _mapper.Map<List<TransferAssetDetail>>(listTransferAssetDetailDtos);
                List<TransferAssetDetail> transferAssetDetails = InitializeEntities(transferAssetDetailsRaw, transferAsset);

                await _transferAssetDetailRepository.InsertMultiAsync(transferAssetDetails);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }

        private static List<T> InitializeEntities<T>(List<T> entitiesRaw, TransferAsset? transferAsset) where T : BaseEntity, IHasKey
        {
            List<T> entities = new List<T>();

            foreach (var entity in entitiesRaw)
            {
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedDate = DateTime.Now;
                entity.SetKey(Guid.NewGuid());

                if (transferAsset != null)
                {
                    if (entity is Receiver receiver)
                    {
                        receiver.TransferAssetId = transferAsset.TransferAssetId;
                    }
                    else if (entity is TransferAssetDetail transferAssetDetail)
                    {
                        transferAssetDetail.TransferAssetId = transferAsset.TransferAssetId;
                    }
                }
                entities.Add(entity);
            }

            return entities;
        }

        #endregion
    }
}
