using Microsoft.AspNetCore.Http;

namespace Foxic.Buisness.ViewModels.AreasViewModels.ProductVMs;

public class ProductCreateVM
{
    public string Name { get; set; }
    public double Price { get; set; }
    public IFormFile MainImage { get; set; }
    public List<IFormFile> Images { get; set; }
    public List<int> ColorIds { get; set; }
    public List<int> SizeIds { get; set; }
    public List<int> BrandIds { get; set; }

}
