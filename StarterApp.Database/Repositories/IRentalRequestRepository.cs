using StarterApp.Database.Models;

namespace StarterApp.Database.Repositories;

public interface IRentalRequestRepository
{
    Task<RentalRequest?> GetByIdAsync(int id);

    Task<List<RentalRequest>> GetIncomingRequestsAsync(int ownerId);
    Task<List<RentalRequest>> GetOutgoingRequestsAsync(int userId);

    Task AddAsync(RentalRequest request);

    void Update(RentalRequest request);
    void Delete(RentalRequest request);

    Task SaveChangesAsync();
}