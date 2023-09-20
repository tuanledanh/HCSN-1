using Domain.Entity;
using Domain.Exceptions;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Enum;
using MSIA.WebFresher052023.Domain.Exceptions;
using MSIA.WebFresher052023.Domain.Interface.Manager;
using MSIA.WebFresher052023.Domain.Interface.Repository;
using MSIA.WebFresher052023.Domain.Resource;

namespace MSIA.WebFresher052023.Domain.Service
{
    public class TransferAssetManager : BaseManager<TransferAsset, TransferAssetModel>, ITransferAssetManager
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFixedAssetRepository _fixedAssetRepository;
        private readonly ITransferAssetRepository _transferAssetRepository;
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;

        #endregion
        #region Constructor
        public TransferAssetManager(ITransferAssetRepository transferAssetRepository, ITransferAssetDetailRepository transferAssetDetailRepository, IDepartmentRepository departmentRepository, IFixedAssetRepository fixedAssetRepository) : base(transferAssetRepository)
        {
            _departmentRepository = departmentRepository;
            _fixedAssetRepository = fixedAssetRepository;
            _transferAssetRepository = transferAssetRepository;
            _transferAssetDetailRepository = transferAssetDetailRepository;
        }
        #endregion
        /// <summary>
        /// Kiểm tra chứng từ và chi tiết chứng từ có tồn tại hay không
        /// </summary>
        /// <param name="transferAsset">Chứng từ</param>
        /// <param name="listTransferAssetDetail">Danh sách tài sản chứng từ</param>
        /// <exception cref="DataException">Lỗi data truyền về</exception>
        /// Created by: ldtuan (30/08/2023)
        public void CheckNullRequest<T>(TransferAsset? transferAsset, List<T>? listTransferAssetDetail)
        {
            if (transferAsset == null || listTransferAssetDetail == null)
            {
                throw new DataException();
            }
        }

