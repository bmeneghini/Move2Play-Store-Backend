using System;
using System.Collections.Generic;

namespace Api
{
    public partial class Video
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Path { get; set; }

        public Game Game { get; set; }
    }
}
