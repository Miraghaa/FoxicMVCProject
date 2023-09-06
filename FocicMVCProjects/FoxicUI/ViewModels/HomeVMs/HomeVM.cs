using Foxic.Core.Entities;
using Foxic.Core.Entities.AreasEntitycontroller;

namespace FoxicUI.ViewModels.HomeVMs;

public class HomeVM
{
    public List<Slider> Sliders { get; set; } = null!;
    public List<Category> Categories { get; set; } = null!;
    public List<Collection> Collections { get; set; } = null!;
    public List<Product> Products { get; set; } = null!;
	public List<Brand> Brands { get; set; } = null!;
	public List<Image> Images { get; set; } = null!;

}
