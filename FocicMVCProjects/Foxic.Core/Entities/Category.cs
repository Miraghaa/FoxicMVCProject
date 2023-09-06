using System.ComponentModel.DataAnnotations;

namespace Foxic.Core.Entities;

public class Category:BaseEntity
{
    [Required]
	public string CategoryName { get; set; }
    [Required]
    public string Image { get; set; }

    public ICollection<Product> Products { get; set; }
}
