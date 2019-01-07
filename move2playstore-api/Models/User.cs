using System;
using System.Collections.Generic;

namespace move2playstoreAPI.Models
{
    public partial class User
    {
        public User()
        {
            Comment = new HashSet<Comment>();
            Game = new HashSet<Game>();
            Purchase = new HashSet<Purchase>();
            Rating = new HashSet<Rating>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Comment> Comment { get; set; }
        public ICollection<Game> Game { get; set; }
        public ICollection<Purchase> Purchase { get; set; }
        public ICollection<Rating> Rating { get; set; }
    }
}
