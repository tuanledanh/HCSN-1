using Domain.Entity;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Enum;
using MSIA.WebFresher052023.Domain.Interface.Manager.Base;

namespace MSIA.WebFresher052023.Domain.Interface.Manager
{
    public interface IFixedAssetManager : IBaseManager<FixedAsset, FixedAssetModel>
    {
        /// <summary>
        /// Kiểm tra tài sản có phát sinh chứng từ không
        /// </summary>
        /// <param name="assetIds">Danh sách id tài sản</param>
        /// <param name="action">Hành động như xóa hay cập nhật</param>
        /// <returns>Ném ra 1 ngoại lệ nếu có chứng từ phát sinh</returns>
        /// Created by: ldtuan (17/09/2023)
        Task CheckExistTransferAsync(List<Guid> assetIds, ActionMode action);

        /// <summary>
        /// Kiểm tra sự tồn tại của tài sản trong db
        /// </summary>
        /// <param name="assetIds">Danh sách id tài sản</param>
        /// <returns>Trả về lỗi nếu không tồn tại</returns>
        Task CheckExistFixedAssetAsync(List<Guid> assetIds);
    }
}
