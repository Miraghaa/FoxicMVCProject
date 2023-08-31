using System.ComponentModel.DataAnnotations;

namespace Foxic.Core.Entities.AreasEntitycontroller;

public class Slider:BaseEntity
{
    [Required]
    public string SliderName { get; set; } = null!;

    [Required]
    public string SliderAbout { get; set; } = null!;

    [Required]
    public string SliderImage { get; set; } = null!;
}
