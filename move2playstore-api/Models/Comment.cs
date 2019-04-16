namespace move2playstoreAPI.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public sbyte Recomendation { get; set; }
        public string Description { get; set; }

        public Game Game { get; set; }
        public User User { get; set; }
    }
}
