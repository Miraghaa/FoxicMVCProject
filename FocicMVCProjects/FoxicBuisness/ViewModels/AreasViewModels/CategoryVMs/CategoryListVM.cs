using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Foxic.Buisness.ViewModels.AreasViewModels.CategoryVMs;

public class CategoryListVM
{
    [Required, MaxLength(50), MinLength(5)]
    public string CategoryName { get; set; }
	public IFormFile Image { get; set; }
}
