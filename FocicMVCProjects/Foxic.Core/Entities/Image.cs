namespace Foxic.Core.Entities;

public class Image:BaseEntity
{
	public string Url { get; set; } = null!;
    public bool IsMain { get; set; } = false;
    public int ProductId { get; set; }
	public Product Product { get; set; }
}
