using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarterApp.Database.Models;

public enum ItemCategory
{
    Electronics,
    Furniture,
    Vehicles,
    Tools,
    Clothing,
    Sports,
    Other
}

public enum ItemStatus
{
    Available,
    Reserved,
    Rented,
    Unavailable
}

[Table("items")]
[PrimaryKey(nameof(Id))]
public class Item
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    public ICollection<Review> Reviews { get; set; } = new List<Review>();

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal DailyRate { get; set; }

    [Required]
    public ItemCategory Category { get; set; }

    [Required]
    public string Location { get; set; } = string.Empty;

    public ItemStatus Status { get; set; } = ItemStatus.Available;

    public int OwnerId { get; set; }

    public User Owner { get; set; } = null!;

    public ICollection<RentalRequest> RentalRequests { get; set; } = new List<RentalRequest>();
}