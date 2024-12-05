using RunGroupApp.Models;

namespace RunGroupApp.ViewModel.Dashboard
{
    public class EditUserDashboardVM
    {
        public string Id { get; set; }
        public int? Pace { get; set; }
        public int? Milage { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ProfileImageUrl { get; set; }
        public IFormFile Image { get; set; }
       
    }
}
