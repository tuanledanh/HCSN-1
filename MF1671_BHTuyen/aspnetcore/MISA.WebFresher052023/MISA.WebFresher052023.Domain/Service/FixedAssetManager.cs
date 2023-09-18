using MISA.WebFresher052023.Domain.Entity.FixedAsset;
using MISA.WebFresher052023.Domain.Enum;
using MISA.WebFresher052023.Domain.Exceptions;
using MISA.WebFresher052023.Domain.Interface.department;
using MISA.WebFresher052023.Domain.Interface.FixedAsset;
using MISA.WebFresher052023.Domain.Interface.FixedAssetCategory;
using MISA.WebFresher052023.Domain.Interface.TransferAsset;
using MISA.WebFresher052023.Domain.Resource;
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
        private readonly ITransferAssetRepository _transferAssetRepository;

        /// <summary>
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        private readonly IFixedAssetCategoryRepository _fixedAssetCategoryRepository;

        /// <summary>
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
        /// <param name="transferAssetRepository"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public FixedAssetManager(IFixedAssetRepository fixedAssetRepository, IFixedAssetCategoryRepository fixedAssetCategoryRepository, IDepartmentRepository departmentRespostory, ITransferAssetRepository transferAssetRepository) : base(fixedAssetRepository)
        {
            _fixedAssetCategoryRepository = fixedAssetCategoryRepository;
            _departmentRespostory = departmentRespostory;
            _transferAssetRepository = transferAssetRepository;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Message lỗi
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string MessageError { get; set; } = VietNamese.FixedAssetCodeConflict;

        #endregion

        #region Methods
        /// <summary>
        /// Kiểm tra Id bộ phận có tồn tại hay không
        /// </summary>
        /// <param name="departmentId">Mã phòng ban</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task CheckExistByDepartmentId(Guid departmentId)
        {
            var departmentEntity = await _departmentRespostory.FindAsync(departmentId);
            if (departmentEntity != null)
            {
                return;
            }
            throw new NotFoundException(VietNamese.DepartmentNotExist);
        }

        /// <summary>
        /// Kiểm tra Id tài sản có tồn tại hay không
        /// </summary>
        /// <param name="fixedAssetCategoryId">Mã loại tài sản</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task CheckExistByFixedAssetCategoryId(Guid fixedAssetCategoryId)
        {
            var fixedAssetCategoryEntity = await _fixedAssetCategoryRepository.FindAsync(fixedAssetCategoryId);
            if (fixedAssetCategoryEntity == null)
            {
                return;
            }
            throw new NotFoundException(VietNamese.FixedAssetCategoryNotExist);
        }

        /// <summary>
        /// Kiểm tra tài sản có tồn tại chứng từ phát sinh không
        /// </summary>
        /// <param name="fixedAssetId">Mã id của tài sản</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task CheckExistTransferAsset(List<Guid> fixedAssetIds, ActionMode actionMode)
        {
            var fixedTransferAssetInfo = await _transferAssetRepository.GetTransferAssetsByFixedAssetIdsAsync(fixedAssetIds);

            if (fixedTransferAssetInfo.TransferAssets.Any())
            {
                var errorMessage = string.Format(VietNamese.TransferAssetExist, fixedTransferAssetInfo.FixedAssetCode);

                errorMessage = string.Concat(errorMessage, " " ,actionMode == ActionMode.UPDATE ? VietNamese.NoUpdateFixedAsset : VietNamese.NoDeleteFixedAsset);
                    
                var transferAssetInfo = fixedTransferAssetInfo.TransferAssets.Select(transferAsset => string.Format(VietNamese.TransferAssetInfo, transferAsset.TransferAssetCode, transferAsset.TransferDate.ToString("dd/MM/yyyy"))).ToList();
                throw new UserException(errorMessage, transferAssetInfo);
            };
            return;
        }

        /// <summary>
        /// Kiểm tra ngày mua và ngày bắt đầu sử dụng
        /// </summary>
        /// <param name="purchaseDate">Ngày mua</param>
        /// <param name="usingStartDate">Ngày bắt đầu sử dụng</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public void CheckUsingStartedDateLaterPurchaseDate(DateTime purchaseDate, DateTime usingStartDate)
        {
            var check = purchaseDate > usingStartDate;
            if (check) throw new UserException(VietNamese.UsingStartedDateLaterPurchaseDate);
            return;
        }
        #endregion
    }
}
