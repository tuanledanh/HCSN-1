using AutoMapper;
using MSIA.WebFresher052023.DL_Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher052023.BL_Services.Service
{
    public class BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IBaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gọi đến hàm GetAsync ở repository để xử lý lấy thông tin của bản ghi
        /// </summary>
        /// <param name="code">Mã code của bản ghi</param>
        /// <returns>Một bản ghi</returns>
        /// Created by: ldtuan (12/07/2023)
        public virtual async Task<TEntityDto> GetAsync(string code)
        {
            var entity = await _baseRepository.GetAsync(code);
            if (entity == null)
            {
                throw new Exception("Service: Không tìm thấy bản ghi " + typeof(TEntity).Name);
            }
            var entityDto = _mapper.Map<TEntityDto>(entity);
            return entityDto;
        }

        /// <summary>
        /// Gọi đến hàm GetAllAsync ở repository để xử lý lấy danh sách toàn bộ bản ghi
        /// </summary>
        /// <returns>Toàn bộ bản ghi</returns>
        /// Created by: ldtuan (12/07/2023)
        public virtual async Task<List<TEntityDto>> GetAllAsync()
        {
            var entities = await _baseRepository.GetAllAsync();
            if (entities.Count() < 0)
            {
                throw new Exception("Service: Không tìm thấy danh sách bản ghi " + typeof(TEntity).Name);
            }
            List<TEntityDto> entitiesDto = new List<TEntityDto>();
            foreach (var entity in entities)
            {
                entitiesDto.Add(_mapper.Map<TEntityDto>(entity));
            }
            return entitiesDto;
        }

        /// <summary>
        /// Gọi đến hàm GetFilterAsync ở repository để xử lý lấy danh cách bản ghi có phân trang và tìm kiếm
        /// </summary>
        /// <param name="pageNumber">Sô trang</param>
        /// <param name="pageLimit">Giới hạn bản ghi ở mỗi trang</param>
        /// <param name="filterName">Từ khóa để tìm kiếm</param>
        /// <returns>Danh sách bản ghi sau khi phân trang và tìm kiếm</returns>
        /// Created by: ldtuan (12/07/2023)
        public virtual async Task<List<TEntityDto>> GetFilterAsync(int? pageNumber, int? pageLimit, string? filterName)
        {
            var entities = await _baseRepository.GetFilterAsync(pageNumber, pageLimit, filterName);
            if (entities.Count() < 0)
            {
                throw new Exception("Service: Không tìm thấy danh sách bản ghi " + typeof(TEntity).Name);
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
        /// <param name="entity">Thông tin của bản ghi</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (12/07/2023)
        public virtual async Task<bool> PostAsync(TEntityCreateDto entityCreateDto)
        {
            var entity = _mapper.Map<TEntity>(entityCreateDto);
            bool result = await _baseRepository.PostAsync(entity);
            if (!result)
            {
                throw new Exception("Service: Không thể thêm mới " + typeof(TEntity).Name);
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Gọi đến hàm PutAsync ở repository để xử lý cập nhật thông tin của bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi cần cập nhật</param>
        /// <param name="entity">Thông tin mới của bản ghi</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (12/07/2023)
        public virtual async Task<bool> PutAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            //var oldEntity = await _baseRepository.GetAsync(id);
            //if (oldEntity == null)
            //{
            //    throw new Exception("Service: Không tìm thấy bản ghi có id này để xóa: " + id);
            //}
            var entity = _mapper.Map<TEntity>(entityUpdateDto);
            bool result = await _baseRepository.PutAsync(id, entity);
            if (!result)
            {
                throw new Exception("Service: Không thể cập nhật " + typeof(TEntity).Name);
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Gọi đến hàm DeleteAsync ở repository để xử lý xóa thông tin một bản ghi thông qua mã code
        /// </summary>
        /// <param name="code">Mã code của bản ghi cần xóa</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (12/07/2023)
        public virtual async Task<bool> DeleteAsync(string code)
        {
            throw new NotImplementedException();
        }
    }
}
