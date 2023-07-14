namespace qlts_api.Repository
{
    public interface IGenericRepository<TResponse, TRequest, Key>
    {
        Task<IEnumerable<TResponse>> GetAllAsync();
        Task<TResponse> Get(Key id);
        Task<TResponse> CreateAsync(TRequest request);
        Task Update(Key id, TRequest value);
        Task Delete(Key id);
    }
}
