using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Procedimento
    {
        public Procedimento()
        {
            ProcedimentosAtendimento = new HashSet<ProcedimentosAtendimento>();
        }

        public int Id { get; set; }
        public int Idtipoatendimento { get; set; }
        public int Codigo { get; set; }
        public int Tabela { get; set; }
        public string Descricao { get; set; }

        public TipoAtendimento IdtipoatendimentoNavigation { get; set; }
        public ICollection<ProcedimentosAtendimento> ProcedimentosAtendimento { get; set; }
    }
}
