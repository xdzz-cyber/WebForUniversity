using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class Incident
{
    public Guid Id { get; set; }

    public long Latitude { get; set; }

    public long Longitude { get; set; }

    public string Commentary { get; set; } = null!;

    [NotMapped]
    public IFormFile PhotoUri { get; set; } = null!;

    public string? ActualPhotoUrl { get; set; }
}