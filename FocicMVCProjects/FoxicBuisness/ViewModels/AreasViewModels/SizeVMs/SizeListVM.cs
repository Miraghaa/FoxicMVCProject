using System.ComponentModel.DataAnnotations;

namespace Foxic.Buisness.ViewModels.AreasViewModels.SizeVMs;

public class SizeListVM
{

    [Required]
    public string SizeName { get; set; } = null!;
}
