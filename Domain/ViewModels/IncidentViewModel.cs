using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Domain.ViewModels;

public class IncidentViewModel
{ 
    [Required]
    [Range(0,63572375290155)]
    public long Latitude { get; set; }
    [Required]
    [Range(0, 106744840359415)]
    public long Longitude { get; set; }
    [Required]
    [DataType(DataType.MultilineText)]
    public string Commentary { get; set; } = null!;
    [Required]
    public IFormFile PhotoUri { get; set; } = null!;
}