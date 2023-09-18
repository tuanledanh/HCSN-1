using Domain.Entity;
using Domain.Exceptions;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Enum;
using MSIA.WebFresher052023.Domain.Exceptions;
using MSIA.WebFresher052023.Domain.Interface.Manager;
using MSIA.WebFresher052023.Domain.Interface.Repository;
using MSIA.WebFresher052023.Domain.Resource;

namespace MSIA.WebFresher052023.Domain.Service
{
    public class FixedAssetManager : BaseManager<FixedAsset, FixedAssetModel>, IFixedAssetManager
    {
        private readonly ITransferAssetRepository _transferAssetRepository;
        private readonly IFixedAssetRepository _fixedAssetRepository;
        public FixedAssetManager(IFixedAssetRepository fixedAssetRepository, ITransferAssetRepository transferAssetRepository) : base(fixedAssetRepository)
        {
            _fixedAssetRepository = fixedAssetRepository;
            _transferAssetRepository = transferAssetRepository;
        }

        /// <summary>
        /// Kiểm tra tài sản có phát sinh chứng từ không
        /// </summary>
        /// <param name="assetIds">Danh sách id tài sản</param>
        /// <param name="action">Hành động như xóa hay cập nhật</param>
        /// <returns>Ném ra 1 ngoại lệ nếu có chứng từ phát sinh</returns>
        /// Created by: ldtuan (17/09/2023)
        public async Task CheckExistTransferAsync(List<Guid> assetIds, ActionMode action)
        {
            var transfer = await _transferAssetRepository.GetAllTransferAssetByAssetId(assetIds);
            foreach (var assetId in assetIds)
            {
                var existTransfers = transfer.Where(transfer => transfer.FixedAssetId == assetId).ToList();
                if (existTransfers.Any())
                {
                    var asset = await _fixedAssetRepository.GetAsync(assetId);

                    switch (action)
                    {
                        case ActionMode.Delete:
                            throw new ValidateException(ErrorMessagesTransferAsset.AriseAssetDelete(asset.FixedAssetCode), ErrorMessagesTransferAsset.Infor(existTransfers));
                        case ActionMode.Update:
                            throw new ValidateException(ErrorMessagesTransferAsset.AriseAssetUpdate(asset.FixedAssetCode), ErrorMessagesTransferAsset.Infor(existTransfers));
                    }
                }
            }
        }

        /// <summary>
        /// Kiểm tra sự tồn tại của tài sản trong db
        /// </summary>
        /// <param name="assetIds">Danh sách id tài sản</param>
        /// <returns>Trả về lỗi nếu không tồn tại</returns>
        public async Task CheckExistFixedAssetAsync(List<Guid> assetIds)
        {
            var fixedAssets = await _fixedAssetRepository.GetListByIdsAsync(assetIds);
            if (!fixedAssets.Any() || fixedAssets.Count != assetIds.Count)
            {
                throw new DataException(ErrorMessages.Data);
            }
        }
    }
}
