using Foxic.Buisness.Exceptions;
using Foxic.Buisness.Services.Interfaces;
using Foxic.Buisness.ViewModels.AreasViewModels.BrandVMs;
using Foxic.Buisness.ViewModels.AreasViewModels.CollectionVMs;
using Foxic.Core.Entities;
using Foxic.DataAccess.contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoxicUI.Areas.Admin.Controllers;
[Area("Admin")]
public class CollectionController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webEnv;
    private readonly IFileService _fileservice;
    public CollectionController(AppDbContext context,
                            IWebHostEnvironment webEnv,
                            IFileService fileservice)
    {
        _context = context;
        _webEnv = webEnv;
        _fileservice = fileservice;
    }
    public IActionResult Collection(int id)
    {
        Collection collection = _context.Collections.FirstOrDefault(c => c.Id == id);
        List<Collection> collections = _context.Collections.ToList();
        return View(collections);
    }
    public async Task<IActionResult> Details(int id)
    {
        Collection? collection = await _context.Collections.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        if (collection == null) return NotFound();
        return View(collection);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CollectionCreateVM  collection)
    {
        if (!ModelState.IsValid) return View(collection);
        string filename = string.Empty;
        try
        {
            Collection collection1 = new()
            {
                CollectionName = collection.CollectionName,
            };
            filename = await _fileservice.UploadFile(collection.Image, _webEnv.WebRootPath, 300, "assets", "images", "slider");
            collection1.Image = filename;
            await _context.Collections.AddAsync(collection1);
            await _context.SaveChangesAsync();
        }
        catch (FileSizeException ex)
        {
            ModelState.AddModelError("CollectionImage", ex.Message);
            return View(collection);
        }
        catch (FileTypeException ex)
        {
            ModelState.AddModelError("CollectionImage", ex.Message);
            return View(collection);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(collection);
        }
        return RedirectToAction(nameof(Collection));
    }
    public async Task<IActionResult> Delete(int id)
    {
        Collection? collection = await _context.Collections.FindAsync(id);
        if (collection == null) return NotFound();
        return View(collection);
    }
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int id)
    {
        Collection? collection = await _context.Collections.FindAsync(id);
        if (collection == null) return NotFound();
        string fileroot = Path.Combine(_webEnv.WebRootPath, collection.Image);
        if (System.IO.File.Exists(fileroot))
        {
            System.IO.File.Delete(fileroot);
        }
        _context.Collections.Remove(collection);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Collection));

    }
    public async Task<IActionResult> Update(int id)
    {
        Collection? collection = await _context.Collections.FindAsync(id);
        if (collection == null) return NotFound();
        CollectionUploadVM collectionUpload = new()
        {
            Id = collection.Id,
            CollectionName = collection.CollectionName,
            CollectionImage = collection.Image,
        };
        return View(collectionUpload);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, CollectionUploadVM collection)
    {
        if (!ModelState.IsValid) return View(collection);
        Collection? collectiondb = await _context.Collections.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        if (collectiondb == null) return NotFound();
        if (collection.Image != null)
        {
            try
            {
                string filename = await _fileservice.UploadFile(collection.Image, _webEnv.WebRootPath, 300, "assets", "images", "slider");
                _fileservice.RemoveFile(_webEnv.WebRootPath, collectiondb.Image);
                Collection collection1 = new()
                {
                    Id = collection.Id,
                    CollectionName = collection.CollectionName,
                    Image = collection.CollectionImage
                };
                collectiondb = collection1;
                collectiondb.Image = filename;
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View(collection);
            }
            catch (FileTypeException ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View(collection);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(collection);
            }
        }
        else
        {
            collection.CollectionImage = collectiondb.Image;
            Collection collection1 = new()
            {
                Id = collection.Id,
                CollectionName = collection.CollectionName,
                Image = collection.CollectionImage
            };
            collectiondb = collection1;
        }
        //return Content(_context.Entry(sliderdb).State.ToString());
        _context.Collections.Update(collectiondb);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Collection));
    }
}

