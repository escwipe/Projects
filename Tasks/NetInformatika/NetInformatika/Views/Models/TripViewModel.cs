using System.ComponentModel.DataAnnotations;

namespace NetInformatika.Views.Models;

public class TripViewModel
{
    [Required]
    public string StartLocation { get; set; } = null!;

    [Required]
    public string DestinationLocation { get; set; } = null!;

    [Required]
    public string StartedOn { get; set; } = null!;
}
