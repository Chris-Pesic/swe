using StarterApp.Database.Models;
using StarterApp.Database.Repositories;

namespace StarterApp.Database.Services;

public class RentalRequestService : IRentalRequestService
{
    private readonly IRentalRequestRepository _requestRepo;
    private readonly IItemRepository _itemRepo;

    public RentalRequestService(
        IRentalRequestRepository requestRepo,
        IItemRepository itemRepo)
    {
        _requestRepo = requestRepo;
        _itemRepo = itemRepo;
    }

    public async Task<RentalRequest> CreateRequestAsync(int itemId, int requesterId, DateTime startDate, DateTime endDate)
    {
        var item = await _itemRepo.GetByIdAsync(itemId);

        if (item == null)
            throw new Exception("Item not found");

        if (item.OwnerId == requesterId)
            throw new Exception("You cannot rent your own item");

        var request = new RentalRequest
        {
            ItemId = itemId,
            RequesterId = requesterId,
            StartDate = startDate,
            EndDate = endDate,
            Status = RentalRequestStatus.Pending
        };

        await _requestRepo.AddAsync(request);
        await _requestRepo.SaveChangesAsync();

        return request;
    }

    public async Task<List<RentalRequest>> GetIncomingRequestsAsync(int ownerId)
    {
        return await _requestRepo.GetIncomingRequestsAsync(ownerId);
    }

    public async Task<List<RentalRequest>> GetOutgoingRequestsAsync(int requesterId)
    {
        return await _requestRepo.GetOutgoingRequestsAsync(requesterId);
    }

    public async Task ApproveRequestAsync(int requestId, int ownerId)
    {
        var request = await _requestRepo.GetByIdAsync(requestId);

        if (request == null)
            throw new Exception("Request not found");

        if (request.Item.OwnerId != ownerId)
            throw new Exception("Not authorized to approve this request");

        request.Status = RentalRequestStatus.Approved;

        _requestRepo.Update(request);
        await _requestRepo.SaveChangesAsync();
    }

    public async Task RejectRequestAsync(int requestId, int ownerId)
    {
        var request = await _requestRepo.GetByIdAsync(requestId);

        if (request == null)
            throw new Exception("Request not found");

        if (request.Item.OwnerId != ownerId)
            throw new Exception("Not authorized to reject this request");

        request.Status = RentalRequestStatus.Rejected;

        _requestRepo.Update(request);
        await _requestRepo.SaveChangesAsync();
    }
}