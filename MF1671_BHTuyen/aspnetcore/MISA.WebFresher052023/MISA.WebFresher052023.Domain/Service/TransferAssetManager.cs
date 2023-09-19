using MISA.WebFresher052023.Domain.Entity.TransferAsset;
using MISA.WebFresher052023.Domain.Enum;
using MISA.WebFresher052023.Domain.Exceptions;
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
    public class TransferAssetManager : BaseManager<TransferAssetEntity>, ITransferAssetManager
    {
        private readonly ITransferAssetRepository _transferAssetRepository;

        #region Constructor
        public TransferAssetManager(ITransferAssetRepository transferAssetRepository) : base(transferAssetRepository)
        {
            _transferAssetRepository = transferAssetRepository;
        }
        #endregion

        #region Field
        /// <summary>
        /// Thông báo lỗi khi trùng mã code
        /// </summary>
        public override string MessageError { get; set; } = VietNamese.TransferAssetCodeConflict;

        #endregion

        #region Methods

        /// <summary>
        /// Kiểm tra ngày điều chuyển khi thêm hoặc cập nhật một chứng từ điều chuyển
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách Id các tài sản điều chuyển</param>
        /// <param name="transferDate">Ngày điều chuyển chứng từ</param>
        /// <param name="transactionDate">Ngày chứng từ</param>
        /// <param name="transferAssetId">Mã id của chứng từ</param>
        /// <returns></returns>
        /// <exception cref="UserException"></exception>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task CheckTransferDateAsync(List<Guid> fixedAssetIds, DateTime transferDate, DateTime transactionDate, Guid? transferAssetId, ActionMode actionMode)
        {
            // Lấy ngày điều chuyển và ngày chứng từ dạng ngày tháng năm
            var transferDateOnly = DateOnly.FromDateTime(transferDate);
            var transactionDateOnly = DateOnly.FromDateTime(transactionDate);

            // Kiểm tra ngày điều chuyển có lớn hơn hoặc bằng ngày chứng từ hay không
            if (transferDateOnly < transactionDateOnly)
            {
                // Nếu ngày điều chuyển nhỏ hơn ngày chứng từ thì gửi thông báo lỗi cho client
                throw new UserException(VietNamese.TransferDateLaterTransactionDate);
            }

           

            var fixedAssetTransferInfo = await _transferAssetRepository.GetTransferAssetsByFixedAssetIdsAsync(fixedAssetIds, transferAssetId);

            var transferDateMin = fixedAssetTransferInfo.TransferDateMin;
            var transferDateMax = fixedAssetTransferInfo.TransferDateMax;
            var transferAssetCodeMin = fixedAssetTransferInfo.TransferAssetCodeMin;
            var transferAssetCodeMax = fixedAssetTransferInfo.TransferAssetCodeMax;

            // Trường hợp chứng từ điều chuyển ở giữa 
            if (transferDateMin != null && transferDateMax != null)
            {
                var transferDateOnlyMin = DateOnly.FromDateTime(transferDateMin.Value);
                var transferDateOnlyMax = DateOnly.FromDateTime(transferDateMax.Value);
                if (transferDateOnly >= transferDateOnlyMin || transferDateOnly <= transferDateOnlyMax)
                {
                    // Nếu ngày điều chuyển không nằm trong khoảng ngày điều chuyển của tài sản thì gửi thông báo lỗi cho client
                    throw new UserException(string.Format(VietNamese.TransferDateNotInRange, transferAssetCodeMin, transferDateOnlyMin.ToString("dd/MM/yyyy"), transferAssetCodeMax, transferDateOnlyMax.ToString("dd/MM/yyyy")));
                }
            }
            // Trường hợp chứng từ điều chuyển đầu tiên
            else if (transferDateMin != null)
            {
                var transferDateOnlyMin = DateOnly.FromDateTime(transferDateMin.Value);
                if (transferDateOnly >= transferDateOnlyMin)
                {
                    // Nếu ngày điều chuyển lớn hơn hoặc bằng ngày điểu chuyển của chứng từ ngay sau thì gửi thông báo lỗi cho client
                    throw new UserException(string.Format(VietNamese.TransferDateNotLessThanMin, transferAssetCodeMin, transferDateOnlyMin.ToString("dd/MM/yyyy")));
                }
            }
            // Trường hợp chứng từ điều chuyển cuối cùng
            else if (transferDateMax != null)
            {
                var transferDateOnlyMax = DateOnly.FromDateTime(transferDateMax.Value);
                if (transferDateOnly <= transferDateOnlyMax && actionMode == ActionMode.UPDATE)
                {
                    // Nếu ngày điều chuyển nhỏ hơn hoặc bằng ngày điểu chuyển của chứng từ ngay trước thì gửi thông báo lỗi cho client
                    var message = string.Format(VietNamese.TransferDateNotMoreThanMaxUpdate, transferAssetCodeMax, transferDateOnlyMax.ToString("dd/MM/yyyy"));
                    throw new UserException(message);


                }
                else if (transferDateOnly <= transferDateOnlyMax && actionMode == ActionMode.CREATE)
                {
                    var message = string.Format(VietNamese.TransferDateNotMoreThanMaxCreate, transferAssetCodeMax, transferDateOnlyMax.ToString("dd/MM/yyyy"));
                    throw new UserException(message);

                }

            }
        }


        /// <summary>
        /// Kiểm tra khi xóa một, nhiều chứng từ điều chuyển
        /// </summary>
        /// <param name="transferAssetIds">Danh sách chứng từ cần xóa</param>
        /// <returns></returns>
        /// <exception cref="UserException">Ngoại lệ user</exception>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task CheckDeleteManyTransferAssetAsync(List<Guid> transferAssetIds)
        {
            // Kiểm tra xem danh sách id rỗng
            if (transferAssetIds.Count == 0)
            {
                throw new UserException(VietNamese.EmptyList);
            }

            // Lấy ra danh sách chứng từ cần xóa
            var fixedAssets = await _transferAssetRepository.FindManyAsync(transferAssetIds);

            // Kiểm tra xem có chứng từ nào không tồn tại
            if (fixedAssets.Count() != transferAssetIds.Count)
            {
                throw new UserException(VietNamese.NoDelete);
            }

            // Kiểm tra có chứng từ phát sinh sau chứng từ cần xóa
            var fixedAssetTransferInfo = await _transferAssetRepository.GetTransferAssetsLaterAsync(transferAssetIds);

            // Nếu có trả lại lỗi về client
            if (fixedAssetTransferInfo.TransferAssets.Any())
            {
                var errorMessage = string.Format(VietNamese.NoDeleteTransferAsset, fixedAssetTransferInfo.TransferAssetCode, fixedAssetTransferInfo.TransferDate?.ToString("dd/MM/yyyy"));

                errorMessage = string.Concat(errorMessage, " ", string.Format(VietNamese.TransferAssetExist, fixedAssetTransferInfo.FixedAssetCode));

                var transferAssetInfo = fixedAssetTransferInfo.TransferAssets.Select(transferAsset => string.Format(VietNamese.TransferAssetInfo, transferAsset.TransferAssetCode, transferAsset.TransferDate.ToString("dd/MM/yyyy"))).ToList();

                throw new UserException(errorMessage, transferAssetInfo);
            }
        }
        #endregion

    }
}
