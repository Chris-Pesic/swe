using StarterApp.Database.Models;

namespace StarterApp.Tests.Models;

public class ReviewTests
{
    [Fact]
    public void NewReview_ShouldHaveDefaultCreatedAt()
    {
        var before = DateTime.UtcNow;

        var review = new Review();

        var after = DateTime.UtcNow;

        Assert.InRange(review.CreatedAt, before, after);
    }

    [Fact]
    public void Review_ShouldStoreComment()
    {
        var review = new Review();

        review.Comment = "Great item!";

        Assert.Equal("Great item!", review.Comment);
    }

    [Fact]
    public void Review_ShouldAllowNullComment()
    {
        var review = new Review();

        Assert.Null(review.Comment);
    }

    [Fact]
    public void Review_ShouldStoreItemId()
    {
        var review = new Review();

        review.ItemId = 12;

        Assert.Equal(12, review.ItemId);
    }

    [Fact]
    public void Review_ShouldStoreUserId()
    {
        var review = new Review();

        review.UserId = 7;

        Assert.Equal(7, review.UserId);
    }

    [Fact]
    public void Review_ShouldAssignItemReference()
    {
        var item = new Item { Name = "Power Drill" };
        var review = new Review();

        review.Item = item;

        Assert.Same(item, review.Item);
    }

    [Fact]
    public void Review_ShouldAssignUserReference()
    {
        var user = new User();
        var review = new Review();

        review.User = user;

        Assert.Same(user, review.User);
    }
}