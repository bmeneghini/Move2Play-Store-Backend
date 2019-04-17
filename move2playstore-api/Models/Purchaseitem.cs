namespace move2playstoreAPI.Models
{
    public class Purchaseitem
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public int GameId { get; set; }
        public float Price { get; set; }
    }
}
