using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PlacesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public PlacesController(AppDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Places
        public async Task<IActionResult> Index(string searchString, string placeType)
        {
            var places = from p in _context.Places select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                places = places.Where(s => s.Name.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(placeType))
            {
                places = places.Where(x => x.Type == placeType);
            }

            // ЗАГРУЖАЕМ ОТЗЫВЫ ДЛЯ ГЛАВНОЙ (Последние 3)
            ViewBag.Reviews = await _context.SiteReviews
                .OrderByDescending(r => r.Id)
                .Take(3)
                .ToListAsync();

            return View(await places.ToListAsync());
        }

        // GET: Places/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var place = await _context.Places.FirstOrDefaultAsync(p => p.Id == id);
            if (place == null) return NotFound();

            return View(place);
        }

        // GET: Places/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Places/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Place place, IFormFile? uploadImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadImage != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(uploadImage.FileName);
                    string folderPath = Path.Combine(_appEnvironment.WebRootPath, "images");

                    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                    string path = Path.Combine(folderPath, fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await uploadImage.CopyToAsync(fileStream);
                    }
                    place.ImageUrl = "/images/" + fileName;
                }
                else
                {
                    place.ImageUrl = "https://via.placeholder.com/400x300?text=No+Photo";
                }

                _context.Add(place);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(place);
        }

        // GET: Places/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var place = await _context.Places.FindAsync(id);
            if (place == null) return NotFound();

            return View(place);
        }

        // POST: Places/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl,Type,City,Address,Latitude,Longitude,WorkingHours,ContactName,ContactPhone,HasWifi,HasParking,HasPayment,HasGuide,IsEco")] Place place)
        {
            if (id != place.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(place);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceExists(place.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(place);
        }

        // GET: Places/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var place = await _context.Places.FirstOrDefaultAsync(m => m.Id == id);
            if (place == null) return NotFound();

            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var place = await _context.Places.FindAsync(id);
            if (place != null) _context.Places.Remove(place);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceExists(int id)
        {
            return _context.Places.Any(e => e.Id == id);
        }

        // Метод добавления отзыва
        [HttpPost]
        public async Task<IActionResult> AddReview(string userName, string text, int stars)
        {
            if (stars == 0) stars = 5; // Если забыли выбрать, ставим 5

            var review = new SiteReview
            {
                UserName = userName,
                Text = text,
                Stars = stars,
                Date = DateTime.Now
            };

            _context.SiteReviews.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Обновляем страницу
        }

        // Метод заказа маршрута
        [HttpPost]
        public async Task<IActionResult> OrderRoute(string clientName, string contact, string[] interests)
        {
            // Собираем интересы в одну строку
            string interestsString = interests.Length > 0 ? string.Join(", ", interests) : "Не указано";

            var order = new ServiceOrder
            {
                ClientName = clientName,
                Contact = contact,
                Interests = interestsString,
                CreatedAt = DateTime.Now
            };

            _context.ServiceOrders.Add(order);
            await _context.SaveChangesAsync();

            // Тут можно добавить отправку Email, но для конкурса надежнее сохранять в БД

            return RedirectToAction(nameof(Index));
        }

        // АДМИНКА
        public async Task<IActionResult> AdminPanel()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("IsAdmin"))
            {
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new AdminViewModel
            {
                Orders = await _context.ServiceOrders.OrderByDescending(o => o.CreatedAt).ToListAsync(),
                Reviews = await _context.SiteReviews.OrderByDescending(r => r.Id).ToListAsync()
            };

            return View(viewModel);
        }

        // Удаление заказа
        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.ServiceOrders.FindAsync(id);
            if (order != null)
            {
                _context.ServiceOrders.Remove(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(AdminPanel));
        }

        // Удаление отзыва
        [HttpPost]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.SiteReviews.FindAsync(id);
            if (review != null)
            {
                _context.SiteReviews.Remove(review);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(AdminPanel));
        }
    }
}