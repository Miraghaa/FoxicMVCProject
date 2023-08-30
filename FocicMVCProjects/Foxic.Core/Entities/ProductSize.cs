namespace Foxic.Core.Entities;

public class ProductSize:BaseEntity
{
	public int SizeId { get; set; }

	public Size Size { get; set; }

	public int ProductId { get; set; }

	public Product Product { get; set; }
}
