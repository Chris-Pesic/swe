using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StarterApp.Database.Models;

public enum RentalRequestStatus
{
    Pending,
    Approved,
    Rejected
}

[Table("rental_requests")]
[PrimaryKey(nameof(Id))]
public class RentalRequest
{
    public int Id { get; set; }

    [Required]
    public int ItemId { get; set; }

    public Item Item { get; set; } = null!;

    [Required]
    public int RequesterId { get; set; }

    public User Requester { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public RentalRequestStatus Status { get; set; } = RentalRequestStatus.Pending;
}