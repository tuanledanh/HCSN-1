using Application.DTO;
using Application.Interface;
using AutoMapper;
using Domain.Exceptions;
using Domain.Model;
using MSIA.WebFresher052023.Application.DTO;
using MSIA.WebFresher052023.Application.Response.Base;
using MSIA.WebFresher052023.Application.Service.Base;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Entity.Base;
using MSIA.WebFresher052023.Domain.Enum;
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

        /// <summary>
        /// Gọi đến hàm GetAsync ở repository để xử lý lấy thông tin của bản ghi
        /// </summary>
        /// <param name="code">Mã code của bản ghi</param>
        /// <returns>Một bản ghi</returns>
        /// Created by: ldtuan (12/07/2023)
        public override async Task<TransferAssetDto> GetAsync(string code)
        {
            var transferAsset = await _transferAssetRepository.FindByCodeAsync(code);
            var transferAssetDto = _mapper.Map<TransferAssetDto>(transferAsset);

            var transferDetails = transferAsset.FixedAssetTransfers;
            var transferDetailDtos = _mapper.Map<List<FixedAssetTransferDto>>(transferDetails);
            transferAssetDto.FixedAssetTransfers = new List<FixedAssetTransferDto>();
            transferAssetDto.FixedAssetTransfers.AddRange(transferDetailDtos);

            return transferAssetDto;
        }

        public async Task<BaseFilterResponse<TransferAssetDto>> GetAllCustomAsync(int? pageNumber, int? pageLimit, string filterName)
        {
            List<TransferAssetModel> entities;
            pageNumber = pageNumber.HasValue ? pageNumber : 1;
            pageLimit = pageLimit.HasValue ? pageLimit : 20;
            filterName = string.IsNullOrEmpty(filterName) ? "" : filterName;

            int totalRecords = await _transferAssetRepository.GetCountAsync();
            int totalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / (double)pageLimit));

            entities = await _transferAssetRepository.GetFilterAsync(pageNumber, pageLimit, filterName);
            List<TransferAssetDto> entitiesDto = _mapper.Map<List<TransferAssetDto>>(entities);
            var filterData = new BaseFilterResponse<TransferAssetDto>(totalPages, totalRecords, entitiesDto);
            return filterData;
        }

        /// <summary>
        /// Thêm mới chứng từ, bên người nhận, danh sách chi tiết chứng từ
        /// </summary>
        /// <param name="transferAssetCreateDto">Thông tin chứng từ mới, danh sách bên người nhận và chi tiết chứng từ cần thêm mới</param>
        /// Created by: ldtuan (30/08/2023)
        public override async Task<bool> InsertAsync(TransferAssetCreateDto transferAssetCreateDto)
        {
            #region Validate
            // ======================================================= VALIDATE START =======================================================
            // 1.Check tài sản điều chuyển, chi tiết tài sản điều chuyển, bên giao nhận xem có null không
            _transferAssetManager.CheckNullRequest(transferAssetCreateDto.TransferAsset, transferAssetCreateDto.ListTransferAssetDetail);

            var transferAssetDto = transferAssetCreateDto.TransferAsset;
            await _transferAssetManager.CheckDuplicateCode(transferAssetDto.TransferAssetCode);
            // 2.Nếu ngày điều chuyển nhỏ hơn ngày chứng từ thì sai
            _transferAssetManager.CheckDate(transferAssetDto);

            var listTransferAssetDetails = _mapper.Map<List<TransferAssetDetail>>(transferAssetCreateDto.ListTransferAssetDetail);

            // 3.Kiểm tra xem các tài sản và phòng ban có tồn tại trong db không
            await _transferAssetManager.CheckExistAsset(listTransferAssetDetails);
            // ======================================================= VALIDATE END  =======================================================
            #endregion

            #region Create-Update-Delete
            // ======================================================= CREATE START  =======================================================
            // 1.Bắt đầu thêm mới
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // 1.1.Thêm mới tài sản điều chuyển
                var transferAsset = _mapper.Map<TransferAsset>(transferAssetDto);
                transferAsset.CreatedDate = DateTime.Now;
                transferAsset.ModifiedDate = DateTime.Now;
                transferAsset.SetKey(Guid.NewGuid());

                await _transferAssetRepository.InsertAsync(transferAsset);

                // 1.2.Thêm nhiều bên giao nhận
                var listReceiverDtos = transferAssetCreateDto.ListReceiver;
                if (listReceiverDtos != null)
                {
                    List<Receiver> receiversRaw = _mapper.Map<List<Receiver>>(listReceiverDtos);
                    List<Receiver> receivers = InitializeEntities(receiversRaw, transferAsset);

                    await _receiverRepository.InsertMultiAsync(receivers);
                }

                // 1.3.Thêm chi tiết điều chuyển tài sản
                List<TransferAssetDetail> transferAssetDetails = InitializeEntities(listTransferAssetDetails, transferAsset);

                await _transferAssetDetailRepository.InsertMultiAsync(transferAssetDetails);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return false;
            }
            // ======================================================= CREATE END  ======================================================= 
            #endregion
        }

        /// <summary>
        /// Hàm cập nhật chứng từ, sửa đổi bên người nhận và danh sách chi tiết chứng từ
        /// </summary>
        /// <param name="transferAssetId">Id của chứng từ cần cập nhật</param>
        /// <param name="transferAssetUpdateDto">Thông tin chứng từ mới, danh sách bên người nhận và chi tiết chứng từ cần sửa đổi</param>
        /// <exception cref="DataException">Lỗi dữ liệu truyền vào</exception>
        /// Created by: ldtuan (30/08/2023)
        public override async Task<bool> UpdateAsync(Guid transferAssetId, TransferAssetUpdateDto transferAssetUpdateDto)
        {
            #region Validate
            // ======================================================= VALIDATE START =======================================================
            // 1.Kiểm tra xem trong db có tồn tại chứng từ này hay không
            var oldTransferAsset = await _transferAssetRepository.GetAsync(transferAssetId) ?? throw new DataException();
            if (transferAssetUpdateDto == null) throw new DataException();

            // 2.Check tài sản điều chuyển, chi tiết tài sản điều chuyển, bên giao nhận xem có null không
            _transferAssetManager.CheckNullRequest(transferAssetUpdateDto.TransferAsset, transferAssetUpdateDto.ListTransferAssetDetail);

            var transferAssetDto = transferAssetUpdateDto.TransferAsset;
            await _transferAssetManager.CheckDuplicateCode(transferAssetDto.TransferAssetCode, oldTransferAsset.TransferAssetCode);
            // 3.Nếu ngày điều chuyển nhỏ hơn ngày chứng từ thì sai
            _transferAssetManager.CheckDate(transferAssetDto);

            // 4.Sau khi check không null thì bắt đầu lấy danh sách tài sản điều chuyển và bên người nhận
            var listTransferAssetDeatilDtos = transferAssetUpdateDto.ListTransferAssetDetail.ToList();
            var listReceiverDtos = transferAssetUpdateDto.ListReceiver.ToList();

            // 5.Kiểm tra xem các tài sản và phòng ban có tồn tại trong db không
            var listTransferAssetDetails = _mapper.Map<List<TransferAssetDetail>>(listTransferAssetDeatilDtos);
            await _transferAssetManager.CheckExistAsset(listTransferAssetDetails);

            // 6.Kiểm tra xem danh sách chi tiết chứng từ có tồn tại trong db hay không, áp dụng với cập nhật và xóa
            var listTransferAssetDetailIds = listTransferAssetDeatilDtos
                .Where(transfer => transfer.Flag == ActionMode.Update || transfer.Flag == ActionMode.Delete)
                .Select(transfer => transfer.TransferAssetDetailId)
                .Distinct()
                .ToList();
            var listTransferAssetDetailInDB = await _transferAssetDetailRepository.GetListByIdsAsync(listTransferAssetDetailIds);
            if (listTransferAssetDetailInDB.Count != listTransferAssetDetailIds.Count)
            {
                throw new DataException();
            }
            // ======================================================= VALIDATE END  =======================================================
            #endregion

            #region Create-Update-Delete
            // ======================================================= CREATE-UPDATE-DELETE START  =======================================================
            // 1.Danh sách tài sản điều chuyển create-update-delete
            var transferDtosCreate = listTransferAssetDeatilDtos.Where(transfer => transfer.Flag == ActionMode.Create).ToList();
            var transferDtosUpdate = listTransferAssetDeatilDtos.Where(transfer => transfer.Flag == ActionMode.Update).ToList();
            var transferDtosDelete = listTransferAssetDeatilDtos.Where(transfer => transfer.Flag == ActionMode.Delete).ToList();

            // 2.Danh sách người nhận create-update-delete
            List<ReceiverUpdateDto> receiverDtosCreate = new();
            List<ReceiverUpdateDto> receiverDtosUpdate = new();
            List<ReceiverUpdateDto> receiverDtosDelete = new();
            if (listReceiverDtos != null)
            {
                receiverDtosCreate = listReceiverDtos.Where(receiver => receiver.Flag == ActionMode.Create).ToList();
                receiverDtosUpdate = listReceiverDtos.Where(receiver => receiver.Flag == ActionMode.Update).ToList();
                receiverDtosDelete = listReceiverDtos.Where(receiver => receiver.Flag == ActionMode.Delete).ToList();
            }

            // 3.Bắt đầu thêm-sửa-xóa
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Cập nhật chứng từ
                DateTime? createdDate = oldTransferAsset.CreatedDate;
                var transferAsset = _mapper.Map(transferAssetDto, oldTransferAsset);
                transferAsset.SetKey(transferAssetId);
                transferAsset.ModifiedDate = DateTime.Now;
                transferAsset.CreatedDate = createdDate;

                await _transferAssetRepository.UpdateAsync(transferAsset);

                // 3.1.Thêm người nhận và chi tiết chứng từ
                if (transferDtosCreate != null)
                {
                    List<TransferAssetDetail> transferAssetDetailsRaw = _mapper.Map<List<TransferAssetDetail>>(transferDtosCreate);
                    List<TransferAssetDetail> transferAssetDetails = InitializeEntities(transferAssetDetailsRaw, transferAsset);

                    await _transferAssetDetailRepository.InsertMultiAsync(transferAssetDetails);
                }

                if (receiverDtosCreate != null)
                {
                    List<Receiver> receiversRaw = _mapper.Map<List<Receiver>>(receiverDtosCreate);
                    List<Receiver> receivers = InitializeEntities(receiversRaw, transferAsset);

                    await _receiverRepository.InsertMultiAsync(receivers);
                }

                // 3.2.Sửa người nhận và chi tiết chứng từ
                if (transferDtosUpdate != null)
                {
                    transferDtosUpdate = transferDtosUpdate.Select(item =>
                    {
                        item.TransferAssetId = transferAsset.TransferAssetId;
                        return item;
                    }).ToList();

                    List<TransferAssetDetail> transferAssetDetailsRaw = _mapper.Map<List<TransferAssetDetail>>(transferDtosUpdate);
                    List<TransferAssetDetail> transferAssetDetails = InitializeEntities(transferAssetDetailsRaw, createdDate);

                    await _transferAssetDetailRepository.UpdateMultiAsync(transferAssetDetails);
                }

                if (receiverDtosUpdate != null)
                {
                    List<Receiver> receiversRaw = _mapper.Map<List<Receiver>>(receiverDtosUpdate);
                    List<Receiver> receivers = InitializeEntities(receiversRaw, createdDate);

                    await _receiverRepository.UpdateMultiAsync(receivers);
                }

                // 3.3.Xóa người nhận và chi tiết chứng từ
                if (transferDtosDelete != null)
                {
                    List<TransferAssetDetail> transferAssetDetails = _mapper.Map<List<TransferAssetDetail>>(transferDtosDelete);

                    await _transferAssetDetailRepository.DeleteManyAsync(transferAssetDetails);
                }

                if (receiverDtosDelete != null)
                {
                    List<Receiver> receivers = _mapper.Map<List<Receiver>>(receiverDtosDelete);

                    await _receiverRepository.DeleteManyAsync(receivers);
                }

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return false;
            }
            // ======================================================= CREATE-UPDATE-DELETE END  =======================================================
            #endregion
        }

        /// <summary>
        /// Thêm ngày tạo, ngày sửa, id cho danh sách bản ghi truyền vào
        /// </summary>
        /// <param name="entitiesRaw">Danh sách bản ghi</param>
        /// <param name="transferAsset">Chứng từ tài sản</param>
        /// <returns>Danh sách sau khi sửa 1 vài thuộc tính</returns>
        /// Created by: ldtuan (30/08/2023)
        private static List<T> InitializeEntities<T>(List<T> entitiesRaw, TransferAsset? transferAsset) where T : BaseEntity, IHasKey
        {
            List<T> entities = new();

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

        /// <summary>
        /// Thêm ngày sửa cho danh sách bản ghi truyền vào (trường hợp update)
        /// </summary>
        /// <param name="entitiesRaw">Danh sách bản ghi</param>
        /// <returns>Danh sách sau khi sửa 1 vài thuộc tính</returns>
        /// Created by: ldtuan (31/08/2023)
        private static List<T> InitializeEntities<T>(List<T> entitiesRaw, DateTime? createdDate) where T : BaseEntity
        {
            List<T> entities = new();

            foreach (var entity in entitiesRaw)
            {
                entity.ModifiedDate = DateTime.Now;
                entity.CreatedDate = createdDate;

                entities.Add(entity);
            }

            return entities;
        }

        #endregion
    }
}
