
namespace HotListing.API.Contracts
{
    //this generic repository is responsible for our communication with database in our behalf
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int? id);
        Task<List<T>> GetAllAsync(); 
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        //we don't want the _context to be referenced in the controller
        Task <bool> Exists(int id);
    }


}
