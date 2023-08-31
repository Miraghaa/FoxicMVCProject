using AutoMapper;
using Foxic.Buisness.Exceptions;
using Foxic.Buisness.Services.Interfaces;
using Foxic.Buisness.ViewModels.SliderViewModels;
using Foxic.Core.Entities.AreasEntitycontroller;
using Foxic.DataAccess.contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoxicUI.Areas.Admin.Controllers;

[Area("Admin")]
public class SliderController : Controller
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webEnv;
    private readonly IFileService _fileservice;
    public SliderController(AppDbContext context, 
                            IMapper mapper, 
                            IWebHostEnvironment webEnv, 
                            IFileService fileservice)
    {
        _context = context;
        _mapper = mapper;
        _webEnv = webEnv;
        _fileservice = fileservice;
    }

    public IActionResult Index()
    {
        var sliders = _context.Sliders.AsNoTracking();
        ViewBag.Count = sliders.Count();
        return View(sliders);
    }
	public async Task<IActionResult> Details(int id)
	{
		Slider? slider = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
		if (slider == null) return NotFound();
		return View(slider);
	}
	public IActionResult Create()
	{
		if (_context.Sliders.Count() >= 5)
		{
			return BadRequest();
		}
		return View();
	}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SliderPostVM slider)
    {
        //anoteysinnari yoxluyur
        if (!ModelState.IsValid) return View(slider);
        string filename = string.Empty;
        try
        {
            filename = await _fileservice.UploadFile(slider.ImageUrl, _webEnv.WebRootPath, 300, "assets", "images", "slider");
            Slider newslider = _mapper.Map<Slider>(slider);
            newslider.SliderImage = filename;
            await _context.Sliders.AddAsync(newslider);
            await _context.SaveChangesAsync();
        }
        catch (FileSizeException ex)
        {
            ModelState.AddModelError("SliderImage", ex.Message);
            return View(slider);
        }
        catch (FileTypeException ex)
        {
            ModelState.AddModelError("SliderImage", ex.Message);
            return View(slider);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(slider);
        }

        return RedirectToAction(nameof(Index));
    }

}
