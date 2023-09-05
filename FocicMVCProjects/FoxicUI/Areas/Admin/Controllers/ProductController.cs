using AutoMapper;
using Foxic.Buisness.Services.Interfaces;
using Foxic.Buisness.Utilities;
using Foxic.Buisness.ViewModels.AreasViewModels.ProductVMs;
using Foxic.Core.Entities;
using Foxic.DataAccess.contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace FoxicUI.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webEnv;
    private readonly IFileService _fileservice;
    public ProductController(AppDbContext context,
                            IWebHostEnvironment webEnv,
                            IFileService fileservice)
    {
        _context = context;
        _webEnv = webEnv;
        _fileservice = fileservice;
    }
    public IActionResult Index()
    {
        List<ProductListVM> product = _context.Products.Select(p => new ProductListVM()
        {
            Name = p.Name,
            Images = p.Images.FirstOrDefault(i => i.IsMain.Equals(true)).Url,
        }).ToList();


        return View(product);
    }
    public IActionResult Create()
    {
        ViewBag.Colors = _context.Colors.ToList();
        ViewBag.Sizes = _context.Sizes.ToList();
        ViewBag.Brands = _context.Brands.ToList();
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateVM productcreate)
    {

        ViewBag.Colors = _context.Colors.ToList();
        ViewBag.Sizes = _context.Sizes.ToList();
        ViewBag.Brands = _context.Brands.ToList();

        string filename = string.Empty;
        Product newProduct = new ()
        {
            Name = productcreate.Name,
            Price = productcreate.Price,
            CategoryId = productcreate.CategoryId,
            CollectionId = productcreate.CollectionId,
            DetailId = productcreate.DetailId,
            BrandId = productcreate.BrandId,
        };
        
        filename = await _fileservice.UploadFile(productcreate.MainImage, _webEnv.WebRootPath, 300, "assets", "images", "slider");
        Image MainImage = new()
        {
            IsMain = true,
            Url = filename
        };

        newProduct.Images.Add(MainImage);
       
        foreach (IFormFile image in productcreate.Images)
        {
            if (!image.CheckFileSize(1000))
            {
                return View(nameof(Create));
            };

            if (!image.CheckFileType("image/"))
            {
                return View(nameof(Create));
            };

            Image NotMainImage = new()
            {
                IsMain = false,
                Url = filename
            };

            newProduct.Images.Add(NotMainImage);
        }
            foreach (int id in productcreate.ColorIds)
            {
                ProductColor productColor = new()
                {
                    ColorId = id,
                };

            newProduct.Colors.Add(productColor);
            }
        foreach (int id in productcreate.SizeIds)
        {
            ProductSize producttSize = new()
            {
                SizeId = id,
            };

            newProduct.Sizes.Add(producttSize);
        }

        _context.Products.Add(newProduct);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}






//[HttpPost]
//[ValidateAntiForgeryToken]
//public async Task<IActionResult> Create(PlantCreateVM plant)
//{

//    ViewBag.Colors = _context.Colors.ToList();
//    ViewBag.Sizes = _context.Sizes.ToList();

//    Plant newPlant = new Plant()
//    {
//        Title = plant.Title,
//        Price = plant.Price,
//    };

//    string Url = await plant.MainImage.CreateFilePath(_env.WebRootPath, "assets", "images", "products");

//    Image MainImage = new()
//    {
//        IsMain = true,
//        Url = Url
//    };

//    newPlant.Images.Add(MainImage);

//    foreach (IFormFile image in plant.Images)
//    {
//        if (!image.CheckFileSize(1000))
//        {
//            return View(nameof(Create));
//        };

//        if (!image.CheckFileType("image/"))
//        {
//            return View(nameof(Create));
//        };

//        Image NotMainImage = new()
//        {
//            IsMain = false,
//            Url = Url
//        };

//        newPlant.Images.Add(NotMainImage);
//    }

//    foreach (int id in plant.ColorIds)
//    {
//        PlantColor plantColor = new()
//        {
//            ColorId = id,
//        };

//        newPlant.Colors.Add(plantColor);
//    }

//    foreach (int id in plant.SizeIds)
//    {
//        PlantSize plantSize = new()
//        {
//            SizeId = id,
//        };

//        newPlant.Sizes.Add(plantSize);
//    }

//    if (!plant.MainImage.CheckFileSize(1000))
//    {
//        return View(nameof(Create));
//    };

//    if (!plant.MainImage.CheckFileType("image/"))
//    {
//        return View(nameof(Create));
//    };

//    _context.Plants.Add(newPlant);
//    _context.SaveChanges();

//    return RedirectToAction(nameof(Index));
//}