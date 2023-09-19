using MISA.WebFresher052023.Domain.Entity.FixedAssetCategory;
using MISA.WebFresher052023.Domain.Interface.FixedAssetCategory;
using MISA.WebFresher052023.Domain.Resource;
using MISA.WebFresher052023.Domain.Service.Base;

namespace MISA.WebFresher052023.Domain.Service
{
    public class FixedAssetCategoryManager : BaseManager<FixedAssetCategoryEntity>, IFixedAssetCategoryManager
    {
        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="fixedAssetCategoryRepository"></param>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public FixedAssetCategoryManager(IFixedAssetCategoryRepository fixedAssetCategoryRepository) : base(fixedAssetCategoryRepository)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Message lỗi
        /// </summary>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public override string MessageError { get; set; } = VietNamese.FixedAssetCategoryCodeConflict;
        #endregion
    }
}
