namespace Foxic.Core.Entities;

public class Order:BaseEntity
{
	public double TotalPrice { get; set; }
	
	public string Description { get; set; }

	public DateTime CreatedTime { get; set; }

	public ICollection<OrderItem> Product {get; set;}

}
