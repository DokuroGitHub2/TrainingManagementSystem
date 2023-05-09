﻿using Application.Interfaces;
using Application.Repositories;
using Domain.Entities.Users;
using Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext dbContext, ICacheService cache) : base(dbContext, cache)
        {
            _context = dbContext;
        }

        public async Task<bool> CheckExistUser(string email)
             => await _context.Users.AnyAsync(x => x.Email == email);

        public async Task<User> Find(string email)
           => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}
