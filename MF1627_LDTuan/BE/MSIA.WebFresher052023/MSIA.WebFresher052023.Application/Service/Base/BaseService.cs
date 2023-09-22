using Application.DTO;
using AutoMapper;
using Domain.Exceptions;
using MSIA.WebFresher052023.Application.Interface.Base;
using MSIA.WebFresher052023.Domain.Entity.Base;
using MSIA.WebFresher052023.Domain.Interface;
using MSIA.WebFresher052023.Domain.Interface.Manager.Base;
using MSIA.WebFresher052023.Domain.Interface.Repository.Base;
using MSIA.WebFresher052023.Domain.Model.Base;
using MSIA.WebFresher052023.Domain.Resource;
using MSIA.WebFresher052023.Domain.Result;

namespace MSIA.WebFresher052023.Application.Service.Base
{
    public abstract class BaseService<TEntity, TModel, TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntityUpdateMultiDto> : BaseReadOnlyService<TEntity, TModel, TEntityDto,
        TEntityCreateDto, TEntityUpdateDto, TEntityUpdateMultiDto>,
        IBaseService<TEntity, TModel, TEntityDto, TEntityCreateDto, TEntityUpdateDto, TEntityUpdateMultiDto> where TModel : IHasKeyModel where TEntity : IHasKey
    {
        #region Fields
        protected readonly IBaseRepository<TEntity, TModel> _baseRepository;
        protected readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<TEntity, TModel> baseRepository, IMapper mapper, IBaseManager<TEntity, TModel> manager, IUnitOfWork unitOfWork) : base(baseRepository, mapper, manager)
        {
            _baseRepository = baseRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Xử lý thêm mới một bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Thông tin của bản ghi</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<bool> InsertAsync(TEntityCreateDto entityCreateDto)
        {
            var model = _mapper.Map<TModel>(entityCreateDto);
            await _manager.CheckDuplicateCodeAsync(model.GetKey());
            var entity = _mapper.Map<TEntity>(entityCreateDto);
            bool result = await _baseRepository.InsertAsync(entity);
            return result;
        }

        /// <summary>
        /// Thêm mới nhiều bản ghi
        /// </summary>
        /// <param name="entityCreateDtos">Thông tin cần thêm mới</param>
        /// Created by: ldtuan (02/09/2023)
        public virtual async Task<ApiResponse> InsertMultiAsync(List<TEntityCreateDto> entityCreateDtos)
        {
            var listModels = _mapper.Map<List<TModel>>(entityCreateDtos);
            foreach (var model in listModels)
            {
                await _manager.CheckDuplicateCodeAsync(model.GetKey());
            }
            var listEntities = _mapper.Map<List<TEntity>>(listModels);
            bool result = await _baseRepository.InsertMultiAsync(listEntities);
            if (result)
            {
                return new ApiResponse { ErrorCode = 200, UserMessage = "Success" };
            }
            else
            {
                return new ApiResponse { UserMessage = "False" };
            }
        }

        /// <summary>
        /// Xử lý cập nhật một bản ghi đã tồn tại
        /// </summary>
        /// <param name="entityUpdateDto">Thông tin của bản ghi</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<bool> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            var oldEntity = await _baseRepository.GetAsync(id);
            var oldModel = _mapper.Map<TModel>(oldEntity);
            var model = _mapper.Map<TModel>(entityUpdateDto);
            await _manager.CheckDuplicateCodeAsync(model.GetKey(), oldModel.GetKey());
            var entity = _mapper.Map(entityUpdateDto, oldEntity);
            bool result = await _baseRepository.UpdateAsync(entity);
            return result;
        }

        /// <summary>
        /// Xử lý xóa một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);
            // GetAsync có throw exception nên k cần check lại
            bool result = await _baseRepository.DeleteAsync(entity);
            return result;
        }

        /// <summary>
        /// Xử lý xóa một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>True nếu thêm mới thành công, false nếu thêm mới thất bại</returns>
        /// Created by: ldtuan (17/07/2023)
        public virtual async Task<bool> DeleteManyAsync(List<Guid> ids)
        {
            var entities = await _baseRepository.GetListByIdsAsync(ids);
            if (ids.Count == 0 || entities.Count < ids.Count)
            {
                throw new DataException(ErrorMessages.Data);
            }
            var result = await _baseRepository.DeleteManyAsync(entities);
            return result;
        }

        /// <summary>
        /// Cập nhật nhiều bản ghi
        /// </summary>
        /// <param name="entityUpdateMultiDtos">Thông tin cần cập nhật</param>
        /// Created by: ldtuan (02/09/2023)
        public virtual Task<ApiResponse> UpdateMultiAsync(List<TEntityUpdateMultiDto> entityUpdateMultiDtos)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}

