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
        /// Tạo mới một chứng từ trong bảng transfer_asset
        /// </summary>
        /// <param name="transferAsset">Chứng từ tạo mới</param>
        /// <param name="TransferAssetId">Mã Id của chứng từ</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền
        public async Task CreateAsync(TransferAssetDto transferAsset, Guid TransferAssetId)
        {
            var transferAssetCreate = _mapper.Map<TransferAssetEntity>(transferAsset);

            transferAssetCreate.SetKey(TransferAssetId);

            if (transferAssetCreate is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.CreatedDate = DateTime.Now;
                baseAuditEntity.CreatedBy = VietNamese.Admin;
                baseAuditEntity.ModifiedDate = DateTime.Now;
                baseAuditEntity.ModifiedBy = VietNamese.Admin;
            }

            await _transferAssetRepository.CreateAsync(transferAssetCreate);
        }

        /// <summary>
        /// Hàm tạo mới một chứng từ với Danh sách tài sản và ban giao nhận
        /// </summary>
        /// <param name="transferAssetCreateFull">Chứng từ cần tạo mới</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (10/09/2023)
        public async Task CreateTransferAssetFullAsync(TransferAssetCreateFull transferAssetCreateFull)
        {
            // Chứng từ cần thêm mới
            var transferAsset = transferAssetCreateFull.TransferAsset;

            // Check trùng mã code chứng từ
            await _transferAssetManager.CheckCodeConflictAsync(transferAsset.TransferAssetCode);

            // Danh sách tài sản điều chuyển
            var transferAssetDetailCreates = transferAssetCreateFull.TransferAssetDetails;

            // Danh sách Id tài sản điều chuyển cần thêm
            var fixedAssetIds = transferAssetDetailCreates.Select(transferAssetDetailCreate => transferAssetDetailCreate.FixedAssetId).ToList();

            // Nếu danh sách id rỗng thì gửi thông báo lỗi cho client
            if (fixedAssetIds == null || fixedAssetIds.Count == 0)
            {
                // Nếu danh sách rỗng thì gửi thông báo lỗi cho client
                throw new UserException(VietNamese.TransferAssetDetailNotAdd);
            }

            // Kiểm tra ngày điều chuyển
            await _transferAssetManager.CheckTransferDateAsync(fixedAssetIds, transferAsset.TransferDate, transferAsset.TransactionDate, null, ActionMode.CREATE);

            // Danh sách ban giao nhận (Có thể để trống)
            var receiverCreates = transferAssetCreateFull.Receivers;

            // Tạo mới một Mã Id của chứng từ
            var transferAssetId = Guid.NewGuid();

            // Mở Transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Thêm chứng từ vào bảng transfer_asset
                await CreateAsync(transferAsset, transferAssetId);

                // Thêm danh sách tài sản điều chuyển
                if (transferAssetDetailCreates != null && transferAssetDetailCreates.Any())
                {
                    await _transferAssetDetailService.CreateManyAsync(transferAssetDetailCreates, transferAssetId);
                }

                // Thêm danh sách ban giao nhận
                if (receiverCreates != null && receiverCreates.Any())
                {
                    await _receiverService.CreateManyAsync(receiverCreates, transferAssetId);
                }

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

        /// <summary>
        /// Cập nhật một chứng từ trong bảng transfer_asset
        /// </summary>
        /// <param name="transferAsset">Chứng từ cần cập nhật vào bảng transfer_asset</param>
        /// <returns></returns>
        public async Task UpdateAsync(TransferAssetDto transferAsset)
        {
            var transferAssetUpdate = _mapper.Map<TransferAssetEntity>(transferAsset);

            if (transferAssetUpdate is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.ModifiedBy = VietNamese.Admin;
                baseAuditEntity.ModifiedDate = DateTime.Now;
            }

            await _transferAssetRepository.UpdateAsync(transferAssetUpdate);
        }

        /// <summary>
        /// Cập nhật một chứng từ đầy đủ
        /// </summary>
        /// <param name="transferAssetUpdateFull">Chứng từ đầy đủ</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (10/09/2023)
        public async Task UpdateTransferAssetFullAsync(TransferAssetUpdateFull transferAssetUpdateFull)
        {
            // Chứng từ cần cập nhật
            var transferAsset = transferAssetUpdateFull.TransferAsset;

            // Lấy ra chứng từ cần cập nhật trong database để kiểm tra
            var transferAssetEntity = await _transferAssetRepository.GetAsync(transferAsset.TransferAssetId);

            // Kiểm tra trùng mã
            if (transferAssetEntity.GetCode() != transferAsset.TransferAssetCode)
            {
                await _transferAssetManager.CheckCodeConflictAsync(transferAsset.TransferAssetCode);
            }

            // Danh sách tài sản điều chuyển cần cập nhật (thêm, sửa, xóa nhiều)
            var transferAssetDetailFlags = transferAssetUpdateFull.TransferAssetDetails;

            // Danh sách tài sản điều chuyển thêm mới
            var transferAssetDetailCreate = transferAssetDetailFlags?.Where(transferAssetDetailFlag => transferAssetDetailFlag.ActionMode == ActionMode.CREATE).Select(transferAssetDetailFlag => transferAssetDetailFlag.TransferAssetDetail);

            // Danh sách tài sản điều chuyển cần cập nhật
            var transferAssetDetailUpdate = transferAssetDetailFlags?.Where(transferAssetDetailFlag => transferAssetDetailFlag.ActionMode == ActionMode.UPDATE).Select(transferAssetDetailFlag => transferAssetDetailFlag.TransferAssetDetail);

            // Danh sách Id tài sản điều chuyển cần xóa
            var transferAssetDetailDeleteIds = transferAssetDetailFlags?.Where(transferAssetDetailFlag => transferAssetDetailFlag.ActionMode == ActionMode.DELETE).Select(transferAssetDetailFlag => transferAssetDetailFlag.TransferAssetDetail.TransferAssetDetailId).ToList();

            // Danh sách Id tài sản điều chuyển cần thêm
            var fixedAssetCreateIds = transferAssetDetailCreate?.Select(transferAssetDetailCreateDto => transferAssetDetailCreateDto.FixedAssetId).ToList();

            // Danh sách Id tài sản điều chuyển cần cập nhật
            var fixedAssetUpdateIds = transferAssetDetailUpdate?.Select(transferAssetDetailCreateDto => transferAssetDetailCreateDto.FixedAssetId).ToList();

            // Danh sách Id để check
            var fixedAssetIds = fixedAssetUpdateIds == null ? fixedAssetCreateIds : fixedAssetCreateIds?.Concat(fixedAssetUpdateIds).ToList();

            if (fixedAssetIds != null)
            {
                // Kiểm tra ngày chứng từ cập nhật
                await _transferAssetManager.CheckTransferDateAsync(fixedAssetIds, transferAsset.TransferDate, transferAsset.TransactionDate, transferAsset.TransferAssetId, ActionMode.UPDATE);
            }

            // Danh sách người trong ban giao nhận có gắn cờ thêm, sửa, xóa
            var receiverFlags = transferAssetUpdateFull.Receivers;

            // Danh sách tạo
            var receiverCreate = receiverFlags?.Where(receiverFlag => receiverFlag.ActionMode == ActionMode.CREATE).Select(receiverFlag => receiverFlag.Receiver);

            // Danh sách cập nhật
            var receiverUpdate = receiverFlags?.Where(receiverFlag => receiverFlag.ActionMode == ActionMode.UPDATE).Select(receiverFlag => receiverFlag.Receiver);

            // Danh sách mã ID cần xóa
            var receiverDeleteIds = receiverFlags?.Where(receiverFlag => receiverFlag.ActionMode == ActionMode.DELETE).Select(receiverFlag => receiverFlag.Receiver.ReceiverId).ToList();

            // Mở Transaction
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Cập nhật chứng từ
                await UpdateAsync(transferAsset);

                // Thêm nhiều tài sản
                if (transferAssetDetailCreate != null && transferAssetDetailCreate.Any())
                {
                    await _transferAssetDetailService.CreateManyAsync(transferAssetDetailCreate, transferAsset.TransferAssetId);
                }

                // Cập nhật nhiều tài sản
                if (transferAssetDetailUpdate != null && transferAssetDetailUpdate.Any())
                {
                    await _transferAssetDetailService.UpdateManyAsync(transferAssetDetailUpdate);
                }

                // Xóa nhiều tài sản
                if (transferAssetDetailDeleteIds != null && transferAssetDetailDeleteIds.Any())
                {
                    await _transferAssetDetailService.DeleteManyAsync(transferAssetDetailDeleteIds);
                }

                // Thêm nhiều ban giao nhận
                if (receiverCreate != null && receiverCreate.Any())
                {
                    await _receiverService.CreateManyAsync(receiverCreate, transferAsset.TransferAssetId);
                }

                // Cập nhật nhiều ban giao nhận
                if (receiverUpdate != null && receiverUpdate.Any())
                {
                    await _receiverService.UpdateManyAsync(receiverUpdate);
                }

                // Xóa nhiều ban giao nhận
                if (receiverDeleteIds != null && receiverDeleteIds.Any())
                {
                    await _receiverService.DeleteManyAsync(receiverDeleteIds);
                }

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
            await _transferAssetManager.CheckDeleteManyTransferAssetAsync(transferAssetIds);

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

            // Kiểm tra điều kiện xóa chứng từ
            await _transferAssetManager.CheckDeleteManyTransferAssetAsync(transferAssetIds);

            // Lấy ra danh sách chứng từ cần xóa
            var fixedAssets = await _transferAssetRepository.FindManyAsync(transferAssetIds);

            // Lấy ra danh sách những tài sản điều chuyển cần xóa
            var transferAssetDetails = await _transferAssetDetailService.GetManyByTransferAssetIdsAsync(transferAssetIds);

            // Lấy ra danh sách ban giao nhận cần xóa
            var receivers = await _receiverService.FindManyByTransferAssetIdsAsync(transferAssetIds);

            // Mở Transaction
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Xóa nhiều tài sản điều chuyển
                if (transferAssetDetails != null && transferAssetDetails.Any())
                {
                    await _transferAssetDetailService.DeleteManyAsync(transferAssetDetails.Select(transferAssetDetail => transferAssetDetail.TransferAssetDetailId).ToList());
                }

                // Xóa nhiều ban giao nhận
                if (receivers != null && receivers.Any())
                {
                    await _receiverService.DeleteManyAsync(receivers.Select(receiver => receiver.ReceiverId).ToList());
                }

                // Xóa nhiều chứng từ
                if (fixedAssets != null && fixedAssets.Any())
                {
                    await _transferAssetRepository.DeleteManyAsync(fixedAssets);
                }

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
