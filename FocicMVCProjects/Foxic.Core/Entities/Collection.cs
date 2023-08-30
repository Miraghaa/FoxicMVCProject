namespace Foxic.Core.Entities;

public class Collection:BaseEntity
{
	public string CollectionName { get; set; }

	public string Image { get; set; }

	public ICollection<Product> Products { get; set; }
}
