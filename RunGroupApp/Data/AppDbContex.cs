using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RunGroupApp.Models;

namespace RunGroupApp.Data
{
    public class AppDbContex : IdentityDbContext<AppUser>
    {
        public AppDbContex(DbContextOptions<AppDbContex> options) : base(options)
        {
        }
        //public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
    }
}
