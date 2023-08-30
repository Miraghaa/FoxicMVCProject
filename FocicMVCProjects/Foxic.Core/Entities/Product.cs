namespace Foxic.Core.Entities;

public class Product:BaseEntity
{
	public  int Rating { get; set; }
	public string Name { get; set; }
    public int Discount { get; set; }
	public int Stock { get; set; }
	public double Price { get; set; }
	public DateTime UpdatedTime { get; set; }
	public DateTime DeletedTime { get; set; }
	public DateTime CreatedTime { get; set; }
	// Reletation
	public int CategoryId { get; set; }
	public Category Category { get; set; }
	public int CollectionId { get; set; }
	public Collection Collection { get; set; }
	public int DetailId { get; set; }
	public ProductDetail Detail { get; set; }
	public int BrandId { get; set; }
	public Brand Brand { get; set; }
	public ICollection<ProductColor> Colors { get; set; }
	public ICollection<ProductSize> Sizes { get; set; }
	public ICollection<OrderItem> Orders { get; set; }
	public List<Image> Images { get; set; }
	public Product()
	{
		Images = new ();
	}
}
