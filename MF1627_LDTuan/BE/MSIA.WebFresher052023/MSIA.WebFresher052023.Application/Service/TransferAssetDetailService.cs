using Application.DTO;
using Application.Interface;
using AutoMapper;
using Domain.Exceptions;
using Domain.Model;
using MSIA.WebFresher052023.Application.Service.Base;
using MSIA.WebFresher052023.Domain.Entity;
using MSIA.WebFresher052023.Domain.Interface;
using MSIA.WebFresher052023.Domain.Interface.Manager;
using MSIA.WebFresher052023.Domain.Interface.Repository;

namespace Application.Service
{
    public class TransferAssetDetailService : BaseService<TransferAssetDetail, TransferAssetDetailModel, TransferAssetDetailDto, TransferAssetDetailCreateDto, TransferAssetDetailUpdateDto, TransferAssetDetailUpdateMultiDto>, ITransferAssetDetailService
    {
        #region Fields
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;
        private readonly IFixedAssetRepository _fixedAssetRepository;
        private readonly ITransferAssetDetailManager _transferAssetDetailManager;
        #endregion

        #region Constructor
        public TransferAssetDetailService(ITransferAssetDetailRepository transferAssetDetailRepository, IFixedAssetRepository fixedAssetRepository, IMapper mapper, ITransferAssetDetailManager transferAssetDetailManager, IUnitOfWork unitOfWork) : base(transferAssetDetailRepository, mapper, transferAssetDetailManager, unitOfWork)
        {
            _transferAssetDetailRepository = transferAssetDetailRepository;
            _fixedAssetRepository = fixedAssetRepository;
            _transferAssetDetailManager = transferAssetDetailManager;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Thêm mới chi tiết chứng từ
        /// </summary>
        /// <param name="transferAssetDetailCreateDto">Thông tin cần thêm mới</param>
        /// Created by: ldtuan (02/09/2023)
        public override async Task<bool> InsertAsync(TransferAssetDetailCreateDto transferAssetDetailCreateDto)
        {
            var entity = _mapper.Map<TransferAssetDetail>(transferAssetDetailCreateDto);
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            var id = Guid.NewGuid();
            entity.SetKey(id);
            bool result = await _transferAssetDetailRepository.InsertAsync(entity);
            return result;
        }
        #endregion
    }
}
