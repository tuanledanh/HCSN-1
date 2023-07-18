using Application.Interface;
using AutoMapper;
using Domain.Entity;
using Domain.Exceptions;
using Domain.Interface;

namespace Application.Service
{
    public class BaseService<TEntity, TModel, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IBaseService<TEntity, TModel, TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        #region Fields
        protected readonly IBaseRepository<TEntity, TModel> _baseRepository;
        protected readonly IMapper _mapper;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<TEntity, TModel> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gọi đến hàm GetAsync ở repository để xử lý lấy thông tin của bản ghi
        /// </summary>
        /// <param name="code">Mã code của bản ghi</param>
        /// <returns>Một bản ghi</returns>
        /// Created by: ldtuan (12/07/2023)
        public virtual async Task<TEntityDto?> GetAsync(string code)
        {
            var entity = await _baseRepository.FindAsync(code);
            var entityDto = _mapper.Map<TEntityDto>(entity);
            return entityDto;
        }

        /// <summary>
        /// Gọi đến hàm GetAllAsync ở repository để xử lý lấy danh sách toàn bộ bản ghi
        /// </summary>
        /// <returns>Toàn bộ bản ghi</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<List<TEntityDto>> GetAllAsync(int? pageNumber, int? pageLimit, string filterName)
        {
            List<TModel> entities;
            if (pageNumber == null && pageLimit == null)
            {
                entities = await _baseRepository.GetAllAsync();
            }
            else
            {
                if (filterName == null)
                {
                    filterName = "";
                }
                entities = await _baseRepository.GetFilterAsync(pageNumber, pageLimit, filterName);
            }
            List<TEntityDto> entitiesDto = new List<TEntityDto>();
            foreach (var entity in entities)
            {
                entitiesDto.Add(_mapper.Map<TEntityDto>(entity));
            }
            return entitiesDto;
        }

        /// <summary>
        /// Gọi đến hàm PostAsync ở repository để xử lý thêm mới một bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Thông tin của bản ghi</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<bool> InsertAsync(TEntityCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            // Validate
            throw new NotImplementedException();
        }
        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);
            if(entity == null)
            {
                throw new NotFoundException("Service: Can not found " + typeof(TEntity).Name);
            }
            bool result = await _baseRepository.DeleteAsync(entity);
            return result;
        } 
        #endregion
    }
}
