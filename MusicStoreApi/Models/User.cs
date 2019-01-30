using System;

namespace MusicStoreApi.Models
{
    public class User
    {
        public User()
        {
            DateTime date = DateTime.Now;
            DateCreated = date;
            DateUpdated = date;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
