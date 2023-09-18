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
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFixedAssetRepository _fixedAssetRepository;

        public TransferAssetDetailManager(ITransferAssetDetailRepository transferAssetDetailRepository, IDepartmentRepository departmentRepository, IFixedAssetRepository fixedAssetRepository)
        {
            _transferAssetDetailRepository = transferAssetDetailRepository;
            _departmentRepository = departmentRepository;
            _fixedAssetRepository = fixedAssetRepository;
        }

        public async Task CheckDepartmentAsync(IEnumerable<TransferAssetDetailEntity> transferAssetDetailEntities)
        {
            // Kiểm tra tài sản có tồn tại không
            var fixedAssetIds = transferAssetDetailEntities.Select(x => x.FixedAssetId).Distinct().ToList();

            var fixedAssets = await _fixedAssetRepository.FindManyFixedAssetAsync(fixedAssetIds);
            if (fixedAssets.Count() != fixedAssetIds.Count)
            {
                throw new NotFoundException(VietNamese.FixedAssetNotExist);
            }

            // Kiểm tra phòng ban có tồn tại không
            var departmentIds = transferAssetDetailEntities.Select(x => x.DepartmentId).Distinct().ToList();
            var transferDepartmentIds = transferAssetDetailEntities.Select(x => x.TransferDepartmentId).Distinct().ToList();

            var departments = await _departmentRepository.FindManyDepartmentAsync(departmentIds);

            var transferDepartments = await _departmentRepository.FindManyDepartmentAsync(transferDepartmentIds);
            if (departments.Count() != departmentIds.Count || transferDepartments.Count() != transferDepartmentIds.Count)
            {
                throw new NotFoundException(VietNamese.DepartmentNotExist);
            }

            // Kiểm tra phòng ban mới không được trùng phòng ban cũ
            foreach (var transferAssetDetailEntity in transferAssetDetailEntities)
            {
                if (transferAssetDetailEntity.DepartmentId == transferAssetDetailEntity.TransferDepartmentId)
                {
                    throw new ConflictException(VietNamese.TransferDepartmentDiffDepartment);
                }
            }
        }

        public async Task DeleteCheckerAsync(Guid FixedAssetIds, Guid TransferAssetId)
        {
            var transferAssetDetails = await _transferAssetDetailRepository.GetTransferAssetDetailLastetAsync(FixedAssetIds);

            if (transferAssetDetails != null && transferAssetDetails.TransferAssetId != TransferAssetId)
            {
                throw new Exception("Tài sản đã có phát sinh chứng từ");
            }
            return;
        }


    }
}
