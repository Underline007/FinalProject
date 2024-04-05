using CustomIdentity.Models;
using FinalProject.Models;

namespace FinalProject.Interface
{
    public interface IDashboardRepository
    {
        Task<List<Category>> GetAllCategories();
        Task<List<Supplier>> GetAllSuppliers();
        Task<List<Product>> GetAllProducts();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
