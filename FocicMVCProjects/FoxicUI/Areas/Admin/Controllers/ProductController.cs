using AutoMapper;
using Foxic.Buisness.Services.Interfaces;
using Foxic.Buisness.Utilities;
using Foxic.Buisness.ViewModels.AreasViewModels.ProductVMs;
using Foxic.Core.Entities;
using Foxic.DataAccess.contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace FoxicUI.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "SuperAdmin")]
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
        ViewBag.Categories = _context.Categories.ToList();
        ViewBag.Collections = _context.Collections.ToList();
        ViewBag.ProductDetails = _context.productDetails.ToList();
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
                    ProductId = id,
                    ColorId = id,
                };

            newProduct.Colors.Add(productColor);
            }
        foreach (int id in productcreate.SizeIds)
        {
            ProductSize producttSize = new()
            {
                ProductId = id,
                SizeId = id,
            };

            newProduct.Sizes.Add(producttSize);
        }
        if (!productcreate.MainImage.CheckFileSize(1000))
        {
            return View(nameof(Create));
        };

        if (!productcreate.MainImage.CheckFileType("image/"))
        {
            return View(nameof(Create));
        };

        _context.Products.Add(newProduct);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}

