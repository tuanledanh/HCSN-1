using Domain.Entity;
using Domain.Exceptions;
using Domain.Model;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface.Manager;
using MSIA.WebFresher052023.Domain.Interface.Repository;
using System.Security.Cryptography.X509Certificates;

namespace MSIA.WebFresher052023.Domain.Service
{
    public class TransferAssetManager : BaseManager<TransferAsset, TransferAssetModel>, ITransferAssetManager
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFixedAssetRepository _fixedAssetRepository;
        public TransferAssetManager(ITransferAssetRepository transferAssetRepository, IDepartmentRepository departmentRepository, IFixedAssetRepository fixedAssetRepository) : base(transferAssetRepository)
        {
            _departmentRepository = departmentRepository;
            _fixedAssetRepository = fixedAssetRepository;
        }

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
        /// Kiểm tra ngày điều chuyển có lớn hơn ngày chứng từ hay không
        /// </summary>
        /// <param name="transferAsset">Chứng từ</param>
        /// <exception cref="DataException">Lỗi data truyền về</exception>
        /// Created by: ldtuan (30/08/2023)
        public void CheckDate(TransferAsset? transferAsset)
        {
            if (transferAsset != null && transferAsset.TransferDate < transferAsset.TransactionDate)
            {
                throw new DataException();
            }
        }

        /// <summary>
        /// Kiểm tra tài sản và phòng ban  xem tồn tại trong db hay không
        /// </summary>
        /// <param name="listTransferAssetDetails">Danh sách chi tiết chứng từ</param>
        /// <exception cref="DataException">Lỗi dữ liệu truyền vào</exception>
        /// Created by: ldtuan (31/08/2023)
        public async Task CheckExistAsset(List<TransferAssetDetail> listTransferAssetDetails)
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
    }
}
