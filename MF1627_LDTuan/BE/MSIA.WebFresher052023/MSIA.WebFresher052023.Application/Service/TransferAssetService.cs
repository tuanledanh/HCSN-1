using Application.DTO;
using Application.Interface;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Exceptions;
using Domain.Model;
using MSIA.WebFresher052023.Application.DTO;
using MSIA.WebFresher052023.Application.Response.Base;
using MSIA.WebFresher052023.Application.Service.Base;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Entity.Base;
using MSIA.WebFresher052023.Domain.Enum;
using MSIA.WebFresher052023.Domain.Exceptions;
using MSIA.WebFresher052023.Domain.Interface;
using MSIA.WebFresher052023.Domain.Interface.Manager;
using MSIA.WebFresher052023.Domain.Interface.Repository;
using MSIA.WebFresher052023.Domain.Resource;
using MSIA.WebFresher052023.Domain.Service;

namespace Application.Service
{
    public class TransferAssetService : BaseService<TransferAsset, TransferAssetModel, TransferAssetDto, TransferAssetCreateDto, TransferAssetUpdateDto, TransferAssetUpdateMultiDto>, ITransferAssetService
    {
        #region Fields
        private readonly ITransferAssetRepository _transferAssetRepository;
        private readonly ITransferAssetManager _transferAssetManager;
        private readonly ITransferAssetDetailManager _transferAssetDetailManager;
        private readonly IReceiverRepository _receiverRepository;
        private readonly IFixedAssetRepository _fixedAssetRepository;
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;
        #endregion

