using MISA.FresherWeb202305.Demo.Domain.Entity;
using MISA.FresherWeb202305.Demo.Domain.Interface.Base;
using MISA.FresherWeb202305.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Domain.Interface.Assets
{
    public interface IAssetRepository : IBaseCodeRepository<Asset,AssetModel>
    {
        /// <summary>
        /// Tìm kiếm 1 tài sản theo code kế thừa từ lớp base
        /// </summary>
        /// <param name="id">code của tài sản muốn tìm kiếm</param>
        /// <returns>Tài sản muốn tìm kiếm (có thể null)</returns>
        /// author: nhtrieu (16/07/2023)
        Task<Pagination> FilterAsync(string search, int currentPage, int pageSize,string departmentCode,string assetTypeCode);

    }
}
