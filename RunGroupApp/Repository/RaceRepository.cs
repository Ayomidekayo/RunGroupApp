using Microsoft.EntityFrameworkCore;
using RunGroupApp.Data;
using RunGroupApp.Interface;
using RunGroupApp.Models;

namespace RunGroupApp.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly AppDbContex _contex;

        public RaceRepository(AppDbContex contex)
        {
            this._contex = contex;
        }
        public bool Add(Race race)
        {
            _contex.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            _contex.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAllRacesAsync()
        {
            return await _contex.Races.ToListAsync();
        }

        public async Task<Race> GetRaceByIdAsync(int id)
        {
            return await _contex.Races.Include(x => x.Address).FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<Race> GetRaceByIdAsyncAsNoTracking(int id)
        {
            return await _contex.Races.Include(x => x.Address).AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Race>> GetRacesByCityAsync(string city)
        {
            return await _contex.Races.Where(x => x.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _contex.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Race race)
        {
            _contex.Update(race);
            return Save();
        }
    }
}
