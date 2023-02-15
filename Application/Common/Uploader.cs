using Microsoft.AspNetCore.Http;

namespace Application.Common;

public static class Uploader
{
    public static async Task<string> UploadImage(IFormFile file)
    {
        var specialId = Guid.NewGuid();
        
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"../WebUI/Images", $"{specialId}-{file.FileName}");

        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);

        return filePath;
    }
}