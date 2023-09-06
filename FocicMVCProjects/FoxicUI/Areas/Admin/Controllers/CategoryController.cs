using Foxic.Buisness.Exceptions;
using Foxic.Buisness.Services.Interfaces;
using Foxic.Buisness.ViewModels.AreasViewModels.CategoryVMs;
using Foxic.Core.Entities;
using Foxic.DataAccess.contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;

namespace FoxicUI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
public class CategoryController : Controller
{
	private readonly AppDbContext _context;
	private readonly IWebHostEnvironment _webEnv;
	private readonly IFileService _fileservice;
	public CategoryController(AppDbContext context,
							IWebHostEnvironment webEnv,
							IFileService fileservice)
	{
		_context = context;
		_webEnv = webEnv;
		_fileservice = fileservice;
	}

	public IActionResult Category(int id)
    {
        Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
        List<Category> categories = _context.Categories.ToList();
        return View(categories);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryListVM categorylist)
    {
        if(!ModelState.IsValid) return View(categorylist);
		string filename = string.Empty;
		try
		{
			Category category = new()
			{
				CategoryName = categorylist.CategoryName
			};
			filename = await _fileservice.UploadFile(categorylist.Image, _webEnv.WebRootPath, 300, "assets", "images", "slider");
            category.Image = filename;
            await _context.Categories.AddAsync(category);
			await _context.SaveChangesAsync();
		}
		catch (FileSizeException ex)
		{
			ModelState.AddModelError("Image", ex.Message);
			return View(categorylist);
		}
		catch (FileTypeException ex)
		{
			ModelState.AddModelError("Image", ex.Message);
			return View(categorylist);
		}
		catch (Exception ex)
		{
			ModelState.AddModelError("", ex.Message);
			return View(categorylist);
		}
		return RedirectToAction(nameof(Category));
		
	}
    public async Task<IActionResult> Delete(int id)
    {
        Category? category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Category? category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Category));

    }
}
