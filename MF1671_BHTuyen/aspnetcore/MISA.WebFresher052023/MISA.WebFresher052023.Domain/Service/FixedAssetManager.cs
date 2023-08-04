using MISA.WebFresher052023.Domain.Entity.FixedAsset;
using MISA.WebFresher052023.Domain.Exceptions;
using MISA.WebFresher052023.Domain.Interface.department;
using MISA.WebFresher052023.Domain.Interface.FixedAsset;
using MISA.WebFresher052023.Domain.Interface.FixedAssetCategory;
using MISA.WebFresher052023.Domain.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Service
{
    public class FixedAssetManager : BaseManager<FixedAssetEntity>, IFixedAssetManager
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        private readonly IFixedAssetCategoryRepository _fixedAssetCategoryRepository;

        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        private readonly IDepartmentRepository _departmentRespostory;
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="fixedAssetCategoryRepository"></param>
        /// <param name="departmentRespostory"></param>
        /// <param name="fixedAssetRepository"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public FixedAssetManager(IFixedAssetRepository fixedAssetRepository, IFixedAssetCategoryRepository fixedAssetCategoryRepository, IDepartmentRepository departmentRespostory) : base(fixedAssetRepository)
        {
            _fixedAssetCategoryRepository = fixedAssetCategoryRepository;
            _departmentRespostory = departmentRespostory;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Message lỗi
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string MessageError { get; set; } = "Mã tài sản đã tồn tại";

        #endregion

        #region Methods
        /// <summary>
        /// Kiểm tra Id bộ phận có tồn tại hay không
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task CheckExistByDepartmentId(string departmentId)
        {
            var departmentEntity = await _departmentRespostory.FindAsync(departmentId);
            if (departmentEntity != null)
            {
                return;
            }
            throw new NotFoundException("Bộ phận không tồn tại");
        }

        /// <summary>
        /// Kiểm tra Id tài sản có tồn tại hay không
        /// </summary>
        /// <param name="fixedAssetCategoryId"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task CheckExistByFixedAssetCategoryId(string fixedAssetCategoryId)
        {
            var fixedAssetCategoryEntity = await _fixedAssetCategoryRepository.FindAsync(fixedAssetCategoryId);
            if (fixedAssetCategoryEntity != null)
            {
                return;
            }
            throw new NotFoundException("Loại tài sản không tồn tại");

        }

        /// <summary>
        /// Kiểm tra ngày mua và ngày bắt đầu sử dụng
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task CheckPurchaseDateLaterUsingStartedDate(DateTime purchaseDate, DateTime usingStartDate)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