        #region Constructor
        public TransferAssetService(ITransferAssetRepository transferAssetRepository, IReceiverRepository receiverRepository, IFixedAssetRepository fixedAssetRepository, ITransferAssetDetailRepository transferAssetDetailRepository, IMapper mapper, ITransferAssetManager transferAssetManager, ITransferAssetDetailManager transferAssetDetailManager, IUnitOfWork unitOfWork) : base(transferAssetRepository, mapper, transferAssetManager, unitOfWork)
        {
            _transferAssetRepository = transferAssetRepository;
            _transferAssetManager = transferAssetManager;
            _receiverRepository = receiverRepository;
            _fixedAssetRepository = fixedAssetRepository;
            _transferAssetDetailRepository = transferAssetDetailRepository;
            _transferAssetDetailManager = transferAssetDetailManager;
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

        /// <summary>
        /// Lấy chứng từ mới nhất của các tài sản trong chứng từ đích
        /// </summary>
        /// <param name="transferId">Id của chứng từ</param>
        /// <returns>Ném ra thông báo lỗi nếu chứng từ mới nhất không phải là chứng từ đích, tức là tài sản bên trong có chứng từ phát sinh</returns>
        /// Created by: ldtuan (12/09/2023)
        public async Task GetNewestTransferAsset(Guid transferId)
        {
            var transferAsset = await _transferAssetRepository.GetAsync(transferId);
            if(transferAsset == null)
            {
                throw new Exception();
            }
            // Để tận dụng GetAllTransferAssetOfAsset, tạo 1 list chứa id chứng từ
            List<Guid> transferIdList = new()
            {
                transferId
            };

            // Lấy danh sách tài sản bên trong chứng từ này
            var transferAssets = await _transferAssetRepository.GetAllTransferAssetOfAsset(transferIdList);
            var assetIds = transferAssets
                .Where(transfer => transfer.TransferAssetId == transferId)
                .Select(transfer => transfer.FixedAssetId)
                .ToList();


            var transferList = await _transferAssetRepository.GetNewestTransferAssetByAssetId(assetIds);
            var newestTransfer = transferList.FirstOrDefault();
            if(newestTransfer != null && newestTransfer.TransferAssetId != transferId)
            {
                // Danh sách tài sản nhận được từ request truyền về
                var fixedAssetList = await _fixedAssetRepository.GetListByIdsAsync(assetIds);
                // Danh sách chi tiết chừng từ (đã tồn tài từ trước) trong DB của các tài sản này
                var detailList = await _transferAssetDetailRepository.GetListDetailByIdsAsync(assetIds);
                // Danh sách tất cả chứng từ của toàn bộ tài sản này
                var transferIds = detailList.Select(detail => detail.TransferAssetId).Distinct().ToList();
                var allTransferAssets = await _transferAssetRepository.GetAllTransferAssetOfAsset(transferIds);

                // Lấy chi tiết chứng từ của chứng từ mới nhất này
                var filterDetails = detailList
                    .Where(detail => detail.TransferAssetId == newestTransfer.TransferAssetId)
                    .ToList();

                // Lấy các tài sản riêng biệt nằm trong chứng từ này
                var filterFixedAssets = fixedAssetList
                    .Where(asset => filterDetails
                    .Any(detail => detail.FixedAssetId == asset.FixedAssetId))
                    .Distinct()
                    .ToList();

                // Xem tài sản nào nằm trong chứng từ đang cần thêm này, chọn 1 cái để hiển thị thông báo lỗi
                var duplicateFixedAsset = filterFixedAssets
                    .Where(asset => assetIds.Contains(asset.FixedAssetId))
                    .OrderByDescending(asset => asset.ModifiedDate)
                    .ToList();
                // Lấy mã code của tài sản này
                var fixedAsset = await _fixedAssetRepository.GetAsync(duplicateFixedAsset.FirstOrDefault().FixedAssetId);
                // Lấy danh sách chứng từ phát sinh của tài sản này
                var generatedDocument = allTransferAssets
                            .Where(transfer => transfer.FixedAssetId == fixedAsset.FixedAssetId && transfer.TransferDate > transferAsset.TransferDate)
                            .OrderByDescending(transfer => transfer.TransferDate)
                            .ToList();
                throw new ValidateException(ErrorMessagesTransferAsset.AriseUpdate(fixedAsset.FixedAssetCode), ErrorMessagesTransferAsset.Infor(generatedDocument));
            }
        }

        /// <summary>
        /// Lấy toàn bộ chứng từ và phân trang
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageLimit">Giới hạn số bản ghi mỗi trang</param>
        /// <param name="filterName">Search theo mã</param>
        /// <returns>Danh sách chứng từ</returns>
        /// Created by: ldtuan (03/09/2023)
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
            await _transferAssetManager.CheckDuplicateCodeAsync(transferAssetDto.TransferAssetCode, null, ErrorMessages.DuplicateCodeTransfer);

            var listTransferAssetDetails = _mapper.Map<List<TransferAssetDetail>>(transferAssetCreateDto.ListTransferAssetDetail);

            // 2.Kiểm tra xem các tài sản và phòng ban có tồn tại trong db không
            await _transferAssetManager.CheckExistAssetAsync(listTransferAssetDetails);

            // 3.Nếu ngày điều chuyển nhỏ hơn ngày chứng từ thì sai
            // Và ngày điều chuyển cũng phải lớn hơn các ngày điều chuyển của chứng từ mà tài sản hiện đang thuộc
            var listAssetIds = listTransferAssetDetails.Select(transfer => transfer.FixedAssetId).Distinct().ToList();
            await _transferAssetManager.CheckDateAsync(transferAssetDto, listAssetIds, ActionMode.Create);

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
            var oldTransferAsset = await _transferAssetRepository.GetAsync(transferAssetId) ?? throw new DataException(ErrorMessages.Data);
            if (transferAssetUpdateDto == null) throw new DataException(ErrorMessages.Data);

            // 2.Check tài sản điều chuyển, chi tiết tài sản điều chuyển, bên giao nhận xem có null không
            _transferAssetManager.CheckNullRequest(transferAssetUpdateDto.TransferAsset, transferAssetUpdateDto.ListTransferAssetDetail);

            var transferAssetDto = transferAssetUpdateDto.TransferAsset;
            await _transferAssetManager.CheckDuplicateCodeAsync(transferAssetDto.TransferAssetCode, oldTransferAsset.TransferAssetCode, ErrorMessages.DuplicateCodeTransfer);

            // 2.1.Không null thì gán lại id
            DateTime? createdDate = oldTransferAsset.CreatedDate;
            var transferAsset = _mapper.Map(transferAssetDto, oldTransferAsset);
            transferAsset.SetKey(transferAssetId);

            // 3.Sau khi check không null thì bắt đầu lấy danh sách tài sản điều chuyển và bên người nhận
            var listTransferAssetDeatilDtos = transferAssetUpdateDto.ListTransferAssetDetail.ToList();
            var listReceiverDtos = transferAssetUpdateDto.ListReceiver.ToList();

            // 4.Kiểm tra phòng ban mới và cũ của từng chi tiết chứng từ có bị trùng không
            foreach (var detail in listTransferAssetDeatilDtos)
            {
                if (detail.OldDepartmentId == detail.NewDepartmentId)
                {
                    throw new DataException(ErrorMessages.Data);
                }
            }

            // 5.Kiểm tra xem các tài sản và phòng ban có tồn tại trong db không
            var listTransferAssetDetails = _mapper.Map<List<TransferAssetDetail>>(listTransferAssetDeatilDtos);
            await _transferAssetManager.CheckExistAssetAsync(listTransferAssetDetails);

            // 6.Nếu ngày điều chuyển nhỏ hơn ngày chứng từ thì sai
            // Và ngày điều chuyển cũng phải lớn hơn các ngày điều chuyển của chứng từ mà tài sản hiện đang thuộc
            var listAssetIds = listTransferAssetDetails.Select(transfer => transfer.FixedAssetId).Distinct().ToList();
            await _transferAssetManager.CheckDateAsync(transferAssetDto, listAssetIds, ActionMode.Update);

            // 7.Kiểm tra xem danh sách chi tiết chứng từ có tồn tại trong db hay không, áp dụng với cập nhật và xóa
            var listTransferAssetDetailIds = listTransferAssetDeatilDtos
                .Where(transfer => transfer.Flag == ActionMode.Update || transfer.Flag == ActionMode.Delete)
                .Select(transfer => transfer.TransferAssetDetailId)
                .Distinct()
                .ToList();
            var listTransferAssetDetailInDB = await _transferAssetDetailRepository.GetListByIdsAsync(listTransferAssetDetailIds);
            if (listTransferAssetDetailInDB.Count != listTransferAssetDetailIds.Count)
            {
                throw new DataException(ErrorMessages.Data);
            }

            // 8.Kiểm tra các chi tiết chứng từ này có chứng từ phát sinh trước đó không (cho mục đích xóa)
            await CheckIfCanUpdateOrDelete(ActionMode.Delete, listTransferAssetDeatilDtos, oldTransferAsset.TransferAssetId);

            // 9.Kiểm tra các chi tiết chứng từ này có chứng từ phát sinh trước đó không (cho mục đích cập nhật)
            await CheckIfCanUpdateOrDelete(ActionMode.Update, listTransferAssetDeatilDtos, oldTransferAsset.TransferAssetId);

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
                // 3.1.Cập nhật chứng từ
                transferAsset.ModifiedDate = DateTime.Now;
                transferAsset.CreatedDate = createdDate;

                await _transferAssetRepository.UpdateAsync(transferAsset);

                // 3.2.Thêm người nhận và chi tiết chứng từ
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

                // 3.3.Sửa người nhận và chi tiết chứng từ
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
                    receiverDtosUpdate = receiverDtosUpdate.Select(item =>
                    {
                        item.TransferAssetId = transferAsset.TransferAssetId;
                        return item;
                    }).ToList();

                    List<Receiver> receiversRaw = _mapper.Map<List<Receiver>>(receiverDtosUpdate);
                    List<Receiver> receivers = InitializeEntities(receiversRaw, createdDate);

                    await _receiverRepository.UpdateMultiAsync(receivers);
                }

