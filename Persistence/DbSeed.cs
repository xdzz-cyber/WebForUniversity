using Application.Interfaces;
using Bogus;
using Domain.Entities;


namespace Persistence;

public class DbSeed
{
    private readonly IApplicationDbContext _ctx;

    public DbSeed(IApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    private static Faker<Incident> FakeData()
    {
        var fakeIncident = new Faker<Incident>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Latitude, f => f.Random.Long(0, 1000))
            .RuleFor(c => c.Longitude, f => f.Random.Long(0, 1000))
            .RuleFor(c => c.Commentary, f => f.Random.Words(10))
            .RuleFor(c => c.ActualPhotoUrl, f => f.Image.PlaceImgUrl());

        return fakeIncident;
    }
    
    public async Task Seed(CancellationToken cancellationToken)
    {
        var incidentsToBeAdded = await Task.Run(() => FakeData().Generate(10), cancellationToken);
        
        await _ctx.Incidents.AddRangeAsync(incidentsToBeAdded, cancellationToken);
        await _ctx.SaveChangesAsync(default);
    }
}
