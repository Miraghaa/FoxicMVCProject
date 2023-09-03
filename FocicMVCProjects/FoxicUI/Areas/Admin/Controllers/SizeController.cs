using Foxic.Buisness.Services.Interfaces;
using Foxic.DataAccess.contexts;
using Microsoft.AspNetCore.Mvc;

namespace FoxicUI.Areas.Admin.Controllers;

public class SizeController : Controller
{
    private readonly AppDbContext _context;
    public SizeController(AppDbContext context,
                            IFileService fileservice)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }
}
