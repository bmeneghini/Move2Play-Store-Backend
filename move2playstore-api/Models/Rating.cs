using System;
using System.Collections.Generic;

namespace move2playstoreAPI.Models
{
    public partial class Rating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }

        public Game Game { get; set; }
        public User User { get; set; }
    }
}
