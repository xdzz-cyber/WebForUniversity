using Application.Common;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WebApplication2.Controllers;

[ApiController]
[Route("[controller]")]

public class JsonRpcController : ControllerBase
{
    private readonly IApplicationDbContext _applicationDbContext;

    public JsonRpcController(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    
    [HttpPost]
    public async Task<string> AddIncident([FromBody] JObject jsonRpcObject)
    {
        var photoBase64 = jsonRpcObject["params"]?["photoUri"]?.ToObject<string>();

        var photoBytes = Convert.FromBase64String(photoBase64!);

        var photoName = jsonRpcObject["params"]?["photoName"]?.ToObject<string>();
        
        var photoFileName = jsonRpcObject["params"]?["photoFileName"]?.ToObject<string>();
    
        var path = await Uploader.UploadImage(new FormFile(new MemoryStream(photoBytes), 0, photoBytes.Length, photoName!, photoFileName!));

        var newIncident = new Incident
        {
            ActualPhotoUrl = path,
            Commentary = jsonRpcObject["params"]?["commentary"]?.ToString()!,
            Id = Guid.NewGuid(),
            Latitude = (long)jsonRpcObject["params"]?["latitude"]!,
            Longitude = (long)jsonRpcObject["params"]?["longitude"]!
        };
        
        await _applicationDbContext.Incidents.AddAsync(newIncident);
        
        await _applicationDbContext.SaveChangesAsync(default);
        
        return JObject.FromObject(new
        {
            jsonrpc = jsonRpcObject["jsonrpc"]?.ToObject<string>(),
            message = $"Your method was called with params: {jsonRpcObject["params"]} and the result was successful",
            id = jsonRpcObject["id"]
        }).ToString();
    }
}