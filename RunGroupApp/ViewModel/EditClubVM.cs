using RunGroupApp.Data.Enum;
using RunGroupApp.Models;

namespace RunGroupApp.ViewModel
{
    public class EditClubVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public string? RUL { get; set; }
        public ClubCategory clubCategory { get; set; }
    }
}
