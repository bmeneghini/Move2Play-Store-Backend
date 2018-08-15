using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class ProcedimentosAtendimento
    {
        public int Idprocedimento { get; set; }
        public int Idatendimento { get; set; }
        public int Quantidade { get; set; }

        public Atendimento IdatendimentoNavigation { get; set; }
        public Procedimento IdprocedimentoNavigation { get; set; }
    }
}
