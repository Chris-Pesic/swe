using StarterApp.Database.Models;

namespace StarterApp.Tests.Models;

public class ItemTests
{
    [Fact]
    public void NewItem_ShouldHaveAvailableStatus()
    {
        var item = new Item();

        Assert.Equal(ItemStatus.Available, item.Status);
    }

    [Fact]
    public void NewItem_ShouldStartWithNoRentalRequests()
    {
        var item = new Item();

        Assert.Empty(item.RentalRequests);
    }

    [Fact]
    public void NewItem_ShouldStartWithNoReviews()
    {
        var item = new Item();

        Assert.Empty(item.Reviews);
    }
}