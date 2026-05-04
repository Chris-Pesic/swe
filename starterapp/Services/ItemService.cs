using Microsoft.EntityFrameworkCore;
using StarterApp.Database.Data;
using StarterApp.Database.Models;

namespace StarterApp.Services;

/// @brief Service implementation for managing rental items
public class ItemService : IItemService
{
    private readonly AppDbContext _context;

    public ItemService(AppDbContext context)
    {
        _context = context;
    }

public async Task<IEnumerable<Item>> GetAllItemsAsync()
{
    return await _context.Items
        .AsNoTracking()
        .OrderByDescending(i => i.Id)
        .ToListAsync();
}

    public async Task AddItemAsync(Item item)
    {
        _context.Set<Item>().Add(item);
        await _context.SaveChangesAsync();
    }
}