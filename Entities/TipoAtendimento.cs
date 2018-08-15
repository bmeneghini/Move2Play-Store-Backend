using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class TipoAtendimento
    {
        public TipoAtendimento()
        {
            Procedimento = new HashSet<Procedimento>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public ICollection<Procedimento> Procedimento { get; set; }
    }
}
