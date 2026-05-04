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
    Requested,
    Rejected,
    Approved,
    OutForRent,
    Returned,
    Collected
}

public enum Rating
{
    OneStar = 1,
    TwoStars = 2,
    ThreeStars = 3,
    FourStars = 4,
    FiveStars = 5
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

    public Rating? Rating { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal DailyRate { get; set; }

    [Required]
    public ItemCategory Category { get; set; }

    [Required]
    public string Location { get; set; } = string.Empty;

    public ItemStatus? Status { get; set; }

    public int OwnerId { get; set; }

    public User Owner { get; set; } = null!;

    public ICollection<RentalRequest> RentalRequests { get; set; } = new List<RentalRequest>();
}