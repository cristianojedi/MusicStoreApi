using MusicStoreApi.Data;
using MusicStoreApi.Models;
using MusicStoreApi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicStoreApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MusicStoreDbContext _context;

        public UserRepository(MusicStoreDbContext context)
        {
            _context = context;
        }

        public User Find(Guid id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public Guid Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user.Id;
        }

        public void Update(User user)
        {
            _context.Attach(user);
            _context.Entry(user).Property(u => u.Name).IsModified = true;
            _context.Entry(user).Property(u => u.Email).IsModified = true;
            _context.Entry(user).Property(u => u.DateUpdated).IsModified = true;
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = _context.Users.Single(u => u.Id == id);
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }
    }
}
