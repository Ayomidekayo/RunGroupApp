using RunGroupApp.Models;

namespace RunGroupApp.Interface
{
    public interface IDashboardRepository
    {
        Task<List<Race>> GetAllRacesAysnc();
        Task<List<Club>> GetAllClubsAysnc();
        Task<AppUser> GetById(string id);
        Task<AppUser> GetByIdAsNoTtracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
