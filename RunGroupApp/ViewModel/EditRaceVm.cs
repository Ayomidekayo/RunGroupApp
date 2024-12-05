using RunGroupApp.Data.Enum;
using RunGroupApp.Models;

namespace RunGroupApp.ViewModel
{
    public class EditRaceVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public string? Url { get; set; }
        public RaceCategory RaceCategory { get; set; }
    }
}
