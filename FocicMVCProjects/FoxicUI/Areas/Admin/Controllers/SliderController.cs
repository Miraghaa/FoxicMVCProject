using AutoMapper;
using Foxic.Buisness.Exceptions;
using Foxic.Buisness.Services.Interfaces;
using Foxic.Buisness.ViewModels.SliderViewModels;
using Foxic.Core.Entities.AreasEntitycontroller;
using Foxic.DataAccess.contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoxicUI.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
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
    public async Task<IActionResult> Delete(int id)
    {
        Slider? slider = await _context.Sliders.FindAsync(id);
        // eger bir sorgu atib tapmiriqqsa cavabi not founddu \404\
        if (slider == null) return NotFound();
        return View(slider);
    }
    [HttpPost]
    [ActionName("Delete")] // BUNU NIYE ELEDIY EYNI ADDA EYNI PARAMETRLERI QEBUL EDE BILMIRY AMMA BELEDE YAZIB ADINI BUCUR EDE BILRIK
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Slider? slider = await _context.Sliders.FindAsync(id);
        if (slider == null) return NotFound();
        //BURDA STROAGE ELAVE ETDIYIMIZ SEKLI SILIRIK BIRINCI YOXLUYURUQ KI VARMI? VARSA OZUN BASA DUSDUN...
        // VE BIZ COMBINE ISTIFADE ETDIK CUNKI TAM YERLESMENI YAZMAYANDA ISLEMEDI
        string fileroot = Path.Combine(_webEnv.WebRootPath, slider.SliderImage);
        if (System.IO.File.Exists(fileroot))
        {
            System.IO.File.Delete(fileroot);
        }

        _context.Sliders.Remove(slider);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
        //return Content(_context.Entry(slider).State.ToString());
        //ASAGIDAKI YAZILISIDA MOVCUDDUR
        //_context.Entry(slider).State= EntityState.Deleted;
        //return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Update(int id)
    {
        Slider? slider = await _context.Sliders.FindAsync(id);
        if (slider == null) return NotFound();
        // eger bir sorgu atib tapmiriqqsa cavabi not founddu \404\
        SliderUploadVM sliderUploadVM = _mapper.Map<SliderUploadVM>(slider);
        return View(sliderUploadVM);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, SliderUploadVM slider)
    {
        if (!ModelState.IsValid) return View(slider);
        Slider? sliderdb = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        if (sliderdb == null) return NotFound();
        if (slider.Image != null)
        {
            try
            {
               string filename = await _fileservice.UploadFile(slider.Image, _webEnv.WebRootPath, 300, "assets", "images", "slider");
                _fileservice.RemoveFile(_webEnv.WebRootPath, sliderdb.SliderImage);
                sliderdb = _mapper.Map<Slider>(slider);
                sliderdb.SliderImage = filename;
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View(slider);
            }
            catch (FileTypeException ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View(slider);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(slider);
            }
        }
        else
        {
            slider.SliderImage=sliderdb.SliderImage;
            sliderdb = _mapper.Map<Slider>(slider);
        }
        //return Content(_context.Entry(sliderdb).State.ToString());
        _context.Sliders.Update(sliderdb);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

}
