using FinalProject.Models;

namespace FinalProject.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetByIdAsync(int id);
        Task<Category> GetByIdAsyncNoTracking(int id);
        //Task<IEnumerable<Supplier>> GetClubByCity(string city);
        bool Add(Category category);
        bool Update(Category category);
        bool Delete(Category category);
        bool Save();
    }
}
