using AutoMapper;
using MISA.WebFresher052023.Application.Interface;
using MISA.WebFresher052023.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.Application.Service.Base
{
    public abstract class BaseReadOnlyService<TEntity, TDto> : IBaseReadOnlyService<TDto>
    {
        #region Fields
        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        protected readonly IBaseReadOnlyRepository<TEntity> _baseReadOnlyRepository;
        /// <summary>
        /// 
        /// </summary>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        protected readonly IMapper _mapper;
        #endregion

        #region Constructors
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="baseReadOnlyRepository"></param>
        /// <param name="mapper"></param>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public BaseReadOnlyService(IBaseReadOnlyRepository<TEntity> baseReadOnlyRepository, IMapper mapper)
        {
            _baseReadOnlyRepository = baseReadOnlyRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả Dto
        /// </summary>
        /// <returns>Tất cả Dto</returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _baseReadOnlyRepository.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<TDto>>(entities);
            return dtos;
        }

        /// <summary>
        /// Lấy một Dto theo Id
        /// </summary>
        /// <param name="dtoId"></param>
        /// <returns>Dto</returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        public async Task<TDto> GetAsync(Guid dtoId)
        {
            var entity = await _baseReadOnlyRepository.GetAsync(dtoId);
            var dto = _mapper.Map<TDto>(entity);
            return dto;
        }
        #endregion
    }
}
