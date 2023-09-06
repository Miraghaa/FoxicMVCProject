using AutoMapper;
using Foxic.Buisness.ViewModels.AreasViewModels.ProductVMs;
using Foxic.Core.Entities;
using Foxic.DataAccess.contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoxicUI.Controllers;

public class CategoryController : Controller
{
	private readonly AppDbContext _context;

	public CategoryController(AppDbContext context)
	{
		_context = context;
	}
	public IActionResult Index()
	{
		return View();
	}
	public async Task<IActionResult> Detail(int Id)
	{

		Product? product = _context.Products
				.Include(p => p.Colors).ThenInclude(pc => pc.Color)
				.Include(p => p.Sizes).ThenInclude(pc => pc.Size)
				.FirstOrDefault(p => p.Id.Equals(Id));

		ProductDVM? productDetail = _context.Products.Select(p => new ProductDVM()
		{
			Id = p.Id,
			Images = p.Images,
			Name = p.Name,
			Price = p.Price,
			Rating = p.Rating,
			Colors = p.Colors,
			Sizes = p.Sizes,

		}).FirstOrDefault(p => p.Id.Equals(Id));
		return View(productDetail);

		//CategoryVM categoryVM = _context.Products.(c=>new() 
		//{ 
		//	Products =await _context.Products.ToListAsync()
		//}).FirstOrDefault(c => c.Id.Equals(Id));
		//return View(categoryVM);
	}
}
