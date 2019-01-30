using MusicStoreApi.Models;
using System;
using System.Collections.Generic;

namespace MusicStoreApi.Repository.Interfaces
{
    public interface IUserRepository
    {
        User Find(Guid id);

        IEnumerable<User> GetAll();

        Guid Create(User user);

        void Update(User user);

        void Delete(Guid id);
    }
}
