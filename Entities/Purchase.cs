using System;
using System.Collections.Generic;

namespace Api
{
    public partial class Purchase
    {
        public Purchase()
        {
            Purchaseitem = new HashSet<Purchaseitem>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int PaymentMethod { get; set; }
        public sbyte PaymentStatus { get; set; }
        public string PaymentToken { get; set; }
        public string PaymentStatusMessage { get; set; }

        public User User { get; set; }
        public ICollection<Purchaseitem> Purchaseitem { get; set; }
    }
}
