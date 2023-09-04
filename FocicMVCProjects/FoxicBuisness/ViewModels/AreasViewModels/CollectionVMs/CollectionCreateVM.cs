using Microsoft.AspNetCore.Http;

namespace Foxic.Buisness.ViewModels.AreasViewModels.CollectionVMs;

public class CollectionCreateVM
{
    public string CollectionName { get; set; } = null!;

    public IFormFile Image { get; set; }
}
