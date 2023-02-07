namespace WebApplication1.ViewModels;

public class IncidentViewModel
{
    public Guid Id { get; set; }

    public long Latitude { get; set; }

    public long Longitude { get; set; }

    public string Commentary { get; set; } = null!;
    public IFormFile PhotoUri { get; set; } = null!;
}