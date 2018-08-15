using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Ocupacao
    {
        public Ocupacao()
        {
            OcupacaoProfissional = new HashSet<OcupacaoProfissional>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public ICollection<OcupacaoProfissional> OcupacaoProfissional { get; set; }
    }
}
