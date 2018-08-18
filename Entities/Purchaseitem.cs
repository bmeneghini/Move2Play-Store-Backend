using System;
using System.Collections.Generic;

namespace Api
{
    public partial class Purchaseitem
    {
        public Purchaseitem()
        {
            Usergame = new HashSet<Usergame>();
        }

        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public int GameId { get; set; }
        public float Price { get; set; }

        public Game Game { get; set; }
        public Purchase Purchase { get; set; }
        public ICollection<Usergame> Usergame { get; set; }
    }
}
