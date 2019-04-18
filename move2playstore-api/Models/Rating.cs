namespace move2playstoreAPI.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int GameId { get; set; }
        public string Evaluation { get; set; }
    }
}
