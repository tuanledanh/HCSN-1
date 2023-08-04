using MISA.WebFresher052023.Domain.Entity.FixedAsset;

namespace MISA.WebFresher052023.Domain.Interface.FixedAsset
{
    public interface IFixedAssetManager : IBaseManager<FixedAssetEntity>
    {

        #region Tasks
        /// <summary>
        /// Kiểm tra Id loại tài sản có tồn tại hay không
        /// </summary>
        /// <param name="fixedAssetCategoryId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        Task CheckExistByFixedAssetCategoryId(string fixedAssetCategoryId);

        /// <summary>
        /// Kiểm tra Id bộ phận có tồn tại hay không
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        Task CheckExistByDepartmentId(string departmentId);

        /// <summary>
        /// Kiểm tra ngày mua và ngày bắt đầu sử dụng
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Task CheckPurchaseDateLaterUsingStartedDate(DateTime purchaseDate, DateTime usingStartedDate);

        #endregion
    }
}
