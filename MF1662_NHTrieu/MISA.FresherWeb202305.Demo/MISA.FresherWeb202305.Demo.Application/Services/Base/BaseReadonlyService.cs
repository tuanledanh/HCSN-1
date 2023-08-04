using AutoMapper;
using MISA.FresherWeb202305.Demo.Application.Interface.Base;
using MISA.FresherWeb202305.Demo.Domain;
using MISA.FresherWeb202305.Demo.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Services
{
    public abstract class BaseReadonlyService<TEntity, TModel, TEntityDto> : IBaseReadonlyService<TEntityDto>
    {
        #region Fileds
        protected readonly IReadonlyRepository<TEntity, TModel> _readonlyRepository;
        protected readonly IMapper _mapper;

        #endregion

        #region Constructer
        protected BaseReadonlyService(IReadonlyRepository<TEntity, TModel> readonlyRepository, IMapper mapper)
        {
            _readonlyRepository = readonlyRepository;
            _mapper = mapper;
        }

        #endregion

        #region Methods
        public async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            var entitiesModel = await _readonlyRepository.GetAllAsync();
            var entityDtos = _mapper.Map<IEnumerable<TEntityDto>>(entitiesModel);
            return entityDtos;

        }

        public async Task<TEntityDto> GetByIdAsync(Guid entityId)
        {
            var entity = await _readonlyRepository.GetByIdAsync(entityId);

            var entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        } 
        #endregion
    }
}
