using System.Collections.Generic;

namespace move2playstoreAPI.DataTransferObjects
{
    public class TransactionDto
    {
        public string UserId { get; set; }
        public ICollection<TransactionItemDto> Itens { get; set; }
    }
}
