using AutoMapper;
using MISA.WebFresher052023.Application.Dto.Base;
using MISA.WebFresher052023.Application.Interface;
using MISA.WebFresher052023.Domain.Entity;
using MISA.WebFresher052023.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Service.Base
{
    public abstract class BaseService<TEntity, TDto, TCreateDto, TUpdateDto> : BaseReadOnlyService<TEntity, TDto>
, IBaseService<TDto, TCreateDto, TUpdateDto> where TEntity : IHasKeyEntity where TCreateDto : IHasKeyDto where TUpdateDto : IHasKeyDto
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
        protected BaseService(IBaseRepository<TEntity> baseRepository, IBaseManager<TEntity> baseManager,
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
        /// <param name="createDto">CreateDto</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public virtual async Task CreateAsync(TCreateDto createDto)
        {
            // Check trùng mã code
            await _baseManager.CheckExistByCode(createDto.GetKeyCode());

            var entity = _mapper.Map<TEntity>(createDto);

            entity.SetKeyId(Guid.NewGuid().ToString());

            if (entity is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.CreatedDate = DateTime.Now;
                baseAuditEntity.CreatedBy = "BHTuyen";
                baseAuditEntity.ModifiedDate = DateTime.Now;
                baseAuditEntity.ModifiedBy = "BHTuyen";
            }

            await _baseRepository.CreateAsync(entity);
        }

        /// <summary>
        /// Cập nhật một Dto
        /// </summary>
        /// <param name="dtoId">DtoId</param>
        /// <param name="updateDto">UpdateDto</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public virtual async Task UpdateAsync(string dtoId, TUpdateDto updateDto)
        {
            var entityOld = await _baseRepository.GetAsync(dtoId);

            if (entityOld.GetKeyCode() != updateDto.GetKeyCode())
            {
                await _baseManager.CheckExistByCode(updateDto.GetKeyCode());
            }

            var entityUpdate = _mapper.Map<TEntity>(updateDto);

            entityUpdate.SetKeyId(dtoId);

            if (entityUpdate is BaseAuditEntity baseAuditEntity)
            {
                baseAuditEntity.ModifiedBy = "BHTuyen";
                baseAuditEntity.ModifiedDate = DateTime.Now;
            }


            await _baseRepository.UpdateAsync(entityUpdate);
        }

        /// <summary>
        /// Xóa một Dto
        /// </summary>
        /// <param name="dtoId">DtoId</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public async Task DeleteAsync(string dtoId)
        {
            var entity = await _baseRepository.GetAsync(dtoId);
            await _baseRepository.DeleteAsync(entity);
        }
        #endregion
    }
}
