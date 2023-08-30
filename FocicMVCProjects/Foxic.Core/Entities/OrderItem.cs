namespace Foxic.Core.Entities;

public class OrderItem:BaseEntity
{
	public int Count { get; set; }

	public double Price { get; set; }

	public string Size { get; set; }

	public string Color { get; set; }

	public int ProductId { get; set; }

	public Product Products { get; set;}

	public int OrderId {get; set; } 

	public Order Orders { get; set; }
}
