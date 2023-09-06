using Foxic.Core.Entities.AreasEntitycontroller;
using Foxic.DataAccess.contexts;
using FoxicUI.ViewModels.HomeVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace FoxicUI.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        HomeVM vm = new()
        {
            Sliders = await _context.Sliders.ToListAsync(),
            Categories = await _context.Categories.ToListAsync(),
            Collections = await _context.Collections.ToListAsync(),
            Products = await _context.Products.ToListAsync(),
			Brands = await _context.Brands.ToListAsync(),
			Images = await _context.Images.ToListAsync(),
		};

        return View(vm);
    }
}
