using System;
using System.Collections.Generic;

namespace move2playstoreAPI.Models
{
    public class Game
    {
        public Game()
        {
            Comment = new HashSet<Comment>();
            Image = new HashSet<Image>();
            Purchaseitem = new HashSet<Purchaseitem>();
            Rating = new HashSet<Rating>();
            Video = new HashSet<Video>();
        }

        public int Id { get; set; }
        public string DeveloperId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ServerPath { get; set; }
        public string Genre { get; set; }

        public User Developer { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public ICollection<Image> Image { get; set; }
        public ICollection<Purchaseitem> Purchaseitem { get; set; }
        public ICollection<Rating> Rating { get; set; }
        public ICollection<Video> Video { get; set; }
    }
}
