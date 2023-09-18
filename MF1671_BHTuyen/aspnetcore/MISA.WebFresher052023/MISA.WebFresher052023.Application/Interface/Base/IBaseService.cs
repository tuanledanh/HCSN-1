namespace MISA.WebFresher052023.Application.Interface
{
    public interface IBaseService<TDto, TCreateDto, TUpdateDto> : IBaseReadOnlyService<TDto>
    {

        #region Tasks
        /// <summary>
        /// Tạo mới một Dto
        /// </summary>
        /// <param name="dtoCreate">DtoUpdate</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        Task CreateAsync(TCreateDto dtoCreate);

        /// <summary>
        /// Cập nhật một Dto
        /// </summary>
        /// <param name="dtoId">ID của Dto</param>
        /// <param name="dtoUpdate">DtoUpdate</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        Task UpdateAsync(Guid dtoId, TUpdateDto dtoUpdate);

        /// <summary>
        /// Xóa Dto
        /// </summary>
        /// <param name="dtoId">ID của Dto</param>
        /// <returns></returns>
        /// Created By: Bùi Huy Tuyền (18/07/2023)
        Task DeleteAsync(Guid dtoId);
        #endregion
    }
}
