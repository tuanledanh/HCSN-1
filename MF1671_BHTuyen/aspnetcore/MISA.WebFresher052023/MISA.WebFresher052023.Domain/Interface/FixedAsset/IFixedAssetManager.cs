﻿using MISA.WebFresher052023.Domain.Entity.FixedAsset;
using MISA.WebFresher052023.Domain.Enum;

namespace MISA.WebFresher052023.Domain.Interface.FixedAsset
{
    public interface IFixedAssetManager : IBaseManager<FixedAssetEntity>
    {
        #region Methods
        /// <summary>
        /// Kiểm tra Id loại tài sản có tồn tại hay không
        /// </summary>
        /// <param name="fixedAssetCategoryId">Mã id của loại tài sản</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task CheckExistByFixedAssetCategoryId(Guid fixedAssetCategoryId);

        /// <summary>
        /// Kiểm tra Id bộ phận có tồn tại hay không
        /// </summary>
        /// <param name="departmentId">mã Id của phòng ban</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task CheckExistByDepartmentId(Guid departmentId);

        /// <summary>
        /// Kiểm tra tài sản có tồn tại chứng từ phát sinh không
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách mã ID của các tài sản cần kiểm tra</param>
        /// <param name="actionMode">Chức năng</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public Task CheckExistTransferAsset(List<Guid> fixedAssetIds, ActionMode actionMode);

        /// <summary>
        /// Kiểm tra ngày mua và ngày bắt đầu sử dụng
        /// </summary>
        /// <param name="purchaseDate">Ngày mua</param>
        /// <param name="usingStartedDate">Ngày bắt đầu sử dụng</param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public void CheckUsingStartedDateLaterPurchaseDate(DateTime purchaseDate, DateTime usingStartedDate);
        #endregion
    }
}
