namespace WebApplication1.Models
{
    public class SiteReview
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public int Stars { get; set; }
        public DateTime Date { get; set; }
    }
}
