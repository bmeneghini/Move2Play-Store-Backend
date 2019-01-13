namespace move2playstoreAPI.DataTransferObjects
{
    public class GameUploadDto
    {
        public string DeveloperId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Genero { get; set; }
        public string TrailerUrl { get; set; }
    }
}
