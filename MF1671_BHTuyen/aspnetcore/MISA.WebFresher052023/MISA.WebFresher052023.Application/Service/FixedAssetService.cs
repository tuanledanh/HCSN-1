using AutoMapper;
using MISA.WebFresher052023.Application.Dto.FixedAsset;
using MISA.WebFresher052023.Application.Interface;
using MISA.WebFresher052023.Application.Service.Base;
using MISA.WebFresher052023.Domain.Entity;
using MISA.WebFresher052023.Domain.Entity.FixedAsset;
using MISA.WebFresher052023.Domain.Interface.FixedAsset;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;

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
            await _fixedAssetManager.CheckExistByCode(fixedAssetCreateDto.FixedAssetCode);
            // Check loại tài sản có tồn tại
            await _fixedAssetManager.CheckExistByFixedAssetCategoryId(fixedAssetCreateDto.FixedAssetCategoryId);

            // Check phòng ban có tồn tại
            await _fixedAssetManager.CheckExistByDepartmentId(fixedAssetCreateDto.DepartmentId);

            // Check ngày mua và ngày bắt đầu sử dụng
            //await _estateManager.CheckPurChaseDayLaterUsingStartDay(createDto.PurchaseDay, createDto.UsingStartDay);

            var fixedAssetEntity = _mapper.Map<FixedAssetEntity>(fixedAssetCreateDto);

            fixedAssetEntity.SetKeyId(Guid.NewGuid().ToString());

            if (fixedAssetEntity is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.CreatedDate = DateTime.Now;
                baseAuditEntity.CreatedBy = "BHTuyen";
                baseAuditEntity.ModifiedDate = DateTime.Now;
                baseAuditEntity.ModifiedBy = "BHTuyen";
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
        public override async Task UpdateAsync(string dtoId, FixedAssetUpdateDto fixedAssetUpdateDto)
        {
            var fixedAssetEntity = await _fixedAssetRepository.GetAsync(dtoId);

            if (fixedAssetEntity.GetKeyCode() != fixedAssetUpdateDto.GetKeyCode())
            {
                await _baseManager.CheckExistByCode(fixedAssetUpdateDto.GetKeyCode());
            }

            // Check loại tài sản có tồn tại
            await _fixedAssetManager.CheckExistByFixedAssetCategoryId(fixedAssetUpdateDto.FixedAssetCategoryId);

            // Check phòng ban có tồn tại
            await _fixedAssetManager.CheckExistByDepartmentId(fixedAssetUpdateDto.DepartmentId);

            var fixedAssetEntityNew = _mapper.Map<FixedAssetEntity>(fixedAssetUpdateDto);

            if (fixedAssetEntity is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.ModifiedDate = DateTime.Now;
                baseAuditEntity.ModifiedBy = "BHTuyen";
            }

            fixedAssetEntityNew.SetKeyId(dtoId);
            await _fixedAssetRepository.UpdateAsync(fixedAssetEntityNew);
        }

        /// <summary>
        /// Xóa nhiều Dto
        /// </summary>
        /// <param name="fixedAssetIds">EstateId</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public async Task DeleteManyAsync(List<string> fixedAssetIds)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                if (fixedAssetIds.Count == 0)
                {
                    throw new Exception("Không được truyền danh sách rỗng");
                }

                var fixedAssetEntities = await _fixedAssetRepository.FindManyByIdAsync(fixedAssetIds);

                if (fixedAssetIds.Count > fixedAssetEntities.Count())
                {
                    throw new Exception("Không thể xóa");
                }
                await _fixedAssetRepository.DeleteManyAsync(fixedAssetEntities);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Tìm kiếm danh sách tài sản cần xuất ra file excel
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách ID của tài sản</param>
        /// <returns>Danh sách tài sản</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<IEnumerable<FixedAssetExcel>> FindManyByIdAsync(List<string> fixedAssetIds)
        {
            try
            {
                var fixedAssetEntities = await _fixedAssetRepository.FindManyByIdAsync(fixedAssetIds);

                if (!fixedAssetEntities.Any())
                {
                    throw new Exception("Không tìm thấy tài sản");
                }

                if (fixedAssetIds.Count > fixedAssetEntities.Count())
                {
                    throw new Exception("Có tài sản không tồn tại");
                }

                var fixedAssetExcels = _mapper.Map<List<FixedAssetExcel>>(fixedAssetEntities);

                return fixedAssetExcels;
            }
            catch (Exception) { throw; }
        }


        /// <summary>
        /// Hàm sinh mã tài sản gợi ý
        /// </summary>
        /// <returns>Mã tài sản</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task<string> GetFixedAssetCodeAsync()
        {
            var fixedAssetCode = await _fixedAssetRepository.GetFixedAssetCode();
            return fixedAssetCode;
        }

        /// <summary>
        /// Lấy ra số tài sản theo trang và bộ lọc
        /// </summary>
        /// <param name="fixedAssetFilterDto"></param>
        /// <returns>Danh sách tài sản theo trang và tổng số bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (27/07/2023)
        public async Task<FixedAssetPagingDto> GetFixedAssetPagingAsync(FixedAssetFilterDto fixedAssetFilterDto)
        {
            var fixedAssetFilterEntity = _mapper.Map<FixedAssetFilterEntity>(fixedAssetFilterDto);

            var fixedAssetPagingEntity = await _fixedAssetRepository.GetFixedAssetPagingAsync(fixedAssetFilterEntity);

            var fixedAssetPagingDto = _mapper.Map<FixedAssetPagingDto>(fixedAssetPagingEntity);
            return fixedAssetPagingDto;
        }
        #endregion
    }
}