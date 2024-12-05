using Microsoft.EntityFrameworkCore;
using RunGroupApp.Data;
using RunGroupApp.Interface;
using RunGroupApp.Models;

namespace RunGroupApp.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly AppDbContex _contex;

        public ClubRepository(AppDbContex contex)
        {
            this._contex = contex;
        }

        public bool AddClub(Club club)
        {
            _contex.Add(club);
            return Save();
        }

        public bool DeleteClub(Club club)
        {
            _contex.Remove(club);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetAllClubAsync()
        {
            return await _contex.Clubs.ToListAsync();
        }

        public async Task<Club> GetClubAsync(int id)
        {
            return await _contex.Clubs.Include(x=>x.Address).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Club> GetClubAsyncAsNoTracking(int id)
        {
            return await _contex.Clubs.Include(x => x.Address).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Club>> GetClubByCityAsync(string city)
        {
            return await  _contex.Clubs.Where(x=>x.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
           _contex.SaveChangesAsync();
            return Save();
        }

        public bool UpdatClub(Club club)
        {
            _contex.Update(club);
            return Save();
        }
    }
}
