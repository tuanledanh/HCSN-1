using MISA.WebFresher052023.Domain.Entity.TransferAssetDetail;
using MISA.WebFresher052023.Domain.Exceptions;
using MISA.WebFresher052023.Domain.Interface.department;
using MISA.WebFresher052023.Domain.Interface.FixedAsset;
using MISA.WebFresher052023.Domain.Interface.TransferAssetDetail;
using MISA.WebFresher052023.Domain.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Domain.Service
{
    public class TransferAssetDetailManager : ITransferAssetDetailManager
    {
        #region Field
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;

        private readonly IDepartmentRepository _departmentRepository;

        private readonly IFixedAssetRepository _fixedAssetRepository;
        #endregion

        #region Constructor
        public TransferAssetDetailManager(ITransferAssetDetailRepository transferAssetDetailRepository, IDepartmentRepository departmentRepository, IFixedAssetRepository fixedAssetRepository)
        {
            _transferAssetDetailRepository = transferAssetDetailRepository;
            _departmentRepository = departmentRepository;
            _fixedAssetRepository = fixedAssetRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Kiểm tra phòng ban khi thêm hoặc sửa tài sản điều chuyển
        /// </summary>
        /// <param name="transferAssetDetails">Danh sách tài sản điều chuyển</param>
        /// <returns></returns>
        /// <exception cref="UserException">Ngoại lên người dùng</exception>
        /// <exception cref="NotFoundException">Ngoại lệ không tồn tại</exception>
        /// <exception cref="ConflictException">Ngoại lệ xung đột</exception>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task CheckDepartmentAsync(IEnumerable<TransferAssetDetailEntity> transferAssetDetails)
        {
            // Thông báo lỗi khi danh sách rỗng
            if (!transferAssetDetails.Any())
            {
                throw new UserException(VietNamese.EmptyList);
            }

            // Kiểm tra tài sản có tồn tại không
            var fixedAssetIds = transferAssetDetails.Select(x => x.FixedAssetId).Distinct().ToList();

            var fixedAssets = await _fixedAssetRepository.FindManyFixedAssetAsync(fixedAssetIds);

            if (fixedAssets.Count() != fixedAssetIds.Count)
            {
                // Có tài sản không tồn tại
                throw new NotFoundException(VietNamese.FixedAssetNotExist);
            }

            // Kiểm tra phòng ban có tồn tại không
            var departmentIds = transferAssetDetails.Select(x => x.DepartmentId).Distinct().ToList();

            var transferDepartmentIds = transferAssetDetails.Select(x => x.TransferDepartmentId).Distinct().ToList();

            var departments = await _departmentRepository.FindManyAsync(departmentIds);

            var transferDepartments = await _departmentRepository.FindManyAsync(transferDepartmentIds);

            if (departments.Count() != departmentIds.Count || transferDepartments.Count() != transferDepartmentIds.Count)
            {
                // Có phòng ban không tồn tại
                throw new NotFoundException(VietNamese.DepartmentNotExist);
            }

            // Kiểm tra phòng ban mới không được trùng phòng ban cũ
            foreach (var transferAssetDetail in transferAssetDetails)
            {
                if (transferAssetDetail.DepartmentId == transferAssetDetail.TransferDepartmentId)
                {
                    throw new ConflictException(VietNamese.TransferDepartmentDiffDepartment);
                }
            }
        }

        /// <summary>
        /// Kiểm tra khi xóa nhiều tài sản điều chuyển
        /// </summary>
        /// <param name="transferAssetDetailIds">Danh sách ID tài sản điều chuyển cần xóa</param>
        /// <returns></returns>
        /// <exception cref="UserException"></exception>
        /// Created By: Bùi Huy Tuyền (19/07/2023)
        public async Task CheckDeleteManyAsync(List<Guid> transferAssetDetailIds)
        {
            // Thông báo lỗi khi truyền danh sách rỗng
            if (transferAssetDetailIds.Count == 0)
            {
                throw new UserException(VietNamese.EmptyList);
            }

            var transferAssetDetails = await _transferAssetDetailRepository.FindManyAsync(transferAssetDetailIds);

            // Thông báo lỗi khi có bản khi không tồn tại
            if (transferAssetDetailIds.Count != transferAssetDetails.Count())
            {
                throw new UserException(VietNamese.NoDelete);
            }
        } 
        #endregion


    }
}
