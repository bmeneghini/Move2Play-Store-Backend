using System;
using System.Collections.Generic;

namespace move2playstoreAPI.Models
{
    public partial class Purchaseitem
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public int GameId { get; set; }
        public float Price { get; set; }

        public Game Game { get; set; }
        public Purchase Purchase { get; set; }
    }
}
