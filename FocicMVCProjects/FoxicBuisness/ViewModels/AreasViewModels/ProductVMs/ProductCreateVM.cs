using Microsoft.AspNetCore.Http;

namespace Foxic.Buisness.ViewModels.AreasViewModels.ProductVMs;

public class ProductCreateVM
{
    public string Name { get; set; }
    public int Discount { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public IFormFile MainImage { get; set; }
    public List<IFormFile> Images { get; set; }
    public List<int> ColorIds { get; set; }
    public List<int> SizeIds { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public int CollectionId { get; set; }
    public int DetailId { get; set; }


}
