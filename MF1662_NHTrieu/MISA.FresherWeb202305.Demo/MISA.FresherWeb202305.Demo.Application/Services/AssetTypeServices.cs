using AutoMapper;
using MISA.FresherWeb202305.Demo.Application.Dto;
using MISA.FresherWeb202305.Demo.Application.Interface;
using MISA.FresherWeb202305.Demo.Application.Interface.Base;
using MISA.FresherWeb202305.Demo.Application.Services.Base;
using MISA.FresherWeb202305.Demo.Domain.Entity;
using MISA.FresherWeb202305.Demo.Domain.Interface.AssetTypes;
using MISA.FresherWeb202305.Demo.Domain.Interface.Base;
using MISA.FresherWeb202305.Demo.Domain.Interface.department;
using MISA.FresherWeb202305.Demo.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Services
{
    public class AssetTypeServices : BaseCodeService<AssetType, AssetType, AssetTypeDto, AssetTypeCreateDto, AssetTypeUpdateDto>, IAssetTypeServices
    {
        protected readonly IAssetTypeRepository _assetTypeRepository;
        protected readonly IAssetTypeManager _assetTypeManager;

        public AssetTypeServices(IAssetTypeRepository assetTypeRepository, IMapper mapper, IAssetTypeManager assetTypeManager)
            : base(assetTypeRepository, mapper)
        {
            _assetTypeManager = assetTypeManager;
            _assetTypeRepository = assetTypeRepository;
        }

        /// <summary>
        /// Ánh xạ thông tin từ DTO sang Entity khi tạo mới đối tượng AssetType
        /// </summary>
        /// <param name="entityCreateDto">DTO chứa thông tin đối tượng AssetType cần tạo</param>
        /// <returns>Đối tượng AssetType đã được ánh xạ thông tin từ DTO</returns>
        /// CreatedBy: nhtrieu (15/07/2023)
        protected async override Task<AssetType> MapCreateDtoToEntityAsync(AssetTypeCreateDto entityCreateDto)
        {
            // Kiểm tra trùng mã loại tài sản
            await _assetTypeManager.CheckExistAssetTypeCode(entityCreateDto.AssetTypeCode);

            // Ánh xạ thông tin từ DTO sang Entity
            var assetType = _mapper.Map<AssetType>(entityCreateDto);
            assetType.AssetTypeId = Guid.NewGuid();
            assetType.CreatedDate = DateTime.Now;
            assetType.CreatedBy = "Htrieu";
            assetType.ModifiedDate = DateTime.Now;
            assetType.ModifiedBy = "Htrieu";

            return assetType;
        }

        /// <summary>
        /// Ánh xạ thông tin từ DTO sang Entity khi cập nhật đối tượng AssetType
        /// </summary>
        /// <param name="entityId">Id của đối tượng AssetType cần cập nhật</param>
        /// <param name="entityUpdateDto">DTO chứa thông tin đối tượng AssetType cần cập nhật</param>
        /// <returns>Đối tượng AssetType đã được ánh xạ thông tin từ DTO</returns>
        /// CreatedBy: nhtrieu (15/07/2023)
        protected async override Task<AssetType> MapUpdateDtoToEntityAsync(Guid entityId, AssetTypeUpdateDto entityUpdateDto)
        {
            // Lấy thông tin AssetType cũ từ cơ sở dữ liệu
            var oldAssetType = await _assetTypeRepository.GetByIdAsync(entityId);

            // Kiểm tra trùng mã loại tài sản
            await _assetTypeManager.CheckExistAssetTypeCode(entityUpdateDto.AssetTypeCode, oldAssetType.AssetTypeCode);

            // Ánh xạ thông tin từ DTO sang Entity
            var newAssetType = _mapper.Map<AssetType>(oldAssetType);
            newAssetType.ModifiedDate = DateTime.Now;
            newAssetType.ModifiedBy = "htrieu";

            return newAssetType;
        }
    }
}
