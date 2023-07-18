using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Application.DTO.AssetDTO;
using MISA.WebFresher052023.HCSN.Application.Interface;
using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Service
{
    public class AssetService : IAssetService
    {
        #region Fields
        private readonly IAssetRepository _assetRepository;
        #endregion

        #region Constructor
        public AssetService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        } 
        #endregion

        #region Mapper
        /// <summary>
        /// Set ánh xạ DTO cho asset
        /// </summary>
        /// <param name="entity">property của Asset</param>
        /// <returns>assetDto</returns>
        /// Created By: LB.Thành (16/07/2023)
        private static AssetDto MapAssetToAssetDto(Asset entity)
        {
            var assetDto = new AssetDto()
            {
                AssetId = entity.AssetId,
                AssetName = entity.AssetName,
                AssetCode = entity.AssetCode,
                AssetQuantity = entity.AssetQuantity,
                AssetPrice = entity.AssetPrice,
                AssetDepreciation = entity.AssetDepreciation,
                YearOfUse = entity.YearOfUse,
                PurchaseDate = entity.PurchaseDate,
                StartUsingDate = entity.StartUsingDate,
                YearOfTracking = entity.YearOfTracking,
                AssetTypeId = entity.AssetTypeId,
                DepartmentId = entity.DepartmentId,
                DepartmentName = entity.DepartmentName,
                AssetTypeName = entity.AssetTypeName,
            };
            return assetDto;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Hàm lấy tất cả các bản ghi
        /// </summary>
        /// <returns>Tất cả các bản ghi</returns>
        /// Created by: LB.Thành (16/07/2023)
        public async Task<IEnumerable<AssetDto>> GetAllAsync()
        {
            var assets = await _assetRepository.GetAllAsync();

            var assetDtos = assets.Select(x => MapAssetToAssetDto(x));
            return assetDtos;
        }

        /// <summary>
        /// Hàm lấy 1 bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>1 bản ghi thỏa mãn</returns>
        /// Created by: LB.Thành (16/07/2023)
        public async Task<AssetDto> GetAsync(Guid id)
        {
            var asset = await _assetRepository.GetAsync(id);
            var assetDto = MapAssetToAssetDto(asset);
            return assetDto;
        }

        /// <summary>
        /// Hàm tạo mới một bản ghi
        /// </summary>
        /// <param name="assetCreateDto">Thông tin cần tạo mới</param>
        /// Created by: LB.Thành (17/07/2023)
        public async Task CreateAsync(AssetCreateDto assetCreateDto)
        {
            var asset = new Asset()
            {
                AssetId = Guid.NewGuid(),
                AssetName = assetCreateDto.AssetName,
                AssetCode = assetCreateDto.AssetCode,
                AssetQuantity = assetCreateDto.AssetQuantity,
                AssetPrice = assetCreateDto.AssetPrice,
                AssetDepreciation = assetCreateDto.AssetDepreciation,
                YearOfUse = assetCreateDto.YearOfUse,
                PurchaseDate = assetCreateDto.PurchaseDate,
                StartUsingDate = assetCreateDto.StartUsingDate,
                YearOfTracking = assetCreateDto.YearOfTracking,
                AssetTypeId = assetCreateDto.AssetTypeId,
                DepartmentId = assetCreateDto.DepartmentId,
            };

            await _assetRepository.InsertAsync(asset);
        }

        /// <summary>
        /// Hàm cập nhật một bản ghi
        /// </summary>
        /// <param name="assetUpdateDto">Thông tin cần cập nhật</param>
        /// Created by: LB.Thành (17/07/2023)
        public async Task UpdateAsync(AssetUpdateDto assetUpdateDto)
        {
            var asset = await _assetRepository.FindAsync(assetUpdateDto.AssetId);

            if (asset == null)
            {
                throw new NotFoundException("Không tìm thấy tài sản");
            }

            asset.AssetName = assetUpdateDto.AssetName;
            asset.AssetCode = assetUpdateDto.AssetCode;
            asset.AssetQuantity = assetUpdateDto.AssetQuantity;
            asset.AssetPrice = assetUpdateDto.AssetPrice;
            asset.AssetDepreciation = assetUpdateDto.AssetDepreciation;
            asset.YearOfUse = assetUpdateDto.YearOfUse;
            asset.PurchaseDate = assetUpdateDto.PurchaseDate;
            asset.StartUsingDate = assetUpdateDto.StartUsingDate;
            asset.YearOfTracking = assetUpdateDto.YearOfTracking;
            asset.AssetTypeId = assetUpdateDto.AssetTypeId;
            asset.DepartmentId = assetUpdateDto.DepartmentId;

            await _assetRepository.UpdateAsync(asset);
        }

        /// <summary>
        /// Hàm xóa một bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// Created by: LB.Thành (17/07/2023)
        public async Task DeleteAsync(Guid id)
        {
            var asset = await _assetRepository.FindAsync(id);

            if (asset == null)
            {
                throw new NotFoundException("Không tìm thấy tài sản");
            }

            await _assetRepository.DeleteAsync(asset);
        }

        #endregion


    }
}
