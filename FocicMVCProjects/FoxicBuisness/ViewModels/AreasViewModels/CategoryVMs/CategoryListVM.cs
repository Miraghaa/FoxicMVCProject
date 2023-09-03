using System.ComponentModel.DataAnnotations;

namespace Foxic.Buisness.ViewModels.AreasViewModels.CategoryVMs;

public class CategoryListVM
{
    [Required, MaxLength(50), MinLength(10)]
    public string CategoryName { get; set; }
}
