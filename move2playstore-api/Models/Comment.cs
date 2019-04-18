namespace move2playstoreAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Recomendation { get; set; }
        public string Description { get; set; }
    }
}
