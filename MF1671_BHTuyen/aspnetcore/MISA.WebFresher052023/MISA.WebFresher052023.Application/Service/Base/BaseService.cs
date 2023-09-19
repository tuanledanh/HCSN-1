using AutoMapper;
using MISA.WebFresher052023.Application.Interface;
using MISA.WebFresher052023.Application.Interface.Base;
using MISA.WebFresher052023.Domain.Entity;
using MISA.WebFresher052023.Domain.Interface;
using MISA.WebFresher052023.Domain.Interface.Base;
using MISA.WebFresher052023.Domain.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Service.Base
{
    public abstract class BaseService<TEntity, TDto, TCreateDto, TUpdateDto> : BaseReadOnlyService<TEntity, TDto>, IBaseService<TDto, TCreateDto, TUpdateDto> where TEntity : IHasKeyEntity, IHasCodeEntity where TCreateDto : IHasCodeDto where TUpdateDto : IHasCodeDto
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        protected readonly IBaseRepository<TEntity> _baseRepository;
        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        protected readonly IBaseManager<TEntity> _baseManager;
        #endregion

        #region Constructors
        /// <summary>
        /// Hà khởi tạo
        /// </summary>
        /// <param name="baseRepository"></param>
        /// <param name="baseManager"></param>
        /// <param name="mapper"></param>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public BaseService(IBaseRepository<TEntity> baseRepository, IBaseManager<TEntity> baseManager,
            IMapper mapper) : base(baseRepository, mapper)
        {
            _baseRepository = baseRepository;
            _baseManager = baseManager;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tạo mới một Dto
        /// </summary>
        /// <param name="createDto">Dữ liệu cần tạo mới</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public virtual async Task CreateAsync(TCreateDto createDto)
        {
            // Check trùng mã code
            await _baseManager.CheckCodeConflictAsync(createDto.GetCode());

            var entity = _mapper.Map<TEntity>(createDto);

            entity.SetKey(Guid.NewGuid());

            if (entity is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.CreatedDate = DateTime.Now;
                baseAuditEntity.CreatedBy = VietNamese.Admin;
                baseAuditEntity.ModifiedDate = DateTime.Now;
                baseAuditEntity.ModifiedBy = VietNamese.Admin;
            }

            await _baseRepository.CreateAsync(entity);
        }

        /// <summary>
        /// Cập nhật một Dto
        /// </summary>
        /// <param name="dtoId">Mã Id của bản ghi cần cập nhật</param>
        /// <param name="updateDto">Dữ liệu cần cập nhật</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public virtual async Task UpdateAsync(Guid dtoId, TUpdateDto updateDto)
        {
            var entityOld = await _baseRepository.GetAsync(dtoId);

            if (entityOld.GetCode() != updateDto.GetCode())
            {
                await _baseManager.CheckCodeConflictAsync(updateDto.GetCode());
            }

            var entityUpdate = _mapper.Map<TEntity>(updateDto);

            entityUpdate.SetKey(dtoId);

            if (entityUpdate is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.ModifiedBy = VietNamese.Admin;
                baseAuditEntity.ModifiedDate = DateTime.Now;
            }

            await _baseRepository.UpdateAsync(entityUpdate);
        }

        /// <summary>
        /// Xóa một Dto
        /// </summary>
        /// <param name="dtoId">Mã Id của bản ghi cần xóa</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public virtual async Task DeleteAsync(Guid dtoId)
        {
            var entity = await _baseRepository.GetAsync(dtoId);
            await _baseRepository.DeleteAsync(entity);
        }
        #endregion
    }
}