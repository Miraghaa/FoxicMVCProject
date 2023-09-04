using System.ComponentModel.DataAnnotations;

namespace Foxic.Buisness.ViewModels.AreasViewModels.ProductDetailVMs;

public class ProductDetailUploadVM
{
    public int Id { get; set; }
    [Required, MaxLength(155), MinLength(15)]
    public string ShortDescription { get; set; }

    [Required, MaxLength(255), MinLength(25)]
    public string LongDescription { get; set; }

    public bool Cotton { get; set; }
    public bool Polyester { get; set; }
    public bool Clean { get; set; }
    public bool Non_Chlorine { get; set; }
    public bool Tax { get; set; }
}
