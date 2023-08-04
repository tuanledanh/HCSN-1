namespace MISA.WebFresher052023.Application.Interface
{
    public interface IBaseReadOnlyService<TDto>
    {
        #region Tasks
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2021)
        Task<IEnumerable<TDto>> GetAllAsync();

        /// <summary>
        /// Lấy một bản ghi theo Id
        /// </summary>
        /// param name="dtoId">Id của Dto</param>
        /// <returns>Một cả bản ghi</returns>
        /// Created By: Bùi Huy Tuyền (19/07/2021)
        Task<TDto> GetAsync(string dtoId); 
        #endregion

    }
}
