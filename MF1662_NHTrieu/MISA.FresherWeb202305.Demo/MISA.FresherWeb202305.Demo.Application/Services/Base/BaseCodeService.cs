using AutoMapper;
using MISA.FresherWeb202305.Demo.Application.Interface.Base;
using MISA.FresherWeb202305.Demo.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.FresherWeb202305.Demo.Application.Services.Base
{
    public abstract class BaseCodeService<TEntity, TModel, TEntityDto, TEntityCreateDto, TEntityUpdateDto> :
        BaseService<TEntity, TModel, TEntityDto, TEntityCreateDto, TEntityUpdateDto>,
        IBaseCodeService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        protected readonly IBaseCodeRepository<TEntity, TModel> _baseCodeRepository;
        protected BaseCodeService(IBaseCodeRepository<TEntity, TModel> baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
            _baseCodeRepository = baseRepository;
        }

        public async Task<string?> FindNewCodeAsync()
        {
            var newCode=await _baseCodeRepository.FindNewCodeAsync();
            return newCode;
        }
    }
}
