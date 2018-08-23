using System;
using System.Collections.Generic;

namespace move2playstoreAPI.Models
{
    public partial class Usergame
    {
        public int UserId { get; set; }
        public int PurchaseItemId { get; set; }

        public Purchaseitem PurchaseItem { get; set; }
        public User User { get; set; }
    }
}
