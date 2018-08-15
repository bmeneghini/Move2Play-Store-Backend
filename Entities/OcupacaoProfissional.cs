using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class OcupacaoProfissional
    {
        public int Idocupacao { get; set; }
        public int Idprofissional { get; set; }

        public Ocupacao IdocupacaoNavigation { get; set; }
        public ProfissionalExecutante IdprofissionalNavigation { get; set; }
    }
}
