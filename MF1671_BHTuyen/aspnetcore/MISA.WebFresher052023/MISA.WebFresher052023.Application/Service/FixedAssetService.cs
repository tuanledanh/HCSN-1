using AutoMapper;
using MISA.WebFresher052023.Application.Dto.FixedAsset;
using MISA.WebFresher052023.Application.Interface;
using MISA.WebFresher052023.Application.Service.Base;
using MISA.WebFresher052023.Domain.Entity;
using MISA.WebFresher052023.Domain.Entity.FixedAsset;
using MISA.WebFresher052023.Domain.Enum;
using MISA.WebFresher052023.Domain.Exceptions;
using MISA.WebFresher052023.Domain.Interface.FixedAsset;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using MISA.WebFresher052023.Domain.Model.FixedAsset;
using MISA.WebFresher052023.Domain.Resource;

namespace MISA.WebFresher052023.Application.Service
{
    public class FixedAssetService : BaseService<FixedAssetEntity, FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>, IFixedAssetService
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        private readonly IFixedAssetRepository _fixedAssetRepository;

        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        private readonly IFixedAssetManager _fixedAssetManager;

        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="fixedAssetRepository"></param>
        /// <param name="fixedAssetManager"></param>
        /// <param name="mapper"></param>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public FixedAssetService(IFixedAssetRepository fixedAssetRepository, IFixedAssetManager fixedAssetManager, IUnitOfWork unitOfWork, IMapper mapper) : base(fixedAssetRepository, fixedAssetManager, mapper)
        {
            _fixedAssetRepository = fixedAssetRepository;
            _fixedAssetManager = fixedAssetManager;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm ghi đè thêm mới
        /// </summary>
        /// <param name="fixedAssetCreateDto">CreateDto</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public override async Task CreateAsync(FixedAssetCreateDto fixedAssetCreateDto)
        {
            // Check trùng mã code
            await _fixedAssetManager.CheckCodeConflictAsync(fixedAssetCreateDto.FixedAssetCode);
            // Check loại tài sản có tồn tại
            await _fixedAssetManager.CheckExistByFixedAssetCategoryId(fixedAssetCreateDto.FixedAssetCategoryId);

            // Check phòng ban có tồn tại
            await _fixedAssetManager.CheckExistByDepartmentId(fixedAssetCreateDto.DepartmentId);

            // Check ngày mua và ngày bắt đầu sử dụng
            //await _estateManager.CheckPurChaseDayLaterUsingStartDay(createDto.PurchaseDay, createDto.UsingStartDay);

            var fixedAssetEntity = _mapper.Map<FixedAssetEntity>(fixedAssetCreateDto);

            fixedAssetEntity.SetKey(Guid.NewGuid());

            if (fixedAssetEntity is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.CreatedDate = DateTime.Now;
                baseAuditEntity.CreatedBy = VietNamese.Admin;
                baseAuditEntity.ModifiedDate = DateTime.Now;
                baseAuditEntity.ModifiedBy = VietNamese.Admin;
            }

            await _fixedAssetRepository.CreateAsync(fixedAssetEntity);
        }

        /// <summary>
        /// Hàm ghi đè cập nhật
        /// </summary>
        /// <param name="dtoId">DtoId</param>
        /// <param name="fixedAssetUpdateDto">UpdateDto</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public override async Task UpdateAsync(Guid dtoId, FixedAssetUpdateDto fixedAssetUpdateDto)
        {
            
            var fixedAssetEntity = await _fixedAssetRepository.GetAsync(dtoId);

            if (fixedAssetEntity.GetCode() != fixedAssetUpdateDto.GetCode())
            {
                await _baseManager.CheckCodeConflictAsync(fixedAssetUpdateDto.GetCode());
            }

            // Check loại tài sản có tồn tại
            await _fixedAssetManager.CheckExistByFixedAssetCategoryId(fixedAssetUpdateDto.FixedAssetCategoryId);

            // Check phòng ban có tồn tại
            await _fixedAssetManager.CheckExistByDepartmentId(fixedAssetUpdateDto.DepartmentId);

            _fixedAssetManager.CheckUsingStartedDateLaterPurchaseDate(fixedAssetEntity.PurchaseDate, fixedAssetEntity.UsingStartedDate);

            var fixedAssetEntityNew = _mapper.Map<FixedAssetEntity>(fixedAssetUpdateDto);

            if (fixedAssetEntityNew is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.ModifiedDate = DateTime.Now;
                baseAuditEntity.ModifiedBy = VietNamese.Admin;
            }

            fixedAssetEntityNew.SetKey(dtoId);
            await _fixedAssetRepository.UpdateAsync(fixedAssetEntityNew);
        }

        public async override Task DeleteAsync(Guid fixedAssetId)
        {
            var fixedAssetIds = new List<Guid>(){
                fixedAssetId
            };

            await _fixedAssetManager.CheckExistTransferAsset(fixedAssetIds, ActionMode.DELETE);

            var fixedAssetEntity = await _fixedAssetRepository.GetAsync(fixedAssetId);

            await _fixedAssetRepository.DeleteAsync(fixedAssetEntity);
        }


        /// <summary>
        /// Xuất danh sách tài sản ra file excel
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách Id của tài sản</param>
        /// <returns>File Excel trả về</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public async Task<byte[]> ExportListFixedAssetToExcelAsync(List<Guid> fixedAssetIds)
        {

            if (fixedAssetIds.Count == 0)
            {
                throw new Exception("Không được truyền danh sách rỗng");
            }

            // Tìm kiếm danh sách tài sản theo Id
            var fixedAssetEntities = await _fixedAssetRepository.FindManyFixedAssetAsync(fixedAssetIds);

            // Kiểm tra
            if (!fixedAssetEntities.Any())
            {
                throw new NotFoundException("Không tìm thấy tài sản");
            }

            if (fixedAssetIds.Count > fixedAssetEntities.Count())
            {
                throw new NotFoundException("Có tài sản không tồn tại");
            }

            // Chuyển sang model
            var fixedAssetExcelModels = _mapper.Map<IEnumerable<FixedAssetExcelModel>>(fixedAssetEntities);

            // Xuất ra file excel
            var contentFile = _fixedAssetRepository.ExportListFixedAssetToExcel(fixedAssetExcelModels);

            return contentFile;
        }

        public async Task<IEnumerable<FixedAssetDto>> GetFixedAssetFilterAsync(string? FixedAssetCodeOrName, string? DepartmentName, string? FixedAssetCategoryName)
        {
            var fixedAssetEntities = await _fixedAssetRepository.GetFixedAssetFilterAsync(FixedAssetCodeOrName, DepartmentName, FixedAssetCategoryName);

            var fixedAssetDtos = _mapper.Map<IEnumerable<FixedAssetDto>>(fixedAssetEntities);

            return fixedAssetDtos;
        }

        public async Task<string> GetFixedAssetCodeAsync()
        {
            var fixedAssetCode = await _fixedAssetRepository.GetFixedAssetCodeNewAsync();

            return fixedAssetCode;
        }

        public async Task<FixedAssetPagingDto> GetFixedAssetPagingAsync(FixedAssetFilterDto fixedAssetFilterDto)
        {
            var fixedAssetFilterModel = _mapper.Map<FixedAssetFilterModel>(fixedAssetFilterDto);

            var fixedAssetPagingModel = await _fixedAssetRepository.GetFixedAssetPagingAsync(fixedAssetFilterModel);

            var fixedAssetPagingDto = _mapper.Map<FixedAssetPagingDto>(fixedAssetPagingModel);

            return fixedAssetPagingDto;
        }

        public async Task DeleteManyAsync(List<Guid> fixedAssetIds)
        {
            await _fixedAssetManager.CheckExistTransferAsset(fixedAssetIds, ActionMode.DELETE);

            if (fixedAssetIds.Count == 0)
            {
                throw new Exception("Không được truyền danh sách rỗng");
            }

            var fixedAssets = await _fixedAssetRepository.FindManyFixedAssetAsync(fixedAssetIds);

            if (fixedAssets.Count() != fixedAssetIds.Count)
            {
                throw new Exception("Không thể xóa");
            }


            await _unitOfWork.BeginTransactionAsync();

            try
            {
                await _fixedAssetRepository.DeleteManyAsync(fixedAssets);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<FixedAssetPagingDto> GetFixedAssetTransferPagingAsync(FixedAssetFilterDto fixedAssetFilterDto)
        {
            var fixedAssetFilterModel = _mapper.Map<FixedAssetFilterModel>(fixedAssetFilterDto);

            var fixedAssetPagingModel = await _fixedAssetRepository.GetFixedAssetTransferPagingAsync(fixedAssetFilterModel);

            var fixedAssetPagingDto = _mapper.Map<FixedAssetPagingDto>(fixedAssetPagingModel);

            return fixedAssetPagingDto;
        }
        #endregion
    }
}