using System;
using System.Collections.Generic;
using move2playstoreAPI.Models;

namespace move2playstoreAPI.DataTransferObjects
{
    public class GameDto
    {
        public int Id { get; set; }
        public string DeveloperName { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ServerPath { get; set; }
        public string Genre { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public ICollection<Image> Image { get; set; }
        public ICollection<Purchaseitem> Purchaseitem { get; set; }
        public ICollection<RatingDto> Rating { get; set; }
        public ICollection<Video> Video { get; set; }
    }
}
