﻿using Microsoft.EntityFrameworkCore;
using RunGroupApp.Data;
using RunGroupApp.Interface;
using RunGroupApp.Models;

namespace RunGroupApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContex _context;

        public UserRepository(AppDbContex context)
        {
            this._context = context;
        }
        public bool Add(AppUser user)
        {
            _context.Users.Add(user);
            return Save();
        }

        public bool Delete(AppUser user)
        {
            _context.Users.Remove(user);
            return Save();      
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved=_context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
           _context.Update(user);
            return Save();
        }
    }
}
