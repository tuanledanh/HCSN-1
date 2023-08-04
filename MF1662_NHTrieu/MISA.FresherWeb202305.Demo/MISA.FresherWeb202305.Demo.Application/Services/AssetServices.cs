using AutoMapper;
using MISA.FresherWeb202305.Demo.Application.Dto;
using MISA.FresherWeb202305.Demo.Application.Interface;
using MISA.FresherWeb202305.Demo.Application.Map;
using MISA.FresherWeb202305.Demo.Application.Services.Base;
using MISA.FresherWeb202305.Demo.Domain.Entity;
using MISA.FresherWeb202305.Demo.Domain.Interface;
using MISA.FresherWeb202305.Demo.Domain.Interface.Assets;
using MISA.FresherWeb202305.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Services
{
    public class AssetServices : BaseCodeService<Asset, AssetModel, AssetDto, AssetCreateDto, AssetUpdateDto>,
        IAssetServices
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetManager _assetManager;

        public AssetServices(
            IAssetRepository assetRepository,
            IAssetManager assetManager,
            IMapper mapper) : base(assetRepository, mapper)
        {
            _assetRepository = assetRepository;
            _assetManager = assetManager;
        }

        /// <summary>
        /// Tìm kiếm, filter và phân trang
        /// </summary>
        /// <param name="search">Từ khóa tìm kiếm theo tên hoặc mã tài sản</param>
        /// <param name="currentPage">Trang hiện tại</param>
        /// <param name="pageSize">Số lượng bản ghi trên mỗi trang</param>
        /// <param name="departmentCode">Mã phòng ban để lọc</param>
        /// <param name="assetTypeCode">Mã loại tài sản để lọc</param>
        /// <returns>Danh sách tài sản sau khi đã được lọc và phân trang</returns>
        /// CreatedBy: nhtrieu (15/07/2023)
        public async Task<Pagination> FilterAsync(string? search, int? currentPage, int? pageSize, string? departmentCode, string? assetTypeCode)
        {
            // Kiểm tra và thiết lập giá trị mặc định cho các tham số
            if (string.IsNullOrEmpty(search))
            {
                search = "";
            }
            if (!currentPage.HasValue)
            {
                currentPage = 1;
            }
            if (!pageSize.HasValue)
            {
                pageSize = 14;
            }
            if (string.IsNullOrEmpty(departmentCode))
            {
                departmentCode = "";
            }
            if (string.IsNullOrEmpty(assetTypeCode))
            {
                assetTypeCode = "";
            }
            // Lấy danh sách tài sản sau khi đã lọc và phân trang
            var assetPaged = await _assetRepository.FilterAsync(search, currentPage.Value, pageSize.Value, departmentCode, assetTypeCode);
            // Mapping danh sách tài sản sang danh sách DTO để trả về cho client
            assetPaged.Data = _mapper.Map<IEnumerable<AssetDto>>(assetPaged.Data);
            return assetPaged;
        }

       
        // Các phương thức này dùng để ánh xạ các đối tượng DTO (AssetCreateDto và AssetUpdateDto) thành các đối tượng Entity (Asset).
        // Các phương thức này được kế thừa từ lớp cơ sở BaseCodeService và ghi đè lại để thực hiện các xử lý cụ thể của từng trường hợp.
        protected override async Task<Asset> MapCreateDtoToEntityAsync(AssetCreateDto entityCreateDto)
        {
            // Kiểm tra trùng mã tài sản
            await _assetManager.CheckExistAssetCode(entityCreateDto.AssetCode);

            // Kiểm tra các ràng buộc về phòng ban và loại tài sản
            await _assetManager.CheckValidConstraint(entityCreateDto.DepartmentId, entityCreateDto.AssetTypeId);

            // Ánh xạ thông tin từ DTO sang Entity
            var asset = _mapper.Map<Asset>(entityCreateDto);
            asset.AssetId = Guid.NewGuid();
            asset.CreatedDate = DateTime.Now;
            asset.CreatedBy = "htrieu";
            asset.ModifiedDate = DateTime.Now;
            asset.ModifiedBy = "htrieu";

            return asset;
        }

        protected async override Task<Asset> MapUpdateDtoToEntityAsync(Guid entityId, AssetUpdateDto entityUpdateDto)
        {
            // Lấy thông tin tài sản cũ từ cơ sở dữ liệu
            var oldAsset = await _assetRepository.GetByIdAsync(entityId);

            // Kiểm tra trùng mã tài sản
            await _assetManager.CheckExistAssetCode(entityUpdateDto.AssetCode, oldAsset.AssetCode);

            // Kiểm tra các ràng buộc về phòng ban và loại tài sản
            await _assetManager.CheckValidConstraint(entityUpdateDto.DepartmentId, entityUpdateDto.AssetTypeId);

            // Ánh xạ thông tin từ DTO sang Entity
            var newAsset = _mapper.Map(entityUpdateDto, oldAsset);
            newAsset.CreatedDate = oldAsset.CreatedDate; // Keep the original CreatedDate value
            newAsset.CreatedBy = oldAsset.CreatedBy; // Keep the original CreatedBy value
            newAsset.ModifiedDate = DateTime.Now;
            newAsset.ModifiedBy = "htrieu";
            return newAsset;
        }
    }
}

    

