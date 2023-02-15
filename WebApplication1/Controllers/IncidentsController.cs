using System.Text.Json;
using Application.Common;
using Application.Interfaces;
using Domain.Entities;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    

    [HttpGet]
    public async Task<string> GetAll()
    {
        return JsonSerializer.Serialize(await _applicationDbContext.Incidents.ToListAsync());
    }

    [HttpPost]
    public async Task<string> AddIncident([FromForm] IncidentViewModel incidentViewModel)
    {
        var path = await Uploader.UploadImage(incidentViewModel.PhotoUri);
        
        var newIncident = new Incident
        {
            ActualPhotoUrl = path,
            Commentary = incidentViewModel.Commentary,
            Id = Guid.NewGuid(),
            Latitude = incidentViewModel.Latitude,
            Longitude = incidentViewModel.Longitude
        };
        
        await _applicationDbContext.Incidents.AddAsync(newIncident);

        await _applicationDbContext.SaveChangesAsync(default);

        return "Incident has been saved successfully";
    }
}