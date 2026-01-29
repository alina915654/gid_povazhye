using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Перенаправление на каталог
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Places");
        }

        // !!! ВОТ ЭТОГО МЕТОДА НЕ ХВАТАЛО !!!
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Страница Экотуризма
        public async Task<IActionResult> Eco()
        {
            var ecoPlaces = await _context.Places
                                          .Where(p => p.IsEco == true)
                                          .ToListAsync();
            return View(ecoPlaces);
        }

        // Секретные методы админа
        public IActionResult AdminOn()
        {
            Response.Cookies.Append("IsAdmin", "true", new CookieOptions { Expires = DateTime.Now.AddDays(30) });
            return RedirectToAction("Index", "Places");
        }

        public IActionResult AdminOff()
        {
            Response.Cookies.Delete("IsAdmin");
            return RedirectToAction("Index", "Places");
        }
    }
}