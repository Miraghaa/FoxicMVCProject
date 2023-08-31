using Microsoft.AspNetCore.Http;

namespace Foxic.Buisness.Utilities;

public static class Extention
{
	public static bool CheckFileSize(this IFormFile file, int kb)
	{
		return file.Length / 1024 <= kb;
	}
	public static bool CheckFileType(this IFormFile file, string filetype)
	{
		return file.ContentType.Contains(filetype);
	}
	public static async Task<string> UploadFile(this IFormFile file,
		string root,
		string folderRoot)
	{
		string name = Guid.NewGuid().ToString() + file.FileName;
		string filename = Path.Combine(folderRoot, name);
		string fileRoot = Path.Combine(root, filename);
		using (FileStream fileStream = new FileStream(fileRoot, FileMode.Create))
		{
			await file.CopyToAsync(fileStream);
		}
		return filename;
	}
}
