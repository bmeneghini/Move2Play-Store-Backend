using System;
using System.Collections.Generic;

namespace Api
{
    public partial class Usergame
    {
        public int UserId { get; set; }
        public int PurchaseItemId { get; set; }

        public Purchaseitem PurchaseItem { get; set; }
        public User User { get; set; }
    }
}
