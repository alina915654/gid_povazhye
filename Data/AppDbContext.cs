using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Эта строка говорит: "Создай в базе данных таблицу Places на основе класса Place"
        public DbSet<Place> Places { get; set; }
        public DbSet<SiteReview> SiteReviews { get; set; }
        public DbSet<ServiceOrder> ServiceOrders { get; set; }
    }
}
