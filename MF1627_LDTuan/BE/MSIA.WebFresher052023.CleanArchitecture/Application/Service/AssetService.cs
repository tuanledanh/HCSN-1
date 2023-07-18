using Application.DTO.Assett;
using Application.Interface;
using AutoMapper;
using Domain.Entity;
using Domain.Exceptions;
using Domain.Interface.Assett;
using Domain.Interface.Assettype;
using Domain.Interface.Depart;
using Domain.Model;

namespace Application.Service
{
    public class AssetService : BaseService<Asset, AssetModel, AssetDto, AssetCreateDto, AssetUpdateDto>, IAssetService
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IAssetTypeRepository _assetTypeRepository;
        #endregion

        #region Constructor
        public AssetService(IAssetRepository assetRepository, IMapper mapper, IDepartmentRepository departmentRepository, IAssetTypeRepository assetTypeRepository) : base(assetRepository, mapper)
        {
            _departmentRepository = departmentRepository;
            _assetTypeRepository = assetTypeRepository;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Gọi đến hàm InsertAsync ở repository để xử lý thêm mới một bản ghi
        /// </summary>
        /// <param name="assetCreateDto">Thông tin của bản ghi</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<bool> InsertAsync(AssetCreateDto assetCreateDto)
        {
            // Thêm check mã trùng
            var existDepartment = await _departmentRepository.GetAsync(assetCreateDto.DepartmentId);
            var existAssetType = await _assetTypeRepository.GetAsync(assetCreateDto.AssetTypeId);
            if (existDepartment == null || existDepartment == null)
            {
                // Để hàm check exist sang 1 class khác để tái sử dụng
                throw new NotFoundException("Không tìm thấy phòng ban hoặc tài sản");
            }
            var entity = _mapper.Map<Asset>(assetCreateDto);
            bool result = await _baseRepository.InsertAsync(entity);
            if (!result)
            {
                throw new Exception("Service: Không thể thêm mới tài sản");
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Gọi đến hàm UpdateAsync ở repository để xử lý cập nhật một bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Thông tin của bản ghi</param>
        /// <returns>True nếu cập nhật thành công, false nếu cập nhật thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<bool> UpdateAsync(Guid id, AssetUpdateDto assetUpdateDto)
        {
            var existAsset = await _baseRepository.GetAsync(id);
            if (existAsset != null)
            {
                throw new DuplicateExeption("Mã đã tồn tại");
            }
            var existDepartment = await _departmentRepository.GetAsync(assetUpdateDto.DepartmentId);
            var existAssetType = await _assetTypeRepository.GetAsync(assetUpdateDto.AssetTypeId);
            if (existDepartment == null || existAssetType == null)
            {
                // Để hàm check exist sang 1 class khác để tái sử dụng
                throw new NotFoundException("Không tìm thấy phòng ban hoặc tài sản");
            }
            var entity = _mapper.Map<Asset>(assetUpdateDto);
            bool result = await _baseRepository.UpdateAsync(entity);
            if (!result)
            {
                throw new Exception("Service: Không thể cập nhật tài sản");
            }
            else
            {
                return result;
            }
        } 
        #endregion
    }
}
