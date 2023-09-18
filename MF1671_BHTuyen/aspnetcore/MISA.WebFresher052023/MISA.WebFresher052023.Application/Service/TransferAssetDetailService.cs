using AutoMapper;
using MISA.WebFresher052023.Application.Dto.TransferAssetDetail;
using MISA.WebFresher052023.Application.Interface;
using MISA.WebFresher052023.Domain.Entity;
using MISA.WebFresher052023.Domain.Entity.TransferAssetDetail;
using MISA.WebFresher052023.Domain.Exceptions;
using MISA.WebFresher052023.Domain.Interface.TransferAssetDetail;
using MISA.WebFresher052023.Domain.Model.TransferAssetDetail;
using MISA.WebFresher052023.Domain.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Service
{
    public class TransferAssetDetailService : ITransferAssetDetailService
    {
        #region Field
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;

        private readonly ITransferAssetDetailManager _transferAssetDetailManager;

        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="transferAssetDetailRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="transferAssetDetailManager"></param>
        /// Created By: Bùi Huy Tuyền
        public TransferAssetDetailService(ITransferAssetDetailRepository transferAssetDetailRepository, IMapper mapper, ITransferAssetDetailManager transferAssetDetailManager)
        {
            _transferAssetDetailRepository = transferAssetDetailRepository;
            _transferAssetDetailManager = transferAssetDetailManager;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Phân trang tài sản điều chuyển
        /// </summary>
        /// <param name="transferAssetDetailFilterDto">Các trường lọc</param>
        /// <returns>Danh sách tài sản điều chuyển</returns>
        /// Created By: Bùi Huy Tuyền 
        public async Task<TransferAssetDetailPagingDto> GetTransferAssetDetailPagingAsync(TransferAssetDetailFilterDto transferAssetDetailFilterDto)
        {
            var transferAssetDetailFilterModel = _mapper.Map<TransferAssetDetailFilterModel>(transferAssetDetailFilterDto);

            var transferAssetDetailPagingModel = await _transferAssetDetailRepository.GetTransferAssetDetailPagingAsync(transferAssetDetailFilterModel);

            var transferAssetDetailPagingDto = _mapper.Map<TransferAssetDetailPagingDto>(transferAssetDetailPagingModel);

            return transferAssetDetailPagingDto;
        }

        /// <summary>
        /// Tạo mới nhiều tài sản điều chuyển
        /// </summary>
        /// <param name="transferAssetDetailCreates">Danh sách các tài sản điều chuyển</param>
        /// <param name="TransferAssetId">Mã Id của chứng từ điều chuyển</param>
        /// <returns></returns>
        /// <exception cref="UserException">Ngoại lệ của người dùng</exception>
        /// Created By: Bùi Huy Tuyền
        public async Task CreateManyAsync(IEnumerable<TransferAssetDetailDto> transferAssetDetailCreates, Guid TransferAssetId)
        {

            // Thông báo lỗi khi danh sách rỗng
            if (!transferAssetDetailCreates.Any())
            {
                throw new UserException(VietNamese.EmptyList);
            }

            var transferAssetDetailEntities = _mapper.Map<IEnumerable<TransferAssetDetailEntity>>(transferAssetDetailCreates);

            // Kiểm tra phòng ban và phòng ban điều chuyển
            await _transferAssetDetailManager.CheckDepartmentAsync(transferAssetDetailEntities);

            foreach (var transferAssetDetail in transferAssetDetailEntities)
            {
                transferAssetDetail.SetKey(Guid.NewGuid());
                transferAssetDetail.SetTransferAssetId(TransferAssetId);
                if (transferAssetDetail is BaseAuditEntity baseAuditEntity)
                {
                    baseAuditEntity.CreatedDate = DateTime.Now;
                    baseAuditEntity.ModifiedDate = DateTime.Now;
                    baseAuditEntity.CreatedBy = VietNamese.Admin;
                    baseAuditEntity.ModifiedBy = VietNamese.Admin;
                }
            }

            await _transferAssetDetailRepository.CreateManyAsync(transferAssetDetailEntities);
        }

        /// <summary>
        /// Cập nhật nhiều tài sản điều chuyển
        /// </summary>
        /// <param name="transferAssetDetails">Danh sách tài sản điều chuyển</param>
        /// <returns></returns>
        /// <exception cref="UserException">Ngoại lệ của người dùng</exception>
        /// Created By: Bùi Huy Tuyền
        public async Task UpdateManyAsync(IEnumerable<TransferAssetDetailDto> transferAssetDetails)
        {

            // Thông báo lỗi khi danh sách rỗng
            if (!transferAssetDetails.Any())
            {
                throw new UserException(VietNamese.EmptyList);
            }

            var transferAssetDetailEntities = _mapper.Map<IEnumerable<TransferAssetDetailEntity>>(transferAssetDetails);

            await _transferAssetDetailManager.CheckDepartmentAsync(transferAssetDetailEntities);

            foreach (var receiver in transferAssetDetailEntities)
            {

                if (receiver is BaseAuditEntity baseAuditEntity)
                {
                    baseAuditEntity.ModifiedDate = DateTime.Now;
                    baseAuditEntity.ModifiedBy = VietNamese.Admin;
                }
            }

            await _transferAssetDetailRepository.UpdateManyAsync(transferAssetDetailEntities);
        }

        /// <summary>
        /// Xóa nhiều tài sản điều chuyển
        /// </summary>
        /// <param name="transferAssetDetailIds">Danh sách mã Id các tài sản cần xóa</param>
        /// <returns></returns>
        /// <exception cref="UserException">Ngoại lệ của người dùng</exception>
        public async Task DeleteManyAsync(List<Guid> transferAssetDetailIds)
        {
            // Thông báo lỗi khi truyền danh sách rỗng
            if (transferAssetDetailIds.Count == 0)
            {
                throw new UserException(VietNamese.EmptyList);
            }

            var transferAssetDetails = await _transferAssetDetailRepository.FindManyAsync(transferAssetDetailIds);

            // Thông báo lỗi khi có bản khi không tồn tại
            if (transferAssetDetails.Count() != transferAssetDetailIds.Count)
            {
                throw new UserException(VietNamese.NoDelete);
            }
            await _transferAssetDetailRepository.DeleteManyAsync(transferAssetDetails);
        }

        /// <summary>
        /// Lấy nhiều tất cả sản các chứng từ
        /// </summary>
        /// <param name="transferAssetIds">Danh sách mã Id của các chứng từ</param>
        /// <returns>Danh sách các tài sản</returns>
        /// Created By: Bùi Huy Tuyền
        public async Task<IEnumerable<TransferAssetDetailEntity>> GetManyByTransferAssetIdsAsync(List<Guid> transferAssetIds)
        {
            var transferAssetDetails = await _transferAssetDetailRepository.GetManyByTransferAssetIdsAsync(transferAssetIds);

            return transferAssetDetails;
        }

        /// <summary>
        /// Lấy tất cả tài sản của một chứng từ
        /// </summary>
        /// <param name="transferAssetId">Mã Id của chứng từ cần lấy</param>
        /// <returns>Danh sách tài sản</returns>
        /// Created By: Bùi Huy Tuyền
        public async Task<IEnumerable<TransferAssetDetailDto>> GetManyByTransferAssetIdAsync(Guid transferAssetId)
        {
            var transferAssetDetails = await _transferAssetDetailRepository.GetManyByTransferAssetIdAsync(transferAssetId);

            var transferAssetDetailDtos = _mapper.Map<IEnumerable<TransferAssetDetailDto>>(transferAssetDetails);

            return transferAssetDetailDtos;
        } 
        #endregion
    }
}
