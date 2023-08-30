using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSIA.WebFresher052023.Application.Interface.Base;
using MSIA.WebFresher052023.Application.Service.Base;
using static Dapper.SqlMapper;

namespace MSIA.WebFresher052023.API.Controllers.Base
{
    public class BaseController<TEntity, TModel, TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntityUpdateMultiDto> : BaseReadOnlyController<TEntity, TModel, TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntityUpdateMultiDto>
    {
        #region Constructor
        public BaseController(IBaseService<TEntity, TModel, TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntityUpdateMultiDto> baseService, IMapper mapper) : base(baseService, mapper)
        {
        } 
        #endregion
    }
}
