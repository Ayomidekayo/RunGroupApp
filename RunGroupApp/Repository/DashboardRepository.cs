using Microsoft.EntityFrameworkCore;
using RunGroupApp.Data;
using RunGroupApp.Interface;
using RunGroupApp.Models;

namespace RunGroupApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
       
        private readonly AppDbContex _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(AppDbContex context,IHttpContextAccessor httpContextAccessor)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Club>> GetAllClubsAysnc()
        {
            var clubUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs = _context.Clubs.Where(x => x.AppUser.Id == clubUser);
            return userClubs.ToList();
        }

        public async Task<List<Race>> GetAllRacesAysnc()
        {
            var Raceuser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userRaces = _context.Races.Where(x => x.AppUser.Id == Raceuser);
            return userRaces.ToList();
        }

        public async Task<AppUser> GetById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetByIdAsNoTtracking(string id)
        {
           return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);
        }

        public bool Save()
        {
            var  saved=_context.SaveChanges();
            return saved >0? true:false;
        }

        public bool Update(AppUser user)
        {
           _context.Users.Update(user);
            return Save();
        }
    }
}
