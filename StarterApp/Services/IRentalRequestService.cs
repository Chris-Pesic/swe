using StarterApp.Database.Models;

namespace StarterApp.Database.Services;

public interface IRentalRequestService
{
    Task<RentalRequest> CreateRequestAsync(int itemId, int requesterId, DateTime startDate, DateTime endDate);

    Task<List<RentalRequest>> GetIncomingRequestsAsync(int ownerId);

    Task<List<RentalRequest>> GetOutgoingRequestsAsync(int requesterId);

    Task ApproveRequestAsync(int requestId, int ownerId);

    Task RejectRequestAsync(int requestId, int ownerId);
}