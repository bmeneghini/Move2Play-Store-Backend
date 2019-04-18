using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace move2playstoreAPI.DataTransferObjects
{
    public class RatingDto
    {
        public string UserId { get; set; }
        public int GameId { get; set; }
        public int Evaluation { get; set; }
    }
}
