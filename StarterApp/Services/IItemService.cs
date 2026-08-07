using StarterApp.Database.Models;

namespace StarterApp.Services;

/// @brief Service interface for managing rental items
public interface IItemService
{
    /// @brief Retrieves all available items
    /// @return A collection of items
    Task<IEnumerable<Item>> GetAllItemsAsync();

    /// @brief Adds a new item to the data source
    /// @param item The item to add
    Task AddItemAsync(Item item);
}