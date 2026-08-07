using StarterApp.Database.Models;

namespace StarterApp.Tests.Models;

public class UserTests
{
    [Fact]
    public void NewUser_ShouldHaveEmptyFirstName()
    {
        var user = new User();

        Assert.Equal(string.Empty, user.FirstName);
    }

    [Fact]
    public void NewUser_ShouldHaveEmptyLastName()
    {
        var user = new User();

        Assert.Equal(string.Empty, user.LastName);
    }

    [Fact]
    public void NewUser_ShouldHaveEmptyEmail()
    {
        var user = new User();

        Assert.Equal(string.Empty, user.Email);
    }

    [Fact]
    public void NewUser_ShouldBeActive()
    {
        var user = new User();

        Assert.True(user.IsActive);
    }

    [Fact]
    public void NewUser_ShouldHaveNoDeletedDate()
    {
        var user = new User();

        Assert.Null(user.DeletedAt);
    }

    [Fact]
    public void NewUser_ShouldHaveCurrentCreatedAt()
    {
        var before = DateTime.UtcNow;

        var user = new User();

        var after = DateTime.UtcNow;

        Assert.NotNull(user.CreatedAt);
        Assert.InRange(user.CreatedAt!.Value, before, after);
    }

    [Fact]
    public void NewUser_ShouldHaveCurrentUpdatedAt()
    {
        var before = DateTime.UtcNow;

        var user = new User();

        var after = DateTime.UtcNow;

        Assert.NotNull(user.UpdatedAt);
        Assert.InRange(user.UpdatedAt!.Value, before, after);
    }

    [Fact]
    public void NewUser_ShouldHaveEmptyUserRoles()
    {
        var user = new User();

        Assert.Empty(user.UserRoles);
    }

    [Fact]
    public void FullName_ShouldCombineFirstAndLastName()
    {
        var user = new User
        {
            FirstName = "Sarah",
            LastName = "Jones"
        };

        Assert.Equal("Sarah Jones", user.FullName);
    }

    [Fact]
    public void User_ShouldStorePasswordHash()
    {
        var user = new User
        {
            PasswordHash = "hashedPassword123"
        };

        Assert.Equal("hashedPassword123", user.PasswordHash);
    }

    [Fact]
    public void User_ShouldStorePasswordSalt()
    {
        var user = new User
        {
            PasswordSalt = "randomSalt456"
        };

        Assert.Equal("randomSalt456", user.PasswordSalt);
    }
}