using System;
using System.Collections.Generic;

namespace Api
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
