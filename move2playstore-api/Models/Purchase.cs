using System;
using System.Collections;
using System.Collections.Generic;

namespace move2playstoreAPI.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int PaymentMethod { get; set; }
        public int PaymentStatus { get; set; }
        public string PaymentToken { get; set; }
        public string PaymentStatusMessage { get; set; }

        public ICollection<Purchaseitem> PurchaseItens { get; set; }
    }
}
