using Microsoft.EntityFrameworkCore;
using StarterApp.Database.Models;
using StarterApp.Database.Data;

namespace StarterApp.Database.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _context;

    public ItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Item>> GetAllAsync()
    {
        return await _context.Items.ToListAsync();
    }

    public async Task<List<Item>> GetAllWithOwnerAsync()
    {
        return await _context.Items
            .Include(i => i.Owner)
            .ToListAsync();
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        return await _context.Items.FindAsync(id);
    }

    public async Task<Item?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Items
            .Include(i => i.Owner)
            .Include(i => i.RentalRequests)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<List<Item>> GetByOwnerIdAsync(int ownerId)
    {
        return await _context.Items
            .Where(i => i.OwnerId == ownerId)
            .ToListAsync();
    }

    public async Task AddAsync(Item item)
    {
        await _context.Items.AddAsync(item);
    }

    public void Update(Item item)
    {
        _context.Items.Update(item);
    }

    public void Delete(Item item)
    {
        _context.Items.Remove(item);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}