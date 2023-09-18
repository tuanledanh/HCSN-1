using AutoMapper;
using MISA.WebFresher052023.Application.Dto.TransferAsset;
using MISA.WebFresher052023.Application.Interface;
using MISA.WebFresher052023.Domain.Entity;
using MISA.WebFresher052023.Domain.Entity.TransferAsset;
using MISA.WebFresher052023.Domain.Enum;
using MISA.WebFresher052023.Domain.Exceptions;
using MISA.WebFresher052023.Domain.Interface.TransferAsset;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using MISA.WebFresher052023.Domain.Model.TransferAsset;
using MISA.WebFresher052023.Domain.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Service
{
    public class TransferAssetService : ITransferAssetService
    {
        #region Field
        private readonly ITransferAssetRepository _transferAssetRepository;

        private readonly ITransferAssetManager _transferAssetManager;

        private readonly IReceiverService _receiverService;

        private readonly ITransferAssetDetailService _transferAssetDetailService;

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="transferAssetRepository"></param>
        /// <param name="transferAssetManager"></param>
        /// <param name="mapper"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="transferAssetDetailService"></param>
        /// <param name="receiverService"></param>
        public TransferAssetService(ITransferAssetRepository transferAssetRepository, ITransferAssetManager transferAssetManager, IMapper mapper, IUnitOfWork unitOfWork, ITransferAssetDetailService transferAssetDetailService, IReceiverService receiverService)
        {
            _transferAssetRepository = transferAssetRepository;
            _transferAssetManager = transferAssetManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _transferAssetDetailService = transferAssetDetailService;
            _receiverService = receiverService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy mã code mới gợi ý khi thêm chứng từ
        /// </summary>
        /// <returns>Mã code mới</returns>
        /// Created By: Bùi Huy Tuyền
        public async Task<string> GetTransferAssetCodeNewAsync()
        {
            var transferAssetCodeNew = await _transferAssetRepository.GetTransferAssetCodeNewAsync();

            return transferAssetCodeNew;
        }

        /// <summary>
        /// Phân trang chứng từ
        /// </summary>
        /// <param name="transferAssetFilterDto">Các trường lọc</param>
        /// <returns>Danh sách chứng từ</returns>
        /// Created By: Bùi Huy Tuyền
        public async Task<TransferAssetPagingDto> GetTransferAssetPagingAsync(TransferAssetFilterDto transferAssetFilterDto)
        {
            var transferAssetFilterModel = _mapper.Map<TransferAssetFilterModel>(transferAssetFilterDto);

            var transferAssetPagingModel = await _transferAssetRepository.GetTransferAssetPagingAsync(transferAssetFilterModel);

            var transferAssetPagingDto = _mapper.Map<TransferAssetPagingDto>(transferAssetPagingModel);

            return transferAssetPagingDto;

        }

        /// <summary>
        /// Tạo mới một chứng từ
        /// </summary>
        /// <param name="transferAssetDto">Chứng từ tạo mới</param>
        /// <param name="TransferAssetId">Mã Id của chứng từ</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền
        public async Task CreateAsync(TransferAssetDto transferAssetDto, Guid TransferAssetId)
        {
            _transferAssetManager.CheckTransferDateLaterTransTransactionDateAsync(transferAssetDto.TransferDate, transferAssetDto.TransactionDate);
            // Check trùng mã code
            await _transferAssetManager.CheckCodeConflictAsync(transferAssetDto.TransferAssetCode);

            var transferAssetEntity = _mapper.Map<TransferAssetEntity>(transferAssetDto);

            transferAssetEntity.SetKey(TransferAssetId);

            if (transferAssetEntity is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.CreatedDate = DateTime.Now;
                baseAuditEntity.CreatedBy = VietNamese.Admin;
                baseAuditEntity.ModifiedDate = DateTime.Now;
                baseAuditEntity.ModifiedBy = VietNamese.Admin;
            }

            await _transferAssetRepository.CreateAsync(transferAssetEntity);
        }

        /// <summary>
        /// Hàm tạo mới một chứng từ với Danh sách tài sản và ban giao nhận
        /// </summary>
        /// <param name="transferAssetCreateFull">Chứng từ cần tạo mới</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (10/09/2023)
        public async Task CreateTransferAssetFullAsync(TransferAssetCreateFull transferAssetCreateFull)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Tạo mới một Mã Id của chứng từ
                var transferAssetId = Guid.NewGuid();

                var transferAssetCreateDto = transferAssetCreateFull.TransferAsset;

                await CreateAsync(transferAssetCreateDto, transferAssetId);

                // Danh sách tài sản điều chuyển
                var transferAssetDetailCreateDtos = transferAssetCreateFull.TransferAssetDetails;


                if (transferAssetDetailCreateDtos.Any())
                {
                    await _transferAssetDetailService.CreateManyAsync(transferAssetDetailCreateDtos, transferAssetId);
                }
                else
                {
                    // Nếu danh sách rỗng thì gửi thông báo lỗi cho client
                    throw new UserException(VietNamese.TransferAssetDetailNotAdd);
                }

                // Danh sách ban giao nhận (Có thể để trống)
                var receiverCreateDtos = transferAssetCreateFull.Receivers;

                if (receiverCreateDtos != null && receiverCreateDtos.Any())
                {
                    await _receiverService.CreateManyAsync(receiverCreateDtos, transferAssetId);
                }

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateAsync(TransferAssetDto transferAssetDto)
        {
            _transferAssetManager.CheckTransferDateLaterTransTransactionDateAsync(transferAssetDto.TransferDate, transferAssetDto.TransactionDate);

            var transferAssetEntity = await _transferAssetRepository.GetAsync(transferAssetDto.TransferAssetId);

            if (transferAssetEntity.GetCode() != transferAssetDto.TransferAssetCode)
            {
                await _transferAssetManager.CheckCodeConflictAsync(transferAssetDto.TransferAssetCode);
            }

            var transferAssetUpdateEntity = _mapper.Map<TransferAssetEntity>(transferAssetDto);

            if (transferAssetUpdateEntity is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.ModifiedBy = VietNamese.Admin;
                baseAuditEntity.ModifiedDate = DateTime.Now;
            }

            await _transferAssetRepository.UpdateAsync(transferAssetUpdateEntity);
        }

        public async Task UpdateTransferAssetFullAsync(TransferAssetUpdateFull transferAssetUpdateFull)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var transferAssetUpdateDto = transferAssetUpdateFull.TransferAsset;

                await UpdateAsync(transferAssetUpdateDto);

                // Thêm sửa xóa nhiều tài sản điều chuyển
                var transferAssetDetailFlags = transferAssetUpdateFull.TransferAssetDetails;

                if (transferAssetDetailFlags != null)
                {
                    var transferAssetDetailCreateDtos = transferAssetDetailFlags.Where(transferAssetDetailFlag => transferAssetDetailFlag.ActionMode == ActionMode.CREATE).Select(transferAssetDetailFlag => transferAssetDetailFlag.TransferAssetDetail);

                    var transferAssetDetailUpdateDtos = transferAssetDetailFlags.Where(transferAssetDetailFlag => transferAssetDetailFlag.ActionMode == ActionMode.UPDATE).Select(transferAssetDetailFlag => transferAssetDetailFlag.TransferAssetDetail);

                    var transferAssetDetailDeleteIds = transferAssetDetailFlags.Where(transferAssetDetailFlag => transferAssetDetailFlag.ActionMode == ActionMode.DELETE).Select(transferAssetDetailFlag => transferAssetDetailFlag.TransferAssetDetail.TransferAssetDetailId).ToList();

                    if (transferAssetDetailCreateDtos.Any())
                    {
                        await _transferAssetDetailService.CreateManyAsync(transferAssetDetailCreateDtos, transferAssetUpdateDto.TransferAssetId);
                    }

                    if (transferAssetDetailUpdateDtos.Any())
                    {
                        await _transferAssetDetailService.UpdateManyAsync(transferAssetDetailUpdateDtos);
                    }

                    if (transferAssetDetailDeleteIds.Any())
                    {
                        await _transferAssetDetailService.DeleteManyAsync(transferAssetDetailDeleteIds);
                    }
                }

                // Thêm sửa xóa nhiều ban giao nhận tài sản
                var receiverFlags = transferAssetUpdateFull.Receivers;

                if (receiverFlags != null)
                {
                    var receiverCreateDtos = receiverFlags.Where(receiverFlag => receiverFlag.ActionMode == ActionMode.CREATE).Select(receiverFlag => receiverFlag.Receiver);

                    var receiverUpdateDtos = receiverFlags.Where(receiverFlag => receiverFlag.ActionMode == ActionMode.UPDATE).Select(receiverFlag => receiverFlag.Receiver);

                    var receiverDeleteIds = receiverFlags.Where(receiverFlag => receiverFlag.ActionMode == ActionMode.DELETE).Select(receiverFlag => receiverFlag.Receiver.ReceiverId).ToList();


                    if (receiverCreateDtos.Any())
                    {
                        await _receiverService.CreateManyAsync(receiverCreateDtos, transferAssetUpdateDto.TransferAssetId);
                    }

                    if (receiverUpdateDtos.Any())
                    {
                        await _receiverService.UpdateManyAsync(receiverUpdateDtos);
                    }

                    if (receiverDeleteIds.Any())
                    {
                        await _receiverService.DeleteManyAsync(receiverDeleteIds);
                    }
                }

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Xóa một chứng từ
        /// </summary>
        /// <param name="transferAssetId">Mã Id của chứng từ cần xóa</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public async Task DeleteAsync(Guid transferAssetId)
        {
            var transferAssetIds = new List<Guid>
            {
                transferAssetId
            };

            // Kiểm tra điều kiện xóa
            await _transferAssetManager.CheckDeleteTransferAssetAsync(transferAssetIds);

            // Lấy ra chứng từ cần xóa
            var entity = await _transferAssetRepository.GetAsync(transferAssetId);

            // Xóa chứng từ
            await _transferAssetRepository.DeleteAsync(entity);
        }

        /// <summary>
        /// Xóa nhiều chứng từ và nhiều tài sản điều chuyền, nhiều ban giao nhận
        /// </summary>
        /// <param name="transferAssetIds">Danh sách mã Id của các chứng từ cần xóa</param>
        /// <returns></returns>
        /// <exception cref="UserException">Loại lệ người dùng</exception>
        public async Task DeleteManyAsync(List<Guid> transferAssetIds)
        {
          
            // Kiểm tra xem danh sách id rỗng
            if (transferAssetIds.Count == 0)
            {
                throw new UserException(VietNamese.EmptyList);
            }

            // Kiểm tra điều kiện xóa chứng từ
            await _transferAssetManager.CheckDeleteTransferAssetAsync(transferAssetIds);

            // Lấy ra danh sách chứng từ cần xóa
            var fixedAssets = await _transferAssetRepository.FindManyAsync(transferAssetIds);

            // Kiểm tra xem có chứng từ nào không tồn tại
            if (fixedAssets.Count() != transferAssetIds.Count)
            {
                throw new UserException(VietNamese.NoDelete);
            }

            // Lấy ra danh sách những tài sản điều chuyển cần xóa
            var transferAssetDetails = await _transferAssetDetailService.GetManyByTransferAssetIdsAsync(transferAssetIds);

            // Lấy ra danh sách ban giao nhận cần xóa
            var receivers = await _receiverService.FindManyByTransferAssetIdsAsync(transferAssetIds);

            // Mở Transaction
            await _unitOfWork.BeginTransactionAsync();
            try
            {   
                // Xóa nhiều tài sản điều chuyển
                if (transferAssetDetails.Any())
                {
                    await _transferAssetDetailService.DeleteManyAsync(transferAssetDetails.Select(transferAssetDetail => transferAssetDetail.TransferAssetDetailId).ToList());
                }
                 
                // Xóa nhiều ban giao nhận
                if (receivers.Any())
                {
                    await _receiverService.DeleteManyAsync(receivers.Select(receiver => receiver.ReceiverId).ToList());
                }

                // Xóa nhiều chứng từ
                await _transferAssetRepository.DeleteManyAsync(fixedAssets);

                // Commit
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {   
                // Rollback khi có lỗi xảy ra
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
        #endregion
    }
}
