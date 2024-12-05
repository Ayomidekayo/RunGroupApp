using RunGroupApp.Models;

namespace RunGroupApp.ViewModel
{
    public class HomeVM
    {
        public IEnumerable<Club> Clubs { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}
