using System.Numerics;

namespace Foxic.Core.Entities;

public class Color:BaseEntity
{
	public string Name { get; set; }

	public string Image {get ; set; }

	public List<ProductColor> Products { get; set;}
    public Color()
    {
        Products = new();
    }
}
