using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class IncidentsController : ControllerBase
{
    private readonly IApplicationDbContext _applicationDbContext;

    public IncidentsController(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    private async Task<string> UploadImage(IFormFile file)
    {
        var specialId = Guid.NewGuid();
        
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Images", $"{specialId}-{file.FileName}");

        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);

        return filePath;
    }

    [HttpGet]
    public async Task<List<Incident>> GetAll()
    {
        return await _applicationDbContext.Incidents.ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AddIncident([FromForm] IncidentViewModel incidentViewModel)
    {
        var path = await UploadImage(incidentViewModel.PhotoUri);
        
        var newIncident = new Incident()
        {
            ActualPhotoUrl = path,
            Commentary = incidentViewModel.Commentary,
            Id = incidentViewModel.Id,
            Latitude = incidentViewModel.Latitude,
            Longitude = incidentViewModel.Longitude,
            PhotoUri = incidentViewModel.PhotoUri 
        };
        
        await _applicationDbContext.Incidents.AddAsync(newIncident);

        await _applicationDbContext.SaveChangesAsync(default);

        return Ok();
    }
}