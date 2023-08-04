using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Domain.Interface.AssetTypes
{
    public interface IAssetTypeManager 

    {
        /// <summary>
        /// Check trùng mã loại tài sản
        /// </summary>
        /// <param name="positionCode">Mã loại tài sản</param>
        /// <param name="oldPositionCode">Mã loại tài sản cữ (trong trường hợp cập nhật mã)</param>
        /// CreatedBy: nhtrieu (15/07/2023)
        Task CheckExistAssetTypeCode(string assetTypeCode, string? oldAssetTypeCode = null);
    }
}
