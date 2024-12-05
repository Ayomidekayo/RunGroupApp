using RunGroupApp.Models;

namespace RunGroupApp.Interface
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetAllClubAsync();
        Task<Club> GetClubAsync(int id);
        Task<Club> GetClubAsyncAsNoTracking(int id);
        Task<IEnumerable<Club>> GetClubByCityAsync(string city);
        bool AddClub(Club club);
        bool UpdatClub(Club club);
        bool DeleteClub(Club club);
        bool Save();
    }
}
