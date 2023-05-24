using Microsoft.Build.Framework;

namespace NetInformatika.Data.Tables;

public class Trip
{
    public Guid TripId { get; set; } = Guid.NewGuid();

    [Required]
    public string UserId { get; set; } = null!;

    [Required]
    public string StartLocation { get; set; } = null!;

    [Required]
    public string DestinationLocation { get; set; } = null!;

    [Required]
    public DateTime StartedOn { get; set; } = DateTime.UtcNow;

    public DateTime FinishedOn { get; set; } = DateTime.UtcNow;
}