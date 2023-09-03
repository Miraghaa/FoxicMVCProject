

using Microsoft.AspNetCore.Http;

namespace Foxic.Buisness.ViewModels.AreasViewModels.BrandVMs;

public class BrandCreateVM
{
    public string BrandName { get; set; } = null!;

    public IFormFile Image { get; set; }
}
