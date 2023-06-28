namespace LibaryWebApi.Repository.Interfaces
{
    public interface IRepositoryBase<T> 
        where T : class
    {
        Task<List<T>> GetAllRecordsAsync();

        Task<T> GetRecordByIdAsync(int id);

        Task RemoveRecordAsync(T record);

        Task UpdateRecordAsync(T record);

        Task CreateRecordAsync(T record);

        Task SaveAsync();
    }
}
