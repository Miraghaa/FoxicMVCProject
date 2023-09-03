using Foxic.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Foxic.Buisness.ViewModels.AreasViewModels.ColorVMs;

public class ColorCreateVM
{
    [Required]
    public string ColorName { get; set; } = null!;
    [Required]
    public IFormFile Image { get; set; }
}
