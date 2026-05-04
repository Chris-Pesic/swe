using Microsoft.EntityFrameworkCore;
using StarterApp.Database.Models;
using StarterApp.Database.Data;

namespace StarterApp.Database.Repositories;

public class RentalRequestRepository : IRentalRequestRepository
{
    private readonly AppDbContext _context;

    public RentalRequestRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RentalRequest?> GetByIdAsync(int id)
    {
        return await _context.RentalRequests
            .Include(r => r.Item)
            .Include(r => r.Requester)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<RentalRequest>> GetIncomingRequestsAsync(int ownerId)
    {
        return await _context.RentalRequests
            .Include(r => r.Item)
            .Include(r => r.Requester)
            .Where(r => r.Item.OwnerId == ownerId)
            .ToListAsync();
    }

    public async Task<List<RentalRequest>> GetOutgoingRequestsAsync(int userId)
    {
        return await _context.RentalRequests
            .Include(r => r.Item)
            .Where(r => r.RequesterId == userId)
            .ToListAsync();
    }

    public async Task AddAsync(RentalRequest request)
    {
        await _context.RentalRequests.AddAsync(request);
    }

    public void Update(RentalRequest request)
    {
        _context.RentalRequests.Update(request);
    }

    public void Delete(RentalRequest request)
    {
        _context.RentalRequests.Remove(request);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}