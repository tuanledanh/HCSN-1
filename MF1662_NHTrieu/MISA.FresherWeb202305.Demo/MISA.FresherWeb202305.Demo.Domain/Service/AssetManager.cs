
using MISA.FresherWeb202305.Demo.Domain.Enums;
using MISA.FresherWeb202305.Demo.Domain.Exceptions;
using MISA.FresherWeb202305.Demo.Domain.Interface.Assets;


using MISA.FresherWeb202305.Demo.Domain.Interface.AssetTypes;
using MISA.FresherWeb202305.Demo.Domain.Interface.department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Domain.Service
{
    public class AssetManager : IAssetManager
    {

        #region Fileds
        /// <summary>
        /// Khởi tạo đối tượng AssetRrpository
        /// </summary>
        private readonly IAssetRepository _assetRepository; 
        /// <summary>
        /// Khởi tạo đối tượng DepartmnetRepository
        /// </summary>
        private readonly IDepartmentRepository _departmentRepository;
        /// <summary>
        /// Khởi tạo đối tượng AssetTypeRepository
        /// </summary>
        private readonly IAssetTypeRepository _assetTypeRepository;

        #endregion

        #region Constructer
        /// <summary>
        /// Thực hiện tạo instance của dối tương thông qua constructer
        /// </summary>
        /// <param name="assetRepository">instance của asset</param>
        /// <param name="departmentRepository">instance department</param>
        /// <param name="assetTypeRepository">instance assetype</param>
        public AssetManager(IAssetRepository assetRepository, IDepartmentRepository departmentRepository, IAssetTypeRepository assetTypeRepository)
        {
            _assetRepository = assetRepository;
            _departmentRepository = departmentRepository;
            _assetTypeRepository = assetTypeRepository;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Hàm kiểm tra xem mã tài sản có trùng hay không
        /// </summary>
        /// <param name="assetCode">Mã tài sản mới</param>
        /// <param name="oldAssetCode">mã tài sản cũ</param>
        /// <returns></returns>
        /// <exception cref="ConflicException"></exception>
        /// author:nhtrieu (16/07/2023)
        public async Task CheckExistAssetCode(string assetCode, string? oldAssetCode = null)
        {
            var assetCodeExist = await _assetRepository.FindByCodeAsync(assetCode);
            if (assetCodeExist != null && assetCodeExist.AssetCode != oldAssetCode)
            {
                throw new ConflicException($"Mã tài sản {assetCode} đã tồn tại", (int)ErrorCode.NotFound);
            }
        }
        /// <summary>
        /// Hàm kiểm tra xem các khóa ngoại có hợp lệ hay không
        /// </summary>
        /// <param name="departmentId">Khóa ngoại bảng department</param>
        /// <param name="assetTypeId">Khoa ngoại bảng assettype</param>
        /// <returns>thông báo lỗi</returns>
        /// <exception cref="NotFoundException"></exception>
        /// author: nhtrieu(15/07/2023)
        public async Task CheckValidConstraint(Guid departmentId, Guid assetTypeId)
        {
            var departmentExist = await _departmentRepository.GetByIdAsync(departmentId);
            var assetTypeExist = await _assetTypeRepository.GetByIdAsync(assetTypeId);
            if (departmentExist.DepartmentCode == null)
            {
                throw new NotFoundException($"Không tồn tại {departmentId} trong bảng");
            }
            if (assetTypeExist.AssetTypeCode == null)
            {
                throw new NotFoundException($"Không tồn tại {assetTypeId} trong bảng");
            }




        } 
        #endregion
    }
}
