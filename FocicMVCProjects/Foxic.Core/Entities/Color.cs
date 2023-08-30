namespace Foxic.Core.Entities;

public class Color:BaseEntity
{
	public string Name { get; set; }

	public string Image {get ; set; }

	public ICollection<ProductColor> Products { get; set;}
}