                // 3.4.Xóa người nhận và chi tiết chứng từ
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
                throw;
            }
            // ======================================================= CREATE-UPDATE-DELETE END  =======================================================
            #endregion
        }

        /// <summary>
        /// Xóa nhiều chứng từ, cùng ban giao nhận và chi tiết chứng từ bên trong chứng từ
        /// </summary>
        /// <param name="ids">Id của các chứng từ</param>
        /// <returns>True nếu xóa thành công, false nếu xóa thất bại</returns>
        /// Created by: ldtuan (02/09/2023)
        public override async Task<bool> DeleteManyAsync(List<Guid> ids)
        {
            // 1.Check chứng từ có tồn tại trong db không, nếu tồn tại thì sắp xếp theo ngày tạo với thứ tự giảm dần
            var transferAssets = await _transferAssetRepository.GetListByIdsAsync(ids);
            if (transferAssets == null || !transferAssets.Any() || transferAssets.Count != ids.Count)
            {
                throw new DataException(ErrorMessages.Data);
            }
            transferAssets = transferAssets.OrderByDescending(transfer => transfer.TransferDate).ToList();


            // 2.Từ các chứng từ trên, lấy được danh sách tài sản bên trong, từ đó tìm được toàn bộ chứng từ của các tài sản đó
            var allTransferAssets = await _transferAssetRepository.GetAllTransferAssetOfAsset(ids);

            // 3.Tạo các list receiver và detail để xóa
            List<TransferAssetDetail> listDetail = new();
            List<Receiver> listReceiver = new();

            // 3.kiểm tra xem chứng từ nhận được từ FE có phải là các chứng từ mới nhất trong DB không
            for (int i = 0; i < transferAssets.Count; i++)
            {
                // 3.1.Danh sách tài sản trong từng chứng từ 1 của danh sách truyền từ FE về
                var detailFE = allTransferAssets
                    .Where(transfer => transfer.TransferAssetId == transferAssets[i].TransferAssetId)
                    .OrderByDescending(transfer => transfer.TransferDate)
                    .ToList();

                // 3.2.So sánh created date của từng tài sản trong FE với trong DB
                for (int j = 0; j < detailFE.Count; j++)
                {
                    var DB = allTransferAssets
                        .Where(transfer => transfer.FixedAssetId == detailFE[j].FixedAssetId)
                        .OrderByDescending(transfer => transfer.TransferDate)
                        .FirstOrDefault();
                    if (DB != null && detailFE[j].CreatedDate != DB.CreatedDate)
                    {
                        // 3.2.1.Lấy mã code của tài sản để hiển thị lên thông báo lỗi
                        var fixedAsset = await _fixedAssetRepository.GetAsync(detailFE[j].FixedAssetId);

                        // 3.2.2.Lấy các chứng từ phát sinh ra
                        var generatedDocument = allTransferAssets
                            .Where(transfer => transfer.FixedAssetId == detailFE[j].FixedAssetId && transfer.TransferDate > detailFE[j].TransferDate)
                            .OrderByDescending(transfer => transfer.TransferDate)
                            .ToList();

                        throw new ValidateException(ErrorMessagesTransferAsset.Arise(fixedAsset.FixedAssetCode), ErrorMessagesTransferAsset.Infor(generatedDocument));
                    }
                }
                // 3.3.Xóa các mục của FE ra khỏi allTransferAssets dựa trên transferAssetId
                allTransferAssets.RemoveAll(transfer => transfer.TransferAssetId == transferAssets[i].TransferAssetId);

                // 3.4.Lấy danh sách người nhận để xóa
                List<Guid> transferAssetIds = new()
                {
                    transferAssets[i].TransferAssetId
                };
                var receivers = await _receiverRepository.GetListByIdsAsync(transferAssetIds);
                listReceiver.AddRange(receivers);

                // 3.5.Lấy danh sách chi tiết chứng từ để xóa
                List<Guid> assetIds = detailFE.Select(detail => detail.FixedAssetId).ToList();
                var transferDetailsModel = await _transferAssetDetailRepository.GetListDetailByIdsAsync(assetIds);
                List<Guid> detailIds = transferDetailsModel
                    .Where(transfer => transfer.TransferAssetId == transferAssets[i].TransferAssetId)
                    .Select(transfer => transfer.TransferAssetDetailId)
                    .ToList();
                var transferDetails = await _transferAssetDetailRepository.GetListByIdsAsync(detailIds);
                listDetail.AddRange(transferDetails);
            }

            // 4.Bắt đầu xóa
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _receiverRepository.DeleteManyAsync(listReceiver);

                await _transferAssetDetailRepository.DeleteManyAsync(listDetail);

                await _transferAssetRepository.DeleteManyAsync(transferAssets);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return false;
            }
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

        /// <summary>
        /// Kiểm tra các chi tiết chứng từ này có chứng từ phát sinh trước đó không
        /// </summary>
        /// <param name="actionMode">Xét theo hành động để lấy bản ghi tương ứng</param>
        /// <param name="list">Danh sách bản ghi cần lọc</param>
        /// <param name="transferAssetId">Id của chứng từ</param>
        /// <returns>Ném ra exception nếu có lỗi</returns>
        private async Task CheckIfCanUpdateOrDelete(ActionMode actionMode, List<TransferAssetDetailUpdateDto> list, Guid transferAssetId)
        {
            List<Guid> ids = new();
            if (actionMode == ActionMode.Update)
            {
                ids = list
                .Where(transfer => transfer.Flag == actionMode || transfer.Flag == ActionMode.Unchage)
                .Select(transfer => transfer.TransferAssetDetailId)
                .Distinct()
                .ToList();
            }
            else
            {
                ids = list
                .Where(transfer => transfer.Flag == actionMode)
                .Select(transfer => transfer.TransferAssetDetailId)
                .Distinct()
                .ToList();
            }
            if (ids != null && ids.Count > 0)
            {
                await _transferAssetDetailManager.CheckExistTransferBefore(ids, transferAssetId);
            }
        }
        #endregion
    }
}
