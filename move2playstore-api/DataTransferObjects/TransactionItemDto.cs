namespace move2playstoreAPI.DataTransferObjects
{
    public class TransactionItemDto
    {
        public int GameId { get; set; }
        public string ItemDescription { get; set; }
        public float ItemAmount { get; set; }
        public int ItemQuantity { get; set; }
    }
}
