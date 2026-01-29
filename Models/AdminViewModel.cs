namespace WebApplication1.Models
{
    public class AdminViewModel
    {
        public IEnumerable<ServiceOrder> Orders { get; set; }
        public IEnumerable<SiteReview> Reviews { get; set; }
    }
}
