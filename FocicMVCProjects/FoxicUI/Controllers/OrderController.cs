using Microsoft.AspNetCore.Mvc;

namespace FoxicUI.Controllers;

public class OrderController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
