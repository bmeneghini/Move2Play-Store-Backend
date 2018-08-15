using System;
using System.Collections.Generic;
using Api.Models;

namespace Api
{
    public partial class Atendimento
    {
        public Atendimento()
        {
            ProcedimentosAtendimento = new HashSet<ProcedimentosAtendimento>();
        }

        public int Id { get; set; }
        public int Idprestador { get; set; }
        public string Nomebeneficiario { get; set; }
        public string Cns { get; set; }
        public byte? Rn { get; set; }
        public DateTime? Dataatendimento { get; set; }
        public DateTime? Dataautorizacao { get; set; }
        public string Nomesolicitante { get; set; }
        public string Registrosolicitante { get; set; }
        public string Ocupacaosolicitante { get; set; }
        public string Indicacaoclinica { get; set; }
        public string Status { get; set; }

        public Prestador IdprestadorNavigation { get; set; }
        public ICollection<ProcedimentosAtendimento> ProcedimentosAtendimento { get; set; }
    }
}
