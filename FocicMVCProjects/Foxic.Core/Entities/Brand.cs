using System.ComponentModel.DataAnnotations;

namespace Foxic.Core.Entities;

public class Brand:BaseEntity
{
	[Required]
	public string BrandName { get; set; }
	[Required]
	public string Image { get; set; }
	[Required]
	public ICollection<Product> Products { get; set; }
}
