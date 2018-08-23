using System;
using System.Collections.Generic;

namespace move2playstoreAPI.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Path { get; set; }

        public Game Game { get; set; }
    }
}
