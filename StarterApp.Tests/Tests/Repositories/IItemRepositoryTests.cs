using Microsoft.EntityFrameworkCore;
using StarterApp.Database.Data;
using StarterApp.Database.Models;
using StarterApp.Database.Repositories;

namespace StarterApp.Tests.Repositories;

public class ItemRepositoryTests
{
    private readonly AppDbContext _context;
    private readonly ItemRepository _repository;

    public ItemRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _repository = new ItemRepository(_context);
    }


    [Fact]
    public async Task AddAsync_ShouldAddItem()
    {
        var item = new Item
        {
            Name = "Power Drill",
            Description = "Cordless drill",
            DailyRate = 5,
            Location = "Bradford",
            Category = ItemCategory.Tools
        };

        await _repository.AddAsync(item);
        await _repository.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(item.Id);

        Assert.NotNull(result);
        Assert.Equal("Power Drill", result.Name);
    }


    [Fact]
    public async Task GetAllAsync_ShouldReturnAllItems()
    {
        _context.Items.Add(new Item
        {
            Name = "Tent"
        });

        _context.Items.Add(new Item
        {
            Name = "Bike"
        });

        await _context.SaveChangesAsync();


        var result = await _repository.GetAllAsync();


        Assert.Equal(2, result.Count);
    }


    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenItemDoesNotExist()
    {
        var result = await _repository.GetByIdAsync(999);

        Assert.Null(result);
    }


    [Fact]
    public async Task Delete_ShouldRemoveItem()
    {
        var item = new Item
        {
            Name = "Hammer"
        };

        _context.Items.Add(item);
        await _context.SaveChangesAsync();


        _repository.Delete(item);
        await _repository.SaveChangesAsync();


        var result = await _repository.GetByIdAsync(item.Id);

        Assert.Null(result);
    }
}