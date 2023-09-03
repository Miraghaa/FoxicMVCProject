using Foxic.Buisness.ViewModels.AreasViewModels.CategoryVMs;
using Foxic.Core.Entities;
using Foxic.DataAccess.contexts;
using Microsoft.AspNetCore.Mvc;

namespace FoxicUI.Areas.Admin.Controllers;
[Area("Admin")]
public class CategoryController : Controller
{
    private readonly AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
        _context = context;
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
        Category category = new()
        {
            CategoryName = categorylist.CategoryName
        };
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
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
