using MISA.FresherWeb202305.Demo.Domain.Exceptions;
using MISA.FresherWeb202305.Demo.Domain.Interface.AssetTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Domain.Service
{
    public class AssetTypeManager : IAssetTypeManager
    {
        private readonly IAssetTypeRepository _assetTypeRepository;

        // Constructor của AssetTypeManager, chấp nhận một đối tượng IAssetTypeRepository để thực hiện các thao tác liên quan đến AssetType.
        public AssetTypeManager(IAssetTypeRepository assetTypeRepository)
        {
            _assetTypeRepository = assetTypeRepository;
        }


        /// <summary>
        ///  // Phương thức CheckExistAssetTypeCode kiểm tra xem mã vị trí đã tồn tại hay chưa.
        /// </summary>
        /// <param name="assetTypeCode">Mã loại tài sản</param>
        /// <param name="oldAssetTypeCode">mã loại tài sản cũ</param>
        /// <returns></returns>
        /// <exception cref="ConflicException"></exception>
        /// author:nhtrieu(15/07/2023)
        public async Task CheckExistAssetTypeCode(string assetTypeCode, string? oldAssetTypeCode = null)
        {
            // Gọi phương thức FindByCodeAsync từ _assetTypeRepository để tìm kiếm AssetType có mã tương ứng.
            var existAssetType = await _assetTypeRepository.FindByCodeAsync(assetTypeCode);

            // Nếu tồn tại một AssetType có mã trùng khớp và mã đó khác với mã cũ (nếu có), thì ném ngoại lệ ConflicException.
            if (existAssetType != null && existAssetType.AssetTypeCode != oldAssetTypeCode)
            {
                throw new ConflicException($"Mã loại tài sản '{assetTypeCode}' đã tồn tại");
            }
        }
    }
}
