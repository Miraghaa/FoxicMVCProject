using AutoMapper;
using Foxic.Buisness.ViewModels.SliderViewModels;
using Foxic.Core.Entities.AreasEntitycontroller;

namespace Foxic.Buisness.Mappers;

public class SliderProfile:Profile
{
    public SliderProfile()
    {
		CreateMap<Slider, SliderPostVM>().ReverseMap();
		CreateMap<SliderUploadVM, Slider>().ReverseMap();
	}
}
