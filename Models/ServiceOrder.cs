namespace WebApplication1.Models
{
    public class ServiceOrder
    {
        public int Id { get; set; }
        public string ClientName { get; set; }  // Имя клиента
        public string Contact { get; set; }     // Почта или Телеграм
        public string Interests { get; set; }   // Что выбрал
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Когда заказал
    }
}
