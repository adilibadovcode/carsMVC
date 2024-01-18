using CarsMVC.Context;
using CarsMVC.ViewModels.CardVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsMVC.Controllers
{
    public class HomeController : Controller
    {
        CarsDBContext _db { get; }

        public HomeController(CarsDBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var data =await _db.Cards.Select(x =>  new CardListItemVM
            {
                Title = x.Title,
                Description = x.Description,
                Image = x.Image,
            }).ToListAsync();
            return View(data);
        }
    }
}
