using StarterApp.Database.Models;

namespace StarterApp.Database.Repositories;

public interface IItemRepository
{
    Task<List<Item>> GetAllAsync();
    Task<List<Item>> GetAllWithOwnerAsync();
    Task<Item?> GetByIdAsync(int id);
    Task<Item?> GetByIdWithDetailsAsync(int id);

    Task<List<Item>> GetByOwnerIdAsync(int ownerId);

    Task AddAsync(Item item);
    void Update(Item item);
    void Delete(Item item);

    Task SaveChangesAsync();
}