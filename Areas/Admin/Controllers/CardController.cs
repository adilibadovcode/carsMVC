using CarsMVC.Context;
using CarsMVC.Helpers;
using CarsMVC.Models;
using CarsMVC.ViewModels.CardVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsMVC.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin,Moderator")]
    [Area("Admin")]
    public class CardController : Controller
    {
        CarsDBContext _db { get; }
        IWebHostEnvironment _env { get; }

        public CardController(CarsDBContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _db.Cards.Select(x => new CardListItemVM
            {
                Id = x.Id,
                Description = x.Description,
                Image = x.Image,
                Title = x.Title
            }).ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null || Id < 0) return BadRequest();
            var data = await _db.Cards.FindAsync(Id);
            if (data == null) return NotFound();
            _db.Cards.Remove(data);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CardCreateVM vm)
        {

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Card card = new Card
            {
                Description = vm.Description,
                Image = await vm.Image.SaveAsync(PathConstants.Card),
                Title = vm.Title,
            };
            await _db.Cards.AddAsync(card);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? Id)
        {
            if (Id == null || Id < 0) return BadRequest();
            var data = await _db.Cards.FindAsync(Id);
            if (data == null) return NotFound();
            return View(new CardUpdateVM
            {
                Title = data.Title,
                Description = data.Description,

            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? Id, CardUpdateVM vm)
        {
            if (Id == null || Id < 0) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var data = await _db.Cards.FindAsync(Id);
            if (data == null) return NotFound();
            if (vm.Image != null)
            {
                string filePath = Path.Combine(PathConstants.RootPath, data.Image);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            data.Image = await vm.Image.SaveAsync(PathConstants.Card);
            }
            data.Title = vm.Title;
            data.Description = vm.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
