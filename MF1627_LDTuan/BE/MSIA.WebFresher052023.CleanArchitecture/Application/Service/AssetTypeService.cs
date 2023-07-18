using Application.DTO.Assett;
using Application.DTO.Assettype;
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
    public class AssetTypeService : BaseService<AssetType, AssetTypeModel, AssetTypeDto, AssetTypeCreateDto, AssetTypeUpdateDto>, IAssetTypeService
    {
        #region Constructor
        public AssetTypeService(IAssetTypeRepository assetTypeRepository, IMapper mapper) : base(assetTypeRepository, mapper)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gọi đến hàm InsertAsync ở repository để xử lý thêm mới một bản ghi
        /// </summary>
        /// <param name="assetTypeCreateDto">Thông tin của bản ghi</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<bool> InsertAsync(AssetTypeCreateDto assetTypeCreateDto)
        {
            // Thêm check mã trùng
            var entity = _mapper.Map<AssetType>(assetTypeCreateDto);
            bool result = await _baseRepository.InsertAsync(entity);
            if (!result)
            {
                throw new Exception("Service: Không thể thêm mới loại tài sản");
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
        public virtual async Task<bool> UpdateAsync(Guid id, AssetTypeUpdateDto assetTypeUpdateDto)
        {
            var existAssetType = await _baseRepository.GetAsync(id);
            if (existAssetType != null)
            {
                throw new DuplicateExeption("Mã đã tồn tại");
            }
            var entity = _mapper.Map<AssetType>(assetTypeUpdateDto);
            bool result = await _baseRepository.UpdateAsync(entity);
            if (!result)
            {
                throw new Exception("Service: Không thể cập nhật loại tài sản");
            }
            else
            {
                return result;
            }
        } 
        #endregion
    }
}