        /// <summary>
        /// Kiểm tra ngày điều chuyển có lớn hơn ngày chứng từ hay không, và ngày chứng điều chuyển cũng phải lớn hơn mọi ngày điều chuyển của các tài sản bên trong
        /// </summary>
        /// <param name="transferAsset">Chứng từ</param>
        /// <exception cref="DataException">Lỗi data truyền về</exception>
        /// Created by: ldtuan (30/08/2023)
        public async Task CheckDateAsync(TransferAsset? transferAsset, List<Guid> listAssetIds, ActionMode actionMode)
        {
            if (transferAsset != null && transferAsset.TransferDate < transferAsset.TransactionDate)
            {
                throw new ValidateException(ErrorMessagesTransferAsset.TransferSmallTransaction);
            }
            // Danh sách tài sản nhận được từ request truyền về
            var fixedAssetList = await _fixedAssetRepository.GetListByIdsAsync(listAssetIds);
            // Danh sách chi tiết chừng từ (đã tồn tài từ trước) trong DB của các tài sản này
            var detailList = await _transferAssetDetailRepository.GetListDetailByIdsAsync(listAssetIds);
            // Danh sách chứng từ (đã tồn tại từ trước) trong DB của các tài sản này
            var transferList = await _transferAssetRepository.GetNewestTransferAssetByAssetId(listAssetIds);
            if (transferList != null && transferList.Count > 0)
            {
                // Chứng từ mới nhất
                var newestTransfer = transferList.FirstOrDefault();
                // Trường hợp thêm mới, ngày điều chuyển của nó luôn phải lớn hơn chứng từ mới nhất trong DB
                if (transferAsset != null && actionMode == ActionMode.Create)
                {
                    if (newestTransfer != null && transferAsset.TransferDate <= newestTransfer.TransferDate)
                    {
                        var fixedAsset = await getFixedAsset(detailList, newestTransfer, fixedAssetList, listAssetIds);

                        throw new ValidateException(ErrorMessagesTransferAsset.InforDate(fixedAsset.FixedAssetCode, newestTransfer));
                    }
                }
                // Các trường hợp còn lại sẽ là cập nhật
                else if (transferAsset != null && actionMode == ActionMode.Update)
                {
                    // Trường hợp có sẵn duy nhất 1 chứng từ trong DB, thì khi cập nhật, bản thân thk cập nhật là mới nhất nên không cần làm gì

                    // Các trường hợp khác là khi có ít nhất 2 chứng từ trong DB trở lên
                    // Trường hợp bản thân thk cập nhật là mới nhất
                    if (newestTransfer != null && transferAsset.TransferAssetId == newestTransfer.TransferAssetId && transferList.Count > 1)
                    {
                        newestTransfer = transferList[1];
                        // Rồi so sánh date với nhau
                        if (transferAsset.TransferDate <= newestTransfer.TransferDate)
                        {
                            var fixedAsset = await getFixedAsset(detailList, newestTransfer, fixedAssetList, listAssetIds);

                            throw new ValidateException(ErrorMessagesTransferAsset.InforDate(fixedAsset.FixedAssetCode, newestTransfer));
                        }
                    }// Trường hợp thk cập nhật không phải là mới nhất
                    else if (newestTransfer != null && transferAsset.TransferAssetId != newestTransfer.TransferAssetId && transferList.Count > 1)
                    {
                        // Rồi so sánh date với nhau
                        if (transferAsset.TransferDate <= newestTransfer.TransferDate)
                        {
                            var fixedAsset = await getFixedAsset(detailList, newestTransfer, fixedAssetList, listAssetIds);

                            throw new ValidateException(ErrorMessagesTransferAsset.InforDate(fixedAsset.FixedAssetCode, newestTransfer));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Kiểm tra tài sản và phòng ban  xem tồn tại trong db hay không
        /// </summary>
        /// <param name="listTransferAssetDetails">Danh sách chi tiết chứng từ</param>
        /// <exception cref="DataException">Lỗi dữ liệu truyền vào</exception>
        /// Created by: ldtuan (31/08/2023)
        public async Task CheckExistAssetAsync(List<TransferAssetDetail> listTransferAssetDetails)
        {
            // Kiểm tra xem các tài sản có bị trùng và có tồn tại trong db không
            var uniqueFixedAsset = listTransferAssetDetails.Select(transfer => transfer.FixedAssetId).Distinct().ToList();
            var fixedAssetInDB = await _fixedAssetRepository.GetCountItemInListAsync(uniqueFixedAsset);
            if (uniqueFixedAsset.Count != listTransferAssetDetails.Count || fixedAssetInDB != listTransferAssetDetails.Count)
            {
                throw new DataException();
            }

            // Kiểm tra xem các phòng ban mới và cũ có tồn tại trong db hay không
            // Select many để gộp 2 tập hợp con old và new lại
            var uniqueDepartment = listTransferAssetDetails.SelectMany(transfer => new[] { transfer.OldDepartmentId, transfer.NewDepartmentId }).Distinct().ToList();
            var departmentsInDB = await _departmentRepository.GetCountItemInListAsync(uniqueDepartment);
            if (uniqueDepartment.Count != departmentsInDB)
            {
                throw new DataException();
            }
        }

        /// <summary>
        /// Lấy tài sản của chứng từ mà có chứng từ phát sinh mới nhất để hiển thị lên thông báo lỗi
        /// </summary>
        /// <param name="detailList">Danh sách chi tiết chứng từ</param>
        /// <param name="newestTransfer">Chứng từ mới nhất</param>
        /// <param name="fixedAssetList">Danh sách tài sản</param>
        /// <param name="listAssetIds">Danh sách id tài sản</param>
        /// Created by: ldtuan (09/09/2023)
        private async Task<FixedAsset> getFixedAsset(List<TransferAssetDetailModel> detailList, TransferAsset newestTransfer, List<FixedAsset> fixedAssetList ,List<Guid> listAssetIds)
        {
            // Lấy chi tiết chứng từ của chứng từ mới nhất này
            var filterDetails = detailList
                .Where(detail => detail.TransferAssetId == newestTransfer.TransferAssetId)
                .ToList();

            // Lấy các tài sản riêng biệt nằm trong chứng từ này
            var filterFixedAssets = fixedAssetList
                .Where(asset => filterDetails
                .Any(detail => detail.FixedAssetId == asset.FixedAssetId))
                .Distinct()
                .ToList();

            // Xem tài sản nào nằm trong chứng từ đang cần thêm này, chọn 1 cái để hiển thị thông báo lỗi
            var duplicateFixedAsset = filterFixedAssets
                .Where(asset => listAssetIds.Contains(asset.FixedAssetId))
                .OrderByDescending(asset => asset.ModifiedDate)
                .ToList();
            // Lấy mã code của tài sản này
            var fixedAsset = await _fixedAssetRepository.GetAsync(duplicateFixedAsset.FirstOrDefault().FixedAssetId);
            return fixedAsset;
        }
    }
}
