using AutoMapper;
using MISA.FresherWeb202305.Demo.Application.Interface.Base;
using MISA.FresherWeb202305.Demo.Domain;
using MISA.FresherWeb202305.Demo.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Services.Base
{
    public abstract class BaseService<TEntity, TModel, TEntityDto, TEntityCreateDto, TEntityUpdateDto> :
        BaseReadonlyService<TEntity, TModel, TEntityDto>, IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>


    {
        protected readonly IBaseRepository<TEntity, TModel> _baseRepository;

        protected BaseService(IBaseRepository<TEntity, TModel> baseRepository, IMapper mapper)
            : base(baseRepository, mapper)
        {
            _baseRepository = baseRepository;
        }

        public async Task<int> DeleteAsync(List<Guid> entityIds)

        {
            if (entityIds.Count==0)
            {
                throw new NotFoundException("Không được truyền danh sách rỗng");
            }
            var entitiesMulplite = await _baseRepository.DeleteAsyncMuliplute(entityIds);
            return entitiesMulplite;

        }

        public async Task<int> DeleteByIdAsync(Guid entityId)
        {
            // Check đối tượng có tồn tại hay không
            var existEntity = await _baseRepository.GetByIdAsync(entityId);

            var result = await _baseRepository.DeleteAsync(existEntity);

            return result;
        }

        public virtual async Task<int> InsertAsync(TEntityCreateDto entityCreateDto)
        {
            var entity = await MapCreateDtoToEntityAsync(entityCreateDto);

            var result = await _baseRepository.CreateAsync(entity);

            return result;
        }

        public virtual async Task<int> UpdateAsync(Guid entityId, TEntityUpdateDto entityUpdateDto)
        {
            var entity = await MapUpdateDtoToEntityAsync(entityId, entityUpdateDto);

            var result = await _baseRepository.UpdateAsync(entity);

            return result;
        }
        /// <summary>
        /// Validate nghiệp vụ cho Insert
        /// </summary>
        /// <param name="entityCreateDto">CreateDto</param>
        /// <returns>Entity</returns>
        /// CreatedBy: txphuc (18/07/2023)
        protected abstract Task<TEntity> MapCreateDtoToEntityAsync(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Validate nghiệp vụ cho Update
        /// </summary>
        /// <param name="entityUpdateDto">UpdateDto</param>
        /// <returns>Entity</returns>
        /// CreatedBy: txphuc (18/07/2023)
        protected abstract Task<TEntity> MapUpdateDtoToEntityAsync(Guid entityId, TEntityUpdateDto entityUpdateDto);
    }
}
