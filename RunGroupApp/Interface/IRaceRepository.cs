using RunGroupApp.Models;

namespace RunGroupApp.Interface
{
    public interface IRaceRepository
    {
      Task<IEnumerable<Race>> GetAllRacesAsync();
     Task<IEnumerable<Race>> GetRacesByCityAsync(string city);
        Task<Race> GetRaceByIdAsync(int id);
        Task<Race> GetRaceByIdAsyncAsNoTracking(int id);
        bool Add(Race race);
        bool Update(Race race);
        bool Delete(Race race);
        bool Save();

    }
}
