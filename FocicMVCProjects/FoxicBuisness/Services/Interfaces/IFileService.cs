using Microsoft.AspNetCore.Http;

namespace Foxic.Buisness.Services.Interfaces;

public interface IFileService
{
    Task<string> UploadFile(IFormFile file, string root, int kb, params string[] folders);
    public void RemoveFile(string root, string filePath);

}
