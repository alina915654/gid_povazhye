using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Добавляем БД
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Добавляем сервисы
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Настройка для ошибок
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// ВАЖНОЕ ИЗМЕНЕНИЕ 1: Классическая раздача статических файлов (CSS, JS, картинки)
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Убрали строку app.MapStaticAssets(); - она может конфликтовать на хостинге

// ВАЖНОЕ ИЗМЕНЕНИЕ 2: Обычный маршрут без привязки к ассетам
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Places}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate(); // Эта команда создаст таблицы сама!
}

app.Run();