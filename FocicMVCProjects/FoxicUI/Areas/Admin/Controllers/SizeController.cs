using Foxic.Buisness.Services.Interfaces;
using Foxic.Buisness.ViewModels.AreasViewModels.CategoryVMs;
using Foxic.Buisness.ViewModels.AreasViewModels.SizeVMs;
using Foxic.Core.Entities;
using Foxic.DataAccess.contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoxicUI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
public class SizeController : Controller
{
    private readonly AppDbContext _context;
    public SizeController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Sizes(int id)
    {
        Size size = _context.Sizes.FirstOrDefault(s => s.Id == id);
        List<Size> sizes = _context.Sizes.ToList();
        return View(sizes);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SizeListVM size)
    {
        if (!ModelState.IsValid) return View(size);
        Size size1 = new()
        {
            Name = size.SizeName
        };
        await _context.Sizes.AddAsync(size1);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Sizes));
    }
    public async Task<IActionResult> Delete(int id)
    {
        Size? size = await _context.Sizes.FindAsync(id);
        if (size == null) return NotFound();
        return View(size);
    }
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Size? size = await _context.Sizes.FindAsync(id);
        if (size == null) return NotFound();
        _context.Sizes.Remove(size);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Sizes));

    }
}
