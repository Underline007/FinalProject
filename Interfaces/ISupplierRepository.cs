using FinalProject.Models;

namespace FinalProject.Interface
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAll();
        Task<Supplier> GetByIdAsync(int id);
        Task<Supplier> GetByIdAsyncNoTracking(int id);
        //Task<IEnumerable<Supplier>> GetClubByCity(string city);
        bool Add(Supplier supplier);
        bool Update(Supplier supplier);
        bool Delete(Supplier supplier);
        bool Save();
    }
}
